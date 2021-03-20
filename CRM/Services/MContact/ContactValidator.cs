using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MContact
{
    public interface IContactValidator : IServiceScoped
    {
        Task<bool> Create(Contact Contact);
        Task<bool> Update(Contact Contact);
        Task<bool> Delete(Contact Contact);
        Task<bool> BulkDelete(List<Contact> Contacts);
        Task<bool> Import(List<Contact> Contacts);
    }

    public class ContactValidator : IContactValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            NameEmpty,
            NameOverLength,
            AddressEmpty,
            AddressOverLength,
            PhoneEmpty,
            PhoneOverLength,
            PhoneExisted,
            AppUserNotExisted,
            AppUserEmpty,
            CompanyNotExisted,
            CompanyEmpty

        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ContactValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Contact Contact)
        {
            ContactFilter ContactFilter = new ContactFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Contact.Id },
                Selects = ContactSelect.Id
            };

            int count = await UOW.ContactRepository.Count(ContactFilter);
            if (count == 0)
                Contact.AddError(nameof(ContactValidator), nameof(Contact.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        public async Task<bool> ValidateName(Contact Contact)
        {
            if (string.IsNullOrWhiteSpace(Contact.Name))
            {
                Contact.AddError(nameof(ContactValidator), nameof(Contact.Name), ErrorCode.NameEmpty);
            }
            else if (Contact.Name.Length > 500)
            {
                Contact.AddError(nameof(ContactValidator), nameof(Contact.Name), ErrorCode.NameOverLength);
            }
            return Contact.IsValidated;
        }
        public async Task<bool> ValidateAddress(Contact Contact)
        {
            if (!string.IsNullOrWhiteSpace(Contact.Address) && Contact.Address.Length > 2000)
            {
                Contact.AddError(nameof(ContactValidator), nameof(Contact.Address), ErrorCode.AddressOverLength);
            }
            return Contact.IsValidated;
        }
        public async Task<bool> ValidatePhone(Contact Contact)
        {
            if (string.IsNullOrWhiteSpace(Contact.Phone))
            {
                Contact.AddError(nameof(ContactValidator), nameof(Customer.Phone), ErrorCode.PhoneEmpty);
            }
            else if (Contact.Phone.Length > 20)
            {
                Contact.AddError(nameof(ContactValidator), nameof(Customer.Phone), ErrorCode.PhoneOverLength);
            }
            else
            {
                ContactFilter ContactFilter = new ContactFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { NotEqual = Contact.Id },
                    Phone = new StringFilter { Equal = Contact.Phone }
                };

                int count = await UOW.ContactRepository.Count(ContactFilter);
                if (count != 0)
                    Contact.AddError(nameof(ContactValidator), nameof(Contact.Phone), ErrorCode.PhoneExisted);
            }
            return Contact.IsValidated;
        }
        public async Task<bool> ValidateAppUser(Contact Contact)
        {
            if (Contact.AppUserId.HasValue)
            {
                AppUserFilter AppUserFilter = new AppUserFilter
                {
                    Id = new IdFilter { Equal = Contact.AppUserId }
                };

                var count = await UOW.AppUserRepository.Count(AppUserFilter);
                if (count == 0)
                    Contact.AddError(nameof(ContactValidator), nameof(Company.AppUser), ErrorCode.AppUserNotExisted);
            }
            else
            {
                Contact.AddError(nameof(ContactValidator), nameof(Company.AppUser), ErrorCode.AppUserEmpty);
            }
            return Contact.IsValidated;
        }
        public async Task<bool> ValidateCompany(Contact Contact)
        {
            if (Contact.CompanyId.HasValue)
            {
                CompanyFilter CompanyFilter = new CompanyFilter
                {
                    Id = new IdFilter { Equal = Contact.CompanyId }
                };

                var count = await UOW.CompanyRepository.Count(CompanyFilter);
                if (count == 0)
                    Contact.AddError(nameof(ContactValidator), nameof(Contact.Company), ErrorCode.CompanyNotExisted);
            }
            else
            {
                Contact.AddError(nameof(ContactValidator), nameof(Contact.Company), ErrorCode.CompanyEmpty);
            }
            return Contact.IsValidated;
        }
        public async Task<bool>Create(Contact Contact)
        {
            await ValidateName(Contact);
            await ValidateAddress(Contact);
            await ValidatePhone(Contact);
            await ValidateAppUser(Contact);
            //await ValidateCompany(Contact);
            return Contact.IsValidated;
        }

        public async Task<bool> Update(Contact Contact)
        {
            if (await ValidateId(Contact))
            {
                await ValidateName(Contact);
                await ValidateAddress(Contact);
                await ValidatePhone(Contact);
                await ValidateAppUser(Contact);
                //await ValidateCompany(Contact);
            }
            return Contact.IsValidated;
        }

        public async Task<bool> Delete(Contact Contact)
        {
            if (await ValidateId(Contact))
            {
            }
            return Contact.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Contact> Contacts)
        {
            foreach (Contact Contact in Contacts)
            {
                await Delete(Contact);
            }
            return Contacts.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Contact> Contacts)
        {
            return true;
        }
    }
}
