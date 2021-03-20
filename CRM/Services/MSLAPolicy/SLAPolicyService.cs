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

namespace CRM.Services.MSLAPolicy
{
    public interface ISLAPolicyService :  IServiceScoped
    {
        Task<int> Count(SLAPolicyFilter SLAPolicyFilter);
        Task<List<SLAPolicy>> List(SLAPolicyFilter SLAPolicyFilter);
        Task<SLAPolicy> Get(long Id);
        Task<SLAPolicy> Create(SLAPolicy SLAPolicy);
        Task<SLAPolicy> Update(SLAPolicy SLAPolicy);
        Task<SLAPolicy> Delete(SLAPolicy SLAPolicy);
        Task<List<SLAPolicy>> BulkDelete(List<SLAPolicy> SLAPolicies);
        Task<List<SLAPolicy>> Import(List<SLAPolicy> SLAPolicies);
        SLAPolicyFilter ToFilter(SLAPolicyFilter SLAPolicyFilter);
    }

    public class SLAPolicyService : BaseService, ISLAPolicyService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAPolicyValidator SLAPolicyValidator;

        public SLAPolicyService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAPolicyValidator SLAPolicyValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAPolicyValidator = SLAPolicyValidator;
        }
        public async Task<int> Count(SLAPolicyFilter SLAPolicyFilter)
        {
            try
            {
                int result = await UOW.SLAPolicyRepository.Count(SLAPolicyFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAPolicyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAPolicyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAPolicy>> List(SLAPolicyFilter SLAPolicyFilter)
        {
            try
            {
                List<SLAPolicy> SLAPolicys = await UOW.SLAPolicyRepository.List(SLAPolicyFilter);
                return SLAPolicys;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAPolicyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAPolicyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAPolicy> Get(long Id)
        {
            SLAPolicy SLAPolicy = await UOW.SLAPolicyRepository.Get(Id);
            if (SLAPolicy == null)
                return null;
            return SLAPolicy;
        }
       
        public async Task<SLAPolicy> Create(SLAPolicy SLAPolicy)
        {
            if (!await SLAPolicyValidator.Create(SLAPolicy))
                return SLAPolicy;

            try
            {
                await UOW.Begin();
                await UOW.SLAPolicyRepository.Create(SLAPolicy);
                await UOW.Commit();
                SLAPolicy = await UOW.SLAPolicyRepository.Get(SLAPolicy.Id);
                await Logging.CreateAuditLog(SLAPolicy, new { }, nameof(SLAPolicyService));
                return SLAPolicy;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAPolicyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAPolicyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAPolicy> Update(SLAPolicy SLAPolicy)
        {
            if (!await SLAPolicyValidator.Update(SLAPolicy))
                return SLAPolicy;
            try
            {
                var oldData = await UOW.SLAPolicyRepository.Get(SLAPolicy.Id);

                await UOW.Begin();
                await UOW.SLAPolicyRepository.Update(SLAPolicy);
                await UOW.Commit();

                SLAPolicy = await UOW.SLAPolicyRepository.Get(SLAPolicy.Id);
                await Logging.CreateAuditLog(SLAPolicy, oldData, nameof(SLAPolicyService));
                return SLAPolicy;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAPolicyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAPolicyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAPolicy> Delete(SLAPolicy SLAPolicy)
        {
            if (!await SLAPolicyValidator.Delete(SLAPolicy))
                return SLAPolicy;

            try
            {
                await UOW.Begin();
                await UOW.SLAPolicyRepository.Delete(SLAPolicy);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAPolicy, nameof(SLAPolicyService));
                return SLAPolicy;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAPolicyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAPolicyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAPolicy>> BulkDelete(List<SLAPolicy> SLAPolicies)
        {
            if (!await SLAPolicyValidator.BulkDelete(SLAPolicies))
                return SLAPolicies;

            try
            {
                await UOW.Begin();
                await UOW.SLAPolicyRepository.BulkDelete(SLAPolicies);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAPolicies, nameof(SLAPolicyService));
                return SLAPolicies;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAPolicyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAPolicyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAPolicy>> Import(List<SLAPolicy> SLAPolicies)
        {
            if (!await SLAPolicyValidator.Import(SLAPolicies))
                return SLAPolicies;
            try
            {
                await UOW.Begin();
                await UOW.SLAPolicyRepository.BulkMerge(SLAPolicies);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAPolicies, new { }, nameof(SLAPolicyService));
                return SLAPolicies;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAPolicyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAPolicyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAPolicyFilter ToFilter(SLAPolicyFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAPolicyFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAPolicyFilter subFilter = new SLAPolicyFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketIssueLevelId))
                        subFilter.TicketIssueLevelId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketPriorityId))
                        subFilter.TicketPriorityId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.FirstResponseTime))
                        
                        subFilter.FirstResponseTime = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ResolveTime))
                        
                        subFilter.ResolveTime = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                }
            }
            return filter;
        }
    }
}
