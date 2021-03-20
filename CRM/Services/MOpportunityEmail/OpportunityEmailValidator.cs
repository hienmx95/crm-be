using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MOpportunityEmail
{
    public interface IOpportunityEmailValidator : IServiceScoped
    {
        Task<bool> Create(OpportunityEmail OpportunityEmail);
        Task<bool> Update(OpportunityEmail OpportunityEmail);
        Task<bool> Delete(OpportunityEmail OpportunityEmail);
        Task<bool> BulkDelete(List<OpportunityEmail> OpportunityEmails);
        Task<bool> Import(List<OpportunityEmail> OpportunityEmails);
    }

    public class OpportunityEmailValidator : IOpportunityEmailValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            ReciepientEmpty,
            ReciepientEmailInvalid,
            ReciepientEmailOverLength
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public OpportunityEmailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(OpportunityEmail OpportunityEmail)
        {
            OpportunityEmailFilter OpportunityEmailFilter = new OpportunityEmailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = OpportunityEmail.Id },
                Selects = OpportunityEmailSelect.Id
            };

            int count = await UOW.OpportunityEmailRepository.Count(OpportunityEmailFilter);
            if (count == 0)
                OpportunityEmail.AddError(nameof(OpportunityEmailValidator), nameof(OpportunityEmail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateReciepient(OpportunityEmail OpportunityEmail)
        {
            if (string.IsNullOrWhiteSpace(OpportunityEmail.Reciepient))
            {
                OpportunityEmail.AddError(nameof(OpportunityEmailValidator), nameof(OpportunityEmail.Reciepient), ErrorCode.ReciepientEmpty);
            }
            else
            {
                if (!IsValidEmail(OpportunityEmail.Reciepient))
                    OpportunityEmail.AddError(nameof(OpportunityEmailValidator), nameof(OpportunityEmail.Reciepient), ErrorCode.ReciepientEmailInvalid);
                if (OpportunityEmail.Reciepient.Length > 255)
                {
                    OpportunityEmail.AddError(nameof(OpportunityEmailValidator), nameof(OpportunityEmail.Reciepient), ErrorCode.ReciepientEmailOverLength);
                }
            }
            return OpportunityEmail.IsValidated;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool>Create(OpportunityEmail OpportunityEmail)
        {
            await ValidateReciepient(OpportunityEmail);
            return OpportunityEmail.IsValidated;
        }

        public async Task<bool> Update(OpportunityEmail OpportunityEmail)
        {
            if (await ValidateId(OpportunityEmail))
            {
            }
            return OpportunityEmail.IsValidated;
        }

        public async Task<bool> Delete(OpportunityEmail OpportunityEmail)
        {
            if (await ValidateId(OpportunityEmail))
            {
            }
            return OpportunityEmail.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<OpportunityEmail> OpportunityEmails)
        {
            foreach (OpportunityEmail OpportunityEmail in OpportunityEmails)
            {
                await Delete(OpportunityEmail);
            }
            return OpportunityEmails.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<OpportunityEmail> OpportunityEmails)
        {
            return true;
        }
    }
}
