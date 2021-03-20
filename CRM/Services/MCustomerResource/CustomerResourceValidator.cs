using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerResource
{
    public interface ICustomerResourceValidator : IServiceScoped
    {
        Task<bool> Create(CustomerResource CustomerResource);
        Task<bool> Update(CustomerResource CustomerResource);
        Task<bool> Delete(CustomerResource CustomerResource);
        Task<bool> BulkDelete(List<CustomerResource> CustomerResources);
        Task<bool> Import(List<CustomerResource> CustomerResources);
    }

    public class CustomerResourceValidator : ICustomerResourceValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerResourceValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerResource CustomerResource)
        {
            CustomerResourceFilter CustomerResourceFilter = new CustomerResourceFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerResource.Id },
                Selects = CustomerResourceSelect.Id
            };

            int count = await UOW.CustomerResourceRepository.Count(CustomerResourceFilter);
            if (count == 0)
                CustomerResource.AddError(nameof(CustomerResourceValidator), nameof(CustomerResource.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerResource CustomerResource)
        {
            return CustomerResource.IsValidated;
        }

        public async Task<bool> Update(CustomerResource CustomerResource)
        {
            if (await ValidateId(CustomerResource))
            {
            }
            return CustomerResource.IsValidated;
        }

        public async Task<bool> Delete(CustomerResource CustomerResource)
        {
            if (await ValidateId(CustomerResource))
            {
            }
            return CustomerResource.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerResource> CustomerResources)
        {
            foreach (CustomerResource CustomerResource in CustomerResources)
            {
                await Delete(CustomerResource);
            }
            return CustomerResources.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerResource> CustomerResources)
        {
            return true;
        }
    }
}
