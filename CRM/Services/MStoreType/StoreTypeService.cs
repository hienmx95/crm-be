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

namespace CRM.Services.MStoreType
{
    public interface IStoreTypeService :  IServiceScoped
    {
        Task<int> Count(StoreTypeFilter StoreTypeFilter);
        Task<List<StoreType>> List(StoreTypeFilter StoreTypeFilter);
        Task<StoreType> Get(long Id);
        StoreTypeFilter ToFilter(StoreTypeFilter StoreTypeFilter);
    }

    public class StoreTypeService : BaseService, IStoreTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IStoreTypeValidator StoreTypeValidator;

        public StoreTypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IStoreTypeValidator StoreTypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.StoreTypeValidator = StoreTypeValidator;
        }
        public async Task<int> Count(StoreTypeFilter StoreTypeFilter)
        {
            try
            {
                int result = await UOW.StoreTypeRepository.Count(StoreTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<StoreType>> List(StoreTypeFilter StoreTypeFilter)
        {
            try
            {
                List<StoreType> StoreTypes = await UOW.StoreTypeRepository.List(StoreTypeFilter);
                return StoreTypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(StoreTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(StoreTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<StoreType> Get(long Id)
        {
            StoreType StoreType = await UOW.StoreTypeRepository.Get(Id);
            if (StoreType == null)
                return null;
            return StoreType;
        }
       
        public StoreTypeFilter ToFilter(StoreTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<StoreTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                StoreTypeFilter subFilter = new StoreTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ColorId))
                        subFilter.ColorId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
