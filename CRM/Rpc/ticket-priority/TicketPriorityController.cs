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
using CRM.Services.MTicketPriority;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.ticket_priority
{
    public partial class TicketPriorityController : RpcController
    {
        private IStatusService StatusService;
        private ITicketPriorityService TicketPriorityService;
        private ICurrentContext CurrentContext;
        public TicketPriorityController(
            IStatusService StatusService,
            ITicketPriorityService TicketPriorityService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.TicketPriorityService = TicketPriorityService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketPriorityRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] TicketPriority_TicketPriorityFilterDTO TicketPriority_TicketPriorityFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketPriorityFilter TicketPriorityFilter = ConvertFilterDTOToFilterEntity(TicketPriority_TicketPriorityFilterDTO);
            TicketPriorityFilter = TicketPriorityService.ToFilter(TicketPriorityFilter);
            int count = await TicketPriorityService.Count(TicketPriorityFilter);
            return count;
        }

        [Route(TicketPriorityRoute.List), HttpPost]
        public async Task<ActionResult<List<TicketPriority_TicketPriorityDTO>>> List([FromBody] TicketPriority_TicketPriorityFilterDTO TicketPriority_TicketPriorityFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketPriorityFilter TicketPriorityFilter = ConvertFilterDTOToFilterEntity(TicketPriority_TicketPriorityFilterDTO);
            TicketPriorityFilter = TicketPriorityService.ToFilter(TicketPriorityFilter);
            List<TicketPriority> TicketPriorities = await TicketPriorityService.List(TicketPriorityFilter);
            List<TicketPriority_TicketPriorityDTO> TicketPriority_TicketPriorityDTOs = TicketPriorities
                .Select(c => new TicketPriority_TicketPriorityDTO(c)).ToList();
            return TicketPriority_TicketPriorityDTOs;
        }

        [Route(TicketPriorityRoute.Get), HttpPost]
        public async Task<ActionResult<TicketPriority_TicketPriorityDTO>> Get([FromBody]TicketPriority_TicketPriorityDTO TicketPriority_TicketPriorityDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketPriority_TicketPriorityDTO.Id))
                return Forbid();

            TicketPriority TicketPriority = await TicketPriorityService.Get(TicketPriority_TicketPriorityDTO.Id);
            return new TicketPriority_TicketPriorityDTO(TicketPriority);
        }

        [Route(TicketPriorityRoute.Create), HttpPost]
        public async Task<ActionResult<TicketPriority_TicketPriorityDTO>> Create([FromBody] TicketPriority_TicketPriorityDTO TicketPriority_TicketPriorityDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketPriority_TicketPriorityDTO.Id))
                return Forbid();

            TicketPriority TicketPriority = ConvertDTOToEntity(TicketPriority_TicketPriorityDTO);
            TicketPriority = await TicketPriorityService.Create(TicketPriority);
            TicketPriority_TicketPriorityDTO = new TicketPriority_TicketPriorityDTO(TicketPriority);
            if (TicketPriority.IsValidated)
                return TicketPriority_TicketPriorityDTO;
            else
                return BadRequest(TicketPriority_TicketPriorityDTO);
        }

        [Route(TicketPriorityRoute.Update), HttpPost]
        public async Task<ActionResult<TicketPriority_TicketPriorityDTO>> Update([FromBody] TicketPriority_TicketPriorityDTO TicketPriority_TicketPriorityDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketPriority_TicketPriorityDTO.Id))
                return Forbid();

            TicketPriority TicketPriority = ConvertDTOToEntity(TicketPriority_TicketPriorityDTO);
            TicketPriority = await TicketPriorityService.Update(TicketPriority);
            TicketPriority_TicketPriorityDTO = new TicketPriority_TicketPriorityDTO(TicketPriority);
            if (TicketPriority.IsValidated)
                return TicketPriority_TicketPriorityDTO;
            else
                return BadRequest(TicketPriority_TicketPriorityDTO);
        }

        [Route(TicketPriorityRoute.Delete), HttpPost]
        public async Task<ActionResult<TicketPriority_TicketPriorityDTO>> Delete([FromBody] TicketPriority_TicketPriorityDTO TicketPriority_TicketPriorityDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketPriority_TicketPriorityDTO.Id))
                return Forbid();

            TicketPriority TicketPriority = ConvertDTOToEntity(TicketPriority_TicketPriorityDTO);
            TicketPriority = await TicketPriorityService.Delete(TicketPriority);
            TicketPriority_TicketPriorityDTO = new TicketPriority_TicketPriorityDTO(TicketPriority);
            if (TicketPriority.IsValidated)
                return TicketPriority_TicketPriorityDTO;
            else
                return BadRequest(TicketPriority_TicketPriorityDTO);
        }
        
        [Route(TicketPriorityRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter();
            TicketPriorityFilter = TicketPriorityService.ToFilter(TicketPriorityFilter);
            TicketPriorityFilter.Id = new IdFilter { In = Ids };
            TicketPriorityFilter.Selects = TicketPrioritySelect.Id;
            TicketPriorityFilter.Skip = 0;
            TicketPriorityFilter.Take = int.MaxValue;

            List<TicketPriority> TicketPriorities = await TicketPriorityService.List(TicketPriorityFilter);
            TicketPriorities = await TicketPriorityService.BulkDelete(TicketPriorities);
            if (TicketPriorities.Any(x => !x.IsValidated))
                return BadRequest(TicketPriorities.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(TicketPriorityRoute.Import), HttpPost]
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
            List<TicketPriority> TicketPriorities = new List<TicketPriority>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(TicketPriorities);
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
                    
                    TicketPriority TicketPriority = new TicketPriority();
                    TicketPriority.Name = NameValue;
                    TicketPriority.OrderNumber = long.TryParse(OrderNumberValue, out long OrderNumber) ? OrderNumber : 0;
                    TicketPriority.ColorCode = ColorCodeValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    TicketPriority.StatusId = Status == null ? 0 : Status.Id;
                    TicketPriority.Status = Status;
                    
                    TicketPriorities.Add(TicketPriority);
                }
            }
            TicketPriorities = await TicketPriorityService.Import(TicketPriorities);
            if (TicketPriorities.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < TicketPriorities.Count; i++)
                {
                    TicketPriority TicketPriority = TicketPriorities[i];
                    if (!TicketPriority.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (TicketPriority.Errors.ContainsKey(nameof(TicketPriority.Id)))
                            Error += TicketPriority.Errors[nameof(TicketPriority.Id)];
                        if (TicketPriority.Errors.ContainsKey(nameof(TicketPriority.Name)))
                            Error += TicketPriority.Errors[nameof(TicketPriority.Name)];
                        if (TicketPriority.Errors.ContainsKey(nameof(TicketPriority.OrderNumber)))
                            Error += TicketPriority.Errors[nameof(TicketPriority.OrderNumber)];
                        if (TicketPriority.Errors.ContainsKey(nameof(TicketPriority.ColorCode)))
                            Error += TicketPriority.Errors[nameof(TicketPriority.ColorCode)];
                        if (TicketPriority.Errors.ContainsKey(nameof(TicketPriority.StatusId)))
                            Error += TicketPriority.Errors[nameof(TicketPriority.StatusId)];
                        if (TicketPriority.Errors.ContainsKey(nameof(TicketPriority.Used)))
                            Error += TicketPriority.Errors[nameof(TicketPriority.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(TicketPriorityRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] TicketPriority_TicketPriorityFilterDTO TicketPriority_TicketPriorityFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketPriority
                var TicketPriorityFilter = ConvertFilterDTOToFilterEntity(TicketPriority_TicketPriorityFilterDTO);
                TicketPriorityFilter.Skip = 0;
                TicketPriorityFilter.Take = int.MaxValue;
                TicketPriorityFilter = TicketPriorityService.ToFilter(TicketPriorityFilter);
                List<TicketPriority> TicketPriorities = await TicketPriorityService.List(TicketPriorityFilter);

                var TicketPriorityHeaders = new List<string[]>()
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
                List<object[]> TicketPriorityData = new List<object[]>();
                for (int i = 0; i < TicketPriorities.Count; i++)
                {
                    var TicketPriority = TicketPriorities[i];
                    TicketPriorityData.Add(new Object[]
                    {
                        TicketPriority.Id,
                        TicketPriority.Name,
                        TicketPriority.OrderNumber,
                        TicketPriority.ColorCode,
                        TicketPriority.StatusId,
                        TicketPriority.Used,
                    });
                }
                excel.GenerateWorksheet("TicketPriority", TicketPriorityHeaders, TicketPriorityData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketPriority.xlsx");
        }

        [Route(TicketPriorityRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] TicketPriority_TicketPriorityFilterDTO TicketPriority_TicketPriorityFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketPriority
                var TicketPriorityHeaders = new List<string[]>()
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
                List<object[]> TicketPriorityData = new List<object[]>();
                excel.GenerateWorksheet("TicketPriority", TicketPriorityHeaders, TicketPriorityData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketPriority.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter();
            TicketPriorityFilter = TicketPriorityService.ToFilter(TicketPriorityFilter);
            if (Id == 0)
            {

            }
            else
            {
                TicketPriorityFilter.Id = new IdFilter { Equal = Id };
                int count = await TicketPriorityService.Count(TicketPriorityFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private TicketPriority ConvertDTOToEntity(TicketPriority_TicketPriorityDTO TicketPriority_TicketPriorityDTO)
        {
            TicketPriority TicketPriority = new TicketPriority();
            TicketPriority.Id = TicketPriority_TicketPriorityDTO.Id;
            TicketPriority.Name = TicketPriority_TicketPriorityDTO.Name;
            TicketPriority.OrderNumber = TicketPriority_TicketPriorityDTO.OrderNumber;
            TicketPriority.ColorCode = TicketPriority_TicketPriorityDTO.ColorCode;
            TicketPriority.StatusId = TicketPriority_TicketPriorityDTO.StatusId;
            TicketPriority.Used = TicketPriority_TicketPriorityDTO.Used;
            TicketPriority.Status = TicketPriority_TicketPriorityDTO.Status == null ? null : new Status
            {
                Id = TicketPriority_TicketPriorityDTO.Status.Id,
                Code = TicketPriority_TicketPriorityDTO.Status.Code,
                Name = TicketPriority_TicketPriorityDTO.Status.Name,
            };
            TicketPriority.BaseLanguage = CurrentContext.Language;
            return TicketPriority;
        }

        private TicketPriorityFilter ConvertFilterDTOToFilterEntity(TicketPriority_TicketPriorityFilterDTO TicketPriority_TicketPriorityFilterDTO)
        {
            TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter();
            TicketPriorityFilter.Selects = TicketPrioritySelect.ALL;
            TicketPriorityFilter.Skip = TicketPriority_TicketPriorityFilterDTO.Skip;
            TicketPriorityFilter.Take = TicketPriority_TicketPriorityFilterDTO.Take;
            TicketPriorityFilter.OrderBy = TicketPriority_TicketPriorityFilterDTO.OrderBy;
            TicketPriorityFilter.OrderType = TicketPriority_TicketPriorityFilterDTO.OrderType;

            TicketPriorityFilter.Id = TicketPriority_TicketPriorityFilterDTO.Id;
            TicketPriorityFilter.Name = TicketPriority_TicketPriorityFilterDTO.Name;
            TicketPriorityFilter.OrderNumber = TicketPriority_TicketPriorityFilterDTO.OrderNumber;
            TicketPriorityFilter.ColorCode = TicketPriority_TicketPriorityFilterDTO.ColorCode;
            TicketPriorityFilter.StatusId = TicketPriority_TicketPriorityFilterDTO.StatusId;
            TicketPriorityFilter.CreatedAt = TicketPriority_TicketPriorityFilterDTO.CreatedAt;
            TicketPriorityFilter.UpdatedAt = TicketPriority_TicketPriorityFilterDTO.UpdatedAt;
            return TicketPriorityFilter;
        }
    }
}

