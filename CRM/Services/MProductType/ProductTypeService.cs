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

namespace CRM.Services.MProductType
{
    public interface IProductTypeService :  IServiceScoped
    {
        Task<int> Count(ProductTypeFilter ProductTypeFilter);
        Task<List<ProductType>> List(ProductTypeFilter ProductTypeFilter);
        Task<ProductType> Get(long Id);
        ProductTypeFilter ToFilter(ProductTypeFilter ProductTypeFilter);
    }

    public class ProductTypeService : BaseService, IProductTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IProductTypeValidator ProductTypeValidator;

        public ProductTypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IProductTypeValidator ProductTypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ProductTypeValidator = ProductTypeValidator;
        }
        public async Task<int> Count(ProductTypeFilter ProductTypeFilter)
        {
            try
            {
                int result = await UOW.ProductTypeRepository.Count(ProductTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ProductTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ProductTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<ProductType>> List(ProductTypeFilter ProductTypeFilter)
        {
            try
            {
                List<ProductType> ProductTypes = await UOW.ProductTypeRepository.List(ProductTypeFilter);
                return ProductTypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ProductTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ProductTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<ProductType> Get(long Id)
        {
            ProductType ProductType = await UOW.ProductTypeRepository.Get(Id);
            if (ProductType == null)
                return null;
            return ProductType;
        }
       
        public ProductTypeFilter ToFilter(ProductTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ProductTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ProductTypeFilter subFilter = new ProductTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        
                        
                        
                        
                        
                        
                        subFilter.Description = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
