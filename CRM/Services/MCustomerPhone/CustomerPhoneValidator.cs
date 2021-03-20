using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerPhone
{
    public interface ICustomerPhoneValidator : IServiceScoped
    {
        Task<bool> Create(CustomerPhone CustomerPhone);
        Task<bool> Update(CustomerPhone CustomerPhone);
        Task<bool> Delete(CustomerPhone CustomerPhone);
        Task<bool> BulkDelete(List<CustomerPhone> CustomerPhones);
        Task<bool> Import(List<CustomerPhone> CustomerPhones);
    }

    public class CustomerPhoneValidator : ICustomerPhoneValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerPhoneValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerPhone CustomerPhone)
        {
            CustomerPhoneFilter CustomerPhoneFilter = new CustomerPhoneFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerPhone.Id },
                Selects = CustomerPhoneSelect.Id
            };

            int count = await UOW.CustomerPhoneRepository.Count(CustomerPhoneFilter);
            if (count == 0)
                CustomerPhone.AddError(nameof(CustomerPhoneValidator), nameof(CustomerPhone.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerPhone CustomerPhone)
        {
            return CustomerPhone.IsValidated;
        }

        public async Task<bool> Update(CustomerPhone CustomerPhone)
        {
            if (await ValidateId(CustomerPhone))
            {
            }
            return CustomerPhone.IsValidated;
        }

        public async Task<bool> Delete(CustomerPhone CustomerPhone)
        {
            if (await ValidateId(CustomerPhone))
            {
            }
            return CustomerPhone.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerPhone> CustomerPhones)
        {
            foreach (CustomerPhone CustomerPhone in CustomerPhones)
            {
                await Delete(CustomerPhone);
            }
            return CustomerPhones.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerPhone> CustomerPhones)
        {
            return true;
        }
    }
}
