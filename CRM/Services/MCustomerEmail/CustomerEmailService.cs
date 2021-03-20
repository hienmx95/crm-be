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

namespace CRM.Services.MCustomerEmail
{
    public interface ICustomerEmailService :  IServiceScoped
    {
        Task<int> Count(CustomerEmailFilter CustomerEmailFilter);
        Task<List<CustomerEmail>> List(CustomerEmailFilter CustomerEmailFilter);
        Task<CustomerEmail> Get(long Id);
        Task<CustomerEmail> Create(CustomerEmail CustomerEmail);
        Task<CustomerEmail> Update(CustomerEmail CustomerEmail);
        Task<CustomerEmail> Delete(CustomerEmail CustomerEmail);
        Task<List<CustomerEmail>> BulkDelete(List<CustomerEmail> CustomerEmails);
        Task<List<CustomerEmail>> Import(List<CustomerEmail> CustomerEmails);
        Task<CustomerEmailFilter> ToFilter(CustomerEmailFilter CustomerEmailFilter);
    }

    public class CustomerEmailService : BaseService, ICustomerEmailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerEmailValidator CustomerEmailValidator;

        public CustomerEmailService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerEmailValidator CustomerEmailValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerEmailValidator = CustomerEmailValidator;
        }
        public async Task<int> Count(CustomerEmailFilter CustomerEmailFilter)
        {
            try
            {
                int result = await UOW.CustomerEmailRepository.Count(CustomerEmailFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailService));
            }
            return 0;
        }

        public async Task<List<CustomerEmail>> List(CustomerEmailFilter CustomerEmailFilter)
        {
            try
            {
                List<CustomerEmail> CustomerEmails = await UOW.CustomerEmailRepository.List(CustomerEmailFilter);
                return CustomerEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailService));
            }
            return null;
        }
        
        public async Task<CustomerEmail> Get(long Id)
        {
            CustomerEmail CustomerEmail = await UOW.CustomerEmailRepository.Get(Id);
            if (CustomerEmail == null)
                return null;
            return CustomerEmail;
        }
        public async Task<CustomerEmail> Create(CustomerEmail CustomerEmail)
        {
            if (!await CustomerEmailValidator.Create(CustomerEmail))
                return CustomerEmail;

            try
            {
                await UOW.CustomerEmailRepository.Create(CustomerEmail);
                CustomerEmail = await UOW.CustomerEmailRepository.Get(CustomerEmail.Id);
                await Logging.CreateAuditLog(CustomerEmail, new { }, nameof(CustomerEmailService));
                return CustomerEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailService));
            }
            return null;
        }

        public async Task<CustomerEmail> Update(CustomerEmail CustomerEmail)
        {
            if (!await CustomerEmailValidator.Update(CustomerEmail))
                return CustomerEmail;
            try
            {
                var oldData = await UOW.CustomerEmailRepository.Get(CustomerEmail.Id);

                await UOW.CustomerEmailRepository.Update(CustomerEmail);

                CustomerEmail = await UOW.CustomerEmailRepository.Get(CustomerEmail.Id);
                await Logging.CreateAuditLog(CustomerEmail, oldData, nameof(CustomerEmailService));
                return CustomerEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailService));
            }
            return null;
        }

        public async Task<CustomerEmail> Delete(CustomerEmail CustomerEmail)
        {
            if (!await CustomerEmailValidator.Delete(CustomerEmail))
                return CustomerEmail;

            try
            {
                await UOW.CustomerEmailRepository.Delete(CustomerEmail);
                await Logging.CreateAuditLog(new { }, CustomerEmail, nameof(CustomerEmailService));
                return CustomerEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailService));
            }
            return null;
        }

        public async Task<List<CustomerEmail>> BulkDelete(List<CustomerEmail> CustomerEmails)
        {
            if (!await CustomerEmailValidator.BulkDelete(CustomerEmails))
                return CustomerEmails;

            try
            {
                await UOW.CustomerEmailRepository.BulkDelete(CustomerEmails);
                await Logging.CreateAuditLog(new { }, CustomerEmails, nameof(CustomerEmailService));
                return CustomerEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailService));
            }
            return null;

        }
        
        public async Task<List<CustomerEmail>> Import(List<CustomerEmail> CustomerEmails)
        {
            if (!await CustomerEmailValidator.Import(CustomerEmails))
                return CustomerEmails;
            try
            {
                await UOW.CustomerEmailRepository.BulkMerge(CustomerEmails);

                await Logging.CreateAuditLog(CustomerEmails, new { }, nameof(CustomerEmailService));
                return CustomerEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailService));
            }
            return null;
        }     
        
        public async Task<CustomerEmailFilter> ToFilter(CustomerEmailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerEmailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerEmailFilter subFilter = new CustomerEmailFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerId))
                        subFilter.CustomerId = FilterBuilder.Merge(subFilter.CustomerId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Email))
                        subFilter.Email = FilterBuilder.Merge(subFilter.Email, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.EmailTypeId))
                        subFilter.EmailTypeId = FilterBuilder.Merge(subFilter.EmailTypeId, FilterPermissionDefinition.IdFilter);
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
