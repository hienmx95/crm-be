using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MScheduleMaster
{
    public interface IScheduleMasterValidator : IServiceScoped
    {
        Task<bool> Create(ScheduleMaster ScheduleMaster);
        Task<bool> Update(ScheduleMaster ScheduleMaster);
        Task<bool> Delete(ScheduleMaster ScheduleMaster);
        Task<bool> BulkDelete(List<ScheduleMaster> ScheduleMasters);
        Task<bool> Import(List<ScheduleMaster> ScheduleMasters);
    }

    public class ScheduleMasterValidator : IScheduleMasterValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ScheduleMasterValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ScheduleMaster ScheduleMaster)
        {
            ScheduleMasterFilter ScheduleMasterFilter = new ScheduleMasterFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ScheduleMaster.Id },
                Selects = ScheduleMasterSelect.Id
            };

            int count = await UOW.ScheduleMasterRepository.Count(ScheduleMasterFilter);
            if (count == 0)
                ScheduleMaster.AddError(nameof(ScheduleMasterValidator), nameof(ScheduleMaster.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(ScheduleMaster ScheduleMaster)
        {
            return ScheduleMaster.IsValidated;
        }

        public async Task<bool> Update(ScheduleMaster ScheduleMaster)
        {
            if (await ValidateId(ScheduleMaster))
            {
            }
            return ScheduleMaster.IsValidated;
        }

        public async Task<bool> Delete(ScheduleMaster ScheduleMaster)
        {
            if (await ValidateId(ScheduleMaster))
            {
            }
            return ScheduleMaster.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ScheduleMaster> ScheduleMasters)
        {
            return true;
        }
        
        public async Task<bool> Import(List<ScheduleMaster> ScheduleMasters)
        {
            return true;
        }
    }
}
