using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MKMSStatus
{
    public interface IKMSStatusValidator : IServiceScoped
    {
        Task<bool> Create(KMSStatus KMSStatus);
        Task<bool> Update(KMSStatus KMSStatus);
        Task<bool> Delete(KMSStatus KMSStatus);
        Task<bool> BulkDelete(List<KMSStatus> KMSStatuses);
        Task<bool> Import(List<KMSStatus> KMSStatuses);
    }

    public class KMSStatusValidator : IKMSStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public KMSStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(KMSStatus KMSStatus)
        {
            KMSStatusFilter KMSStatusFilter = new KMSStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = KMSStatus.Id },
                Selects = KMSStatusSelect.Id
            };

            int count = await UOW.KMSStatusRepository.Count(KMSStatusFilter);
            if (count == 0)
                KMSStatus.AddError(nameof(KMSStatusValidator), nameof(KMSStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(KMSStatus KMSStatus)
        {
            return KMSStatus.IsValidated;
        }

        public async Task<bool> Update(KMSStatus KMSStatus)
        {
            if (await ValidateId(KMSStatus))
            {
            }
            return KMSStatus.IsValidated;
        }

        public async Task<bool> Delete(KMSStatus KMSStatus)
        {
            if (await ValidateId(KMSStatus))
            {
            }
            return KMSStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<KMSStatus> KMSStatuses)
        {
            foreach (KMSStatus KMSStatus in KMSStatuses)
            {
                await Delete(KMSStatus);
            }
            return KMSStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<KMSStatus> KMSStatuses)
        {
            return true;
        }
    }
}
