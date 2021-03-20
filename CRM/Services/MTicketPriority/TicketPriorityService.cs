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

namespace CRM.Services.MTicketPriority
{
    public interface ITicketPriorityService :  IServiceScoped
    {
        Task<int> Count(TicketPriorityFilter TicketPriorityFilter);
        Task<List<TicketPriority>> List(TicketPriorityFilter TicketPriorityFilter);
        Task<TicketPriority> Get(long Id);
        Task<TicketPriority> Create(TicketPriority TicketPriority);
        Task<TicketPriority> Update(TicketPriority TicketPriority);
        Task<TicketPriority> Delete(TicketPriority TicketPriority);
        Task<List<TicketPriority>> BulkDelete(List<TicketPriority> TicketPriorities);
        Task<List<TicketPriority>> Import(List<TicketPriority> TicketPriorities);
        TicketPriorityFilter ToFilter(TicketPriorityFilter TicketPriorityFilter);
    }

    public class TicketPriorityService : BaseService, ITicketPriorityService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketPriorityValidator TicketPriorityValidator;

        public TicketPriorityService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketPriorityValidator TicketPriorityValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketPriorityValidator = TicketPriorityValidator;
        }
        public async Task<int> Count(TicketPriorityFilter TicketPriorityFilter)
        {
            try
            {
                int result = await UOW.TicketPriorityRepository.Count(TicketPriorityFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketPriorityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketPriorityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketPriority>> List(TicketPriorityFilter TicketPriorityFilter)
        {
            try
            {
                List<TicketPriority> TicketPrioritys = await UOW.TicketPriorityRepository.List(TicketPriorityFilter);
                return TicketPrioritys;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketPriorityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketPriorityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<TicketPriority> Get(long Id)
        {
            TicketPriority TicketPriority = await UOW.TicketPriorityRepository.Get(Id);
            if (TicketPriority == null)
                return null;
            return TicketPriority;
        }
       
        public async Task<TicketPriority> Create(TicketPriority TicketPriority)
        {
            if (!await TicketPriorityValidator.Create(TicketPriority))
                return TicketPriority;

            try
            {

                TicketPriorityFilter TicketPriorityFilter = new TicketPriorityFilter
                {
                    Take = 1,
                    Selects = TicketPrioritySelect.ALL,
                    OrderBy = TicketPriorityOrder.OrderNumber,
                    OrderType = OrderType.DESC
                };

                await UOW.Begin();
                
                if (TicketPriority.OrderNumber == 0)
                {
                    List<TicketPriority> TicketPriorities = await UOW.TicketPriorityRepository.List(TicketPriorityFilter);
                    TicketPriority.OrderNumber = TicketPriorities.Any() ? TicketPriorities.Max(c => c.OrderNumber) + 1 : 1;
                }
                await UOW.TicketPriorityRepository.Create(TicketPriority);
                await UOW.Commit();
                TicketPriority = await UOW.TicketPriorityRepository.Get(TicketPriority.Id);
                await Logging.CreateAuditLog(TicketPriority, new { }, nameof(TicketPriorityService));
                return TicketPriority;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketPriorityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketPriorityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketPriority> Update(TicketPriority TicketPriority)
        {
            if (!await TicketPriorityValidator.Update(TicketPriority))
                return TicketPriority;
            try
            {
                var oldData = await UOW.TicketPriorityRepository.Get(TicketPriority.Id);

                await UOW.Begin();
                await UOW.TicketPriorityRepository.Update(TicketPriority);
                await UOW.Commit();

                TicketPriority = await UOW.TicketPriorityRepository.Get(TicketPriority.Id);
                await Logging.CreateAuditLog(TicketPriority, oldData, nameof(TicketPriorityService));
                return TicketPriority;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketPriorityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketPriorityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketPriority> Delete(TicketPriority TicketPriority)
        {
            if (!await TicketPriorityValidator.Delete(TicketPriority))
                return TicketPriority;

            try
            {
                await UOW.Begin();
                await UOW.TicketPriorityRepository.Delete(TicketPriority);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketPriority, nameof(TicketPriorityService));
                return TicketPriority;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketPriorityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketPriorityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketPriority>> BulkDelete(List<TicketPriority> TicketPriorities)
        {
            if (!await TicketPriorityValidator.BulkDelete(TicketPriorities))
                return TicketPriorities;

            try
            {
                await UOW.Begin();
                await UOW.TicketPriorityRepository.BulkDelete(TicketPriorities);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketPriorities, nameof(TicketPriorityService));
                return TicketPriorities;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketPriorityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketPriorityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<TicketPriority>> Import(List<TicketPriority> TicketPriorities)
        {
            if (!await TicketPriorityValidator.Import(TicketPriorities))
                return TicketPriorities;
            try
            {
                await UOW.Begin();
                await UOW.TicketPriorityRepository.BulkMerge(TicketPriorities);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketPriorities, new { }, nameof(TicketPriorityService));
                return TicketPriorities;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketPriorityService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketPriorityService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public TicketPriorityFilter ToFilter(TicketPriorityFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketPriorityFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketPriorityFilter subFilter = new TicketPriorityFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrderNumber))
                        
                        subFilter.OrderNumber = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ColorCode))
                        
                        
                        
                        
                        
                        
                        subFilter.ColorCode = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
