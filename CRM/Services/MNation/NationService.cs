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

namespace CRM.Services.MNation
{
    public interface INationService : IServiceScoped
    {
        Task<int> Count(NationFilter NationFilter);
        Task<List<Nation>> List(NationFilter NationFilter);
        Task<Nation> Get(long Id);
        NationFilter ToFilter(NationFilter NationFilter);
    }

    public class NationService : BaseService, INationService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private INationValidator NationValidator;

        public NationService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            INationValidator NationValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.NationValidator = NationValidator;
        }
        public async Task<int> Count(NationFilter NationFilter)
        {
            try
            {
                int result = await UOW.NationRepository.Count(NationFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(NationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(NationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Nation>> List(NationFilter NationFilter)
        {
            try
            {
                List<Nation> Nations = await UOW.NationRepository.List(NationFilter);
                return Nations;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(NationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(NationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Nation> Get(long Id)
        {
            Nation Nation = await UOW.NationRepository.Get(Id);
            if (Nation == null)
                return null;
            return Nation;
        }

        public NationFilter ToFilter(NationFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<NationFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                NationFilter subFilter = new NationFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Priority))
                        subFilter.Priority = FilterPermissionDefinition.LongFilter;
                }
            }
            return filter;
        }
    }
}
