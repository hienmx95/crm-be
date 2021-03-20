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

namespace CRM.Services.MUnitOfMeasure
{
    public interface IUnitOfMeasureService :  IServiceScoped
    {
        Task<int> Count(UnitOfMeasureFilter UnitOfMeasureFilter);
        Task<List<UnitOfMeasure>> List(UnitOfMeasureFilter UnitOfMeasureFilter);
        Task<UnitOfMeasure> Get(long Id);
        UnitOfMeasureFilter ToFilter(UnitOfMeasureFilter UnitOfMeasureFilter);
    }

    public class UnitOfMeasureService : BaseService, IUnitOfMeasureService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IUnitOfMeasureValidator UnitOfMeasureValidator;

        public UnitOfMeasureService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IUnitOfMeasureValidator UnitOfMeasureValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.UnitOfMeasureValidator = UnitOfMeasureValidator;
        }
        public async Task<int> Count(UnitOfMeasureFilter UnitOfMeasureFilter)
        {
            try
            {
                int result = await UOW.UnitOfMeasureRepository.Count(UnitOfMeasureFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(UnitOfMeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(UnitOfMeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<UnitOfMeasure>> List(UnitOfMeasureFilter UnitOfMeasureFilter)
        {
            try
            {
                List<UnitOfMeasure> UnitOfMeasures = await UOW.UnitOfMeasureRepository.List(UnitOfMeasureFilter);
                return UnitOfMeasures;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(UnitOfMeasureService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(UnitOfMeasureService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<UnitOfMeasure> Get(long Id)
        {
            UnitOfMeasure UnitOfMeasure = await UOW.UnitOfMeasureRepository.Get(Id);
            if (UnitOfMeasure == null)
                return null;
            return UnitOfMeasure;
        }
       
        public UnitOfMeasureFilter ToFilter(UnitOfMeasureFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<UnitOfMeasureFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                UnitOfMeasureFilter subFilter = new UnitOfMeasureFilter();
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
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
