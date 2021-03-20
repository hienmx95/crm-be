using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MActivityType
{
    public interface IActivityTypeValidator : IServiceScoped
    {
        Task<bool> Create(ActivityType ActivityType);
        Task<bool> Update(ActivityType ActivityType);
        Task<bool> Delete(ActivityType ActivityType);
        Task<bool> BulkDelete(List<ActivityType> ActivityTypes);
        Task<bool> Import(List<ActivityType> ActivityTypes);
    }

    public class ActivityTypeValidator : IActivityTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ActivityTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ActivityType ActivityType)
        {
            ActivityTypeFilter ActivityTypeFilter = new ActivityTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ActivityType.Id },
                Selects = ActivityTypeSelect.Id
            };

            int count = await UOW.ActivityTypeRepository.Count(ActivityTypeFilter);
            if (count == 0)
                ActivityType.AddError(nameof(ActivityTypeValidator), nameof(ActivityType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(ActivityType ActivityType)
        {
            return ActivityType.IsValidated;
        }

        public async Task<bool> Update(ActivityType ActivityType)
        {
            if (await ValidateId(ActivityType))
            {
            }
            return ActivityType.IsValidated;
        }

        public async Task<bool> Delete(ActivityType ActivityType)
        {
            if (await ValidateId(ActivityType))
            {
            }
            return ActivityType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ActivityType> ActivityTypes)
        {
            foreach (ActivityType ActivityType in ActivityTypes)
            {
                await Delete(ActivityType);
            }
            return ActivityTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<ActivityType> ActivityTypes)
        {
            return true;
        }
    }
}
