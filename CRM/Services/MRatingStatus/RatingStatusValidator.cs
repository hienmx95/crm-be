using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MRatingStatus
{
    public interface IRatingStatusValidator : IServiceScoped
    {
        Task<bool> Create(RatingStatus RatingStatus);
        Task<bool> Update(RatingStatus RatingStatus);
        Task<bool> Delete(RatingStatus RatingStatus);
        Task<bool> BulkDelete(List<RatingStatus> RatingStatuses);
        Task<bool> Import(List<RatingStatus> RatingStatuses);
    }

    public class RatingStatusValidator : IRatingStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public RatingStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(RatingStatus RatingStatus)
        {
            RatingStatusFilter RatingStatusFilter = new RatingStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = RatingStatus.Id },
                Selects = RatingStatusSelect.Id
            };

            int count = await UOW.RatingStatusRepository.Count(RatingStatusFilter);
            if (count == 0)
                RatingStatus.AddError(nameof(RatingStatusValidator), nameof(RatingStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(RatingStatus RatingStatus)
        {
            return RatingStatus.IsValidated;
        }

        public async Task<bool> Update(RatingStatus RatingStatus)
        {
            if (await ValidateId(RatingStatus))
            {
            }
            return RatingStatus.IsValidated;
        }

        public async Task<bool> Delete(RatingStatus RatingStatus)
        {
            if (await ValidateId(RatingStatus))
            {
            }
            return RatingStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<RatingStatus> RatingStatuses)
        {
            foreach (RatingStatus RatingStatus in RatingStatuses)
            {
                await Delete(RatingStatus);
            }
            return RatingStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<RatingStatus> RatingStatuses)
        {
            return true;
        }
    }
}
