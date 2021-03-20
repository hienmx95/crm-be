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

namespace CRM.Services.MSLAEscalationFRTMail
{
    public interface ISLAEscalationFRTMailService :  IServiceScoped
    {
        Task<int> Count(SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter);
        Task<List<SLAEscalationFRTMail>> List(SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter);
        Task<SLAEscalationFRTMail> Get(long Id);
        Task<SLAEscalationFRTMail> Create(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<SLAEscalationFRTMail> Update(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<SLAEscalationFRTMail> Delete(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<List<SLAEscalationFRTMail>> BulkDelete(List<SLAEscalationFRTMail> SLAEscalationFRTMails);
        Task<List<SLAEscalationFRTMail>> Import(List<SLAEscalationFRTMail> SLAEscalationFRTMails);
        SLAEscalationFRTMailFilter ToFilter(SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter);
    }

    public class SLAEscalationFRTMailService : BaseService, ISLAEscalationFRTMailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAEscalationFRTMailValidator SLAEscalationFRTMailValidator;

        public SLAEscalationFRTMailService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAEscalationFRTMailValidator SLAEscalationFRTMailValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAEscalationFRTMailValidator = SLAEscalationFRTMailValidator;
        }
        public async Task<int> Count(SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter)
        {
            try
            {
                int result = await UOW.SLAEscalationFRTMailRepository.Count(SLAEscalationFRTMailFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationFRTMail>> List(SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter)
        {
            try
            {
                List<SLAEscalationFRTMail> SLAEscalationFRTMails = await UOW.SLAEscalationFRTMailRepository.List(SLAEscalationFRTMailFilter);
                return SLAEscalationFRTMails;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAEscalationFRTMail> Get(long Id)
        {
            SLAEscalationFRTMail SLAEscalationFRTMail = await UOW.SLAEscalationFRTMailRepository.Get(Id);
            if (SLAEscalationFRTMail == null)
                return null;
            return SLAEscalationFRTMail;
        }
       
        public async Task<SLAEscalationFRTMail> Create(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            if (!await SLAEscalationFRTMailValidator.Create(SLAEscalationFRTMail))
                return SLAEscalationFRTMail;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTMailRepository.Create(SLAEscalationFRTMail);
                await UOW.Commit();
                SLAEscalationFRTMail = await UOW.SLAEscalationFRTMailRepository.Get(SLAEscalationFRTMail.Id);
                await Logging.CreateAuditLog(SLAEscalationFRTMail, new { }, nameof(SLAEscalationFRTMailService));
                return SLAEscalationFRTMail;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationFRTMail> Update(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            if (!await SLAEscalationFRTMailValidator.Update(SLAEscalationFRTMail))
                return SLAEscalationFRTMail;
            try
            {
                var oldData = await UOW.SLAEscalationFRTMailRepository.Get(SLAEscalationFRTMail.Id);

                await UOW.Begin();
                await UOW.SLAEscalationFRTMailRepository.Update(SLAEscalationFRTMail);
                await UOW.Commit();

                SLAEscalationFRTMail = await UOW.SLAEscalationFRTMailRepository.Get(SLAEscalationFRTMail.Id);
                await Logging.CreateAuditLog(SLAEscalationFRTMail, oldData, nameof(SLAEscalationFRTMailService));
                return SLAEscalationFRTMail;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationFRTMail> Delete(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            if (!await SLAEscalationFRTMailValidator.Delete(SLAEscalationFRTMail))
                return SLAEscalationFRTMail;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTMailRepository.Delete(SLAEscalationFRTMail);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationFRTMail, nameof(SLAEscalationFRTMailService));
                return SLAEscalationFRTMail;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationFRTMail>> BulkDelete(List<SLAEscalationFRTMail> SLAEscalationFRTMails)
        {
            if (!await SLAEscalationFRTMailValidator.BulkDelete(SLAEscalationFRTMails))
                return SLAEscalationFRTMails;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTMailRepository.BulkDelete(SLAEscalationFRTMails);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationFRTMails, nameof(SLAEscalationFRTMailService));
                return SLAEscalationFRTMails;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAEscalationFRTMail>> Import(List<SLAEscalationFRTMail> SLAEscalationFRTMails)
        {
            if (!await SLAEscalationFRTMailValidator.Import(SLAEscalationFRTMails))
                return SLAEscalationFRTMails;
            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationFRTMailRepository.BulkMerge(SLAEscalationFRTMails);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAEscalationFRTMails, new { }, nameof(SLAEscalationFRTMailService));
                return SLAEscalationFRTMails;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAEscalationFRTMailFilter ToFilter(SLAEscalationFRTMailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAEscalationFRTMailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAEscalationFRTMailFilter subFilter = new SLAEscalationFRTMailFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAEscalationFRTId))
                        subFilter.SLAEscalationFRTId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Mail))
                        
                        
                        
                        
                        
                        
                        subFilter.Mail = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
