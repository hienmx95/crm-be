using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerEmailHistory
{
    public interface ICustomerEmailHistoryValidator : IServiceScoped
    {
        Task<bool> Create(CustomerEmailHistory CustomerEmailHistory);
        Task<bool> Update(CustomerEmailHistory CustomerEmailHistory);
        Task<bool> Delete(CustomerEmailHistory CustomerEmailHistory);
        Task<bool> BulkDelete(List<CustomerEmailHistory> CustomerEmailHistories);
        Task<bool> Import(List<CustomerEmailHistory> CustomerEmailHistories);
    }

    public class CustomerEmailHistoryValidator : ICustomerEmailHistoryValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerEmailHistoryValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerEmailHistory CustomerEmailHistory)
        {
            CustomerEmailHistoryFilter CustomerEmailHistoryFilter = new CustomerEmailHistoryFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerEmailHistory.Id },
                Selects = CustomerEmailHistorySelect.Id
            };

            int count = await UOW.CustomerEmailHistoryRepository.Count(CustomerEmailHistoryFilter);
            if (count == 0)
                CustomerEmailHistory.AddError(nameof(CustomerEmailHistoryValidator), nameof(CustomerEmailHistory.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerEmailHistory CustomerEmailHistory)
        {
            return CustomerEmailHistory.IsValidated;
        }

        public async Task<bool> Update(CustomerEmailHistory CustomerEmailHistory)
        {
            if (await ValidateId(CustomerEmailHistory))
            {
            }
            return CustomerEmailHistory.IsValidated;
        }

        public async Task<bool> Delete(CustomerEmailHistory CustomerEmailHistory)
        {
            if (await ValidateId(CustomerEmailHistory))
            {
            }
            return CustomerEmailHistory.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerEmailHistory> CustomerEmailHistories)
        {
            foreach (CustomerEmailHistory CustomerEmailHistory in CustomerEmailHistories)
            {
                await Delete(CustomerEmailHistory);
            }
            return CustomerEmailHistories.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerEmailHistory> CustomerEmailHistories)
        {
            return true;
        }
    }
}
