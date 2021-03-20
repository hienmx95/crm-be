using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Handlers;
using CRM.Helpers;
using CRM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.MContactEmail
{
    public interface IContactEmailService : IServiceScoped
    {
        Task<int> Count(ContactEmailFilter ContactEmailFilter);
        Task<List<ContactEmail>> List(ContactEmailFilter ContactEmailFilter);
        Task<ContactEmail> Get(long Id);
        Task<ContactEmail> Send(ContactEmail ContactEmail);
        Task<ContactEmail> Create(ContactEmail ContactEmail);
        Task<ContactEmail> Update(ContactEmail ContactEmail);
        Task<ContactEmail> Delete(ContactEmail ContactEmail);
        Task<List<ContactEmail>> BulkDelete(List<ContactEmail> ContactEmails);
        Task<List<ContactEmail>> Import(List<ContactEmail> ContactEmails);
        Task<ContactEmailFilter> ToFilter(ContactEmailFilter ContactEmailFilter);
    }

    public class ContactEmailService : BaseService, IContactEmailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IContactEmailValidator ContactEmailValidator;
        private IRabbitManager RabbitManager;

        public ContactEmailService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            IContactEmailValidator ContactEmailValidator,
            IRabbitManager RabbitManager,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.ContactEmailValidator = ContactEmailValidator;
            this.RabbitManager = RabbitManager;
        }
        public async Task<int> Count(ContactEmailFilter ContactEmailFilter)
        {
            try
            {
                int result = await UOW.ContactEmailRepository.Count(ContactEmailFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactEmailService));
            }
            return 0;
        }

        public async Task<List<ContactEmail>> List(ContactEmailFilter ContactEmailFilter)
        {
            try
            {
                List<ContactEmail> ContactEmails = await UOW.ContactEmailRepository.List(ContactEmailFilter);
                return ContactEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactEmailService));
            }
            return null;
        }

        public async Task<ContactEmail> Get(long Id)
        {
            ContactEmail ContactEmail = await UOW.ContactEmailRepository.Get(Id);
            if (ContactEmail == null)
                return null;
            return ContactEmail;
        }
        public async Task<ContactEmail> Send(ContactEmail ContactEmail)
        {
            try
            {
                var oldData = await UOW.ContactEmailRepository.Get(ContactEmail.Id);
                if (oldData == null)
                {
                    await Create(ContactEmail);
                }
                else
                {
                    await Update(ContactEmail);
                }

                var AppUserIds = ContactEmail.ContactEmailCCMappings?.Select(x => x.AppUserId).ToList();
                var Reciepients = new List<string>();
                Reciepients.Add(ContactEmail.Reciepient);
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
                    Subject = ContactEmail.Title,
                    Body = ContactEmail.Content,
                    Recipients = Reciepients,
                    RowId = Guid.NewGuid()
                };
                RabbitManager.PublishSingle(new EventMessage<Mail>(mail, mail.RowId), RoutingKeyEnum.MailSend);
                ContactEmail.EmailStatusId = EmailStatusEnum.DONE.Id;
                await UOW.ContactEmailRepository.Update(ContactEmail);

                ContactEmail = await UOW.ContactEmailRepository.Get(ContactEmail.Id);
                await Logging.CreateAuditLog(ContactEmail, new { }, nameof(ContactEmailService));
                return ContactEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactEmailService));
            }
            return null;
        }
        public async Task<ContactEmail> Create(ContactEmail ContactEmail)
        {
            if (!await ContactEmailValidator.Create(ContactEmail))
                return ContactEmail;

            try
            {
                ContactEmail.CreatorId = CurrentContext.UserId;
                ContactEmail.EmailStatusId = EmailStatusEnum.NOT_DONE.Id;
                await UOW.ContactEmailRepository.Create(ContactEmail);
                ContactEmail = await UOW.ContactEmailRepository.Get(ContactEmail.Id);
                await Logging.CreateAuditLog(ContactEmail, new { }, nameof(ContactEmailService));
                return ContactEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactEmailService));
            }
            return null;
        }

        public async Task<ContactEmail> Update(ContactEmail ContactEmail)
        {
            if (!await ContactEmailValidator.Update(ContactEmail))
                return ContactEmail;
            try
            {
                var oldData = await UOW.ContactEmailRepository.Get(ContactEmail.Id);

                await UOW.ContactEmailRepository.Update(ContactEmail);

                ContactEmail = await UOW.ContactEmailRepository.Get(ContactEmail.Id);
                await Logging.CreateAuditLog(ContactEmail, oldData, nameof(ContactEmailService));
                return ContactEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactEmailService));
            }
            return null;
        }

        public async Task<ContactEmail> Delete(ContactEmail ContactEmail)
        {
            if (!await ContactEmailValidator.Delete(ContactEmail))
                return ContactEmail;

            try
            {
                await UOW.ContactEmailRepository.Delete(ContactEmail);
                await Logging.CreateAuditLog(new { }, ContactEmail, nameof(ContactEmailService));
                return ContactEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactEmailService));
            }
            return null;
        }

        public async Task<List<ContactEmail>> BulkDelete(List<ContactEmail> ContactEmails)
        {
            if (!await ContactEmailValidator.BulkDelete(ContactEmails))
                return ContactEmails;

            try
            {
                await UOW.ContactEmailRepository.BulkDelete(ContactEmails);
                await Logging.CreateAuditLog(new { }, ContactEmails, nameof(ContactEmailService));
                return ContactEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactEmailService));
            }
            return null;

        }

        public async Task<List<ContactEmail>> Import(List<ContactEmail> ContactEmails)
        {
            if (!await ContactEmailValidator.Import(ContactEmails))
                return ContactEmails;
            try
            {
                await UOW.ContactEmailRepository.BulkMerge(ContactEmails);

                await Logging.CreateAuditLog(ContactEmails, new { }, nameof(ContactEmailService));
                return ContactEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(ContactEmailService));
            }
            return null;
        }

        public async Task<ContactEmailFilter> ToFilter(ContactEmailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<ContactEmailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                ContactEmailFilter subFilter = new ContactEmailFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.ContactId))
                        subFilter.ContactId = FilterBuilder.Merge(subFilter.ContactId, FilterPermissionDefinition.IdFilter);
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
