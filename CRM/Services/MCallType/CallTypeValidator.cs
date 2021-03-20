using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Enums;

namespace CRM.Services.MCallType
{
    public interface ICallTypeValidator : IServiceScoped
    {
        Task<bool> Create(CallType CallType);
        Task<bool> Update(CallType CallType);
        Task<bool> Delete(CallType CallType);
        Task<bool> BulkDelete(List<CallType> CallTypes);
        Task<bool> Import(List<CallType> CallTypes);
    }

    public class CallTypeValidator : ICallTypeValidator
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

        public CallTypeValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CallType CallType)
        {
            CallTypeFilter CallTypeFilter = new CallTypeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CallType.Id },
                Selects = CallTypeSelect.Id
            };

            int count = await UOW.CallTypeRepository.Count(CallTypeFilter);
            if (count == 0)
                CallType.AddError(nameof(CallTypeValidator), nameof(CallType.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool> ValidateCode(CallType CallType)
        {
            if (string.IsNullOrWhiteSpace(CallType.Code))
            {
                CallType.AddError(nameof(CallTypeValidator), nameof(CallType.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = CallType.Code;
                if (CallType.Code.Contains(" ") || !FilterExtension.ChangeToEnglishChar(Code).Equals(CallType.Code))
                {
                    CallType.AddError(nameof(CallTypeValidator), nameof(CallType.Code), ErrorCode.CodeHasSpecialCharacter);
                }

                CallTypeFilter CallTypeFilter = new CallTypeFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { NotEqual = CallType.Id },
                    Code = new StringFilter { Equal = CallType.Code },
                    Selects = CallTypeSelect.Code
                };

                int count = await UOW.CallTypeRepository.Count(CallTypeFilter);
                if (count != 0)
                    CallType.AddError(nameof(CallTypeValidator), nameof(CallType.Code), ErrorCode.CodeExisted);
            }
            return CallType.IsValidated;
        }

        public async Task<bool> ValidateName(CallType CallType)
        {
            if (string.IsNullOrWhiteSpace(CallType.Name))
            {
                CallType.AddError(nameof(CallTypeValidator), nameof(CallType.Name), ErrorCode.NameEmpty);
            }
            else if (CallType.Name.Length > 255)
            {
                CallType.AddError(nameof(CallTypeValidator), nameof(CallType.Name), ErrorCode.NameOverLength);
            }
            return CallType.IsValidated;
        }

        public async Task<bool> ValidateColorCode(CallType CallType)
        {
            if (string.IsNullOrWhiteSpace(CallType.ColorCode))
            {
                CallType.AddError(nameof(CallTypeValidator), nameof(CallType.ColorCode), ErrorCode.ColorCodeEmpty);
            }
            else if (CallType.ColorCode.Length > 20)
            {
                CallType.AddError(nameof(CallTypeValidator), nameof(CallType.ColorCode), ErrorCode.ColorCodeOverLength);
            }
            return CallType.IsValidated;
        }

        public async Task<bool> ValidateStatus(CallType CallType)
        {
            if (StatusEnum.ACTIVE.Id != CallType.StatusId && StatusEnum.INACTIVE.Id != CallType.StatusId)
                CallType.AddError(nameof(CallTypeValidator), nameof(CallType.Status), ErrorCode.StatusNotExisted);
            return CallType.IsValidated;
        }

        public async Task<bool>Create(CallType CallType)
        {
            await ValidateCode(CallType);
            await ValidateName(CallType);
            await ValidateColorCode(CallType);
            await ValidateStatus(CallType);
            return CallType.IsValidated;
        }

        public async Task<bool> Update(CallType CallType)
        {
            if (await ValidateId(CallType))
            {
                await ValidateCode(CallType);
                await ValidateName(CallType);
                await ValidateColorCode(CallType);
                await ValidateStatus(CallType);
            }
            return CallType.IsValidated;
        }

        public async Task<bool> Delete(CallType CallType)
        {
            if (await ValidateId(CallType))
            {
            }
            return CallType.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CallType> CallTypes)
        {
            foreach (CallType CallType in CallTypes)
            {
                await Delete(CallType);
            }
            return CallTypes.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CallType> CallTypes)
        {
            return true;
        }
    }
}
