using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MBrand
{
    public interface IBrandValidator : IServiceScoped
    {
        Task<bool> Create(Brand Brand);
        Task<bool> Update(Brand Brand);
        Task<bool> Delete(Brand Brand);
        Task<bool> BulkDelete(List<Brand> Brands);
        Task<bool> Import(List<Brand> Brands);
    }

    public class BrandValidator : IBrandValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public BrandValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(Brand Brand)
        {
            BrandFilter BrandFilter = new BrandFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Brand.Id },
                Selects = BrandSelect.Id
            };

            int count = await UOW.BrandRepository.Count(BrandFilter);
            if (count == 0)
                Brand.AddError(nameof(BrandValidator), nameof(Brand.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        public async Task<bool>Create(Brand Brand)
        {
            return Brand.IsValidated;
        }

        public async Task<bool> Update(Brand Brand)
        {
            if (await ValidateId(Brand))
            {
            }
            return Brand.IsValidated;
        }

        public async Task<bool> Delete(Brand Brand)
        {
            if (await ValidateId(Brand))
            {
            }
            return Brand.IsValidated;
        }
        
        public async Task<bool> BulkDelete(List<Brand> Brands)
        {
            foreach (Brand Brand in Brands)
            {
                await Delete(Brand);
            }
            return Brands.All(x => x.IsValidated);
        }
        
        public async Task<bool> Import(List<Brand> Brands)
        {
            return true;
        }
    }
}
