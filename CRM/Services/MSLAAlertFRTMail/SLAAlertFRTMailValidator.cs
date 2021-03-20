using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAAlertFRTMail
{
    public interface ISLAAlertFRTMailValidator : IServiceScoped
    {
        Task<bool> Create(SLAAlertFRTMail SLAAlertFRTMail);
        Task<bool> Update(SLAAlertFRTMail SLAAlertFRTMail);
        Task<bool> Delete(SLAAlertFRTMail SLAAlertFRTMail);
        Task<bool> BulkDelete(List<SLAAlertFRTMail> SLAAlertFRTMails);
        Task<bool> Import(List<SLAAlertFRTMail> SLAAlertFRTMails);
    }

    public class SLAAlertFRTMailValidator : ISLAAlertFRTMailValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAAlertFRTMailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAAlertFRTMail SLAAlertFRTMail)
        {
            SLAAlertFRTMailFilter SLAAlertFRTMailFilter = new SLAAlertFRTMailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAAlertFRTMail.Id },
                Selects = SLAAlertFRTMailSelect.Id
            };

            int count = await UOW.SLAAlertFRTMailRepository.Count(SLAAlertFRTMailFilter);
            if (count == 0)
                SLAAlertFRTMail.AddError(nameof(SLAAlertFRTMailValidator), nameof(SLAAlertFRTMail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAAlertFRTMail SLAAlertFRTMail)
        {
            return SLAAlertFRTMail.IsValidated;
        }

        public async Task<bool> Update(SLAAlertFRTMail SLAAlertFRTMail)
        {
            if (await ValidateId(SLAAlertFRTMail))
            {
            }
            return SLAAlertFRTMail.IsValidated;
        }

        public async Task<bool> Delete(SLAAlertFRTMail SLAAlertFRTMail)
        {
            if (await ValidateId(SLAAlertFRTMail))
            {
            }
            return SLAAlertFRTMail.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAAlertFRTMail> SLAAlertFRTMails)
        {
            foreach (SLAAlertFRTMail SLAAlertFRTMail in SLAAlertFRTMails)
            {
                await Delete(SLAAlertFRTMail);
            }
            return SLAAlertFRTMails.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAAlertFRTMail> SLAAlertFRTMails)
        {
            return true;
        }
    }
}
