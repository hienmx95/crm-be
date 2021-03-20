using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerLevel
{
    public interface ICustomerLevelValidator : IServiceScoped
    {
        Task<bool> Create(CustomerLevel CustomerLevel);
        Task<bool> Update(CustomerLevel CustomerLevel);
        Task<bool> Delete(CustomerLevel CustomerLevel);
        Task<bool> BulkDelete(List<CustomerLevel> CustomerLevels);
        Task<bool> Import(List<CustomerLevel> CustomerLevels);
    }

    public class CustomerLevelValidator : ICustomerLevelValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerLevelValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerLevel CustomerLevel)
        {
            CustomerLevelFilter CustomerLevelFilter = new CustomerLevelFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerLevel.Id },
                Selects = CustomerLevelSelect.Id
            };

            int count = await UOW.CustomerLevelRepository.Count(CustomerLevelFilter);
            if (count == 0)
                CustomerLevel.AddError(nameof(CustomerLevelValidator), nameof(CustomerLevel.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerLevel CustomerLevel)
        {
            return CustomerLevel.IsValidated;
        }

        public async Task<bool> Update(CustomerLevel CustomerLevel)
        {
            if (await ValidateId(CustomerLevel))
            {
            }
            return CustomerLevel.IsValidated;
        }

        public async Task<bool> Delete(CustomerLevel CustomerLevel)
        {
            if (await ValidateId(CustomerLevel))
            {
            }
            return CustomerLevel.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerLevel> CustomerLevels)
        {
            foreach (CustomerLevel CustomerLevel in CustomerLevels)
            {
                await Delete(CustomerLevel);
            }
            return CustomerLevels.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerLevel> CustomerLevels)
        {
            return true;
        }
    }
}
