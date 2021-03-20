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

namespace CRM.Services.MCompanyActivity
{
    public interface ICompanyActivityService :  IServiceScoped
    {
        Task<int> Count(CompanyActivityFilter CompanyActivityFilter);
        Task<List<CompanyActivity>> List(CompanyActivityFilter CompanyActivityFilter);
        Task<CompanyActivity> Get(long Id);
        Task<CompanyActivity> Create(CompanyActivity CompanyActivity);
        Task<CompanyActivity> Update(CompanyActivity CompanyActivity);
        Task<CompanyActivity> Delete(CompanyActivity CompanyActivity);
        Task<List<CompanyActivity>> BulkDelete(List<CompanyActivity> CompanyActivities);
        Task<List<CompanyActivity>> Import(List<CompanyActivity> CompanyActivities);
        Task<CompanyActivityFilter> ToFilter(CompanyActivityFilter CompanyActivityFilter);
    }

    public class CompanyActivityService : BaseService, ICompanyActivityService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICompanyActivityValidator CompanyActivityValidator;

        public CompanyActivityService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICompanyActivityValidator CompanyActivityValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CompanyActivityValidator = CompanyActivityValidator;
        }
        public async Task<int> Count(CompanyActivityFilter CompanyActivityFilter)
        {
            try
            {
                int result = await UOW.CompanyActivityRepository.Count(CompanyActivityFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyActivityService));
            }
            return 0;
        }

        public async Task<List<CompanyActivity>> List(CompanyActivityFilter CompanyActivityFilter)
        {
            try
            {
                List<CompanyActivity> CompanyActivitys = await UOW.CompanyActivityRepository.List(CompanyActivityFilter);
                return CompanyActivitys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyActivityService));
            }
            return null;
        }
        
        public async Task<CompanyActivity> Get(long Id)
        {
            CompanyActivity CompanyActivity = await UOW.CompanyActivityRepository.Get(Id);
            if (CompanyActivity == null)
                return null;
            return CompanyActivity;
        }
        public async Task<CompanyActivity> Create(CompanyActivity CompanyActivity)
        {
            if (!await CompanyActivityValidator.Create(CompanyActivity))
                return CompanyActivity;

            try
            {
                await UOW.CompanyActivityRepository.Create(CompanyActivity);
                CompanyActivity = await UOW.CompanyActivityRepository.Get(CompanyActivity.Id);
                await Logging.CreateAuditLog(CompanyActivity, new { }, nameof(CompanyActivityService));
                return CompanyActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyActivityService));
            }
            return null;
        }

        public async Task<CompanyActivity> Update(CompanyActivity CompanyActivity)
        {
            if (!await CompanyActivityValidator.Update(CompanyActivity))
                return CompanyActivity;
            try
            {
                var oldData = await UOW.CompanyActivityRepository.Get(CompanyActivity.Id);

                await UOW.CompanyActivityRepository.Update(CompanyActivity);

                CompanyActivity = await UOW.CompanyActivityRepository.Get(CompanyActivity.Id);
                await Logging.CreateAuditLog(CompanyActivity, oldData, nameof(CompanyActivityService));
                return CompanyActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyActivityService));
            }
            return null;
        }

        public async Task<CompanyActivity> Delete(CompanyActivity CompanyActivity)
        {
            if (!await CompanyActivityValidator.Delete(CompanyActivity))
                return CompanyActivity;

            try
            {
                await UOW.CompanyActivityRepository.Delete(CompanyActivity);
                await Logging.CreateAuditLog(new { }, CompanyActivity, nameof(CompanyActivityService));
                return CompanyActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyActivityService));
            }
            return null;
        }

        public async Task<List<CompanyActivity>> BulkDelete(List<CompanyActivity> CompanyActivities)
        {
            if (!await CompanyActivityValidator.BulkDelete(CompanyActivities))
                return CompanyActivities;

            try
            {
                await UOW.CompanyActivityRepository.BulkDelete(CompanyActivities);
                await Logging.CreateAuditLog(new { }, CompanyActivities, nameof(CompanyActivityService));
                return CompanyActivities;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyActivityService));
            }
            return null;

        }
        
        public async Task<List<CompanyActivity>> Import(List<CompanyActivity> CompanyActivities)
        {
            if (!await CompanyActivityValidator.Import(CompanyActivities))
                return CompanyActivities;
            try
            {
                await UOW.CompanyActivityRepository.BulkMerge(CompanyActivities);

                await Logging.CreateAuditLog(CompanyActivities, new { }, nameof(CompanyActivityService));
                return CompanyActivities;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyActivityService));
            }
            return null;
        }     
        
        public async Task<CompanyActivityFilter> ToFilter(CompanyActivityFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CompanyActivityFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CompanyActivityFilter subFilter = new CompanyActivityFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CompanyId))
                        subFilter.CompanyId = FilterBuilder.Merge(subFilter.CompanyId, FilterPermissionDefinition.IdFilter);
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
