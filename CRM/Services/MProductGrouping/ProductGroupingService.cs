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

namespace CRM.Services.MProductGrouping
{
    public interface IProductGroupingService :  IServiceScoped
    {
        Task<int> Count(ProductGroupingFilter ProductGroupingFilter);
        Task<List<ProductGrouping>> List(ProductGroupingFilter ProductGroupingFilter);
        Task<ProductGrouping> Get(long Id);
        ProductGroupingFilter ToFilter(ProductGroupingFilter ProductGroupingFilter);
    }

    public class ProductGroupingService : BaseService, IProductGroupingService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IProductGroupingValidator ProductGroupingValidator;

        public ProductGroupingService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IProductGroupingValidator ProductGroupingValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ProductGroupingValidator = ProductGroupingValidator;
        }
        public async Task<int> Count(ProductGroupingFilter ProductGroupingFilter)
        {
            try
            {
                int result = await UOW.ProductGroupingRepository.Count(ProductGroupingFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ProductGroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ProductGroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<ProductGrouping>> List(ProductGroupingFilter ProductGroupingFilter)
        {
            try
            {
                List<ProductGrouping> ProductGroupings = await UOW.ProductGroupingRepository.List(ProductGroupingFilter);
                return ProductGroupings;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(ProductGroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(ProductGroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<ProductGrouping> Get(long Id)
        {
            ProductGrouping ProductGrouping = await UOW.ProductGroupingRepository.Get(Id);
            if (ProductGrouping == null)
                return null;
            return ProductGrouping;
        }
       
        public ProductGroupingFilter ToFilter(ProductGroupingFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ProductGroupingFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ProductGroupingFilter subFilter = new ProductGroupingFilter();
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
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ParentId))
                        subFilter.ParentId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Path))
                        
                        
                        
                        
                        
                        
                        subFilter.Path = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Level))
                        
                        subFilter.Level = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                }
            }
            return filter;
        }
    }
}
