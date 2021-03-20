using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCurrency
{
    public interface ICurrencyValidator : IServiceScoped
    {
        Task<bool> Create(Currency Currency);
        Task<bool> Update(Currency Currency);
        Task<bool> Delete(Currency Currency);
        Task<bool> BulkDelete(List<Currency> Currencies);
        Task<bool> Import(List<Currency> Currencies);
    }

    public class CurrencyValidator : ICurrencyValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CurrencyValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Currency Currency)
        {
            CurrencyFilter CurrencyFilter = new CurrencyFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Currency.Id },
                Selects = CurrencySelect.Id
            };

            int count = await UOW.CurrencyRepository.Count(CurrencyFilter);
            if (count == 0)
                Currency.AddError(nameof(CurrencyValidator), nameof(Currency.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(Currency Currency)
        {
            return Currency.IsValidated;
        }

        public async Task<bool> Update(Currency Currency)
        {
            if (await ValidateId(Currency))
            {
            }
            return Currency.IsValidated;
        }

        public async Task<bool> Delete(Currency Currency)
        {
            if (await ValidateId(Currency))
            {
            }
            return Currency.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Currency> Currencies)
        {
            foreach (Currency Currency in Currencies)
            {
                await Delete(Currency);
            }
            return Currencies.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Currency> Currencies)
        {
            return true;
        }
    }
}
