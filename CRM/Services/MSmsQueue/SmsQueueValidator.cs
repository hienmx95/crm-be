using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSmsQueue
{
    public interface ISmsQueueValidator : IServiceScoped
    {
        Task<bool> Create(SmsQueue SmsQueue);
        Task<bool> Update(SmsQueue SmsQueue);
        Task<bool> Delete(SmsQueue SmsQueue);
        Task<bool> BulkDelete(List<SmsQueue> SmsQueues);
        Task<bool> Import(List<SmsQueue> SmsQueues);
    }

    public class SmsQueueValidator : ISmsQueueValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SmsQueueValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SmsQueue SmsQueue)
        {
            SmsQueueFilter SmsQueueFilter = new SmsQueueFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SmsQueue.Id },
                Selects = SmsQueueSelect.Id
            };

            int count = await UOW.SmsQueueRepository.Count(SmsQueueFilter);
            if (count == 0)
                SmsQueue.AddError(nameof(SmsQueueValidator), nameof(SmsQueue.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SmsQueue SmsQueue)
        {
            return SmsQueue.IsValidated;
        }

        public async Task<bool> Update(SmsQueue SmsQueue)
        {
            if (await ValidateId(SmsQueue))
            {
            }
            return SmsQueue.IsValidated;
        }

        public async Task<bool> Delete(SmsQueue SmsQueue)
        {
            if (await ValidateId(SmsQueue))
            {
            }
            return SmsQueue.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SmsQueue> SmsQueues)
        {
            foreach (SmsQueue SmsQueue in SmsQueues)
            {
                await Delete(SmsQueue);
            }
            return SmsQueues.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SmsQueue> SmsQueues)
        {
            return true;
        }
    }
}
