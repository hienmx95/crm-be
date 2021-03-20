using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using System.Dynamic;
using CRM.Entities;
using CRM.Services.MProfession;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.profession
{
    public partial class ProfessionController : RpcController
    {
        private IStatusService StatusService;
        private IProfessionService ProfessionService;
        private ICurrentContext CurrentContext;
        public ProfessionController(
            IStatusService StatusService,
            IProfessionService ProfessionService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.ProfessionService = ProfessionService;
            this.CurrentContext = CurrentContext;
        }

        [Route(ProfessionRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Profession_ProfessionFilterDTO Profession_ProfessionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProfessionFilter ProfessionFilter = ConvertFilterDTOToFilterEntity(Profession_ProfessionFilterDTO);
            ProfessionFilter = await ProfessionService.ToFilter(ProfessionFilter);
            int count = await ProfessionService.Count(ProfessionFilter);
            return count;
        }

        [Route(ProfessionRoute.List), HttpPost]
        public async Task<ActionResult<List<Profession_ProfessionDTO>>> List([FromBody] Profession_ProfessionFilterDTO Profession_ProfessionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProfessionFilter ProfessionFilter = ConvertFilterDTOToFilterEntity(Profession_ProfessionFilterDTO);
            ProfessionFilter = await ProfessionService.ToFilter(ProfessionFilter);
            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            List<Profession_ProfessionDTO> Profession_ProfessionDTOs = Professions
                .Select(c => new Profession_ProfessionDTO(c)).ToList();
            return Profession_ProfessionDTOs;
        }

        [Route(ProfessionRoute.Get), HttpPost]
        public async Task<ActionResult<Profession_ProfessionDTO>> Get([FromBody]Profession_ProfessionDTO Profession_ProfessionDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Profession_ProfessionDTO.Id))
                return Forbid();

            Profession Profession = await ProfessionService.Get(Profession_ProfessionDTO.Id);
            return new Profession_ProfessionDTO(Profession);
        }

        [Route(ProfessionRoute.Create), HttpPost]
        public async Task<ActionResult<Profession_ProfessionDTO>> Create([FromBody] Profession_ProfessionDTO Profession_ProfessionDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Profession_ProfessionDTO.Id))
                return Forbid();

            Profession Profession = ConvertDTOToEntity(Profession_ProfessionDTO);
            Profession = await ProfessionService.Create(Profession);
            Profession_ProfessionDTO = new Profession_ProfessionDTO(Profession);
            if (Profession.IsValidated)
                return Profession_ProfessionDTO;
            else
                return BadRequest(Profession_ProfessionDTO);
        }

        [Route(ProfessionRoute.Update), HttpPost]
        public async Task<ActionResult<Profession_ProfessionDTO>> Update([FromBody] Profession_ProfessionDTO Profession_ProfessionDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Profession_ProfessionDTO.Id))
                return Forbid();

            Profession Profession = ConvertDTOToEntity(Profession_ProfessionDTO);
            Profession = await ProfessionService.Update(Profession);
            Profession_ProfessionDTO = new Profession_ProfessionDTO(Profession);
            if (Profession.IsValidated)
                return Profession_ProfessionDTO;
            else
                return BadRequest(Profession_ProfessionDTO);
        }

        [Route(ProfessionRoute.Delete), HttpPost]
        public async Task<ActionResult<Profession_ProfessionDTO>> Delete([FromBody] Profession_ProfessionDTO Profession_ProfessionDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Profession_ProfessionDTO.Id))
                return Forbid();

            Profession Profession = ConvertDTOToEntity(Profession_ProfessionDTO);
            Profession = await ProfessionService.Delete(Profession);
            Profession_ProfessionDTO = new Profession_ProfessionDTO(Profession);
            if (Profession.IsValidated)
                return Profession_ProfessionDTO;
            else
                return BadRequest(Profession_ProfessionDTO);
        }
        
        [Route(ProfessionRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProfessionFilter ProfessionFilter = new ProfessionFilter();
            ProfessionFilter = await ProfessionService.ToFilter(ProfessionFilter);
            ProfessionFilter.Id = new IdFilter { In = Ids };
            ProfessionFilter.Selects = ProfessionSelect.Id;
            ProfessionFilter.Skip = 0;
            ProfessionFilter.Take = int.MaxValue;

            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            Professions = await ProfessionService.BulkDelete(Professions);
            if (Professions.Any(x => !x.IsValidated))
                return BadRequest(Professions.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(ProfessionRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Profession> Professions = new List<Profession>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(Professions);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int StatusIdColumn = 3 + StartColumn;
                int RowIdColumn = 7 + StartColumn;
                int UsedColumn = 8 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string RowIdValue = worksheet.Cells[i + StartRow, RowIdColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    
                    Profession Profession = new Profession();
                    Profession.Code = CodeValue;
                    Profession.Name = NameValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    Profession.StatusId = Status == null ? 0 : Status.Id;
                    Profession.Status = Status;
                    
                    Professions.Add(Profession);
                }
            }
            Professions = await ProfessionService.Import(Professions);
            if (Professions.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < Professions.Count; i++)
                {
                    Profession Profession = Professions[i];
                    if (!Profession.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (Profession.Errors.ContainsKey(nameof(Profession.Id)))
                            Error += Profession.Errors[nameof(Profession.Id)];
                        if (Profession.Errors.ContainsKey(nameof(Profession.Code)))
                            Error += Profession.Errors[nameof(Profession.Code)];
                        if (Profession.Errors.ContainsKey(nameof(Profession.Name)))
                            Error += Profession.Errors[nameof(Profession.Name)];
                        if (Profession.Errors.ContainsKey(nameof(Profession.StatusId)))
                            Error += Profession.Errors[nameof(Profession.StatusId)];
                        if (Profession.Errors.ContainsKey(nameof(Profession.RowId)))
                            Error += Profession.Errors[nameof(Profession.RowId)];
                        if (Profession.Errors.ContainsKey(nameof(Profession.Used)))
                            Error += Profession.Errors[nameof(Profession.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(ProfessionRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] Profession_ProfessionFilterDTO Profession_ProfessionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region Profession
                var ProfessionFilter = ConvertFilterDTOToFilterEntity(Profession_ProfessionFilterDTO);
                ProfessionFilter.Skip = 0;
                ProfessionFilter.Take = int.MaxValue;
                ProfessionFilter = await ProfessionService.ToFilter(ProfessionFilter);
                List<Profession> Professions = await ProfessionService.List(ProfessionFilter);

                var ProfessionHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "StatusId",
                        "RowId",
                        "Used",
                    }
                };
                List<object[]> ProfessionData = new List<object[]>();
                for (int i = 0; i < Professions.Count; i++)
                {
                    var Profession = Professions[i];
                    ProfessionData.Add(new Object[]
                    {
                        Profession.Id,
                        Profession.Code,
                        Profession.Name,
                        Profession.StatusId,
                        Profession.RowId,
                        Profession.Used,
                    });
                }
                excel.GenerateWorksheet("Profession", ProfessionHeaders, ProfessionData);
                #endregion
                
                #region Status
                var StatusFilter = new StatusFilter();
                StatusFilter.Selects = StatusSelect.ALL;
                StatusFilter.OrderBy = StatusOrder.Id;
                StatusFilter.OrderType = OrderType.ASC;
                StatusFilter.Skip = 0;
                StatusFilter.Take = int.MaxValue;
                List<Status> Statuses = await StatusService.List(StatusFilter);

                var StatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> StatusData = new List<object[]>();
                for (int i = 0; i < Statuses.Count; i++)
                {
                    var Status = Statuses[i];
                    StatusData.Add(new Object[]
                    {
                        Status.Id,
                        Status.Code,
                        Status.Name,
                    });
                }
                excel.GenerateWorksheet("Status", StatusHeaders, StatusData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "Profession.xlsx");
        }

        [Route(ProfessionRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] Profession_ProfessionFilterDTO Profession_ProfessionFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/Profession_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "Profession.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            ProfessionFilter ProfessionFilter = new ProfessionFilter();
            ProfessionFilter = await ProfessionService.ToFilter(ProfessionFilter);
            if (Id == 0)
            {

            }
            else
            {
                ProfessionFilter.Id = new IdFilter { Equal = Id };
                int count = await ProfessionService.Count(ProfessionFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Profession ConvertDTOToEntity(Profession_ProfessionDTO Profession_ProfessionDTO)
        {
            Profession Profession = new Profession();
            Profession.Id = Profession_ProfessionDTO.Id;
            Profession.Code = Profession_ProfessionDTO.Code;
            Profession.Name = Profession_ProfessionDTO.Name;
            Profession.StatusId = Profession_ProfessionDTO.StatusId;
            Profession.RowId = Profession_ProfessionDTO.RowId;
            Profession.Used = Profession_ProfessionDTO.Used;
            Profession.Status = Profession_ProfessionDTO.Status == null ? null : new Status
            {
                Id = Profession_ProfessionDTO.Status.Id,
                Code = Profession_ProfessionDTO.Status.Code,
                Name = Profession_ProfessionDTO.Status.Name,
            };
            Profession.BaseLanguage = CurrentContext.Language;
            return Profession;
        }

        private ProfessionFilter ConvertFilterDTOToFilterEntity(Profession_ProfessionFilterDTO Profession_ProfessionFilterDTO)
        {
            ProfessionFilter ProfessionFilter = new ProfessionFilter();
            ProfessionFilter.Selects = ProfessionSelect.ALL;
            ProfessionFilter.Skip = Profession_ProfessionFilterDTO.Skip;
            ProfessionFilter.Take = Profession_ProfessionFilterDTO.Take;
            ProfessionFilter.OrderBy = Profession_ProfessionFilterDTO.OrderBy;
            ProfessionFilter.OrderType = Profession_ProfessionFilterDTO.OrderType;

            ProfessionFilter.Id = Profession_ProfessionFilterDTO.Id;
            ProfessionFilter.Code = Profession_ProfessionFilterDTO.Code;
            ProfessionFilter.Name = Profession_ProfessionFilterDTO.Name;
            ProfessionFilter.StatusId = Profession_ProfessionFilterDTO.StatusId;
            ProfessionFilter.RowId = Profession_ProfessionFilterDTO.RowId;
            ProfessionFilter.CreatedAt = Profession_ProfessionFilterDTO.CreatedAt;
            ProfessionFilter.UpdatedAt = Profession_ProfessionFilterDTO.UpdatedAt;
            return ProfessionFilter;
        }
    }
}

