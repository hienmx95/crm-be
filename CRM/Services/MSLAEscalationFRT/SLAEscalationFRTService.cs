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

namespace CRM.Services.MSLAEscalationFRT
{
    public interface ISLAEscalationFRTService :  IServiceScoped
    {
        Task<int> Count(SLAEscalationFRTFilter SLAEscalationFRTFilter);
        Task<List<SLAEscalationFRT>> List(SLAEscalationFRTFilter SLAEscalationFRTFilter);
        Task<SLAEscalationFRT> Get(long Id);
        Task<SLAEscalationFRT> Create(SLAEscalationFRT SLAEscalationFRT);
        Task<SLAEscalationFRT> Update(SLAEscalationFRT SLAEscalationFRT);
        Task<SLAEscalationFRT> Delete(SLAEscalationFRT SLAEscalationFRT);
        Task<List<SLAEscalationFRT>> BulkDelete(List<SLAEscalationFRT> SLAEscalationFRTs);
        Task<List<SLAEscalationFRT>> Import(List<SLAEscalationFRT> SLAEscalationFRTs);
        SLAEscalationFRTFilter ToFilter(SLAEscalationFRTFilter SLAEscalationFRTFilter);
    }

    public class SLAEscalationFRTService : BaseService, ISLAEscalationFRTService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAEscalationFRTValidator SLAEscalationFRTValidator;

        public SLAEscalationFRTService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAEscalationFRTValidator SLAEscalationFRTValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAEscalationFRTValidator = SLAEscalationFRTValidator;
        }
        public async Task<int> Count(SLAEscalationFRTFilter SLAEscalationFRTFilter)
        {
            try
            {
                int result = await UOW.SLAEscalationFRTRepository.Count(SLAEscalationFRTFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationFRT>> List(SLAEscalationFRTFilter SLAEscalationFRTFilter)
        {
            try
            {
                List<SLAEscalationFRT> SLAEscalationFRTs = await UOW.SLAEscalationFRTRepository.List(SLAEscalationFRTFilter);
                return SLAEscalationFRTs;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAEscalationFRT> Get(long Id)
        {
            SLAEscalationFRT SLAEscalationFRT = await UOW.SLAEscalationFRTRepository.Get(Id);
            if (SLAEscalationFRT == null)
                return null;
            return SLAEscalationFRT;
        }
       
        public async Task<SLAEscalationFRT> Create(SLAEscalationFRT SLAEscalationFRT)
        {
            if (!await SLAEscalationFRTValidator.Create(SLAEscalationFRT))
                return SLAEscalationFRT;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTRepository.Create(SLAEscalationFRT);
                await UOW.Commit();
                SLAEscalationFRT = await UOW.SLAEscalationFRTRepository.Get(SLAEscalationFRT.Id);
                await Logging.CreateAuditLog(SLAEscalationFRT, new { }, nameof(SLAEscalationFRTService));
                return SLAEscalationFRT;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationFRT> Update(SLAEscalationFRT SLAEscalationFRT)
        {
            if (!await SLAEscalationFRTValidator.Update(SLAEscalationFRT))
                return SLAEscalationFRT;
            try
            {
                var oldData = await UOW.SLAEscalationFRTRepository.Get(SLAEscalationFRT.Id);

                await UOW.Begin();
                await UOW.SLAEscalationFRTRepository.Update(SLAEscalationFRT);
                await UOW.Commit();

                SLAEscalationFRT = await UOW.SLAEscalationFRTRepository.Get(SLAEscalationFRT.Id);
                await Logging.CreateAuditLog(SLAEscalationFRT, oldData, nameof(SLAEscalationFRTService));
                return SLAEscalationFRT;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationFRT> Delete(SLAEscalationFRT SLAEscalationFRT)
        {
            if (!await SLAEscalationFRTValidator.Delete(SLAEscalationFRT))
                return SLAEscalationFRT;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTRepository.Delete(SLAEscalationFRT);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationFRT, nameof(SLAEscalationFRTService));
                return SLAEscalationFRT;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationFRT>> BulkDelete(List<SLAEscalationFRT> SLAEscalationFRTs)
        {
            if (!await SLAEscalationFRTValidator.BulkDelete(SLAEscalationFRTs))
                return SLAEscalationFRTs;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTRepository.BulkDelete(SLAEscalationFRTs);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationFRTs, nameof(SLAEscalationFRTService));
                return SLAEscalationFRTs;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAEscalationFRT>> Import(List<SLAEscalationFRT> SLAEscalationFRTs)
        {
            if (!await SLAEscalationFRTValidator.Import(SLAEscalationFRTs))
                return SLAEscalationFRTs;
            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTRepository.BulkMerge(SLAEscalationFRTs);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAEscalationFRTs, new { }, nameof(SLAEscalationFRTService));
                return SLAEscalationFRTs;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAEscalationFRTFilter ToFilter(SLAEscalationFRTFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAEscalationFRTFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAEscalationFRTFilter subFilter = new SLAEscalationFRTFilter();
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
