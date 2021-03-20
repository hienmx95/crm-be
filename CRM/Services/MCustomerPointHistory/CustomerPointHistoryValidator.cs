using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerPointHistory
{
    public interface ICustomerPointHistoryValidator : IServiceScoped
    {
        Task<bool> Create(CustomerPointHistory CustomerPointHistory);
        Task<bool> Update(CustomerPointHistory CustomerPointHistory);
        Task<bool> Delete(CustomerPointHistory CustomerPointHistory);
        Task<bool> BulkDelete(List<CustomerPointHistory> CustomerPointHistories);
        Task<bool> Import(List<CustomerPointHistory> CustomerPointHistories);
    }

    public class CustomerPointHistoryValidator : ICustomerPointHistoryValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            ChangePointEmpty,
            DescriptionEmpty
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerPointHistoryValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerPointHistory CustomerPointHistory)
        {
            CustomerPointHistoryFilter CustomerPointHistoryFilter = new CustomerPointHistoryFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerPointHistory.Id },
                Selects = CustomerPointHistorySelect.Id
            };

            int count = await UOW.CustomerPointHistoryRepository.Count(CustomerPointHistoryFilter);
            if (count == 0)
                CustomerPointHistory.AddError(nameof(CustomerPointHistoryValidator), nameof(CustomerPointHistory.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidatePoint(CustomerPointHistory CustomerPointHistory)
        {
            if(CustomerPointHistory.ChangePoint <= 0)
            {
                CustomerPointHistory.AddError(nameof(CustomerPointHistoryValidator), nameof(CustomerPointHistory.ChangePoint), ErrorCode.ChangePointEmpty);
            }
            return CustomerPointHistory.IsValidated;
        }

        private async Task<bool> ValidateNote(CustomerPointHistory CustomerPointHistory)
        {
            if (string.IsNullOrWhiteSpace(CustomerPointHistory.Description))
            {
                CustomerPointHistory.AddError(nameof(CustomerPointHistoryValidator), nameof(CustomerPointHistory.Description), ErrorCode.DescriptionEmpty);
            }
            return CustomerPointHistory.IsValidated;
        }

        public async Task<bool>Create(CustomerPointHistory CustomerPointHistory)
        {
            await ValidatePoint(CustomerPointHistory);
            await ValidateNote(CustomerPointHistory);
            return CustomerPointHistory.IsValidated;
        }

        public async Task<bool> Update(CustomerPointHistory CustomerPointHistory)
        {
            if (await ValidateId(CustomerPointHistory))
            {
            }
            return CustomerPointHistory.IsValidated;
        }

        public async Task<bool> Delete(CustomerPointHistory CustomerPointHistory)
        {
            if (await ValidateId(CustomerPointHistory))
            {
            }
            return CustomerPointHistory.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerPointHistory> CustomerPointHistories)
        {
            foreach (CustomerPointHistory CustomerPointHistory in CustomerPointHistories)
            {
                await Delete(CustomerPointHistory);
            }
            return CustomerPointHistories.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerPointHistory> CustomerPointHistories)
        {
            return true;
        }
    }
}
