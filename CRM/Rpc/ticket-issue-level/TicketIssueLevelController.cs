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
using CRM.Services.MTicketIssueLevel;
using CRM.Services.MStatus;
using CRM.Services.MTicketGroup;
using CRM.Services.MTicketType;
using CRM.Services.MTicketPriority;
using CRM.Services.MSLATimeUnit;
using CRM.Services.MSmsTemplate;
using CRM.Services.MMailTemplate;
using CRM.Services.MAppUser;
using CRM.Enums;
using CRM.Services.MOrganization;
using CRM.Models;

namespace CRM.Rpc.ticket_issue_level
{
    public partial class TicketIssueLevelController : RpcController
    {
        private IOrganizationService OrganizationService;
        private IStatusService StatusService;
        private ITicketGroupService TicketGroupService;
        private ITicketIssueLevelService TicketIssueLevelService;
        private ITicketTypeService TicketTypeService;
        private ITicketPriorityService TicketPriorityService;
        private ISLATimeUnitService SLATimeUnitService;
        private ISmsTemplateService SmsTemplateService;
        private IMailTemplateService MailTemplateService;
        private IAppUserService AppUserService;
        private ICurrentContext CurrentContext;
        public TicketIssueLevelController(
            IOrganizationService OrganizationService,
            IStatusService StatusService,
            ITicketGroupService TicketGroupService,
            ITicketIssueLevelService TicketIssueLevelService,
            ITicketTypeService TicketTypeService,
            ITicketPriorityService TicketPriorityService,
            ISLATimeUnitService SLATimeUnitService,
            ISmsTemplateService SmsTemplateService,
            IMailTemplateService MailTemplateService,
            IAppUserService AppUserService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.OrganizationService = OrganizationService;
            this.StatusService = StatusService;
            this.TicketGroupService = TicketGroupService;
            this.TicketIssueLevelService = TicketIssueLevelService;
            this.TicketTypeService = TicketTypeService;
            this.TicketPriorityService = TicketPriorityService;
            this.SLATimeUnitService = SLATimeUnitService;
            this.SmsTemplateService = SmsTemplateService;
            this.MailTemplateService = MailTemplateService;
            this.AppUserService = AppUserService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketIssueLevelRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] TicketIssueLevel_TicketIssueLevelFilterDTO TicketIssueLevel_TicketIssueLevelFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketIssueLevelFilter TicketIssueLevelFilter = ConvertFilterDTOToFilterEntity(TicketIssueLevel_TicketIssueLevelFilterDTO);
            TicketIssueLevelFilter = TicketIssueLevelService.ToFilter(TicketIssueLevelFilter);
            int count = await TicketIssueLevelService.Count(TicketIssueLevelFilter);
            return count;
        }

        [Route(TicketIssueLevelRoute.List), HttpPost]
        public async Task<ActionResult<List<TicketIssueLevel_TicketIssueLevelDTO>>> List([FromBody] TicketIssueLevel_TicketIssueLevelFilterDTO TicketIssueLevel_TicketIssueLevelFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketIssueLevelFilter TicketIssueLevelFilter = ConvertFilterDTOToFilterEntity(TicketIssueLevel_TicketIssueLevelFilterDTO);
            TicketIssueLevelFilter = TicketIssueLevelService.ToFilter(TicketIssueLevelFilter);
            List<TicketIssueLevel> TicketIssueLevels = await TicketIssueLevelService.List(TicketIssueLevelFilter);
            List<TicketIssueLevel_TicketIssueLevelDTO> TicketIssueLevel_TicketIssueLevelDTOs = TicketIssueLevels
                .Select(c => new TicketIssueLevel_TicketIssueLevelDTO(c)).ToList();
            return TicketIssueLevel_TicketIssueLevelDTOs;
        }

