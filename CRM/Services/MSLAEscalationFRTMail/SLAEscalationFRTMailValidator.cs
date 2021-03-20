using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAEscalationFRTMail
{
    public interface ISLAEscalationFRTMailValidator : IServiceScoped
    {
        Task<bool> Create(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<bool> Update(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<bool> Delete(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<bool> BulkDelete(List<SLAEscalationFRTMail> SLAEscalationFRTMails);
        Task<bool> Import(List<SLAEscalationFRTMail> SLAEscalationFRTMails);
    }

    public class SLAEscalationFRTMailValidator : ISLAEscalationFRTMailValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAEscalationFRTMailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter = new SLAEscalationFRTMailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAEscalationFRTMail.Id },
                Selects = SLAEscalationFRTMailSelect.Id
            };

            int count = await UOW.SLAEscalationFRTMailRepository.Count(SLAEscalationFRTMailFilter);
            if (count == 0)
                SLAEscalationFRTMail.AddError(nameof(SLAEscalationFRTMailValidator), nameof(SLAEscalationFRTMail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            return SLAEscalationFRTMail.IsValidated;
        }

        public async Task<bool> Update(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            if (await ValidateId(SLAEscalationFRTMail))
            {
            }
            return SLAEscalationFRTMail.IsValidated;
        }

        public async Task<bool> Delete(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            if (await ValidateId(SLAEscalationFRTMail))
            {
            }
            return SLAEscalationFRTMail.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAEscalationFRTMail> SLAEscalationFRTMails)
        {
            foreach (SLAEscalationFRTMail SLAEscalationFRTMail in SLAEscalationFRTMails)
            {
                await Delete(SLAEscalationFRTMail);
            }
            return SLAEscalationFRTMails.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAEscalationFRTMail> SLAEscalationFRTMails)
        {
            return true;
        }
    }
}
