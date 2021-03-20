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

namespace CRM.Services.MSLAEscalationMail
{
    public interface ISLAEscalationMailService :  IServiceScoped
    {
        Task<int> Count(SLAEscalationMailFilter SLAEscalationMailFilter);
        Task<List<SLAEscalationMail>> List(SLAEscalationMailFilter SLAEscalationMailFilter);
        Task<SLAEscalationMail> Get(long Id);
        Task<SLAEscalationMail> Create(SLAEscalationMail SLAEscalationMail);
        Task<SLAEscalationMail> Update(SLAEscalationMail SLAEscalationMail);
        Task<SLAEscalationMail> Delete(SLAEscalationMail SLAEscalationMail);
        Task<List<SLAEscalationMail>> BulkDelete(List<SLAEscalationMail> SLAEscalationMails);
        Task<List<SLAEscalationMail>> Import(List<SLAEscalationMail> SLAEscalationMails);
        SLAEscalationMailFilter ToFilter(SLAEscalationMailFilter SLAEscalationMailFilter);
    }

    public class SLAEscalationMailService : BaseService, ISLAEscalationMailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISLAEscalationMailValidator SLAEscalationMailValidator;

        public SLAEscalationMailService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISLAEscalationMailValidator SLAEscalationMailValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SLAEscalationMailValidator = SLAEscalationMailValidator;
        }
        public async Task<int> Count(SLAEscalationMailFilter SLAEscalationMailFilter)
        {
            try
            {
                int result = await UOW.SLAEscalationMailRepository.Count(SLAEscalationMailFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationMail>> List(SLAEscalationMailFilter SLAEscalationMailFilter)
        {
            try
            {
                List<SLAEscalationMail> SLAEscalationMails = await UOW.SLAEscalationMailRepository.List(SLAEscalationMailFilter);
                return SLAEscalationMails;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SLAEscalationMail> Get(long Id)
        {
            SLAEscalationMail SLAEscalationMail = await UOW.SLAEscalationMailRepository.Get(Id);
            if (SLAEscalationMail == null)
                return null;
            return SLAEscalationMail;
        }
       
        public async Task<SLAEscalationMail> Create(SLAEscalationMail SLAEscalationMail)
        {
            if (!await SLAEscalationMailValidator.Create(SLAEscalationMail))
                return SLAEscalationMail;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationMailRepository.Create(SLAEscalationMail);
                await UOW.Commit();
                SLAEscalationMail = await UOW.SLAEscalationMailRepository.Get(SLAEscalationMail.Id);
                await Logging.CreateAuditLog(SLAEscalationMail, new { }, nameof(SLAEscalationMailService));
                return SLAEscalationMail;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationMail> Update(SLAEscalationMail SLAEscalationMail)
        {
            if (!await SLAEscalationMailValidator.Update(SLAEscalationMail))
                return SLAEscalationMail;
            try
            {
                var oldData = await UOW.SLAEscalationMailRepository.Get(SLAEscalationMail.Id);

                await UOW.Begin();
                await UOW.SLAEscalationMailRepository.Update(SLAEscalationMail);
                await UOW.Commit();

                SLAEscalationMail = await UOW.SLAEscalationMailRepository.Get(SLAEscalationMail.Id);
                await Logging.CreateAuditLog(SLAEscalationMail, oldData, nameof(SLAEscalationMailService));
                return SLAEscalationMail;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SLAEscalationMail> Delete(SLAEscalationMail SLAEscalationMail)
        {
            if (!await SLAEscalationMailValidator.Delete(SLAEscalationMail))
                return SLAEscalationMail;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationMailRepository.Delete(SLAEscalationMail);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationMail, nameof(SLAEscalationMailService));
                return SLAEscalationMail;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SLAEscalationMail>> BulkDelete(List<SLAEscalationMail> SLAEscalationMails)
        {
            if (!await SLAEscalationMailValidator.BulkDelete(SLAEscalationMails))
                return SLAEscalationMails;

            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationMailRepository.BulkDelete(SLAEscalationMails);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SLAEscalationMails, nameof(SLAEscalationMailService));
                return SLAEscalationMails;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SLAEscalationMail>> Import(List<SLAEscalationMail> SLAEscalationMails)
        {
            if (!await SLAEscalationMailValidator.Import(SLAEscalationMails))
                return SLAEscalationMails;
            try
            {
                await UOW.Begin();
                await UOW.SLAEscalationMailRepository.BulkMerge(SLAEscalationMails);
                await UOW.Commit();

                await Logging.CreateAuditLog(SLAEscalationMails, new { }, nameof(SLAEscalationMailService));
                return SLAEscalationMails;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SLAEscalationMailService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SLAEscalationMailService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SLAEscalationMailFilter ToFilter(SLAEscalationMailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SLAEscalationMailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SLAEscalationMailFilter subFilter = new SLAEscalationMailFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.SLAEscalationId))
                        subFilter.SLAEscalationId = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Mail))
                        
                        
                        
                        
                        
                        
                        subFilter.Mail = FilterPermissionDefinition.StringFilter;
                        
                }
            }
            return filter;
        }
    }
}
