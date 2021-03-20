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

namespace CRM.Services.MUnitOfMeasureGrouping
{
    public interface IUnitOfMeasureGroupingService :  IServiceScoped
    {
        Task<int> Count(UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter);
        Task<List<UnitOfMeasureGrouping>> List(UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter);
        Task<UnitOfMeasureGrouping> Get(long Id);
        UnitOfMeasureGroupingFilter ToFilter(UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter);
    }

    public class UnitOfMeasureGroupingService : BaseService, IUnitOfMeasureGroupingService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IUnitOfMeasureGroupingValidator UnitOfMeasureGroupingValidator;

        public UnitOfMeasureGroupingService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IUnitOfMeasureGroupingValidator UnitOfMeasureGroupingValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.UnitOfMeasureGroupingValidator = UnitOfMeasureGroupingValidator;
        }
        public async Task<int> Count(UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter)
        {
            try
            {
                int result = await UOW.UnitOfMeasureGroupingRepository.Count(UnitOfMeasureGroupingFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(UnitOfMeasureGroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(UnitOfMeasureGroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<UnitOfMeasureGrouping>> List(UnitOfMeasureGroupingFilter UnitOfMeasureGroupingFilter)
        {
            try
            {
                List<UnitOfMeasureGrouping> UnitOfMeasureGroupings = await UOW.UnitOfMeasureGroupingRepository.List(UnitOfMeasureGroupingFilter);
                return UnitOfMeasureGroupings;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(UnitOfMeasureGroupingService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(UnitOfMeasureGroupingService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<UnitOfMeasureGrouping> Get(long Id)
        {
            UnitOfMeasureGrouping UnitOfMeasureGrouping = await UOW.UnitOfMeasureGroupingRepository.Get(Id);
            if (UnitOfMeasureGrouping == null)
                return null;
            return UnitOfMeasureGrouping;
        }
       
        public UnitOfMeasureGroupingFilter ToFilter(UnitOfMeasureGroupingFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<UnitOfMeasureGroupingFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                UnitOfMeasureGroupingFilter subFilter = new UnitOfMeasureGroupingFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Description))
                        
                        
                        
                        
                        
                        
                        subFilter.Description = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.UnitOfMeasureId))
                        subFilter.UnitOfMeasureId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
