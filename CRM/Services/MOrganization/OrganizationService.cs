using CRM.Common;
using CRM.Entities;
using CRM.Repositories;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Services.MOrganization
{
    public interface IOrganizationService : IServiceScoped
    {
        Task<int> Count(OrganizationFilter OrganizationFilter);
        Task<List<Organization>> List(OrganizationFilter OrganizationFilter);
        Task<Organization> Get(long Id);
        OrganizationFilter ToFilter(OrganizationFilter OrganizationFilter);
    }

    public class OrganizationService : BaseService, IOrganizationService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IOrganizationValidator OrganizationValidator;

        public OrganizationService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IOrganizationValidator OrganizationValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.OrganizationValidator = OrganizationValidator;
        }
        public async Task<int> Count(OrganizationFilter OrganizationFilter)
        {
            try
            {
                int result = await UOW.OrganizationRepository.Count(OrganizationFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex.InnerException, nameof(OrganizationService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }

        public async Task<List<Organization>> List(OrganizationFilter OrganizationFilter)
        {
            try
            {
                List<Organization> Organizations = await UOW.OrganizationRepository.List(OrganizationFilter);
                return Organizations;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex.InnerException, nameof(OrganizationService));
                if (ex.InnerException == null)
                    throw new MessageException(ex);
                else
                    throw new MessageException(ex.InnerException);
            }
        }
        public async Task<Organization> Get(long Id)
        {
            Organization Organization = await UOW.OrganizationRepository.Get(Id);
            if (Organization == null)
                return null;
            return Organization;
        }
        public OrganizationFilter ToFilter(OrganizationFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<OrganizationFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                OrganizationFilter subFilter = new OrganizationFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                }
            }
            return filter;
        }
    }
}
