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

namespace CRM.Services.MCustomer
{
    public interface ICustomerService :  IServiceScoped
    {
        Task<int> Count(CustomerFilter CustomerFilter);
        Task<List<Customer>> List(CustomerFilter CustomerFilter);
        Task<Customer> Get(long Id);
        Task<Customer> Create(Customer Customer);
        Task<Customer> Update(Customer Customer);
        Task<Customer> Delete(Customer Customer);
        Task<List<Customer>> BulkDelete(List<Customer> Customers);
        Task<List<Customer>> Import(List<Customer> Customers);
        Task<CustomerFilter> ToFilter(CustomerFilter CustomerFilter);
    }

    public class CustomerService : BaseService, ICustomerService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerValidator CustomerValidator;

        public CustomerService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerValidator CustomerValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerValidator = CustomerValidator;
        }
        public async Task<int> Count(CustomerFilter CustomerFilter)
        {
            try
            {
                int result = await UOW.CustomerRepository.Count(CustomerFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerService));
            }
            return 0;
        }

        public async Task<List<Customer>> List(CustomerFilter CustomerFilter)
        {
            try
            {
                List<Customer> Customers = await UOW.CustomerRepository.List(CustomerFilter);
                return Customers;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerService));
            }
            return null;
        }
        
        public async Task<Customer> Get(long Id)
        {
            Customer Customer = await UOW.CustomerRepository.Get(Id);
            if (Customer == null)
                return null;
            return Customer;
        }
        public async Task<Customer> Create(Customer Customer)
        {
            if (!await CustomerValidator.Create(Customer))
                return Customer;

            try
            {
                var Creator = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                Customer.CreatorId = Creator.Id;
                Customer.OrganizationId = Creator.OrganizationId;
                await UOW.CustomerRepository.Create(Customer);
                Customer = await UOW.CustomerRepository.Get(Customer.Id);
                await Logging.CreateAuditLog(Customer, new { }, nameof(CustomerService));
                return Customer;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerService));
            }
            return null;
        }

        public async Task<Customer> Update(Customer Customer)
        {
            if (!await CustomerValidator.Update(Customer))
                return Customer;
            try
            {
                var oldData = await UOW.CustomerRepository.Get(Customer.Id);

                await UOW.CustomerRepository.Update(Customer);

                Customer = await UOW.CustomerRepository.Get(Customer.Id);
                await Logging.CreateAuditLog(Customer, oldData, nameof(CustomerService));
                return Customer;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerService));
            }
            return null;
        }

        public async Task<Customer> Delete(Customer Customer)
        {
            if (!await CustomerValidator.Delete(Customer))
                return Customer;

            try
            {
                await UOW.CustomerRepository.Delete(Customer);
                await Logging.CreateAuditLog(new { }, Customer, nameof(CustomerService));
                return Customer;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerService));
            }
            return null;
        }

        public async Task<List<Customer>> BulkDelete(List<Customer> Customers)
        {
            if (!await CustomerValidator.BulkDelete(Customers))
                return Customers;

            try
            {
                await UOW.CustomerRepository.BulkDelete(Customers);
                await Logging.CreateAuditLog(new { }, Customers, nameof(CustomerService));
                return Customers;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerService));
            }
            return null;

        }
        
        public async Task<List<Customer>> Import(List<Customer> Customers)
        {
            if (!await CustomerValidator.Import(Customers))
                return Customers;
            try
            {
                await UOW.CustomerRepository.BulkMerge(Customers);

                await Logging.CreateAuditLog(Customers, new { }, nameof(CustomerService));
                return Customers;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerService));
            }
            return null;
        }     
        
        public async Task<CustomerFilter> ToFilter(CustomerFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerFilter subFilter = new CustomerFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerTypeId))
                        subFilter.CustomerTypeId = FilterBuilder.Merge(subFilter.CustomerTypeId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerGroupingId))
                        subFilter.CustomerGroupingId = FilterBuilder.Merge(subFilter.CustomerGroupingId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                        subFilter.OrganizationId = FilterBuilder.Merge(subFilter.OrganizationId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CreatorId))
                        subFilter.CreatorId = FilterBuilder.Merge(subFilter.CreatorId, FilterPermissionDefinition.IdFilter);
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
