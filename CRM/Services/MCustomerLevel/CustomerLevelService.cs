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

namespace CRM.Services.MCustomerLevel
{
    public interface ICustomerLevelService :  IServiceScoped
    {
        Task<int> Count(CustomerLevelFilter CustomerLevelFilter);
        Task<List<CustomerLevel>> List(CustomerLevelFilter CustomerLevelFilter);
        Task<CustomerLevel> Get(long Id);
        Task<CustomerLevel> Create(CustomerLevel CustomerLevel);
        Task<CustomerLevel> Update(CustomerLevel CustomerLevel);
        Task<CustomerLevel> Delete(CustomerLevel CustomerLevel);
        Task<List<CustomerLevel>> BulkDelete(List<CustomerLevel> CustomerLevels);
        Task<List<CustomerLevel>> Import(List<CustomerLevel> CustomerLevels);
        Task<CustomerLevelFilter> ToFilter(CustomerLevelFilter CustomerLevelFilter);
    }

    public class CustomerLevelService : BaseService, ICustomerLevelService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerLevelValidator CustomerLevelValidator;

        public CustomerLevelService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerLevelValidator CustomerLevelValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerLevelValidator = CustomerLevelValidator;
        }
        public async Task<int> Count(CustomerLevelFilter CustomerLevelFilter)
        {
            try
            {
                int result = await UOW.CustomerLevelRepository.Count(CustomerLevelFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLevelService));
            }
            return 0;
        }

        public async Task<List<CustomerLevel>> List(CustomerLevelFilter CustomerLevelFilter)
        {
            try
            {
                List<CustomerLevel> CustomerLevels = await UOW.CustomerLevelRepository.List(CustomerLevelFilter);
                return CustomerLevels;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLevelService));
            }
            return null;
        }
        
        public async Task<CustomerLevel> Get(long Id)
        {
            CustomerLevel CustomerLevel = await UOW.CustomerLevelRepository.Get(Id);
            if (CustomerLevel == null)
                return null;
            return CustomerLevel;
        }
        public async Task<CustomerLevel> Create(CustomerLevel CustomerLevel)
        {
            if (!await CustomerLevelValidator.Create(CustomerLevel))
                return CustomerLevel;

            try
            {
                await UOW.CustomerLevelRepository.Create(CustomerLevel);
                CustomerLevel = await UOW.CustomerLevelRepository.Get(CustomerLevel.Id);
                await Logging.CreateAuditLog(CustomerLevel, new { }, nameof(CustomerLevelService));
                return CustomerLevel;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLevelService));
            }
            return null;
        }

        public async Task<CustomerLevel> Update(CustomerLevel CustomerLevel)
        {
            if (!await CustomerLevelValidator.Update(CustomerLevel))
                return CustomerLevel;
            try
            {
                var oldData = await UOW.CustomerLevelRepository.Get(CustomerLevel.Id);

                await UOW.CustomerLevelRepository.Update(CustomerLevel);

                CustomerLevel = await UOW.CustomerLevelRepository.Get(CustomerLevel.Id);
                await Logging.CreateAuditLog(CustomerLevel, oldData, nameof(CustomerLevelService));
                return CustomerLevel;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLevelService));
            }
            return null;
        }

        public async Task<CustomerLevel> Delete(CustomerLevel CustomerLevel)
        {
            if (!await CustomerLevelValidator.Delete(CustomerLevel))
                return CustomerLevel;

            try
            {
                await UOW.CustomerLevelRepository.Delete(CustomerLevel);
                await Logging.CreateAuditLog(new { }, CustomerLevel, nameof(CustomerLevelService));
                return CustomerLevel;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLevelService));
            }
            return null;
        }

        public async Task<List<CustomerLevel>> BulkDelete(List<CustomerLevel> CustomerLevels)
        {
            if (!await CustomerLevelValidator.BulkDelete(CustomerLevels))
                return CustomerLevels;

            try
            {
                await UOW.CustomerLevelRepository.BulkDelete(CustomerLevels);
                await Logging.CreateAuditLog(new { }, CustomerLevels, nameof(CustomerLevelService));
                return CustomerLevels;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLevelService));
            }
            return null;

        }
        
        public async Task<List<CustomerLevel>> Import(List<CustomerLevel> CustomerLevels)
        {
            if (!await CustomerLevelValidator.Import(CustomerLevels))
                return CustomerLevels;
            try
            {
                await UOW.CustomerLevelRepository.BulkMerge(CustomerLevels);

                await Logging.CreateAuditLog(CustomerLevels, new { }, nameof(CustomerLevelService));
                return CustomerLevels;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLevelService));
            }
            return null;
        }     
        
        public async Task<CustomerLevelFilter> ToFilter(CustomerLevelFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerLevelFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerLevelFilter subFilter = new CustomerLevelFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Color))
                        subFilter.Color = FilterBuilder.Merge(subFilter.Color, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PointFrom))
                        subFilter.PointFrom = FilterBuilder.Merge(subFilter.PointFrom, FilterPermissionDefinition.LongFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.PointTo))
                        subFilter.PointTo = FilterBuilder.Merge(subFilter.PointTo, FilterPermissionDefinition.LongFilter);
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
