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

namespace CRM.Services.MCompanyStatus
{
    public interface ICompanyStatusService :  IServiceScoped
    {
        Task<int> Count(CompanyStatusFilter CompanyStatusFilter);
        Task<List<CompanyStatus>> List(CompanyStatusFilter CompanyStatusFilter);
        Task<CompanyStatus> Get(long Id);
        Task<CompanyStatusFilter> ToFilter(CompanyStatusFilter CompanyStatusFilter);
    }

    public class CompanyStatusService : BaseService, ICompanyStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICompanyStatusValidator CompanyStatusValidator;

        public CompanyStatusService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICompanyStatusValidator CompanyStatusValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CompanyStatusValidator = CompanyStatusValidator;
        }
        public async Task<int> Count(CompanyStatusFilter CompanyStatusFilter)
        {
            try
            {
                int result = await UOW.CompanyStatusRepository.Count(CompanyStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyStatusService));
            }
            return 0;
        }

        public async Task<List<CompanyStatus>> List(CompanyStatusFilter CompanyStatusFilter)
        {
            try
            {
                List<CompanyStatus> CompanyStatuss = await UOW.CompanyStatusRepository.List(CompanyStatusFilter);
                return CompanyStatuss;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyStatusService));
            }
            return null;
        }
        
        public async Task<CompanyStatus> Get(long Id)
        {
            CompanyStatus CompanyStatus = await UOW.CompanyStatusRepository.Get(Id);
            if (CompanyStatus == null)
                return null;
            return CompanyStatus;
        }
        
        public async Task<CompanyStatusFilter> ToFilter(CompanyStatusFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CompanyStatusFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CompanyStatusFilter subFilter = new CompanyStatusFilter();
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
