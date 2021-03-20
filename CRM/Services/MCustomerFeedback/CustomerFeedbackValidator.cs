using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerFeedback
{
    public interface ICustomerFeedbackValidator : IServiceScoped
    {
        Task<bool> Create(CustomerFeedback CustomerFeedback);
        Task<bool> Update(CustomerFeedback CustomerFeedback);
        Task<bool> Delete(CustomerFeedback CustomerFeedback);
        Task<bool> BulkDelete(List<CustomerFeedback> CustomerFeedbacks);
        Task<bool> Import(List<CustomerFeedback> CustomerFeedbacks);
    }

    public class CustomerFeedbackValidator : ICustomerFeedbackValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerFeedbackValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerFeedback CustomerFeedback)
        {
            CustomerFeedbackFilter CustomerFeedbackFilter = new CustomerFeedbackFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerFeedback.Id },
                Selects = CustomerFeedbackSelect.Id
            };

            int count = await UOW.CustomerFeedbackRepository.Count(CustomerFeedbackFilter);
            if (count == 0)
                CustomerFeedback.AddError(nameof(CustomerFeedbackValidator), nameof(CustomerFeedback.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerFeedback CustomerFeedback)
        {
            return CustomerFeedback.IsValidated;
        }

        public async Task<bool> Update(CustomerFeedback CustomerFeedback)
        {
            if (await ValidateId(CustomerFeedback))
            {
            }
            return CustomerFeedback.IsValidated;
        }

        public async Task<bool> Delete(CustomerFeedback CustomerFeedback)
        {
            if (await ValidateId(CustomerFeedback))
            {
            }
            return CustomerFeedback.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerFeedback> CustomerFeedbacks)
        {
            foreach (CustomerFeedback CustomerFeedback in CustomerFeedbacks)
            {
                await Delete(CustomerFeedback);
            }
            return CustomerFeedbacks.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerFeedback> CustomerFeedbacks)
        {
            return true;
        }
    }
}
