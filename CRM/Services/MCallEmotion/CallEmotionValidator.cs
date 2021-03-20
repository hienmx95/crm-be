using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCallEmotion
{
    public interface ICallEmotionValidator : IServiceScoped
    {
        Task<bool> Create(CallEmotion CallEmotion);
        Task<bool> Update(CallEmotion CallEmotion);
        Task<bool> Delete(CallEmotion CallEmotion);
        Task<bool> BulkDelete(List<CallEmotion> CallEmotions);
        Task<bool> Import(List<CallEmotion> CallEmotions);
    }

    public class CallEmotionValidator : ICallEmotionValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CallEmotionValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CallEmotion CallEmotion)
        {
            CallEmotionFilter CallEmotionFilter = new CallEmotionFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CallEmotion.Id },
                Selects = CallEmotionSelect.Id
            };

            int count = await UOW.CallEmotionRepository.Count(CallEmotionFilter);
            if (count == 0)
                CallEmotion.AddError(nameof(CallEmotionValidator), nameof(CallEmotion.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CallEmotion CallEmotion)
        {
            return CallEmotion.IsValidated;
        }

        public async Task<bool> Update(CallEmotion CallEmotion)
        {
            if (await ValidateId(CallEmotion))
            {
            }
            return CallEmotion.IsValidated;
        }

        public async Task<bool> Delete(CallEmotion CallEmotion)
        {
            if (await ValidateId(CallEmotion))
            {
            }
            return CallEmotion.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CallEmotion> CallEmotions)
        {
            foreach (CallEmotion CallEmotion in CallEmotions)
            {
                await Delete(CallEmotion);
            }
            return CallEmotions.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CallEmotion> CallEmotions)
        {
            return true;
        }
    }
}
