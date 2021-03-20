using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSmsQueueStatus
{
    public interface ISmsQueueStatusValidator : IServiceScoped
    {
        Task<bool> Create(SmsQueueStatus SmsQueueStatus);
        Task<bool> Update(SmsQueueStatus SmsQueueStatus);
        Task<bool> Delete(SmsQueueStatus SmsQueueStatus);
        Task<bool> BulkDelete(List<SmsQueueStatus> SmsQueueStatuses);
        Task<bool> Import(List<SmsQueueStatus> SmsQueueStatuses);
    }

    public class SmsQueueStatusValidator : ISmsQueueStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SmsQueueStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SmsQueueStatus SmsQueueStatus)
        {
            SmsQueueStatusFilter SmsQueueStatusFilter = new SmsQueueStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SmsQueueStatus.Id },
                Selects = SmsQueueStatusSelect.Id
            };

            int count = await UOW.SmsQueueStatusRepository.Count(SmsQueueStatusFilter);
            if (count == 0)
                SmsQueueStatus.AddError(nameof(SmsQueueStatusValidator), nameof(SmsQueueStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SmsQueueStatus SmsQueueStatus)
        {
            return SmsQueueStatus.IsValidated;
        }

        public async Task<bool> Update(SmsQueueStatus SmsQueueStatus)
        {
            if (await ValidateId(SmsQueueStatus))
            {
            }
            return SmsQueueStatus.IsValidated;
        }

        public async Task<bool> Delete(SmsQueueStatus SmsQueueStatus)
        {
            if (await ValidateId(SmsQueueStatus))
            {
            }
            return SmsQueueStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SmsQueueStatus> SmsQueueStatuses)
        {
            foreach (SmsQueueStatus SmsQueueStatus in SmsQueueStatuses)
            {
                await Delete(SmsQueueStatus);
            }
            return SmsQueueStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SmsQueueStatus> SmsQueueStatuses)
        {
            return true;
        }
    }
}
