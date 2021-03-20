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
using CRM.Enums;

namespace CRM.Services.MCustomerType
{
    public interface ICustomerTypeService :  IServiceScoped
    {
        Task<int> Count(CustomerTypeFilter CustomerTypeFilter);
        Task<List<CustomerType>> List(CustomerTypeFilter CustomerTypeFilter);
        Task<CustomerType> Get(long Id);
        Task<CustomerTypeFilter> ToFilter(CustomerTypeFilter CustomerTypeFilter);
    }

    public class CustomerTypeService : BaseService, ICustomerTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;

        public CustomerTypeService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
        }
        public async Task<int> Count(CustomerTypeFilter CustomerTypeFilter)
        {
            try
            {
                int result = await UOW.CustomerTypeRepository.Count(CustomerTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerTypeService));
            }
            return 0;
        }

        public async Task<List<CustomerType>> List(CustomerTypeFilter CustomerTypeFilter)
        {
            try
            {
                List<CustomerType> CustomerTypes = await UOW.CustomerTypeRepository.List(CustomerTypeFilter);
                return CustomerTypes;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerTypeService));
            }
            return null;
        }
        
        public async Task<CustomerType> Get(long Id)
        {
            CustomerType CustomerType = await UOW.CustomerTypeRepository.Get(Id);
            if (CustomerType == null)
                return null;
            return CustomerType;
        }
        public async Task<CustomerTypeFilter> ToFilter(CustomerTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerTypeFilter subFilter = new CustomerTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterBuilder.Merge(subFilter.Code, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterBuilder.Merge(subFilter.Name, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                        }
                    }
                }
            }
            return filter;
        }
    }
}
