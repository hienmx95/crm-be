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

namespace CRM.Services.MSLAAlertFRT
{
    public interface ISLAAlertFRTService :  IServiceScoped
    {
        Task<int> Count(SLAAlertFRTFilter SLAAlertFRTFilter);
        Task<List<SLAAlertFRT>> List(SLAAlertFRTFilter SLAAlertFRTFilter);
        Task<SLAAlertFRT> Get(long Id);
        Task<SLAAlertFRT> Create(SLAAlertFRT SLAAlertFRT);
        Task<SLAAlertFRT> Update(SLAAlertFRT SLAAlertFRT);
        Task<SLAAlertFRT> Delete(SLAAlertFRT SLAAlertFRT);
        Task<List<SLAAlertFRT>> BulkDelete(List<SLAAlertFRT> SLAAlertFRTs);
        Task<List<SLAAlertFRT>> Import(List<SLAAlertFRT> SLAAlertFRTs);
        SLAAlertFRTFilter ToFilter(SLAAlertFRTFilter SLAAlertFRTFilter);
    }

    public class SLAAlertFRTService : BaseService, ISLAAlertFRTService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAAlertFRTValidator SLAAlertFRTValidator;

        public SLAAlertFRTService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAAlertFRTValidator SLAAlertFRTValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAAlertFRTValidator = SLAAlertFRTValidator;
        }
        public async Task<int> Count(SLAAlertFRTFilter SLAAlertFRTFilter)
        {
            try
            {
                int result = await UOW.SLAAlertFRTRepository.Count(SLAAlertFRTFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertFRT>> List(SLAAlertFRTFilter SLAAlertFRTFilter)
        {
            try
            {
                List<SLAAlertFRT> SLAAlertFRTs = await UOW.SLAAlertFRTRepository.List(SLAAlertFRTFilter);
                return SLAAlertFRTs;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAAlertFRT> Get(long Id)
        {
            SLAAlertFRT SLAAlertFRT = await UOW.SLAAlertFRTRepository.Get(Id);
            if (SLAAlertFRT == null)
                return null;
            return SLAAlertFRT;
        }
       
        public async Task<SLAAlertFRT> Create(SLAAlertFRT SLAAlertFRT)
        {
            if (!await SLAAlertFRTValidator.Create(SLAAlertFRT))
                return SLAAlertFRT;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTRepository.Create(SLAAlertFRT);
                await UOW.Commit();
                SLAAlertFRT = await UOW.SLAAlertFRTRepository.Get(SLAAlertFRT.Id);
                await Logging.CreateAuditLog(SLAAlertFRT, new { }, nameof(SLAAlertFRTService));
                return SLAAlertFRT;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertFRT> Update(SLAAlertFRT SLAAlertFRT)
        {
            if (!await SLAAlertFRTValidator.Update(SLAAlertFRT))
                return SLAAlertFRT;
            try
            {
                var oldData = await UOW.SLAAlertFRTRepository.Get(SLAAlertFRT.Id);

                await UOW.Begin();
                await UOW.SLAAlertFRTRepository.Update(SLAAlertFRT);
                await UOW.Commit();

                SLAAlertFRT = await UOW.SLAAlertFRTRepository.Get(SLAAlertFRT.Id);
                await Logging.CreateAuditLog(SLAAlertFRT, oldData, nameof(SLAAlertFRTService));
                return SLAAlertFRT;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertFRT> Delete(SLAAlertFRT SLAAlertFRT)
        {
            if (!await SLAAlertFRTValidator.Delete(SLAAlertFRT))
                return SLAAlertFRT;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTRepository.Delete(SLAAlertFRT);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertFRT, nameof(SLAAlertFRTService));
                return SLAAlertFRT;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertFRT>> BulkDelete(List<SLAAlertFRT> SLAAlertFRTs)
        {
            if (!await SLAAlertFRTValidator.BulkDelete(SLAAlertFRTs))
                return SLAAlertFRTs;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTRepository.BulkDelete(SLAAlertFRTs);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertFRTs, nameof(SLAAlertFRTService));
                return SLAAlertFRTs;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAAlertFRT>> Import(List<SLAAlertFRT> SLAAlertFRTs)
        {
            if (!await SLAAlertFRTValidator.Import(SLAAlertFRTs))
                return SLAAlertFRTs;
            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTRepository.BulkMerge(SLAAlertFRTs);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAAlertFRTs, new { }, nameof(SLAAlertFRTService));
                return SLAAlertFRTs;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAAlertFRTFilter ToFilter(SLAAlertFRTFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAAlertFRTFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAAlertFRTFilter subFilter = new SLAAlertFRTFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketIssueLevelId))
                        subFilter.TicketIssueLevelId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Time))
                        
                        subFilter.Time = FilterPermissionDefinition.LongFilter;
                        
                        
                        
                        
                        
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TimeUnitId))
                        subFilter.TimeUnitId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SmsTemplateId))
                        subFilter.SmsTemplateId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.MailTemplateId))
                        subFilter.MailTemplateId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
