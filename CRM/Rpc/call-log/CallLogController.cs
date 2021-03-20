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
using CRM.Services.MCallLog;
using CRM.Services.MCallCategory;
using CRM.Services.MCallType;
using CRM.Services.MCallStatus;
using CRM.Services.MCustomer;
using CRM.Services.MCallEmotion;
using CRM.Services.MEntityReference;
using CRM.Services.MAppUser;
using CRM.Services.MCustomerLead;
using CRM.Services.MContact;
using CRM.Services.MCompany;
using CRM.Services.MOpportunity;
using CRM.Services.MOrganization;
using CRM.Models;

namespace CRM.Rpc.call_log
{
    public partial class CallLogController : RpcController
    {
        private ICallCategoryService CallCategoryService;
        private ICallStatusService CallStatusService;
        private ICallTypeService CallTypeService;
        private ICustomerService CustomerService;
        private ICallEmotionService CallEmotionService;
        private ICallLogService CallLogService;
        private IEntityReferenceService EntityReferenceService;
        private IAppUserService AppUserService;
        private ICustomerLeadService CustomerLeadService;
        private IContactService ContactService;
        private ICompanyService CompanyService;
        private IOrganizationService OrganizationService;
        private IOpportunityService OpportunityService;
        private ICurrentContext CurrentContext;
        public CallLogController(
            ICallCategoryService CallCategoryService,
            ICallStatusService CallStatusService,
            ICallTypeService CallTypeService,
            ICustomerService CustomerService,
            ICallEmotionService CallEmotionService,
            ICallLogService CallLogService,
            IEntityReferenceService EntityReferenceService,
            IAppUserService AppUserService,
            ICustomerLeadService CustomerLeadService,
            IContactService ContactService,
            ICompanyService CompanyService,
            IOrganizationService OrganizationService,
            IOpportunityService OpportunityService,
            ICurrentContext CurrentContext
       , IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ) : base(httpContextAccessor, _DataContext)
        {
            this.CallCategoryService = CallCategoryService;
            this.CallStatusService = CallStatusService;
            this.CallTypeService = CallTypeService;
            this.CustomerService = CustomerService;
            this.CallEmotionService = CallEmotionService;
            this.CallLogService = CallLogService;
            this.EntityReferenceService = EntityReferenceService;
            this.AppUserService = AppUserService;
            this.CustomerLeadService = CustomerLeadService;
            this.ContactService = ContactService;
            this.CompanyService = CompanyService;
            this.OrganizationService = OrganizationService;
            this.OpportunityService = OpportunityService;
            this.CurrentContext = CurrentContext;
        }

        [Route(CallLogRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CallLog_CallLogFilterDTO CallLog_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertFilterDTOToFilterEntity(CallLog_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            int count = await CallLogService.Count(CallLogFilter);
            return count;
        }

        [Route(CallLogRoute.List), HttpPost]
        public async Task<ActionResult<List<CallLog_CallLogDTO>>> List([FromBody] CallLog_CallLogFilterDTO CallLog_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertFilterDTOToFilterEntity(CallLog_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);
            List<CallLog_CallLogDTO> CallLog_CallLogDTOs = CallLogs
                .Select(c => new CallLog_CallLogDTO(c)).ToList();
            return CallLog_CallLogDTOs;
        }

        [Route(CallLogRoute.Get), HttpPost]
        public async Task<ActionResult<CallLog_CallLogDTO>> Get([FromBody] CallLog_CallLogDTO CallLog_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CallLog_CallLogDTO.Id))
                return Forbid();

            CallLog CallLog = await CallLogService.Get(CallLog_CallLogDTO.Id);
            return new CallLog_CallLogDTO(CallLog);
        }

        [Route(CallLogRoute.Create), HttpPost]
        public async Task<ActionResult<CallLog_CallLogDTO>> Create([FromBody] CallLog_CallLogDTO CallLog_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CallLog_CallLogDTO.Id))
                return Forbid();

            CallLog CallLog = ConvertDTOToEntity(CallLog_CallLogDTO);

