using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;
using CRM.Handlers;
using CRM.Enums;
using CRM.Services.MOrganization;

namespace CRM.Services.MCallLog
{
    public interface ICallLogService : IServiceScoped
    {
        Task<int> Count(CallLogFilter CallLogFilter);
        Task<List<CallLog>> List(CallLogFilter CallLogFilter);
        Task<CallLog> Get(long Id);
        Task<CallLog> Create(CallLog CallLog);
        Task<CallLog> Update(CallLog CallLog);
        Task<CallLog> Delete(CallLog CallLog);
        Task<List<CallLog>> BulkDelete(List<CallLog> CallLogs);
        Task<List<CallLog>> Import(List<CallLog> CallLogs);
        Task<CallLogFilter> ToFilter(CallLogFilter CallLogFilter);
    }

    public class CallLogService : BaseService, ICallLogService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICallLogValidator CallLogValidator;
        private IRabbitManager RabbitManager;
        private IOrganizationService OrganizationService;
        public CallLogService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ICallLogValidator CallLogValidator,
            IOrganizationService OrganizationService,
            IRabbitManager RabbitManager
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CallLogValidator = CallLogValidator;
            this.RabbitManager = RabbitManager;
            this.OrganizationService = OrganizationService;
        }
        public async Task<int> Count(CallLogFilter CallLogFilter)
        {
            try
            {
                int result = await UOW.CallLogRepository.Count(CallLogFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallLogService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallLogService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<CallLog>> List(CallLogFilter CallLogFilter)
        {
            try
            {
                List<CallLog> CallLogs = await UOW.CallLogRepository.List(CallLogFilter);
                return CallLogs;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallLogService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallLogService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<CallLog> Get(long Id)
        {
            CallLog CallLog = await UOW.CallLogRepository.Get(Id);
            if (CallLog == null)
                return null;
            return CallLog;
        }

        public async Task<CallLog> Create(CallLog CallLog)
        {
            if (!await CallLogValidator.Create(CallLog))
                return CallLog;

            try
            {
                CallLog.CreatorId = CurrentContext.UserId;
                await UOW.Begin();
                await UOW.CallLogRepository.Create(CallLog);
                await UOW.Commit();
                CallLog = await UOW.CallLogRepository.Get(CallLog.Id);
                NotifyUsed(CallLog);
                await Logging.CreateAuditLog(CallLog, new { }, nameof(CallLogService));
                return CallLog;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallLogService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallLogService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CallLog> Update(CallLog CallLog)
        {
            if (!await CallLogValidator.Update(CallLog))
                return CallLog;
            try
            {
                var oldData = await UOW.CallLogRepository.Get(CallLog.Id);

                await UOW.Begin();
                await UOW.CallLogRepository.Update(CallLog);
                await UOW.Commit();

                CallLog = await UOW.CallLogRepository.Get(CallLog.Id);
                NotifyUsed(CallLog);
                await Logging.CreateAuditLog(CallLog, oldData, nameof(CallLogService));
                return CallLog;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallLogService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallLogService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CallLog> Delete(CallLog CallLog)
        {
            if (!await CallLogValidator.Delete(CallLog))
                return CallLog;

            try
            {
                await UOW.Begin();
                await UOW.CallLogRepository.Delete(CallLog);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, CallLog, nameof(CallLogService));
                return CallLog;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallLogService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallLogService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<CallLog>> BulkDelete(List<CallLog> CallLogs)
        {
            if (!await CallLogValidator.BulkDelete(CallLogs))
                return CallLogs;

            try
            {
                await UOW.Begin();
                await UOW.CallLogRepository.BulkDelete(CallLogs);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, CallLogs, nameof(CallLogService));
                return CallLogs;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallLogService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallLogService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<CallLog>> Import(List<CallLog> CallLogs)
        {
            if (!await CallLogValidator.Import(CallLogs))
                return CallLogs;
            try
            {
                await UOW.Begin();
                await UOW.CallLogRepository.BulkMerge(CallLogs);
                await UOW.Commit();

                await Logging.CreateAuditLog(CallLogs, new { }, nameof(CallLogService));
                return CallLogs;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CallLogService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CallLogService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CallLogFilter> ToFilter(CallLogFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CallLogFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            List<Organization> Organizations = await OrganizationService.List(new OrganizationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrganizationSelect.ALL,
                OrderBy = OrganizationOrder.Id,
                OrderType = OrderType.ASC
            });
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CallLogFilter subFilter = new CallLogFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                }
            }
            return filter;
        }

        private void NotifyUsed(CallLog CallLog)
        {
            //if (CallLog.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_RETAIL.Id)
            //{
            //    EventMessage<CustomerRetail> CustomerRetailMessage = new EventMessage<CustomerRetail>
            //    {
            //        Content = new CustomerRetail { Id = CallLog.Mapping.Id },
            //        EntityName = nameof(CustomerRetail),
            //        RowId = Guid.NewGuid(),
            //        Time = StaticParams.DateTimeNow,
            //    };
            //    RabbitManager.PublishSingle(CustomerRetailMessage, RoutingKeyEnum.CustomerRetailUsed);
            //}

            //else if (CallLog.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_AGENT.Id)
            //{
            //    EventMessage<CustomerAgent> CustomerAgentMessage = new EventMessage<CustomerAgent>
            //    {
            //        Content = new CustomerAgent { Id = CallLog.Mapping.Id },
            //        EntityName = nameof(CustomerAgent),
            //        RowId = Guid.NewGuid(),
            //        Time = StaticParams.DateTimeNow,
            //    };
            //    RabbitManager.PublishSingle(CustomerAgentMessage, RoutingKeyEnum.CustomerAgentUsed);
            //}
            //else if (CallLog.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_EXPORT.Id)
            //{
            //    EventMessage<CustomerExport> CustomerExportMessage = new EventMessage<CustomerExport>
            //    {
            //        Content = new CustomerExport { Id = CallLog.Mapping.Id },
            //        EntityName = nameof(CustomerExport),
            //        RowId = Guid.NewGuid(),
            //        Time = StaticParams.DateTimeNow,
            //    };
            //    RabbitManager.PublishSingle(CustomerExportMessage, RoutingKeyEnum.CustomerExportUsed);
            //}
            //else if (CallLog.EntityReferenceId == Enums.EntityReferenceEnum.CUSTOMER_PROJECT.Id)
            //{
            //    EventMessage<CustomerProject> CustomerProjectMessage = new EventMessage<CustomerProject>
            //    {
            //        Content = new CustomerProject { Id = CallLog.Mapping.Id },
            //        EntityName = nameof(CustomerProject),
            //        RowId = Guid.NewGuid(),
            //        Time = StaticParams.DateTimeNow,
            //    };
            //    RabbitManager.PublishSingle(CustomerProjectMessage, RoutingKeyEnum.CustomerProjectUsed);
            //}
        }
    }
}
