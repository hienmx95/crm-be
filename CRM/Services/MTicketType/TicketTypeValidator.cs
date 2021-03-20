using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MTicketType
{
    public interface ITicketTypeValidator : IServiceScoped
    {
        Task<bool> Create(TicketType TicketType);
        Task<bool> Update(TicketType TicketType);
        Task<bool> Delete(TicketType TicketType);
        Task<bool> BulkDelete(List<TicketType> TicketTypes);
        Task<bool> Import(List<TicketType> TicketTypes);
    }

    public class TicketTypeValidator : ITicketTypeValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CodeEmpty,
            CodeExisted,
            CodeHasSpecialCharacter,
            NameEmpty,
            NameOverLength,
            ColorCodeEmpty,
            ColorCodeOverLength,
            StatusNotExisted
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public TicketTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(TicketType TicketType)
        {
            TicketTypeFilter TicketTypeFilter = new TicketTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = TicketType.Id },
                Selects = TicketTypeSelect.Id
            };

            int count = await UOW.TicketTypeRepository.Count(TicketTypeFilter);
            if (count == 0)
                TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateCode(TicketType TicketType)
        {
            if (string.IsNullOrWhiteSpace(TicketType.Code))
            {
                TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = TicketType.Code;
                if (TicketType.Code.Contains(" ") || !FilterExtension.ChangeToEnglishChar(Code).Equals(TicketType.Code))
                {
                    TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.Code), ErrorCode.CodeHasSpecialCharacter);
                }

                TicketTypeFilter TicketTypeFilter = new TicketTypeFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { NotEqual = TicketType.Id },
                    Code = new StringFilter { Equal = TicketType.Code },
                    Selects = TicketTypeSelect.Code
                };

                int count = await UOW.TicketTypeRepository.Count(TicketTypeFilter);
                if (count != 0)
                    TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.Code), ErrorCode.CodeExisted);
            }
            return TicketType.IsValidated;
        }

        public async Task<bool> ValidateName(TicketType TicketType)
        {
            if (string.IsNullOrWhiteSpace(TicketType.Name))
            {
                TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.Name), ErrorCode.NameEmpty);
            }
            else if (TicketType.Name.Length > 255)
            {
                TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.Name), ErrorCode.NameOverLength);
            }
            return TicketType.IsValidated;
        }

        public async Task<bool> ValidateColorCode(TicketType TicketType)
        {
            if (string.IsNullOrWhiteSpace(TicketType.ColorCode))
            {
                TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.ColorCode), ErrorCode.ColorCodeEmpty);
            }
            else if (TicketType.ColorCode.Length > 20)
            {
                TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.ColorCode), ErrorCode.ColorCodeOverLength);
            }
            return TicketType.IsValidated;
        }

        public async Task<bool> ValidateStatus(TicketType TicketType)
        {
            if (StatusEnum.ACTIVE.Id != TicketType.StatusId && StatusEnum.INACTIVE.Id != TicketType.StatusId)
                TicketType.AddError(nameof(TicketTypeValidator), nameof(TicketType.Status), ErrorCode.StatusNotExisted);
            return TicketType.IsValidated;
        }

        public async Task<bool>Create(TicketType TicketType)
        {
            await ValidateCode(TicketType);
            await ValidateName(TicketType);
            await ValidateColorCode(TicketType);
            await ValidateStatus(TicketType);
            return TicketType.IsValidated;
        }

        public async Task<bool> Update(TicketType TicketType)
        {
            if (await ValidateId(TicketType))
            {
                await ValidateCode(TicketType);
                await ValidateName(TicketType);
                await ValidateColorCode(TicketType);
                await ValidateStatus(TicketType);
            }
            return TicketType.IsValidated;
        }

        public async Task<bool> Delete(TicketType TicketType)
        {
            if (await ValidateId(TicketType))
            {
            }
            return TicketType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<TicketType> TicketTypes)
        {
            return true;
        }
        
        public async Task<bool> Import(List<TicketType> TicketTypes)
        {
            return true;
        }
    }
}
