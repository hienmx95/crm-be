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
using CRM.Services.MPhoneType;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.phone_type
{
    public partial class PhoneTypeController : RpcController
    {
        private IStatusService StatusService;
        private IPhoneTypeService PhoneTypeService;
        private ICurrentContext CurrentContext;
        public PhoneTypeController(
            IStatusService StatusService,
            IPhoneTypeService PhoneTypeService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.PhoneTypeService = PhoneTypeService;
            this.CurrentContext = CurrentContext;
        }

        [Route(PhoneTypeRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] PhoneType_PhoneTypeFilterDTO PhoneType_PhoneTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PhoneTypeFilter PhoneTypeFilter = ConvertFilterDTOToFilterEntity(PhoneType_PhoneTypeFilterDTO);
            PhoneTypeFilter = await PhoneTypeService.ToFilter(PhoneTypeFilter);
            int count = await PhoneTypeService.Count(PhoneTypeFilter);
            return count;
        }

        [Route(PhoneTypeRoute.List), HttpPost]
        public async Task<ActionResult<List<PhoneType_PhoneTypeDTO>>> List([FromBody] PhoneType_PhoneTypeFilterDTO PhoneType_PhoneTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PhoneTypeFilter PhoneTypeFilter = ConvertFilterDTOToFilterEntity(PhoneType_PhoneTypeFilterDTO);
            PhoneTypeFilter = await PhoneTypeService.ToFilter(PhoneTypeFilter);
            List<PhoneType> PhoneTypes = await PhoneTypeService.List(PhoneTypeFilter);
            List<PhoneType_PhoneTypeDTO> PhoneType_PhoneTypeDTOs = PhoneTypes
                .Select(c => new PhoneType_PhoneTypeDTO(c)).ToList();
            return PhoneType_PhoneTypeDTOs;
        }

        [Route(PhoneTypeRoute.Get), HttpPost]
        public async Task<ActionResult<PhoneType_PhoneTypeDTO>> Get([FromBody]PhoneType_PhoneTypeDTO PhoneType_PhoneTypeDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(PhoneType_PhoneTypeDTO.Id))
                return Forbid();

            PhoneType PhoneType = await PhoneTypeService.Get(PhoneType_PhoneTypeDTO.Id);
            return new PhoneType_PhoneTypeDTO(PhoneType);
        }

        [Route(PhoneTypeRoute.Create), HttpPost]
        public async Task<ActionResult<PhoneType_PhoneTypeDTO>> Create([FromBody] PhoneType_PhoneTypeDTO PhoneType_PhoneTypeDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(PhoneType_PhoneTypeDTO.Id))
                return Forbid();

            PhoneType PhoneType = ConvertDTOToEntity(PhoneType_PhoneTypeDTO);
            PhoneType = await PhoneTypeService.Create(PhoneType);
            PhoneType_PhoneTypeDTO = new PhoneType_PhoneTypeDTO(PhoneType);
            if (PhoneType.IsValidated)
                return PhoneType_PhoneTypeDTO;
            else
                return BadRequest(PhoneType_PhoneTypeDTO);
        }

        [Route(PhoneTypeRoute.Update), HttpPost]
        public async Task<ActionResult<PhoneType_PhoneTypeDTO>> Update([FromBody] PhoneType_PhoneTypeDTO PhoneType_PhoneTypeDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(PhoneType_PhoneTypeDTO.Id))
                return Forbid();

            PhoneType PhoneType = ConvertDTOToEntity(PhoneType_PhoneTypeDTO);
            PhoneType = await PhoneTypeService.Update(PhoneType);
            PhoneType_PhoneTypeDTO = new PhoneType_PhoneTypeDTO(PhoneType);
            if (PhoneType.IsValidated)
                return PhoneType_PhoneTypeDTO;
            else
                return BadRequest(PhoneType_PhoneTypeDTO);
        }

        [Route(PhoneTypeRoute.Delete), HttpPost]
        public async Task<ActionResult<PhoneType_PhoneTypeDTO>> Delete([FromBody] PhoneType_PhoneTypeDTO PhoneType_PhoneTypeDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(PhoneType_PhoneTypeDTO.Id))
                return Forbid();

            PhoneType PhoneType = ConvertDTOToEntity(PhoneType_PhoneTypeDTO);
            PhoneType = await PhoneTypeService.Delete(PhoneType);
            PhoneType_PhoneTypeDTO = new PhoneType_PhoneTypeDTO(PhoneType);
            if (PhoneType.IsValidated)
                return PhoneType_PhoneTypeDTO;
            else
                return BadRequest(PhoneType_PhoneTypeDTO);
        }
        
        [Route(PhoneTypeRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PhoneTypeFilter PhoneTypeFilter = new PhoneTypeFilter();
            PhoneTypeFilter = await PhoneTypeService.ToFilter(PhoneTypeFilter);
            PhoneTypeFilter.Id = new IdFilter { In = Ids };
            PhoneTypeFilter.Selects = PhoneTypeSelect.Id;
            PhoneTypeFilter.Skip = 0;
            PhoneTypeFilter.Take = int.MaxValue;

            List<PhoneType> PhoneTypes = await PhoneTypeService.List(PhoneTypeFilter);
            PhoneTypes = await PhoneTypeService.BulkDelete(PhoneTypes);
            if (PhoneTypes.Any(x => !x.IsValidated))
                return BadRequest(PhoneTypes.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(PhoneTypeRoute.Import), HttpPost]
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
            List<PhoneType> PhoneTypes = new List<PhoneType>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(PhoneTypes);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int StatusIdColumn = 3 + StartColumn;
                int UsedColumn = 7 + StartColumn;
                int RowIdColumn = 8 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    string RowIdValue = worksheet.Cells[i + StartRow, RowIdColumn].Value?.ToString();
                    
                    PhoneType PhoneType = new PhoneType();
                    PhoneType.Code = CodeValue;
                    PhoneType.Name = NameValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    PhoneType.StatusId = Status == null ? 0 : Status.Id;
                    PhoneType.Status = Status;
                    
                    PhoneTypes.Add(PhoneType);
                }
            }
            PhoneTypes = await PhoneTypeService.Import(PhoneTypes);
            if (PhoneTypes.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < PhoneTypes.Count; i++)
                {
                    PhoneType PhoneType = PhoneTypes[i];
                    if (!PhoneType.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (PhoneType.Errors.ContainsKey(nameof(PhoneType.Id)))
                            Error += PhoneType.Errors[nameof(PhoneType.Id)];
                        if (PhoneType.Errors.ContainsKey(nameof(PhoneType.Code)))
                            Error += PhoneType.Errors[nameof(PhoneType.Code)];
                        if (PhoneType.Errors.ContainsKey(nameof(PhoneType.Name)))
                            Error += PhoneType.Errors[nameof(PhoneType.Name)];
                        if (PhoneType.Errors.ContainsKey(nameof(PhoneType.StatusId)))
                            Error += PhoneType.Errors[nameof(PhoneType.StatusId)];
                        if (PhoneType.Errors.ContainsKey(nameof(PhoneType.Used)))
                            Error += PhoneType.Errors[nameof(PhoneType.Used)];
                        if (PhoneType.Errors.ContainsKey(nameof(PhoneType.RowId)))
                            Error += PhoneType.Errors[nameof(PhoneType.RowId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(PhoneTypeRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] PhoneType_PhoneTypeFilterDTO PhoneType_PhoneTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region PhoneType
                var PhoneTypeFilter = ConvertFilterDTOToFilterEntity(PhoneType_PhoneTypeFilterDTO);
                PhoneTypeFilter.Skip = 0;
                PhoneTypeFilter.Take = int.MaxValue;
                PhoneTypeFilter = await PhoneTypeService.ToFilter(PhoneTypeFilter);
                List<PhoneType> PhoneTypes = await PhoneTypeService.List(PhoneTypeFilter);

                var PhoneTypeHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "StatusId",
                        "Used",
                        "RowId",
                    }
                };
                List<object[]> PhoneTypeData = new List<object[]>();
                for (int i = 0; i < PhoneTypes.Count; i++)
                {
                    var PhoneType = PhoneTypes[i];
                    PhoneTypeData.Add(new Object[]
                    {
                        PhoneType.Id,
                        PhoneType.Code,
                        PhoneType.Name,
                        PhoneType.StatusId,
                        PhoneType.Used,
                        PhoneType.RowId,
                    });
                }
                excel.GenerateWorksheet("PhoneType", PhoneTypeHeaders, PhoneTypeData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "PhoneType.xlsx");
        }

        [Route(PhoneTypeRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] PhoneType_PhoneTypeFilterDTO PhoneType_PhoneTypeFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/PhoneType_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "PhoneType.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            PhoneTypeFilter PhoneTypeFilter = new PhoneTypeFilter();
            PhoneTypeFilter = await PhoneTypeService.ToFilter(PhoneTypeFilter);
            if (Id == 0)
            {

            }
            else
            {
                PhoneTypeFilter.Id = new IdFilter { Equal = Id };
                int count = await PhoneTypeService.Count(PhoneTypeFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private PhoneType ConvertDTOToEntity(PhoneType_PhoneTypeDTO PhoneType_PhoneTypeDTO)
        {
            PhoneType PhoneType = new PhoneType();
            PhoneType.Id = PhoneType_PhoneTypeDTO.Id;
            PhoneType.Code = PhoneType_PhoneTypeDTO.Code;
            PhoneType.Name = PhoneType_PhoneTypeDTO.Name;
            PhoneType.StatusId = PhoneType_PhoneTypeDTO.StatusId;
            PhoneType.Used = PhoneType_PhoneTypeDTO.Used;
            PhoneType.RowId = PhoneType_PhoneTypeDTO.RowId;
            PhoneType.Status = PhoneType_PhoneTypeDTO.Status == null ? null : new Status
            {
                Id = PhoneType_PhoneTypeDTO.Status.Id,
                Code = PhoneType_PhoneTypeDTO.Status.Code,
                Name = PhoneType_PhoneTypeDTO.Status.Name,
            };
            PhoneType.BaseLanguage = CurrentContext.Language;
            return PhoneType;
        }

        private PhoneTypeFilter ConvertFilterDTOToFilterEntity(PhoneType_PhoneTypeFilterDTO PhoneType_PhoneTypeFilterDTO)
        {
            PhoneTypeFilter PhoneTypeFilter = new PhoneTypeFilter();
            PhoneTypeFilter.Selects = PhoneTypeSelect.ALL;
            PhoneTypeFilter.Skip = PhoneType_PhoneTypeFilterDTO.Skip;
            PhoneTypeFilter.Take = PhoneType_PhoneTypeFilterDTO.Take;
            PhoneTypeFilter.OrderBy = PhoneType_PhoneTypeFilterDTO.OrderBy;
            PhoneTypeFilter.OrderType = PhoneType_PhoneTypeFilterDTO.OrderType;

            PhoneTypeFilter.Id = PhoneType_PhoneTypeFilterDTO.Id;
            PhoneTypeFilter.Code = PhoneType_PhoneTypeFilterDTO.Code;
            PhoneTypeFilter.Name = PhoneType_PhoneTypeFilterDTO.Name;
            PhoneTypeFilter.StatusId = PhoneType_PhoneTypeFilterDTO.StatusId;
            PhoneTypeFilter.RowId = PhoneType_PhoneTypeFilterDTO.RowId;
            PhoneTypeFilter.CreatedAt = PhoneType_PhoneTypeFilterDTO.CreatedAt;
            PhoneTypeFilter.UpdatedAt = PhoneType_PhoneTypeFilterDTO.UpdatedAt;
            return PhoneTypeFilter;
        }
    }
}

