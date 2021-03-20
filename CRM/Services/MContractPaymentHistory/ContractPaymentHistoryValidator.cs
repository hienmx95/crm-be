using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MContractPaymentHistory
{
    public interface IContractPaymentHistoryValidator : IServiceScoped
    {
        Task<bool> Create(ContractPaymentHistory ContractPaymentHistory);
        Task<bool> Update(ContractPaymentHistory ContractPaymentHistory);
        Task<bool> Delete(ContractPaymentHistory ContractPaymentHistory);
        Task<bool> BulkDelete(List<ContractPaymentHistory> ContractPaymentHistories);
        Task<bool> Import(List<ContractPaymentHistory> ContractPaymentHistories);
    }

    public class ContractPaymentHistoryValidator : IContractPaymentHistoryValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ContractPaymentHistoryValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(ContractPaymentHistory ContractPaymentHistory)
        {
            ContractPaymentHistoryFilter ContractPaymentHistoryFilter = new ContractPaymentHistoryFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ContractPaymentHistory.Id },
                Selects = ContractPaymentHistorySelect.Id
            };

            int count = await UOW.ContractPaymentHistoryRepository.Count(ContractPaymentHistoryFilter);
            if (count == 0)
                ContractPaymentHistory.AddError(nameof(ContractPaymentHistoryValidator), nameof(ContractPaymentHistory.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(ContractPaymentHistory ContractPaymentHistory)
        {
            return ContractPaymentHistory.IsValidated;
        }

        public async Task<bool> Update(ContractPaymentHistory ContractPaymentHistory)
        {
            if (await ValidateId(ContractPaymentHistory))
            {
            }
            return ContractPaymentHistory.IsValidated;
        }

        public async Task<bool> Delete(ContractPaymentHistory ContractPaymentHistory)
        {
            if (await ValidateId(ContractPaymentHistory))
            {
            }
            return ContractPaymentHistory.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<ContractPaymentHistory> ContractPaymentHistories)
        {
            foreach (ContractPaymentHistory ContractPaymentHistory in ContractPaymentHistories)
            {
                await Delete(ContractPaymentHistory);
            }
            return ContractPaymentHistories.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<ContractPaymentHistory> ContractPaymentHistories)
        {
            return true;
        }
    }
}
