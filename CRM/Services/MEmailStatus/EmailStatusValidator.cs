using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MEmailStatus
{
    public interface IEmailStatusValidator : IServiceScoped
    {
        Task<bool> Create(EmailStatus EmailStatus);
        Task<bool> Update(EmailStatus EmailStatus);
        Task<bool> Delete(EmailStatus EmailStatus);
        Task<bool> BulkDelete(List<EmailStatus> EmailStatuses);
        Task<bool> Import(List<EmailStatus> EmailStatuses);
    }

    public class EmailStatusValidator : IEmailStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public EmailStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(EmailStatus EmailStatus)
        {
            EmailStatusFilter EmailStatusFilter = new EmailStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = EmailStatus.Id },
                Selects = EmailStatusSelect.Id
            };

            int count = await UOW.EmailStatusRepository.Count(EmailStatusFilter);
            if (count == 0)
                EmailStatus.AddError(nameof(EmailStatusValidator), nameof(EmailStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(EmailStatus EmailStatus)
        {
            return EmailStatus.IsValidated;
        }

        public async Task<bool> Update(EmailStatus EmailStatus)
        {
            if (await ValidateId(EmailStatus))
            {
            }
            return EmailStatus.IsValidated;
        }

        public async Task<bool> Delete(EmailStatus EmailStatus)
        {
            if (await ValidateId(EmailStatus))
            {
            }
            return EmailStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<EmailStatus> EmailStatuses)
        {
            foreach (EmailStatus EmailStatus in EmailStatuses)
            {
                await Delete(EmailStatus);
            }
            return EmailStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<EmailStatus> EmailStatuses)
        {
            return true;
        }
    }
}
