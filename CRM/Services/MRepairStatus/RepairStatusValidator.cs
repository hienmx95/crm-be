using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MRepairStatus
{
    public interface IRepairStatusValidator : IServiceScoped
    {
        Task<bool> Create(RepairStatus RepairStatus);
        Task<bool> Update(RepairStatus RepairStatus);
        Task<bool> Delete(RepairStatus RepairStatus);
        Task<bool> BulkDelete(List<RepairStatus> RepairStatuses);
        Task<bool> Import(List<RepairStatus> RepairStatuses);
    }

    public class RepairStatusValidator : IRepairStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public RepairStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(RepairStatus RepairStatus)
        {
            RepairStatusFilter RepairStatusFilter = new RepairStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = RepairStatus.Id },
                Selects = RepairStatusSelect.Id
            };

            int count = await UOW.RepairStatusRepository.Count(RepairStatusFilter);
            if (count == 0)
                RepairStatus.AddError(nameof(RepairStatusValidator), nameof(RepairStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(RepairStatus RepairStatus)
        {
            return RepairStatus.IsValidated;
        }

        public async Task<bool> Update(RepairStatus RepairStatus)
        {
            if (await ValidateId(RepairStatus))
            {
            }
            return RepairStatus.IsValidated;
        }

        public async Task<bool> Delete(RepairStatus RepairStatus)
        {
            if (await ValidateId(RepairStatus))
            {
            }
            return RepairStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<RepairStatus> RepairStatuses)
        {
            foreach (RepairStatus RepairStatus in RepairStatuses)
            {
                await Delete(RepairStatus);
            }
            return RepairStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<RepairStatus> RepairStatuses)
        {
            return true;
        }
    }
}
