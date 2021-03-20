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

namespace CRM.Services.MTicketSource
{
    public interface ITicketSourceService :  IServiceScoped
    {
        Task<int> Count(TicketSourceFilter TicketSourceFilter);
        Task<List<TicketSource>> List(TicketSourceFilter TicketSourceFilter);
        Task<TicketSource> Get(long Id);
        Task<TicketSource> Create(TicketSource TicketSource);
        Task<TicketSource> Update(TicketSource TicketSource);
        Task<TicketSource> Delete(TicketSource TicketSource);
        Task<List<TicketSource>> BulkDelete(List<TicketSource> TicketSources);
        Task<List<TicketSource>> Import(List<TicketSource> TicketSources);
        TicketSourceFilter ToFilter(TicketSourceFilter TicketSourceFilter);
    }

    public class TicketSourceService : BaseService, ITicketSourceService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketSourceValidator TicketSourceValidator;

        public TicketSourceService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketSourceValidator TicketSourceValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketSourceValidator = TicketSourceValidator;
        }
        public async Task<int> Count(TicketSourceFilter TicketSourceFilter)
        {
            try
            {
                int result = await UOW.TicketSourceRepository.Count(TicketSourceFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketSourceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketSourceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketSource>> List(TicketSourceFilter TicketSourceFilter)
        {
            try
            {
                List<TicketSource> TicketSources = await UOW.TicketSourceRepository.List(TicketSourceFilter);
                return TicketSources;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketSourceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketSourceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<TicketSource> Get(long Id)
        {
            TicketSource TicketSource = await UOW.TicketSourceRepository.Get(Id);
            if (TicketSource == null)
                return null;
            return TicketSource;
        }
       
        public async Task<TicketSource> Create(TicketSource TicketSource)
        {
            if (!await TicketSourceValidator.Create(TicketSource))
                return TicketSource;

            try
            {
                await UOW.Begin();
                TicketSourceFilter TicketSourceFilter = new TicketSourceFilter
                {
                    Take = 1,
                    Selects = TicketSourceSelect.ALL,
                    OrderBy = TicketSourceOrder.OrderNumber,
                    OrderType = OrderType.DESC
                };
                if (TicketSource.OrderNumber == 0)
                {
                    List<TicketSource> TicketSources = await UOW.TicketSourceRepository.List(TicketSourceFilter);
                    TicketSource.OrderNumber = TicketSources.Any() ? TicketSources.Max(c => c.OrderNumber) + 1 : 1;
                }
                
                await UOW.TicketSourceRepository.Create(TicketSource);
                await UOW.Commit();
                TicketSource = await UOW.TicketSourceRepository.Get(TicketSource.Id);
                await Logging.CreateAuditLog(TicketSource, new { }, nameof(TicketSourceService));
                return TicketSource;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketSourceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketSourceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketSource> Update(TicketSource TicketSource)
        {
            if (!await TicketSourceValidator.Update(TicketSource))
                return TicketSource;
            try
            {
                var oldData = await UOW.TicketSourceRepository.Get(TicketSource.Id);

                await UOW.Begin();
                await UOW.TicketSourceRepository.Update(TicketSource);
                await UOW.Commit();

                TicketSource = await UOW.TicketSourceRepository.Get(TicketSource.Id);
                await Logging.CreateAuditLog(TicketSource, oldData, nameof(TicketSourceService));
                return TicketSource;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketSourceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketSourceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketSource> Delete(TicketSource TicketSource)
        {
            if (!await TicketSourceValidator.Delete(TicketSource))
                return TicketSource;

            try
            {
                await UOW.Begin();
                await UOW.TicketSourceRepository.Delete(TicketSource);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketSource, nameof(TicketSourceService));
                return TicketSource;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketSourceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketSourceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketSource>> BulkDelete(List<TicketSource> TicketSources)
        {
            if (!await TicketSourceValidator.BulkDelete(TicketSources))
                return TicketSources;

            try
            {
                await UOW.Begin();
                await UOW.TicketSourceRepository.BulkDelete(TicketSources);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketSources, nameof(TicketSourceService));
                return TicketSources;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketSourceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketSourceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<TicketSource>> Import(List<TicketSource> TicketSources)
        {
            if (!await TicketSourceValidator.Import(TicketSources))
                return TicketSources;
            try
            {
                await UOW.Begin();
                await UOW.TicketSourceRepository.BulkMerge(TicketSources);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketSources, new { }, nameof(TicketSourceService));
                return TicketSources;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketSourceService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketSourceService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public TicketSourceFilter ToFilter(TicketSourceFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketSourceFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketSourceFilter subFilter = new TicketSourceFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrderNumber))
                        
                        subFilter.OrderNumber = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
