using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MSLAStatus
{
    public interface ISLAStatusValidator : IServiceScoped
    {
        Task<bool> Create(SLAStatus SLAStatus);
        Task<bool> Update(SLAStatus SLAStatus);
        Task<bool> Delete(SLAStatus SLAStatus);
        Task<bool> BulkDelete(List<SLAStatus> SLAStatuses);
        Task<bool> Import(List<SLAStatus> SLAStatuses);
    }

    public class SLAStatusValidator : ISLAStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public SLAStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(SLAStatus SLAStatus)
        {
            SLAStatusFilter SLAStatusFilter = new SLAStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = SLAStatus.Id },
                Selects = SLAStatusSelect.Id
            };

            int count = await UOW.SLAStatusRepository.Count(SLAStatusFilter);
            if (count == 0)
                SLAStatus.AddError(nameof(SLAStatusValidator), nameof(SLAStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(SLAStatus SLAStatus)
        {
            return SLAStatus.IsValidated;
        }

        public async Task<bool> Update(SLAStatus SLAStatus)
        {
            if (await ValidateId(SLAStatus))
            {
            }
            return SLAStatus.IsValidated;
        }

        public async Task<bool> Delete(SLAStatus SLAStatus)
        {
            if (await ValidateId(SLAStatus))
            {
            }
            return SLAStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<SLAStatus> SLAStatuses)
        {
            foreach (SLAStatus SLAStatus in SLAStatuses)
            {
                await Delete(SLAStatus);
            }
            return SLAStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<SLAStatus> SLAStatuses)
        {
            return true;
        }
    }
}
