using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MContactEmail
{
    public interface IContactEmailValidator : IServiceScoped
    {
        Task<bool> Create(ContactEmail ContactEmail);
        Task<bool> Update(ContactEmail ContactEmail);
        Task<bool> Delete(ContactEmail ContactEmail);
        Task<bool> BulkDelete(List<ContactEmail> ContactEmails);
        Task<bool> Import(List<ContactEmail> ContactEmails);
    }

    public class ContactEmailValidator : IContactEmailValidator
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

        public ContactEmailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ContactEmail ContactEmail)
        {
            ContactEmailFilter ContactEmailFilter = new ContactEmailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ContactEmail.Id },
                Selects = ContactEmailSelect.Id
            };

            int count = await UOW.ContactEmailRepository.Count(ContactEmailFilter);
            if (count == 0)
                ContactEmail.AddError(nameof(ContactEmailValidator), nameof(ContactEmail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateReciepient(ContactEmail ContactEmail)
        {
            if (string.IsNullOrWhiteSpace(ContactEmail.Reciepient))
            {
                ContactEmail.AddError(nameof(ContactEmailValidator), nameof(ContactEmail.Reciepient), ErrorCode.ReciepientEmpty);
            }
            else
            {
                if (!IsValidEmail(ContactEmail.Reciepient))
                    ContactEmail.AddError(nameof(ContactEmailValidator), nameof(ContactEmail.Reciepient), ErrorCode.ReciepientEmailInvalid);
                if (ContactEmail.Reciepient.Length > 255)
                {
                    ContactEmail.AddError(nameof(ContactEmailValidator), nameof(ContactEmail.Reciepient), ErrorCode.ReciepientEmailOverLength);
                }
            }
            return ContactEmail.IsValidated;
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

        public async Task<bool> Create(ContactEmail ContactEmail)
        {
            await ValidateReciepient(ContactEmail);
            return ContactEmail.IsValidated;
        }

        public async Task<bool> Update(ContactEmail ContactEmail)
        {
            if (await ValidateId(ContactEmail))
            {
                await ValidateReciepient(ContactEmail);
            }
            return ContactEmail.IsValidated;
        }

        public async Task<bool> Delete(ContactEmail ContactEmail)
        {
            if (await ValidateId(ContactEmail))
            {
            }
            return ContactEmail.IsValidated;
        }

        public async Task<bool> BulkDelete(List<ContactEmail> ContactEmails)
        {
            foreach (ContactEmail ContactEmail in ContactEmails)
            {
                await Delete(ContactEmail);
            }
            return ContactEmails.All(x => x.IsValidated);
        }

        public async Task<bool> Import(List<ContactEmail> ContactEmails)
        {
            return true;
        }
    }
}
