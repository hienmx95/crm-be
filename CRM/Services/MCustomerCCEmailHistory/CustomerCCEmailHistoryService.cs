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

namespace CRM.Services.MCustomerCCEmailHistory
{
    public interface ICustomerCCEmailHistoryService :  IServiceScoped
    {
        Task<int> Count(CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter);
        Task<List<CustomerCCEmailHistory>> List(CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter);
        Task<CustomerCCEmailHistory> Get(long Id);
        Task<CustomerCCEmailHistory> Create(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<CustomerCCEmailHistory> Update(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<CustomerCCEmailHistory> Delete(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<List<CustomerCCEmailHistory>> BulkDelete(List<CustomerCCEmailHistory> CustomerCCEmailHistories);
        Task<List<CustomerCCEmailHistory>> Import(List<CustomerCCEmailHistory> CustomerCCEmailHistories);
        Task<CustomerCCEmailHistoryFilter> ToFilter(CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter);
    }

    public class CustomerCCEmailHistoryService : BaseService, ICustomerCCEmailHistoryService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerCCEmailHistoryValidator CustomerCCEmailHistoryValidator;

        public CustomerCCEmailHistoryService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerCCEmailHistoryValidator CustomerCCEmailHistoryValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerCCEmailHistoryValidator = CustomerCCEmailHistoryValidator;
        }
        public async Task<int> Count(CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter)
        {
            try
            {
                int result = await UOW.CustomerCCEmailHistoryRepository.Count(CustomerCCEmailHistoryFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerCCEmailHistoryService));
            }
            return 0;
        }

        public async Task<List<CustomerCCEmailHistory>> List(CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter)
        {
            try
            {
                List<CustomerCCEmailHistory> CustomerCCEmailHistorys = await UOW.CustomerCCEmailHistoryRepository.List(CustomerCCEmailHistoryFilter);
                return CustomerCCEmailHistorys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerCCEmailHistoryService));
            }
            return null;
        }
        
        public async Task<CustomerCCEmailHistory> Get(long Id)
        {
            CustomerCCEmailHistory CustomerCCEmailHistory = await UOW.CustomerCCEmailHistoryRepository.Get(Id);
            if (CustomerCCEmailHistory == null)
                return null;
            return CustomerCCEmailHistory;
        }
        public async Task<CustomerCCEmailHistory> Create(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            if (!await CustomerCCEmailHistoryValidator.Create(CustomerCCEmailHistory))
                return CustomerCCEmailHistory;

            try
            {
                await UOW.CustomerCCEmailHistoryRepository.Create(CustomerCCEmailHistory);
                CustomerCCEmailHistory = await UOW.CustomerCCEmailHistoryRepository.Get(CustomerCCEmailHistory.Id);
                await Logging.CreateAuditLog(CustomerCCEmailHistory, new { }, nameof(CustomerCCEmailHistoryService));
                return CustomerCCEmailHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerCCEmailHistoryService));
            }
            return null;
        }

        public async Task<CustomerCCEmailHistory> Update(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            if (!await CustomerCCEmailHistoryValidator.Update(CustomerCCEmailHistory))
                return CustomerCCEmailHistory;
            try
            {
                var oldData = await UOW.CustomerCCEmailHistoryRepository.Get(CustomerCCEmailHistory.Id);

                await UOW.CustomerCCEmailHistoryRepository.Update(CustomerCCEmailHistory);

                CustomerCCEmailHistory = await UOW.CustomerCCEmailHistoryRepository.Get(CustomerCCEmailHistory.Id);
                await Logging.CreateAuditLog(CustomerCCEmailHistory, oldData, nameof(CustomerCCEmailHistoryService));
                return CustomerCCEmailHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerCCEmailHistoryService));
            }
            return null;
        }

        public async Task<CustomerCCEmailHistory> Delete(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            if (!await CustomerCCEmailHistoryValidator.Delete(CustomerCCEmailHistory))
                return CustomerCCEmailHistory;

            try
            {
                await UOW.CustomerCCEmailHistoryRepository.Delete(CustomerCCEmailHistory);
                await Logging.CreateAuditLog(new { }, CustomerCCEmailHistory, nameof(CustomerCCEmailHistoryService));
                return CustomerCCEmailHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerCCEmailHistoryService));
            }
            return null;
        }

        public async Task<List<CustomerCCEmailHistory>> BulkDelete(List<CustomerCCEmailHistory> CustomerCCEmailHistories)
        {
            if (!await CustomerCCEmailHistoryValidator.BulkDelete(CustomerCCEmailHistories))
                return CustomerCCEmailHistories;

            try
            {
                await UOW.CustomerCCEmailHistoryRepository.BulkDelete(CustomerCCEmailHistories);
                await Logging.CreateAuditLog(new { }, CustomerCCEmailHistories, nameof(CustomerCCEmailHistoryService));
                return CustomerCCEmailHistories;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerCCEmailHistoryService));
            }
            return null;

        }
        
        public async Task<List<CustomerCCEmailHistory>> Import(List<CustomerCCEmailHistory> CustomerCCEmailHistories)
        {
            if (!await CustomerCCEmailHistoryValidator.Import(CustomerCCEmailHistories))
                return CustomerCCEmailHistories;
            try
            {
                await UOW.CustomerCCEmailHistoryRepository.BulkMerge(CustomerCCEmailHistories);

                await Logging.CreateAuditLog(CustomerCCEmailHistories, new { }, nameof(CustomerCCEmailHistoryService));
                return CustomerCCEmailHistories;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerCCEmailHistoryService));
            }
            return null;
        }     
        
        public async Task<CustomerCCEmailHistoryFilter> ToFilter(CustomerCCEmailHistoryFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerCCEmailHistoryFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerCCEmailHistoryFilter subFilter = new CustomerCCEmailHistoryFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerEmailHistoryId))
                        subFilter.CustomerEmailHistoryId = FilterBuilder.Merge(subFilter.CustomerEmailHistoryId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CCEmail))
                        subFilter.CCEmail = FilterBuilder.Merge(subFilter.CCEmail, FilterPermissionDefinition.StringFilter);
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
