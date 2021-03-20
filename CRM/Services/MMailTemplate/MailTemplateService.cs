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

namespace CRM.Services.MMailTemplate
{
    public interface IMailTemplateService :  IServiceScoped
    {
        Task<int> Count(MailTemplateFilter MailTemplateFilter);
        Task<List<MailTemplate>> List(MailTemplateFilter MailTemplateFilter);
        Task<MailTemplate> Get(long Id);
        Task<MailTemplate> Create(MailTemplate MailTemplate);
        Task<MailTemplate> Update(MailTemplate MailTemplate);
        Task<MailTemplate> Delete(MailTemplate MailTemplate);
        Task<List<MailTemplate>> BulkDelete(List<MailTemplate> MailTemplates);
        Task<List<MailTemplate>> Import(List<MailTemplate> MailTemplates);
        MailTemplateFilter ToFilter(MailTemplateFilter MailTemplateFilter);
    }

    public class MailTemplateService : BaseService, IMailTemplateService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IMailTemplateValidator MailTemplateValidator;

        public MailTemplateService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IMailTemplateValidator MailTemplateValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.MailTemplateValidator = MailTemplateValidator;
        }
        public async Task<int> Count(MailTemplateFilter MailTemplateFilter)
        {
            try
            {
                int result = await UOW.MailTemplateRepository.Count(MailTemplateFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(MailTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(MailTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<MailTemplate>> List(MailTemplateFilter MailTemplateFilter)
        {
            try
            {
                List<MailTemplate> MailTemplates = await UOW.MailTemplateRepository.List(MailTemplateFilter);
                return MailTemplates;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(MailTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(MailTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<MailTemplate> Get(long Id)
        {
            MailTemplate MailTemplate = await UOW.MailTemplateRepository.Get(Id);
            if (MailTemplate == null)
                return null;
            return MailTemplate;
        }
       
        public async Task<MailTemplate> Create(MailTemplate MailTemplate)
        {
            if (!await MailTemplateValidator.Create(MailTemplate))
                return MailTemplate;

            try
            {
                await UOW.Begin();
                await UOW.MailTemplateRepository.Create(MailTemplate);
                await UOW.Commit();
                MailTemplate = await UOW.MailTemplateRepository.Get(MailTemplate.Id);
                await Logging.CreateAuditLog(MailTemplate, new { }, nameof(MailTemplateService));
                return MailTemplate;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(MailTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(MailTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<MailTemplate> Update(MailTemplate MailTemplate)
        {
            if (!await MailTemplateValidator.Update(MailTemplate))
                return MailTemplate;
            try
            {
                var oldData = await UOW.MailTemplateRepository.Get(MailTemplate.Id);

                await UOW.Begin();
                await UOW.MailTemplateRepository.Update(MailTemplate);
                await UOW.Commit();

                MailTemplate = await UOW.MailTemplateRepository.Get(MailTemplate.Id);
                await Logging.CreateAuditLog(MailTemplate, oldData, nameof(MailTemplateService));
                return MailTemplate;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(MailTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(MailTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<MailTemplate> Delete(MailTemplate MailTemplate)
        {
            if (!await MailTemplateValidator.Delete(MailTemplate))
                return MailTemplate;

            try
            {
                await UOW.Begin();
                await UOW.MailTemplateRepository.Delete(MailTemplate);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, MailTemplate, nameof(MailTemplateService));
                return MailTemplate;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(MailTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(MailTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<MailTemplate>> BulkDelete(List<MailTemplate> MailTemplates)
        {
            if (!await MailTemplateValidator.BulkDelete(MailTemplates))
                return MailTemplates;

            try
            {
                await UOW.Begin();
                await UOW.MailTemplateRepository.BulkDelete(MailTemplates);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, MailTemplates, nameof(MailTemplateService));
                return MailTemplates;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(MailTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(MailTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<MailTemplate>> Import(List<MailTemplate> MailTemplates)
        {
            if (!await MailTemplateValidator.Import(MailTemplates))
                return MailTemplates;
            try
            {
                await UOW.Begin();
                await UOW.MailTemplateRepository.BulkMerge(MailTemplates);
                await UOW.Commit();

                await Logging.CreateAuditLog(MailTemplates, new { }, nameof(MailTemplateService));
                return MailTemplates;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(MailTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(MailTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public MailTemplateFilter ToFilter(MailTemplateFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<MailTemplateFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                MailTemplateFilter subFilter = new MailTemplateFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterPermissionDefinition.IdFilter;                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        
                        
                        
                        
                        
                        
                        subFilter.Code = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        
                        
                        
                        
                        
                        
                        subFilter.Name = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Content))
                        
                        
                        
                        
                        
                        
                        subFilter.Content = FilterPermissionDefinition.StringFilter;
                        
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterPermissionDefinition.IdFilter;                }
            }
            return filter;
        }
    }
}
