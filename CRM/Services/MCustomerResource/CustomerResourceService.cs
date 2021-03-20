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

namespace CRM.Services.MCustomerResource
{
    public interface ICustomerResourceService :  IServiceScoped
    {
        Task<int> Count(CustomerResourceFilter CustomerResourceFilter);
        Task<List<CustomerResource>> List(CustomerResourceFilter CustomerResourceFilter);
        Task<CustomerResource> Get(long Id);
        Task<CustomerResource> Create(CustomerResource CustomerResource);
        Task<CustomerResource> Update(CustomerResource CustomerResource);
        Task<CustomerResource> Delete(CustomerResource CustomerResource);
        Task<List<CustomerResource>> BulkDelete(List<CustomerResource> CustomerResources);
        Task<List<CustomerResource>> Import(List<CustomerResource> CustomerResources);
        Task<CustomerResourceFilter> ToFilter(CustomerResourceFilter CustomerResourceFilter);
    }

    public class CustomerResourceService : BaseService, ICustomerResourceService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerResourceValidator CustomerResourceValidator;

        public CustomerResourceService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerResourceValidator CustomerResourceValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerResourceValidator = CustomerResourceValidator;
        }
        public async Task<int> Count(CustomerResourceFilter CustomerResourceFilter)
        {
            try
            {
                int result = await UOW.CustomerResourceRepository.Count(CustomerResourceFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerResourceService));
            }
            return 0;
        }

        public async Task<List<CustomerResource>> List(CustomerResourceFilter CustomerResourceFilter)
        {
            try
            {
                List<CustomerResource> CustomerResources = await UOW.CustomerResourceRepository.List(CustomerResourceFilter);
                return CustomerResources;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerResourceService));
            }
            return null;
        }
        
        public async Task<CustomerResource> Get(long Id)
        {
            CustomerResource CustomerResource = await UOW.CustomerResourceRepository.Get(Id);
            if (CustomerResource == null)
                return null;
            return CustomerResource;
        }
        public async Task<CustomerResource> Create(CustomerResource CustomerResource)
        {
            if (!await CustomerResourceValidator.Create(CustomerResource))
                return CustomerResource;

            try
            {
                await UOW.CustomerResourceRepository.Create(CustomerResource);
                CustomerResource = await UOW.CustomerResourceRepository.Get(CustomerResource.Id);
                await Logging.CreateAuditLog(CustomerResource, new { }, nameof(CustomerResourceService));
                return CustomerResource;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerResourceService));
            }
            return null;
        }

        public async Task<CustomerResource> Update(CustomerResource CustomerResource)
        {
            if (!await CustomerResourceValidator.Update(CustomerResource))
                return CustomerResource;
            try
            {
                var oldData = await UOW.CustomerResourceRepository.Get(CustomerResource.Id);

                await UOW.CustomerResourceRepository.Update(CustomerResource);

                CustomerResource = await UOW.CustomerResourceRepository.Get(CustomerResource.Id);
                await Logging.CreateAuditLog(CustomerResource, oldData, nameof(CustomerResourceService));
                return CustomerResource;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerResourceService));
            }
            return null;
        }

        public async Task<CustomerResource> Delete(CustomerResource CustomerResource)
        {
            if (!await CustomerResourceValidator.Delete(CustomerResource))
                return CustomerResource;

            try
            {
                await UOW.CustomerResourceRepository.Delete(CustomerResource);
                await Logging.CreateAuditLog(new { }, CustomerResource, nameof(CustomerResourceService));
                return CustomerResource;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerResourceService));
            }
            return null;
        }

        public async Task<List<CustomerResource>> BulkDelete(List<CustomerResource> CustomerResources)
        {
            if (!await CustomerResourceValidator.BulkDelete(CustomerResources))
                return CustomerResources;

            try
            {
                await UOW.CustomerResourceRepository.BulkDelete(CustomerResources);
                await Logging.CreateAuditLog(new { }, CustomerResources, nameof(CustomerResourceService));
                return CustomerResources;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerResourceService));
            }
            return null;

        }
        
        public async Task<List<CustomerResource>> Import(List<CustomerResource> CustomerResources)
        {
            if (!await CustomerResourceValidator.Import(CustomerResources))
                return CustomerResources;
            try
            {
                await UOW.CustomerResourceRepository.BulkMerge(CustomerResources);

                await Logging.CreateAuditLog(CustomerResources, new { }, nameof(CustomerResourceService));
                return CustomerResources;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerResourceService));
            }
            return null;
        }     
        
        public async Task<CustomerResourceFilter> ToFilter(CustomerResourceFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerResourceFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerResourceFilter subFilter = new CustomerResourceFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterBuilder.Merge(subFilter.StatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        subFilter.Description = FilterBuilder.Merge(subFilter.Description, FilterPermissionDefinition.StringFilter);
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
