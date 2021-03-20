using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;

namespace CRM.Services.MBrand
{
    public interface IBrandService :  IServiceScoped
    {
        Task<int> Count(BrandFilter BrandFilter);
        Task<List<Brand>> List(BrandFilter BrandFilter);
        Task<Brand> Get(long Id);
        BrandFilter ToFilter(BrandFilter BrandFilter);
    }

    public class BrandService : BaseService, IBrandService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IBrandValidator BrandValidator;

        public BrandService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IBrandValidator BrandValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.BrandValidator = BrandValidator;
        }
        public async Task<int> Count(BrandFilter BrandFilter)
        {
            try
            {
                int result = await UOW.BrandRepository.Count(BrandFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(BrandService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(BrandService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Brand>> List(BrandFilter BrandFilter)
        {
            try
            {
                List<Brand> Brands = await UOW.BrandRepository.List(BrandFilter);
                return Brands;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(BrandService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(BrandService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Brand> Get(long Id)
        {
            Brand Brand = await UOW.BrandRepository.Get(Id);
            if (Brand == null)
                return null;
            return Brand;
        }

        public BrandFilter ToFilter(BrandFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<BrandFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                BrandFilter subFilter = new BrandFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        
                        
                        
                        
                        
                        
                        subFilter.Description = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
