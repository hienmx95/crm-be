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

namespace CRM.Services.MSLAAlertFRTMail
{
    public interface ISLAAlertFRTMailService :  IServiceScoped
    {
        Task<int> Count(SLAAlertFRTMailFilter SLAAlertFRTMailFilter);
        Task<List<SLAAlertFRTMail>> List(SLAAlertFRTMailFilter SLAAlertFRTMailFilter);
        Task<SLAAlertFRTMail> Get(long Id);
        Task<SLAAlertFRTMail> Create(SLAAlertFRTMail SLAAlertFRTMail);
        Task<SLAAlertFRTMail> Update(SLAAlertFRTMail SLAAlertFRTMail);
        Task<SLAAlertFRTMail> Delete(SLAAlertFRTMail SLAAlertFRTMail);
        Task<List<SLAAlertFRTMail>> BulkDelete(List<SLAAlertFRTMail> SLAAlertFRTMails);
        Task<List<SLAAlertFRTMail>> Import(List<SLAAlertFRTMail> SLAAlertFRTMails);
        SLAAlertFRTMailFilter ToFilter(SLAAlertFRTMailFilter SLAAlertFRTMailFilter);
    }

    public class SLAAlertFRTMailService : BaseService, ISLAAlertFRTMailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAAlertFRTMailValidator SLAAlertFRTMailValidator;

        public SLAAlertFRTMailService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAAlertFRTMailValidator SLAAlertFRTMailValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAAlertFRTMailValidator = SLAAlertFRTMailValidator;
        }
        public async Task<int> Count(SLAAlertFRTMailFilter SLAAlertFRTMailFilter)
        {
            try
            {
                int result = await UOW.SLAAlertFRTMailRepository.Count(SLAAlertFRTMailFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertFRTMail>> List(SLAAlertFRTMailFilter SLAAlertFRTMailFilter)
        {
            try
            {
                List<SLAAlertFRTMail> SLAAlertFRTMails = await UOW.SLAAlertFRTMailRepository.List(SLAAlertFRTMailFilter);
                return SLAAlertFRTMails;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAAlertFRTMail> Get(long Id)
        {
            SLAAlertFRTMail SLAAlertFRTMail = await UOW.SLAAlertFRTMailRepository.Get(Id);
            if (SLAAlertFRTMail == null)
                return null;
            return SLAAlertFRTMail;
        }
       
        public async Task<SLAAlertFRTMail> Create(SLAAlertFRTMail SLAAlertFRTMail)
        {
            if (!await SLAAlertFRTMailValidator.Create(SLAAlertFRTMail))
                return SLAAlertFRTMail;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTMailRepository.Create(SLAAlertFRTMail);
                await UOW.Commit();
                SLAAlertFRTMail = await UOW.SLAAlertFRTMailRepository.Get(SLAAlertFRTMail.Id);
                await Logging.CreateAuditLog(SLAAlertFRTMail, new { }, nameof(SLAAlertFRTMailService));
                return SLAAlertFRTMail;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertFRTMail> Update(SLAAlertFRTMail SLAAlertFRTMail)
        {
            if (!await SLAAlertFRTMailValidator.Update(SLAAlertFRTMail))
                return SLAAlertFRTMail;
            try
            {
                var oldData = await UOW.SLAAlertFRTMailRepository.Get(SLAAlertFRTMail.Id);

                await UOW.Begin();
                await UOW.SLAAlertFRTMailRepository.Update(SLAAlertFRTMail);
                await UOW.Commit();

                SLAAlertFRTMail = await UOW.SLAAlertFRTMailRepository.Get(SLAAlertFRTMail.Id);
                await Logging.CreateAuditLog(SLAAlertFRTMail, oldData, nameof(SLAAlertFRTMailService));
                return SLAAlertFRTMail;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertFRTMail> Delete(SLAAlertFRTMail SLAAlertFRTMail)
        {
            if (!await SLAAlertFRTMailValidator.Delete(SLAAlertFRTMail))
                return SLAAlertFRTMail;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTMailRepository.Delete(SLAAlertFRTMail);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertFRTMail, nameof(SLAAlertFRTMailService));
                return SLAAlertFRTMail;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertFRTMail>> BulkDelete(List<SLAAlertFRTMail> SLAAlertFRTMails)
        {
            if (!await SLAAlertFRTMailValidator.BulkDelete(SLAAlertFRTMails))
                return SLAAlertFRTMails;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTMailRepository.BulkDelete(SLAAlertFRTMails);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertFRTMails, nameof(SLAAlertFRTMailService));
                return SLAAlertFRTMails;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAAlertFRTMail>> Import(List<SLAAlertFRTMail> SLAAlertFRTMails)
        {
            if (!await SLAAlertFRTMailValidator.Import(SLAAlertFRTMails))
                return SLAAlertFRTMails;
            try
            {
                await UOW.Begin();
                await UOW.SLAAlertFRTMailRepository.BulkMerge(SLAAlertFRTMails);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAAlertFRTMails, new { }, nameof(SLAAlertFRTMailService));
                return SLAAlertFRTMails;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertFRTMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAAlertFRTMailFilter ToFilter(SLAAlertFRTMailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAAlertFRTMailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAAlertFRTMailFilter subFilter = new SLAAlertFRTMailFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAAlertFRTId))
                        subFilter.SLAAlertFRTId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Mail))
                        
                        
                        
                        
                        
                        
                        subFilter.Mail = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
