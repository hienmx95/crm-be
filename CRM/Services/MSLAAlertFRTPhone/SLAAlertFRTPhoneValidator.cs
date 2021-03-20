using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAAlertFRTPhone
{
    public interface ISLAAlertFRTPhoneValidator : IServiceScoped
    {
        Task<bool> Create(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<bool> Update(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<bool> Delete(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<bool> BulkDelete(List<SLAAlertFRTPhone> SLAAlertFRTPhones);
        Task<bool> Import(List<SLAAlertFRTPhone> SLAAlertFRTPhones);
    }

    public class SLAAlertFRTPhoneValidator : ISLAAlertFRTPhoneValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAAlertFRTPhoneValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter = new SLAAlertFRTPhoneFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAAlertFRTPhone.Id },
                Selects = SLAAlertFRTPhoneSelect.Id
            };

            int count = await UOW.SLAAlertFRTPhoneRepository.Count(SLAAlertFRTPhoneFilter);
            if (count == 0)
                SLAAlertFRTPhone.AddError(nameof(SLAAlertFRTPhoneValidator), nameof(SLAAlertFRTPhone.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            return SLAAlertFRTPhone.IsValidated;
        }

        public async Task<bool> Update(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            if (await ValidateId(SLAAlertFRTPhone))
            {
            }
            return SLAAlertFRTPhone.IsValidated;
        }

        public async Task<bool> Delete(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            if (await ValidateId(SLAAlertFRTPhone))
            {
            }
            return SLAAlertFRTPhone.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAAlertFRTPhone> SLAAlertFRTPhones)
        {
            foreach (SLAAlertFRTPhone SLAAlertFRTPhone in SLAAlertFRTPhones)
            {
                await Delete(SLAAlertFRTPhone);
            }
            return SLAAlertFRTPhones.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAAlertFRTPhone> SLAAlertFRTPhones)
        {
            return true;
        }
    }
}
