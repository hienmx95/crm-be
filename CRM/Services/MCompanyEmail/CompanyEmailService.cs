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
using CRM.Enums;
using CRM.Handlers;

namespace CRM.Services.MCompanyEmail
{
    public interface ICompanyEmailService :  IServiceScoped
    {
        Task<int> Count(CompanyEmailFilter CompanyEmailFilter);
        Task<List<CompanyEmail>> List(CompanyEmailFilter CompanyEmailFilter);
        Task<CompanyEmail> Get(long Id);
        Task<CompanyEmail> Send(CompanyEmail CompanyEmail);
        Task<CompanyEmail> Create(CompanyEmail CompanyEmail);
        Task<CompanyEmail> Update(CompanyEmail CompanyEmail);
        Task<CompanyEmail> Delete(CompanyEmail CompanyEmail);
        Task<List<CompanyEmail>> BulkDelete(List<CompanyEmail> CompanyEmails);
        Task<List<CompanyEmail>> Import(List<CompanyEmail> CompanyEmails);
        Task<CompanyEmailFilter> ToFilter(CompanyEmailFilter CompanyEmailFilter);
    }

    public class CompanyEmailService : BaseService, ICompanyEmailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICompanyEmailValidator CompanyEmailValidator;
        private IRabbitManager RabbitManager;

