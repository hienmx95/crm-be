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

namespace CRM.Services.MCustomerEmailHistory
{
    public interface ICustomerEmailHistoryService :  IServiceScoped
    {
        Task<int> Count(CustomerEmailHistoryFilter CustomerEmailHistoryFilter);
        Task<List<CustomerEmailHistory>> List(CustomerEmailHistoryFilter CustomerEmailHistoryFilter);
        Task<CustomerEmailHistory> Get(long Id);
        Task<CustomerEmailHistory> Create(CustomerEmailHistory CustomerEmailHistory);
        Task<CustomerEmailHistory> Send(CustomerEmailHistory CustomerEmailHistory);
        Task<CustomerEmailHistory> Update(CustomerEmailHistory CustomerEmailHistory);
        Task<CustomerEmailHistory> Delete(CustomerEmailHistory CustomerEmailHistory);
        Task<List<CustomerEmailHistory>> BulkDelete(List<CustomerEmailHistory> CustomerEmailHistories);
        Task<List<CustomerEmailHistory>> Import(List<CustomerEmailHistory> CustomerEmailHistories);
        Task<CustomerEmailHistoryFilter> ToFilter(CustomerEmailHistoryFilter CustomerEmailHistoryFilter);
    }

    public class CustomerEmailHistoryService : BaseService, ICustomerEmailHistoryService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerEmailHistoryValidator CustomerEmailHistoryValidator;
        private IRabbitManager RabbitManager;

