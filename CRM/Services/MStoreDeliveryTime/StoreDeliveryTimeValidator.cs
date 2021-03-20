using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MStoreDeliveryTime
{
    public interface IStoreDeliveryTimeValidator : IServiceScoped
    {
        Task<bool> Create(StoreDeliveryTime StoreDeliveryTime);
        Task<bool> Update(StoreDeliveryTime StoreDeliveryTime);
        Task<bool> Delete(StoreDeliveryTime StoreDeliveryTime);
        Task<bool> BulkDelete(List<StoreDeliveryTime> StoreDeliveryTimes);
        Task<bool> Import(List<StoreDeliveryTime> StoreDeliveryTimes);
    }

    public class StoreDeliveryTimeValidator : IStoreDeliveryTimeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public StoreDeliveryTimeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(StoreDeliveryTime StoreDeliveryTime)
        {
            StoreDeliveryTimeFilter StoreDeliveryTimeFilter = new StoreDeliveryTimeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = StoreDeliveryTime.Id },
                Selects = StoreDeliveryTimeSelect.Id
            };

            int count = await UOW.StoreDeliveryTimeRepository.Count(StoreDeliveryTimeFilter);
            if (count == 0)
                StoreDeliveryTime.AddError(nameof(StoreDeliveryTimeValidator), nameof(StoreDeliveryTime.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(StoreDeliveryTime StoreDeliveryTime)
        {
            return StoreDeliveryTime.IsValidated;
        }

        public async Task<bool> Update(StoreDeliveryTime StoreDeliveryTime)
        {
            if (await ValidateId(StoreDeliveryTime))
            {
            }
            return StoreDeliveryTime.IsValidated;
        }

        public async Task<bool> Delete(StoreDeliveryTime StoreDeliveryTime)
        {
            if (await ValidateId(StoreDeliveryTime))
            {
            }
            return StoreDeliveryTime.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<StoreDeliveryTime> StoreDeliveryTimes)
        {
            foreach (StoreDeliveryTime StoreDeliveryTime in StoreDeliveryTimes)
            {
                await Delete(StoreDeliveryTime);
            }
            return StoreDeliveryTimes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<StoreDeliveryTime> StoreDeliveryTimes)
        {
            return true;
        }
    }
}
