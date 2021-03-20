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

namespace CRM.Services.MTicketStatus
{
    public interface ITicketStatusService :  IServiceScoped
    {
        Task<int> Count(TicketStatusFilter TicketStatusFilter);
        Task<List<TicketStatus>> List(TicketStatusFilter TicketStatusFilter);
        Task<TicketStatus> Get(long Id);
        Task<TicketStatus> Create(TicketStatus TicketStatus);
        Task<TicketStatus> Update(TicketStatus TicketStatus);
        Task<TicketStatus> Delete(TicketStatus TicketStatus);
        Task<List<TicketStatus>> BulkDelete(List<TicketStatus> TicketStatuses);
        Task<List<TicketStatus>> Import(List<TicketStatus> TicketStatuses);
        TicketStatusFilter ToFilter(TicketStatusFilter TicketStatusFilter);
    }

    public class TicketStatusService : BaseService, ITicketStatusService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketStatusValidator TicketStatusValidator;

        public TicketStatusService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketStatusValidator TicketStatusValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketStatusValidator = TicketStatusValidator;
        }
        public async Task<int> Count(TicketStatusFilter TicketStatusFilter)
        {
            try
            {
                int result = await UOW.TicketStatusRepository.Count(TicketStatusFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketStatus>> List(TicketStatusFilter TicketStatusFilter)
        {
            try
            {
                TicketStatusFilter.OrderBy = TicketStatusOrder.OrderNumber;
                TicketStatusFilter.OrderType = OrderType.ASC;
                List<TicketStatus> TicketStatuss = await UOW.TicketStatusRepository.List(TicketStatusFilter);
                return TicketStatuss;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<TicketStatus> Get(long Id)
        {
            TicketStatus TicketStatus = await UOW.TicketStatusRepository.Get(Id);
            if (TicketStatus == null)
                return null;
            return TicketStatus;
        }
       
        public async Task<TicketStatus> Create(TicketStatus TicketStatus)
        {
            if (!await TicketStatusValidator.Create(TicketStatus))
                return TicketStatus;

            try
            {
                TicketStatusFilter TicketStatusFilter = new TicketStatusFilter
                {
                    Take = 1,
                    Selects = TicketStatusSelect.ALL,
                    OrderBy = TicketStatusOrder.OrderNumber,
                    OrderType = OrderType.DESC
                };
                await UOW.Begin();
                if (TicketStatus.OrderNumber == 0)
                {
                    List<TicketStatus> TicketStatuss = await UOW.TicketStatusRepository.List(TicketStatusFilter);
                    TicketStatus.OrderNumber = TicketStatuss.Any() ? TicketStatuss.Max(c => c.OrderNumber) + 1 : 1;
                }
                await UOW.TicketStatusRepository.Create(TicketStatus);
                await UOW.Commit();
                TicketStatus = await UOW.TicketStatusRepository.Get(TicketStatus.Id);
                await Logging.CreateAuditLog(TicketStatus, new { }, nameof(TicketStatusService));
                return TicketStatus;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketStatus> Update(TicketStatus TicketStatus)
        {
            if (!await TicketStatusValidator.Update(TicketStatus))
                return TicketStatus;
            try
            {
                var oldData = await UOW.TicketStatusRepository.Get(TicketStatus.Id);

                await UOW.Begin();
                await UOW.TicketStatusRepository.Update(TicketStatus);
                await UOW.Commit();

                TicketStatus = await UOW.TicketStatusRepository.Get(TicketStatus.Id);
                await Logging.CreateAuditLog(TicketStatus, oldData, nameof(TicketStatusService));
                return TicketStatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketStatus> Delete(TicketStatus TicketStatus)
        {
            if (!await TicketStatusValidator.Delete(TicketStatus))
                return TicketStatus;

            try
            {
                await UOW.Begin();
                await UOW.TicketStatusRepository.Delete(TicketStatus);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketStatus, nameof(TicketStatusService));
                return TicketStatus;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketStatus>> BulkDelete(List<TicketStatus> TicketStatuses)
        {
            if (!await TicketStatusValidator.BulkDelete(TicketStatuses))
                return TicketStatuses;

            try
            {
                await UOW.Begin();
                await UOW.TicketStatusRepository.BulkDelete(TicketStatuses);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketStatuses, nameof(TicketStatusService));
                return TicketStatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<TicketStatus>> Import(List<TicketStatus> TicketStatuses)
        {
            if (!await TicketStatusValidator.Import(TicketStatuses))
                return TicketStatuses;
            try
            {
                await UOW.Begin();
                await UOW.TicketStatusRepository.BulkMerge(TicketStatuses);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketStatuses, new { }, nameof(TicketStatusService));
                return TicketStatuses;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketStatusService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketStatusService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public TicketStatusFilter ToFilter(TicketStatusFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketStatusFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketStatusFilter subFilter = new TicketStatusFilter();
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
