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

namespace CRM.Services.MOpportunityEmail
{
    public interface IOpportunityEmailService :  IServiceScoped
    {
        Task<int> Count(OpportunityEmailFilter OpportunityEmailFilter);
        Task<List<OpportunityEmail>> List(OpportunityEmailFilter OpportunityEmailFilter);
        Task<OpportunityEmail> Get(long Id);
        Task<OpportunityEmail> Send(OpportunityEmail OpportunityEmail);
        Task<OpportunityEmail> Create(OpportunityEmail OpportunityEmail);
        Task<OpportunityEmail> Update(OpportunityEmail OpportunityEmail);
        Task<OpportunityEmail> Delete(OpportunityEmail OpportunityEmail);
        Task<List<OpportunityEmail>> BulkDelete(List<OpportunityEmail> OpportunityEmails);
        Task<List<OpportunityEmail>> Import(List<OpportunityEmail> OpportunityEmails);
        Task<OpportunityEmailFilter> ToFilter(OpportunityEmailFilter OpportunityEmailFilter);
    }

    public class OpportunityEmailService : BaseService, IOpportunityEmailService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IOpportunityEmailValidator OpportunityEmailValidator;
        private IRabbitManager RabbitManager;

        public OpportunityEmailService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            IOpportunityEmailValidator OpportunityEmailValidator,
            IRabbitManager RabbitManager,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.RabbitManager = RabbitManager;
            this.OpportunityEmailValidator = OpportunityEmailValidator;
        }
        public async Task<int> Count(OpportunityEmailFilter OpportunityEmailFilter)
        {
            try
            {
                int result = await UOW.OpportunityEmailRepository.Count(OpportunityEmailFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityEmailService));
            }
            return 0;
        }

        public async Task<List<OpportunityEmail>> List(OpportunityEmailFilter OpportunityEmailFilter)
        {
            try
            {
                List<OpportunityEmail> OpportunityEmails = await UOW.OpportunityEmailRepository.List(OpportunityEmailFilter);
                return OpportunityEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityEmailService));
            }
            return null;
        }
        
        public async Task<OpportunityEmail> Get(long Id)
        {
            OpportunityEmail OpportunityEmail = await UOW.OpportunityEmailRepository.Get(Id);
            if (OpportunityEmail == null)
                return null;
            return OpportunityEmail;
        }
        public async Task<OpportunityEmail> Send(OpportunityEmail OpportunityEmail)
        {
            try
            {
                var oldData = await UOW.OpportunityEmailRepository.Get(OpportunityEmail.Id);
                if (oldData == null)
                {
                    await Create(OpportunityEmail);
                }
                else
                {
                    await Update(OpportunityEmail);
                }

                var AppUserIds = OpportunityEmail.OpportunityEmailCCMappings?.Select(x => x.AppUserId).ToList();
                var Reciepients = new List<string>();
                Reciepients.Add(OpportunityEmail.Reciepient);
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
                    Subject = OpportunityEmail.Title,
                    Body = OpportunityEmail.Content,
                    Recipients = Reciepients,
                    RowId = Guid.NewGuid()
                };
                RabbitManager.PublishSingle(new EventMessage<Mail>(mail, mail.RowId), RoutingKeyEnum.MailSend);
                OpportunityEmail.EmailStatusId = EmailStatusEnum.DONE.Id;
                await UOW.OpportunityEmailRepository.Update(OpportunityEmail);

                OpportunityEmail = await UOW.OpportunityEmailRepository.Get(OpportunityEmail.Id);
                await Logging.CreateAuditLog(OpportunityEmail, new { }, nameof(OpportunityEmailService));
                return OpportunityEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityEmailService));
            }
            return null;
        }
        public async Task<OpportunityEmail> Create(OpportunityEmail OpportunityEmail)
        {
            if (!await OpportunityEmailValidator.Create(OpportunityEmail))
                return OpportunityEmail;

            try
            {
                OpportunityEmail.CreatorId = CurrentContext.UserId;
                OpportunityEmail.EmailStatusId = EmailStatusEnum.NOT_DONE.Id;
                await UOW.OpportunityEmailRepository.Create(OpportunityEmail);
                OpportunityEmail = await UOW.OpportunityEmailRepository.Get(OpportunityEmail.Id);
                await Logging.CreateAuditLog(OpportunityEmail, new { }, nameof(OpportunityEmailService));
                return OpportunityEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityEmailService));
            }
            return null;
        }

        public async Task<OpportunityEmail> Update(OpportunityEmail OpportunityEmail)
        {
            if (!await OpportunityEmailValidator.Update(OpportunityEmail))
                return OpportunityEmail;
            try
            {
                var oldData = await UOW.OpportunityEmailRepository.Get(OpportunityEmail.Id);

                await UOW.OpportunityEmailRepository.Update(OpportunityEmail);

                OpportunityEmail = await UOW.OpportunityEmailRepository.Get(OpportunityEmail.Id);
                await Logging.CreateAuditLog(OpportunityEmail, oldData, nameof(OpportunityEmailService));
                return OpportunityEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityEmailService));
            }
            return null;
        }

        public async Task<OpportunityEmail> Delete(OpportunityEmail OpportunityEmail)
        {
            if (!await OpportunityEmailValidator.Delete(OpportunityEmail))
                return OpportunityEmail;

            try
            {
                await UOW.OpportunityEmailRepository.Delete(OpportunityEmail);
                await Logging.CreateAuditLog(new { }, OpportunityEmail, nameof(OpportunityEmailService));
                return OpportunityEmail;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityEmailService));
            }
            return null;
        }

        public async Task<List<OpportunityEmail>> BulkDelete(List<OpportunityEmail> OpportunityEmails)
        {
            if (!await OpportunityEmailValidator.BulkDelete(OpportunityEmails))
                return OpportunityEmails;

            try
            {
                await UOW.OpportunityEmailRepository.BulkDelete(OpportunityEmails);
                await Logging.CreateAuditLog(new { }, OpportunityEmails, nameof(OpportunityEmailService));
                return OpportunityEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityEmailService));
            }
            return null;

        }
        
        public async Task<List<OpportunityEmail>> Import(List<OpportunityEmail> OpportunityEmails)
        {
            if (!await OpportunityEmailValidator.Import(OpportunityEmails))
                return OpportunityEmails;
            try
            {
                await UOW.OpportunityEmailRepository.BulkMerge(OpportunityEmails);

                await Logging.CreateAuditLog(OpportunityEmails, new { }, nameof(OpportunityEmailService));
                return OpportunityEmails;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(OpportunityEmailService));
            }
            return null;
        }     
        
        public async Task<OpportunityEmailFilter> ToFilter(OpportunityEmailFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<OpportunityEmailFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                OpportunityEmailFilter subFilter = new OpportunityEmailFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OpportunityId))
                        subFilter.OpportunityId = FilterBuilder.Merge(subFilter.OpportunityId, FilterPermissionDefinition.IdFilter);
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
