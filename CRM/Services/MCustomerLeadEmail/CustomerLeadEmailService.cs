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

namespace CRM.Services.MCustomerLeadEmail
{
    public interface ICustomerLeadEmailService :  IServiceScoped
    {
        Task<int> Count(CustomerLeadEmailFilter CustomerLeadEmailFilter);
        Task<List<CustomerLeadEmail>> List(CustomerLeadEmailFilter CustomerLeadEmailFilter);
        Task<CustomerLeadEmail> Get(long Id);
        Task<CustomerLeadEmail> Send(CustomerLeadEmail CustomerLeadEmail);
        Task<CustomerLeadEmail> Create(CustomerLeadEmail CustomerLeadEmail);
        Task<CustomerLeadEmail> Update(CustomerLeadEmail CustomerLeadEmail);
        Task<CustomerLeadEmail> Delete(CustomerLeadEmail CustomerLeadEmail);
        Task<List<CustomerLeadEmail>> BulkDelete(List<CustomerLeadEmail> CustomerLeadEmails);
        Task<List<CustomerLeadEmail>> Import(List<CustomerLeadEmail> CustomerLeadEmails);
        Task<CustomerLeadEmailFilter> ToFilter(CustomerLeadEmailFilter CustomerLeadEmailFilter);
    }

    public class CustomerLeadEmailService : BaseService, ICustomerLeadEmailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerLeadEmailValidator CustomerLeadEmailValidator;
        private IRabbitManager RabbitManager;

