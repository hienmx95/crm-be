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
using CRM.Enums;

namespace CRM.Services.MOpportunityActivity
{
    public interface IOpportunityActivityService :  IServiceScoped
    {
        Task<int> Count(OpportunityActivityFilter OpportunityActivityFilter);
        Task<List<OpportunityActivity>> List(OpportunityActivityFilter OpportunityActivityFilter);
        Task<OpportunityActivity> Get(long Id);
        Task<OpportunityActivity> Create(OpportunityActivity OpportunityActivity);
        Task<OpportunityActivity> Update(OpportunityActivity OpportunityActivity);
        Task<OpportunityActivity> Delete(OpportunityActivity OpportunityActivity);
        Task<List<OpportunityActivity>> BulkDelete(List<OpportunityActivity> OpportunityActivities);
        Task<List<OpportunityActivity>> Import(List<OpportunityActivity> OpportunityActivities);
        Task<OpportunityActivityFilter> ToFilter(OpportunityActivityFilter OpportunityActivityFilter);
    }

    public class OpportunityActivityService : BaseService, IOpportunityActivityService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IOpportunityActivityValidator OpportunityActivityValidator;

        public OpportunityActivityService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            IOpportunityActivityValidator OpportunityActivityValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.OpportunityActivityValidator = OpportunityActivityValidator;
        }
        public async Task<int> Count(OpportunityActivityFilter OpportunityActivityFilter)
        {
            try
            {
                int result = await UOW.OpportunityActivityRepository.Count(OpportunityActivityFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityActivityService));
            }
            return 0;
        }

        public async Task<List<OpportunityActivity>> List(OpportunityActivityFilter OpportunityActivityFilter)
        {
            try
            {
                List<OpportunityActivity> OpportunityActivitys = await UOW.OpportunityActivityRepository.List(OpportunityActivityFilter);
                return OpportunityActivitys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityActivityService));
            }
            return null;
        }
        
        public async Task<OpportunityActivity> Get(long Id)
        {
            OpportunityActivity OpportunityActivity = await UOW.OpportunityActivityRepository.Get(Id);
            if (OpportunityActivity == null)
                return null;
            return OpportunityActivity;
        }
        public async Task<OpportunityActivity> Create(OpportunityActivity OpportunityActivity)
        {
            if (!await OpportunityActivityValidator.Create(OpportunityActivity))
                return OpportunityActivity;

            try
            {
                await UOW.OpportunityActivityRepository.Create(OpportunityActivity);
                OpportunityActivity = await UOW.OpportunityActivityRepository.Get(OpportunityActivity.Id);
                await Logging.CreateAuditLog(OpportunityActivity, new { }, nameof(OpportunityActivityService));
                return OpportunityActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityActivityService));
            }
            return null;
        }

        public async Task<OpportunityActivity> Update(OpportunityActivity OpportunityActivity)
        {
            if (!await OpportunityActivityValidator.Update(OpportunityActivity))
                return OpportunityActivity;
            try
            {
                var oldData = await UOW.OpportunityActivityRepository.Get(OpportunityActivity.Id);

                await UOW.OpportunityActivityRepository.Update(OpportunityActivity);

                OpportunityActivity = await UOW.OpportunityActivityRepository.Get(OpportunityActivity.Id);
                await Logging.CreateAuditLog(OpportunityActivity, oldData, nameof(OpportunityActivityService));
                return OpportunityActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityActivityService));
            }
            return null;
        }

        public async Task<OpportunityActivity> Delete(OpportunityActivity OpportunityActivity)
        {
            if (!await OpportunityActivityValidator.Delete(OpportunityActivity))
                return OpportunityActivity;

            try
            {
                await UOW.OpportunityActivityRepository.Delete(OpportunityActivity);
                await Logging.CreateAuditLog(new { }, OpportunityActivity, nameof(OpportunityActivityService));
                return OpportunityActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityActivityService));
            }
            return null;
        }

        public async Task<List<OpportunityActivity>> BulkDelete(List<OpportunityActivity> OpportunityActivities)
        {
            if (!await OpportunityActivityValidator.BulkDelete(OpportunityActivities))
                return OpportunityActivities;

            try
            {
                await UOW.OpportunityActivityRepository.BulkDelete(OpportunityActivities);
                await Logging.CreateAuditLog(new { }, OpportunityActivities, nameof(OpportunityActivityService));
                return OpportunityActivities;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityActivityService));
            }
            return null;

        }
        
        public async Task<List<OpportunityActivity>> Import(List<OpportunityActivity> OpportunityActivities)
        {
            if (!await OpportunityActivityValidator.Import(OpportunityActivities))
                return OpportunityActivities;
            try
            {
                await UOW.OpportunityActivityRepository.BulkMerge(OpportunityActivities);

                await Logging.CreateAuditLog(OpportunityActivities, new { }, nameof(OpportunityActivityService));
                return OpportunityActivities;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityActivityService));
            }
            return null;
        }     
        
        public async Task<OpportunityActivityFilter> ToFilter(OpportunityActivityFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<OpportunityActivityFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                OpportunityActivityFilter subFilter = new OpportunityActivityFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Title))
                        subFilter.Title = FilterBuilder.Merge(subFilter.Title, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.FromDate))
                        subFilter.FromDate = FilterBuilder.Merge(subFilter.FromDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ToDate))
                        subFilter.ToDate = FilterBuilder.Merge(subFilter.ToDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ActivityTypeId))
                        subFilter.ActivityTypeId = FilterBuilder.Merge(subFilter.ActivityTypeId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ActivityPriorityId))
                        subFilter.ActivityPriorityId = FilterBuilder.Merge(subFilter.ActivityPriorityId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        subFilter.Description = FilterBuilder.Merge(subFilter.Description, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Address))
                        subFilter.Address = FilterBuilder.Merge(subFilter.Address, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OpportunityId))
                        subFilter.OpportunityId = FilterBuilder.Merge(subFilter.OpportunityId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ActivityStatusId))
                        subFilter.ActivityStatusId = FilterBuilder.Merge(subFilter.ActivityStatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                        }
                    }
                }
            }
            return filter;
        }
    }
}
