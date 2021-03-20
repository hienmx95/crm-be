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

namespace CRM.Services.MTaxType
{
    public interface ITaxTypeService :  IServiceScoped
    {
        Task<int> Count(TaxTypeFilter TaxTypeFilter);
        Task<List<TaxType>> List(TaxTypeFilter TaxTypeFilter);
        Task<TaxType> Get(long Id);
        TaxTypeFilter ToFilter(TaxTypeFilter TaxTypeFilter);
    }

    public class TaxTypeService : BaseService, ITaxTypeService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITaxTypeValidator TaxTypeValidator;

        public TaxTypeService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITaxTypeValidator TaxTypeValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TaxTypeValidator = TaxTypeValidator;
        }
        public async Task<int> Count(TaxTypeFilter TaxTypeFilter)
        {
            try
            {
                int result = await UOW.TaxTypeRepository.Count(TaxTypeFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TaxTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TaxTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TaxType>> List(TaxTypeFilter TaxTypeFilter)
        {
            try
            {
                List<TaxType> TaxTypes = await UOW.TaxTypeRepository.List(TaxTypeFilter);
                return TaxTypes;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TaxTypeService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TaxTypeService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<TaxType> Get(long Id)
        {
            TaxType TaxType = await UOW.TaxTypeRepository.Get(Id);
            if (TaxType == null)
                return null;
            return TaxType;
        }
       
        public TaxTypeFilter ToFilter(TaxTypeFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TaxTypeFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TaxTypeFilter subFilter = new TaxTypeFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Percentage))
                        
                        
                        subFilter.Percentage = FilterPermissionDefinition.DecimalFilter;
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
