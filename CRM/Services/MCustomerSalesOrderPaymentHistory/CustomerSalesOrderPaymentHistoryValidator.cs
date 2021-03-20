using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerSalesOrderPaymentHistory
{
    public interface ICustomerSalesOrderPaymentHistoryValidator : IServiceScoped
    {
        Task<bool> Create(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory);
        Task<bool> Update(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory);
        Task<bool> Delete(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory);
        Task<bool> BulkDelete(List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories);
        Task<bool> Import(List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories);
    }

    public class CustomerSalesOrderPaymentHistoryValidator : ICustomerSalesOrderPaymentHistoryValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerSalesOrderPaymentHistoryValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
            CustomerSalesOrderPaymentHistoryFilter CustomerSalesOrderPaymentHistoryFilter = new CustomerSalesOrderPaymentHistoryFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerSalesOrderPaymentHistory.Id },
                Selects = CustomerSalesOrderPaymentHistorySelect.Id
            };

            int count = await UOW.CustomerSalesOrderPaymentHistoryRepository.Count(CustomerSalesOrderPaymentHistoryFilter);
            if (count == 0)
                CustomerSalesOrderPaymentHistory.AddError(nameof(CustomerSalesOrderPaymentHistoryValidator), nameof(CustomerSalesOrderPaymentHistory.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
            return CustomerSalesOrderPaymentHistory.IsValidated;
        }

        public async Task<bool> Update(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
            if (await ValidateId(CustomerSalesOrderPaymentHistory))
            {
            }
            return CustomerSalesOrderPaymentHistory.IsValidated;
        }

        public async Task<bool> Delete(CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory)
        {
            if (await ValidateId(CustomerSalesOrderPaymentHistory))
            {
            }
            return CustomerSalesOrderPaymentHistory.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories)
        {
            foreach (CustomerSalesOrderPaymentHistory CustomerSalesOrderPaymentHistory in CustomerSalesOrderPaymentHistories)
            {
                await Delete(CustomerSalesOrderPaymentHistory);
            }
            return CustomerSalesOrderPaymentHistories.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories)
        {
            return true;
        }
    }
}
