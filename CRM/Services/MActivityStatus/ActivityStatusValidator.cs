using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MActivityStatus
{
    public interface IActivityStatusValidator : IServiceScoped
    {
        Task<bool> Create(ActivityStatus ActivityStatus);
        Task<bool> Update(ActivityStatus ActivityStatus);
        Task<bool> Delete(ActivityStatus ActivityStatus);
        Task<bool> BulkDelete(List<ActivityStatus> ActivityStatuses);
        Task<bool> Import(List<ActivityStatus> ActivityStatuses);
    }

    public class ActivityStatusValidator : IActivityStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ActivityStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ActivityStatus ActivityStatus)
        {
            ActivityStatusFilter ActivityStatusFilter = new ActivityStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ActivityStatus.Id },
                Selects = ActivityStatusSelect.Id
            };

            int count = await UOW.ActivityStatusRepository.Count(ActivityStatusFilter);
            if (count == 0)
                ActivityStatus.AddError(nameof(ActivityStatusValidator), nameof(ActivityStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(ActivityStatus ActivityStatus)
        {
            return ActivityStatus.IsValidated;
        }

        public async Task<bool> Update(ActivityStatus ActivityStatus)
        {
            if (await ValidateId(ActivityStatus))
            {
            }
            return ActivityStatus.IsValidated;
        }

        public async Task<bool> Delete(ActivityStatus ActivityStatus)
        {
            if (await ValidateId(ActivityStatus))
            {
            }
            return ActivityStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ActivityStatus> ActivityStatuses)
        {
            foreach (ActivityStatus ActivityStatus in ActivityStatuses)
            {
                await Delete(ActivityStatus);
            }
            return ActivityStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<ActivityStatus> ActivityStatuses)
        {
            return true;
        }
    }
}
