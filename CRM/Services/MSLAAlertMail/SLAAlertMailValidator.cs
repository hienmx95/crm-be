using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAAlertMail
{
    public interface ISLAAlertMailValidator : IServiceScoped
    {
        Task<bool> Create(SLAAlertMail SLAAlertMail);
        Task<bool> Update(SLAAlertMail SLAAlertMail);
        Task<bool> Delete(SLAAlertMail SLAAlertMail);
        Task<bool> BulkDelete(List<SLAAlertMail> SLAAlertMails);
        Task<bool> Import(List<SLAAlertMail> SLAAlertMails);
    }

    public class SLAAlertMailValidator : ISLAAlertMailValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAAlertMailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAAlertMail SLAAlertMail)
        {
            SLAAlertMailFilter SLAAlertMailFilter = new SLAAlertMailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAAlertMail.Id },
                Selects = SLAAlertMailSelect.Id
            };

            int count = await UOW.SLAAlertMailRepository.Count(SLAAlertMailFilter);
            if (count == 0)
                SLAAlertMail.AddError(nameof(SLAAlertMailValidator), nameof(SLAAlertMail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAAlertMail SLAAlertMail)
        {
            return SLAAlertMail.IsValidated;
        }

        public async Task<bool> Update(SLAAlertMail SLAAlertMail)
        {
            if (await ValidateId(SLAAlertMail))
            {
            }
            return SLAAlertMail.IsValidated;
        }

        public async Task<bool> Delete(SLAAlertMail SLAAlertMail)
        {
            if (await ValidateId(SLAAlertMail))
            {
            }
            return SLAAlertMail.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAAlertMail> SLAAlertMails)
        {
            foreach (SLAAlertMail SLAAlertMail in SLAAlertMails)
            {
                await Delete(SLAAlertMail);
            }
            return SLAAlertMails.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAAlertMail> SLAAlertMails)
        {
            return true;
        }
    }
}
