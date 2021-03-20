using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerGrouping
{
    public interface ICustomerGroupingValidator : IServiceScoped
    {
        Task<bool> Create(CustomerGrouping CustomerGrouping);
        Task<bool> Update(CustomerGrouping CustomerGrouping);
        Task<bool> Delete(CustomerGrouping CustomerGrouping);
        Task<bool> BulkDelete(List<CustomerGrouping> CustomerGroupings);
        Task<bool> Import(List<CustomerGrouping> CustomerGroupings);
    }

    public class CustomerGroupingValidator : ICustomerGroupingValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerGroupingValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerGrouping.Id },
                Selects = CustomerGroupingSelect.Id
            };

            int count = await UOW.CustomerGroupingRepository.Count(CustomerGroupingFilter);
            if (count == 0)
                CustomerGrouping.AddError(nameof(CustomerGroupingValidator), nameof(CustomerGrouping.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerGrouping CustomerGrouping)
        {
            return CustomerGrouping.IsValidated;
        }

        public async Task<bool> Update(CustomerGrouping CustomerGrouping)
        {
            if (await ValidateId(CustomerGrouping))
            {
            }
            return CustomerGrouping.IsValidated;
        }

        public async Task<bool> Delete(CustomerGrouping CustomerGrouping)
        {
            if (await ValidateId(CustomerGrouping))
            {
            }
            return CustomerGrouping.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerGrouping> CustomerGroupings)
        {
            foreach (CustomerGrouping CustomerGrouping in CustomerGroupings)
            {
                await Delete(CustomerGrouping);
            }
            return CustomerGroupings.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerGrouping> CustomerGroupings)
        {
            return true;
        }
    }
}
