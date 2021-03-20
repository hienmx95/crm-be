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

namespace CRM.Services.MSmsQueue
{
    public interface ISmsQueueService :  IServiceScoped
    {
        Task<int> Count(SmsQueueFilter SmsQueueFilter);
        Task<List<SmsQueue>> List(SmsQueueFilter SmsQueueFilter);
        Task<SmsQueue> Get(long Id);
        Task<SmsQueue> Create(SmsQueue SmsQueue);
        Task<SmsQueue> Update(SmsQueue SmsQueue);
        Task<SmsQueue> Delete(SmsQueue SmsQueue);
        Task<List<SmsQueue>> BulkDelete(List<SmsQueue> SmsQueues);
        Task<List<SmsQueue>> Import(List<SmsQueue> SmsQueues);
        SmsQueueFilter ToFilter(SmsQueueFilter SmsQueueFilter);
    }

    public class SmsQueueService : BaseService, ISmsQueueService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISmsQueueValidator SmsQueueValidator;

        public SmsQueueService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISmsQueueValidator SmsQueueValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SmsQueueValidator = SmsQueueValidator;
        }
        public async Task<int> Count(SmsQueueFilter SmsQueueFilter)
        {
            try
            {
                int result = await UOW.SmsQueueRepository.Count(SmsQueueFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SmsQueue>> List(SmsQueueFilter SmsQueueFilter)
        {
            try
            {
                List<SmsQueue> SmsQueues = await UOW.SmsQueueRepository.List(SmsQueueFilter);
                return SmsQueues;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SmsQueue> Get(long Id)
        {
            SmsQueue SmsQueue = await UOW.SmsQueueRepository.Get(Id);
            if (SmsQueue == null)
                return null;
            return SmsQueue;
        }
       
        public async Task<SmsQueue> Create(SmsQueue SmsQueue)
        {
            if (!await SmsQueueValidator.Create(SmsQueue))
                return SmsQueue;

            try
            {
                await UOW.Begin();
                await UOW.SmsQueueRepository.Create(SmsQueue);
                await UOW.Commit();
                SmsQueue = await UOW.SmsQueueRepository.Get(SmsQueue.Id);
                await Logging.CreateAuditLog(SmsQueue, new { }, nameof(SmsQueueService));
                return SmsQueue;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SmsQueue> Update(SmsQueue SmsQueue)
        {
            if (!await SmsQueueValidator.Update(SmsQueue))
                return SmsQueue;
            try
            {
                var oldData = await UOW.SmsQueueRepository.Get(SmsQueue.Id);

                await UOW.Begin();
                await UOW.SmsQueueRepository.Update(SmsQueue);
                await UOW.Commit();

                SmsQueue = await UOW.SmsQueueRepository.Get(SmsQueue.Id);
                await Logging.CreateAuditLog(SmsQueue, oldData, nameof(SmsQueueService));
                return SmsQueue;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SmsQueue> Delete(SmsQueue SmsQueue)
        {
            if (!await SmsQueueValidator.Delete(SmsQueue))
                return SmsQueue;

            try
            {
                await UOW.Begin();
                await UOW.SmsQueueRepository.Delete(SmsQueue);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SmsQueue, nameof(SmsQueueService));
                return SmsQueue;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SmsQueue>> BulkDelete(List<SmsQueue> SmsQueues)
        {
            if (!await SmsQueueValidator.BulkDelete(SmsQueues))
                return SmsQueues;

            try
            {
                await UOW.Begin();
                await UOW.SmsQueueRepository.BulkDelete(SmsQueues);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SmsQueues, nameof(SmsQueueService));
                return SmsQueues;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SmsQueue>> Import(List<SmsQueue> SmsQueues)
        {
            if (!await SmsQueueValidator.Import(SmsQueues))
                return SmsQueues;
            try
            {
                await UOW.Begin();
                await UOW.SmsQueueRepository.BulkMerge(SmsQueues);
                await UOW.Commit();

                await Logging.CreateAuditLog(SmsQueues, new { }, nameof(SmsQueueService));
                return SmsQueues;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsQueueService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsQueueService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SmsQueueFilter ToFilter(SmsQueueFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SmsQueueFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SmsQueueFilter subFilter = new SmsQueueFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Phone))
                        
                        
                        
                        
                        
                        
                        subFilter.Phone = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SmsCode))
                        
                        
                        
                        
                        
                        
                        subFilter.SmsCode = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SmsTitle))
                        
                        
                        
                        
                        
                        
                        subFilter.SmsTitle = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SmsContent))
                        
                        
                        
                        
                        
                        
                        subFilter.SmsContent = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SmsQueueStatusId))
                        subFilter.SmsQueueStatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
