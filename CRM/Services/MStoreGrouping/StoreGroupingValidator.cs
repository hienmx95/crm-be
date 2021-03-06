using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MStoreGrouping
{
    public interface IStoreGroupingValidator : IServiceScoped
    {
        Task<bool> Create(StoreGrouping StoreGrouping);
        Task<bool> Update(StoreGrouping StoreGrouping);
        Task<bool> Delete(StoreGrouping StoreGrouping);
        Task<bool> BulkDelete(List<StoreGrouping> StoreGroupings);
        Task<bool> Import(List<StoreGrouping> StoreGroupings);
    }

    public class StoreGroupingValidator : IStoreGroupingValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public StoreGroupingValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(StoreGrouping StoreGrouping)
        {
            StoreGroupingFilter StoreGroupingFilter = new StoreGroupingFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = StoreGrouping.Id },
                Selects = StoreGroupingSelect.Id
            };

            int count = await UOW.StoreGroupingRepository.Count(StoreGroupingFilter);
            if (count == 0)
                StoreGrouping.AddError(nameof(StoreGroupingValidator), nameof(StoreGrouping.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(StoreGrouping StoreGrouping)
        {
            return StoreGrouping.IsValidated;
        }

        public async Task<bool> Update(StoreGrouping StoreGrouping)
        {
            if (await ValidateId(StoreGrouping))
            {
            }
            return StoreGrouping.IsValidated;
        }

        public async Task<bool> Delete(StoreGrouping StoreGrouping)
        {
            if (await ValidateId(StoreGrouping))
            {
            }
            return StoreGrouping.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<StoreGrouping> StoreGroupings)
        {
            foreach (StoreGrouping StoreGrouping in StoreGroupings)
            {
                await Delete(StoreGrouping);
            }
            return StoreGroupings.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<StoreGrouping> StoreGroupings)
        {
            return true;
        }
    }
}
