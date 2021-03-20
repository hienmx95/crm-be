using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerEmail
{
    public interface ICustomerEmailValidator : IServiceScoped
    {
        Task<bool> Create(CustomerEmail CustomerEmail);
        Task<bool> Update(CustomerEmail CustomerEmail);
        Task<bool> Delete(CustomerEmail CustomerEmail);
        Task<bool> BulkDelete(List<CustomerEmail> CustomerEmails);
        Task<bool> Import(List<CustomerEmail> CustomerEmails);
    }

    public class CustomerEmailValidator : ICustomerEmailValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerEmailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerEmail CustomerEmail)
        {
            CustomerEmailFilter CustomerEmailFilter = new CustomerEmailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerEmail.Id },
                Selects = CustomerEmailSelect.Id
            };

            int count = await UOW.CustomerEmailRepository.Count(CustomerEmailFilter);
            if (count == 0)
                CustomerEmail.AddError(nameof(CustomerEmailValidator), nameof(CustomerEmail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerEmail CustomerEmail)
        {
            return CustomerEmail.IsValidated;
        }

        public async Task<bool> Update(CustomerEmail CustomerEmail)
        {
            if (await ValidateId(CustomerEmail))
            {
            }
            return CustomerEmail.IsValidated;
        }

        public async Task<bool> Delete(CustomerEmail CustomerEmail)
        {
            if (await ValidateId(CustomerEmail))
            {
            }
            return CustomerEmail.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerEmail> CustomerEmails)
        {
            foreach (CustomerEmail CustomerEmail in CustomerEmails)
            {
                await Delete(CustomerEmail);
            }
            return CustomerEmails.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerEmail> CustomerEmails)
        {
            return true;
        }
    }
}
