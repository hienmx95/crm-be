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
using CRM.Entities;
using CRM.Services.MTicketType;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.ticket_type
{
    public class TicketTypeController : RpcController
    {
        private IStatusService StatusService;
        private ITicketTypeService TicketTypeService;
        private ICurrentContext CurrentContext;
        public TicketTypeController(
            IStatusService StatusService,
            ITicketTypeService TicketTypeService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.TicketTypeService = TicketTypeService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketTypeRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] TicketType_TicketTypeFilterDTO TicketType_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = ConvertFilterDTOToFilterEntity(TicketType_TicketTypeFilterDTO);
            TicketTypeFilter = TicketTypeService.ToFilter(TicketTypeFilter);
            int count = await TicketTypeService.Count(TicketTypeFilter);
            return count;
        }

        [Route(TicketTypeRoute.List), HttpPost]
        public async Task<ActionResult<List<TicketType_TicketTypeDTO>>> List([FromBody] TicketType_TicketTypeFilterDTO TicketType_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = ConvertFilterDTOToFilterEntity(TicketType_TicketTypeFilterDTO);
            TicketTypeFilter = TicketTypeService.ToFilter(TicketTypeFilter);
            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<TicketType_TicketTypeDTO> TicketType_TicketTypeDTOs = TicketTypes
                .Select(c => new TicketType_TicketTypeDTO(c)).ToList();
            return TicketType_TicketTypeDTOs;
        }

        [Route(TicketTypeRoute.Get), HttpPost]
        public async Task<ActionResult<TicketType_TicketTypeDTO>> Get([FromBody]TicketType_TicketTypeDTO TicketType_TicketTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketType_TicketTypeDTO.Id))
                return Forbid();

            TicketType TicketType = await TicketTypeService.Get(TicketType_TicketTypeDTO.Id);
            return new TicketType_TicketTypeDTO(TicketType);
        }

        [Route(TicketTypeRoute.Create), HttpPost]
        public async Task<ActionResult<TicketType_TicketTypeDTO>> Create([FromBody] TicketType_TicketTypeDTO TicketType_TicketTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketType_TicketTypeDTO.Id))
                return Forbid();

            TicketType TicketType = ConvertDTOToEntity(TicketType_TicketTypeDTO);
            TicketType = await TicketTypeService.Create(TicketType);
            TicketType_TicketTypeDTO = new TicketType_TicketTypeDTO(TicketType);
            if (TicketType.IsValidated)
                return TicketType_TicketTypeDTO;
            else
                return BadRequest(TicketType_TicketTypeDTO);
        }

        [Route(TicketTypeRoute.Update), HttpPost]
        public async Task<ActionResult<TicketType_TicketTypeDTO>> Update([FromBody] TicketType_TicketTypeDTO TicketType_TicketTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketType_TicketTypeDTO.Id))
                return Forbid();

            TicketType TicketType = ConvertDTOToEntity(TicketType_TicketTypeDTO);
            TicketType = await TicketTypeService.Update(TicketType);
            TicketType_TicketTypeDTO = new TicketType_TicketTypeDTO(TicketType);
            if (TicketType.IsValidated)
                return TicketType_TicketTypeDTO;
            else
                return BadRequest(TicketType_TicketTypeDTO);
        }

        [Route(TicketTypeRoute.Delete), HttpPost]
        public async Task<ActionResult<TicketType_TicketTypeDTO>> Delete([FromBody] TicketType_TicketTypeDTO TicketType_TicketTypeDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketType_TicketTypeDTO.Id))
                return Forbid();

            TicketType TicketType = ConvertDTOToEntity(TicketType_TicketTypeDTO);
            TicketType = await TicketTypeService.Delete(TicketType);
            TicketType_TicketTypeDTO = new TicketType_TicketTypeDTO(TicketType);
            if (TicketType.IsValidated)
                return TicketType_TicketTypeDTO;
            else
                return BadRequest(TicketType_TicketTypeDTO);
        }
        
        [Route(TicketTypeRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter = TicketTypeService.ToFilter(TicketTypeFilter);
            TicketTypeFilter.Id = new IdFilter { In = Ids };
            TicketTypeFilter.Selects = TicketTypeSelect.Id;
            TicketTypeFilter.Skip = 0;
            TicketTypeFilter.Take = int.MaxValue;

            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            TicketTypes = await TicketTypeService.BulkDelete(TicketTypes);
            return true;
        }
        
        [Route(TicketTypeRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<TicketType> TicketTypes = new List<TicketType>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(TicketTypes);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int ColorCodeColumn = 3 + StartColumn;
                int StatusIdColumn = 4 + StartColumn;
                int UsedColumn = 8 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string ColorCodeValue = worksheet.Cells[i + StartRow, ColorCodeColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    
                    TicketType TicketType = new TicketType();
                    TicketType.Code = CodeValue;
                    TicketType.Name = NameValue;
                    TicketType.ColorCode = ColorCodeValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    TicketType.StatusId = Status == null ? 0 : Status.Id;
                    TicketType.Status = Status;
                    
                    TicketTypes.Add(TicketType);
                }
            }
            TicketTypes = await TicketTypeService.Import(TicketTypes);
            if (TicketTypes.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < TicketTypes.Count; i++)
                {
                    TicketType TicketType = TicketTypes[i];
                    if (!TicketType.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (TicketType.Errors.ContainsKey(nameof(TicketType.Id)))
                            Error += TicketType.Errors[nameof(TicketType.Id)];
                        if (TicketType.Errors.ContainsKey(nameof(TicketType.Code)))
                            Error += TicketType.Errors[nameof(TicketType.Code)];
                        if (TicketType.Errors.ContainsKey(nameof(TicketType.Name)))
                            Error += TicketType.Errors[nameof(TicketType.Name)];
                        if (TicketType.Errors.ContainsKey(nameof(TicketType.ColorCode)))
                            Error += TicketType.Errors[nameof(TicketType.ColorCode)];
                        if (TicketType.Errors.ContainsKey(nameof(TicketType.StatusId)))
                            Error += TicketType.Errors[nameof(TicketType.StatusId)];
                        if (TicketType.Errors.ContainsKey(nameof(TicketType.Used)))
                            Error += TicketType.Errors[nameof(TicketType.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(TicketTypeRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] TicketType_TicketTypeFilterDTO TicketType_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketType
                var TicketTypeFilter = ConvertFilterDTOToFilterEntity(TicketType_TicketTypeFilterDTO);
                TicketTypeFilter.Skip = 0;
                TicketTypeFilter.Take = int.MaxValue;
                TicketTypeFilter = TicketTypeService.ToFilter(TicketTypeFilter);
                List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);

                var TicketTypeHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "ColorCode",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketTypeData = new List<object[]>();
                for (int i = 0; i < TicketTypes.Count; i++)
                {
                    var TicketType = TicketTypes[i];
                    TicketTypeData.Add(new Object[]
                    {
                        TicketType.Id,
                        TicketType.Code,
                        TicketType.Name,
                        TicketType.ColorCode,
                        TicketType.StatusId,
                        TicketType.Used,
                    });
                }
                excel.GenerateWorksheet("TicketType", TicketTypeHeaders, TicketTypeData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketType.xlsx");
        }

        [Route(TicketTypeRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] TicketType_TicketTypeFilterDTO TicketType_TicketTypeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketType
                var TicketTypeHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "ColorCode",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketTypeData = new List<object[]>();
                excel.GenerateWorksheet("TicketType", TicketTypeHeaders, TicketTypeData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketType.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter = TicketTypeService.ToFilter(TicketTypeFilter);
            if (Id == 0)
            {

            }
            else
            {
                TicketTypeFilter.Id = new IdFilter { Equal = Id };
                int count = await TicketTypeService.Count(TicketTypeFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private TicketType ConvertDTOToEntity(TicketType_TicketTypeDTO TicketType_TicketTypeDTO)
        {
            TicketType TicketType = new TicketType();
            TicketType.Id = TicketType_TicketTypeDTO.Id;
            TicketType.Code = TicketType_TicketTypeDTO.Code;
            TicketType.Name = TicketType_TicketTypeDTO.Name;
            TicketType.ColorCode = TicketType_TicketTypeDTO.ColorCode;
            TicketType.StatusId = TicketType_TicketTypeDTO.StatusId;
            TicketType.Used = TicketType_TicketTypeDTO.Used;
            TicketType.Status = TicketType_TicketTypeDTO.Status == null ? null : new Status
            {
                Id = TicketType_TicketTypeDTO.Status.Id,
                Code = TicketType_TicketTypeDTO.Status.Code,
                Name = TicketType_TicketTypeDTO.Status.Name,
            };
            TicketType.BaseLanguage = CurrentContext.Language;
            return TicketType;
        }

        private TicketTypeFilter ConvertFilterDTOToFilterEntity(TicketType_TicketTypeFilterDTO TicketType_TicketTypeFilterDTO)
        {
            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter();
            TicketTypeFilter.Selects = TicketTypeSelect.ALL;
            TicketTypeFilter.Skip = TicketType_TicketTypeFilterDTO.Skip;
            TicketTypeFilter.Take = TicketType_TicketTypeFilterDTO.Take;
            TicketTypeFilter.OrderBy = TicketType_TicketTypeFilterDTO.OrderBy;
            TicketTypeFilter.OrderType = TicketType_TicketTypeFilterDTO.OrderType;

            TicketTypeFilter.Id = TicketType_TicketTypeFilterDTO.Id;
            TicketTypeFilter.Code = TicketType_TicketTypeFilterDTO.Code;
            TicketTypeFilter.Name = TicketType_TicketTypeFilterDTO.Name;
            TicketTypeFilter.ColorCode = TicketType_TicketTypeFilterDTO.ColorCode;
            TicketTypeFilter.StatusId = TicketType_TicketTypeFilterDTO.StatusId;
            TicketTypeFilter.CreatedAt = TicketType_TicketTypeFilterDTO.CreatedAt;
            TicketTypeFilter.UpdatedAt = TicketType_TicketTypeFilterDTO.UpdatedAt;
            return TicketTypeFilter;
        }

        [Route(TicketTypeRoute.FilterListStatus), HttpPost]
        public async Task<List<TicketType_StatusDTO>> FilterListStatus([FromBody] TicketType_StatusFilterDTO TicketType_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<TicketType_StatusDTO> TicketType_StatusDTOs = Statuses
                .Select(x => new TicketType_StatusDTO(x)).ToList();
            return TicketType_StatusDTOs;
        }

        [Route(TicketTypeRoute.SingleListStatus), HttpPost]
        public async Task<List<TicketType_StatusDTO>> SingleListStatus([FromBody] TicketType_StatusFilterDTO TicketType_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = int.MaxValue;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<TicketType_StatusDTO> TicketType_StatusDTOs = Statuses
                .Select(x => new TicketType_StatusDTO(x)).ToList();
            return TicketType_StatusDTOs;
        }

    }
}

