using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MCustomerSalesOrderPromotion
{
    public interface ICustomerSalesOrderPromotionValidator : IServiceScoped
    {
        Task<bool> Create(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<bool> Update(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<bool> Delete(CustomerSalesOrderPromotion CustomerSalesOrderPromotion);
        Task<bool> BulkDelete(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions);
        Task<bool> Import(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions);
    }

    public class CustomerSalesOrderPromotionValidator : ICustomerSalesOrderPromotionValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public CustomerSalesOrderPromotionValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            CustomerSalesOrderPromotionFilter CustomerSalesOrderPromotionFilter = new CustomerSalesOrderPromotionFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = CustomerSalesOrderPromotion.Id },
                Selects = CustomerSalesOrderPromotionSelect.Id
            };

            int count = await UOW.CustomerSalesOrderPromotionRepository.Count(CustomerSalesOrderPromotionFilter);
            if (count == 0)
                CustomerSalesOrderPromotion.AddError(nameof(CustomerSalesOrderPromotionValidator), nameof(CustomerSalesOrderPromotion.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            return CustomerSalesOrderPromotion.IsValidated;
        }

        public async Task<bool> Update(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            if (await ValidateId(CustomerSalesOrderPromotion))
            {
            }
            return CustomerSalesOrderPromotion.IsValidated;
        }

        public async Task<bool> Delete(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            if (await ValidateId(CustomerSalesOrderPromotion))
            {
            }
            return CustomerSalesOrderPromotion.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions)
        {
            foreach (CustomerSalesOrderPromotion CustomerSalesOrderPromotion in CustomerSalesOrderPromotions)
            {
                await Delete(CustomerSalesOrderPromotion);
            }
            return CustomerSalesOrderPromotions.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions)
        {
            return true;
        }
    }
}
