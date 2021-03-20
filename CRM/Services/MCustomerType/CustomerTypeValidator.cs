using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerType
{
    public interface ICustomerTypeValidator : IServiceScoped
    {
        Task<bool> Create(CustomerType CustomerType);
        Task<bool> Update(CustomerType CustomerType);
        Task<bool> Delete(CustomerType CustomerType);
        Task<bool> BulkDelete(List<CustomerType> CustomerTypes);
        Task<bool> Import(List<CustomerType> CustomerTypes);
    }

    public class CustomerTypeValidator : ICustomerTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerType CustomerType)
        {
            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerType.Id },
                Selects = CustomerTypeSelect.Id
            };

            int count = await UOW.CustomerTypeRepository.Count(CustomerTypeFilter);
            if (count == 0)
                CustomerType.AddError(nameof(CustomerTypeValidator), nameof(CustomerType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerType CustomerType)
        {
            return CustomerType.IsValidated;
        }

        public async Task<bool> Update(CustomerType CustomerType)
        {
            if (await ValidateId(CustomerType))
            {
            }
            return CustomerType.IsValidated;
        }

        public async Task<bool> Delete(CustomerType CustomerType)
        {
            if (await ValidateId(CustomerType))
            {
            }
            return CustomerType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerType> CustomerTypes)
        {
            foreach (CustomerType CustomerType in CustomerTypes)
            {
                await Delete(CustomerType);
            }
            return CustomerTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerType> CustomerTypes)
        {
            return true;
        }
    }
}
