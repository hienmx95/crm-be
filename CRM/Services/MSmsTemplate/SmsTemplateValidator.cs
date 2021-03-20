using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSmsTemplate
{
    public interface ISmsTemplateValidator : IServiceScoped
    {
        Task<bool> Create(SmsTemplate SmsTemplate);
        Task<bool> Update(SmsTemplate SmsTemplate);
        Task<bool> Delete(SmsTemplate SmsTemplate);
        Task<bool> BulkDelete(List<SmsTemplate> SmsTemplates);
        Task<bool> Import(List<SmsTemplate> SmsTemplates);
    }

    public class SmsTemplateValidator : ISmsTemplateValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CodeEmpty,
            CodeExisted,
            CodeHasSpecialCharacter,
            NameEmpty,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SmsTemplateValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SmsTemplate SmsTemplate)
        {
            SmsTemplateFilter SmsTemplateFilter = new SmsTemplateFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SmsTemplate.Id },
                Selects = SmsTemplateSelect.Id
            };

            int count = await UOW.SmsTemplateRepository.Count(SmsTemplateFilter);
            if (count == 0)
                SmsTemplate.AddError(nameof(SmsTemplateValidator), nameof(SmsTemplate.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateCode(SmsTemplate SmsTemplate)
        {
            if (string.IsNullOrWhiteSpace(SmsTemplate.Code))
            {
                SmsTemplate.AddError(nameof(SmsTemplateValidator), nameof(SmsTemplate.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = SmsTemplate.Code;
                if (SmsTemplate.Code.Contains(" ") || !FilterExtension.ChangeToEnglishChar(Code).Equals(SmsTemplate.Code))
                {
                    SmsTemplate.AddError(nameof(SmsTemplateValidator), nameof(SmsTemplate.Code), ErrorCode.CodeHasSpecialCharacter);
                }

                SmsTemplateFilter SmsTemplateFilter = new SmsTemplateFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { NotEqual = SmsTemplate.Id },
                    Code = new StringFilter { Equal = SmsTemplate.Code },
                    Selects = SmsTemplateSelect.Code
                };

                int count = await UOW.SmsTemplateRepository.Count(SmsTemplateFilter);
                if (count != 0)
                    SmsTemplate.AddError(nameof(SmsTemplateValidator), nameof(SmsTemplate.Code), ErrorCode.CodeExisted);
            }
            return SmsTemplate.IsValidated;
        }

        public async Task<bool> ValidateName(SmsTemplate SmsTemplate)
        {
            if (string.IsNullOrWhiteSpace(SmsTemplate.Name))
            {
                SmsTemplate.AddError(nameof(SmsTemplateValidator), nameof(SmsTemplate.Name), ErrorCode.NameEmpty);
            } 
            return SmsTemplate.IsValidated;
        }
        public async Task<bool>Create(SmsTemplate SmsTemplate)
        {
            await ValidateCode(SmsTemplate);
            await ValidateName(SmsTemplate);
            return SmsTemplate.IsValidated;
        }

        public async Task<bool> Update(SmsTemplate SmsTemplate)
        {
            if (await ValidateId(SmsTemplate))
            {
                await ValidateCode(SmsTemplate);
                await ValidateName(SmsTemplate);
            }
            return SmsTemplate.IsValidated;
        }

        public async Task<bool> Delete(SmsTemplate SmsTemplate)
        {
            if (await ValidateId(SmsTemplate))
            {
            }
            return SmsTemplate.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SmsTemplate> SmsTemplates)
        {
            foreach (SmsTemplate SmsTemplate in SmsTemplates)
            {
                await Delete(SmsTemplate);
            }
            return SmsTemplates.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SmsTemplate> SmsTemplates)
        {
            return true;
        }
    }
}