        public CompanyEmailService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICompanyEmailValidator CompanyEmailValidator,
            ILogging Logging,
            IRabbitManager RabbitManager
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CompanyEmailValidator = CompanyEmailValidator;
            this.RabbitManager = RabbitManager;
        }
        public async Task<int> Count(CompanyEmailFilter CompanyEmailFilter)
        {
            try
            {
                int result = await UOW.CompanyEmailRepository.Count(CompanyEmailFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyEmailService));
            }
            return 0;
        }

        public async Task<List<CompanyEmail>> List(CompanyEmailFilter CompanyEmailFilter)
        {
            try
            {
                List<CompanyEmail> CompanyEmails = await UOW.CompanyEmailRepository.List(CompanyEmailFilter);
                return CompanyEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyEmailService));
            }
            return null;
        }
        
        public async Task<CompanyEmail> Get(long Id)
        {
            CompanyEmail CompanyEmail = await UOW.CompanyEmailRepository.Get(Id);
            if (CompanyEmail == null)
                return null;
            return CompanyEmail;
        }

        public async Task<CompanyEmail> Send(CompanyEmail CompanyEmail)
        {
            try
            {
                var oldData = await UOW.CompanyEmailRepository.Get(CompanyEmail.Id);
                if (oldData == null)
                {
                    await Create(CompanyEmail);
                }
                else
                {
                    await Update(CompanyEmail);
                }

                var AppUserIds = CompanyEmail.CompanyEmailCCMappings?.Select(x => x.AppUserId).ToList();
                var Reciepients = new List<string>();
                Reciepients.Add(CompanyEmail.Reciepient);
                if (AppUserIds != null && AppUserIds.Count > 0)
                {
                    AppUserFilter AppUserFilter = new AppUserFilter
                    {
                        Skip = 0,
                        Take = int.MaxValue,
                        Selects = AppUserSelect.Id | AppUserSelect.Email
                    };
                    var AppUsers = await UOW.AppUserRepository.List(AppUserFilter);
                    var AppUserEmails = AppUsers.Select(x => x.Email).ToList();
                    Reciepients.AddRange(AppUserEmails);
                }
                Mail mail = new Mail
                {
                    Subject = CompanyEmail.Title,
                    Body = CompanyEmail.Content,
                    Recipients = Reciepients,
                    RowId = Guid.NewGuid()
                };
                RabbitManager.PublishSingle(new EventMessage<Mail>(mail, mail.RowId), RoutingKeyEnum.MailSend);
                CompanyEmail.EmailStatusId = EmailStatusEnum.DONE.Id;
                await UOW.CompanyEmailRepository.Update(CompanyEmail);

                CompanyEmail = await UOW.CompanyEmailRepository.Get(CompanyEmail.Id);
                await Logging.CreateAuditLog(CompanyEmail, new { }, nameof(CompanyEmailService));
                return CompanyEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyEmailService));
            }
            return null;
        }
        public async Task<CompanyEmail> Create(CompanyEmail CompanyEmail)
        {
            if (!await CompanyEmailValidator.Create(CompanyEmail))
                return CompanyEmail;

            try
            {
                CompanyEmail.CreatorId = CurrentContext.UserId;
                CompanyEmail.EmailStatusId = EmailStatusEnum.NOT_DONE.Id;
                await UOW.CompanyEmailRepository.Create(CompanyEmail);
                CompanyEmail = await UOW.CompanyEmailRepository.Get(CompanyEmail.Id);
                await Logging.CreateAuditLog(CompanyEmail, new { }, nameof(CompanyEmailService));
                return CompanyEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyEmailService));
            }
            return null;
        }

        public async Task<CompanyEmail> Update(CompanyEmail CompanyEmail)
        {
            if (!await CompanyEmailValidator.Update(CompanyEmail))
                return CompanyEmail;
            try
            {
                var oldData = await UOW.CompanyEmailRepository.Get(CompanyEmail.Id);

                await UOW.CompanyEmailRepository.Update(CompanyEmail);

                CompanyEmail = await UOW.CompanyEmailRepository.Get(CompanyEmail.Id);
                await Logging.CreateAuditLog(CompanyEmail, oldData, nameof(CompanyEmailService));
                return CompanyEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyEmailService));
            }
            return null;
        }

        public async Task<CompanyEmail> Delete(CompanyEmail CompanyEmail)
        {
            if (!await CompanyEmailValidator.Delete(CompanyEmail))
                return CompanyEmail;

            try
            {
                await UOW.CompanyEmailRepository.Delete(CompanyEmail);
                await Logging.CreateAuditLog(new { }, CompanyEmail, nameof(CompanyEmailService));
                return CompanyEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyEmailService));
            }
            return null;
        }

        public async Task<List<CompanyEmail>> BulkDelete(List<CompanyEmail> CompanyEmails)
        {
            if (!await CompanyEmailValidator.BulkDelete(CompanyEmails))
                return CompanyEmails;

            try
            {
                await UOW.CompanyEmailRepository.BulkDelete(CompanyEmails);
                await Logging.CreateAuditLog(new { }, CompanyEmails, nameof(CompanyEmailService));
                return CompanyEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyEmailService));
            }
            return null;

        }
        
        public async Task<List<CompanyEmail>> Import(List<CompanyEmail> CompanyEmails)
        {
            if (!await CompanyEmailValidator.Import(CompanyEmails))
                return CompanyEmails;
            try
            {
                await UOW.CompanyEmailRepository.BulkMerge(CompanyEmails);

                await Logging.CreateAuditLog(CompanyEmails, new { }, nameof(CompanyEmailService));
                return CompanyEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CompanyEmailService));
            }
            return null;
        }     
        
        public async Task<CompanyEmailFilter> ToFilter(CompanyEmailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CompanyEmailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CompanyEmailFilter subFilter = new CompanyEmailFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Title))
                        subFilter.Title = FilterBuilder.Merge(subFilter.Title, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Content))
                        subFilter.Content = FilterBuilder.Merge(subFilter.Content, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Reciepient))
                        subFilter.Reciepient = FilterBuilder.Merge(subFilter.Reciepient, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CompanyId))
                        subFilter.CompanyId = FilterBuilder.Merge(subFilter.CompanyId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CreatorId))
                        subFilter.CreatorId = FilterBuilder.Merge(subFilter.CreatorId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.EmailStatusId))
                        subFilter.EmailStatusId = FilterBuilder.Merge(subFilter.EmailStatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                        }
                    }
                }
            }
            return filter;
        }
    }
}
