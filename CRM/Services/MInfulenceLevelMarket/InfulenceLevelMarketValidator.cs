using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MInfulenceLevelMarket
{
    public interface IInfulenceLevelMarketValidator : IServiceScoped
    {
        Task<bool> Create(InfulenceLevelMarket InfulenceLevelMarket);
        Task<bool> Update(InfulenceLevelMarket InfulenceLevelMarket);
        Task<bool> Delete(InfulenceLevelMarket InfulenceLevelMarket);
        Task<bool> BulkDelete(List<InfulenceLevelMarket> InfulenceLevelMarkets);
        Task<bool> Import(List<InfulenceLevelMarket> InfulenceLevelMarkets);
    }

    public class InfulenceLevelMarketValidator : IInfulenceLevelMarketValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public InfulenceLevelMarketValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(InfulenceLevelMarket InfulenceLevelMarket)
        {
            InfulenceLevelMarketFilter InfulenceLevelMarketFilter = new InfulenceLevelMarketFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = InfulenceLevelMarket.Id },
                Selects = InfulenceLevelMarketSelect.Id
            };

            int count = await UOW.InfulenceLevelMarketRepository.Count(InfulenceLevelMarketFilter);
            if (count == 0)
                InfulenceLevelMarket.AddError(nameof(InfulenceLevelMarketValidator), nameof(InfulenceLevelMarket.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(InfulenceLevelMarket InfulenceLevelMarket)
        {
            return InfulenceLevelMarket.IsValidated;
        }

        public async Task<bool> Update(InfulenceLevelMarket InfulenceLevelMarket)
        {
            if (await ValidateId(InfulenceLevelMarket))
            {
            }
            return InfulenceLevelMarket.IsValidated;
        }

        public async Task<bool> Delete(InfulenceLevelMarket InfulenceLevelMarket)
        {
            if (await ValidateId(InfulenceLevelMarket))
            {
            }
            return InfulenceLevelMarket.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<InfulenceLevelMarket> InfulenceLevelMarkets)
        {
            foreach (InfulenceLevelMarket InfulenceLevelMarket in InfulenceLevelMarkets)
            {
                await Delete(InfulenceLevelMarket);
            }
            return InfulenceLevelMarkets.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<InfulenceLevelMarket> InfulenceLevelMarkets)
        {
            return true;
        }
    }
}
