using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAEscalationPhone
{
    public interface ISLAEscalationPhoneValidator : IServiceScoped
    {
        Task<bool> Create(SLAEscalationPhone SLAEscalationPhone);
        Task<bool> Update(SLAEscalationPhone SLAEscalationPhone);
        Task<bool> Delete(SLAEscalationPhone SLAEscalationPhone);
        Task<bool> BulkDelete(List<SLAEscalationPhone> SLAEscalationPhones);
        Task<bool> Import(List<SLAEscalationPhone> SLAEscalationPhones);
    }

    public class SLAEscalationPhoneValidator : ISLAEscalationPhoneValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAEscalationPhoneValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAEscalationPhone SLAEscalationPhone)
        {
            SLAEscalationPhoneFilter SLAEscalationPhoneFilter = new SLAEscalationPhoneFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAEscalationPhone.Id },
                Selects = SLAEscalationPhoneSelect.Id
            };

            int count = await UOW.SLAEscalationPhoneRepository.Count(SLAEscalationPhoneFilter);
            if (count == 0)
                SLAEscalationPhone.AddError(nameof(SLAEscalationPhoneValidator), nameof(SLAEscalationPhone.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAEscalationPhone SLAEscalationPhone)
        {
            return SLAEscalationPhone.IsValidated;
        }

        public async Task<bool> Update(SLAEscalationPhone SLAEscalationPhone)
        {
            if (await ValidateId(SLAEscalationPhone))
            {
            }
            return SLAEscalationPhone.IsValidated;
        }

        public async Task<bool> Delete(SLAEscalationPhone SLAEscalationPhone)
        {
            if (await ValidateId(SLAEscalationPhone))
            {
            }
            return SLAEscalationPhone.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAEscalationPhone> SLAEscalationPhones)
        {
            foreach (SLAEscalationPhone SLAEscalationPhone in SLAEscalationPhones)
            {
                await Delete(SLAEscalationPhone);
            }
            return SLAEscalationPhones.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAEscalationPhone> SLAEscalationPhones)
        {
            return true;
        }
    }
}
