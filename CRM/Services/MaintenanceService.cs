using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Handlers;
using CRM.Helpers;
using CRM.Models;
using CRM.Rpc.ticket;
using CRM.Services.MNotification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services
{
    public interface IMaintenanceService : IServiceScoped
    {
        Task CleanEventMessage();
        Task CleanHangfire();
        //Task ReminderActivity();
        Task UpdateKMSStatus();
    }
    public class MaintenanceService : IMaintenanceService
    {
        private DataContext DataContext;
        private INotificationService NotificationService;
        private ILogging Logging;
        private IRabbitManager RabbitManager;
        public MaintenanceService(DataContext DataContext, INotificationService NotificationService, ILogging Logging, IRabbitManager RabbitManager)
        {
            this.DataContext = DataContext;
            this.NotificationService = NotificationService;
            this.Logging = Logging;
            this.RabbitManager = RabbitManager;
        }

        public async Task CleanEventMessage()
        {
            DateTime Checkpoint = StaticParams.DateTimeNow.AddDays(-1);
            await DataContext.EventMessage.Where(em => em.Time < Checkpoint).DeleteFromQueryAsync();
        }

        public async Task CleanHangfire()
        {
            var commandText = @"
                TRUNCATE TABLE [HangFire].[AggregatedCounter]
                TRUNCATE TABLE[HangFire].[Counter]
                TRUNCATE TABLE[HangFire].[JobParameter]
                TRUNCATE TABLE[HangFire].[JobQueue]
                TRUNCATE TABLE[HangFire].[List]
                TRUNCATE TABLE[HangFire].[State]
                DELETE FROM[HangFire].[Job]
                DBCC CHECKIDENT('[HangFire].[Job]', reseed, 0)
                UPDATE[HangFire].[Hash] SET Value = 1 WHERE Field = 'LastJobId'";
            var result = await DataContext.Database.ExecuteSqlRawAsync(commandText);

        }
        //public async Task ReminderActivity()
        //{
        //    List<ActivityDAO> ActivityDAOs = await DataContext.Activity
        //        .AsNoTracking()
        //        .Where(x => x.ActivityStatusId != Enums.ActivityStatusEnum.DONE.Id
        //        )
        //        .ToListAsync();

        //    DateTime Now = StaticParams.DateTimeNow;
        //    List<UserNotification> UserNotifications = new List<UserNotification>();
        //    foreach (var item in ActivityDAOs)
        //    {
        //        if (item.IsAlert.HasValue && item.IsAlert.Value && item.IsAlerted.HasValue && !item.IsAlerted.Value && item.ToDate >= StaticParams.DateTimeNow)
        //        {
        //            ActivityAlertDAO ActivityAlertDAO = await DataContext.ActivityAlert.AsNoTracking().Where(x => x.ActivityId == item.Id).FirstOrDefaultAsync();
        //            if (ActivityAlertDAO == null) continue;
        //            long Time = ActivityAlertDAO.Time;
        //            if (item.ToDate.AddMinutes(-Time) <= StaticParams.DateTimeNow
        //                && ActivityAlertDAO.IsNotification.HasValue && ActivityAlertDAO.IsNotification.Value)
        //            {
        //                UserNotification NotificationUtils = new UserNotification
        //                {
        //                    TitleWeb = $"Thông báo từ CRM",
        //                    Time = Now,
        //                    Unread = true,
        //                    ContentWeb = $"Hoạt động [{item.Name}] sẽ hết hạn vào lúc {item.ToDate}. Vui lòng kiểm tra công việc và hoàn thành!",
        //                    LinkWebsite = $"{ActivityRoute.Detail}/*".Replace("*", item.Id.ToString()),
        //                    SenderId = item.UserId,
        //                    RecipientId = item.UserId,
        //                    RowId = Guid.NewGuid(),
        //                };
        //                UserNotifications.Add(NotificationUtils);

        //                await DataContext.Activity.Where(x => x.Id == item.Id)
        //                .UpdateFromQueryAsync(x => new ActivityDAO
        //                {
        //                    IsAlerted = true,
        //                    UpdatedAt = StaticParams.DateTimeNow
        //                });

        //            }
        //        }
        //        else if (item.IsEscalation.HasValue && item.IsEscalation.Value
        //            && item.ToDate < StaticParams.DateTimeNow && item.IsEscalated.HasValue
        //            && !item.IsEscalated.Value)
        //        {
        //            ActivityAlertDAO ActivityAlertDAO = await DataContext.ActivityAlert.AsNoTracking().Where(x => x.ActivityId == item.Id).FirstOrDefaultAsync();
        //            if (ActivityAlertDAO == null) continue;
        //            long Time = ActivityAlertDAO.Time;

        //            if (item.ToDate.AddMinutes(1) <= StaticParams.DateTimeNow
        //                && ActivityAlertDAO.IsNotification.HasValue && ActivityAlertDAO.IsNotification.Value)
        //            {
        //                UserNotification NotificationUtils = new UserNotification
        //                {
        //                    TitleWeb = $"Thông báo từ CRM",
        //                    Time = Now,
        //                    Unread = true,
        //                    ContentWeb = $"Hoạt động [{item.Name}] đã hết hạn lúc {item.ToDate}. Bạn chưa hoàn thành công việc đúng thời hạn.",
        //                    LinkWebsite = $"{ActivityRoute.Detail}/*".Replace("*", item.Id.ToString()),
        //                    SenderId = item.UserId,
        //                    RecipientId = item.UserId,
        //                    RowId = Guid.NewGuid(),
        //                };
        //                UserNotifications.Add(NotificationUtils);
        //                await DataContext.Activity.Where(x => x.Id == item.Id)
        //                .UpdateFromQueryAsync(x => new ActivityDAO
        //                {
        //                    IsEscalated = true,
        //                    UpdatedAt = StaticParams.DateTimeNow
        //                });
        //            }
        //        }
        //    }

        //    foreach (var UserNotification in UserNotifications)
        //    {
        //        RabbitManager.PublishSingle(new EventMessage<UserNotification>(UserNotification, UserNotification.RowId), RoutingKeyEnum.UserNotificationSend);
        //    }
        //}
        public async Task ReminderTicket()
        {
            // Lấy danh sách Ticket chưa đóng và chưa xử lý
            List<Ticket> Tickets = await DataContext.Ticket
                .AsNoTracking()
                .Where(x => x.TicketStatusId != Enums.TicketStatusEnum.RESOLVED.Id && x.TicketStatusId != Enums.TicketStatusEnum.CLOSED.Id
                ).Select(x => new Ticket()
                {
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    CustomerId = x.CustomerId,
                    UserId = x.UserId,
                    CustomerTypeId = x.CustomerTypeId,
                    CreatorId = x.CreatorId,
                    ProductId = x.ProductId,
                    ReceiveDate = x.ReceiveDate,
                    ProcessDate = x.ProcessDate,
                    FinishDate = x.FinishDate,
                    Subject = x.Subject,
                    Content = x.Content,
                    TicketIssueLevelId = x.TicketIssueLevelId,
                    TicketPriorityId = x.TicketPriorityId,
                    TicketStatusId = x.TicketStatusId,
                    TicketSourceId = x.TicketSourceId,
                    TicketNumber = x.TicketNumber,
                    DepartmentId = x.DepartmentId,
                    RelatedTicketId = x.RelatedTicketId,
                    SLA = x.SLA,
                    RelatedCallLogId = x.RelatedCallLogId,
                    ResponseMethodId = x.ResponseMethodId,
                    StatusId = x.StatusId,
                    IsAlerted = x.IsAlerted,
                    IsAlertedFRT = x.IsAlertedFRT,
                    IsEscalated = x.IsEscalated,
                    IsEscalatedFRT = x.IsEscalatedFRT,
                    Used = x.Used,
                    IsClose = x.IsClose,
                    IsWork = x.IsWork,
                    IsOpen = x.IsOpen,
                    IsWait = x.IsWait,
                    FirstResponeTime = x.FirstResponeTime,
                    ResolveTime = x.ResolveTime,
                    Customer = x.Customer == null ? null : new Customer
                    {
                        Id = x.Customer.Id,
                        Code = x.Customer.Code,
                        Name = x.Customer.Name,
                        Phone = x.Customer.Phone,
                    },
                    CustomerType = x.CustomerType == null ? null : new CustomerType
                    {
                        Id = x.CustomerType.Id,
                        Name = x.CustomerType.Name,
                        Code = x.CustomerType.Code,
                    },
                    Department = x.Department == null ? null : new Organization
                    {
                        Id = x.Department.Id,
                        Name = x.Department.Name,
                    },
                    Product = x.Product == null ? null : new Product
                    {
                        Id = x.Product.Id,
                        Name = x.Product.Name,
                    },
                    RelatedCallLog = x.RelatedCallLog == null ? null : new CallLog
                    {
                        Id = x.RelatedCallLog.Id,
                        EntityReferenceId = x.RelatedCallLog.EntityReferenceId,
                        CallTypeId = x.RelatedCallLog.CallTypeId,
                        CallEmotionId = x.RelatedCallLog.CallEmotionId,
                        AppUserId = x.RelatedCallLog.AppUserId,
                        Title = x.RelatedCallLog.Title,
                        Content = x.RelatedCallLog.Content,
                        Phone = x.RelatedCallLog.Phone,
                        CallTime = x.RelatedCallLog.CallTime,
                    },
                    RelatedTicket = x.RelatedTicket == null ? null : new Ticket
                    {
                        Id = x.RelatedTicket.Id,
                        Name = x.RelatedTicket.Name,
                        Phone = x.RelatedTicket.Phone,
                        CustomerId = x.RelatedTicket.CustomerId,
                        UserId = x.RelatedTicket.UserId,
                        ProductId = x.RelatedTicket.ProductId,
                        ReceiveDate = x.RelatedTicket.ReceiveDate,
                        ProcessDate = x.RelatedTicket.ProcessDate,
                        FinishDate = x.RelatedTicket.FinishDate,
                        Subject = x.RelatedTicket.Subject,
                        Content = x.RelatedTicket.Content,
                        TicketIssueLevelId = x.RelatedTicket.TicketIssueLevelId,
                        TicketPriorityId = x.RelatedTicket.TicketPriorityId,
                        TicketStatusId = x.RelatedTicket.TicketStatusId,
                        TicketSourceId = x.RelatedTicket.TicketSourceId,
                        TicketNumber = x.RelatedTicket.TicketNumber,
                        DepartmentId = x.RelatedTicket.DepartmentId,
                        RelatedTicketId = x.RelatedTicket.RelatedTicketId,
                        SLA = x.RelatedTicket.SLA,
                        RelatedCallLogId = x.RelatedTicket.RelatedCallLogId,
                        ResponseMethodId = x.RelatedTicket.ResponseMethodId,
                        StatusId = x.RelatedTicket.StatusId,
                        Used = x.RelatedTicket.Used,
                    },
                    Status = x.Status == null ? null : new Status
                    {
                        Id = x.Status.Id,
                        Code = x.Status.Code,
                        Name = x.Status.Name,
                    },
                    TicketIssueLevel = x.TicketIssueLevel == null ? null : new TicketIssueLevel
                    {
                        Id = x.TicketIssueLevel.Id,
                        Name = x.TicketIssueLevel.Name,
                        OrderNumber = x.TicketIssueLevel.OrderNumber,
                        TicketGroupId = x.TicketIssueLevel.TicketGroupId,
                        TicketGroup = x.TicketIssueLevel.TicketGroup == null ? null : new TicketGroup
                        {
                            Id = x.TicketIssueLevel.TicketGroup.Id,
                            Name = x.TicketIssueLevel.TicketGroup.Name,
                            TicketTypeId = x.TicketIssueLevel.TicketGroup.TicketTypeId,
                            TicketType = x.TicketIssueLevel.TicketGroup.TicketType == null ? null : new TicketType
                            {
                                Id = x.TicketIssueLevel.TicketGroup.TicketType.Id,
                                Name = x.TicketIssueLevel.TicketGroup.TicketType.Name,
                            },
                        },
                        StatusId = x.TicketIssueLevel.StatusId,
                        SLA = x.TicketIssueLevel.SLA,
                        Used = x.TicketIssueLevel.Used,
                    },
                    TicketPriority = x.TicketPriority == null ? null : new TicketPriority
                    {
                        Id = x.TicketPriority.Id,
                        Name = x.TicketPriority.Name,
                        OrderNumber = x.TicketPriority.OrderNumber,
                        ColorCode = x.TicketPriority.ColorCode,
                        StatusId = x.TicketPriority.StatusId,
                        Used = x.TicketPriority.Used,
                    },
                    TicketSource = x.TicketSource == null ? null : new TicketSource
                    {
                        Id = x.TicketSource.Id,
                        Name = x.TicketSource.Name,
                        OrderNumber = x.TicketSource.OrderNumber,
                        StatusId = x.TicketSource.StatusId,
                        Used = x.TicketSource.Used,
                    },
                    TicketStatus = x.TicketStatus == null ? null : new TicketStatus
                    {
                        Id = x.TicketStatus.Id,
                        Name = x.TicketStatus.Name,
                        OrderNumber = x.TicketStatus.OrderNumber,
                        ColorCode = x.TicketStatus.ColorCode,
                        StatusId = x.TicketStatus.StatusId,
                        Used = x.TicketStatus.Used,
                    },
                    User = x.User == null ? null : new AppUser
                    {
                        Id = x.User.Id,
                        Username = x.User.Username,
                        DisplayName = x.User.DisplayName,
                        Email = x.User.Email,
                    },
                    Creator = x.Creator == null ? null : new AppUser
                    {
                        Id = x.Creator.Id,
                        Username = x.Creator.Username,
                        DisplayName = x.Creator.DisplayName
                    },
                })
                .ToListAsync();

            // Lấy danh sách SLA
            List<SLAPolicyDAO> SLAPolicyDAOs = await DataContext.SLAPolicy.ToListAsync();

            #region Lấy thông tin nhắc nhỏ xử lý
            List<SLAAlert> SLAAlerts = await DataContext.SLAAlert.Select(x => new SLAAlert
            {
                Id = x.Id,
                TicketIssueLevelId = x.TicketIssueLevelId,
                IsNotification = x.IsNotification,
                IsMail = x.IsMail,
                IsSMS = x.IsSMS,
                Time = x.Time,
                TimeUnitId = x.TimeUnitId,
                IsAssignedToUser = x.IsAssignedToUser,
                IsAssignedToGroup = x.IsAssignedToGroup,
                SmsTemplateId = x.SmsTemplateId,
                MailTemplateId = x.MailTemplateId,
                MailTemplate = x.MailTemplate == null ? null : new MailTemplate
                {
                    Name = x.MailTemplate.Name,
                    Content = x.MailTemplate.Content
                },
                SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                {
                    Name = x.SmsTemplate.Name,
                    Content = x.SmsTemplate.Content
                },
            }).ToListAsync();

            var SLAAlertIds = SLAAlerts.Select(x => x.Id).ToList();

            List<SLAAlertMail> SLAAlertMails = await DataContext.SLAAlertMail
                .Where(x => SLAAlertIds.Contains(x.SLAAlertId.Value))
                .Select(x => new SLAAlertMail
                {
                    Id = x.Id,
                    SLAAlertId = x.SLAAlertId,
                    Mail = x.Mail
                }).ToListAsync();

            foreach (var SLAAlert in SLAAlerts)
            {
                SLAAlert.SLAAlertMails = SLAAlertMails.Where(x => x.SLAAlertId == SLAAlert.Id).ToList();
            }
            #endregion

            #region Lấy thông tin nhắc nhở phản hồi
            List<SLAAlertFRT> SLAAlertFRTs = await DataContext.SLAAlertFRT.Select(x => new SLAAlertFRT
            {
                Id = x.Id,
                TicketIssueLevelId = x.TicketIssueLevelId,
                IsNotification = x.IsNotification,
                IsMail = x.IsMail,
                IsSMS = x.IsSMS,
                Time = x.Time,
                TimeUnitId = x.TimeUnitId,
                IsAssignedToUser = x.IsAssignedToUser,
                IsAssignedToGroup = x.IsAssignedToGroup,
                SmsTemplateId = x.SmsTemplateId,
                MailTemplateId = x.MailTemplateId,
                MailTemplate = x.MailTemplate == null ? null : new MailTemplate
                {
                    Name = x.MailTemplate.Name,
                    Content = x.MailTemplate.Content
                },
                SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                {
                    Name = x.SmsTemplate.Name,
                    Content = x.SmsTemplate.Content
                },
            }).ToListAsync();

            var SLAAlertFRTIds = SLAAlertFRTs.Select(x => x.Id).ToList();

            List<SLAAlertFRTMail> SLAAlertFRTMails = await DataContext.SLAAlertFRTMail
                .Where(x => SLAAlertFRTIds.Contains(x.SLAAlertFRTId.Value))
                .Select(x => new SLAAlertFRTMail
                {
                    Id = x.Id,
                    SLAAlertFRTId = x.SLAAlertFRTId,
                    Mail = x.Mail
                }).ToListAsync();

            foreach (var SLAAlertFRT in SLAAlertFRTs)
            {
                SLAAlertFRT.SLAAlertFRTMails = SLAAlertFRTMails.Where(x => x.SLAAlertFRTId == SLAAlertFRT.Id).ToList();
            }
            #endregion

            #region Lấy thông tin cảnh báo xử lý
            List<SLAEscalation> SLAEscalations = await DataContext.SLAEscalation.Select(x => new SLAEscalation
            {
                Id = x.Id,
                TicketIssueLevelId = x.TicketIssueLevelId,
                IsNotification = x.IsNotification,
                IsMail = x.IsMail,
                IsSMS = x.IsSMS,
                Time = x.Time,
                TimeUnitId = x.TimeUnitId,
                IsAssignedToUser = x.IsAssignedToUser,
                IsAssignedToGroup = x.IsAssignedToGroup,
                SmsTemplateId = x.SmsTemplateId,
                MailTemplateId = x.MailTemplateId,
                MailTemplate = x.MailTemplate == null ? null : new MailTemplate
                {
                    Name = x.MailTemplate.Name,
                    Content = x.MailTemplate.Content
                },
                SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                {
                    Name = x.SmsTemplate.Name,
                    Content = x.SmsTemplate.Content
                },
            }).ToListAsync();

            var SLAEscalationIds = SLAEscalations.Select(x => x.Id).ToList();

            List<SLAEscalationMail> SLAEscalationMails = await DataContext.SLAEscalationMail
                .Where(x => SLAEscalationIds.Contains(x.SLAEscalationId.Value))
                .Select(x => new SLAEscalationMail
                {
                    Id = x.Id,
                    SLAEscalationId = x.SLAEscalationId,
                    Mail = x.Mail
                }).ToListAsync();

            foreach (var SLAEscalation in SLAEscalations)
            {
                SLAEscalation.SLAEscalationMails = SLAEscalationMails.Where(x => x.SLAEscalationId == SLAEscalation.Id).ToList();
            }
            #endregion

            #region Lấy thông tin cảnh báo phản hồi
            List<SLAEscalationFRT> SLAEscalationFRTs = await DataContext.SLAEscalationFRT.Select(x => new SLAEscalationFRT
            {
                Id = x.Id,
                TicketIssueLevelId = x.TicketIssueLevelId,
                IsNotification = x.IsNotification,
                IsMail = x.IsMail,
                IsSMS = x.IsSMS,
                Time = x.Time,
                TimeUnitId = x.TimeUnitId,
                IsAssignedToUser = x.IsAssignedToUser,
                IsAssignedToGroup = x.IsAssignedToGroup,
                SmsTemplateId = x.SmsTemplateId,
                MailTemplateId = x.MailTemplateId,
                MailTemplate = x.MailTemplate == null ? null : new MailTemplate
                {
                    Name = x.MailTemplate.Name,
                    Content = x.MailTemplate.Content
                },
                SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                {
                    Name = x.SmsTemplate.Name,
                    Content = x.SmsTemplate.Content
                },
            }).ToListAsync();

            var SLAEscalationFRTIds = SLAEscalationFRTs.Select(x => x.Id).ToList();

            List<SLAEscalationFRTMail> SLAEscalationFRTMails = await DataContext.SLAEscalationFRTMail
                .Where(x => SLAEscalationFRTIds.Contains(x.SLAEscalationFRTId.Value))
                .Select(x => new SLAEscalationFRTMail
                {
                    Id = x.Id,
                    SLAEscalationFRTId = x.SLAEscalationFRTId,
                    Mail = x.Mail
                }).ToListAsync();

            foreach (var SLAEscalationFRT in SLAEscalationFRTs)
            {
                SLAEscalationFRT.SLAEscalationFRTMails = SLAEscalationFRTMails.Where(x => x.SLAEscalationFRTId == SLAEscalationFRT.Id).ToList();
            }
            #endregion

            DateTime Now = StaticParams.DateTimeNow;
            List<UserNotification> UserNotifications = new List<UserNotification>();

            if (SLAPolicyDAOs.Count > 0)
            {
                foreach (var Ticket in Tickets)
                {
                    if (!Ticket.IsWork.Value || Ticket.IsClose.Value) continue;

                    SLAPolicyDAO SLAPolicyDAO = SLAPolicyDAOs.Find(p => p.TicketPriorityId == Ticket.TicketPriorityId && p.TicketIssueLevelId == Ticket.TicketIssueLevelId);
                    if (SLAPolicyDAO == null) continue;

                    DateTime ResolveTimeAt = Ticket.ResolveTime.Value;
                    DateTime FirstResponeAt = Ticket.FirstResponeTime.Value;

                    // Nhắc nhở xử lý
                    if (Ticket.IsAlerted.HasValue && !Ticket.IsAlerted.Value)
                    {
                        if (SLAPolicyDAO.IsAlert.HasValue && SLAPolicyDAO.IsAlert.Value)
                        {

                            SLAAlert SLAAlert = SLAAlerts.Find(o => o.TicketIssueLevelId == Ticket.TicketIssueLevelId);
                            if (SLAAlert == null) continue;
                            long Time = SLAAlert.Time.Value;
                            if (ResolveTimeAt.AddMinutes(-Time) <= Now && ResolveTimeAt > Now)
                            {
                                if (SLAAlert.IsNotification.HasValue && SLAAlert.IsNotification.Value)
                                {
                                    if (SLAAlert.MailTemplate != null)
                                    {
                                        UserNotification NotificationUtils = new UserNotification
                                        {
                                            TitleWeb = $"Thông báo từ CRM",
                                            Time = Now,
                                            Unread = true,
                                            ContentWeb = SLAAlert.MailTemplate.Content.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{TIME_AT}}", ResolveTimeAt.ToString()),
                                            LinkWebsite = $"{TicketRoute.Detail}/preview/*".Replace("*", Ticket.Id.ToString()),
                                            SenderId = Ticket.UserId,
                                            RecipientId = Ticket.UserId,
                                            RowId = Guid.NewGuid(),
                                        };
                                        UserNotifications.Add(NotificationUtils);
                                    }

                                }
                                if (SLAAlert.IsMail.HasValue && SLAAlert.IsMail.Value)
                                {
                                    List<Mail> Mails = new List<Mail>();
                                    List<string> emails = new List<string>();
                                    if (SLAAlert.IsAssignedToUser.HasValue && SLAAlert.IsAssignedToUser.Value)
                                    {
                                        emails.Add(Ticket.User.Email);
                                    }
                                    if (SLAAlert.SLAAlertMails.Any())
                                    {
                                        foreach (var Mail in SLAAlert.SLAAlertMails)
                                        {
                                            emails.Add(Mail.Mail);
                                        }
                                    }
                                    if (SLAAlert.MailTemplate != null)
                                    {
                                        var Subject = SLAAlert.MailTemplate.Name.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                        .Replace("{{CUSTOMER_NAME}}", Ticket.Customer.Name);

                                        var Content = SLAAlert.MailTemplate.Content.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{TIME_AT}}", ResolveTimeAt.ToString());

                                        Mail MailSend = new Mail
                                        {
                                            Recipients = emails.Where(p => !string.IsNullOrEmpty(p)).ToList(),
                                            Subject = Subject,
                                            Body = Content,
                                            RowId = Guid.NewGuid(),
                                        };
                                        Mails.Add(MailSend);
                                        List<EventMessage<Mail>> messages = Mails.Select(m => new EventMessage<Mail>(m, m.RowId)).ToList();
                                        RabbitManager.PublishList(messages, RoutingKeyEnum.MailSend);
                                    }
                                }
                                if (SLAAlert.IsSMS.HasValue && SLAAlert.IsSMS.Value)
                                {
                                    //Do something
                                }

                                await DataContext.Ticket.Where(x => x.Id == Ticket.Id)
                                .UpdateFromQueryAsync(x => new TicketDAO
                                {
                                    IsAlerted = true,
                                    UpdatedAt = StaticParams.DateTimeNow
                                });
                            }
                        }
                    }

                    // Nhắc nhở phản hồi
                    if (Ticket.IsAlertedFRT.HasValue && !Ticket.IsAlertedFRT.Value)
                    {
                        if (SLAPolicyDAO.IsAlertFRT.HasValue && SLAPolicyDAO.IsAlertFRT.Value)
                        {

                            SLAAlertFRT SLAAlertFRT = SLAAlertFRTs.Find(o => o.TicketIssueLevelId == Ticket.TicketIssueLevelId);
                            if (SLAAlertFRT == null) continue;
                            long Time = SLAAlertFRT.Time.Value;
                            if (FirstResponeAt.AddMinutes(-Time) <= Now && FirstResponeAt > Now)
                            {
                                if (SLAAlertFRT.IsNotification.HasValue && SLAAlertFRT.IsNotification.Value)
                                {
                                    if (SLAAlertFRT.MailTemplate != null)
                                    {
                                        UserNotification NotificationUtils = new UserNotification
                                        {
                                            TitleWeb = $"Thông báo từ CRM",
                                            Time = Now,
                                            Unread = true,
                                            ContentWeb = SLAAlertFRT.MailTemplate.Content.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{TIME_AT}}", FirstResponeAt.ToString()),
                                            LinkWebsite = $"{TicketRoute.Detail}/preview/*".Replace("*", Ticket.Id.ToString()),
                                            SenderId = Ticket.UserId,
                                            RecipientId = Ticket.UserId,
                                            RowId = Guid.NewGuid(),
                                        };
                                        UserNotifications.Add(NotificationUtils);
                                    }

                                }
                                if (SLAAlertFRT.IsMail.HasValue && SLAAlertFRT.IsMail.Value)
                                {
                                    List<Mail> Mails = new List<Mail>();
                                    List<string> emails = new List<string>();
                                    if (SLAAlertFRT.IsAssignedToUser.HasValue && SLAAlertFRT.IsAssignedToUser.Value)
                                    {
                                        emails.Add(Ticket.User.Email);
                                    }
                                    if (SLAAlertFRT.SLAAlertFRTMails.Any())
                                    {
                                        foreach (var Mail in SLAAlertFRT.SLAAlertFRTMails)
                                        {
                                            emails.Add(Mail.Mail);
                                        }
                                    }
                                    if (SLAAlertFRT.MailTemplate != null)
                                    {
                                        var Subject = SLAAlertFRT.MailTemplate.Name.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                        .Replace("{{CUSTOMER_NAME}}", Ticket.Customer.Name);

                                        var Content = SLAAlertFRT.MailTemplate.Content.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{TIME_AT}}", FirstResponeAt.ToString());

                                        Mail MailSend = new Mail
                                        {
                                            Recipients = emails.Where(p => !string.IsNullOrEmpty(p)).ToList(),
                                            Subject = Subject,
                                            Body = Content,
                                            RowId = Guid.NewGuid(),
                                        };
                                        Mails.Add(MailSend);
                                        List<EventMessage<Mail>> messages = Mails.Select(m => new EventMessage<Mail>(m, m.RowId)).ToList();
                                        RabbitManager.PublishList(messages, RoutingKeyEnum.MailSend);
                                    }
                                }
                                if (SLAAlertFRT.IsSMS.HasValue && SLAAlertFRT.IsSMS.Value)
                                {
                                    //Do something
                                }

                                await DataContext.Ticket.Where(x => x.Id == Ticket.Id)
                                .UpdateFromQueryAsync(x => new TicketDAO
                                {
                                    IsAlertedFRT = true,
                                    UpdatedAt = StaticParams.DateTimeNow
                                });
                            }
                        }
                    }

                    // Cảnh báo xử lý
                    if (Ticket.IsEscalated.HasValue && !Ticket.IsEscalated.Value)
                    {
                        if (SLAPolicyDAO.IsEscalation.HasValue && SLAPolicyDAO.IsEscalation.Value)
                        {

                            SLAEscalation SLAEscalation = SLAEscalations.Find(o => o.TicketIssueLevelId == Ticket.TicketIssueLevelId);
                            if (SLAEscalation == null) continue;
                            long Time = SLAEscalation.Time.Value;
                            if (ResolveTimeAt.AddMinutes(Time) <= Now)
                            {
                                if (SLAEscalation.IsNotification.HasValue && SLAEscalation.IsNotification.Value)
                                {
                                    if (SLAEscalation.MailTemplate != null)
                                    {
                                        UserNotification NotificationUtils = new UserNotification
                                        {
                                            TitleWeb = $"Thông báo từ CRM",
                                            Time = Now,
                                            Unread = true,
                                            ContentWeb = SLAEscalation.MailTemplate.Content.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{TIME_AT}}", ResolveTimeAt.ToString()),
                                            LinkWebsite = $"{TicketRoute.Detail}/preview/*".Replace("*", Ticket.Id.ToString()),
                                            SenderId = Ticket.UserId,
                                            RecipientId = Ticket.UserId,
                                            RowId = Guid.NewGuid(),
                                        };
                                        UserNotifications.Add(NotificationUtils);
                                    }

                                }
                                if (SLAEscalation.IsMail.HasValue && SLAEscalation.IsMail.Value)
                                {
                                    List<Mail> Mails = new List<Mail>();
                                    List<string> emails = new List<string>();
                                    if (SLAEscalation.IsAssignedToUser.HasValue && SLAEscalation.IsAssignedToUser.Value)
                                    {
                                        emails.Add(Ticket.User.Email);
                                    }
                                    if (SLAEscalation.SLAEscalationMails.Any())
                                    {
                                        foreach (var Mail in SLAEscalation.SLAEscalationMails)
                                        {
                                            emails.Add(Mail.Mail);
                                        }
                                    }
                                    if (SLAEscalation.MailTemplate != null)
                                    {
                                        var Subject = SLAEscalation.MailTemplate.Name.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                        .Replace("{{CUSTOMER_NAME}}", Ticket.Customer.Name);

                                        var Content = SLAEscalation.MailTemplate.Content.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{TIME_AT}}", ResolveTimeAt.ToString());

                                        Mail MailSend = new Mail
                                        {
                                            Recipients = emails.Where(p => !string.IsNullOrEmpty(p)).ToList(),
                                            Subject = Subject,
                                            Body = Content,
                                            RowId = Guid.NewGuid(),
                                        };
                                        Mails.Add(MailSend);
                                        List<EventMessage<Mail>> messages = Mails.Select(m => new EventMessage<Mail>(m, m.RowId)).ToList();
                                        RabbitManager.PublishList(messages, RoutingKeyEnum.MailSend);
                                    }
                                }
                                if (SLAEscalation.IsSMS.HasValue && SLAEscalation.IsSMS.Value)
                                {
                                    //Do something
                                }

                                await DataContext.Ticket.Where(x => x.Id == Ticket.Id)
                                .UpdateFromQueryAsync(x => new TicketDAO
                                {
                                    IsEscalated = true,
                                    UpdatedAt = StaticParams.DateTimeNow
                                });
                            }
                        }
                    }

                    // Cảnh báo phản hồi
                    if (Ticket.IsEscalatedFRT.HasValue && !Ticket.IsEscalatedFRT.Value)
                    {
                        if (SLAPolicyDAO.IsEscalationFRT.HasValue && SLAPolicyDAO.IsEscalationFRT.Value)
                        {

                            SLAEscalationFRT SLAEscalationFRT = SLAEscalationFRTs.Find(o => o.TicketIssueLevelId == Ticket.TicketIssueLevelId);
                            if (SLAEscalationFRT == null) continue;
                            long Time = SLAEscalationFRT.Time.Value;
                            if (FirstResponeAt.AddMinutes(Time) <= Now)
                            {
                                if (SLAEscalationFRT.IsNotification.HasValue && SLAEscalationFRT.IsNotification.Value)
                                {
                                    if (SLAEscalationFRT.MailTemplate != null)
                                    {
                                        UserNotification NotificationUtils = new UserNotification
                                        {
                                            TitleWeb = $"Thông báo từ CRM",
                                            Time = Now,
                                            Unread = true,
                                            ContentWeb = SLAEscalationFRT.MailTemplate.Content.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{TIME_AT}}", FirstResponeAt.ToString()),
                                            LinkWebsite = $"{TicketRoute.Detail}/preview/*".Replace("*", Ticket.Id.ToString()),
                                            SenderId = Ticket.UserId,
                                            RecipientId = Ticket.UserId,
                                            RowId = Guid.NewGuid(),
                                        };
                                        UserNotifications.Add(NotificationUtils);
                                    }

                                }
                                if (SLAEscalationFRT.IsMail.HasValue && SLAEscalationFRT.IsMail.Value)
                                {
                                    List<Mail> Mails = new List<Mail>();
                                    List<string> emails = new List<string>();
                                    if (SLAEscalationFRT.IsAssignedToUser.HasValue && SLAEscalationFRT.IsAssignedToUser.Value)
                                    {
                                        emails.Add(Ticket.User.Email);
                                    }
                                    if (SLAEscalationFRT.SLAEscalationFRTMails.Any())
                                    {
                                        foreach (var Mail in SLAEscalationFRT.SLAEscalationFRTMails)
                                        {
                                            emails.Add(Mail.Mail);
                                        }
                                    }
                                    if (SLAEscalationFRT.MailTemplate != null)
                                    {
                                        var Subject = SLAEscalationFRT.MailTemplate.Name.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                        .Replace("{{CUSTOMER_NAME}}", Ticket.Customer.Name);

                                        var Content = SLAEscalationFRT.MailTemplate.Content.Replace("{{TICKET_NUMBER}}", Ticket.TicketNumber)
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{TIME_AT}}", FirstResponeAt.ToString());

                                        Mail MailSend = new Mail
                                        {
                                            Recipients = emails.Where(p => !string.IsNullOrEmpty(p)).ToList(),
                                            Subject = Subject,
                                            Body = Content,
                                            RowId = Guid.NewGuid(),
                                        };
                                        Mails.Add(MailSend);
                                        List<EventMessage<Mail>> messages = Mails.Select(m => new EventMessage<Mail>(m, m.RowId)).ToList();
                                        RabbitManager.PublishList(messages, RoutingKeyEnum.MailSend);
                                    }
                                }
                                if (SLAEscalationFRT.IsSMS.HasValue && SLAEscalationFRT.IsSMS.Value)
                                {
                                    //Do something
                                }

                                await DataContext.Ticket.Where(x => x.Id == Ticket.Id)
                                .UpdateFromQueryAsync(x => new TicketDAO
                                {
                                    IsEscalatedFRT = true,
                                    UpdatedAt = StaticParams.DateTimeNow
                                });
                            }
                        }
                    }
                }
            }
            List<EventMessage<UserNotification>> EventUserNotifications = UserNotifications.Select(x => new EventMessage<UserNotification>(x, x.RowId)).ToList();
            RabbitManager.PublishList(EventUserNotifications, RoutingKeyEnum.UserNotificationSend);
        }
        public async Task UpdateTicketSLAStatus()
        {
            var Now = StaticParams.DateTimeNow;
            List<TicketDAO> TicketDAOs = await DataContext.Ticket.Where(x => !x.SLAStatusId.HasValue).ToListAsync();

            foreach (var item in TicketDAOs)
            {
                if (item.ResolveTime < Now)
                {
                    await DataContext.Ticket.Where(x => x.Id == item.Id)
                        .UpdateFromQueryAsync(x => new TicketDAO
                        {
                            SLAStatusId = SLAStatusEnum.Fail.Id,
                            UpdatedAt = Now
                        });
                }
            }
        }
        public async Task UpdateKMSStatus()
        {
            var now = StaticParams.DateTimeNow;
            var KnowledgeArticles = await DataContext.KnowledgeArticle.Where(p => !p.DeletedAt.HasValue).ToListAsync();
            var KnowledgeArticleDoings = new List<KnowledgeArticleDAO>();
            var KnowledgeArticleExpireds = new List<KnowledgeArticleDAO>();
            foreach (var KnowledgeArticle in KnowledgeArticles)
            {
                var Now = StaticParams.DateTimeNow;
                if (Now >= KnowledgeArticle.FromDate && (KnowledgeArticle.ToDate.HasValue == false || KnowledgeArticle.ToDate >= Now))
                {
                    KnowledgeArticleDoings.Add(KnowledgeArticle);
                }
                else if (KnowledgeArticle.ToDate < Now)
                {
                    KnowledgeArticleExpireds.Add(KnowledgeArticle);
                }
            }
            if(KnowledgeArticleDoings.Count > 0)
            {
                var Ids = KnowledgeArticleDoings.Select(x => x.Id).ToList();
                await DataContext.KnowledgeArticle.Where(x => Ids.Contains(x.Id))
                                .UpdateFromQueryAsync(x => new KnowledgeArticleDAO
                                {
                                    KMSStatusId = KMSStatusEnum.DOING.Id,
                                    UpdatedAt = StaticParams.DateTimeNow
                                });
            }
            if (KnowledgeArticleExpireds.Count > 0)
            {
                var Ids = KnowledgeArticleExpireds.Select(x => x.Id).ToList();
                await DataContext.KnowledgeArticle.Where(x => Ids.Contains(x.Id))
                                .UpdateFromQueryAsync(x => new KnowledgeArticleDAO
                                {
                                    KMSStatusId = KMSStatusEnum.EXPIRED.Id,
                                    UpdatedAt = StaticParams.DateTimeNow
                                });
            }
        }
    }
}
