using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Repositories;
using CRM.Entities;
using CRM.Services.MTicketGeneratedId;
using CRM.Enums;
using CRM.Services.MTicketOfUser;
using CRM.Rpc.ticket;
using CRM.Services.MNotification;
using CRM.Handlers;
using CRM.Services.MOrganization;

namespace CRM.Services.MTicket
{
    public interface ITicketService : IServiceScoped
    {
        Task<int> Count(TicketFilter TicketFilter);
        Task<List<Ticket>> List(TicketFilter TicketFilter);
        Task<Ticket> Get(long Id);
        Task<Ticket> Create(Ticket Ticket);
        Task<Ticket> Update(Ticket Ticket);
        Task<Ticket> Delete(Ticket Ticket);
        Task<List<Ticket>> BulkDelete(List<Ticket> Tickets);
        Task<List<Ticket>> Import(List<Ticket> Tickets);
        Task<TicketFilter> ToFilter(TicketFilter TicketFilter);
    }

    public class TicketService : BaseService, ITicketService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ITicketValidator TicketValidator;
        private ITicketGeneratedIdService TicketGeneratedIdService;
        private ITicketOfUserService TicketOfUserService;
        private INotificationService NotificationService;
        private IRabbitManager RabbitManager;
        private IOrganizationService OrganizationService;
        public TicketService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            ITicketValidator TicketValidator,
            ITicketGeneratedIdService TicketGeneratedIdService,
            ITicketOfUserService TicketOfUserService,
            INotificationService NotificationService,
            IOrganizationService OrganizationService,
            IRabbitManager RabbitManager
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.TicketValidator = TicketValidator;
            this.TicketGeneratedIdService = TicketGeneratedIdService;
            this.TicketOfUserService = TicketOfUserService;
            this.NotificationService = NotificationService;
            this.RabbitManager = RabbitManager;
            this.OrganizationService = OrganizationService;
        }
        public async Task<int> Count(TicketFilter TicketFilter)
        {
            try
            {
                int result = await UOW.TicketRepository.Count(TicketFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Ticket>> List(TicketFilter TicketFilter)
        {
            try
            {
                List<Ticket> Tickets = await UOW.TicketRepository.List(TicketFilter);
                return Tickets;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Ticket> Get(long Id)
        {
            Ticket Ticket = await UOW.TicketRepository.Get(Id);
            DateTime Now = StaticParams.DateTimeNow;

            if (!Ticket.IsWork.Value)
            {
                Ticket.HoldingTime = (long)(Now.Subtract(Ticket.LastHoldingAt.Value).TotalMinutes);
                Ticket.FirstResponeTime = null;
                Ticket.ResolveTime = null;
            }
            else
            {
                var ot = (long)(Now.Subtract(Ticket.ResolveTime.Value).TotalMinutes);
                if (ot > 0 && Ticket.TicketStatusId != TicketStatusEnum.RESOLVED.Id && Ticket.TicketStatusId != TicketStatusEnum.CLOSED.Id)
                    Ticket.SLAOverTime = ot;

                var rtr = (long)(Ticket.ResolveTime.Value.Subtract(Now).TotalMinutes);
                Ticket.ResolveTimeRemaining = rtr < 0 ? 0 : rtr;
            }

            if (Ticket == null)
                return null;
            return Ticket;
        }

        public async Task<Ticket> Create(Ticket Ticket)
        {
            if (!await TicketValidator.Create(Ticket))
                return Ticket;

            try
            {
                await UOW.Begin();
                var newId = await TicketGeneratedIdService.GetNewTicketId();
                Ticket.TicketNumber = newId.Id.ToString().PadLeft(6, '0');
                Ticket.UserId = CurrentContext.UserId;
                Ticket.CreatorId = CurrentContext.UserId;
                Ticket.Subject = "";
                Ticket.SLA = Ticket.TicketIssueLevel.SLA;
                Ticket.IsAlerted = false;
                Ticket.IsAlertedFRT = false;
                Ticket.IsEscalated = false;
                Ticket.IsEscalatedFRT = false;
                Ticket.IsClose = false;
                Ticket.IsOpen = true;
                Ticket.IsWait = false;
                Ticket.IsWork = true;
                Ticket.ReraisedTimes = 0;
                Ticket.HoldingTime = 0;
                Ticket.TicketStatusId = TicketStatusEnum.NEW.Id;
                Ticket.TicketOfUsers = new List<TicketOfUser>();
                Ticket.TicketOfUsers.Add(
                    new TicketOfUser()
                    {
                        TicketStatusId = TicketStatusEnum.NEW.Id,
                        UserId = Ticket.UserId
                    }
                );
                SLAPolicy SLAPolicy = await UOW.SLAPolicyRepository.GetByTicket(Ticket);
                DateTime Now = StaticParams.DateTimeNow;
                if (SLAPolicy != null)
                {
                    // Ticket.SLAPolicyId = SLAPolicy.Id;
                    Ticket.FirstResponeTime = Now.AddMinutes(Utils.ConvertSLATimeToMenute(SLAPolicy.FirstResponseTime.Value, SLAPolicy.FirstResponseUnitId.Value));
                    Ticket.ResolveTime = Now.AddMinutes(Utils.ConvertSLATimeToMenute(SLAPolicy.ResolveTime.Value, SLAPolicy.ResolveUnitId.Value));
                }
                await UOW.TicketRepository.Create(Ticket);
                await TicketGeneratedIdService.UpdateUsed(int.Parse(Ticket.TicketNumber));
                await UOW.Commit();

                Ticket = await UOW.TicketRepository.Get(Ticket.Id);
                NotifyUsed(Ticket);
                var CurrentUser = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                var RecipientIds = await UOW.PermissionRepository.ListAppUser(TicketRoute.Update);
                List<UserNotification> UserNotifications = new List<UserNotification>();
                //foreach (var id in RecipientIds)
                //{
                UserNotification NotificationUtils = new UserNotification
                {
                    TitleWeb = $"Thông báo từ CRM",
                    ContentWeb = $"Ticket [{Ticket.TicketNumber} - {Ticket.Name} - {Ticket.TicketIssueLevel.Name}] đã được thêm mới lên hệ thống bởi {CurrentUser.DisplayName}",
                    LinkWebsite = $"{TicketRoute.Detail}/*".Replace("*", Ticket.Id.ToString()),
                    Time = Now,
                    Unread = true,
                    SenderId = CurrentContext.UserId,
                    RecipientId = CurrentContext.UserId,
                    RowId = Guid.NewGuid(),
                };
                UserNotifications.Add(NotificationUtils);
                //}

                List<EventMessage<UserNotification>> EventUserNotifications = UserNotifications.Select(x => new EventMessage<UserNotification>(x, x.RowId)).ToList();
                RabbitManager.PublishList(EventUserNotifications, RoutingKeyEnum.UserNotificationSend);


                await Logging.CreateAuditLog(Ticket, new { }, nameof(TicketService));
                return Ticket;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Ticket> Update(Ticket Ticket)
        {
            if (!await TicketValidator.Update(Ticket))
                return Ticket;
            try
            {
                var oldData = await UOW.TicketRepository.Get(Ticket.Id);

                await UOW.Begin();

                DateTime Now = StaticParams.DateTimeNow;
                // Gán thời gian xử lý và thời gian phản hồi cho ticket từ SLAPolicy
                SLAPolicy SLAPolicy = await UOW.SLAPolicyRepository.GetByTicket(Ticket);
                if (SLAPolicy != null)
                {
                    //Ticket.SLAPolicyId = SLAPolicy.Id;
                    Ticket.FirstResponeTime = oldData.CreatedAt.AddMinutes(Utils.ConvertSLATimeToMenute(SLAPolicy.FirstResponseTime.Value, SLAPolicy.FirstResponseUnitId.Value));
                    Ticket.ResolveTime = oldData.CreatedAt.AddMinutes(Utils.ConvertSLATimeToMenute(SLAPolicy.ResolveTime.Value, SLAPolicy.ResolveUnitId.Value));
                }
                // Ticket chuyển từ trạng thái hoạt động sang dừng
                if (!Ticket.IsWork.Value && oldData.IsWork.Value)
                {
                    Ticket.LastHoldingAt = Now;
                }
                // Ticket chuyển từ trạng thái dừng sang hoạt động
                else if (Ticket.IsWork.Value && !oldData.IsWork.Value)
                {
                    Ticket.FirstResponeTime = Now.AddMinutes(oldData.FirstRespTimeRemaining.Value);
                    Ticket.ResolveTime = Now.AddMinutes(oldData.ResolveTimeRemaining.Value);
                    Ticket.HoldingTime = oldData.HoldingTime + (long)(Now.Subtract(oldData.LastHoldingAt.Value).TotalMinutes);
                }
                // Lưu lại lịch sử ticket
                Ticket.TicketOfUsers = new List<TicketOfUser>();
                Ticket.TicketOfUsers.Add(
                    new TicketOfUser()
                    {
                        TicketStatusId = Ticket.TicketStatusId,
                        UserId = Ticket.UserId,
                        Notes = Ticket.Notes,
                        TicketId = Ticket.Id
                    }
                );
                // Đóng ticket
                if (Ticket.TicketStatusId == TicketStatusEnum.RESOLVED.Id || Ticket.TicketStatusId == TicketStatusEnum.CLOSED.Id)
                {
                    Ticket.ResolvedAt = Now;
                    Ticket.ResolveMinute = (long)(Now.Subtract(oldData.CreatedAt).TotalMinutes);
                    Ticket.IsClose = true;

                    var ot = (long)(Now.Subtract(Ticket.ResolveTime.Value).TotalMinutes);
                    if (ot > 0)
                        Ticket.SLAOverTime = ot;

                    if (Ticket.TicketStatusId == TicketStatusEnum.CLOSED.Id)
                        Ticket.closedAt = Now;

                    // Update trạng thái SLA
                    if (oldData.ResolveTime < Now)
                        Ticket.SLAStatusId = SLAStatusEnum.Fail.Id;
                    else
                        Ticket.SLAStatusId = SLAStatusEnum.Success.Id;
                }
                // Mở lại ticket
                if (Ticket.TicketStatusId != TicketStatusEnum.RESOLVED.Id && Ticket.TicketStatusId != TicketStatusEnum.CLOSED.Id && oldData.IsClose.Value)
                {
                    Ticket.ReraisedTimes = oldData.ReraisedTimes.Value + 1;
                    Ticket.IsClose = false;
                }

                // Update thời gian phản hồi còn lại và thời gian xử lý còn lại
                var frtr = (long)(oldData.FirstResponeTime.Value.Subtract(Now).TotalMinutes);
                Ticket.FirstRespTimeRemaining = frtr < 0 ? 0 : frtr;
                var rtr = (long)(oldData.ResolveTime.Value.Subtract(Now).TotalMinutes);
                Ticket.ResolveTimeRemaining = rtr < 0 ? 0 : rtr;

                await UOW.TicketRepository.Update(Ticket);
                await UOW.Commit();

                Ticket = await UOW.TicketRepository.Get(Ticket.Id);
                // Push noti ticket đã sử dụng
                NotifyUsed(Ticket);

                // Thông báo cho người phụ trách ticket
                List<UserNotification> UserNotifications = new List<UserNotification>();
                var AssignUser = await UOW.AppUserRepository.Get(Ticket.UserId);
                var CurrentUser = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                List<long> RecipientIds = new List<long>();
                RecipientIds.Add(Ticket.UserId);
                RecipientIds.Add(Ticket.CreatorId);
                foreach (var id in RecipientIds)
                {
                    UserNotification NotificationUtils = new UserNotification
                    {
                        TitleWeb = $"Thông báo từ CRM",
                        ContentWeb = $"Ticket [{Ticket.TicketNumber} - {Ticket.Name} - {Ticket.TicketIssueLevel.Name}] đã được gán cho {AssignUser.DisplayName} bởi {CurrentUser.DisplayName}",
                        LinkWebsite = $"{TicketRoute.Detail}/*".Replace("*", Ticket.Id.ToString()),
                        Time = Now,
                        Unread = true,
                        SenderId = CurrentContext.UserId,
                        RecipientId = id
                    };
                    UserNotifications.Add(NotificationUtils);
                }

                await NotificationService.BulkSend(UserNotifications);

                await Logging.CreateAuditLog(Ticket, oldData, nameof(TicketService));
                return Ticket;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Ticket> Delete(Ticket Ticket)
        {
            if (!await TicketValidator.Delete(Ticket))
                return Ticket;

            try
            {
                await UOW.Begin();
                await UOW.TicketRepository.Delete(Ticket);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Ticket, nameof(TicketService));
                return Ticket;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Ticket>> BulkDelete(List<Ticket> Tickets)
        {
            if (!await TicketValidator.BulkDelete(Tickets))
                return Tickets;

            try
            {
                await UOW.Begin();
                await UOW.TicketRepository.BulkDelete(Tickets);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Tickets, nameof(TicketService));
                return Tickets;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Ticket>> Import(List<Ticket> Tickets)
        {
            if (!await TicketValidator.Import(Tickets))
                return Tickets;
            try
            {
                await UOW.Begin();
                await UOW.TicketRepository.BulkMerge(Tickets);
                await UOW.Commit();

                await Logging.CreateAuditLog(Tickets, new { }, nameof(TicketService));
                return Tickets;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(TicketService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(TicketService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<TicketFilter> ToFilter(TicketFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<TicketFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            List<Organization> Organizations = await OrganizationService.List(new OrganizationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrganizationSelect.ALL,
                OrderBy = OrganizationOrder.Id,
                OrderType = OrderType.ASC
            });
            foreach (var currentFilter in CurrentContext.Filters)
            {
                TicketFilter subFilter = new TicketFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                    {
                        var organizationIds = FilterOrganization(Organizations, FilterPermissionDefinition.IdFilter);
                        IdFilter IdFilter = new IdFilter { In = organizationIds };
                        subFilter.OrganizationId = FilterBuilder.Merge(subFilter.OrganizationId, IdFilter);
                    }
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                            if (subFilter.UserId == null) subFilter.UserId = new IdFilter { };
                            subFilter.UserId.Equal = CurrentContext.UserId;
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                            if (subFilter.UserId == null) subFilter.UserId = new IdFilter { };
                            subFilter.UserId.NotEqual = CurrentContext.UserId;
                        }
                    }
                    if (FilterPermissionDefinition.Name == nameof(subFilter.TicketTypeId))
                        subFilter.TicketTypeId = FilterBuilder.Merge(subFilter.TicketTypeId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.DepartmentId))
                        subFilter.DepartmentId = FilterBuilder.Merge(subFilter.DepartmentId, FilterPermissionDefinition.IdFilter);


                }
            }
            return filter;
        }

        private void NotifyUsed(Ticket Ticket)
        {
            //if (Ticket.CustomerTypeId == Enums.CustomerTypeEnum.PROJECT.Id)
            //{
            //    EventMessage<CustomerProject> CustomerProjectMessage = new EventMessage<CustomerProject>
            //    {
            //        Content = new CustomerProject { Id = Ticket.CustomerId },
            //        EntityName = nameof(CustomerProject),
            //        RowId = Guid.NewGuid(),
            //        Time = StaticParams.DateTimeNow,
            //    };
            //    RabbitManager.PublishSingle(CustomerProjectMessage, RoutingKeyEnum.CustomerProjectUsed);
            //}
        }
    }
}
