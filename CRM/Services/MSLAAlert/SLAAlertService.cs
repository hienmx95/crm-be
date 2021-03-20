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

namespace CRM.Services.MSLAAlert
{
    public interface ISLAAlertService :  IServiceScoped
    {
        Task<int> Count(SLAAlertFilter SLAAlertFilter);
        Task<List<SLAAlert>> List(SLAAlertFilter SLAAlertFilter);
        Task<SLAAlert> Get(long Id);
        Task<SLAAlert> Create(SLAAlert SLAAlert);
        Task<SLAAlert> Update(SLAAlert SLAAlert);
        Task<SLAAlert> Delete(SLAAlert SLAAlert);
        Task<List<SLAAlert>> BulkDelete(List<SLAAlert> SLAAlerts);
        Task<List<SLAAlert>> Import(List<SLAAlert> SLAAlerts);
        SLAAlertFilter ToFilter(SLAAlertFilter SLAAlertFilter);
    }

    public class SLAAlertService : BaseService, ISLAAlertService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAAlertValidator SLAAlertValidator;

        public SLAAlertService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAAlertValidator SLAAlertValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAAlertValidator = SLAAlertValidator;
        }
        public async Task<int> Count(SLAAlertFilter SLAAlertFilter)
        {
            try
            {
                int result = await UOW.SLAAlertRepository.Count(SLAAlertFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlert>> List(SLAAlertFilter SLAAlertFilter)
        {
            try
            {
                List<SLAAlert> SLAAlerts = await UOW.SLAAlertRepository.List(SLAAlertFilter);
                return SLAAlerts;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAAlert> Get(long Id)
        {
            SLAAlert SLAAlert = await UOW.SLAAlertRepository.Get(Id);
            if (SLAAlert == null)
                return null;
            return SLAAlert;
        }
       
        public async Task<SLAAlert> Create(SLAAlert SLAAlert)
        {
            if (!await SLAAlertValidator.Create(SLAAlert))
                return SLAAlert;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertRepository.Create(SLAAlert);
                await UOW.Commit();
                SLAAlert = await UOW.SLAAlertRepository.Get(SLAAlert.Id);
                await Logging.CreateAuditLog(SLAAlert, new { }, nameof(SLAAlertService));
                return SLAAlert;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlert> Update(SLAAlert SLAAlert)
        {
            if (!await SLAAlertValidator.Update(SLAAlert))
                return SLAAlert;
            try
            {
                var oldData = await UOW.SLAAlertRepository.Get(SLAAlert.Id);

                await UOW.Begin();
                await UOW.SLAAlertRepository.Update(SLAAlert);
                await UOW.Commit();

                SLAAlert = await UOW.SLAAlertRepository.Get(SLAAlert.Id);
                await Logging.CreateAuditLog(SLAAlert, oldData, nameof(SLAAlertService));
                return SLAAlert;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlert> Delete(SLAAlert SLAAlert)
        {
            if (!await SLAAlertValidator.Delete(SLAAlert))
                return SLAAlert;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertRepository.Delete(SLAAlert);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlert, nameof(SLAAlertService));
                return SLAAlert;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlert>> BulkDelete(List<SLAAlert> SLAAlerts)
        {
            if (!await SLAAlertValidator.BulkDelete(SLAAlerts))
                return SLAAlerts;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertRepository.BulkDelete(SLAAlerts);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlerts, nameof(SLAAlertService));
                return SLAAlerts;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAAlert>> Import(List<SLAAlert> SLAAlerts)
        {
            if (!await SLAAlertValidator.Import(SLAAlerts))
                return SLAAlerts;
            try
            {
                await UOW.Begin();
                await UOW.SLAAlertRepository.BulkMerge(SLAAlerts);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAAlerts, new { }, nameof(SLAAlertService));
                return SLAAlerts;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAAlertFilter ToFilter(SLAAlertFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAAlertFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAAlertFilter subFilter = new SLAAlertFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketIssueLevelId))
                        subFilter.TicketIssueLevelId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Time))
                        
                        subFilter.Time = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                }
            }
            return filter;
        }
    }
}
