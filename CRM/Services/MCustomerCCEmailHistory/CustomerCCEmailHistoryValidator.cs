using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerCCEmailHistory
{
    public interface ICustomerCCEmailHistoryValidator : IServiceScoped
    {
        Task<bool> Create(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<bool> Update(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<bool> Delete(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<bool> BulkDelete(List<CustomerCCEmailHistory> CustomerCCEmailHistories);
        Task<bool> Import(List<CustomerCCEmailHistory> CustomerCCEmailHistories);
    }

    public class CustomerCCEmailHistoryValidator : ICustomerCCEmailHistoryValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerCCEmailHistoryValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter = new CustomerCCEmailHistoryFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerCCEmailHistory.Id },
                Selects = CustomerCCEmailHistorySelect.Id
            };

            int count = await UOW.CustomerCCEmailHistoryRepository.Count(CustomerCCEmailHistoryFilter);
            if (count == 0)
                CustomerCCEmailHistory.AddError(nameof(CustomerCCEmailHistoryValidator), nameof(CustomerCCEmailHistory.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            return CustomerCCEmailHistory.IsValidated;
        }

        public async Task<bool> Update(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            if (await ValidateId(CustomerCCEmailHistory))
            {
            }
            return CustomerCCEmailHistory.IsValidated;
        }

        public async Task<bool> Delete(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            if (await ValidateId(CustomerCCEmailHistory))
            {
            }
            return CustomerCCEmailHistory.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerCCEmailHistory> CustomerCCEmailHistories)
        {
            foreach (CustomerCCEmailHistory CustomerCCEmailHistory in CustomerCCEmailHistories)
            {
                await Delete(CustomerCCEmailHistory);
            }
            return CustomerCCEmailHistories.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerCCEmailHistory> CustomerCCEmailHistories)
        {
            return true;
        }
    }
}
