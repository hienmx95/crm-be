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

namespace CRM.Services.MStoreGrouping
{
    public interface IStoreGroupingService :  IServiceScoped
    {
        Task<int> Count(StoreGroupingFilter StoreGroupingFilter);
        Task<List<StoreGrouping>> List(StoreGroupingFilter StoreGroupingFilter);
        Task<StoreGrouping> Get(long Id);
        StoreGroupingFilter ToFilter(StoreGroupingFilter StoreGroupingFilter);
    }

    public class StoreGroupingService : BaseService, IStoreGroupingService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IStoreGroupingValidator StoreGroupingValidator;

        public StoreGroupingService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IStoreGroupingValidator StoreGroupingValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.StoreGroupingValidator = StoreGroupingValidator;
        }
        public async Task<int> Count(StoreGroupingFilter StoreGroupingFilter)
        {
            try
            {
                int result = await UOW.StoreGroupingRepository.Count(StoreGroupingFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreGroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreGroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<StoreGrouping>> List(StoreGroupingFilter StoreGroupingFilter)
        {
            try
            {
                List<StoreGrouping> StoreGroupings = await UOW.StoreGroupingRepository.List(StoreGroupingFilter);
                return StoreGroupings;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreGroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreGroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<StoreGrouping> Get(long Id)
        {
            StoreGrouping StoreGrouping = await UOW.StoreGroupingRepository.Get(Id);
            if (StoreGrouping == null)
                return null;
            return StoreGrouping;
        }
       
        public StoreGroupingFilter ToFilter(StoreGroupingFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<StoreGroupingFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                StoreGroupingFilter subFilter = new StoreGroupingFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ParentId))
                        subFilter.ParentId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Path))
                        
                        
                        
                        
                        
                        
                        subFilter.Path = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Level))
                        
                        subFilter.Level = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
