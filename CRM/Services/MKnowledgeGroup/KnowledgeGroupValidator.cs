using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MKnowledgeGroup
{
    public interface IKnowledgeGroupValidator : IServiceScoped
    {
        Task<bool> Create(KnowledgeGroup KnowledgeGroup);
        Task<bool> Update(KnowledgeGroup KnowledgeGroup);
        Task<bool> Delete(KnowledgeGroup KnowledgeGroup);
        Task<bool> BulkDelete(List<KnowledgeGroup> KnowledgeGroups);
        Task<bool> Import(List<KnowledgeGroup> KnowledgeGroups);
    }

    public class KnowledgeGroupValidator : IKnowledgeGroupValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CodeEmpty,
            CodeExisted,
            CodeHasSpecialCharacter,
            NameEmpty,
            NameOverLength,
            StatusNotExisted
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public KnowledgeGroupValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(KnowledgeGroup KnowledgeGroup)
        {
            KnowledgeGroupFilter KnowledgeGroupFilter = new KnowledgeGroupFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = KnowledgeGroup.Id },
                Selects = KnowledgeGroupSelect.Id
            };

            int count = await UOW.KnowledgeGroupRepository.Count(KnowledgeGroupFilter);
            if (count == 0)
                KnowledgeGroup.AddError(nameof(KnowledgeGroupValidator), nameof(KnowledgeGroup.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        public async Task<bool> ValidateCode(KnowledgeGroup KnowledgeGroup)
        {
            if (string.IsNullOrWhiteSpace(KnowledgeGroup.Code))
            {
                KnowledgeGroup.AddError(nameof(KnowledgeGroupValidator), nameof(KnowledgeGroup.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = KnowledgeGroup.Code;
                if (KnowledgeGroup.Code.Contains(" ") || !FilterExtension.ChangeToEnglishChar(Code).Equals(KnowledgeGroup.Code))
                {
                    KnowledgeGroup.AddError(nameof(KnowledgeGroupValidator), nameof(KnowledgeGroup.Code), ErrorCode.CodeHasSpecialCharacter);
                }

                KnowledgeGroupFilter KnowledgeGroupFilter = new KnowledgeGroupFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { NotEqual = KnowledgeGroup.Id },
                    Code = new StringFilter { Equal = KnowledgeGroup.Code },
                    Selects = KnowledgeGroupSelect.Code
                };

                int count = await UOW.KnowledgeGroupRepository.Count(KnowledgeGroupFilter);
                if (count != 0)
                    KnowledgeGroup.AddError(nameof(KnowledgeGroupValidator), nameof(KnowledgeGroup.Code), ErrorCode.CodeExisted);
            }
            return KnowledgeGroup.IsValidated;
        }
        public async Task<bool> ValidateName(KnowledgeGroup KnowledgeGroup)
        {
            if (string.IsNullOrWhiteSpace(KnowledgeGroup.Name))
            {
                KnowledgeGroup.AddError(nameof(KnowledgeGroupValidator), nameof(KnowledgeGroup.Name), ErrorCode.NameEmpty);
            }
            else if (KnowledgeGroup.Name.Length > 255)
            {
                KnowledgeGroup.AddError(nameof(KnowledgeGroupValidator), nameof(KnowledgeGroup.Name), ErrorCode.NameOverLength);
            }
            return KnowledgeGroup.IsValidated;
        }
        public async Task<bool> ValidateStatus(KnowledgeGroup KnowledgeGroup)
        {
            if (StatusEnum.ACTIVE.Id != KnowledgeGroup.StatusId && StatusEnum.INACTIVE.Id != KnowledgeGroup.StatusId)
                KnowledgeGroup.AddError(nameof(KnowledgeGroupValidator), nameof(KnowledgeGroup.Status), ErrorCode.StatusNotExisted);
            return KnowledgeGroup.IsValidated;
        }

        public async Task<bool>Create(KnowledgeGroup KnowledgeGroup)
        {
            await ValidateCode(KnowledgeGroup);
            await ValidateName(KnowledgeGroup);
            await ValidateStatus(KnowledgeGroup);
            return KnowledgeGroup.IsValidated;
        }

        public async Task<bool> Update(KnowledgeGroup KnowledgeGroup)
        {
            if (await ValidateId(KnowledgeGroup))
            {
                await ValidateCode(KnowledgeGroup);
                await ValidateName(KnowledgeGroup);
                await ValidateStatus(KnowledgeGroup);
            }
            return KnowledgeGroup.IsValidated;
        }

        public async Task<bool> Delete(KnowledgeGroup KnowledgeGroup)
        {
            if (await ValidateId(KnowledgeGroup))
            {
            }
            return KnowledgeGroup.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<KnowledgeGroup> KnowledgeGroups)
        {
            foreach (KnowledgeGroup KnowledgeGroup in KnowledgeGroups)
            {
                await Delete(KnowledgeGroup);
            }
            return KnowledgeGroups.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<KnowledgeGroup> KnowledgeGroups)
        {
            return true;
        }
    }
}
