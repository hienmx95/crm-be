using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MMailTemplate
{
    public interface IMailTemplateValidator : IServiceScoped
    {
        Task<bool> Create(MailTemplate MailTemplate);
        Task<bool> Update(MailTemplate MailTemplate);
        Task<bool> Delete(MailTemplate MailTemplate);
        Task<bool> BulkDelete(List<MailTemplate> MailTemplates);
        Task<bool> Import(List<MailTemplate> MailTemplates);
    }

    public class MailTemplateValidator : IMailTemplateValidator
    {
        public enum ErrorCode
        {
            IdNotExisted, CodeEmpty,
            CodeExisted,
            CodeHasSpecialCharacter,
            NameEmpty,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public MailTemplateValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(MailTemplate MailTemplate)
        {
            MailTemplateFilter MailTemplateFilter = new MailTemplateFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = MailTemplate.Id },
                Selects = MailTemplateSelect.Id
            };

            int count = await UOW.MailTemplateRepository.Count(MailTemplateFilter);
            if (count == 0)
                MailTemplate.AddError(nameof(MailTemplateValidator), nameof(MailTemplate.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        public async Task<bool> ValidateName(MailTemplate MailTemplate)
        {
            if (string.IsNullOrWhiteSpace(MailTemplate.Name))
            {
                MailTemplate.AddError(nameof(MailTemplateValidator), nameof(MailTemplate.Name), ErrorCode.NameEmpty);
            }
            return MailTemplate.IsValidated;
        }
        public async Task<bool> ValidateCode(MailTemplate MailTemplate)
        {
            if (string.IsNullOrWhiteSpace(MailTemplate.Code))
            {
                MailTemplate.AddError(nameof(MailTemplateValidator), nameof(MailTemplate.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = MailTemplate.Code;
                if (MailTemplate.Code.Contains(" ") || !FilterExtension.ChangeToEnglishChar(Code).Equals(MailTemplate.Code))
                {
                    MailTemplate.AddError(nameof(MailTemplateValidator), nameof(MailTemplate.Code), ErrorCode.CodeHasSpecialCharacter);
                }

                MailTemplateFilter MailTemplateFilter = new MailTemplateFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { NotEqual = MailTemplate.Id },
                    Code = new StringFilter { Equal = MailTemplate.Code },
                    Selects = MailTemplateSelect.Code
                };

                int count = await UOW.MailTemplateRepository.Count(MailTemplateFilter);
                if (count != 0)
                    MailTemplate.AddError(nameof(MailTemplateValidator), nameof(MailTemplate.Code), ErrorCode.CodeExisted);
            }
            return MailTemplate.IsValidated;
        }
        public async Task<bool> Create(MailTemplate MailTemplate)
        {
            await ValidateCode(MailTemplate);
            await ValidateName(MailTemplate);
            return MailTemplate.IsValidated;
        }

        public async Task<bool> Update(MailTemplate MailTemplate)
        {
            if (await ValidateId(MailTemplate))
            {
                await ValidateCode(MailTemplate);
                await ValidateName(MailTemplate);
            }
            return MailTemplate.IsValidated;
        }

        public async Task<bool> Delete(MailTemplate MailTemplate)
        {
            if (await ValidateId(MailTemplate))
            {
            }
            return MailTemplate.IsValidated;
        }

        public async Task<bool> BulkDelete(List<MailTemplate> MailTemplates)
        {
            foreach (MailTemplate MailTemplate in MailTemplates)
            {
                await Delete(MailTemplate);
            }
            return MailTemplates.All(x => x.IsValidated);
        }

        public async Task<bool> Import(List<MailTemplate> MailTemplates)
        {
            return true;
        }
    }
}
