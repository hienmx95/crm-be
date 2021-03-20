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
using CRM.Services.MTicketGroup;
using CRM.Services.MStatus;
using CRM.Services.MTicketType;
using CRM.Models;

namespace CRM.Rpc.ticket_group
{
    public partial class TicketGroupController : RpcController
    {
        private IStatusService StatusService;
        private ITicketTypeService TicketTypeService;
        private ITicketGroupService TicketGroupService;
        private ICurrentContext CurrentContext;
        public TicketGroupController(
            IStatusService StatusService,
            ITicketTypeService TicketTypeService,
            ITicketGroupService TicketGroupService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.TicketTypeService = TicketTypeService;
            this.TicketGroupService = TicketGroupService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketGroupRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] TicketGroup_TicketGroupFilterDTO TicketGroup_TicketGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketGroupFilter TicketGroupFilter = ConvertFilterDTOToFilterEntity(TicketGroup_TicketGroupFilterDTO);
            TicketGroupFilter = TicketGroupService.ToFilter(TicketGroupFilter);
            int count = await TicketGroupService.Count(TicketGroupFilter);
            return count;
        }

        [Route(TicketGroupRoute.List), HttpPost]
        public async Task<ActionResult<List<TicketGroup_TicketGroupDTO>>> List([FromBody] TicketGroup_TicketGroupFilterDTO TicketGroup_TicketGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketGroupFilter TicketGroupFilter = ConvertFilterDTOToFilterEntity(TicketGroup_TicketGroupFilterDTO);
            TicketGroupFilter = TicketGroupService.ToFilter(TicketGroupFilter);
            List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);
            List<TicketGroup_TicketGroupDTO> TicketGroup_TicketGroupDTOs = TicketGroups
                .Select(c => new TicketGroup_TicketGroupDTO(c)).ToList();
            return TicketGroup_TicketGroupDTOs;
        }

        [Route(TicketGroupRoute.Get), HttpPost]
        public async Task<ActionResult<TicketGroup_TicketGroupDTO>> Get([FromBody]TicketGroup_TicketGroupDTO TicketGroup_TicketGroupDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketGroup_TicketGroupDTO.Id))
                return Forbid();

            TicketGroup TicketGroup = await TicketGroupService.Get(TicketGroup_TicketGroupDTO.Id);
            return new TicketGroup_TicketGroupDTO(TicketGroup);
        }

        [Route(TicketGroupRoute.Create), HttpPost]
        public async Task<ActionResult<TicketGroup_TicketGroupDTO>> Create([FromBody] TicketGroup_TicketGroupDTO TicketGroup_TicketGroupDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketGroup_TicketGroupDTO.Id))
                return Forbid();

            TicketGroup TicketGroup = ConvertDTOToEntity(TicketGroup_TicketGroupDTO);
            TicketGroup = await TicketGroupService.Create(TicketGroup);
            TicketGroup_TicketGroupDTO = new TicketGroup_TicketGroupDTO(TicketGroup);
            if (TicketGroup.IsValidated)
                return TicketGroup_TicketGroupDTO;
            else
                return BadRequest(TicketGroup_TicketGroupDTO);
        }

        [Route(TicketGroupRoute.Update), HttpPost]
        public async Task<ActionResult<TicketGroup_TicketGroupDTO>> Update([FromBody] TicketGroup_TicketGroupDTO TicketGroup_TicketGroupDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketGroup_TicketGroupDTO.Id))
                return Forbid();

            TicketGroup TicketGroup = ConvertDTOToEntity(TicketGroup_TicketGroupDTO);
            TicketGroup = await TicketGroupService.Update(TicketGroup);
            TicketGroup_TicketGroupDTO = new TicketGroup_TicketGroupDTO(TicketGroup);
            if (TicketGroup.IsValidated)
                return TicketGroup_TicketGroupDTO;
            else
                return BadRequest(TicketGroup_TicketGroupDTO);
        }

        [Route(TicketGroupRoute.Delete), HttpPost]
        public async Task<ActionResult<TicketGroup_TicketGroupDTO>> Delete([FromBody] TicketGroup_TicketGroupDTO TicketGroup_TicketGroupDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketGroup_TicketGroupDTO.Id))
                return Forbid();

            TicketGroup TicketGroup = ConvertDTOToEntity(TicketGroup_TicketGroupDTO);
            TicketGroup = await TicketGroupService.Delete(TicketGroup);
            TicketGroup_TicketGroupDTO = new TicketGroup_TicketGroupDTO(TicketGroup);
            if (TicketGroup.IsValidated)
                return TicketGroup_TicketGroupDTO;
            else
                return BadRequest(TicketGroup_TicketGroupDTO);
        }
        
        [Route(TicketGroupRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter();
            TicketGroupFilter = TicketGroupService.ToFilter(TicketGroupFilter);
            TicketGroupFilter.Id = new IdFilter { In = Ids };
            TicketGroupFilter.Selects = TicketGroupSelect.Id;
            TicketGroupFilter.Skip = 0;
            TicketGroupFilter.Take = int.MaxValue;

            List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);
            TicketGroups = await TicketGroupService.BulkDelete(TicketGroups);
            if (TicketGroups.Any(x => !x.IsValidated))
                return BadRequest(TicketGroups.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(TicketGroupRoute.Import), HttpPost]
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
            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = TicketTypeSelect.ALL
            };
            List<TicketType> TicketTypes = await TicketTypeService.List(TicketTypeFilter);
            List<TicketGroup> TicketGroups = new List<TicketGroup>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(TicketGroups);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NameColumn = 1 + StartColumn;
                int OrderNumberColumn = 2 + StartColumn;
                int StatusIdColumn = 3 + StartColumn;
                int TicketTypeIdColumn = 4 + StartColumn;
                int UsedColumn = 8 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string OrderNumberValue = worksheet.Cells[i + StartRow, OrderNumberColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string TicketTypeIdValue = worksheet.Cells[i + StartRow, TicketTypeIdColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    
                    TicketGroup TicketGroup = new TicketGroup();
                    TicketGroup.Name = NameValue;
                    TicketGroup.OrderNumber = long.TryParse(OrderNumberValue, out long OrderNumber) ? OrderNumber : 0;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    TicketGroup.StatusId = Status == null ? 0 : Status.Id;
                    TicketGroup.Status = Status;
                    TicketType TicketType = TicketTypes.Where(x => x.Id.ToString() == TicketTypeIdValue).FirstOrDefault();
                    TicketGroup.TicketTypeId = TicketType == null ? 0 : TicketType.Id;
                    TicketGroup.TicketType = TicketType;
                    
                    TicketGroups.Add(TicketGroup);
                }
            }
            TicketGroups = await TicketGroupService.Import(TicketGroups);
            if (TicketGroups.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < TicketGroups.Count; i++)
                {
                    TicketGroup TicketGroup = TicketGroups[i];
                    if (!TicketGroup.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (TicketGroup.Errors.ContainsKey(nameof(TicketGroup.Id)))
                            Error += TicketGroup.Errors[nameof(TicketGroup.Id)];
                        if (TicketGroup.Errors.ContainsKey(nameof(TicketGroup.Name)))
                            Error += TicketGroup.Errors[nameof(TicketGroup.Name)];
                        if (TicketGroup.Errors.ContainsKey(nameof(TicketGroup.OrderNumber)))
                            Error += TicketGroup.Errors[nameof(TicketGroup.OrderNumber)];
                        if (TicketGroup.Errors.ContainsKey(nameof(TicketGroup.StatusId)))
                            Error += TicketGroup.Errors[nameof(TicketGroup.StatusId)];
                        if (TicketGroup.Errors.ContainsKey(nameof(TicketGroup.TicketTypeId)))
                            Error += TicketGroup.Errors[nameof(TicketGroup.TicketTypeId)];
                        if (TicketGroup.Errors.ContainsKey(nameof(TicketGroup.Used)))
                            Error += TicketGroup.Errors[nameof(TicketGroup.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(TicketGroupRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] TicketGroup_TicketGroupFilterDTO TicketGroup_TicketGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketGroup
                var TicketGroupFilter = ConvertFilterDTOToFilterEntity(TicketGroup_TicketGroupFilterDTO);
                TicketGroupFilter.Skip = 0;
                TicketGroupFilter.Take = int.MaxValue;
                TicketGroupFilter = TicketGroupService.ToFilter(TicketGroupFilter);
                List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);

                var TicketGroupHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "OrderNumber",
                        "StatusId",
                        "TicketTypeId",
                        "Used",
                    }
                };
                List<object[]> TicketGroupData = new List<object[]>();
                for (int i = 0; i < TicketGroups.Count; i++)
                {
                    var TicketGroup = TicketGroups[i];
                    TicketGroupData.Add(new Object[]
                    {
                        TicketGroup.Id,
                        TicketGroup.Name,
                        TicketGroup.OrderNumber,
                        TicketGroup.StatusId,
                        TicketGroup.TicketTypeId,
                        TicketGroup.Used,
                    });
                }
                excel.GenerateWorksheet("TicketGroup", TicketGroupHeaders, TicketGroupData);
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
                #region TicketType
                var TicketTypeFilter = new TicketTypeFilter();
                TicketTypeFilter.Selects = TicketTypeSelect.ALL;
                TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
                TicketTypeFilter.OrderType = OrderType.ASC;
                TicketTypeFilter.Skip = 0;
                TicketTypeFilter.Take = int.MaxValue;
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
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketGroup.xlsx");
        }

        [Route(TicketGroupRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] TicketGroup_TicketGroupFilterDTO TicketGroup_TicketGroupFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketGroup
                var TicketGroupHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "OrderNumber",
                        "StatusId",
                        "TicketTypeId",
                        "Used",
                    }
                };
                List<object[]> TicketGroupData = new List<object[]>();
                excel.GenerateWorksheet("TicketGroup", TicketGroupHeaders, TicketGroupData);
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
                #region TicketType
                var TicketTypeFilter = new TicketTypeFilter();
                TicketTypeFilter.Selects = TicketTypeSelect.ALL;
                TicketTypeFilter.OrderBy = TicketTypeOrder.Id;
                TicketTypeFilter.OrderType = OrderType.ASC;
                TicketTypeFilter.Skip = 0;
                TicketTypeFilter.Take = int.MaxValue;
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
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketGroup.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter();
            TicketGroupFilter = TicketGroupService.ToFilter(TicketGroupFilter);
            if (Id == 0)
            {

            }
            else
            {
                TicketGroupFilter.Id = new IdFilter { Equal = Id };
                int count = await TicketGroupService.Count(TicketGroupFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private TicketGroup ConvertDTOToEntity(TicketGroup_TicketGroupDTO TicketGroup_TicketGroupDTO)
        {
            TicketGroup TicketGroup = new TicketGroup();
            TicketGroup.Id = TicketGroup_TicketGroupDTO.Id;
            TicketGroup.Name = TicketGroup_TicketGroupDTO.Name;
            TicketGroup.OrderNumber = TicketGroup_TicketGroupDTO.OrderNumber;
            TicketGroup.StatusId = TicketGroup_TicketGroupDTO.StatusId;
            TicketGroup.TicketTypeId = TicketGroup_TicketGroupDTO.TicketTypeId;
            TicketGroup.Used = TicketGroup_TicketGroupDTO.Used;
            TicketGroup.Status = TicketGroup_TicketGroupDTO.Status == null ? null : new Status
            {
                Id = TicketGroup_TicketGroupDTO.Status.Id,
                Code = TicketGroup_TicketGroupDTO.Status.Code,
                Name = TicketGroup_TicketGroupDTO.Status.Name,
            };
            TicketGroup.TicketType = TicketGroup_TicketGroupDTO.TicketType == null ? null : new TicketType
            {
                Id = TicketGroup_TicketGroupDTO.TicketType.Id,
                Code = TicketGroup_TicketGroupDTO.TicketType.Code,
                Name = TicketGroup_TicketGroupDTO.TicketType.Name,
                ColorCode = TicketGroup_TicketGroupDTO.TicketType.ColorCode,
                StatusId = TicketGroup_TicketGroupDTO.TicketType.StatusId,
                Used = TicketGroup_TicketGroupDTO.TicketType.Used,
            };
            TicketGroup.BaseLanguage = CurrentContext.Language;
            return TicketGroup;
        }

        private TicketGroupFilter ConvertFilterDTOToFilterEntity(TicketGroup_TicketGroupFilterDTO TicketGroup_TicketGroupFilterDTO)
        {
            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter();
            TicketGroupFilter.Selects = TicketGroupSelect.ALL;
            TicketGroupFilter.Skip = TicketGroup_TicketGroupFilterDTO.Skip;
            TicketGroupFilter.Take = TicketGroup_TicketGroupFilterDTO.Take;
            TicketGroupFilter.OrderBy = TicketGroup_TicketGroupFilterDTO.OrderBy;
            TicketGroupFilter.OrderType = TicketGroup_TicketGroupFilterDTO.OrderType;

            TicketGroupFilter.Id = TicketGroup_TicketGroupFilterDTO.Id;
            TicketGroupFilter.Name = TicketGroup_TicketGroupFilterDTO.Name;
            TicketGroupFilter.OrderNumber = TicketGroup_TicketGroupFilterDTO.OrderNumber;
            TicketGroupFilter.StatusId = TicketGroup_TicketGroupFilterDTO.StatusId;
            TicketGroupFilter.TicketTypeId = TicketGroup_TicketGroupFilterDTO.TicketTypeId;
            TicketGroupFilter.CreatedAt = TicketGroup_TicketGroupFilterDTO.CreatedAt;
            TicketGroupFilter.UpdatedAt = TicketGroup_TicketGroupFilterDTO.UpdatedAt;
            return TicketGroupFilter;
        }
    }
}

