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

namespace CRM.Services.MSmsQueueStatus
{
    public interface ISmsQueueStatusService :  IServiceScoped
    {
        Task<int> Count(SmsQueueStatusFilter SmsQueueStatusFilter);
        Task<List<SmsQueueStatus>> List(SmsQueueStatusFilter SmsQueueStatusFilter);
        Task<SmsQueueStatus> Get(long Id);
        Task<SmsQueueStatus> Create(SmsQueueStatus SmsQueueStatus);
        Task<SmsQueueStatus> Update(SmsQueueStatus SmsQueueStatus);
        Task<SmsQueueStatus> Delete(SmsQueueStatus SmsQueueStatus);
        Task<List<SmsQueueStatus>> BulkDelete(List<SmsQueueStatus> SmsQueueStatuses);
        Task<List<SmsQueueStatus>> Import(List<SmsQueueStatus> SmsQueueStatuses);
        SmsQueueStatusFilter ToFilter(SmsQueueStatusFilter SmsQueueStatusFilter);
    }

    public class SmsQueueStatusService : BaseService, ISmsQueueStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISmsQueueStatusValidator SmsQueueStatusValidator;

        public SmsQueueStatusService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISmsQueueStatusValidator SmsQueueStatusValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SmsQueueStatusValidator = SmsQueueStatusValidator;
        }
        public async Task<int> Count(SmsQueueStatusFilter SmsQueueStatusFilter)
        {
            try
            {
                int result = await UOW.SmsQueueStatusRepository.Count(SmsQueueStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SmsQueueStatus>> List(SmsQueueStatusFilter SmsQueueStatusFilter)
        {
            try
            {
                List<SmsQueueStatus> SmsQueueStatuss = await UOW.SmsQueueStatusRepository.List(SmsQueueStatusFilter);
                return SmsQueueStatuss;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SmsQueueStatus> Get(long Id)
        {
            SmsQueueStatus SmsQueueStatus = await UOW.SmsQueueStatusRepository.Get(Id);
            if (SmsQueueStatus == null)
                return null;
            return SmsQueueStatus;
        }
       
        public async Task<SmsQueueStatus> Create(SmsQueueStatus SmsQueueStatus)
        {
            if (!await SmsQueueStatusValidator.Create(SmsQueueStatus))
                return SmsQueueStatus;

            try
            {
                await UOW.Begin();
                await UOW.SmsQueueStatusRepository.Create(SmsQueueStatus);
                await UOW.Commit();
                SmsQueueStatus = await UOW.SmsQueueStatusRepository.Get(SmsQueueStatus.Id);
                await Logging.CreateAuditLog(SmsQueueStatus, new { }, nameof(SmsQueueStatusService));
                return SmsQueueStatus;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SmsQueueStatus> Update(SmsQueueStatus SmsQueueStatus)
        {
            if (!await SmsQueueStatusValidator.Update(SmsQueueStatus))
                return SmsQueueStatus;
            try
            {
                var oldData = await UOW.SmsQueueStatusRepository.Get(SmsQueueStatus.Id);

                await UOW.Begin();
                await UOW.SmsQueueStatusRepository.Update(SmsQueueStatus);
                await UOW.Commit();

                SmsQueueStatus = await UOW.SmsQueueStatusRepository.Get(SmsQueueStatus.Id);
                await Logging.CreateAuditLog(SmsQueueStatus, oldData, nameof(SmsQueueStatusService));
                return SmsQueueStatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SmsQueueStatus> Delete(SmsQueueStatus SmsQueueStatus)
        {
            if (!await SmsQueueStatusValidator.Delete(SmsQueueStatus))
                return SmsQueueStatus;

            try
            {
                await UOW.Begin();
                await UOW.SmsQueueStatusRepository.Delete(SmsQueueStatus);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SmsQueueStatus, nameof(SmsQueueStatusService));
                return SmsQueueStatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SmsQueueStatus>> BulkDelete(List<SmsQueueStatus> SmsQueueStatuses)
        {
            if (!await SmsQueueStatusValidator.BulkDelete(SmsQueueStatuses))
                return SmsQueueStatuses;

            try
            {
                await UOW.Begin();
                await UOW.SmsQueueStatusRepository.BulkDelete(SmsQueueStatuses);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SmsQueueStatuses, nameof(SmsQueueStatusService));
                return SmsQueueStatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SmsQueueStatus>> Import(List<SmsQueueStatus> SmsQueueStatuses)
        {
            if (!await SmsQueueStatusValidator.Import(SmsQueueStatuses))
                return SmsQueueStatuses;
            try
            {
                await UOW.Begin();
                await UOW.SmsQueueStatusRepository.BulkMerge(SmsQueueStatuses);
                await UOW.Commit();

                await Logging.CreateAuditLog(SmsQueueStatuses, new { }, nameof(SmsQueueStatusService));
                return SmsQueueStatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SmsQueueStatusFilter ToFilter(SmsQueueStatusFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SmsQueueStatusFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SmsQueueStatusFilter subFilter = new SmsQueueStatusFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
