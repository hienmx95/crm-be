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
using CRM.Services.MTicketSource;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.ticket_source
{
    public partial class TicketSourceController : RpcController
    {
        private IStatusService StatusService;
        private ITicketSourceService TicketSourceService;
        private ICurrentContext CurrentContext;
        public TicketSourceController(
            IStatusService StatusService,
            ITicketSourceService TicketSourceService,
            ICurrentContext CurrentContext
       , IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ) : base(httpContextAccessor, _DataContext)
        {
            this.StatusService = StatusService;
            this.TicketSourceService = TicketSourceService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketSourceRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] TicketSource_TicketSourceFilterDTO TicketSource_TicketSourceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketSourceFilter TicketSourceFilter = ConvertFilterDTOToFilterEntity(TicketSource_TicketSourceFilterDTO);
            TicketSourceFilter = TicketSourceService.ToFilter(TicketSourceFilter);
            int count = await TicketSourceService.Count(TicketSourceFilter);
            return count;
        }

        [Route(TicketSourceRoute.List), HttpPost]
        public async Task<ActionResult<List<TicketSource_TicketSourceDTO>>> List([FromBody] TicketSource_TicketSourceFilterDTO TicketSource_TicketSourceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketSourceFilter TicketSourceFilter = ConvertFilterDTOToFilterEntity(TicketSource_TicketSourceFilterDTO);
            TicketSourceFilter = TicketSourceService.ToFilter(TicketSourceFilter);
            List<TicketSource> TicketSources = await TicketSourceService.List(TicketSourceFilter);
            List<TicketSource_TicketSourceDTO> TicketSource_TicketSourceDTOs = TicketSources
                .Select(c => new TicketSource_TicketSourceDTO(c)).ToList();
            return TicketSource_TicketSourceDTOs;
        }

        [Route(TicketSourceRoute.Get), HttpPost]
        public async Task<ActionResult<TicketSource_TicketSourceDTO>> Get([FromBody] TicketSource_TicketSourceDTO TicketSource_TicketSourceDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketSource_TicketSourceDTO.Id))
                return Forbid();

            TicketSource TicketSource = await TicketSourceService.Get(TicketSource_TicketSourceDTO.Id);
            return new TicketSource_TicketSourceDTO(TicketSource);
        }

        [Route(TicketSourceRoute.Create), HttpPost]
        public async Task<ActionResult<TicketSource_TicketSourceDTO>> Create([FromBody] TicketSource_TicketSourceDTO TicketSource_TicketSourceDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketSource_TicketSourceDTO.Id))
                return Forbid();

            TicketSource TicketSource = ConvertDTOToEntity(TicketSource_TicketSourceDTO);
            TicketSource = await TicketSourceService.Create(TicketSource);
            TicketSource_TicketSourceDTO = new TicketSource_TicketSourceDTO(TicketSource);
            if (TicketSource.IsValidated)
                return TicketSource_TicketSourceDTO;
            else
                return BadRequest(TicketSource_TicketSourceDTO);
        }

        [Route(TicketSourceRoute.Update), HttpPost]
        public async Task<ActionResult<TicketSource_TicketSourceDTO>> Update([FromBody] TicketSource_TicketSourceDTO TicketSource_TicketSourceDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketSource_TicketSourceDTO.Id))
                return Forbid();

            TicketSource TicketSource = ConvertDTOToEntity(TicketSource_TicketSourceDTO);
            TicketSource = await TicketSourceService.Update(TicketSource);
            TicketSource_TicketSourceDTO = new TicketSource_TicketSourceDTO(TicketSource);
            if (TicketSource.IsValidated)
                return TicketSource_TicketSourceDTO;
            else
                return BadRequest(TicketSource_TicketSourceDTO);
        }

        [Route(TicketSourceRoute.Delete), HttpPost]
        public async Task<ActionResult<TicketSource_TicketSourceDTO>> Delete([FromBody] TicketSource_TicketSourceDTO TicketSource_TicketSourceDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketSource_TicketSourceDTO.Id))
                return Forbid();

            TicketSource TicketSource = ConvertDTOToEntity(TicketSource_TicketSourceDTO);
            TicketSource = await TicketSourceService.Delete(TicketSource);
            TicketSource_TicketSourceDTO = new TicketSource_TicketSourceDTO(TicketSource);
            if (TicketSource.IsValidated)
                return TicketSource_TicketSourceDTO;
            else
                return BadRequest(TicketSource_TicketSourceDTO);
        }

        [Route(TicketSourceRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketSourceFilter TicketSourceFilter = new TicketSourceFilter();
            TicketSourceFilter = TicketSourceService.ToFilter(TicketSourceFilter);
            TicketSourceFilter.Id = new IdFilter { In = Ids };
            TicketSourceFilter.Selects = TicketSourceSelect.Id;
            TicketSourceFilter.Skip = 0;
            TicketSourceFilter.Take = int.MaxValue;

            List<TicketSource> TicketSources = await TicketSourceService.List(TicketSourceFilter);
            TicketSources = await TicketSourceService.BulkDelete(TicketSources);
            if (TicketSources.Any(x => !x.IsValidated))
                return BadRequest(TicketSources.Where(x => !x.IsValidated));
            return true;
        }

        [Route(TicketSourceRoute.Import), HttpPost]
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
            List<TicketSource> TicketSources = new List<TicketSource>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(TicketSources);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NameColumn = 1 + StartColumn;
                int OrderNumberColumn = 2 + StartColumn;
                int StatusIdColumn = 3 + StartColumn;
                int UsedColumn = 7 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string OrderNumberValue = worksheet.Cells[i + StartRow, OrderNumberColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();

                    TicketSource TicketSource = new TicketSource();
                    TicketSource.Name = NameValue;
                    TicketSource.OrderNumber = long.TryParse(OrderNumberValue, out long OrderNumber) ? OrderNumber : 0;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    TicketSource.StatusId = Status == null ? 0 : Status.Id;
                    TicketSource.Status = Status;

                    TicketSources.Add(TicketSource);
                }
            }
            TicketSources = await TicketSourceService.Import(TicketSources);
            if (TicketSources.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < TicketSources.Count; i++)
                {
                    TicketSource TicketSource = TicketSources[i];
                    if (!TicketSource.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (TicketSource.Errors.ContainsKey(nameof(TicketSource.Id)))
                            Error += TicketSource.Errors[nameof(TicketSource.Id)];
                        if (TicketSource.Errors.ContainsKey(nameof(TicketSource.Name)))
                            Error += TicketSource.Errors[nameof(TicketSource.Name)];
                        if (TicketSource.Errors.ContainsKey(nameof(TicketSource.OrderNumber)))
                            Error += TicketSource.Errors[nameof(TicketSource.OrderNumber)];
                        if (TicketSource.Errors.ContainsKey(nameof(TicketSource.StatusId)))
                            Error += TicketSource.Errors[nameof(TicketSource.StatusId)];
                        if (TicketSource.Errors.ContainsKey(nameof(TicketSource.Used)))
                            Error += TicketSource.Errors[nameof(TicketSource.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(TicketSourceRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] TicketSource_TicketSourceFilterDTO TicketSource_TicketSourceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketSource
                var TicketSourceFilter = ConvertFilterDTOToFilterEntity(TicketSource_TicketSourceFilterDTO);
                TicketSourceFilter.Skip = 0;
                TicketSourceFilter.Take = int.MaxValue;
                TicketSourceFilter = TicketSourceService.ToFilter(TicketSourceFilter);
                List<TicketSource> TicketSources = await TicketSourceService.List(TicketSourceFilter);

                var TicketSourceHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "OrderNumber",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketSourceData = new List<object[]>();
                for (int i = 0; i < TicketSources.Count; i++)
                {
                    var TicketSource = TicketSources[i];
                    TicketSourceData.Add(new Object[]
                    {
                        TicketSource.Id,
                        TicketSource.Name,
                        TicketSource.OrderNumber,
                        TicketSource.StatusId,
                        TicketSource.Used,
                    });
                }
                excel.GenerateWorksheet("TicketSource", TicketSourceHeaders, TicketSourceData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketSource.xlsx");
        }

        [Route(TicketSourceRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] TicketSource_TicketSourceFilterDTO TicketSource_TicketSourceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketSource
                var TicketSourceHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "OrderNumber",
                        "StatusId",
                        "Used",
                    }
                };
                List<object[]> TicketSourceData = new List<object[]>();
                excel.GenerateWorksheet("TicketSource", TicketSourceHeaders, TicketSourceData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketSource.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            TicketSourceFilter TicketSourceFilter = new TicketSourceFilter();
            TicketSourceFilter = TicketSourceService.ToFilter(TicketSourceFilter);
            if (Id == 0)
            {

            }
            else
            {
                TicketSourceFilter.Id = new IdFilter { Equal = Id };
                int count = await TicketSourceService.Count(TicketSourceFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private TicketSource ConvertDTOToEntity(TicketSource_TicketSourceDTO TicketSource_TicketSourceDTO)
        {
            TicketSource TicketSource = new TicketSource();
            TicketSource.Id = TicketSource_TicketSourceDTO.Id;
            TicketSource.Name = TicketSource_TicketSourceDTO.Name;
            TicketSource.OrderNumber = TicketSource_TicketSourceDTO.OrderNumber;
            TicketSource.StatusId = TicketSource_TicketSourceDTO.StatusId;
            TicketSource.Used = TicketSource_TicketSourceDTO.Used;
            TicketSource.Status = TicketSource_TicketSourceDTO.Status == null ? null : new Status
            {
                Id = TicketSource_TicketSourceDTO.Status.Id,
                Code = TicketSource_TicketSourceDTO.Status.Code,
                Name = TicketSource_TicketSourceDTO.Status.Name,
            };
            TicketSource.BaseLanguage = CurrentContext.Language;
            return TicketSource;
        }

        private TicketSourceFilter ConvertFilterDTOToFilterEntity(TicketSource_TicketSourceFilterDTO TicketSource_TicketSourceFilterDTO)
        {
            TicketSourceFilter TicketSourceFilter = new TicketSourceFilter();
            TicketSourceFilter.Selects = TicketSourceSelect.ALL;
            TicketSourceFilter.Skip = TicketSource_TicketSourceFilterDTO.Skip;
            TicketSourceFilter.Take = TicketSource_TicketSourceFilterDTO.Take;
            TicketSourceFilter.OrderBy = TicketSource_TicketSourceFilterDTO.OrderBy;
            TicketSourceFilter.OrderType = TicketSource_TicketSourceFilterDTO.OrderType;

            TicketSourceFilter.Id = TicketSource_TicketSourceFilterDTO.Id;
            TicketSourceFilter.Name = TicketSource_TicketSourceFilterDTO.Name;
            TicketSourceFilter.OrderNumber = TicketSource_TicketSourceFilterDTO.OrderNumber;
            TicketSourceFilter.StatusId = TicketSource_TicketSourceFilterDTO.StatusId;
            TicketSourceFilter.CreatedAt = TicketSource_TicketSourceFilterDTO.CreatedAt;
            TicketSourceFilter.UpdatedAt = TicketSource_TicketSourceFilterDTO.UpdatedAt;
            return TicketSourceFilter;
        }
    }
}

