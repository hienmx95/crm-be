using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCompanyEmail
{
    public interface ICompanyEmailValidator : IServiceScoped
    {
        Task<bool> Create(CompanyEmail CompanyEmail);
        Task<bool> Update(CompanyEmail CompanyEmail);
        Task<bool> Delete(CompanyEmail CompanyEmail);
        Task<bool> BulkDelete(List<CompanyEmail> CompanyEmails);
        Task<bool> Import(List<CompanyEmail> CompanyEmails);
    }

    public class CompanyEmailValidator : ICompanyEmailValidator
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

        public CompanyEmailValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CompanyEmail CompanyEmail)
        {
            CompanyEmailFilter CompanyEmailFilter = new CompanyEmailFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CompanyEmail.Id },
                Selects = CompanyEmailSelect.Id
            };

            int count = await UOW.CompanyEmailRepository.Count(CompanyEmailFilter);
            if (count == 0)
                CompanyEmail.AddError(nameof(CompanyEmailValidator), nameof(CompanyEmail.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateReciepient(CompanyEmail CompanyEmail)
        {
            if (string.IsNullOrWhiteSpace(CompanyEmail.Reciepient))
            {
                CompanyEmail.AddError(nameof(CompanyEmailValidator), nameof(CompanyEmail.Reciepient), ErrorCode.ReciepientEmpty);
            }
            else
            {
                if (!IsValidEmail(CompanyEmail.Reciepient))
                    CompanyEmail.AddError(nameof(CompanyEmailValidator), nameof(CompanyEmail.Reciepient), ErrorCode.ReciepientEmailInvalid);
                if (CompanyEmail.Reciepient.Length > 255)
                {
                    CompanyEmail.AddError(nameof(CompanyEmailValidator), nameof(CompanyEmail.Reciepient), ErrorCode.ReciepientEmailOverLength);
                }
            }
            return CompanyEmail.IsValidated;
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

        public async Task<bool> Create(CompanyEmail CompanyEmail)
        {
            await ValidateReciepient(CompanyEmail);
            return CompanyEmail.IsValidated;
        }

        public async Task<bool> Update(CompanyEmail CompanyEmail)
        {
            if (await ValidateId(CompanyEmail))
            {
                await ValidateReciepient(CompanyEmail);
            }
            return CompanyEmail.IsValidated;
        }

        public async Task<bool> Delete(CompanyEmail CompanyEmail)
        {
            if (await ValidateId(CompanyEmail))
            {
            }
            return CompanyEmail.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CompanyEmail> CompanyEmails)
        {
            foreach (CompanyEmail CompanyEmail in CompanyEmails)
            {
                await Delete(CompanyEmail);
            }
            return CompanyEmails.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CompanyEmail> CompanyEmails)
        {
            return true;
        }
    }
}
