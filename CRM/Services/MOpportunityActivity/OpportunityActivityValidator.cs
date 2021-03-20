using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Helpers;

namespace CRM.Services.MOpportunityActivity
{
    public interface IOpportunityActivityValidator : IServiceScoped
    {
        Task<bool> Create(OpportunityActivity OpportunityActivity);
        Task<bool> Update(OpportunityActivity OpportunityActivity);
        Task<bool> Delete(OpportunityActivity OpportunityActivity);
        Task<bool> BulkDelete(List<OpportunityActivity> OpportunityActivities);
        Task<bool> Import(List<OpportunityActivity> OpportunityActivities);
    }

    public class OpportunityActivityValidator : IOpportunityActivityValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            FromDateEmpty,
            ToDateInvalid
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public OpportunityActivityValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(OpportunityActivity OpportunityActivity)
        {
            OpportunityActivityFilter OpportunityActivityFilter = new OpportunityActivityFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = OpportunityActivity.Id },
                Selects = OpportunityActivitySelect.Id
            };

            int count = await UOW.OpportunityActivityRepository.Count(OpportunityActivityFilter);
            if (count == 0)
                OpportunityActivity.AddError(nameof(OpportunityActivityValidator), nameof(OpportunityActivity.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateDate(OpportunityActivity OpportunityActivity)
        {
            if (OpportunityActivity.FromDate == DateTime.MinValue)
            {
                OpportunityActivity.AddError(nameof(OpportunityActivityValidator), nameof(OpportunityActivity.FromDate), ErrorCode.FromDateEmpty);
            }
            else
            {
                if (OpportunityActivity.ToDate.HasValue)
                {
                    if (OpportunityActivity.ToDate.Value <= OpportunityActivity.FromDate)
                    {
                        OpportunityActivity.AddError(nameof(OpportunityActivityValidator), nameof(OpportunityActivity.ToDate), ErrorCode.ToDateInvalid);
                    }
                }
            }
            return OpportunityActivity.IsValidated;
        }

        public async Task<bool>Create(OpportunityActivity OpportunityActivity)
        {
            await ValidateDate(OpportunityActivity);
            return OpportunityActivity.IsValidated;
        }

        public async Task<bool> Update(OpportunityActivity OpportunityActivity)
        {
            if (await ValidateId(OpportunityActivity))
            {
                await ValidateDate(OpportunityActivity);
            }
            return OpportunityActivity.IsValidated;
        }

        public async Task<bool> Delete(OpportunityActivity OpportunityActivity)
        {
            if (await ValidateId(OpportunityActivity))
            {
            }
            return OpportunityActivity.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<OpportunityActivity> OpportunityActivities)
        {
            foreach (OpportunityActivity OpportunityActivity in OpportunityActivities)
            {
                await Delete(OpportunityActivity);
            }
            return OpportunityActivities.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<OpportunityActivity> OpportunityActivities)
        {
            return true;
        }
    }
}
