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
using CRM.Services.MTicketStatus;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.ticket_status
{
    public partial class TicketStatusController : RpcController
    {
        private IStatusService StatusService;
        private ITicketStatusService TicketStatusService;
        private ICurrentContext CurrentContext;
        public TicketStatusController(
            IStatusService StatusService,
            ITicketStatusService TicketStatusService,
            ICurrentContext CurrentContext
        ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.TicketStatusService = TicketStatusService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketStatusRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] TicketStatus_TicketStatusFilterDTO TicketStatus_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = ConvertFilterDTOToFilterEntity(TicketStatus_TicketStatusFilterDTO);
            TicketStatusFilter = TicketStatusService.ToFilter(TicketStatusFilter);
            int count = await TicketStatusService.Count(TicketStatusFilter);
            return count;
        }

        [Route(TicketStatusRoute.List), HttpPost]
        public async Task<ActionResult<List<TicketStatus_TicketStatusDTO>>> List([FromBody] TicketStatus_TicketStatusFilterDTO TicketStatus_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = ConvertFilterDTOToFilterEntity(TicketStatus_TicketStatusFilterDTO);
            TicketStatusFilter = TicketStatusService.ToFilter(TicketStatusFilter);
            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            List<TicketStatus_TicketStatusDTO> TicketStatus_TicketStatusDTOs = TicketStatuses
                .Select(c => new TicketStatus_TicketStatusDTO(c)).ToList();
            return TicketStatus_TicketStatusDTOs;
        }

        [Route(TicketStatusRoute.Get), HttpPost]
        public async Task<ActionResult<TicketStatus_TicketStatusDTO>> Get([FromBody]TicketStatus_TicketStatusDTO TicketStatus_TicketStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketStatus_TicketStatusDTO.Id))
                return Forbid();

            TicketStatus TicketStatus = await TicketStatusService.Get(TicketStatus_TicketStatusDTO.Id);
            return new TicketStatus_TicketStatusDTO(TicketStatus);
        }

        [Route(TicketStatusRoute.Create), HttpPost]
        public async Task<ActionResult<TicketStatus_TicketStatusDTO>> Create([FromBody] TicketStatus_TicketStatusDTO TicketStatus_TicketStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketStatus_TicketStatusDTO.Id))
                return Forbid();

            TicketStatus TicketStatus = ConvertDTOToEntity(TicketStatus_TicketStatusDTO);
            TicketStatus = await TicketStatusService.Create(TicketStatus);
            TicketStatus_TicketStatusDTO = new TicketStatus_TicketStatusDTO(TicketStatus);
            if (TicketStatus.IsValidated)
                return TicketStatus_TicketStatusDTO;
            else
                return BadRequest(TicketStatus_TicketStatusDTO);
        }

        [Route(TicketStatusRoute.Update), HttpPost]
        public async Task<ActionResult<TicketStatus_TicketStatusDTO>> Update([FromBody] TicketStatus_TicketStatusDTO TicketStatus_TicketStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketStatus_TicketStatusDTO.Id))
                return Forbid();

            TicketStatus TicketStatus = ConvertDTOToEntity(TicketStatus_TicketStatusDTO);
            TicketStatus = await TicketStatusService.Update(TicketStatus);
            TicketStatus_TicketStatusDTO = new TicketStatus_TicketStatusDTO(TicketStatus);
            if (TicketStatus.IsValidated)
                return TicketStatus_TicketStatusDTO;
            else
                return BadRequest(TicketStatus_TicketStatusDTO);
        }

        [Route(TicketStatusRoute.Delete), HttpPost]
        public async Task<ActionResult<TicketStatus_TicketStatusDTO>> Delete([FromBody] TicketStatus_TicketStatusDTO TicketStatus_TicketStatusDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketStatus_TicketStatusDTO.Id))
                return Forbid();

            TicketStatus TicketStatus = ConvertDTOToEntity(TicketStatus_TicketStatusDTO);
            TicketStatus = await TicketStatusService.Delete(TicketStatus);
            TicketStatus_TicketStatusDTO = new TicketStatus_TicketStatusDTO(TicketStatus);
            if (TicketStatus.IsValidated)
                return TicketStatus_TicketStatusDTO;
            else
                return BadRequest(TicketStatus_TicketStatusDTO);
        }
        
        [Route(TicketStatusRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter = TicketStatusService.ToFilter(TicketStatusFilter);
            TicketStatusFilter.Id = new IdFilter { In = Ids };
            TicketStatusFilter.Selects = TicketStatusSelect.Id;
            TicketStatusFilter.Skip = 0;
            TicketStatusFilter.Take = int.MaxValue;

            List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);
            TicketStatuses = await TicketStatusService.BulkDelete(TicketStatuses);
            if (TicketStatuses.Any(x => !x.IsValidated))
                return BadRequest(TicketStatuses.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(TicketStatusRoute.Import), HttpPost]
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
            List<TicketStatus> TicketStatuses = new List<TicketStatus>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(TicketStatuses);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NameColumn = 1 + StartColumn;
                int OrderNumberColumn = 2 + StartColumn;
                int ColorCodeColumn = 3 + StartColumn;
                int StatusIdColumn = 4 + StartColumn;
                int UsedColumn = 8 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string OrderNumberValue = worksheet.Cells[i + StartRow, OrderNumberColumn].Value?.ToString();
                    string ColorCodeValue = worksheet.Cells[i + StartRow, ColorCodeColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    
                    TicketStatus TicketStatus = new TicketStatus();
                    TicketStatus.Name = NameValue;
                    TicketStatus.OrderNumber = long.TryParse(OrderNumberValue, out long OrderNumber) ? OrderNumber : 0;
                    TicketStatus.ColorCode = ColorCodeValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    TicketStatus.StatusId = Status == null ? 0 : Status.Id;
                    TicketStatus.Status = Status;
                    
                    TicketStatuses.Add(TicketStatus);
                }
            }
            TicketStatuses = await TicketStatusService.Import(TicketStatuses);
            if (TicketStatuses.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < TicketStatuses.Count; i++)
                {
                    TicketStatus TicketStatus = TicketStatuses[i];
                    if (!TicketStatus.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (TicketStatus.Errors.ContainsKey(nameof(TicketStatus.Id)))
                            Error += TicketStatus.Errors[nameof(TicketStatus.Id)];
                        if (TicketStatus.Errors.ContainsKey(nameof(TicketStatus.Name)))
                            Error += TicketStatus.Errors[nameof(TicketStatus.Name)];
                        if (TicketStatus.Errors.ContainsKey(nameof(TicketStatus.OrderNumber)))
                            Error += TicketStatus.Errors[nameof(TicketStatus.OrderNumber)];
                        if (TicketStatus.Errors.ContainsKey(nameof(TicketStatus.ColorCode)))
                            Error += TicketStatus.Errors[nameof(TicketStatus.ColorCode)];
                        if (TicketStatus.Errors.ContainsKey(nameof(TicketStatus.StatusId)))
                            Error += TicketStatus.Errors[nameof(TicketStatus.StatusId)];
                        if (TicketStatus.Errors.ContainsKey(nameof(TicketStatus.Used)))
                            Error += TicketStatus.Errors[nameof(TicketStatus.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(TicketStatusRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] TicketStatus_TicketStatusFilterDTO TicketStatus_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketStatus
                var TicketStatusFilter = ConvertFilterDTOToFilterEntity(TicketStatus_TicketStatusFilterDTO);
                TicketStatusFilter.Skip = 0;
                TicketStatusFilter.Take = int.MaxValue;
                TicketStatusFilter = TicketStatusService.ToFilter(TicketStatusFilter);
                List<TicketStatus> TicketStatuses = await TicketStatusService.List(TicketStatusFilter);

                var TicketStatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "OrderNumber",
                        "ColorCode",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketStatusData = new List<object[]>();
                for (int i = 0; i < TicketStatuses.Count; i++)
                {
                    var TicketStatus = TicketStatuses[i];
                    TicketStatusData.Add(new Object[]
                    {
                        TicketStatus.Id,
                        TicketStatus.Name,
                        TicketStatus.OrderNumber,
                        TicketStatus.ColorCode,
                        TicketStatus.StatusId,
                        TicketStatus.Used,
                    });
                }
                excel.GenerateWorksheet("TicketStatus", TicketStatusHeaders, TicketStatusData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketStatus.xlsx");
        }

        [Route(TicketStatusRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] TicketStatus_TicketStatusFilterDTO TicketStatus_TicketStatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketStatus
                var TicketStatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "OrderNumber",
                        "ColorCode",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketStatusData = new List<object[]>();
                excel.GenerateWorksheet("TicketStatus", TicketStatusHeaders, TicketStatusData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketStatus.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter = TicketStatusService.ToFilter(TicketStatusFilter);
            if (Id == 0)
            {

            }
            else
            {
                TicketStatusFilter.Id = new IdFilter { Equal = Id };
                int count = await TicketStatusService.Count(TicketStatusFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private TicketStatus ConvertDTOToEntity(TicketStatus_TicketStatusDTO TicketStatus_TicketStatusDTO)
        {
            TicketStatus TicketStatus = new TicketStatus();
            TicketStatus.Id = TicketStatus_TicketStatusDTO.Id;
            TicketStatus.Name = TicketStatus_TicketStatusDTO.Name;
            TicketStatus.OrderNumber = TicketStatus_TicketStatusDTO.OrderNumber;
            TicketStatus.ColorCode = TicketStatus_TicketStatusDTO.ColorCode;
            TicketStatus.StatusId = TicketStatus_TicketStatusDTO.StatusId;
            TicketStatus.Used = TicketStatus_TicketStatusDTO.Used;
            TicketStatus.Status = TicketStatus_TicketStatusDTO.Status == null ? null : new Status
            {
                Id = TicketStatus_TicketStatusDTO.Status.Id,
                Code = TicketStatus_TicketStatusDTO.Status.Code,
                Name = TicketStatus_TicketStatusDTO.Status.Name,
            };
            TicketStatus.BaseLanguage = CurrentContext.Language;
            return TicketStatus;
        }

        private TicketStatusFilter ConvertFilterDTOToFilterEntity(TicketStatus_TicketStatusFilterDTO TicketStatus_TicketStatusFilterDTO)
        {
            TicketStatusFilter TicketStatusFilter = new TicketStatusFilter();
            TicketStatusFilter.Selects = TicketStatusSelect.ALL;
            TicketStatusFilter.Skip = TicketStatus_TicketStatusFilterDTO.Skip;
            TicketStatusFilter.Take = TicketStatus_TicketStatusFilterDTO.Take;
            TicketStatusFilter.OrderBy = TicketStatus_TicketStatusFilterDTO.OrderBy;
            TicketStatusFilter.OrderType = TicketStatus_TicketStatusFilterDTO.OrderType;

            TicketStatusFilter.Id = TicketStatus_TicketStatusFilterDTO.Id;
            TicketStatusFilter.Name = TicketStatus_TicketStatusFilterDTO.Name;
            TicketStatusFilter.OrderNumber = TicketStatus_TicketStatusFilterDTO.OrderNumber;
            TicketStatusFilter.ColorCode = TicketStatus_TicketStatusFilterDTO.ColorCode;
            TicketStatusFilter.StatusId = TicketStatus_TicketStatusFilterDTO.StatusId;
            TicketStatusFilter.CreatedAt = TicketStatus_TicketStatusFilterDTO.CreatedAt;
            TicketStatusFilter.UpdatedAt = TicketStatus_TicketStatusFilterDTO.UpdatedAt;
            return TicketStatusFilter;
        }
    }
}

