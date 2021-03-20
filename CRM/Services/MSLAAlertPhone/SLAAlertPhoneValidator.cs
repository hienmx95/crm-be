using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAAlertPhone
{
    public interface ISLAAlertPhoneValidator : IServiceScoped
    {
        Task<bool> Create(SLAAlertPhone SLAAlertPhone);
        Task<bool> Update(SLAAlertPhone SLAAlertPhone);
        Task<bool> Delete(SLAAlertPhone SLAAlertPhone);
        Task<bool> BulkDelete(List<SLAAlertPhone> SLAAlertPhones);
        Task<bool> Import(List<SLAAlertPhone> SLAAlertPhones);
    }

    public class SLAAlertPhoneValidator : ISLAAlertPhoneValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAAlertPhoneValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAAlertPhone SLAAlertPhone)
        {
            SLAAlertPhoneFilter SLAAlertPhoneFilter = new SLAAlertPhoneFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAAlertPhone.Id },
                Selects = SLAAlertPhoneSelect.Id
            };

            int count = await UOW.SLAAlertPhoneRepository.Count(SLAAlertPhoneFilter);
            if (count == 0)
                SLAAlertPhone.AddError(nameof(SLAAlertPhoneValidator), nameof(SLAAlertPhone.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAAlertPhone SLAAlertPhone)
        {
            return SLAAlertPhone.IsValidated;
        }

        public async Task<bool> Update(SLAAlertPhone SLAAlertPhone)
        {
            if (await ValidateId(SLAAlertPhone))
            {
            }
            return SLAAlertPhone.IsValidated;
        }

        public async Task<bool> Delete(SLAAlertPhone SLAAlertPhone)
        {
            if (await ValidateId(SLAAlertPhone))
            {
            }
            return SLAAlertPhone.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAAlertPhone> SLAAlertPhones)
        {
            foreach (SLAAlertPhone SLAAlertPhone in SLAAlertPhones)
            {
                await Delete(SLAAlertPhone);
            }
            return SLAAlertPhones.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAAlertPhone> SLAAlertPhones)
        {
            return true;
        }
    }
}
