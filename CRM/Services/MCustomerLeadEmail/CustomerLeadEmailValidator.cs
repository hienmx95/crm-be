using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerLeadEmail
{
    public interface ICustomerLeadEmailValidator : IServiceScoped
    {
        Task<bool> Create(CustomerLeadEmail CustomerLeadEmail);
        Task<bool> Update(CustomerLeadEmail CustomerLeadEmail);
        Task<bool> Delete(CustomerLeadEmail CustomerLeadEmail);
        Task<bool> BulkDelete(List<CustomerLeadEmail> CustomerLeadEmails);
        Task<bool> Import(List<CustomerLeadEmail> CustomerLeadEmails);
    }

    public class CustomerLeadEmailValidator : ICustomerLeadEmailValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            ReciepientEmpty,
            ReciepientEmailInvalid,
            ReciepientEmailOverLength
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerLeadEmailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerLeadEmail CustomerLeadEmail)
        {
            CustomerLeadEmailFilter CustomerLeadEmailFilter = new CustomerLeadEmailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerLeadEmail.Id },
                Selects = CustomerLeadEmailSelect.Id
            };

            int count = await UOW.CustomerLeadEmailRepository.Count(CustomerLeadEmailFilter);
            if (count == 0)
                CustomerLeadEmail.AddError(nameof(CustomerLeadEmailValidator), nameof(CustomerLeadEmail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateReciepient(CustomerLeadEmail CustomerLeadEmail)
        {
            if (string.IsNullOrWhiteSpace(CustomerLeadEmail.Reciepient))
            {
                CustomerLeadEmail.AddError(nameof(CustomerLeadEmailValidator), nameof(CustomerLeadEmail.Reciepient), ErrorCode.ReciepientEmpty);
            }
            else
            {
                if (!IsValidEmail(CustomerLeadEmail.Reciepient))
                    CustomerLeadEmail.AddError(nameof(CustomerLeadEmailValidator), nameof(CustomerLeadEmail.Reciepient), ErrorCode.ReciepientEmailInvalid);
                if (CustomerLeadEmail.Reciepient.Length > 255)
                {
                    CustomerLeadEmail.AddError(nameof(CustomerLeadEmailValidator), nameof(CustomerLeadEmail.Reciepient), ErrorCode.ReciepientEmailOverLength);
                }
            }
            return CustomerLeadEmail.IsValidated;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool>Create(CustomerLeadEmail CustomerLeadEmail)
        {
            await ValidateReciepient(CustomerLeadEmail);
            return CustomerLeadEmail.IsValidated;
        }

        public async Task<bool> Update(CustomerLeadEmail CustomerLeadEmail)
        {
            if (await ValidateId(CustomerLeadEmail))
            {
                await ValidateReciepient(CustomerLeadEmail);
            }
            return CustomerLeadEmail.IsValidated;
        }

        public async Task<bool> Delete(CustomerLeadEmail CustomerLeadEmail)
        {
            if (await ValidateId(CustomerLeadEmail))
            {
            }
            return CustomerLeadEmail.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerLeadEmail> CustomerLeadEmails)
        {
            foreach (CustomerLeadEmail CustomerLeadEmail in CustomerLeadEmails)
            {
                await Delete(CustomerLeadEmail);
            }
            return CustomerLeadEmails.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerLeadEmail> CustomerLeadEmails)
        {
            return true;
        }
    }
}
