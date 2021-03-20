using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MMarketPrice
{
    public interface IMarketPriceValidator : IServiceScoped
    {
        Task<bool> Create(MarketPrice MarketPrice);
        Task<bool> Update(MarketPrice MarketPrice);
        Task<bool> Delete(MarketPrice MarketPrice);
        Task<bool> BulkDelete(List<MarketPrice> MarketPrices);
        Task<bool> Import(List<MarketPrice> MarketPrices);
    }

    public class MarketPriceValidator : IMarketPriceValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public MarketPriceValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(MarketPrice MarketPrice)
        {
            MarketPriceFilter MarketPriceFilter = new MarketPriceFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = MarketPrice.Id },
                Selects = MarketPriceSelect.Id
            };

            int count = await UOW.MarketPriceRepository.Count(MarketPriceFilter);
            if (count == 0)
                MarketPrice.AddError(nameof(MarketPriceValidator), nameof(MarketPrice.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(MarketPrice MarketPrice)
        {
            return MarketPrice.IsValidated;
        }

        public async Task<bool> Update(MarketPrice MarketPrice)
        {
            if (await ValidateId(MarketPrice))
            {
            }
            return MarketPrice.IsValidated;
        }

        public async Task<bool> Delete(MarketPrice MarketPrice)
        {
            if (await ValidateId(MarketPrice))
            {
            }
            return MarketPrice.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<MarketPrice> MarketPrices)
        {
            foreach (MarketPrice MarketPrice in MarketPrices)
            {
                await Delete(MarketPrice);
            }
            return MarketPrices.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<MarketPrice> MarketPrices)
        {
            return true;
        }
    }
}
