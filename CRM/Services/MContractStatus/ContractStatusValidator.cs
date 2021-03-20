using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MContractStatus
{
    public interface IContractStatusValidator : IServiceScoped
    {
        Task<bool> Create(ContractStatus ContractStatus);
        Task<bool> Update(ContractStatus ContractStatus);
        Task<bool> Delete(ContractStatus ContractStatus);
        Task<bool> BulkDelete(List<ContractStatus> ContractStatuses);
        Task<bool> Import(List<ContractStatus> ContractStatuses);
    }

    public class ContractStatusValidator : IContractStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ContractStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ContractStatus ContractStatus)
        {
            ContractStatusFilter ContractStatusFilter = new ContractStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ContractStatus.Id },
                Selects = ContractStatusSelect.Id
            };

            int count = await UOW.ContractStatusRepository.Count(ContractStatusFilter);
            if (count == 0)
                ContractStatus.AddError(nameof(ContractStatusValidator), nameof(ContractStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(ContractStatus ContractStatus)
        {
            return ContractStatus.IsValidated;
        }

        public async Task<bool> Update(ContractStatus ContractStatus)
        {
            if (await ValidateId(ContractStatus))
            {
            }
            return ContractStatus.IsValidated;
        }

        public async Task<bool> Delete(ContractStatus ContractStatus)
        {
            if (await ValidateId(ContractStatus))
            {
            }
            return ContractStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ContractStatus> ContractStatuses)
        {
            foreach (ContractStatus ContractStatus in ContractStatuses)
            {
                await Delete(ContractStatus);
            }
            return ContractStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<ContractStatus> ContractStatuses)
        {
            return true;
        }
    }
}
