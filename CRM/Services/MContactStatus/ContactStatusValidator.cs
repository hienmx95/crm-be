using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MContactStatus
{
    public interface IContactStatusValidator : IServiceScoped
    {
        Task<bool> Create(ContactStatus ContactStatus);
        Task<bool> Update(ContactStatus ContactStatus);
        Task<bool> Delete(ContactStatus ContactStatus);
        Task<bool> BulkDelete(List<ContactStatus> ContactStatuses);
        Task<bool> Import(List<ContactStatus> ContactStatuses);
    }

    public class ContactStatusValidator : IContactStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ContactStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ContactStatus ContactStatus)
        {
            ContactStatusFilter ContactStatusFilter = new ContactStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ContactStatus.Id },
                Selects = ContactStatusSelect.Id
            };

            int count = await UOW.ContactStatusRepository.Count(ContactStatusFilter);
            if (count == 0)
                ContactStatus.AddError(nameof(ContactStatusValidator), nameof(ContactStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(ContactStatus ContactStatus)
        {
            return ContactStatus.IsValidated;
        }

        public async Task<bool> Update(ContactStatus ContactStatus)
        {
            if (await ValidateId(ContactStatus))
            {
            }
            return ContactStatus.IsValidated;
        }

        public async Task<bool> Delete(ContactStatus ContactStatus)
        {
            if (await ValidateId(ContactStatus))
            {
            }
            return ContactStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ContactStatus> ContactStatuses)
        {
            foreach (ContactStatus ContactStatus in ContactStatuses)
            {
                await Delete(ContactStatus);
            }
            return ContactStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<ContactStatus> ContactStatuses)
        {
            return true;
        }
    }
}