        public CustomerEmailHistoryService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            ICustomerEmailHistoryValidator CustomerEmailHistoryValidator,
            ILogging Logging,
            IRabbitManager RabbitManager
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.CustomerEmailHistoryValidator = CustomerEmailHistoryValidator;
            this.RabbitManager = RabbitManager;
        }
        public async Task<int> Count(CustomerEmailHistoryFilter CustomerEmailHistoryFilter)
        {
            try
            {
                int result = await UOW.CustomerEmailHistoryRepository.Count(CustomerEmailHistoryFilter);
                return result;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailHistoryService));
            }
            return 0;
        }

        public async Task<List<CustomerEmailHistory>> List(CustomerEmailHistoryFilter CustomerEmailHistoryFilter)
        {
            try
            {
                List<CustomerEmailHistory> CustomerEmailHistorys = await UOW.CustomerEmailHistoryRepository.List(CustomerEmailHistoryFilter);
                return CustomerEmailHistorys;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailHistoryService));
            }
            return null;
        }
        
        public async Task<CustomerEmailHistory> Get(long Id)
        {
            CustomerEmailHistory CustomerEmailHistory = await UOW.CustomerEmailHistoryRepository.Get(Id);
            if (CustomerEmailHistory == null)
                return null;
            return CustomerEmailHistory;
        }
        public async Task<CustomerEmailHistory> Create(CustomerEmailHistory CustomerEmailHistory)
        {
            if (!await CustomerEmailHistoryValidator.Create(CustomerEmailHistory))
                return CustomerEmailHistory;

            try
            {
                CustomerEmailHistory.CreatorId = CurrentContext.UserId;
                await UOW.CustomerEmailHistoryRepository.Create(CustomerEmailHistory);
                CustomerEmailHistory = await UOW.CustomerEmailHistoryRepository.Get(CustomerEmailHistory.Id);
                await Logging.CreateAuditLog(CustomerEmailHistory, new { }, nameof(CustomerEmailHistoryService));
                return CustomerEmailHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailHistoryService));
            }
            return null;
        }

        public async Task<CustomerEmailHistory> Send(CustomerEmailHistory CustomerEmailHistory)
        {
            try
            {
                var oldData = await UOW.CustomerEmailHistoryRepository.Get(CustomerEmailHistory.Id);
                if (oldData == null)
                {
                    await Create(CustomerEmailHistory);
                }
                else
                {
                    await Update(CustomerEmailHistory);
                }

                var Reciepients = CustomerEmailHistory.CustomerCCEmailHistories?.Select(x => x.CCEmail).ToList();
                if (Reciepients == null)
                    Reciepients = new List<string>();
                Reciepients.Add(CustomerEmailHistory.Reciepient);
                Mail mail = new Mail
                {
                    Subject = CustomerEmailHistory.Title,
                    Body = CustomerEmailHistory.Content,
                    Recipients = Reciepients,
                    RowId = Guid.NewGuid()
                };
                RabbitManager.PublishSingle(new EventMessage<Mail>(mail, mail.RowId), RoutingKeyEnum.MailSend);
                CustomerEmailHistory.EmailStatusId = EmailStatusEnum.DONE.Id;
                await UOW.CustomerEmailHistoryRepository.Update(CustomerEmailHistory);

                CustomerEmailHistory = await UOW.CustomerEmailHistoryRepository.Get(CustomerEmailHistory.Id);
                await Logging.CreateAuditLog(CustomerEmailHistory, new { }, nameof(CustomerEmailHistoryService));
                return CustomerEmailHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailHistoryService));
            }
            return null;
        }

        public async Task<CustomerEmailHistory> Update(CustomerEmailHistory CustomerEmailHistory)
        {
            if (!await CustomerEmailHistoryValidator.Update(CustomerEmailHistory))
                return CustomerEmailHistory;
            try
            {
                var oldData = await UOW.CustomerEmailHistoryRepository.Get(CustomerEmailHistory.Id);

                await UOW.CustomerEmailHistoryRepository.Update(CustomerEmailHistory);

                CustomerEmailHistory = await UOW.CustomerEmailHistoryRepository.Get(CustomerEmailHistory.Id);
                await Logging.CreateAuditLog(CustomerEmailHistory, oldData, nameof(CustomerEmailHistoryService));
                return CustomerEmailHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailHistoryService));
            }
            return null;
        }

        public async Task<CustomerEmailHistory> Delete(CustomerEmailHistory CustomerEmailHistory)
        {
            if (!await CustomerEmailHistoryValidator.Delete(CustomerEmailHistory))
                return CustomerEmailHistory;

            try
            {
                await UOW.CustomerEmailHistoryRepository.Delete(CustomerEmailHistory);
                await Logging.CreateAuditLog(new { }, CustomerEmailHistory, nameof(CustomerEmailHistoryService));
                return CustomerEmailHistory;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailHistoryService));
            }
            return null;
        }

        public async Task<List<CustomerEmailHistory>> BulkDelete(List<CustomerEmailHistory> CustomerEmailHistories)
        {
            if (!await CustomerEmailHistoryValidator.BulkDelete(CustomerEmailHistories))
                return CustomerEmailHistories;

            try
            {
                await UOW.CustomerEmailHistoryRepository.BulkDelete(CustomerEmailHistories);
                await Logging.CreateAuditLog(new { }, CustomerEmailHistories, nameof(CustomerEmailHistoryService));
                return CustomerEmailHistories;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailHistoryService));
            }
            return null;

        }
        
        public async Task<List<CustomerEmailHistory>> Import(List<CustomerEmailHistory> CustomerEmailHistories)
        {
            if (!await CustomerEmailHistoryValidator.Import(CustomerEmailHistories))
                return CustomerEmailHistories;
            try
            {
                await UOW.CustomerEmailHistoryRepository.BulkMerge(CustomerEmailHistories);

                await Logging.CreateAuditLog(CustomerEmailHistories, new { }, nameof(CustomerEmailHistoryService));
                return CustomerEmailHistories;
            }
            catch (Exception ex)
            {
                await Logging.CreateSystemLog(ex, nameof(CustomerEmailHistoryService));
            }
            return null;
        }     
        
        public async Task<CustomerEmailHistoryFilter> ToFilter(CustomerEmailHistoryFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerEmailHistoryFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                CustomerEmailHistoryFilter subFilter = new CustomerEmailHistoryFilter();
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
                    if (FilterPermissionDefinition.Name == nameof(subFilter.CustomerId))
                        subFilter.CustomerId = FilterBuilder.Merge(subFilter.CustomerId, FilterPermissionDefinition.IdFilter);
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
