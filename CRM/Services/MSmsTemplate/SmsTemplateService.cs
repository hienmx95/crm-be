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

namespace CRM.Services.MSmsTemplate
{
    public interface ISmsTemplateService :  IServiceScoped
    {
        Task<int> Count(SmsTemplateFilter SmsTemplateFilter);
        Task<List<SmsTemplate>> List(SmsTemplateFilter SmsTemplateFilter);
        Task<SmsTemplate> Get(long Id);
        Task<SmsTemplate> Create(SmsTemplate SmsTemplate);
        Task<SmsTemplate> Update(SmsTemplate SmsTemplate);
        Task<SmsTemplate> Delete(SmsTemplate SmsTemplate);
        Task<List<SmsTemplate>> BulkDelete(List<SmsTemplate> SmsTemplates);
        Task<List<SmsTemplate>> Import(List<SmsTemplate> SmsTemplates);
        SmsTemplateFilter ToFilter(SmsTemplateFilter SmsTemplateFilter);
    }

    public class SmsTemplateService : BaseService, ISmsTemplateService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ISmsTemplateValidator SmsTemplateValidator;

        public SmsTemplateService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ISmsTemplateValidator SmsTemplateValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.SmsTemplateValidator = SmsTemplateValidator;
        }
        public async Task<int> Count(SmsTemplateFilter SmsTemplateFilter)
        {
            try
            {
                int result = await UOW.SmsTemplateRepository.Count(SmsTemplateFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SmsTemplate>> List(SmsTemplateFilter SmsTemplateFilter)
        {
            try
            {
                List<SmsTemplate> SmsTemplates = await UOW.SmsTemplateRepository.List(SmsTemplateFilter);
                return SmsTemplates;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<SmsTemplate> Get(long Id)
        {
            SmsTemplate SmsTemplate = await UOW.SmsTemplateRepository.Get(Id);
            if (SmsTemplate == null)
                return null;
            return SmsTemplate;
        }
       
        public async Task<SmsTemplate> Create(SmsTemplate SmsTemplate)
        {
            if (!await SmsTemplateValidator.Create(SmsTemplate))
                return SmsTemplate;

            try
            {
                await UOW.Begin();
                await UOW.SmsTemplateRepository.Create(SmsTemplate);
                await UOW.Commit();
                SmsTemplate = await UOW.SmsTemplateRepository.Get(SmsTemplate.Id);
                await Logging.CreateAuditLog(SmsTemplate, new { }, nameof(SmsTemplateService));
                return SmsTemplate;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SmsTemplate> Update(SmsTemplate SmsTemplate)
        {
            if (!await SmsTemplateValidator.Update(SmsTemplate))
                return SmsTemplate;
            try
            {
                var oldData = await UOW.SmsTemplateRepository.Get(SmsTemplate.Id);

                await UOW.Begin();
                await UOW.SmsTemplateRepository.Update(SmsTemplate);
                await UOW.Commit();

                SmsTemplate = await UOW.SmsTemplateRepository.Get(SmsTemplate.Id);
                await Logging.CreateAuditLog(SmsTemplate, oldData, nameof(SmsTemplateService));
                return SmsTemplate;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<SmsTemplate> Delete(SmsTemplate SmsTemplate)
        {
            if (!await SmsTemplateValidator.Delete(SmsTemplate))
                return SmsTemplate;

            try
            {
                await UOW.Begin();
                await UOW.SmsTemplateRepository.Delete(SmsTemplate);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SmsTemplate, nameof(SmsTemplateService));
                return SmsTemplate;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<SmsTemplate>> BulkDelete(List<SmsTemplate> SmsTemplates)
        {
            if (!await SmsTemplateValidator.BulkDelete(SmsTemplates))
                return SmsTemplates;

            try
            {
                await UOW.Begin();
                await UOW.SmsTemplateRepository.BulkDelete(SmsTemplates);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, SmsTemplates, nameof(SmsTemplateService));
                return SmsTemplates;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<SmsTemplate>> Import(List<SmsTemplate> SmsTemplates)
        {
            if (!await SmsTemplateValidator.Import(SmsTemplates))
                return SmsTemplates;
            try
            {
                await UOW.Begin();
                await UOW.SmsTemplateRepository.BulkMerge(SmsTemplates);
                await UOW.Commit();

                await Logging.CreateAuditLog(SmsTemplates, new { }, nameof(SmsTemplateService));
                return SmsTemplates;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(SmsTemplateService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(SmsTemplateService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public SmsTemplateFilter ToFilter(SmsTemplateFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<SmsTemplateFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                SmsTemplateFilter subFilter = new SmsTemplateFilter();
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
