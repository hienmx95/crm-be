using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerLeadSource
{
    public interface ICustomerLeadSourceValidator : IServiceScoped
    {
        Task<bool> Create(CustomerLeadSource CustomerLeadSource);
        Task<bool> Update(CustomerLeadSource CustomerLeadSource);
        Task<bool> Delete(CustomerLeadSource CustomerLeadSource);
        Task<bool> BulkDelete(List<CustomerLeadSource> CustomerLeadSources);
        Task<bool> Import(List<CustomerLeadSource> CustomerLeadSources);
    }

    public class CustomerLeadSourceValidator : ICustomerLeadSourceValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerLeadSourceValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerLeadSource CustomerLeadSource)
        {
            CustomerLeadSourceFilter CustomerLeadSourceFilter = new CustomerLeadSourceFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerLeadSource.Id },
                Selects = CustomerLeadSourceSelect.Id
            };

            int count = await UOW.CustomerLeadSourceRepository.Count(CustomerLeadSourceFilter);
            if (count == 0)
                CustomerLeadSource.AddError(nameof(CustomerLeadSourceValidator), nameof(CustomerLeadSource.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerLeadSource CustomerLeadSource)
        {
            return CustomerLeadSource.IsValidated;
        }

        public async Task<bool> Update(CustomerLeadSource CustomerLeadSource)
        {
            if (await ValidateId(CustomerLeadSource))
            {
            }
            return CustomerLeadSource.IsValidated;
        }

        public async Task<bool> Delete(CustomerLeadSource CustomerLeadSource)
        {
            if (await ValidateId(CustomerLeadSource))
            {
            }
            return CustomerLeadSource.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerLeadSource> CustomerLeadSources)
        {
            foreach (CustomerLeadSource CustomerLeadSource in CustomerLeadSources)
            {
                await Delete(CustomerLeadSource);
            }
            return CustomerLeadSources.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerLeadSource> CustomerLeadSources)
        {
            return true;
        }
    }
}
