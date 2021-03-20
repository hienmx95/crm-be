using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAEscalationFRTPhone
{
    public interface ISLAEscalationFRTPhoneValidator : IServiceScoped
    {
        Task<bool> Create(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<bool> Update(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<bool> Delete(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<bool> BulkDelete(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones);
        Task<bool> Import(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones);
    }

    public class SLAEscalationFRTPhoneValidator : ISLAEscalationFRTPhoneValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAEscalationFRTPhoneValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter = new SLAEscalationFRTPhoneFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAEscalationFRTPhone.Id },
                Selects = SLAEscalationFRTPhoneSelect.Id
            };

            int count = await UOW.SLAEscalationFRTPhoneRepository.Count(SLAEscalationFRTPhoneFilter);
            if (count == 0)
                SLAEscalationFRTPhone.AddError(nameof(SLAEscalationFRTPhoneValidator), nameof(SLAEscalationFRTPhone.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            return SLAEscalationFRTPhone.IsValidated;
        }

        public async Task<bool> Update(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            if (await ValidateId(SLAEscalationFRTPhone))
            {
            }
            return SLAEscalationFRTPhone.IsValidated;
        }

        public async Task<bool> Delete(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            if (await ValidateId(SLAEscalationFRTPhone))
            {
            }
            return SLAEscalationFRTPhone.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones)
        {
            foreach (SLAEscalationFRTPhone SLAEscalationFRTPhone in SLAEscalationFRTPhones)
            {
                await Delete(SLAEscalationFRTPhone);
            }
            return SLAEscalationFRTPhones.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones)
        {
            return true;
        }
    }
}
