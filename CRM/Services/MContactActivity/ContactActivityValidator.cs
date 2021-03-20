using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Helpers;

namespace CRM.Services.MContactActivity
{
    public interface IContactActivityValidator : IServiceScoped
    {
        Task<bool> Create(ContactActivity ContactActivity);
        Task<bool> Update(ContactActivity ContactActivity);
        Task<bool> Delete(ContactActivity ContactActivity);
        Task<bool> BulkDelete(List<ContactActivity> ContactActivities);
        Task<bool> Import(List<ContactActivity> ContactActivities);
    }

    public class ContactActivityValidator : IContactActivityValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            FromDateEmpty,
            ToDateInvalid,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ContactActivityValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ContactActivity ContactActivity)
        {
            ContactActivityFilter ContactActivityFilter = new ContactActivityFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ContactActivity.Id },
                Selects = ContactActivitySelect.Id
            };

            int count = await UOW.ContactActivityRepository.Count(ContactActivityFilter);
            if (count == 0)
                ContactActivity.AddError(nameof(ContactActivityValidator), nameof(ContactActivity.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateDate(ContactActivity ContactActivity)
        {
            if (ContactActivity.FromDate == DateTime.MinValue)
            {
                ContactActivity.AddError(nameof(ContactActivityValidator), nameof(ContactActivity.FromDate), ErrorCode.FromDateEmpty);
            }
            else
            {
                if (ContactActivity.ToDate.HasValue)
                {
                    if (ContactActivity.ToDate.Value <= ContactActivity.FromDate)
                    {
                        ContactActivity.AddError(nameof(ContactActivityValidator), nameof(ContactActivity.ToDate), ErrorCode.ToDateInvalid);
                    }
                }
            }
            return ContactActivity.IsValidated;
        }

        public async Task<bool>Create(ContactActivity ContactActivity)
        {
            await ValidateDate(ContactActivity);
            return ContactActivity.IsValidated;
        }

        public async Task<bool> Update(ContactActivity ContactActivity)
        {
            if (await ValidateId(ContactActivity))
            {
                await ValidateDate(ContactActivity);
            }
            return ContactActivity.IsValidated;
        }

        public async Task<bool> Delete(ContactActivity ContactActivity)
        {
            if (await ValidateId(ContactActivity))
            {
            }
            return ContactActivity.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ContactActivity> ContactActivities)
        {
            foreach (ContactActivity ContactActivity in ContactActivities)
            {
                await Delete(ContactActivity);
            }
            return ContactActivities.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<ContactActivity> ContactActivities)
        {
            return true;
        }
    }
}
