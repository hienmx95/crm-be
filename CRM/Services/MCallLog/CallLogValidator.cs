using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCallLog
{
    public interface ICallLogValidator : IServiceScoped
    {
        Task<bool> Create(CallLog CallLog);
        Task<bool> Update(CallLog CallLog);
        Task<bool> Delete(CallLog CallLog);
        Task<bool> BulkDelete(List<CallLog> CallLogs);
        Task<bool> Import(List<CallLog> CallLogs);
    }

    public class CallLogValidator : ICallLogValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CustomerEmpty,
            CallTypeEmpty,
            ContentEmpty,
            ContentOverLength,
            AppUserEmpty
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CallLogValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CallLog CallLog)
        {
            CallLogFilter CallLogFilter = new CallLogFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CallLog.Id },
                Selects = CallLogSelect.Id
            };

            int count = await UOW.CallLogRepository.Count(CallLogFilter);
            if (count == 0)
                CallLog.AddError(nameof(CallLogValidator), nameof(CallLog.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        public async Task<bool> ValidateContent(CallLog CallLog)
        {
            if (string.IsNullOrWhiteSpace(CallLog.Content))
            {
                CallLog.AddError(nameof(CallLogValidator), nameof(CallLog.Content), ErrorCode.ContentEmpty);
            }
            else if (CallLog.Content.Length > 255)
            {
                CallLog.AddError(nameof(CallLogValidator), nameof(CallLog.Content), ErrorCode.ContentOverLength);
            }
            return CallLog.IsValidated;
        }
        public async Task<bool> ValidateCallType(CallLog CallLog)
        {
            if (CallLog.CallType == null)
            {
                CallLog.AddError(nameof(CallLogValidator), nameof(CallLog.CallType), ErrorCode.CallTypeEmpty);
            }
            return CallLog.IsValidated;
        }
        public async Task<bool> ValidateAppUser(CallLog CallLog)
        {
            if (CallLog.AppUser == null)
            {
                CallLog.AddError(nameof(CallLogValidator), nameof(CallLog.AppUser), ErrorCode.AppUserEmpty);
            }
            return CallLog.IsValidated;
        }

        public async Task<bool> Create(CallLog CallLog)
        {
            await ValidateContent(CallLog);
            await ValidateCallType(CallLog);
            await ValidateAppUser(CallLog);
            return CallLog.IsValidated;
        }

        public async Task<bool> Update(CallLog CallLog)
        {
            if (await ValidateId(CallLog))
            {
                await ValidateContent(CallLog);
                await ValidateCallType(CallLog);
                await ValidateAppUser(CallLog);
            }
            return CallLog.IsValidated;
        }

        public async Task<bool> Delete(CallLog CallLog)
        {
            if (await ValidateId(CallLog))
            {
            }
            return CallLog.IsValidated;
        }

        public async Task<bool> BulkDelete(List<CallLog> CallLogs)
        {
            foreach (CallLog CallLog in CallLogs)
            {
                await Delete(CallLog);
            }
            return CallLogs.All(x => x.IsValidated);
        }

        public async Task<bool> Import(List<CallLog> CallLogs)
        {
            return true;
        }
    }
}
