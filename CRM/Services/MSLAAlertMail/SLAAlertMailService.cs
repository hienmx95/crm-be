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

namespace CRM.Services.MSLAAlertMail
{
    public interface ISLAAlertMailService :  IServiceScoped
    {
        Task<int> Count(SLAAlertMailFilter SLAAlertMailFilter);
        Task<List<SLAAlertMail>> List(SLAAlertMailFilter SLAAlertMailFilter);
        Task<SLAAlertMail> Get(long Id);
        Task<SLAAlertMail> Create(SLAAlertMail SLAAlertMail);
        Task<SLAAlertMail> Update(SLAAlertMail SLAAlertMail);
        Task<SLAAlertMail> Delete(SLAAlertMail SLAAlertMail);
        Task<List<SLAAlertMail>> BulkDelete(List<SLAAlertMail> SLAAlertMails);
        Task<List<SLAAlertMail>> Import(List<SLAAlertMail> SLAAlertMails);
        SLAAlertMailFilter ToFilter(SLAAlertMailFilter SLAAlertMailFilter);
    }

    public class SLAAlertMailService : BaseService, ISLAAlertMailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAAlertMailValidator SLAAlertMailValidator;

        public SLAAlertMailService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAAlertMailValidator SLAAlertMailValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAAlertMailValidator = SLAAlertMailValidator;
        }
        public async Task<int> Count(SLAAlertMailFilter SLAAlertMailFilter)
        {
            try
            {
                int result = await UOW.SLAAlertMailRepository.Count(SLAAlertMailFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertMail>> List(SLAAlertMailFilter SLAAlertMailFilter)
        {
            try
            {
                List<SLAAlertMail> SLAAlertMails = await UOW.SLAAlertMailRepository.List(SLAAlertMailFilter);
                return SLAAlertMails;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAAlertMail> Get(long Id)
        {
            SLAAlertMail SLAAlertMail = await UOW.SLAAlertMailRepository.Get(Id);
            if (SLAAlertMail == null)
                return null;
            return SLAAlertMail;
        }
       
        public async Task<SLAAlertMail> Create(SLAAlertMail SLAAlertMail)
        {
            if (!await SLAAlertMailValidator.Create(SLAAlertMail))
                return SLAAlertMail;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertMailRepository.Create(SLAAlertMail);
                await UOW.Commit();
                SLAAlertMail = await UOW.SLAAlertMailRepository.Get(SLAAlertMail.Id);
                await Logging.CreateAuditLog(SLAAlertMail, new { }, nameof(SLAAlertMailService));
                return SLAAlertMail;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertMail> Update(SLAAlertMail SLAAlertMail)
        {
            if (!await SLAAlertMailValidator.Update(SLAAlertMail))
                return SLAAlertMail;
            try
            {
                var oldData = await UOW.SLAAlertMailRepository.Get(SLAAlertMail.Id);

                await UOW.Begin();
                await UOW.SLAAlertMailRepository.Update(SLAAlertMail);
                await UOW.Commit();

                SLAAlertMail = await UOW.SLAAlertMailRepository.Get(SLAAlertMail.Id);
                await Logging.CreateAuditLog(SLAAlertMail, oldData, nameof(SLAAlertMailService));
                return SLAAlertMail;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAAlertMail> Delete(SLAAlertMail SLAAlertMail)
        {
            if (!await SLAAlertMailValidator.Delete(SLAAlertMail))
                return SLAAlertMail;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertMailRepository.Delete(SLAAlertMail);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertMail, nameof(SLAAlertMailService));
                return SLAAlertMail;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAAlertMail>> BulkDelete(List<SLAAlertMail> SLAAlertMails)
        {
            if (!await SLAAlertMailValidator.BulkDelete(SLAAlertMails))
                return SLAAlertMails;

            try
            {
                await UOW.Begin();
                await UOW.SLAAlertMailRepository.BulkDelete(SLAAlertMails);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAAlertMails, nameof(SLAAlertMailService));
                return SLAAlertMails;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAAlertMail>> Import(List<SLAAlertMail> SLAAlertMails)
        {
            if (!await SLAAlertMailValidator.Import(SLAAlertMails))
                return SLAAlertMails;
            try
            {
                await UOW.Begin();
                await UOW.SLAAlertMailRepository.BulkMerge(SLAAlertMails);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAAlertMails, new { }, nameof(SLAAlertMailService));
                return SLAAlertMails;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAAlertMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAAlertMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAAlertMailFilter ToFilter(SLAAlertMailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAAlertMailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAAlertMailFilter subFilter = new SLAAlertMailFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAAlertId))
                        subFilter.SLAAlertId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Mail))
                        
                        
                        
                        
                        
                        
                        subFilter.Mail = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
