using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAEscalationMail
{
    public interface ISLAEscalationMailValidator : IServiceScoped
    {
        Task<bool> Create(SLAEscalationMail SLAEscalationMail);
        Task<bool> Update(SLAEscalationMail SLAEscalationMail);
        Task<bool> Delete(SLAEscalationMail SLAEscalationMail);
        Task<bool> BulkDelete(List<SLAEscalationMail> SLAEscalationMails);
        Task<bool> Import(List<SLAEscalationMail> SLAEscalationMails);
    }

    public class SLAEscalationMailValidator : ISLAEscalationMailValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAEscalationMailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAEscalationMail SLAEscalationMail)
        {
            SLAEscalationMailFilter SLAEscalationMailFilter = new SLAEscalationMailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAEscalationMail.Id },
                Selects = SLAEscalationMailSelect.Id
            };

            int count = await UOW.SLAEscalationMailRepository.Count(SLAEscalationMailFilter);
            if (count == 0)
                SLAEscalationMail.AddError(nameof(SLAEscalationMailValidator), nameof(SLAEscalationMail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAEscalationMail SLAEscalationMail)
        {
            return SLAEscalationMail.IsValidated;
        }

        public async Task<bool> Update(SLAEscalationMail SLAEscalationMail)
        {
            if (await ValidateId(SLAEscalationMail))
            {
            }
            return SLAEscalationMail.IsValidated;
        }

        public async Task<bool> Delete(SLAEscalationMail SLAEscalationMail)
        {
            if (await ValidateId(SLAEscalationMail))
            {
            }
            return SLAEscalationMail.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAEscalationMail> SLAEscalationMails)
        {
            foreach (SLAEscalationMail SLAEscalationMail in SLAEscalationMails)
            {
                await Delete(SLAEscalationMail);
            }
            return SLAEscalationMails.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAEscalationMail> SLAEscalationMails)
        {
            return true;
        }
    }
}