        [Route(TicketIssueLevelRoute.Get), HttpPost]
        public async Task<ActionResult<TicketIssueLevel_TicketIssueLevelDTO>> Get([FromBody]TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel_TicketIssueLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketIssueLevel_TicketIssueLevelDTO.Id))
                return Forbid();

            TicketIssueLevel TicketIssueLevel = await TicketIssueLevelService.Get(TicketIssueLevel_TicketIssueLevelDTO.Id);
            return new TicketIssueLevel_TicketIssueLevelDTO(TicketIssueLevel);
        }

        [Route(TicketIssueLevelRoute.GetDraft), HttpPost]
        public async Task<ActionResult<TicketIssueLevel_TicketIssueLevelDTO>> GetDraft([FromBody]TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel_TicketIssueLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel_TicketIssueLevelDTODraft = new TicketIssueLevel_TicketIssueLevelDTO();
            List<TicketPriority> TicketPriorities = await TicketPriorityService.List(new TicketPriorityFilter 
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = TicketPrioritySelect.ALL,
            });
            TicketIssueLevel_TicketIssueLevelDTODraft.SLAPolicies = TicketPriorities.Select(x => new TicketIssueLevel_SLAPolicyDTO
            {
                TicketPriorityId = x.Id,
                TicketPriority = new TicketIssueLevel_TicketPriorityDTO(x),
                IsAlert = false,
                IsAlertFRT = false,
                IsEscalation = false,
                IsEscalationFRT = false,
                FirstResponseTime = 0,
                ResolveTime = 0,
                ResolveUnit = new TicketIssueLevel_SLATimeUnitDTO
                {
                    Id = SLATimeUnitEnum.HOURS.Id,
                    Code = SLATimeUnitEnum.HOURS.Code,
                    Name = SLATimeUnitEnum.HOURS.Name
                },
                FirstResponseUnit = new TicketIssueLevel_SLATimeUnitDTO
                {
                    Id = SLATimeUnitEnum.MINUTES.Id,
                    Code = SLATimeUnitEnum.MINUTES.Code,
                    Name = SLATimeUnitEnum.MINUTES.Name
                },
                FirstResponseUnitId = SLATimeUnitEnum.MINUTES.Id,
                ResolveUnitId = SLATimeUnitEnum.HOURS.Id
            }).ToList();
            TicketIssueLevel_TicketIssueLevelDTODraft.Status = new TicketIssueLevel_StatusDTO
            {
                Code = Enums.StatusEnum.ACTIVE.Code,
                Id = Enums.StatusEnum.ACTIVE.Id,
                Name = Enums.StatusEnum.ACTIVE.Name
            };
            TicketIssueLevel_TicketIssueLevelDTODraft.StatusId = Enums.StatusEnum.ACTIVE.Id;

            TicketIssueLevel_TicketIssueLevelDTODraft.SLAAlerts = new List<TicketIssueLevel_SLAAlertDTO>();

            TicketIssueLevel_TicketIssueLevelDTODraft.SLAAlerts.Add(new TicketIssueLevel_SLAAlertDTO
            {
                IsNotification = true,
                Time = 15,
                IsAssignedToUser = true,
                TimeUnitId = SLATimeUnitEnum.MINUTES.Id,
                TimeUnit = new TicketIssueLevel_SLATimeUnitDTO
                {
                    Id = SLATimeUnitEnum.MINUTES.Id,
                    Code = SLATimeUnitEnum.MINUTES.Code,
                    Name = SLATimeUnitEnum.MINUTES.Name
                },
            });

            TicketIssueLevel_TicketIssueLevelDTODraft.SLAAlertFRTs = new List<TicketIssueLevel_SLAAlertFRTDTO>();
            TicketIssueLevel_TicketIssueLevelDTODraft.SLAAlertFRTs.Add(new TicketIssueLevel_SLAAlertFRTDTO
            {
                IsNotification = true,
                Time = 15,
                IsAssignedToUser = true,
                TimeUnitId = SLATimeUnitEnum.MINUTES.Id,
                TimeUnit = new TicketIssueLevel_SLATimeUnitDTO
                {
                    Id = SLATimeUnitEnum.MINUTES.Id,
                    Code = SLATimeUnitEnum.MINUTES.Code,
                    Name = SLATimeUnitEnum.MINUTES.Name
                },
            });

            TicketIssueLevel_TicketIssueLevelDTODraft.SLAEscalations = new List<TicketIssueLevel_SLAEscalationDTO>();
            TicketIssueLevel_TicketIssueLevelDTODraft.SLAEscalations.Add(new TicketIssueLevel_SLAEscalationDTO
            {
                IsNotification = true,
                Time = 15,
                IsAssignedToUser = true,
                TimeUnitId = SLATimeUnitEnum.MINUTES.Id,
                TimeUnit = new TicketIssueLevel_SLATimeUnitDTO
                {
                    Id = SLATimeUnitEnum.MINUTES.Id,
                    Code = SLATimeUnitEnum.MINUTES.Code,
                    Name = SLATimeUnitEnum.MINUTES.Name
                },
            });

            TicketIssueLevel_TicketIssueLevelDTODraft.SLAEscalationFRTs = new List<TicketIssueLevel_SLAEscalationFRTDTO>();
            TicketIssueLevel_TicketIssueLevelDTODraft.SLAEscalationFRTs.Add(new TicketIssueLevel_SLAEscalationFRTDTO
            {
                IsNotification = true,
                Time = 15,
                IsAssignedToUser = true,
                TimeUnitId = SLATimeUnitEnum.MINUTES.Id,
                TimeUnit = new TicketIssueLevel_SLATimeUnitDTO
                {
                    Id = SLATimeUnitEnum.MINUTES.Id,
                    Code = SLATimeUnitEnum.MINUTES.Code,
                    Name = SLATimeUnitEnum.MINUTES.Name
                },
            });

            return TicketIssueLevel_TicketIssueLevelDTODraft;
        }

        [Route(TicketIssueLevelRoute.Create), HttpPost]
        public async Task<ActionResult<TicketIssueLevel_TicketIssueLevelDTO>> Create([FromBody] TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel_TicketIssueLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketIssueLevel_TicketIssueLevelDTO.Id))
                return Forbid();

            TicketIssueLevel TicketIssueLevel = ConvertDTOToEntity(TicketIssueLevel_TicketIssueLevelDTO);
            TicketIssueLevel = await TicketIssueLevelService.Create(TicketIssueLevel);
            TicketIssueLevel_TicketIssueLevelDTO = new TicketIssueLevel_TicketIssueLevelDTO(TicketIssueLevel);
            if (TicketIssueLevel.IsValidated)
                return TicketIssueLevel_TicketIssueLevelDTO;
            else
                return BadRequest(TicketIssueLevel_TicketIssueLevelDTO);
        }

        [Route(TicketIssueLevelRoute.Update), HttpPost]
        public async Task<ActionResult<TicketIssueLevel_TicketIssueLevelDTO>> Update([FromBody] TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel_TicketIssueLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(TicketIssueLevel_TicketIssueLevelDTO.Id))
                return Forbid();

            TicketIssueLevel TicketIssueLevel = ConvertDTOToEntity(TicketIssueLevel_TicketIssueLevelDTO);
            TicketIssueLevel = await TicketIssueLevelService.Update(TicketIssueLevel);
            TicketIssueLevel_TicketIssueLevelDTO = new TicketIssueLevel_TicketIssueLevelDTO(TicketIssueLevel);
            if (TicketIssueLevel.IsValidated)
                return TicketIssueLevel_TicketIssueLevelDTO;
            else
                return BadRequest(TicketIssueLevel_TicketIssueLevelDTO);
        }

        [Route(TicketIssueLevelRoute.Delete), HttpPost]
        public async Task<ActionResult<TicketIssueLevel_TicketIssueLevelDTO>> Delete([FromBody] TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel_TicketIssueLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(TicketIssueLevel_TicketIssueLevelDTO.Id))
                return Forbid();

            TicketIssueLevel TicketIssueLevel = ConvertDTOToEntity(TicketIssueLevel_TicketIssueLevelDTO);
            TicketIssueLevel = await TicketIssueLevelService.Delete(TicketIssueLevel);
            TicketIssueLevel_TicketIssueLevelDTO = new TicketIssueLevel_TicketIssueLevelDTO(TicketIssueLevel);
            if (TicketIssueLevel.IsValidated)
                return TicketIssueLevel_TicketIssueLevelDTO;
            else
                return BadRequest(TicketIssueLevel_TicketIssueLevelDTO);
        }
        
        [Route(TicketIssueLevelRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketIssueLevelFilter TicketIssueLevelFilter = new TicketIssueLevelFilter();
            TicketIssueLevelFilter = TicketIssueLevelService.ToFilter(TicketIssueLevelFilter);
            TicketIssueLevelFilter.Id = new IdFilter { In = Ids };
            TicketIssueLevelFilter.Selects = TicketIssueLevelSelect.Id;
            TicketIssueLevelFilter.Skip = 0;
            TicketIssueLevelFilter.Take = int.MaxValue;

            List<TicketIssueLevel> TicketIssueLevels = await TicketIssueLevelService.List(TicketIssueLevelFilter);
            TicketIssueLevels = await TicketIssueLevelService.BulkDelete(TicketIssueLevels);
            if (TicketIssueLevels.Any(x => !x.IsValidated))
                return BadRequest(TicketIssueLevels.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(TicketIssueLevelRoute.Import), HttpPost]
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
            TicketGroupFilter TicketGroupFilter = new TicketGroupFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = TicketGroupSelect.ALL
            };
            List<TicketGroup> TicketGroups = await TicketGroupService.List(TicketGroupFilter);
            List<TicketIssueLevel> TicketIssueLevels = new List<TicketIssueLevel>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(TicketIssueLevels);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NameColumn = 1 + StartColumn;
                int OrderNumberColumn = 2 + StartColumn;
                int TicketGroupIdColumn = 3 + StartColumn;
                int StatusIdColumn = 4 + StartColumn;
                int SLAColumn = 5 + StartColumn;
                int UsedColumn = 9 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string OrderNumberValue = worksheet.Cells[i + StartRow, OrderNumberColumn].Value?.ToString();
                    string TicketGroupIdValue = worksheet.Cells[i + StartRow, TicketGroupIdColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string SLAValue = worksheet.Cells[i + StartRow, SLAColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    
                    TicketIssueLevel TicketIssueLevel = new TicketIssueLevel();
                    TicketIssueLevel.Name = NameValue;
                    TicketIssueLevel.OrderNumber = long.TryParse(OrderNumberValue, out long OrderNumber) ? OrderNumber : 0;
                    TicketIssueLevel.SLA = long.TryParse(SLAValue, out long SLA) ? SLA : 0;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    TicketIssueLevel.StatusId = Status == null ? 0 : Status.Id;
                    TicketIssueLevel.Status = Status;
                    TicketGroup TicketGroup = TicketGroups.Where(x => x.Id.ToString() == TicketGroupIdValue).FirstOrDefault();
                    TicketIssueLevel.TicketGroupId = TicketGroup == null ? 0 : TicketGroup.Id;
                    TicketIssueLevel.TicketGroup = TicketGroup;
                    
                    TicketIssueLevels.Add(TicketIssueLevel);
                }
            }
            TicketIssueLevels = await TicketIssueLevelService.Import(TicketIssueLevels);
            if (TicketIssueLevels.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < TicketIssueLevels.Count; i++)
                {
                    TicketIssueLevel TicketIssueLevel = TicketIssueLevels[i];
                    if (!TicketIssueLevel.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (TicketIssueLevel.Errors.ContainsKey(nameof(TicketIssueLevel.Id)))
                            Error += TicketIssueLevel.Errors[nameof(TicketIssueLevel.Id)];
                        if (TicketIssueLevel.Errors.ContainsKey(nameof(TicketIssueLevel.Name)))
                            Error += TicketIssueLevel.Errors[nameof(TicketIssueLevel.Name)];
                        if (TicketIssueLevel.Errors.ContainsKey(nameof(TicketIssueLevel.OrderNumber)))
                            Error += TicketIssueLevel.Errors[nameof(TicketIssueLevel.OrderNumber)];
                        if (TicketIssueLevel.Errors.ContainsKey(nameof(TicketIssueLevel.TicketGroupId)))
                            Error += TicketIssueLevel.Errors[nameof(TicketIssueLevel.TicketGroupId)];
                        if (TicketIssueLevel.Errors.ContainsKey(nameof(TicketIssueLevel.StatusId)))
                            Error += TicketIssueLevel.Errors[nameof(TicketIssueLevel.StatusId)];
                        if (TicketIssueLevel.Errors.ContainsKey(nameof(TicketIssueLevel.SLA)))
                            Error += TicketIssueLevel.Errors[nameof(TicketIssueLevel.SLA)];
                        if (TicketIssueLevel.Errors.ContainsKey(nameof(TicketIssueLevel.Used)))
                            Error += TicketIssueLevel.Errors[nameof(TicketIssueLevel.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(TicketIssueLevelRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] TicketIssueLevel_TicketIssueLevelFilterDTO TicketIssueLevel_TicketIssueLevelFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketIssueLevel
                var TicketIssueLevelFilter = ConvertFilterDTOToFilterEntity(TicketIssueLevel_TicketIssueLevelFilterDTO);
                TicketIssueLevelFilter.Skip = 0;
                TicketIssueLevelFilter.Take = int.MaxValue;
                TicketIssueLevelFilter = TicketIssueLevelService.ToFilter(TicketIssueLevelFilter);
                List<TicketIssueLevel> TicketIssueLevels = await TicketIssueLevelService.List(TicketIssueLevelFilter);

                var TicketIssueLevelHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "OrderNumber",
                        "TicketGroupId",
                        "StatusId",
                        "SLA",
                        "Used",
                    }
                };
                List<object[]> TicketIssueLevelData = new List<object[]>();
                for (int i = 0; i < TicketIssueLevels.Count; i++)
                {
                    var TicketIssueLevel = TicketIssueLevels[i];
                    TicketIssueLevelData.Add(new Object[]
                    {
                        TicketIssueLevel.Id,
                        TicketIssueLevel.Name,
                        TicketIssueLevel.OrderNumber,
                        TicketIssueLevel.TicketGroupId,
                        TicketIssueLevel.StatusId,
                        TicketIssueLevel.SLA,
                        TicketIssueLevel.Used,
                    });
                }
                excel.GenerateWorksheet("TicketIssueLevel", TicketIssueLevelHeaders, TicketIssueLevelData);
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
                #region TicketGroup
                var TicketGroupFilter = new TicketGroupFilter();
                TicketGroupFilter.Selects = TicketGroupSelect.ALL;
                TicketGroupFilter.OrderBy = TicketGroupOrder.Id;
                TicketGroupFilter.OrderType = OrderType.ASC;
                TicketGroupFilter.Skip = 0;
                TicketGroupFilter.Take = int.MaxValue;
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
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketIssueLevel.xlsx");
        }

        [Route(TicketIssueLevelRoute.ExportTemplate), HttpPost]
        public async Task<FileResult> ExportTemplate([FromBody] TicketIssueLevel_TicketIssueLevelFilterDTO TicketIssueLevel_TicketIssueLevelFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region TicketIssueLevel
                var TicketIssueLevelHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "OrderNumber",
                        "TicketGroupId",
                        "StatusId",
                        "SLA",
                        "Used",
                    }
                };
                List<object[]> TicketIssueLevelData = new List<object[]>();
                excel.GenerateWorksheet("TicketIssueLevel", TicketIssueLevelHeaders, TicketIssueLevelData);
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
                #region TicketGroup
                var TicketGroupFilter = new TicketGroupFilter();
                TicketGroupFilter.Selects = TicketGroupSelect.ALL;
                TicketGroupFilter.OrderBy = TicketGroupOrder.Id;
                TicketGroupFilter.OrderType = OrderType.ASC;
                TicketGroupFilter.Skip = 0;
                TicketGroupFilter.Take = int.MaxValue;
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
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "TicketIssueLevel.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            TicketIssueLevelFilter TicketIssueLevelFilter = new TicketIssueLevelFilter();
            TicketIssueLevelFilter = TicketIssueLevelService.ToFilter(TicketIssueLevelFilter);
            if (Id == 0)
            {

            }
            else
            {
                TicketIssueLevelFilter.Id = new IdFilter { Equal = Id };
                int count = await TicketIssueLevelService.Count(TicketIssueLevelFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private TicketIssueLevel ConvertDTOToEntity(TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel_TicketIssueLevelDTO)
        {
            TicketIssueLevel TicketIssueLevel = new TicketIssueLevel();
            TicketIssueLevel.Id = TicketIssueLevel_TicketIssueLevelDTO.Id;
            TicketIssueLevel.Name = TicketIssueLevel_TicketIssueLevelDTO.Name;
            TicketIssueLevel.OrderNumber = TicketIssueLevel_TicketIssueLevelDTO.OrderNumber;
            TicketIssueLevel.TicketGroupId = TicketIssueLevel_TicketIssueLevelDTO.TicketGroupId;
            TicketIssueLevel.TicketTypeId = TicketIssueLevel_TicketIssueLevelDTO.TicketTypeId;
            TicketIssueLevel.StatusId = TicketIssueLevel_TicketIssueLevelDTO.StatusId;
            TicketIssueLevel.SLA = TicketIssueLevel_TicketIssueLevelDTO.SLA;
            TicketIssueLevel.Used = TicketIssueLevel_TicketIssueLevelDTO.Used;
            TicketIssueLevel.Status = TicketIssueLevel_TicketIssueLevelDTO.Status == null ? null : new Status
            {
                Id = TicketIssueLevel_TicketIssueLevelDTO.Status.Id,
                Code = TicketIssueLevel_TicketIssueLevelDTO.Status.Code,
                Name = TicketIssueLevel_TicketIssueLevelDTO.Status.Name,
            };
            TicketIssueLevel.TicketGroup = TicketIssueLevel_TicketIssueLevelDTO.TicketGroup == null ? null : new TicketGroup
            {
                Id = TicketIssueLevel_TicketIssueLevelDTO.TicketGroup.Id,
                Name = TicketIssueLevel_TicketIssueLevelDTO.TicketGroup.Name,
                OrderNumber = TicketIssueLevel_TicketIssueLevelDTO.TicketGroup.OrderNumber,
                StatusId = TicketIssueLevel_TicketIssueLevelDTO.TicketGroup.StatusId,
                TicketTypeId = TicketIssueLevel_TicketIssueLevelDTO.TicketGroup.TicketTypeId,
                Used = TicketIssueLevel_TicketIssueLevelDTO.TicketGroup.Used,
            };
            TicketIssueLevel.SLAPolicies = TicketIssueLevel_TicketIssueLevelDTO.SLAPolicies?.Select(p => new SLAPolicy
            {
                Id = p.Id,
                TicketIssueLevelId = p.TicketIssueLevelId,
                TicketPriorityId = p.TicketPriorityId,
                FirstResponseTime = p.FirstResponseTime,
                FirstResponseUnitId = p.FirstResponseUnitId,
                FirstResponseUnit = p.FirstResponseUnit == null ? null : new SLATimeUnit
                {
                    Id = p.FirstResponseUnit.Id,
                    Code = p.FirstResponseUnit.Code,
                    Name = p.FirstResponseUnit.Name
                },
                ResolveTime = p.ResolveTime,
                ResolveUnitId = p.ResolveUnitId,
                ResolveUnit = p.ResolveUnit == null ? null : new SLATimeUnit
                {
                    Id = p.ResolveUnit.Id,
                    Code = p.ResolveUnit.Code,
                    Name = p.ResolveUnit.Name
                },
                IsAlert = p.IsAlert,
                IsAlertFRT = p.IsAlertFRT,
                IsEscalation = p.IsEscalation,
                IsEscalationFRT = p.IsEscalationFRT,
            }).ToList();
            TicketIssueLevel.SLAAlerts = TicketIssueLevel_TicketIssueLevelDTO.SLAAlerts?.Select(p => new SLAAlert 
            {
                Id = p.Id,
                TicketIssueLevelId = p.TicketIssueLevelId,
                IsNotification = p.IsNotification,
                IsMail = p.IsMail,
                IsSMS = p.IsSMS,
                Time = p.Time,
                TimeUnitId = p.TimeUnitId,
                IsAssignedToUser = p.IsAssignedToUser,
                IsAssignedToGroup = p.IsAssignedToGroup,
                SmsTemplateId = p.SmsTemplateId,
                MailTemplateId = p.MailTemplateId,
                SLAAlertMails = p.SLAAlertMails?.Select(x => new SLAAlertMail { 
                    Id = x.Id,
                    SLAAlertId = x.SLAAlertId,
                    Mail = x.Mail
                }).ToList(),
                SLAAlertPhones = p.SLAAlertPhones?.Select(x => new SLAAlertPhone
                {
                    Id = x.Id,
                    SLAAlertId = x.SLAAlertId,
                    Phone = x.Phone
                }).ToList(),
                SLAAlertUsers = p.SLAAlertUsers?.Select(x => new SLAAlertUser { 
                    Id = x.Id,
                    SLAAlertId = x.SLAAlertId,
                    AppUserId = x.AppUserId
                }).ToList()
            }).ToList();
            TicketIssueLevel.SLAAlertFRTs = TicketIssueLevel_TicketIssueLevelDTO.SLAAlertFRTs?.Select(p => new SLAAlertFRT
            {
                Id = p.Id,
                TicketIssueLevelId = p.TicketIssueLevelId,
                IsNotification = p.IsNotification,
                IsMail = p.IsMail,
                IsSMS = p.IsSMS,
                Time = p.Time,
                TimeUnitId = p.TimeUnitId,
                IsAssignedToUser = p.IsAssignedToUser,
                IsAssignedToGroup = p.IsAssignedToGroup,
                SmsTemplateId = p.SmsTemplateId,
                MailTemplateId = p.MailTemplateId,
                SLAAlertFRTMails = p.SLAAlertFRTMails?.Select(x => new SLAAlertFRTMail
                {
                    Id = x.Id,
                    SLAAlertFRTId = x.SLAAlertFRTId,
                    Mail = x.Mail
                }).ToList(),
                SLAAlertFRTPhones = p.SLAAlertFRTPhones?.Select(x => new SLAAlertFRTPhone
                {
                    Id = x.Id,
                    SLAAlertFRTId = x.SLAAlertFRTId,
                    Phone = x.Phone
                }).ToList(),
                SLAAlertFRTUsers = p.SLAAlertFRTUsers?.Select(x => new SLAAlertFRTUser
                {
                    Id = x.Id,
                    SLAAlertFRTId = x.SLAAlertFRTId,
                    AppUserId = x.AppUserId
                }).ToList()
            }).ToList();
            TicketIssueLevel.SLAEscalations = TicketIssueLevel_TicketIssueLevelDTO.SLAEscalations?.Select(p => new SLAEscalation
            {
                Id = p.Id,
                TicketIssueLevelId = p.TicketIssueLevelId,
                IsNotification = p.IsNotification,
                IsMail = p.IsMail,
                IsSMS = p.IsSMS,
                Time = p.Time,
                TimeUnitId = p.TimeUnitId,
                IsAssignedToUser = p.IsAssignedToUser,
                IsAssignedToGroup = p.IsAssignedToGroup,
                SmsTemplateId = p.SmsTemplateId,
                MailTemplateId = p.MailTemplateId,
                SLAEscalationMails = p.SLAEscalationMails?.Select(x => new SLAEscalationMail
                {
                    Id = x.Id,
                    SLAEscalationId = x.SLAEscalationId,
                    Mail = x.Mail
                }).ToList(),
                SLAEscalationPhones = p.SLAEscalationPhones?.Select(x => new SLAEscalationPhone
                {
                    Id = x.Id,
                    SLAEscalationId = x.SLAEscalationId,
                    Phone = x.Phone
                }).ToList(),
                SLAEscalationUsers = p.SLAEscalationUsers?.Select(x => new SLAEscalationUser
                {
                    Id = x.Id,
                    SLAEscalationId = x.SLAEscalationId,
                    AppUserId = x.AppUserId
                }).ToList()
            }).ToList();
            TicketIssueLevel.SLAEscalationFRTs = TicketIssueLevel_TicketIssueLevelDTO.SLAEscalationFRTs?.Select(p => new SLAEscalationFRT
            {
                Id = p.Id,
                TicketIssueLevelId = p.TicketIssueLevelId,
                IsNotification = p.IsNotification,
                IsMail = p.IsMail,
                IsSMS = p.IsSMS,
                Time = p.Time,
                TimeUnitId = p.TimeUnitId,
                IsAssignedToUser = p.IsAssignedToUser,
                IsAssignedToGroup = p.IsAssignedToGroup,
                SmsTemplateId = p.SmsTemplateId,
                MailTemplateId = p.MailTemplateId,
                SLAEscalationFRTMails = p.SLAEscalationFRTMails?.Select(x => new SLAEscalationFRTMail
                {
                    Id = x.Id,
                    SLAEscalationFRTId = x.SLAEscalationFRTId,
                    Mail = x.Mail
                }).ToList(),
                SLAEscalationFRTPhones = p.SLAEscalationFRTPhones?.Select(x => new SLAEscalationFRTPhone
                {
                    Id = x.Id,
                    SLAEscalationFRTId = x.SLAEscalationFRTId,
                    Phone = x.Phone
                }).ToList(),
                SLAEscalationFRTUsers = p.SLAEscalationFRTUsers?.Select(x => new SLAEscalationFRTUser
                {
                    Id = x.Id,
                    SLAEscalationFRTId = x.SLAEscalationFRTId,
                    AppUserId = x.AppUserId
                }).ToList()
            }).ToList();
            TicketIssueLevel.BaseLanguage = CurrentContext.Language;
            return TicketIssueLevel;
        }

        private TicketIssueLevelFilter ConvertFilterDTOToFilterEntity(TicketIssueLevel_TicketIssueLevelFilterDTO TicketIssueLevel_TicketIssueLevelFilterDTO)
        {
            TicketIssueLevelFilter TicketIssueLevelFilter = new TicketIssueLevelFilter();
            TicketIssueLevelFilter.Selects = TicketIssueLevelSelect.ALL;
            TicketIssueLevelFilter.Skip = TicketIssueLevel_TicketIssueLevelFilterDTO.Skip;
            TicketIssueLevelFilter.Take = TicketIssueLevel_TicketIssueLevelFilterDTO.Take;
            TicketIssueLevelFilter.OrderBy = TicketIssueLevel_TicketIssueLevelFilterDTO.OrderBy;
            TicketIssueLevelFilter.OrderType = TicketIssueLevel_TicketIssueLevelFilterDTO.OrderType;

            TicketIssueLevelFilter.Id = TicketIssueLevel_TicketIssueLevelFilterDTO.Id;
            TicketIssueLevelFilter.Name = TicketIssueLevel_TicketIssueLevelFilterDTO.Name;
            TicketIssueLevelFilter.OrderNumber = TicketIssueLevel_TicketIssueLevelFilterDTO.OrderNumber;
            TicketIssueLevelFilter.TicketGroupId = TicketIssueLevel_TicketIssueLevelFilterDTO.TicketGroupId;
            TicketIssueLevelFilter.TicketTypeId = TicketIssueLevel_TicketIssueLevelFilterDTO.TicketTypeId;
            TicketIssueLevelFilter.StatusId = TicketIssueLevel_TicketIssueLevelFilterDTO.StatusId;
            TicketIssueLevelFilter.SLA = TicketIssueLevel_TicketIssueLevelFilterDTO.SLA;
            TicketIssueLevelFilter.CreatedAt = TicketIssueLevel_TicketIssueLevelFilterDTO.CreatedAt;
            TicketIssueLevelFilter.UpdatedAt = TicketIssueLevel_TicketIssueLevelFilterDTO.UpdatedAt;
            return TicketIssueLevelFilter;
        }
    }
}

