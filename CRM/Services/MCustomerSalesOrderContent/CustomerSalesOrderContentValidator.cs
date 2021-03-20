using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerSalesOrderContent
{
    public interface ICustomerSalesOrderContentValidator : IServiceScoped
    {
        Task<bool> Create(CustomerSalesOrderContent CustomerSalesOrderContent);
        Task<bool> Update(CustomerSalesOrderContent CustomerSalesOrderContent);
        Task<bool> Delete(CustomerSalesOrderContent CustomerSalesOrderContent);
        Task<bool> BulkDelete(List<CustomerSalesOrderContent> CustomerSalesOrderContents);
        Task<bool> Import(List<CustomerSalesOrderContent> CustomerSalesOrderContents);
    }

    public class CustomerSalesOrderContentValidator : ICustomerSalesOrderContentValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerSalesOrderContentValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
            CustomerSalesOrderContentFilter CustomerSalesOrderContentFilter = new CustomerSalesOrderContentFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerSalesOrderContent.Id },
                Selects = CustomerSalesOrderContentSelect.Id
            };

            int count = await UOW.CustomerSalesOrderContentRepository.Count(CustomerSalesOrderContentFilter);
            if (count == 0)
                CustomerSalesOrderContent.AddError(nameof(CustomerSalesOrderContentValidator), nameof(CustomerSalesOrderContent.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
            return CustomerSalesOrderContent.IsValidated;
        }

        public async Task<bool> Update(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
            if (await ValidateId(CustomerSalesOrderContent))
            {
            }
            return CustomerSalesOrderContent.IsValidated;
        }

        public async Task<bool> Delete(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
            if (await ValidateId(CustomerSalesOrderContent))
            {
            }
            return CustomerSalesOrderContent.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerSalesOrderContent> CustomerSalesOrderContents)
        {
            foreach (CustomerSalesOrderContent CustomerSalesOrderContent in CustomerSalesOrderContents)
            {
                await Delete(CustomerSalesOrderContent);
            }
            return CustomerSalesOrderContents.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerSalesOrderContent> CustomerSalesOrderContents)
        {
            return true;
        }
    }
}
