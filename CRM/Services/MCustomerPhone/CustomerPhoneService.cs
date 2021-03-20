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

namespace CRM.Services.MCustomerPhone
{
    public interface ICustomerPhoneService :  IServiceScoped
    {
        Task<int> Count(CustomerPhoneFilter CustomerPhoneFilter);
        Task<List<CustomerPhone>> List(CustomerPhoneFilter CustomerPhoneFilter);
        Task<CustomerPhone> Get(long Id);
        Task<CustomerPhone> Create(CustomerPhone CustomerPhone);
        Task<CustomerPhone> Update(CustomerPhone CustomerPhone);
        Task<CustomerPhone> Delete(CustomerPhone CustomerPhone);
        Task<List<CustomerPhone>> BulkDelete(List<CustomerPhone> CustomerPhones);
        Task<List<CustomerPhone>> Import(List<CustomerPhone> CustomerPhones);
        Task<CustomerPhoneFilter> ToFilter(CustomerPhoneFilter CustomerPhoneFilter);
    }

    public class CustomerPhoneService : BaseService, ICustomerPhoneService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerPhoneValidator CustomerPhoneValidator;

        public CustomerPhoneService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerPhoneValidator CustomerPhoneValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerPhoneValidator = CustomerPhoneValidator;
        }
        public async Task<int> Count(CustomerPhoneFilter CustomerPhoneFilter)
        {
            try
            {
                int result = await UOW.CustomerPhoneRepository.Count(CustomerPhoneFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPhoneService));
            }
            return 0;
        }

        public async Task<List<CustomerPhone>> List(CustomerPhoneFilter CustomerPhoneFilter)
        {
            try
            {
                List<CustomerPhone> CustomerPhones = await UOW.CustomerPhoneRepository.List(CustomerPhoneFilter);
                return CustomerPhones;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPhoneService));
            }
            return null;
        }
        
        public async Task<CustomerPhone> Get(long Id)
        {
            CustomerPhone CustomerPhone = await UOW.CustomerPhoneRepository.Get(Id);
            if (CustomerPhone == null)
                return null;
            return CustomerPhone;
        }
        public async Task<CustomerPhone> Create(CustomerPhone CustomerPhone)
        {
            if (!await CustomerPhoneValidator.Create(CustomerPhone))
                return CustomerPhone;

            try
            {
                await UOW.CustomerPhoneRepository.Create(CustomerPhone);
                CustomerPhone = await UOW.CustomerPhoneRepository.Get(CustomerPhone.Id);
                await Logging.CreateAuditLog(CustomerPhone, new { }, nameof(CustomerPhoneService));
                return CustomerPhone;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPhoneService));
            }
            return null;
        }

        public async Task<CustomerPhone> Update(CustomerPhone CustomerPhone)
        {
            if (!await CustomerPhoneValidator.Update(CustomerPhone))
                return CustomerPhone;
            try
            {
                var oldData = await UOW.CustomerPhoneRepository.Get(CustomerPhone.Id);

                await UOW.CustomerPhoneRepository.Update(CustomerPhone);

                CustomerPhone = await UOW.CustomerPhoneRepository.Get(CustomerPhone.Id);
                await Logging.CreateAuditLog(CustomerPhone, oldData, nameof(CustomerPhoneService));
                return CustomerPhone;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPhoneService));
            }
            return null;
        }

        public async Task<CustomerPhone> Delete(CustomerPhone CustomerPhone)
        {
            if (!await CustomerPhoneValidator.Delete(CustomerPhone))
                return CustomerPhone;

            try
            {
                await UOW.CustomerPhoneRepository.Delete(CustomerPhone);
                await Logging.CreateAuditLog(new { }, CustomerPhone, nameof(CustomerPhoneService));
                return CustomerPhone;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPhoneService));
            }
            return null;
        }

        public async Task<List<CustomerPhone>> BulkDelete(List<CustomerPhone> CustomerPhones)
        {
            if (!await CustomerPhoneValidator.BulkDelete(CustomerPhones))
                return CustomerPhones;

            try
            {
                await UOW.CustomerPhoneRepository.BulkDelete(CustomerPhones);
                await Logging.CreateAuditLog(new { }, CustomerPhones, nameof(CustomerPhoneService));
                return CustomerPhones;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPhoneService));
            }
            return null;

        }
        
        public async Task<List<CustomerPhone>> Import(List<CustomerPhone> CustomerPhones)
        {
            if (!await CustomerPhoneValidator.Import(CustomerPhones))
                return CustomerPhones;
            try
            {
                await UOW.CustomerPhoneRepository.BulkMerge(CustomerPhones);

                await Logging.CreateAuditLog(CustomerPhones, new { }, nameof(CustomerPhoneService));
                return CustomerPhones;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPhoneService));
            }
            return null;
        }     
        
        public async Task<CustomerPhoneFilter> ToFilter(CustomerPhoneFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerPhoneFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerPhoneFilter subFilter = new CustomerPhoneFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerId))
                        subFilter.CustomerId = FilterBuilder.Merge(subFilter.CustomerId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Phone))
                        subFilter.Phone = FilterBuilder.Merge(subFilter.Phone, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PhoneTypeId))
                        subFilter.PhoneTypeId = FilterBuilder.Merge(subFilter.PhoneTypeId, FilterPermissionDefinition.IdFilter);
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
