using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;
using CRM.Helpers;

namespace CRM.Services.MCustomerLeadActivity
{
    public interface ICustomerLeadActivityValidator : IServiceScoped
    {
        Task<bool> Create(CustomerLeadActivity CustomerLeadActivity);
        Task<bool> Update(CustomerLeadActivity CustomerLeadActivity);
        Task<bool> Delete(CustomerLeadActivity CustomerLeadActivity);
        Task<bool> BulkDelete(List<CustomerLeadActivity> CustomerLeadActivities);
        Task<bool> Import(List<CustomerLeadActivity> CustomerLeadActivities);
    }

    public class CustomerLeadActivityValidator : ICustomerLeadActivityValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            FromDateEmpty,
            ToDateInvalid
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerLeadActivityValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerLeadActivity CustomerLeadActivity)
        {
            CustomerLeadActivityFilter CustomerLeadActivityFilter = new CustomerLeadActivityFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerLeadActivity.Id },
                Selects = CustomerLeadActivitySelect.Id
            };

            int count = await UOW.CustomerLeadActivityRepository.Count(CustomerLeadActivityFilter);
            if (count == 0)
                CustomerLeadActivity.AddError(nameof(CustomerLeadActivityValidator), nameof(CustomerLeadActivity.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateDate(CustomerLeadActivity CustomerLeadActivity)
        {
            if (CustomerLeadActivity.FromDate == DateTime.MinValue)
            {
                CustomerLeadActivity.AddError(nameof(CustomerLeadActivityValidator), nameof(CustomerLeadActivity.FromDate), ErrorCode.FromDateEmpty);
            }
            else
            {
                if (CustomerLeadActivity.ToDate.HasValue)
                {
                    if (CustomerLeadActivity.ToDate.Value <= CustomerLeadActivity.FromDate)
                    {
                        CustomerLeadActivity.AddError(nameof(CustomerLeadActivityValidator), nameof(CustomerLeadActivity.ToDate), ErrorCode.ToDateInvalid);
                    }
                }
            }
            return CustomerLeadActivity.IsValidated;
        }

        public async Task<bool>Create(CustomerLeadActivity CustomerLeadActivity)
        {
            await ValidateDate(CustomerLeadActivity);
            return CustomerLeadActivity.IsValidated;
        }

        public async Task<bool> Update(CustomerLeadActivity CustomerLeadActivity)
        {
            if (await ValidateId(CustomerLeadActivity))
            {
                await ValidateDate(CustomerLeadActivity);
            }
            return CustomerLeadActivity.IsValidated;
        }

        public async Task<bool> Delete(CustomerLeadActivity CustomerLeadActivity)
        {
            if (await ValidateId(CustomerLeadActivity))
            {
            }
            return CustomerLeadActivity.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerLeadActivity> CustomerLeadActivities)
        {
            foreach (CustomerLeadActivity CustomerLeadActivity in CustomerLeadActivities)
            {
                await Delete(CustomerLeadActivity);
            }
            return CustomerLeadActivities.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerLeadActivity> CustomerLeadActivities)
        {
            return true;
        }
    }
}
