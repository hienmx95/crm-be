using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MProduct
{
    public interface IProductValidator : IServiceScoped
    {
        Task<bool> Create(Product Product);
        Task<bool> Update(Product Product);
        Task<bool> Delete(Product Product);
        Task<bool> BulkDelete(List<Product> Products);
        Task<bool> Import(List<Product> Products);
    }

    public class ProductValidator : IProductValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public ProductValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Product Product)
        {
            ProductFilter ProductFilter = new ProductFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Product.Id },
                Selects = ProductSelect.Id
            };

            int count = await UOW.ProductRepository.Count(ProductFilter);
            if (count == 0)
                Product.AddError(nameof(ProductValidator), nameof(Product.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(Product Product)
        {
            return Product.IsValidated;
        }

        public async Task<bool> Update(Product Product)
        {
            if (await ValidateId(Product))
            {
            }
            return Product.IsValidated;
        }

        public async Task<bool> Delete(Product Product)
        {
            if (await ValidateId(Product))
            {
            }
            return Product.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Product> Products)
        {
            foreach (Product Product in Products)
            {
                await Delete(Product);
            }
            return Products.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Product> Products)
        {
            return true;
        }
    }
}
