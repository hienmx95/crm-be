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

namespace CRM.Services.MSLAEscalation
{
    public interface ISLAEscalationService :  IServiceScoped
    {
        Task<int> Count(SLAEscalationFilter SLAEscalationFilter);
        Task<List<SLAEscalation>> List(SLAEscalationFilter SLAEscalationFilter);
        Task<SLAEscalation> Get(long Id);
        Task<SLAEscalation> Create(SLAEscalation SLAEscalation);
        Task<SLAEscalation> Update(SLAEscalation SLAEscalation);
        Task<SLAEscalation> Delete(SLAEscalation SLAEscalation);
        Task<List<SLAEscalation>> BulkDelete(List<SLAEscalation> SLAEscalations);
        Task<List<SLAEscalation>> Import(List<SLAEscalation> SLAEscalations);
        SLAEscalationFilter ToFilter(SLAEscalationFilter SLAEscalationFilter);
    }

    public class SLAEscalationService : BaseService, ISLAEscalationService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAEscalationValidator SLAEscalationValidator;

        public SLAEscalationService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAEscalationValidator SLAEscalationValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAEscalationValidator = SLAEscalationValidator;
        }
        public async Task<int> Count(SLAEscalationFilter SLAEscalationFilter)
        {
            try
            {
                int result = await UOW.SLAEscalationRepository.Count(SLAEscalationFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalation>> List(SLAEscalationFilter SLAEscalationFilter)
        {
            try
            {
                List<SLAEscalation> SLAEscalations = await UOW.SLAEscalationRepository.List(SLAEscalationFilter);
                return SLAEscalations;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAEscalation> Get(long Id)
        {
            SLAEscalation SLAEscalation = await UOW.SLAEscalationRepository.Get(Id);
            if (SLAEscalation == null)
                return null;
            return SLAEscalation;
        }
       
        public async Task<SLAEscalation> Create(SLAEscalation SLAEscalation)
        {
            if (!await SLAEscalationValidator.Create(SLAEscalation))
                return SLAEscalation;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationRepository.Create(SLAEscalation);
                await UOW.Commit();
                SLAEscalation = await UOW.SLAEscalationRepository.Get(SLAEscalation.Id);
                await Logging.CreateAuditLog(SLAEscalation, new { }, nameof(SLAEscalationService));
                return SLAEscalation;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalation> Update(SLAEscalation SLAEscalation)
        {
            if (!await SLAEscalationValidator.Update(SLAEscalation))
                return SLAEscalation;
            try
            {
                var oldData = await UOW.SLAEscalationRepository.Get(SLAEscalation.Id);

                await UOW.Begin();
                await UOW.SLAEscalationRepository.Update(SLAEscalation);
                await UOW.Commit();

                SLAEscalation = await UOW.SLAEscalationRepository.Get(SLAEscalation.Id);
                await Logging.CreateAuditLog(SLAEscalation, oldData, nameof(SLAEscalationService));
                return SLAEscalation;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalation> Delete(SLAEscalation SLAEscalation)
        {
            if (!await SLAEscalationValidator.Delete(SLAEscalation))
                return SLAEscalation;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationRepository.Delete(SLAEscalation);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalation, nameof(SLAEscalationService));
                return SLAEscalation;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalation>> BulkDelete(List<SLAEscalation> SLAEscalations)
        {
            if (!await SLAEscalationValidator.BulkDelete(SLAEscalations))
                return SLAEscalations;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationRepository.BulkDelete(SLAEscalations);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalations, nameof(SLAEscalationService));
                return SLAEscalations;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAEscalation>> Import(List<SLAEscalation> SLAEscalations)
        {
            if (!await SLAEscalationValidator.Import(SLAEscalations))
                return SLAEscalations;
            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationRepository.BulkMerge(SLAEscalations);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAEscalations, new { }, nameof(SLAEscalationService));
                return SLAEscalations;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAEscalationFilter ToFilter(SLAEscalationFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAEscalationFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAEscalationFilter subFilter = new SLAEscalationFilter();
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
