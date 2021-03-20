using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerLeadStatus
{
    public interface ICustomerLeadStatusValidator : IServiceScoped
    {
        Task<bool> Create(CustomerLeadStatus CustomerLeadStatus);
        Task<bool> Update(CustomerLeadStatus CustomerLeadStatus);
        Task<bool> Delete(CustomerLeadStatus CustomerLeadStatus);
        Task<bool> BulkDelete(List<CustomerLeadStatus> CustomerLeadStatuses);
        Task<bool> Import(List<CustomerLeadStatus> CustomerLeadStatuses);
    }

    public class CustomerLeadStatusValidator : ICustomerLeadStatusValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerLeadStatusValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerLeadStatus CustomerLeadStatus)
        {
            CustomerLeadStatusFilter CustomerLeadStatusFilter = new CustomerLeadStatusFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerLeadStatus.Id },
                Selects = CustomerLeadStatusSelect.Id
            };

            int count = await UOW.CustomerLeadStatusRepository.Count(CustomerLeadStatusFilter);
            if (count == 0)
                CustomerLeadStatus.AddError(nameof(CustomerLeadStatusValidator), nameof(CustomerLeadStatus.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerLeadStatus CustomerLeadStatus)
        {
            return CustomerLeadStatus.IsValidated;
        }

        public async Task<bool> Update(CustomerLeadStatus CustomerLeadStatus)
        {
            if (await ValidateId(CustomerLeadStatus))
            {
            }
            return CustomerLeadStatus.IsValidated;
        }

        public async Task<bool> Delete(CustomerLeadStatus CustomerLeadStatus)
        {
            if (await ValidateId(CustomerLeadStatus))
            {
            }
            return CustomerLeadStatus.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerLeadStatus> CustomerLeadStatuses)
        {
            foreach (CustomerLeadStatus CustomerLeadStatus in CustomerLeadStatuses)
            {
                await Delete(CustomerLeadStatus);
            }
            return CustomerLeadStatuses.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerLeadStatus> CustomerLeadStatuses)
        {
            return true;
        }
    }
}
