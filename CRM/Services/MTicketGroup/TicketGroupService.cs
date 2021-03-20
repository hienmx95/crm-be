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

namespace CRM.Services.MTicketGroup
{
    public interface ITicketGroupService :  IServiceScoped
    {
        Task<int> Count(TicketGroupFilter TicketGroupFilter);
        Task<List<TicketGroup>> List(TicketGroupFilter TicketGroupFilter);
        Task<TicketGroup> Get(long Id);
        Task<TicketGroup> Create(TicketGroup TicketGroup);
        Task<TicketGroup> Update(TicketGroup TicketGroup);
        Task<TicketGroup> Delete(TicketGroup TicketGroup);
        Task<List<TicketGroup>> BulkDelete(List<TicketGroup> TicketGroups);
        Task<List<TicketGroup>> Import(List<TicketGroup> TicketGroups);
        TicketGroupFilter ToFilter(TicketGroupFilter TicketGroupFilter);
    }

    public class TicketGroupService : BaseService, ITicketGroupService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketGroupValidator TicketGroupValidator;

        public TicketGroupService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketGroupValidator TicketGroupValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketGroupValidator = TicketGroupValidator;
        }
        public async Task<int> Count(TicketGroupFilter TicketGroupFilter)
        {
            try
            {
                int result = await UOW.TicketGroupRepository.Count(TicketGroupFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketGroup>> List(TicketGroupFilter TicketGroupFilter)
        {
            try
            {
                List<TicketGroup> TicketGroups = await UOW.TicketGroupRepository.List(TicketGroupFilter);
                return TicketGroups;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<TicketGroup> Get(long Id)
        {
            TicketGroup TicketGroup = await UOW.TicketGroupRepository.Get(Id);
            if (TicketGroup == null)
                return null;
            return TicketGroup;
        }
       
        public async Task<TicketGroup> Create(TicketGroup TicketGroup)
        {
            if (!await TicketGroupValidator.Create(TicketGroup))
                return TicketGroup;

            try
            {
                TicketGroupFilter TicketGroupFilter = new TicketGroupFilter
                {
                    Take = 1,
                    Selects = TicketGroupSelect.ALL,
                    OrderBy = TicketGroupOrder.OrderNumber,
                    OrderType = OrderType.DESC
                };
                await UOW.Begin();
                if (TicketGroup.OrderNumber == 0)
                {
                    List<TicketGroup> ticketGroups = await UOW.TicketGroupRepository.List(TicketGroupFilter);
                    TicketGroup.OrderNumber = ticketGroups.Any() ? ticketGroups.Max(c => c.OrderNumber) + 1 : 1;
                }
                await UOW.TicketGroupRepository.Create(TicketGroup);
                await UOW.Commit();
                TicketGroup = await UOW.TicketGroupRepository.Get(TicketGroup.Id);
                await Logging.CreateAuditLog(TicketGroup, new { }, nameof(TicketGroupService));
                return TicketGroup;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketGroup> Update(TicketGroup TicketGroup)
        {
            if (!await TicketGroupValidator.Update(TicketGroup))
                return TicketGroup;
            try
            {
                var oldData = await UOW.TicketGroupRepository.Get(TicketGroup.Id);

                await UOW.Begin();
                await UOW.TicketGroupRepository.Update(TicketGroup);
                await UOW.Commit();

                TicketGroup = await UOW.TicketGroupRepository.Get(TicketGroup.Id);
                await Logging.CreateAuditLog(TicketGroup, oldData, nameof(TicketGroupService));
                return TicketGroup;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketGroup> Delete(TicketGroup TicketGroup)
        {
            if (!await TicketGroupValidator.Delete(TicketGroup))
                return TicketGroup;

            try
            {
                await UOW.Begin();
                await UOW.TicketGroupRepository.Delete(TicketGroup);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketGroup, nameof(TicketGroupService));
                return TicketGroup;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketGroup>> BulkDelete(List<TicketGroup> TicketGroups)
        {
            if (!await TicketGroupValidator.BulkDelete(TicketGroups))
                return TicketGroups;

            try
            {
                await UOW.Begin();
                await UOW.TicketGroupRepository.BulkDelete(TicketGroups);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketGroups, nameof(TicketGroupService));
                return TicketGroups;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<TicketGroup>> Import(List<TicketGroup> TicketGroups)
        {
            if (!await TicketGroupValidator.Import(TicketGroups))
                return TicketGroups;
            try
            {
                await UOW.Begin();
                await UOW.TicketGroupRepository.BulkMerge(TicketGroups);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketGroups, new { }, nameof(TicketGroupService));
                return TicketGroups;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketGroupService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketGroupService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public TicketGroupFilter ToFilter(TicketGroupFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketGroupFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketGroupFilter subFilter = new TicketGroupFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrderNumber))
                        subFilter.OrderNumber = FilterPermissionDefinition.LongFilter;
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                    
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketTypeId))
                        subFilter.TicketTypeId = FilterPermissionDefinition.IdFilter;                
                }
            }
            return filter;
        }
    }
}
