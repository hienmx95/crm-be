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

namespace CRM.Services.MCustomerLeadActivity
{
    public interface ICustomerLeadActivityService :  IServiceScoped
    {
        Task<int> Count(CustomerLeadActivityFilter CustomerLeadActivityFilter);
        Task<List<CustomerLeadActivity>> List(CustomerLeadActivityFilter CustomerLeadActivityFilter);
        Task<CustomerLeadActivity> Get(long Id);
        Task<CustomerLeadActivity> Create(CustomerLeadActivity CustomerLeadActivity);
        Task<CustomerLeadActivity> Update(CustomerLeadActivity CustomerLeadActivity);
        Task<CustomerLeadActivity> Delete(CustomerLeadActivity CustomerLeadActivity);
        Task<List<CustomerLeadActivity>> BulkDelete(List<CustomerLeadActivity> CustomerLeadActivities);
        Task<List<CustomerLeadActivity>> Import(List<CustomerLeadActivity> CustomerLeadActivities);
        Task<CustomerLeadActivityFilter> ToFilter(CustomerLeadActivityFilter CustomerLeadActivityFilter);
    }

    public class CustomerLeadActivityService : BaseService, ICustomerLeadActivityService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerLeadActivityValidator CustomerLeadActivityValidator;

        public CustomerLeadActivityService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerLeadActivityValidator CustomerLeadActivityValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerLeadActivityValidator = CustomerLeadActivityValidator;
        }
        public async Task<int> Count(CustomerLeadActivityFilter CustomerLeadActivityFilter)
        {
            try
            {
                int result = await UOW.CustomerLeadActivityRepository.Count(CustomerLeadActivityFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadActivityService));
            }
            return 0;
        }

        public async Task<List<CustomerLeadActivity>> List(CustomerLeadActivityFilter CustomerLeadActivityFilter)
        {
            try
            {
                List<CustomerLeadActivity> CustomerLeadActivitys = await UOW.CustomerLeadActivityRepository.List(CustomerLeadActivityFilter);
                return CustomerLeadActivitys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadActivityService));
            }
            return null;
        }
        
        public async Task<CustomerLeadActivity> Get(long Id)
        {
            CustomerLeadActivity CustomerLeadActivity = await UOW.CustomerLeadActivityRepository.Get(Id);
            if (CustomerLeadActivity == null)
                return null;
            return CustomerLeadActivity;
        }
        public async Task<CustomerLeadActivity> Create(CustomerLeadActivity CustomerLeadActivity)
        {
            if (!await CustomerLeadActivityValidator.Create(CustomerLeadActivity))
                return CustomerLeadActivity;

            try
            {
                await UOW.CustomerLeadActivityRepository.Create(CustomerLeadActivity);
                CustomerLeadActivity = await UOW.CustomerLeadActivityRepository.Get(CustomerLeadActivity.Id);
                await Logging.CreateAuditLog(CustomerLeadActivity, new { }, nameof(CustomerLeadActivityService));
                return CustomerLeadActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadActivityService));
            }
            return null;
        }

        public async Task<CustomerLeadActivity> Update(CustomerLeadActivity CustomerLeadActivity)
        {
            if (!await CustomerLeadActivityValidator.Update(CustomerLeadActivity))
                return CustomerLeadActivity;
            try
            {
                var oldData = await UOW.CustomerLeadActivityRepository.Get(CustomerLeadActivity.Id);

                await UOW.CustomerLeadActivityRepository.Update(CustomerLeadActivity);

                CustomerLeadActivity = await UOW.CustomerLeadActivityRepository.Get(CustomerLeadActivity.Id);
                await Logging.CreateAuditLog(CustomerLeadActivity, oldData, nameof(CustomerLeadActivityService));
                return CustomerLeadActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadActivityService));
            }
            return null;
        }

        public async Task<CustomerLeadActivity> Delete(CustomerLeadActivity CustomerLeadActivity)
        {
            if (!await CustomerLeadActivityValidator.Delete(CustomerLeadActivity))
                return CustomerLeadActivity;

            try
            {
                await UOW.CustomerLeadActivityRepository.Delete(CustomerLeadActivity);
                await Logging.CreateAuditLog(new { }, CustomerLeadActivity, nameof(CustomerLeadActivityService));
                return CustomerLeadActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadActivityService));
            }
            return null;
        }

        public async Task<List<CustomerLeadActivity>> BulkDelete(List<CustomerLeadActivity> CustomerLeadActivities)
        {
            if (!await CustomerLeadActivityValidator.BulkDelete(CustomerLeadActivities))
                return CustomerLeadActivities;

            try
            {
                await UOW.CustomerLeadActivityRepository.BulkDelete(CustomerLeadActivities);
                await Logging.CreateAuditLog(new { }, CustomerLeadActivities, nameof(CustomerLeadActivityService));
                return CustomerLeadActivities;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadActivityService));
            }
            return null;

        }
        
        public async Task<List<CustomerLeadActivity>> Import(List<CustomerLeadActivity> CustomerLeadActivities)
        {
            if (!await CustomerLeadActivityValidator.Import(CustomerLeadActivities))
                return CustomerLeadActivities;
            try
            {
                await UOW.CustomerLeadActivityRepository.BulkMerge(CustomerLeadActivities);

                await Logging.CreateAuditLog(CustomerLeadActivities, new { }, nameof(CustomerLeadActivityService));
                return CustomerLeadActivities;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadActivityService));
            }
            return null;
        }     
        
        public async Task<CustomerLeadActivityFilter> ToFilter(CustomerLeadActivityFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerLeadActivityFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerLeadActivityFilter subFilter = new CustomerLeadActivityFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerLeadId))
                        subFilter.CustomerLeadId = FilterBuilder.Merge(subFilter.CustomerLeadId, FilterPermissionDefinition.IdFilter);
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
