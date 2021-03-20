using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerLeadLevel
{
    public interface ICustomerLeadLevelValidator : IServiceScoped
    {
        Task<bool> Create(CustomerLeadLevel CustomerLeadLevel);
        Task<bool> Update(CustomerLeadLevel CustomerLeadLevel);
        Task<bool> Delete(CustomerLeadLevel CustomerLeadLevel);
        Task<bool> BulkDelete(List<CustomerLeadLevel> CustomerLeadLevels);
        Task<bool> Import(List<CustomerLeadLevel> CustomerLeadLevels);
    }

    public class CustomerLeadLevelValidator : ICustomerLeadLevelValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerLeadLevelValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerLeadLevel CustomerLeadLevel)
        {
            CustomerLeadLevelFilter CustomerLeadLevelFilter = new CustomerLeadLevelFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerLeadLevel.Id },
                Selects = CustomerLeadLevelSelect.Id
            };

            int count = await UOW.CustomerLeadLevelRepository.Count(CustomerLeadLevelFilter);
            if (count == 0)
                CustomerLeadLevel.AddError(nameof(CustomerLeadLevelValidator), nameof(CustomerLeadLevel.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerLeadLevel CustomerLeadLevel)
        {
            return CustomerLeadLevel.IsValidated;
        }

        public async Task<bool> Update(CustomerLeadLevel CustomerLeadLevel)
        {
            if (await ValidateId(CustomerLeadLevel))
            {
            }
            return CustomerLeadLevel.IsValidated;
        }

        public async Task<bool> Delete(CustomerLeadLevel CustomerLeadLevel)
        {
            if (await ValidateId(CustomerLeadLevel))
            {
            }
            return CustomerLeadLevel.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerLeadLevel> CustomerLeadLevels)
        {
            foreach (CustomerLeadLevel CustomerLeadLevel in CustomerLeadLevels)
            {
                await Delete(CustomerLeadLevel);
            }
            return CustomerLeadLevels.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerLeadLevel> CustomerLeadLevels)
        {
            return true;
        }
    }
}
