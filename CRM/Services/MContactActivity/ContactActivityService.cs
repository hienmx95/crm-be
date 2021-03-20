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

namespace CRM.Services.MContactActivity
{
    public interface IContactActivityService :  IServiceScoped
    {
        Task<int> Count(ContactActivityFilter ContactActivityFilter);
        Task<List<ContactActivity>> List(ContactActivityFilter ContactActivityFilter);
        Task<ContactActivity> Get(long Id);
        Task<ContactActivity> Create(ContactActivity ContactActivity);
        Task<ContactActivity> Update(ContactActivity ContactActivity);
        Task<ContactActivity> Delete(ContactActivity ContactActivity);
        Task<List<ContactActivity>> BulkDelete(List<ContactActivity> ContactActivities);
        Task<List<ContactActivity>> Import(List<ContactActivity> ContactActivities);
        Task<ContactActivityFilter> ToFilter(ContactActivityFilter ContactActivityFilter);
    }

    public class ContactActivityService : BaseService, IContactActivityService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IContactActivityValidator ContactActivityValidator;

        public ContactActivityService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            IContactActivityValidator ContactActivityValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ContactActivityValidator = ContactActivityValidator;
        }
        public async Task<int> Count(ContactActivityFilter ContactActivityFilter)
        {
            try
            {
                int result = await UOW.ContactActivityRepository.Count(ContactActivityFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactActivityService));
            }
            return 0;
        }

        public async Task<List<ContactActivity>> List(ContactActivityFilter ContactActivityFilter)
        {
            try
            {
                List<ContactActivity> ContactActivitys = await UOW.ContactActivityRepository.List(ContactActivityFilter);
                return ContactActivitys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactActivityService));
            }
            return null;
        }
        
        public async Task<ContactActivity> Get(long Id)
        {
            ContactActivity ContactActivity = await UOW.ContactActivityRepository.Get(Id);
            if (ContactActivity == null)
                return null;
            return ContactActivity;
        }
        public async Task<ContactActivity> Create(ContactActivity ContactActivity)
        {
            if (!await ContactActivityValidator.Create(ContactActivity))
                return ContactActivity;

            try
            {
                await UOW.ContactActivityRepository.Create(ContactActivity);
                ContactActivity = await UOW.ContactActivityRepository.Get(ContactActivity.Id);
                await Logging.CreateAuditLog(ContactActivity, new { }, nameof(ContactActivityService));
                return ContactActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactActivityService));
            }
            return null;
        }

        public async Task<ContactActivity> Update(ContactActivity ContactActivity)
        {
            if (!await ContactActivityValidator.Update(ContactActivity))
                return ContactActivity;
            try
            {
                var oldData = await UOW.ContactActivityRepository.Get(ContactActivity.Id);

                await UOW.ContactActivityRepository.Update(ContactActivity);

                ContactActivity = await UOW.ContactActivityRepository.Get(ContactActivity.Id);
                await Logging.CreateAuditLog(ContactActivity, oldData, nameof(ContactActivityService));
                return ContactActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactActivityService));
            }
            return null;
        }

        public async Task<ContactActivity> Delete(ContactActivity ContactActivity)
        {
            if (!await ContactActivityValidator.Delete(ContactActivity))
                return ContactActivity;

            try
            {
                await UOW.ContactActivityRepository.Delete(ContactActivity);
                await Logging.CreateAuditLog(new { }, ContactActivity, nameof(ContactActivityService));
                return ContactActivity;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactActivityService));
            }
            return null;
        }

        public async Task<List<ContactActivity>> BulkDelete(List<ContactActivity> ContactActivities)
        {
            if (!await ContactActivityValidator.BulkDelete(ContactActivities))
                return ContactActivities;

            try
            {
                await UOW.ContactActivityRepository.BulkDelete(ContactActivities);
                await Logging.CreateAuditLog(new { }, ContactActivities, nameof(ContactActivityService));
                return ContactActivities;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactActivityService));
            }
            return null;

        }
        
        public async Task<List<ContactActivity>> Import(List<ContactActivity> ContactActivities)
        {
            if (!await ContactActivityValidator.Import(ContactActivities))
                return ContactActivities;
            try
            {
                await UOW.ContactActivityRepository.BulkMerge(ContactActivities);

                await Logging.CreateAuditLog(ContactActivities, new { }, nameof(ContactActivityService));
                return ContactActivities;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactActivityService));
            }
            return null;
        }     
        
        public async Task<ContactActivityFilter> ToFilter(ContactActivityFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ContactActivityFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ContactActivityFilter subFilter = new ContactActivityFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ContactId))
                        subFilter.ContactId = FilterBuilder.Merge(subFilter.ContactId, FilterPermissionDefinition.IdFilter);
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