            CallLog.CreatorId = CurrentContext.UserId;
            CallLog = await CallLogService.Create(CallLog);
            CallLog_CallLogDTO = new CallLog_CallLogDTO(CallLog);
            if (CallLog.IsValidated)
                return CallLog_CallLogDTO;
            else
                return BadRequest(CallLog_CallLogDTO);
        }

        [Route(CallLogRoute.Update), HttpPost]
        public async Task<ActionResult<CallLog_CallLogDTO>> Update([FromBody] CallLog_CallLogDTO CallLog_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CallLog_CallLogDTO.Id))
                return Forbid();

            CallLog CallLog = ConvertDTOToEntity(CallLog_CallLogDTO);
            CallLog = await CallLogService.Update(CallLog);
            CallLog_CallLogDTO = new CallLog_CallLogDTO(CallLog);
            if (CallLog.IsValidated)
                return CallLog_CallLogDTO;
            else
                return BadRequest(CallLog_CallLogDTO);
        }

        [Route(CallLogRoute.Delete), HttpPost]
        public async Task<ActionResult<CallLog_CallLogDTO>> Delete([FromBody] CallLog_CallLogDTO CallLog_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CallLog_CallLogDTO.Id))
                return Forbid();

            CallLog CallLog = ConvertDTOToEntity(CallLog_CallLogDTO);
            CallLog = await CallLogService.Delete(CallLog);
            CallLog_CallLogDTO = new CallLog_CallLogDTO(CallLog);
            if (CallLog.IsValidated)
                return CallLog_CallLogDTO;
            else
                return BadRequest(CallLog_CallLogDTO);
        }

        [Route(CallLogRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            CallLogFilter.Id = new IdFilter { In = Ids };
            CallLogFilter.Selects = CallLogSelect.Id;
            CallLogFilter.Skip = 0;
            CallLogFilter.Take = int.MaxValue;

            List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);
            CallLogs = await CallLogService.BulkDelete(CallLogs);
            if (CallLogs.Any(x => !x.IsValidated))
                return BadRequest(CallLogs.Where(x => !x.IsValidated));
            return true;
        }

        [Route(CallLogRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            AppUserFilter AppUserFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            EntityReferenceFilter EntityReferenceFilter = new EntityReferenceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = EntityReferenceSelect.ALL
            };
            List<EntityReference> EntityReferences = await EntityReferenceService.List(EntityReferenceFilter);
            CallTypeFilter CallTypeFilter = new CallTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CallTypeSelect.ALL
            };
            List<CallType> CallTypes = await CallTypeService.List(CallTypeFilter);
            CallEmotionFilter CallEmotionFilter = new CallEmotionFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CallEmotionSelect.ALL
            };
            List<CallEmotion> CallEmotions = await CallEmotionService.List(CallEmotionFilter);
            List<CallLog> CallLogs = new List<CallLog>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(CallLogs);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int EntityReferenceIdColumn = 1 + StartColumn;
                int CallTypeIdColumn = 2 + StartColumn;
                int CallEmotionIdColumn = 3 + StartColumn;
                int AppUserIdColumn = 4 + StartColumn;
                int TitleColumn = 5 + StartColumn;
                int ContentColumn = 6 + StartColumn;
                int PhoneColumn = 7 + StartColumn;
                int CallTimeColumn = 8 + StartColumn;
                int UsedColumn = 12 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string EntityReferenceIdValue = worksheet.Cells[i + StartRow, EntityReferenceIdColumn].Value?.ToString();
                    string CallTypeIdValue = worksheet.Cells[i + StartRow, CallTypeIdColumn].Value?.ToString();
                    string CallEmotionIdValue = worksheet.Cells[i + StartRow, CallEmotionIdColumn].Value?.ToString();
                    string AppUserIdValue = worksheet.Cells[i + StartRow, AppUserIdColumn].Value?.ToString();
                    string TitleValue = worksheet.Cells[i + StartRow, TitleColumn].Value?.ToString();
                    string ContentValue = worksheet.Cells[i + StartRow, ContentColumn].Value?.ToString();
                    string PhoneValue = worksheet.Cells[i + StartRow, PhoneColumn].Value?.ToString();
                    string CallTimeValue = worksheet.Cells[i + StartRow, CallTimeColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();

                    CallLog CallLog = new CallLog();
                    CallLog.Title = TitleValue;
                    CallLog.Content = ContentValue;
                    CallLog.Phone = PhoneValue;
                    CallLog.CallTime = DateTime.TryParse(CallTimeValue, out DateTime CallTime) ? CallTime : DateTime.Now;
                    AppUser AppUser = AppUsers.Where(x => x.Id.ToString() == AppUserIdValue).FirstOrDefault();
                    CallLog.AppUserId = AppUser == null ? 0 : AppUser.Id;
                    CallLog.AppUser = AppUser;
                    EntityReference EntityReference = EntityReferences.Where(x => x.Id.ToString() == EntityReferenceIdValue).FirstOrDefault();
                    CallLog.EntityReferenceId = EntityReference == null ? 0 : EntityReference.Id;
                    CallLog.EntityReference = EntityReference;
                    CallType CallType = CallTypes.Where(x => x.Id.ToString() == CallTypeIdValue).FirstOrDefault();
                    CallLog.CallTypeId = CallType == null ? 0 : CallType.Id;
                    CallLog.CallType = CallType;
                    CallEmotion CallEmotion = CallEmotions.Where(x => x.Id.ToString() == CallEmotionIdValue).FirstOrDefault();
                    CallLog.CallEmotionId = CallEmotion == null ? 0 : CallEmotion.Id;
                    CallLog.CallEmotion = CallEmotion;

                    CallLogs.Add(CallLog);
                }
            }
            CallLogs = await CallLogService.Import(CallLogs);
            if (CallLogs.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < CallLogs.Count; i++)
                {
                    CallLog CallLog = CallLogs[i];
                    if (!CallLog.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.Id)))
                            Error += CallLog.Errors[nameof(CallLog.Id)];
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.EntityReferenceId)))
                            Error += CallLog.Errors[nameof(CallLog.EntityReferenceId)];
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.CallTypeId)))
                            Error += CallLog.Errors[nameof(CallLog.CallTypeId)];
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.CallEmotionId)))
                            Error += CallLog.Errors[nameof(CallLog.CallEmotionId)];
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.AppUserId)))
                            Error += CallLog.Errors[nameof(CallLog.AppUserId)];
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.Title)))
                            Error += CallLog.Errors[nameof(CallLog.Title)];
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.Content)))
                            Error += CallLog.Errors[nameof(CallLog.Content)];
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.Phone)))
                            Error += CallLog.Errors[nameof(CallLog.Phone)];
                        if (CallLog.Errors.ContainsKey(nameof(CallLog.CallTime)))
                            Error += CallLog.Errors[nameof(CallLog.CallTime)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(CallLogRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] CallLog_CallLogFilterDTO CallLog_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CallLog
                var CallLogFilter = ConvertFilterDTOToFilterEntity(CallLog_CallLogFilterDTO);
                CallLogFilter.Skip = 0;
                CallLogFilter.Take = int.MaxValue;
                CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
                List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);

                var CallLogHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "EntityReferenceId",
                        "CallTypeId",
                        "CallEmotionId",
                        "AppUserId",
                        "Title",
                        "Content",
                        "Phone",
                        "CallTime",
                        "Used",
                    }
                };
                List<object[]> CallLogData = new List<object[]>();
                for (int i = 0; i < CallLogs.Count; i++)
                {
                    var CallLog = CallLogs[i];
                    CallLogData.Add(new Object[]
                    {
                        CallLog.Id,
                        CallLog.EntityReferenceId,
                        CallLog.CallTypeId,
                        CallLog.CallEmotionId,
                        CallLog.AppUserId,
                        CallLog.Title,
                        CallLog.Content,
                        CallLog.Phone,
                        CallLog.CallTime,
                    });
                }
                excel.GenerateWorksheet("CallLog", CallLogHeaders, CallLogData);
                #endregion

                #region AppUser
                var AppUserFilter = new AppUserFilter();
                AppUserFilter.Selects = AppUserSelect.ALL;
                AppUserFilter.OrderBy = AppUserOrder.Id;
                AppUserFilter.OrderType = OrderType.ASC;
                AppUserFilter.Skip = 0;
                AppUserFilter.Take = int.MaxValue;
                List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);

                var AppUserHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Username",
                        "DisplayName",
                        "Address",
                        "Email",
                        "Phone",
                        "SexId",
                        "Birthday",
                        "Avatar",
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "ProvinceId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
                    }
                };
                List<object[]> AppUserData = new List<object[]>();
                for (int i = 0; i < AppUsers.Count; i++)
                {
                    var AppUser = AppUsers[i];
                    AppUserData.Add(new Object[]
                    {
                        AppUser.Id,
                        AppUser.Username,
                        AppUser.DisplayName,
                        AppUser.Address,
                        AppUser.Email,
                        AppUser.Phone,
                        AppUser.SexId,
                        AppUser.Birthday,
                        AppUser.Avatar,
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.ProvinceId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                #region EntityReference
                var EntityReferenceFilter = new EntityReferenceFilter();
                EntityReferenceFilter.Selects = EntityReferenceSelect.ALL;
                EntityReferenceFilter.OrderBy = EntityReferenceOrder.Id;
                EntityReferenceFilter.OrderType = OrderType.ASC;
                EntityReferenceFilter.Skip = 0;
                EntityReferenceFilter.Take = int.MaxValue;
                List<EntityReference> EntityReferences = await EntityReferenceService.List(EntityReferenceFilter);

                var EntityReferenceHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> EntityReferenceData = new List<object[]>();
                for (int i = 0; i < EntityReferences.Count; i++)
                {
                    var EntityReference = EntityReferences[i];
                    EntityReferenceData.Add(new Object[]
                    {
                        EntityReference.Id,
                        EntityReference.Code,
                        EntityReference.Name,
                    });
                }
                excel.GenerateWorksheet("EntityReference", EntityReferenceHeaders, EntityReferenceData);
                #endregion
                #region CallType
                var CallTypeFilter = new CallTypeFilter();
                CallTypeFilter.Selects = CallTypeSelect.ALL;
                CallTypeFilter.OrderBy = CallTypeOrder.Id;
                CallTypeFilter.OrderType = OrderType.ASC;
                CallTypeFilter.Skip = 0;
                CallTypeFilter.Take = int.MaxValue;
                List<CallType> CallTypes = await CallTypeService.List(CallTypeFilter);

                var CallTypeHeaders = new List<string[]>()
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
                List<object[]> CallTypeData = new List<object[]>();
                for (int i = 0; i < CallTypes.Count; i++)
                {
                    var CallType = CallTypes[i];
                    CallTypeData.Add(new Object[]
                    {
                        CallType.Id,
                        CallType.Code,
                        CallType.Name,
                        CallType.ColorCode,
                        CallType.StatusId,
                        CallType.Used,
                    });
                }
                excel.GenerateWorksheet("CallType", CallTypeHeaders, CallTypeData);
                #endregion
                #region CallEmotion
                var CallEmotionFilter = new CallEmotionFilter();
                CallEmotionFilter.Selects = CallEmotionSelect.ALL;
                CallEmotionFilter.OrderBy = CallEmotionOrder.Id;
                CallEmotionFilter.OrderType = OrderType.ASC;
                CallEmotionFilter.Skip = 0;
                CallEmotionFilter.Take = int.MaxValue;
                List<CallEmotion> CallEmotions = await CallEmotionService.List(CallEmotionFilter);

                var CallEmotionHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "Code",
                        "StatusId",
                        "DisplayOrder",
                        "Description",
                    }
                };
                List<object[]> CallEmotionData = new List<object[]>();
                for (int i = 0; i < CallEmotions.Count; i++)
                {
                    var CallEmotion = CallEmotions[i];
                    CallEmotionData.Add(new Object[]
                    {
                        CallEmotion.Id,
                        CallEmotion.Name,
                        CallEmotion.Code,
                        CallEmotion.StatusId,
                        CallEmotion.Description,
                    });
                }
                excel.GenerateWorksheet("CallEmotion", CallEmotionHeaders, CallEmotionData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "CallLog.xlsx");
        }
        [Route(CallLogRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] CallLog_CallLogFilterDTO CallLog_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CallLog
                var CallLogHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "EntityReferenceId",
                        "CallTypeId",
                        "CallEmotionId",
                        "AppUserId",
                        "Title",
                        "Content",
                        "Phone",
                        "CallTime",
                        "Used",
                    }
                };
                List<object[]> CallLogData = new List<object[]>();
                excel.GenerateWorksheet("CallLog", CallLogHeaders, CallLogData);
                #endregion

                #region AppUser
                var AppUserFilter = new AppUserFilter();
                AppUserFilter.Selects = AppUserSelect.ALL;
                AppUserFilter.OrderBy = AppUserOrder.Id;
                AppUserFilter.OrderType = OrderType.ASC;
                AppUserFilter.Skip = 0;
                AppUserFilter.Take = int.MaxValue;
                List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);

                var AppUserHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Username",
                        "DisplayName",
                        "Address",
                        "Email",
                        "Phone",
                        "SexId",
                        "Birthday",
                        "Avatar",
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "ProvinceId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
                    }
                };
                List<object[]> AppUserData = new List<object[]>();
                for (int i = 0; i < AppUsers.Count; i++)
                {
                    var AppUser = AppUsers[i];
                    AppUserData.Add(new Object[]
                    {
                        AppUser.Id,
                        AppUser.Username,
                        AppUser.DisplayName,
                        AppUser.Address,
                        AppUser.Email,
                        AppUser.Phone,
                        AppUser.SexId,
                        AppUser.Birthday,
                        AppUser.Avatar,
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.ProvinceId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                #region EntityReference
                var EntityReferenceFilter = new EntityReferenceFilter();
                EntityReferenceFilter.Selects = EntityReferenceSelect.ALL;
                EntityReferenceFilter.OrderBy = EntityReferenceOrder.Id;
                EntityReferenceFilter.OrderType = OrderType.ASC;
                EntityReferenceFilter.Skip = 0;
                EntityReferenceFilter.Take = int.MaxValue;
                List<EntityReference> EntityReferences = await EntityReferenceService.List(EntityReferenceFilter);

                var EntityReferenceHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> EntityReferenceData = new List<object[]>();
                for (int i = 0; i < EntityReferences.Count; i++)
                {
                    var EntityReference = EntityReferences[i];
                    EntityReferenceData.Add(new Object[]
                    {
                        EntityReference.Id,
                        EntityReference.Code,
                        EntityReference.Name,
                    });
                }
                excel.GenerateWorksheet("EntityReference", EntityReferenceHeaders, EntityReferenceData);
                #endregion
                #region CallType
                var CallTypeFilter = new CallTypeFilter();
                CallTypeFilter.Selects = CallTypeSelect.ALL;
                CallTypeFilter.OrderBy = CallTypeOrder.Id;
                CallTypeFilter.OrderType = OrderType.ASC;
                CallTypeFilter.Skip = 0;
                CallTypeFilter.Take = int.MaxValue;
                List<CallType> CallTypes = await CallTypeService.List(CallTypeFilter);

                var CallTypeHeaders = new List<string[]>()
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
                List<object[]> CallTypeData = new List<object[]>();
                for (int i = 0; i < CallTypes.Count; i++)
                {
                    var CallType = CallTypes[i];
                    CallTypeData.Add(new Object[]
                    {
                        CallType.Id,
                        CallType.Code,
                        CallType.Name,
                        CallType.ColorCode,
                        CallType.StatusId,
                        CallType.Used,
                    });
                }
                excel.GenerateWorksheet("CallType", CallTypeHeaders, CallTypeData);
                #endregion
                #region CallEmotion
                var CallEmotionFilter = new CallEmotionFilter();
                CallEmotionFilter.Selects = CallEmotionSelect.ALL;
                CallEmotionFilter.OrderBy = CallEmotionOrder.Id;
                CallEmotionFilter.OrderType = OrderType.ASC;
                CallEmotionFilter.Skip = 0;
                CallEmotionFilter.Take = int.MaxValue;
                List<CallEmotion> CallEmotions = await CallEmotionService.List(CallEmotionFilter);

                var CallEmotionHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "Code",
                        "StatusId",
                        "DisplayOrder",
                        "Description",
                    }
                };
                List<object[]> CallEmotionData = new List<object[]>();
                for (int i = 0; i < CallEmotions.Count; i++)
                {
                    var CallEmotion = CallEmotions[i];
                    CallEmotionData.Add(new Object[]
                    {
                        CallEmotion.Id,
                        CallEmotion.Name,
                        CallEmotion.Code,
                        CallEmotion.StatusId,
                        CallEmotion.Description,
                    });
                }
                excel.GenerateWorksheet("CallEmotion", CallEmotionHeaders, CallEmotionData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "CallLog.xlsx");
        }
        private async Task<bool> HasPermission(long Id)
        {
            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            if (Id == 0)
            {

            }
            else
            {
                CallLogFilter.Id = new IdFilter { Equal = Id };
                int count = await CallLogService.Count(CallLogFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private CallLog ConvertDTOToEntity(CallLog_CallLogDTO CallLog_CallLogDTO)
        {
            CallLog CallLog = new CallLog();
            CallLog.Id = CallLog_CallLogDTO.Id;
            CallLog.Title = CallLog_CallLogDTO.Title;
            CallLog.Content = CallLog_CallLogDTO.Content;
            CallLog.Phone = CallLog_CallLogDTO.Phone;
            CallLog.CallTime = CallLog_CallLogDTO.CallTime;
            CallLog.EntityReferenceId = CallLog_CallLogDTO.EntityReferenceId;
            CallLog.EntityId = CallLog_CallLogDTO.EntityId;
            CallLog.CallTypeId = CallLog_CallLogDTO.CallTypeId;
            CallLog.CallCategoryId = CallLog_CallLogDTO.CallCategoryId;
            CallLog.CallEmotionId = CallLog_CallLogDTO.CallEmotionId;
            CallLog.CallStatusId = CallLog_CallLogDTO.CallStatusId;
            CallLog.AppUserId = CallLog_CallLogDTO.AppUserId;
            CallLog.CreatorId = CallLog_CallLogDTO.CreatorId;
            CallLog.AppUser = CallLog_CallLogDTO.AppUser == null ? null : new AppUser
            {
                Id = CallLog_CallLogDTO.AppUser.Id,
                Username = CallLog_CallLogDTO.AppUser.Username,
                DisplayName = CallLog_CallLogDTO.AppUser.DisplayName,
                Address = CallLog_CallLogDTO.AppUser.Address,
                Email = CallLog_CallLogDTO.AppUser.Email,
                Phone = CallLog_CallLogDTO.AppUser.Phone,
                SexId = CallLog_CallLogDTO.AppUser.SexId,
                Birthday = CallLog_CallLogDTO.AppUser.Birthday,
                Avatar = CallLog_CallLogDTO.AppUser.Avatar,
                Department = CallLog_CallLogDTO.AppUser.Department,
                OrganizationId = CallLog_CallLogDTO.AppUser.OrganizationId,
                Longitude = CallLog_CallLogDTO.AppUser.Longitude,
                Latitude = CallLog_CallLogDTO.AppUser.Latitude,
                StatusId = CallLog_CallLogDTO.AppUser.StatusId,
            };
            CallLog.CallCategory = CallLog_CallLogDTO.CallCategory == null ? null : new CallCategory
            {
                Id = CallLog_CallLogDTO.CallCategory.Id,
                Code = CallLog_CallLogDTO.CallCategory.Code,
                Name = CallLog_CallLogDTO.CallCategory.Name,
            };
            CallLog.CallEmotion = CallLog_CallLogDTO.CallEmotion == null ? null : new CallEmotion
            {
                Id = CallLog_CallLogDTO.CallEmotion.Id,
                Code = CallLog_CallLogDTO.CallEmotion.Code,
                Name = CallLog_CallLogDTO.CallEmotion.Name,
                StatusId = CallLog_CallLogDTO.CallEmotion.StatusId,
                Description = CallLog_CallLogDTO.CallEmotion.Description,
            };
            CallLog.CallStatus = CallLog_CallLogDTO.CallStatus == null ? null : new CallStatus
            {
                Id = CallLog_CallLogDTO.CallStatus.Id,
                Code = CallLog_CallLogDTO.CallStatus.Code,
                Name = CallLog_CallLogDTO.CallStatus.Name,
            };
            CallLog.CallType = CallLog_CallLogDTO.CallType == null ? null : new CallType
            {
                Id = CallLog_CallLogDTO.CallType.Id,
                Code = CallLog_CallLogDTO.CallType.Code,
                Name = CallLog_CallLogDTO.CallType.Name,
                ColorCode = CallLog_CallLogDTO.CallType.ColorCode,
                StatusId = CallLog_CallLogDTO.CallType.StatusId,
                Used = CallLog_CallLogDTO.CallType.Used,
            };
            CallLog.Creator = CallLog_CallLogDTO.Creator == null ? null : new AppUser
            {
                Id = CallLog_CallLogDTO.Creator.Id,
                Username = CallLog_CallLogDTO.Creator.Username,
                DisplayName = CallLog_CallLogDTO.Creator.DisplayName,
                Address = CallLog_CallLogDTO.Creator.Address,
                Email = CallLog_CallLogDTO.Creator.Email,
                Phone = CallLog_CallLogDTO.Creator.Phone,
                SexId = CallLog_CallLogDTO.Creator.SexId,
                Birthday = CallLog_CallLogDTO.Creator.Birthday,
                Avatar = CallLog_CallLogDTO.Creator.Avatar,
                Department = CallLog_CallLogDTO.Creator.Department,
                OrganizationId = CallLog_CallLogDTO.Creator.OrganizationId,
                Longitude = CallLog_CallLogDTO.Creator.Longitude,
                Latitude = CallLog_CallLogDTO.Creator.Latitude,
                StatusId = CallLog_CallLogDTO.Creator.StatusId,
            };
            CallLog.EntityReference = CallLog_CallLogDTO.EntityReference == null ? null : new EntityReference
            {
                Id = CallLog_CallLogDTO.EntityReference.Id,
                Code = CallLog_CallLogDTO.EntityReference.Code,
                Name = CallLog_CallLogDTO.EntityReference.Name,
            };
            CallLog.BaseLanguage = CurrentContext.Language;
            return CallLog;
        }

        private CallLogFilter ConvertFilterDTOToFilterEntity(CallLog_CallLogFilterDTO CallLog_CallLogFilterDTO)
        {
            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter.Selects = CallLogSelect.ALL;
            CallLogFilter.Skip = CallLog_CallLogFilterDTO.Skip;
            CallLogFilter.Take = CallLog_CallLogFilterDTO.Take;
            CallLogFilter.OrderBy = CallLog_CallLogFilterDTO.OrderBy;
            CallLogFilter.OrderType = CallLog_CallLogFilterDTO.OrderType;

            CallLogFilter.Id = CallLog_CallLogFilterDTO.Id;
            CallLogFilter.Title = CallLog_CallLogFilterDTO.Title;
            CallLogFilter.Content = CallLog_CallLogFilterDTO.Content;
            CallLogFilter.Phone = CallLog_CallLogFilterDTO.Phone;
            CallLogFilter.CallTime = CallLog_CallLogFilterDTO.CallTime;
            CallLogFilter.EntityReferenceId = CallLog_CallLogFilterDTO.EntityReferenceId;
            CallLogFilter.EntityId = CallLog_CallLogFilterDTO.EntityId;
            CallLogFilter.CallTypeId = CallLog_CallLogFilterDTO.CallTypeId;
            CallLogFilter.CallCategoryId = CallLog_CallLogFilterDTO.CallCategoryId;
            CallLogFilter.CallEmotionId = CallLog_CallLogFilterDTO.CallEmotionId;
            CallLogFilter.CallStatusId = CallLog_CallLogFilterDTO.CallStatusId;
            CallLogFilter.AppUserId = CallLog_CallLogFilterDTO.AppUserId;
            CallLogFilter.CreatorId = CallLog_CallLogFilterDTO.CreatorId;
            CallLogFilter.CreatedAt = CallLog_CallLogFilterDTO.CreatedAt;
            CallLogFilter.UpdatedAt = CallLog_CallLogFilterDTO.UpdatedAt;
            return CallLogFilter;
        }
    }
}

