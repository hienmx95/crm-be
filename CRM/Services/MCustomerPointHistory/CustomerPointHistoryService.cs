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

namespace CRM.Services.MCustomerPointHistory
{
    public interface ICustomerPointHistoryService :  IServiceScoped
    {
        Task<int> Count(CustomerPointHistoryFilter CustomerPointHistoryFilter);
        Task<List<CustomerPointHistory>> List(CustomerPointHistoryFilter CustomerPointHistoryFilter);
        Task<CustomerPointHistory> Get(long Id);
        Task<CustomerPointHistory> Create(CustomerPointHistory CustomerPointHistory);
        Task<CustomerPointHistory> Update(CustomerPointHistory CustomerPointHistory);
        Task<CustomerPointHistory> Delete(CustomerPointHistory CustomerPointHistory);
        Task<List<CustomerPointHistory>> BulkDelete(List<CustomerPointHistory> CustomerPointHistories);
        Task<List<CustomerPointHistory>> Import(List<CustomerPointHistory> CustomerPointHistories);
        Task<CustomerPointHistoryFilter> ToFilter(CustomerPointHistoryFilter CustomerPointHistoryFilter);
    }

    public class CustomerPointHistoryService : BaseService, ICustomerPointHistoryService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerPointHistoryValidator CustomerPointHistoryValidator;

        public CustomerPointHistoryService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerPointHistoryValidator CustomerPointHistoryValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerPointHistoryValidator = CustomerPointHistoryValidator;
        }
        public async Task<int> Count(CustomerPointHistoryFilter CustomerPointHistoryFilter)
        {
            try
            {
                int result = await UOW.CustomerPointHistoryRepository.Count(CustomerPointHistoryFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPointHistoryService));
            }
            return 0;
        }

        public async Task<List<CustomerPointHistory>> List(CustomerPointHistoryFilter CustomerPointHistoryFilter)
        {
            try
            {
                List<CustomerPointHistory> CustomerPointHistorys = await UOW.CustomerPointHistoryRepository.List(CustomerPointHistoryFilter);
                return CustomerPointHistorys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPointHistoryService));
            }
            return null;
        }
        
        public async Task<CustomerPointHistory> Get(long Id)
        {
            CustomerPointHistory CustomerPointHistory = await UOW.CustomerPointHistoryRepository.Get(Id);
            if (CustomerPointHistory == null)
                return null;
            return CustomerPointHistory;
        }
        public async Task<CustomerPointHistory> Create(CustomerPointHistory CustomerPointHistory)
        {
            if (!await CustomerPointHistoryValidator.Create(CustomerPointHistory))
                return CustomerPointHistory;

            try
            {
                var oldDatas = await UOW.CustomerPointHistoryRepository.List(new CustomerPointHistoryFilter
                {
                    Skip = 0,
                    Take = int.MaxValue,
                    Selects = CustomerPointHistorySelect.ALL,
                    CustomerId = new IdFilter { Equal = CustomerPointHistory.CustomerId },
                });
                CustomerPointHistory.TotalPoint = oldDatas.Where(x => x.IsIncrease == true).Select(x => x.ChangePoint).DefaultIfEmpty(0).Sum();
                
                CustomerPointHistory.CurrentPoint = oldDatas.Where(x => x.IsIncrease == true).Select(x => x.ChangePoint).DefaultIfEmpty(0).Sum() -
                    oldDatas.Where(x => x.IsIncrease == false).Select(x => x.ChangePoint).DefaultIfEmpty(0).Sum();
                if (CustomerPointHistory.IsIncrease)
                {
                    CustomerPointHistory.TotalPoint += CustomerPointHistory.ChangePoint;
                    CustomerPointHistory.CurrentPoint += CustomerPointHistory.ChangePoint;
                }
                else
                {
                    CustomerPointHistory.CurrentPoint -= CustomerPointHistory.ChangePoint;
                    if (CustomerPointHistory.ReduceTotal)
                    {
                        CustomerPointHistory.TotalPoint -= CustomerPointHistory.ChangePoint;
                    }
                }
                await UOW.CustomerPointHistoryRepository.Create(CustomerPointHistory);
                CustomerPointHistory = await UOW.CustomerPointHistoryRepository.Get(CustomerPointHistory.Id);
                await Logging.CreateAuditLog(CustomerPointHistory, new { }, nameof(CustomerPointHistoryService));
                return CustomerPointHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPointHistoryService));
            }
            return null;
        }

        public async Task<CustomerPointHistory> Update(CustomerPointHistory CustomerPointHistory)
        {
            if (!await CustomerPointHistoryValidator.Update(CustomerPointHistory))
                return CustomerPointHistory;
            try
            {
                var oldData = await UOW.CustomerPointHistoryRepository.Get(CustomerPointHistory.Id);

                await UOW.CustomerPointHistoryRepository.Update(CustomerPointHistory);

                CustomerPointHistory = await UOW.CustomerPointHistoryRepository.Get(CustomerPointHistory.Id);
                await Logging.CreateAuditLog(CustomerPointHistory, oldData, nameof(CustomerPointHistoryService));
                return CustomerPointHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPointHistoryService));
            }
            return null;
        }

        public async Task<CustomerPointHistory> Delete(CustomerPointHistory CustomerPointHistory)
        {
            if (!await CustomerPointHistoryValidator.Delete(CustomerPointHistory))
                return CustomerPointHistory;

            try
            {
                await UOW.CustomerPointHistoryRepository.Delete(CustomerPointHistory);
                await Logging.CreateAuditLog(new { }, CustomerPointHistory, nameof(CustomerPointHistoryService));
                return CustomerPointHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPointHistoryService));
            }
            return null;
        }

        public async Task<List<CustomerPointHistory>> BulkDelete(List<CustomerPointHistory> CustomerPointHistories)
        {
            if (!await CustomerPointHistoryValidator.BulkDelete(CustomerPointHistories))
                return CustomerPointHistories;

            try
            {
                await UOW.CustomerPointHistoryRepository.BulkDelete(CustomerPointHistories);
                await Logging.CreateAuditLog(new { }, CustomerPointHistories, nameof(CustomerPointHistoryService));
                return CustomerPointHistories;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPointHistoryService));
            }
            return null;

        }
        
        public async Task<List<CustomerPointHistory>> Import(List<CustomerPointHistory> CustomerPointHistories)
        {
            if (!await CustomerPointHistoryValidator.Import(CustomerPointHistories))
                return CustomerPointHistories;
            try
            {
                await UOW.CustomerPointHistoryRepository.BulkMerge(CustomerPointHistories);

                await Logging.CreateAuditLog(CustomerPointHistories, new { }, nameof(CustomerPointHistoryService));
                return CustomerPointHistories;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerPointHistoryService));
            }
            return null;
        }     
        
        public async Task<CustomerPointHistoryFilter> ToFilter(CustomerPointHistoryFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerPointHistoryFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerPointHistoryFilter subFilter = new CustomerPointHistoryFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerId))
                        subFilter.CustomerId = FilterBuilder.Merge(subFilter.CustomerId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TotalPoint))
                        subFilter.TotalPoint = FilterBuilder.Merge(subFilter.TotalPoint, FilterPermissionDefinition.LongFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CurrentPoint))
                        subFilter.CurrentPoint = FilterBuilder.Merge(subFilter.CurrentPoint, FilterPermissionDefinition.LongFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ChangePoint))
                        subFilter.ChangePoint = FilterBuilder.Merge(subFilter.ChangePoint, FilterPermissionDefinition.LongFilter);
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