        public CustomerLeadEmailService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerLeadEmailValidator CustomerLeadEmailValidator,
            IRabbitManager RabbitManager,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerLeadEmailValidator = CustomerLeadEmailValidator;
            this.RabbitManager = RabbitManager;
        }
        public async Task<int> Count(CustomerLeadEmailFilter CustomerLeadEmailFilter)
        {
            try
            {
                int result = await UOW.CustomerLeadEmailRepository.Count(CustomerLeadEmailFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadEmailService));
            }
            return 0;
        }

        public async Task<List<CustomerLeadEmail>> List(CustomerLeadEmailFilter CustomerLeadEmailFilter)
        {
            try
            {
                List<CustomerLeadEmail> CustomerLeadEmails = await UOW.CustomerLeadEmailRepository.List(CustomerLeadEmailFilter);
                return CustomerLeadEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadEmailService));
            }
            return null;
        }
        
        public async Task<CustomerLeadEmail> Get(long Id)
        {
            CustomerLeadEmail CustomerLeadEmail = await UOW.CustomerLeadEmailRepository.Get(Id);
            if (CustomerLeadEmail == null)
                return null;
            return CustomerLeadEmail;
        }
        public async Task<CustomerLeadEmail> Send(CustomerLeadEmail CustomerLeadEmail)
        {
            try
            {
                var oldData = await UOW.CustomerLeadEmailRepository.Get(CustomerLeadEmail.Id);
                if(oldData == null)
                {
                    await Create(CustomerLeadEmail);
                }
                else
                {
                    await Update(CustomerLeadEmail);
                }

                var AppUserIds = CustomerLeadEmail.CustomerLeadEmailCCMappings?.Select(x => x.AppUserId).ToList();
                var Reciepients = new List<string>();
                Reciepients.Add(CustomerLeadEmail.Reciepient);
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
                    Subject = CustomerLeadEmail.Title,
                    Body = CustomerLeadEmail.Content,
                    Recipients = Reciepients,
                    RowId = Guid.NewGuid()
                };
                RabbitManager.PublishSingle(new EventMessage<Mail>(mail, mail.RowId), RoutingKeyEnum.MailSend);
                CustomerLeadEmail.EmailStatusId = EmailStatusEnum.DONE.Id;
                await UOW.CustomerLeadEmailRepository.Update(CustomerLeadEmail);

                CustomerLeadEmail = await UOW.CustomerLeadEmailRepository.Get(CustomerLeadEmail.Id);
                await Logging.CreateAuditLog(CustomerLeadEmail, new { }, nameof(CustomerLeadEmailService));
                return CustomerLeadEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadEmailService));
            }
            return null;
        }
        public async Task<CustomerLeadEmail> Create(CustomerLeadEmail CustomerLeadEmail)
        {
            if (!await CustomerLeadEmailValidator.Create(CustomerLeadEmail))
                return CustomerLeadEmail;

            try
            {
                CustomerLeadEmail.CreatorId = CurrentContext.UserId;
                CustomerLeadEmail.EmailStatusId = EmailStatusEnum.NOT_DONE.Id;
                await UOW.CustomerLeadEmailRepository.Create(CustomerLeadEmail);
                CustomerLeadEmail = await UOW.CustomerLeadEmailRepository.Get(CustomerLeadEmail.Id);
                await Logging.CreateAuditLog(CustomerLeadEmail, new { }, nameof(CustomerLeadEmailService));
                return CustomerLeadEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadEmailService));
            }
            return null;
        }

        public async Task<CustomerLeadEmail> Update(CustomerLeadEmail CustomerLeadEmail)
        {
            if (!await CustomerLeadEmailValidator.Update(CustomerLeadEmail))
                return CustomerLeadEmail;
            try
            {
                var oldData = await UOW.CustomerLeadEmailRepository.Get(CustomerLeadEmail.Id);

                await UOW.CustomerLeadEmailRepository.Update(CustomerLeadEmail);

                CustomerLeadEmail = await UOW.CustomerLeadEmailRepository.Get(CustomerLeadEmail.Id);
                await Logging.CreateAuditLog(CustomerLeadEmail, oldData, nameof(CustomerLeadEmailService));
                return CustomerLeadEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadEmailService));
            }
            return null;
        }

        public async Task<CustomerLeadEmail> Delete(CustomerLeadEmail CustomerLeadEmail)
        {
            if (!await CustomerLeadEmailValidator.Delete(CustomerLeadEmail))
                return CustomerLeadEmail;

            try
            {
                await UOW.CustomerLeadEmailRepository.Delete(CustomerLeadEmail);
                await Logging.CreateAuditLog(new { }, CustomerLeadEmail, nameof(CustomerLeadEmailService));
                return CustomerLeadEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadEmailService));
            }
            return null;
        }

        public async Task<List<CustomerLeadEmail>> BulkDelete(List<CustomerLeadEmail> CustomerLeadEmails)
        {
            if (!await CustomerLeadEmailValidator.BulkDelete(CustomerLeadEmails))
                return CustomerLeadEmails;

            try
            {
                await UOW.CustomerLeadEmailRepository.BulkDelete(CustomerLeadEmails);
                await Logging.CreateAuditLog(new { }, CustomerLeadEmails, nameof(CustomerLeadEmailService));
                return CustomerLeadEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadEmailService));
            }
            return null;

        }
        
        public async Task<List<CustomerLeadEmail>> Import(List<CustomerLeadEmail> CustomerLeadEmails)
        {
            if (!await CustomerLeadEmailValidator.Import(CustomerLeadEmails))
                return CustomerLeadEmails;
            try
            {
                await UOW.CustomerLeadEmailRepository.BulkMerge(CustomerLeadEmails);

                await Logging.CreateAuditLog(CustomerLeadEmails, new { }, nameof(CustomerLeadEmailService));
                return CustomerLeadEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerLeadEmailService));
            }
            return null;
        }     
        
        public async Task<CustomerLeadEmailFilter> ToFilter(CustomerLeadEmailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerLeadEmailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerLeadEmailFilter subFilter = new CustomerLeadEmailFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerLeadId))
                        subFilter.CustomerLeadId = FilterBuilder.Merge(subFilter.CustomerLeadId, FilterPermissionDefinition.IdFilter);
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
