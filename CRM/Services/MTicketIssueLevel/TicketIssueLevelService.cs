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

namespace CRM.Services.MTicketIssueLevel
{
    public interface ITicketIssueLevelService :  IServiceScoped
    {
        Task<int> Count(TicketIssueLevelFilter TicketIssueLevelFilter);
        Task<List<TicketIssueLevel>> List(TicketIssueLevelFilter TicketIssueLevelFilter);
        Task<TicketIssueLevel> Get(long Id);
        Task<TicketIssueLevel> Create(TicketIssueLevel TicketIssueLevel);
        Task<TicketIssueLevel> Update(TicketIssueLevel TicketIssueLevel);
        Task<TicketIssueLevel> Delete(TicketIssueLevel TicketIssueLevel);
        Task<List<TicketIssueLevel>> BulkDelete(List<TicketIssueLevel> TicketIssueLevels);
        Task<List<TicketIssueLevel>> Import(List<TicketIssueLevel> TicketIssueLevels);
        TicketIssueLevelFilter ToFilter(TicketIssueLevelFilter TicketIssueLevelFilter);
    }

    public class TicketIssueLevelService : BaseService, ITicketIssueLevelService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketIssueLevelValidator TicketIssueLevelValidator;

        public TicketIssueLevelService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketIssueLevelValidator TicketIssueLevelValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketIssueLevelValidator = TicketIssueLevelValidator;
        }
        public async Task<int> Count(TicketIssueLevelFilter TicketIssueLevelFilter)
        {
            try
            {
                int result = await UOW.TicketIssueLevelRepository.Count(TicketIssueLevelFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketIssueLevelService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketIssueLevelService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketIssueLevel>> List(TicketIssueLevelFilter TicketIssueLevelFilter)
        {
            try
            {
                List<TicketIssueLevel> TicketIssueLevels = await UOW.TicketIssueLevelRepository.List(TicketIssueLevelFilter);
                return TicketIssueLevels;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketIssueLevelService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketIssueLevelService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<TicketIssueLevel> Get(long Id)
        {
            TicketIssueLevel TicketIssueLevel = await UOW.TicketIssueLevelRepository.Get(Id);
            if (TicketIssueLevel == null)
                return null;
            return TicketIssueLevel;
        }
       
        public async Task<TicketIssueLevel> Create(TicketIssueLevel TicketIssueLevel)
        {
            if (!await TicketIssueLevelValidator.Create(TicketIssueLevel))
                return TicketIssueLevel;

            try
            {
                TicketIssueLevelFilter TicketIssueLevelFilter = new TicketIssueLevelFilter
                {
                    Take = 1,
                    Selects = TicketIssueLevelSelect.ALL,
                    OrderBy = TicketIssueLevelOrder.OrderNumber,
                    OrderType = OrderType.DESC
                };

                await UOW.Begin();
                if (TicketIssueLevel.OrderNumber == 0)
                {
                    List<TicketIssueLevel> TicketIssueLevels = await UOW.TicketIssueLevelRepository.List(TicketIssueLevelFilter);
                    TicketIssueLevel.OrderNumber = TicketIssueLevels.Any() ? TicketIssueLevels.Max(c => c.OrderNumber) + 1 : 1;
                }
                // Nhắc nhở xử lý
                List<SLAAlert> SLAAlerts = new List<SLAAlert>();
                if (TicketIssueLevel.SLAAlerts != null && TicketIssueLevel.SLAAlerts.Any())
                {
                    foreach (var SLAAlert in TicketIssueLevel.SLAAlerts)
                    {
                        var newObj = SLAAlert;
                        newObj.RowId = Guid.NewGuid();
                        if (!newObj.Time.HasValue)
                        {
                            newObj.Time = 15;
                            newObj.TimeUnitId = SLATimeUnitEnum.MINUTES.Id;
                        }
                        SLAAlerts.Add(newObj);
                    }
                }
                TicketIssueLevel.SLAAlerts = SLAAlerts;
                // Nhắc nhở phản hồi
                List<SLAAlertFRT> SLAAlertFRTs = new List<SLAAlertFRT>();
                if (TicketIssueLevel.SLAAlertFRTs != null && TicketIssueLevel.SLAAlertFRTs.Any())
                {
                    foreach (var SLAAlertFRT in TicketIssueLevel.SLAAlertFRTs)
                    {
                        var newObj = SLAAlertFRT;
                        newObj.RowId = Guid.NewGuid();
                        if (!newObj.Time.HasValue)
                        {
                            newObj.Time = 15;
                            newObj.TimeUnitId = SLATimeUnitEnum.MINUTES.Id;
                        }
                        SLAAlertFRTs.Add(newObj);
                    }
                }
                TicketIssueLevel.SLAAlertFRTs = SLAAlertFRTs;
                // Cảnh báo xử lý
                List<SLAEscalation> SLAEscalations = new List<SLAEscalation>();
                if (TicketIssueLevel.SLAEscalations != null && TicketIssueLevel.SLAEscalations.Any())
                {
                    foreach (var SLAEscalation in TicketIssueLevel.SLAEscalations)
                    {
                        var newObj = SLAEscalation;
                        newObj.RowId = Guid.NewGuid();
                        if (!newObj.Time.HasValue)
                        {
                            newObj.Time = 15;
                            newObj.TimeUnitId = SLATimeUnitEnum.MINUTES.Id;
                        }
                        SLAEscalations.Add(newObj);
                    }
                }
                TicketIssueLevel.SLAEscalations = SLAEscalations;
                // Cảnh báo phản hồi
                List<SLAEscalationFRT> SLAEscalationFRTs = new List<SLAEscalationFRT>();
                if (TicketIssueLevel.SLAEscalationFRTs != null && TicketIssueLevel.SLAEscalationFRTs.Any())
                {
                    foreach (var SLAEscalationFRT in TicketIssueLevel.SLAEscalationFRTs)
                    {
                        var newObj = SLAEscalationFRT;
                        newObj.RowId = Guid.NewGuid();
                        if (!newObj.Time.HasValue)
                        {
                            newObj.Time = 15;
                            newObj.TimeUnitId = SLATimeUnitEnum.MINUTES.Id;
                        }
                        SLAEscalationFRTs.Add(newObj);
                    }
                }
                TicketIssueLevel.SLAEscalationFRTs = SLAEscalationFRTs;

                await UOW.TicketIssueLevelRepository.Create(TicketIssueLevel);
                await UOW.Commit();
                TicketIssueLevel = await UOW.TicketIssueLevelRepository.Get(TicketIssueLevel.Id);
                await Logging.CreateAuditLog(TicketIssueLevel, new { }, nameof(TicketIssueLevelService));
                return TicketIssueLevel;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketIssueLevelService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketIssueLevelService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketIssueLevel> Update(TicketIssueLevel TicketIssueLevel)
        {
            if (!await TicketIssueLevelValidator.Update(TicketIssueLevel))
                return TicketIssueLevel;
            try
            {
                var oldData = await UOW.TicketIssueLevelRepository.Get(TicketIssueLevel.Id);

                await UOW.Begin();
                await UOW.TicketIssueLevelRepository.Update(TicketIssueLevel);
                await UOW.Commit();

                TicketIssueLevel = await UOW.TicketIssueLevelRepository.Get(TicketIssueLevel.Id);
                await Logging.CreateAuditLog(TicketIssueLevel, oldData, nameof(TicketIssueLevelService));
                return TicketIssueLevel;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketIssueLevelService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketIssueLevelService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketIssueLevel> Delete(TicketIssueLevel TicketIssueLevel)
        {
            if (!await TicketIssueLevelValidator.Delete(TicketIssueLevel))
                return TicketIssueLevel;

            try
            {
                await UOW.Begin();
                await UOW.TicketIssueLevelRepository.Delete(TicketIssueLevel);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketIssueLevel, nameof(TicketIssueLevelService));
                return TicketIssueLevel;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketIssueLevelService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketIssueLevelService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<TicketIssueLevel>> BulkDelete(List<TicketIssueLevel> TicketIssueLevels)
        {
            if (!await TicketIssueLevelValidator.BulkDelete(TicketIssueLevels))
                return TicketIssueLevels;

            try
            {
                await UOW.Begin();
                await UOW.TicketIssueLevelRepository.BulkDelete(TicketIssueLevels);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, TicketIssueLevels, nameof(TicketIssueLevelService));
                return TicketIssueLevels;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketIssueLevelService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketIssueLevelService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<TicketIssueLevel>> Import(List<TicketIssueLevel> TicketIssueLevels)
        {
            if (!await TicketIssueLevelValidator.Import(TicketIssueLevels))
                return TicketIssueLevels;
            try
            {
                await UOW.Begin();
                await UOW.TicketIssueLevelRepository.BulkMerge(TicketIssueLevels);
                await UOW.Commit();

                await Logging.CreateAuditLog(TicketIssueLevels, new { }, nameof(TicketIssueLevelService));
                return TicketIssueLevels;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketIssueLevelService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketIssueLevelService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public TicketIssueLevelFilter ToFilter(TicketIssueLevelFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketIssueLevelFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketIssueLevelFilter subFilter = new TicketIssueLevelFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketGroupId))
                        subFilter.TicketGroupId = FilterPermissionDefinition.IdFilter;                    
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                    
                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLA))
                        subFilter.SLA = FilterPermissionDefinition.LongFilter;
                }
            }
            return filter;
        }
    }
}
