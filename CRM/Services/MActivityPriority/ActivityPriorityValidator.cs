using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MActivityPriority
{
    public interface IActivityPriorityValidator : IServiceScoped
    {
        Task<bool> Create(ActivityPriority ActivityPriority);
        Task<bool> Update(ActivityPriority ActivityPriority);
        Task<bool> Delete(ActivityPriority ActivityPriority);
        Task<bool> BulkDelete(List<ActivityPriority> ActivityPriorities);
        Task<bool> Import(List<ActivityPriority> ActivityPriorities);
    }

    public class ActivityPriorityValidator : IActivityPriorityValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ActivityPriorityValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ActivityPriority ActivityPriority)
        {
            ActivityPriorityFilter ActivityPriorityFilter = new ActivityPriorityFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ActivityPriority.Id },
                Selects = ActivityPrioritySelect.Id
            };

            int count = await UOW.ActivityPriorityRepository.Count(ActivityPriorityFilter);
            if (count == 0)
                ActivityPriority.AddError(nameof(ActivityPriorityValidator), nameof(ActivityPriority.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(ActivityPriority ActivityPriority)
        {
            return ActivityPriority.IsValidated;
        }

        public async Task<bool> Update(ActivityPriority ActivityPriority)
        {
            if (await ValidateId(ActivityPriority))
            {
            }
            return ActivityPriority.IsValidated;
        }

        public async Task<bool> Delete(ActivityPriority ActivityPriority)
        {
            if (await ValidateId(ActivityPriority))
            {
            }
            return ActivityPriority.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ActivityPriority> ActivityPriorities)
        {
            foreach (ActivityPriority ActivityPriority in ActivityPriorities)
            {
                await Delete(ActivityPriority);
            }
            return ActivityPriorities.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<ActivityPriority> ActivityPriorities)
        {
            return true;
        }
    }
}
