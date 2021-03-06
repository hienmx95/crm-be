using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MTaxType
{
    public interface ITaxTypeValidator : IServiceScoped
    {
        Task<bool> Create(TaxType TaxType);
        Task<bool> Update(TaxType TaxType);
        Task<bool> Delete(TaxType TaxType);
        Task<bool> BulkDelete(List<TaxType> TaxTypes);
        Task<bool> Import(List<TaxType> TaxTypes);
    }

    public class TaxTypeValidator : ITaxTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TaxTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TaxType TaxType)
        {
            TaxTypeFilter TaxTypeFilter = new TaxTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TaxType.Id },
                Selects = TaxTypeSelect.Id
            };

            int count = await UOW.TaxTypeRepository.Count(TaxTypeFilter);
            if (count == 0)
                TaxType.AddError(nameof(TaxTypeValidator), nameof(TaxType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(TaxType TaxType)
        {
            return TaxType.IsValidated;
        }

        public async Task<bool> Update(TaxType TaxType)
        {
            if (await ValidateId(TaxType))
            {
            }
            return TaxType.IsValidated;
        }

        public async Task<bool> Delete(TaxType TaxType)
        {
            if (await ValidateId(TaxType))
            {
            }
            return TaxType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TaxType> TaxTypes)
        {
            foreach (TaxType TaxType in TaxTypes)
            {
                await Delete(TaxType);
            }
            return TaxTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<TaxType> TaxTypes)
        {
            return true;
        }
    }
}
