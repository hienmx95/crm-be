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

namespace CRM.Services.MCustomerFeedback
{
    public interface ICustomerFeedbackService :  IServiceScoped
    {
        Task<int> Count(CustomerFeedbackFilter CustomerFeedbackFilter);
        Task<List<CustomerFeedback>> List(CustomerFeedbackFilter CustomerFeedbackFilter);
        Task<CustomerFeedback> Get(long Id);
        Task<CustomerFeedback> Create(CustomerFeedback CustomerFeedback);
        Task<CustomerFeedback> Update(CustomerFeedback CustomerFeedback);
        Task<CustomerFeedback> Delete(CustomerFeedback CustomerFeedback);
        Task<List<CustomerFeedback>> BulkDelete(List<CustomerFeedback> CustomerFeedbacks);
        Task<List<CustomerFeedback>> Import(List<CustomerFeedback> CustomerFeedbacks);
        Task<CustomerFeedbackFilter> ToFilter(CustomerFeedbackFilter CustomerFeedbackFilter);
    }

    public class CustomerFeedbackService : BaseService, ICustomerFeedbackService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerFeedbackValidator CustomerFeedbackValidator;

        public CustomerFeedbackService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerFeedbackValidator CustomerFeedbackValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerFeedbackValidator = CustomerFeedbackValidator;
        }
        public async Task<int> Count(CustomerFeedbackFilter CustomerFeedbackFilter)
        {
            try
            {
                int result = await UOW.CustomerFeedbackRepository.Count(CustomerFeedbackFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackService));
            }
            return 0;
        }

        public async Task<List<CustomerFeedback>> List(CustomerFeedbackFilter CustomerFeedbackFilter)
        {
            try
            {
                List<CustomerFeedback> CustomerFeedbacks = await UOW.CustomerFeedbackRepository.List(CustomerFeedbackFilter);
                return CustomerFeedbacks;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackService));
            }
            return null;
        }
        
        public async Task<CustomerFeedback> Get(long Id)
        {
            CustomerFeedback CustomerFeedback = await UOW.CustomerFeedbackRepository.Get(Id);
            if (CustomerFeedback == null)
                return null;
            return CustomerFeedback;
        }
        public async Task<CustomerFeedback> Create(CustomerFeedback CustomerFeedback)
        {
            if (!await CustomerFeedbackValidator.Create(CustomerFeedback))
                return CustomerFeedback;

            try
            {
                await UOW.CustomerFeedbackRepository.Create(CustomerFeedback);
                CustomerFeedback = await UOW.CustomerFeedbackRepository.Get(CustomerFeedback.Id);
                await Logging.CreateAuditLog(CustomerFeedback, new { }, nameof(CustomerFeedbackService));
                return CustomerFeedback;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackService));
            }
            return null;
        }

        public async Task<CustomerFeedback> Update(CustomerFeedback CustomerFeedback)
        {
            if (!await CustomerFeedbackValidator.Update(CustomerFeedback))
                return CustomerFeedback;
            try
            {
                var oldData = await UOW.CustomerFeedbackRepository.Get(CustomerFeedback.Id);

                await UOW.CustomerFeedbackRepository.Update(CustomerFeedback);

                CustomerFeedback = await UOW.CustomerFeedbackRepository.Get(CustomerFeedback.Id);
                await Logging.CreateAuditLog(CustomerFeedback, oldData, nameof(CustomerFeedbackService));
                return CustomerFeedback;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackService));
            }
            return null;
        }

        public async Task<CustomerFeedback> Delete(CustomerFeedback CustomerFeedback)
        {
            if (!await CustomerFeedbackValidator.Delete(CustomerFeedback))
                return CustomerFeedback;

            try
            {
                await UOW.CustomerFeedbackRepository.Delete(CustomerFeedback);
                await Logging.CreateAuditLog(new { }, CustomerFeedback, nameof(CustomerFeedbackService));
                return CustomerFeedback;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackService));
            }
            return null;
        }

        public async Task<List<CustomerFeedback>> BulkDelete(List<CustomerFeedback> CustomerFeedbacks)
        {
            if (!await CustomerFeedbackValidator.BulkDelete(CustomerFeedbacks))
                return CustomerFeedbacks;

            try
            {
                await UOW.CustomerFeedbackRepository.BulkDelete(CustomerFeedbacks);
                await Logging.CreateAuditLog(new { }, CustomerFeedbacks, nameof(CustomerFeedbackService));
                return CustomerFeedbacks;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackService));
            }
            return null;

        }
        
        public async Task<List<CustomerFeedback>> Import(List<CustomerFeedback> CustomerFeedbacks)
        {
            if (!await CustomerFeedbackValidator.Import(CustomerFeedbacks))
                return CustomerFeedbacks;
            try
            {
                await UOW.CustomerFeedbackRepository.BulkMerge(CustomerFeedbacks);

                await Logging.CreateAuditLog(CustomerFeedbacks, new { }, nameof(CustomerFeedbackService));
                return CustomerFeedbacks;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerFeedbackService));
            }
            return null;
        }     
        
        public async Task<CustomerFeedbackFilter> ToFilter(CustomerFeedbackFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerFeedbackFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerFeedbackFilter subFilter = new CustomerFeedbackFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerId))
                        subFilter.CustomerId = FilterBuilder.Merge(subFilter.CustomerId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.FullName))
                        subFilter.FullName = FilterBuilder.Merge(subFilter.FullName, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Email))
                        subFilter.Email = FilterBuilder.Merge(subFilter.Email, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PhoneNumber))
                        subFilter.PhoneNumber = FilterBuilder.Merge(subFilter.PhoneNumber, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerFeedbackTypeId))
                        subFilter.CustomerFeedbackTypeId = FilterBuilder.Merge(subFilter.CustomerFeedbackTypeId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Title))
                        subFilter.Title = FilterBuilder.Merge(subFilter.Title, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SendDate))
                        subFilter.SendDate = FilterBuilder.Merge(subFilter.SendDate, FilterPermissionDefinition.DateFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Content))
                        subFilter.Content = FilterBuilder.Merge(subFilter.Content, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterBuilder.Merge(subFilter.StatusId, FilterPermissionDefinition.IdFilter);
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
