using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MBusinessType
{
    public interface IBusinessTypeValidator : IServiceScoped
    {
        Task<bool> Create(BusinessType BusinessType);
        Task<bool> Update(BusinessType BusinessType);
        Task<bool> Delete(BusinessType BusinessType);
        Task<bool> BulkDelete(List<BusinessType> BusinessTypes);
        Task<bool> Import(List<BusinessType> BusinessTypes);
    }

    public class BusinessTypeValidator : IBusinessTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public BusinessTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(BusinessType BusinessType)
        {
            BusinessTypeFilter BusinessTypeFilter = new BusinessTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = BusinessType.Id },
                Selects = BusinessTypeSelect.Id
            };

            int count = await UOW.BusinessTypeRepository.Count(BusinessTypeFilter);
            if (count == 0)
                BusinessType.AddError(nameof(BusinessTypeValidator), nameof(BusinessType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(BusinessType BusinessType)
        {
            return BusinessType.IsValidated;
        }

        public async Task<bool> Update(BusinessType BusinessType)
        {
            if (await ValidateId(BusinessType))
            {
            }
            return BusinessType.IsValidated;
        }

        public async Task<bool> Delete(BusinessType BusinessType)
        {
            if (await ValidateId(BusinessType))
            {
            }
            return BusinessType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<BusinessType> BusinessTypes)
        {
            foreach (BusinessType BusinessType in BusinessTypes)
            {
                await Delete(BusinessType);
            }
            return BusinessTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<BusinessType> BusinessTypes)
        {
            return true;
        }
    }
}
