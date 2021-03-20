using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public interface ITicketIssueLevelRepository
    {
        Task<int> Count(TicketIssueLevelFilter TicketIssueLevelFilter);
        Task<List<TicketIssueLevel>> List(TicketIssueLevelFilter TicketIssueLevelFilter);
        Task<TicketIssueLevel> Get(long Id);
        Task<bool> Create(TicketIssueLevel TicketIssueLevel);
        Task<bool> Update(TicketIssueLevel TicketIssueLevel);
        Task<bool> Delete(TicketIssueLevel TicketIssueLevel);
        Task<bool> BulkMerge(List<TicketIssueLevel> TicketIssueLevels);
        Task<bool> BulkDelete(List<TicketIssueLevel> TicketIssueLevels);
    }
    public class TicketIssueLevelRepository : ITicketIssueLevelRepository
    {
        private DataContext DataContext;
        public TicketIssueLevelRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketIssueLevelDAO> DynamicFilter(IQueryable<TicketIssueLevelDAO> query, TicketIssueLevelFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.OrderNumber != null)
                query = query.Where(q => q.OrderNumber, filter.OrderNumber);
            if (filter.TicketGroupId != null)
                query = query.Where(q => q.TicketGroupId, filter.TicketGroupId);
            if (filter.TicketTypeId != null)
                query = query.Where(q => q.TicketGroup.TicketTypeId, filter.TicketTypeId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.SLA != null)
                query = query.Where(q => q.SLA, filter.SLA);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<TicketIssueLevelDAO> OrFilter(IQueryable<TicketIssueLevelDAO> query, TicketIssueLevelFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketIssueLevelDAO> initQuery = query.Where(q => false);
            foreach (TicketIssueLevelFilter TicketIssueLevelFilter in filter.OrFilter)
            {
                IQueryable<TicketIssueLevelDAO> queryable = query;
                if (TicketIssueLevelFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TicketIssueLevelFilter.Id);
                if (TicketIssueLevelFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, TicketIssueLevelFilter.Name);
                if (TicketIssueLevelFilter.OrderNumber != null)
                    queryable = queryable.Where(q => q.OrderNumber, TicketIssueLevelFilter.OrderNumber);
                if (TicketIssueLevelFilter.TicketGroupId != null)
                    queryable = queryable.Where(q => q.TicketGroupId, TicketIssueLevelFilter.TicketGroupId);
                if (TicketIssueLevelFilter.TicketTypeId != null)
                    queryable = queryable.Where(q => q.TicketGroup.TicketTypeId, TicketIssueLevelFilter.TicketTypeId);
                if (TicketIssueLevelFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, TicketIssueLevelFilter.StatusId);
                if (TicketIssueLevelFilter.SLA != null)
                    queryable = queryable.Where(q => q.SLA, TicketIssueLevelFilter.SLA);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketIssueLevelDAO> DynamicOrder(IQueryable<TicketIssueLevelDAO> query, TicketIssueLevelFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketIssueLevelOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketIssueLevelOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TicketIssueLevelOrder.OrderNumber:
                            query = query.OrderBy(q => q.OrderNumber);
                            break;
                        case TicketIssueLevelOrder.TicketGroup:
                            query = query.OrderBy(q => q.TicketGroupId);
                            break;
                        case TicketIssueLevelOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case TicketIssueLevelOrder.SLA:
                            query = query.OrderBy(q => q.SLA);
                            break;
                        case TicketIssueLevelOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketIssueLevelOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketIssueLevelOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TicketIssueLevelOrder.OrderNumber:
                            query = query.OrderByDescending(q => q.OrderNumber);
                            break;
                        case TicketIssueLevelOrder.TicketGroup:
                            query = query.OrderByDescending(q => q.TicketGroupId);
                            break;
                        case TicketIssueLevelOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case TicketIssueLevelOrder.SLA:
                            query = query.OrderByDescending(q => q.SLA);
                            break;
                        case TicketIssueLevelOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketIssueLevel>> DynamicSelect(IQueryable<TicketIssueLevelDAO> query, TicketIssueLevelFilter filter)
        {
            List<TicketIssueLevel> TicketIssueLevels = await query.Select(q => new TicketIssueLevel()
            {
                Id = filter.Selects.Contains(TicketIssueLevelSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(TicketIssueLevelSelect.Name) ? q.Name : default(string),
                OrderNumber = filter.Selects.Contains(TicketIssueLevelSelect.OrderNumber) ? q.OrderNumber : default(long),
                TicketGroupId = filter.Selects.Contains(TicketIssueLevelSelect.TicketGroup) ? q.TicketGroupId : default(long),
                StatusId = filter.Selects.Contains(TicketIssueLevelSelect.Status) ? q.StatusId : default(long),
                SLA = filter.Selects.Contains(TicketIssueLevelSelect.SLA) ? q.SLA : default(long),
                Used = filter.Selects.Contains(TicketIssueLevelSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(TicketIssueLevelSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                TicketGroup = filter.Selects.Contains(TicketIssueLevelSelect.TicketGroup) && q.TicketGroup != null ? new TicketGroup
                {
                    Id = q.TicketGroup.Id,
                    Name = q.TicketGroup.Name,
                    OrderNumber = q.TicketGroup.OrderNumber,
                    StatusId = q.TicketGroup.StatusId,
                    TicketTypeId = q.TicketGroup.TicketTypeId,
                    TicketType = q.TicketGroup.TicketType == null ? null : new TicketType { 
                        Id = q.TicketGroup.TicketType.Id,
                        Code = q.TicketGroup.TicketType.Code,
                        Name = q.TicketGroup.TicketType.Name,
                        StatusId = q.TicketGroup.TicketType.StatusId,
                        Used = q.TicketGroup.TicketType.Used,
                    },
                    Used = q.TicketGroup.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketIssueLevels;
        }

        public async Task<int> Count(TicketIssueLevelFilter filter)
        {
            IQueryable<TicketIssueLevelDAO> TicketIssueLevels = DataContext.TicketIssueLevel.AsNoTracking();
            TicketIssueLevels = DynamicFilter(TicketIssueLevels, filter);
            return await TicketIssueLevels.CountAsync();
        }

        public async Task<List<TicketIssueLevel>> List(TicketIssueLevelFilter filter)
        {
            if (filter == null) return new List<TicketIssueLevel>();
            IQueryable<TicketIssueLevelDAO> TicketIssueLevelDAOs = DataContext.TicketIssueLevel.AsNoTracking();
            TicketIssueLevelDAOs = DynamicFilter(TicketIssueLevelDAOs, filter);
            TicketIssueLevelDAOs = DynamicOrder(TicketIssueLevelDAOs, filter);
            List<TicketIssueLevel> TicketIssueLevels = await DynamicSelect(TicketIssueLevelDAOs, filter);
            return TicketIssueLevels;
        }

        public async Task<TicketIssueLevel> Get(long Id)
        {
            TicketIssueLevel TicketIssueLevel = await DataContext.TicketIssueLevel.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketIssueLevel()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                OrderNumber = x.OrderNumber,
                TicketGroupId = x.TicketGroupId,
                StatusId = x.StatusId,
                SLA = x.SLA,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                TicketGroup = x.TicketGroup == null ? null : new TicketGroup
                {
                    Id = x.TicketGroup.Id,
                    Name = x.TicketGroup.Name,
                    OrderNumber = x.TicketGroup.OrderNumber,
                    StatusId = x.TicketGroup.StatusId,
                    TicketTypeId = x.TicketGroup.TicketTypeId,
                    TicketType = x.TicketGroup.TicketType == null ? null : new TicketType { 
                        Id = x.TicketGroup.TicketType.Id,
                        Code = x.TicketGroup.TicketType.Code,
                        Name = x.TicketGroup.TicketType.Name,
                        StatusId = x.TicketGroup.TicketType.StatusId,
                        Used = x.TicketGroup.TicketType.Used,
                    },
                    Used = x.TicketGroup.Used,
                },
            }).FirstOrDefaultAsync();

            if (TicketIssueLevel == null)
                return null;

            TicketIssueLevel.SLAPolicies = await DataContext.SLAPolicy.AsNoTracking()
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .Select(x => new SLAPolicy
                {
                    Id = x.Id,
                    TicketIssueLevelId = x.TicketIssueLevelId,
                    TicketPriorityId = x.TicketPriorityId,
                    TicketPriority = x.TicketPriority == null ? null : new TicketPriority
                    {
                        Id = x.TicketPriority.Id,
                        Name = x.TicketPriority.Name,
                        OrderNumber = x.TicketPriority.OrderNumber
                    },
                    FirstResponseTime = x.FirstResponseTime,
                    FirstResponseUnitId = x.FirstResponseUnitId,
                    FirstResponseUnit = x.FirstResponseUnit == null ? null : new SLATimeUnit
                    {
                        Id = x.ResolveUnit.Id,
                        Code = x.ResolveUnit.Code,
                        Name = x.ResolveUnit.Name
                    },
                    ResolveTime = x.ResolveTime,
                    ResolveUnitId = x.ResolveUnitId,
                    ResolveUnit = x.ResolveUnit == null ? null : new SLATimeUnit
                    {
                        Id = x.ResolveUnit.Id,
                        Code = x.ResolveUnit.Code,
                        Name = x.ResolveUnit.Name
                    },
                    IsAlert = x.IsAlert,
                    IsAlertFRT = x.IsAlertFRT,
                    IsEscalation = x.IsEscalation,
                    IsEscalationFRT = x.IsEscalationFRT,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).ToListAsync();
            //SLAAlerts
            TicketIssueLevel.SLAAlerts = await DataContext.SLAAlert.AsNoTracking()
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .Select(x => new SLAAlert
                {
                    Id = x.Id,
                    TicketIssueLevelId = x.TicketIssueLevelId,
                    IsNotification = x.IsNotification,
                    IsMail = x.IsMail,
                    IsSMS = x.IsSMS,
                    Time = x.Time,
                    IsAssignedToGroup = x.IsAssignedToGroup,
                    IsAssignedToUser = x.IsAssignedToUser,
                    MailTemplateId = x.MailTemplateId,
                    MailTemplate = x.MailTemplate == null ? null : new MailTemplate
                    {
                        Id = x.MailTemplate.Id,
                        Code = x.MailTemplate.Code,
                        Name = x.MailTemplate.Name,
                        Content = x.MailTemplate.Content
                    },
                    SmsTemplateId = x.SmsTemplateId,
                    SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                    {
                        Id = x.SmsTemplate.Id,
                        Code = x.SmsTemplate.Code,
                        Name = x.SmsTemplate.Name,
                        Content = x.SmsTemplate.Content
                    },
                    TimeUnitId = x.TimeUnitId,
                    TimeUnit = x.TimeUnit == null ? null : new SLATimeUnit{
                        Id = x.TimeUnit.Id,
                        Code = x.TimeUnit.Code,
                        Name = x.TimeUnit.Name
                    },
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).ToListAsync();

            var SLAAlertIds = TicketIssueLevel.SLAAlerts.Select(x => x.Id).ToList();
            List<SLAAlertMail> SLAAlertMails = await DataContext.SLAAlertMail
                .Where(x => SLAAlertIds.Contains(x.SLAAlertId.Value))
                .Select(x => new SLAAlertMail {
                    Id = x.Id,
                    SLAAlertId = x.SLAAlertId,
                    Mail = x.Mail
                }).ToListAsync();

            List<SLAAlertPhone> SLAAlertPhones = await DataContext.SLAAlertPhone
                .Where(x => SLAAlertIds.Contains(x.SLAAlertId.Value))
                .Select(x => new SLAAlertPhone
                {
                    Id = x.Id,
                    SLAAlertId = x.SLAAlertId,
                    Phone = x.Phone
                }).ToListAsync();

            List<SLAAlertUser> SLAAlertUsers = await DataContext.SLAAlertUser
                .Where(x => SLAAlertIds.Contains(x.SLAAlertId.Value))
                .Select(x => new SLAAlertUser
                {
                    Id = x.Id,
                    SLAAlertId = x.SLAAlertId,
                    AppUserId = x.AppUserId,
                    AppUser = x.AppUser == null ? null : new AppUser
                    {
                        Id = x.AppUser.Id,
                        Username = x.AppUser.Username,
                        DisplayName = x.AppUser.DisplayName
                    }
                }).ToListAsync();

            foreach (SLAAlert SLAAlert in TicketIssueLevel.SLAAlerts)
            {
                SLAAlert.SLAAlertMails = SLAAlertMails.Where(x => x.SLAAlertId == SLAAlert.Id).ToList();
                SLAAlert.SLAAlertPhones = SLAAlertPhones.Where(x => x.SLAAlertId == SLAAlert.Id).ToList();
                SLAAlert.SLAAlertUsers = SLAAlertUsers.Where(x => x.SLAAlertId == SLAAlert.Id).ToList();
            }


            //SLAAlertFRTs
            TicketIssueLevel.SLAAlertFRTs = await DataContext.SLAAlertFRT.AsNoTracking()
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .Select(x => new SLAAlertFRT
                {
                    Id = x.Id,
                    TicketIssueLevelId = x.TicketIssueLevelId,
                    IsNotification = x.IsNotification,
                    IsMail = x.IsMail,
                    IsSMS = x.IsSMS,
                    Time = x.Time,
                    IsAssignedToGroup = x.IsAssignedToGroup,
                    IsAssignedToUser = x.IsAssignedToUser,
                    MailTemplateId = x.MailTemplateId,
                    MailTemplate = x.MailTemplate == null ? null : new MailTemplate
                    {
                        Id = x.MailTemplate.Id,
                        Code = x.MailTemplate.Code,
                        Name = x.MailTemplate.Name,
                        Content = x.MailTemplate.Content
                    },
                    SmsTemplateId = x.SmsTemplateId,
                    SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                    {
                        Id = x.SmsTemplate.Id,
                        Code = x.SmsTemplate.Code,
                        Name = x.SmsTemplate.Name,
                        Content = x.SmsTemplate.Content
                    },
                    TimeUnitId = x.TimeUnitId,
                    TimeUnit = x.TimeUnit == null ? null : new SLATimeUnit
                    {
                        Id = x.TimeUnit.Id,
                        Code = x.TimeUnit.Code,
                        Name = x.TimeUnit.Name
                    },
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).ToListAsync();

            var SLAAlertFRTIds = TicketIssueLevel.SLAAlertFRTs.Select(x => x.Id).ToList();
            List<SLAAlertFRTMail> SLAAlertFRTMails = await DataContext.SLAAlertFRTMail
                .Where(x => SLAAlertFRTIds.Contains(x.SLAAlertFRTId.Value))
                .Select(x => new SLAAlertFRTMail
                {
                    Id = x.Id,
                    SLAAlertFRTId = x.SLAAlertFRTId,
                    Mail = x.Mail
                }).ToListAsync();

            List<SLAAlertFRTPhone> SLAAlertFRTPhones = await DataContext.SLAAlertFRTPhone
                .Where(x => SLAAlertFRTIds.Contains(x.SLAAlertFRTId.Value))
                .Select(x => new SLAAlertFRTPhone
                {
                    Id = x.Id,
                    SLAAlertFRTId = x.SLAAlertFRTId,
                    Phone = x.Phone
                }).ToListAsync();

            List<SLAAlertFRTUser> SLAAlertFRTUsers = await DataContext.SLAAlertFRTUser
                .Where(x => SLAAlertFRTIds.Contains(x.SLAAlertFRTId.Value))
                .Select(x => new SLAAlertFRTUser
                {
                    Id = x.Id,
                    SLAAlertFRTId = x.SLAAlertFRTId,
                    AppUserId = x.AppUserId,
                    AppUser = x.AppUser == null ? null : new AppUser
                    {
                        Id = x.AppUser.Id,
                        Username = x.AppUser.Username,
                        DisplayName = x.AppUser.DisplayName
                    }
                }).ToListAsync();

            foreach (SLAAlertFRT SLAAlertFRT in TicketIssueLevel.SLAAlertFRTs)
            {
                SLAAlertFRT.SLAAlertFRTMails = SLAAlertFRTMails.Where(x => x.SLAAlertFRTId == SLAAlertFRT.Id).ToList();
                SLAAlertFRT.SLAAlertFRTPhones = SLAAlertFRTPhones.Where(x => x.SLAAlertFRTId == SLAAlertFRT.Id).ToList();
                SLAAlertFRT.SLAAlertFRTUsers = SLAAlertFRTUsers.Where(x => x.SLAAlertFRTId == SLAAlertFRT.Id).ToList();
            }

            //SLAEscalations
            TicketIssueLevel.SLAEscalations = await DataContext.SLAEscalation.AsNoTracking()
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .Select(x => new SLAEscalation
                {
                    Id = x.Id,
                    TicketIssueLevelId = x.TicketIssueLevelId,
                    IsNotification = x.IsNotification,
                    IsMail = x.IsMail,
                    IsSMS = x.IsSMS,
                    Time = x.Time,
                    IsAssignedToGroup = x.IsAssignedToGroup,
                    IsAssignedToUser = x.IsAssignedToUser,
                    MailTemplateId = x.MailTemplateId,
                    MailTemplate = x.MailTemplate == null ? null : new MailTemplate
                    {
                        Id = x.MailTemplate.Id,
                        Code = x.MailTemplate.Code,
                        Name = x.MailTemplate.Name,
                        Content = x.MailTemplate.Content
                    },
                    SmsTemplateId = x.SmsTemplateId,
                    SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                    {
                        Id = x.SmsTemplate.Id,
                        Code = x.SmsTemplate.Code,
                        Name = x.SmsTemplate.Name,
                        Content = x.SmsTemplate.Content
                    },
                    TimeUnitId = x.TimeUnitId,
                    TimeUnit = x.TimeUnit == null ? null : new SLATimeUnit
                    {
                        Id = x.TimeUnit.Id,
                        Code = x.TimeUnit.Code,
                        Name = x.TimeUnit.Name
                    },
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).ToListAsync();

            var SLAEscalationIds = TicketIssueLevel.SLAEscalations.Select(x => x.Id).ToList();
            List<SLAEscalationMail> SLAEscalationMails = await DataContext.SLAEscalationMail
                .Where(x => SLAEscalationIds.Contains(x.SLAEscalationId.Value))
                .Select(x => new SLAEscalationMail
                {
                    Id = x.Id,
                    SLAEscalationId = x.SLAEscalationId,
                    Mail = x.Mail
                }).ToListAsync();

            List<SLAEscalationPhone> SLAEscalationPhones = await DataContext.SLAEscalationPhone
                .Where(x => SLAEscalationIds.Contains(x.SLAEscalationId.Value))
                .Select(x => new SLAEscalationPhone
                {
                    Id = x.Id,
                    SLAEscalationId = x.SLAEscalationId,
                    Phone = x.Phone
                }).ToListAsync();

            List<SLAEscalationUser> SLAEscalationUsers = await DataContext.SLAEscalationUser
                .Where(x => SLAEscalationIds.Contains(x.SLAEscalationId.Value))
                .Select(x => new SLAEscalationUser
                {
                    Id = x.Id,
                    SLAEscalationId = x.SLAEscalationId,
                    AppUserId = x.AppUserId,
                    AppUser = x.AppUser == null ? null : new AppUser
                    {
                        Id = x.AppUser.Id,
                        Username = x.AppUser.Username,
                        DisplayName = x.AppUser.DisplayName
                    }
                }).ToListAsync();

            foreach (SLAEscalation SLAEscalation in TicketIssueLevel.SLAEscalations)
            {
                SLAEscalation.SLAEscalationMails = SLAEscalationMails.Where(x => x.SLAEscalationId == SLAEscalation.Id).ToList();
                SLAEscalation.SLAEscalationPhones = SLAEscalationPhones.Where(x => x.SLAEscalationId == SLAEscalation.Id).ToList();
                SLAEscalation.SLAEscalationUsers = SLAEscalationUsers.Where(x => x.SLAEscalationId == SLAEscalation.Id).ToList();
            }

            //SLAEscalationFRTs
            TicketIssueLevel.SLAEscalationFRTs = await DataContext.SLAEscalationFRT.AsNoTracking()
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .Select(x => new SLAEscalationFRT
                {
                    Id = x.Id,
                    TicketIssueLevelId = x.TicketIssueLevelId,
                    IsNotification = x.IsNotification,
                    IsMail = x.IsMail,
                    IsSMS = x.IsSMS,
                    Time = x.Time,
                    IsAssignedToGroup = x.IsAssignedToGroup,
                    IsAssignedToUser = x.IsAssignedToUser,
                    MailTemplateId = x.MailTemplateId,
                    MailTemplate = x.MailTemplate == null ? null : new MailTemplate
                    {
                        Id = x.MailTemplate.Id,
                        Code = x.MailTemplate.Code,
                        Name = x.MailTemplate.Name,
                        Content = x.MailTemplate.Content
                    },
                    SmsTemplateId = x.SmsTemplateId,
                    SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                    {
                        Id = x.SmsTemplate.Id,
                        Code = x.SmsTemplate.Code,
                        Name = x.SmsTemplate.Name,
                        Content = x.SmsTemplate.Content
                    },
                    TimeUnitId = x.TimeUnitId,
                    TimeUnit = x.TimeUnit == null ? null : new SLATimeUnit
                    {
                        Id = x.TimeUnit.Id,
                        Code = x.TimeUnit.Code,
                        Name = x.TimeUnit.Name
                    },
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).ToListAsync();

            var SLAEscalationFRTIds = TicketIssueLevel.SLAEscalationFRTs.Select(x => x.Id).ToList();
            List<SLAEscalationFRTMail> SLAEscalationFRTMails = await DataContext.SLAEscalationFRTMail
                .Where(x => SLAEscalationFRTIds.Contains(x.SLAEscalationFRTId.Value))
                .Select(x => new SLAEscalationFRTMail
                {
                    Id = x.Id,
                    SLAEscalationFRTId = x.SLAEscalationFRTId,
                    Mail = x.Mail
                }).ToListAsync();

            List<SLAEscalationFRTPhone> SLAEscalationFRTPhones = await DataContext.SLAEscalationFRTPhone
                .Where(x => SLAEscalationFRTIds.Contains(x.SLAEscalationFRTId.Value))
                .Select(x => new SLAEscalationFRTPhone
                {
                    Id = x.Id,
                    SLAEscalationFRTId = x.SLAEscalationFRTId,
                    Phone = x.Phone
                }).ToListAsync();

            List<SLAEscalationFRTUser> SLAEscalationFRTUsers = await DataContext.SLAEscalationFRTUser
                .Where(x => SLAEscalationFRTIds.Contains(x.SLAEscalationFRTId.Value))
                .Select(x => new SLAEscalationFRTUser
                {
                    Id = x.Id,
                    SLAEscalationFRTId = x.SLAEscalationFRTId,
                    AppUserId = x.AppUserId,
                    AppUser = x.AppUser == null ? null : new AppUser
                    {
                        Id = x.AppUser.Id,
                        Username = x.AppUser.Username,
                        DisplayName = x.AppUser.DisplayName
                    }
                }).ToListAsync();

            foreach (SLAEscalationFRT SLAEscalationFRT in TicketIssueLevel.SLAEscalationFRTs)
            {
                SLAEscalationFRT.SLAEscalationFRTMails = SLAEscalationFRTMails.Where(x => x.SLAEscalationFRTId == SLAEscalationFRT.Id).ToList();
                SLAEscalationFRT.SLAEscalationFRTPhones = SLAEscalationFRTPhones.Where(x => x.SLAEscalationFRTId == SLAEscalationFRT.Id).ToList();
                SLAEscalationFRT.SLAEscalationFRTUsers = SLAEscalationFRTUsers.Where(x => x.SLAEscalationFRTId == SLAEscalationFRT.Id).ToList();
            }


            return TicketIssueLevel;
        }
        public async Task<bool> Create(TicketIssueLevel TicketIssueLevel)
        {
            TicketIssueLevelDAO TicketIssueLevelDAO = new TicketIssueLevelDAO();
            TicketIssueLevelDAO.Id = TicketIssueLevel.Id;
            TicketIssueLevelDAO.Name = TicketIssueLevel.Name;
            TicketIssueLevelDAO.OrderNumber = TicketIssueLevel.OrderNumber;
            TicketIssueLevelDAO.TicketGroupId = TicketIssueLevel.TicketGroupId;
            TicketIssueLevelDAO.StatusId = TicketIssueLevel.StatusId;
            TicketIssueLevelDAO.SLA = TicketIssueLevel.SLA;
            TicketIssueLevelDAO.Used = TicketIssueLevel.Used;
            TicketIssueLevelDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketIssueLevelDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketIssueLevel.Add(TicketIssueLevelDAO);
            await DataContext.SaveChangesAsync();
            TicketIssueLevel.Id = TicketIssueLevelDAO.Id;
            await SaveReference(TicketIssueLevel);
            return true;
        }

        public async Task<bool> Update(TicketIssueLevel TicketIssueLevel)
        {
            TicketIssueLevelDAO TicketIssueLevelDAO = DataContext.TicketIssueLevel.Where(x => x.Id == TicketIssueLevel.Id).FirstOrDefault();
            if (TicketIssueLevelDAO == null)
                return false;
            TicketIssueLevelDAO.Id = TicketIssueLevel.Id;
            TicketIssueLevelDAO.Name = TicketIssueLevel.Name;
            TicketIssueLevelDAO.OrderNumber = TicketIssueLevel.OrderNumber;
            TicketIssueLevelDAO.TicketGroupId = TicketIssueLevel.TicketGroupId;
            TicketIssueLevelDAO.StatusId = TicketIssueLevel.StatusId;
            TicketIssueLevelDAO.SLA = TicketIssueLevel.SLA;
            TicketIssueLevelDAO.Used = TicketIssueLevel.Used;
            TicketIssueLevelDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(TicketIssueLevel);
            return true;
        }

        public async Task<bool> Delete(TicketIssueLevel TicketIssueLevel)
        {
            await DataContext.TicketIssueLevel.Where(x => x.Id == TicketIssueLevel.Id).UpdateFromQueryAsync(x => new TicketIssueLevelDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<TicketIssueLevel> TicketIssueLevels)
        {
            List<TicketIssueLevelDAO> TicketIssueLevelDAOs = new List<TicketIssueLevelDAO>();
            foreach (TicketIssueLevel TicketIssueLevel in TicketIssueLevels)
            {
                TicketIssueLevelDAO TicketIssueLevelDAO = new TicketIssueLevelDAO();
                TicketIssueLevelDAO.Id = TicketIssueLevel.Id;
                TicketIssueLevelDAO.Name = TicketIssueLevel.Name;
                TicketIssueLevelDAO.OrderNumber = TicketIssueLevel.OrderNumber;
                TicketIssueLevelDAO.TicketGroupId = TicketIssueLevel.TicketGroupId;
                TicketIssueLevelDAO.StatusId = TicketIssueLevel.StatusId;
                TicketIssueLevelDAO.SLA = TicketIssueLevel.SLA;
                TicketIssueLevelDAO.Used = TicketIssueLevel.Used;
                TicketIssueLevelDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketIssueLevelDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketIssueLevelDAOs.Add(TicketIssueLevelDAO);
            }
            await DataContext.BulkMergeAsync(TicketIssueLevelDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<TicketIssueLevel> TicketIssueLevels)
        {
            List<long> Ids = TicketIssueLevels.Select(x => x.Id).ToList();
            await DataContext.TicketIssueLevel
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketIssueLevelDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(TicketIssueLevel TicketIssueLevel)
        {
            await DataContext.SLAPolicy
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .DeleteFromQueryAsync();

            List<SLAPolicyDAO> SLAPolicyDAOs = new List<SLAPolicyDAO>();
            if (TicketIssueLevel.SLAPolicies != null)
            {
                foreach (SLAPolicy SLAPolicy in TicketIssueLevel.SLAPolicies)
                {
                    SLAPolicyDAO SLAPolicyDAO = new SLAPolicyDAO();
                    SLAPolicyDAO.Id = SLAPolicy.Id;
                    SLAPolicyDAO.TicketIssueLevelId = TicketIssueLevel.Id;
                    SLAPolicyDAO.TicketPriorityId = SLAPolicy.TicketPriorityId;
                    SLAPolicyDAO.FirstResponseTime = SLAPolicy.FirstResponseTime;
                    SLAPolicyDAO.FirstResponseUnitId = SLAPolicy.FirstResponseUnitId;
                    SLAPolicyDAO.ResolveTime = SLAPolicy.ResolveTime;
                    SLAPolicyDAO.ResolveUnitId = SLAPolicy.ResolveUnitId;
                    SLAPolicyDAO.IsAlert = SLAPolicy.IsAlert;
                    SLAPolicyDAO.IsAlertFRT = SLAPolicy.IsAlertFRT;
                    SLAPolicyDAO.IsEscalation = SLAPolicy.IsEscalation;
                    SLAPolicyDAO.IsEscalationFRT = SLAPolicy.IsEscalationFRT;
                    SLAPolicyDAO.CreatedAt = StaticParams.DateTimeNow;
                    SLAPolicyDAO.UpdatedAt = StaticParams.DateTimeNow;
                    SLAPolicyDAOs.Add(SLAPolicyDAO);
                }
                await DataContext.SLAPolicy.BulkMergeAsync(SLAPolicyDAOs);
            }

            //SLAAlert
            List<SLAAlertDAO> SLAAlertDAOs = new List<SLAAlertDAO>();
            if (TicketIssueLevel.SLAAlerts != null)
            {
                foreach (SLAAlert SLAAlert in TicketIssueLevel.SLAAlerts)
                {
                    SLAAlertDAO SLAAlertDAO = new SLAAlertDAO();
                    SLAAlertDAO.Id = SLAAlert.Id;
                    SLAAlertDAO.TicketIssueLevelId = TicketIssueLevel.Id;
                    SLAAlertDAO.IsNotification = SLAAlert.IsNotification;
                    SLAAlertDAO.IsMail = SLAAlert.IsMail;
                    SLAAlertDAO.IsSMS = SLAAlert.IsSMS;
                    SLAAlertDAO.Time = SLAAlert.Time;
                    SLAAlertDAO.TimeUnitId = SLAAlert.TimeUnitId;
                    SLAAlertDAO.IsAssignedToUser = SLAAlert.IsAssignedToUser;
                    SLAAlertDAO.IsAssignedToGroup = SLAAlert.IsAssignedToGroup;
                    SLAAlertDAO.SmsTemplateId = SLAAlert.SmsTemplateId;
                    SLAAlertDAO.MailTemplateId = SLAAlert.MailTemplateId;
                    SLAAlertDAO.CreatedAt = StaticParams.DateTimeNow;
                    SLAAlertDAO.UpdatedAt = StaticParams.DateTimeNow;
                    SLAAlertDAO.RowId = SLAAlert.RowId;
                    SLAAlertDAOs.Add(SLAAlertDAO);
                }

                var SLAAlertIds = SLAAlertDAOs.Select(x => x.Id).ToList();
                await DataContext.SLAAlertMail
                    .Where(x => SLAAlertIds.Contains(x.SLAAlertId.Value))
                    .DeleteFromQueryAsync();
                await DataContext.SLAAlertPhone
                    .Where(x => SLAAlertIds.Contains(x.SLAAlertId.Value))
                    .DeleteFromQueryAsync();
                await DataContext.SLAAlertUser
                    .Where(x => SLAAlertIds.Contains(x.SLAAlertId.Value))
                    .DeleteFromQueryAsync();

                await DataContext.SLAAlert
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .DeleteFromQueryAsync();

                await DataContext.SLAAlert.BulkMergeAsync(SLAAlertDAOs);

                List<SLAAlertMailDAO> SLAAlertMailDAOs = new List<SLAAlertMailDAO>();
                List<SLAAlertPhoneDAO> SLAAlertPhoneDAOs = new List<SLAAlertPhoneDAO>();
                List<SLAAlertUserDAO> SLAAlertUserDAOs = new List<SLAAlertUserDAO>();

                foreach (SLAAlert SLAAlert in TicketIssueLevel.SLAAlerts)
                {
                    SLAAlert.Id = SLAAlertDAOs.Where(x => x.RowId == SLAAlert.RowId).Select(x => x.Id).FirstOrDefault();
                    if (SLAAlert.SLAAlertMails != null && SLAAlert.SLAAlertMails.Any())
                    {
                        var listMail = SLAAlert.SLAAlertMails.Select(x => new SLAAlertMailDAO
                        {
                            Id = x.Id,
                            SLAAlertId = SLAAlert.Id,
                            Mail = x.Mail,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow

                        }).ToList();
                        SLAAlertMailDAOs.AddRange(listMail);
                    }
                    if (SLAAlert.SLAAlertPhones != null && SLAAlert.SLAAlertPhones.Any())
                    {
                        var listPhone = SLAAlert.SLAAlertPhones.Select(x => new SLAAlertPhoneDAO
                        {
                            Id = x.Id,
                            SLAAlertId = SLAAlert.Id,
                            Phone = x.Phone,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow
                        }).ToList();
                        SLAAlertPhoneDAOs.AddRange(listPhone);
                    }
                    if (SLAAlert.SLAAlertUsers != null && SLAAlert.SLAAlertUsers.Any())
                    {
                        var listUser = SLAAlert.SLAAlertUsers.Select(x => new SLAAlertUserDAO
                        {
                            Id = x.Id,
                            SLAAlertId = SLAAlert.Id,
                            AppUserId = x.AppUserId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow
                        }).ToList();
                        SLAAlertUserDAOs.AddRange(listUser);
                    }
                }
                await DataContext.SLAAlertMail.BulkMergeAsync(SLAAlertMailDAOs);
                await DataContext.SLAAlertPhone.BulkMergeAsync(SLAAlertPhoneDAOs);
                await DataContext.SLAAlertUser.BulkMergeAsync(SLAAlertUserDAOs);
            }
            //SLAAlertFRT
            List<SLAAlertFRTDAO> SLAAlertFRTDAOs = new List<SLAAlertFRTDAO>();
            if (TicketIssueLevel.SLAAlertFRTs != null)
            {


                foreach (SLAAlertFRT SLAAlertFRT in TicketIssueLevel.SLAAlertFRTs)
                {
                    SLAAlertFRTDAO SLAAlertFRTDAO = new SLAAlertFRTDAO();
                    SLAAlertFRTDAO.Id = SLAAlertFRT.Id;
                    SLAAlertFRTDAO.TicketIssueLevelId = TicketIssueLevel.Id;
                    SLAAlertFRTDAO.IsNotification = SLAAlertFRT.IsNotification;
                    SLAAlertFRTDAO.IsMail = SLAAlertFRT.IsMail;
                    SLAAlertFRTDAO.IsSMS = SLAAlertFRT.IsSMS;
                    SLAAlertFRTDAO.Time = SLAAlertFRT.Time;
                    SLAAlertFRTDAO.TimeUnitId = SLAAlertFRT.TimeUnitId;
                    SLAAlertFRTDAO.IsAssignedToUser = SLAAlertFRT.IsAssignedToUser;
                    SLAAlertFRTDAO.IsAssignedToGroup = SLAAlertFRT.IsAssignedToGroup;
                    SLAAlertFRTDAO.SmsTemplateId = SLAAlertFRT.SmsTemplateId;
                    SLAAlertFRTDAO.MailTemplateId = SLAAlertFRT.MailTemplateId;
                    SLAAlertFRTDAO.CreatedAt = StaticParams.DateTimeNow;
                    SLAAlertFRTDAO.UpdatedAt = StaticParams.DateTimeNow;
                    SLAAlertFRTDAO.RowId = SLAAlertFRT.RowId;
                    SLAAlertFRTDAOs.Add(SLAAlertFRTDAO);
                }

                var SLAAlertFRTIds = SLAAlertFRTDAOs.Select(x => x.Id).ToList();
                await DataContext.SLAAlertFRTMail
                    .Where(x => SLAAlertFRTIds.Contains(x.SLAAlertFRTId.Value))
                    .DeleteFromQueryAsync();
                await DataContext.SLAAlertFRTPhone
                    .Where(x => SLAAlertFRTIds.Contains(x.SLAAlertFRTId.Value))
                    .DeleteFromQueryAsync();
                await DataContext.SLAAlertFRTUser
                    .Where(x => SLAAlertFRTIds.Contains(x.SLAAlertFRTId.Value))
                    .DeleteFromQueryAsync();

                await DataContext.SLAAlertFRT
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .DeleteFromQueryAsync();

                await DataContext.SLAAlertFRT.BulkMergeAsync(SLAAlertFRTDAOs);

                List<SLAAlertFRTMailDAO> SLAAlertFRTMailDAOs = new List<SLAAlertFRTMailDAO>();
                List<SLAAlertFRTPhoneDAO> SLAAlertFRTPhoneDAOs = new List<SLAAlertFRTPhoneDAO>();
                List<SLAAlertFRTUserDAO> SLAAlertFRTUserDAOs = new List<SLAAlertFRTUserDAO>();

                foreach (SLAAlertFRT SLAAlertFRT in TicketIssueLevel.SLAAlertFRTs)
                {
                    SLAAlertFRT.Id = SLAAlertFRTDAOs.Where(x => x.RowId == SLAAlertFRT.RowId).Select(x => x.Id).FirstOrDefault();
                    if (SLAAlertFRT.SLAAlertFRTMails != null && SLAAlertFRT.SLAAlertFRTMails.Any())
                    {
                        var listMail = SLAAlertFRT.SLAAlertFRTMails.Select(x => new SLAAlertFRTMailDAO
                        {
                            Id = x.Id,
                            SLAAlertFRTId = SLAAlertFRT.Id,
                            Mail = x.Mail,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow

                        }).ToList();
                        SLAAlertFRTMailDAOs.AddRange(listMail);
                    }
                    if (SLAAlertFRT.SLAAlertFRTPhones != null && SLAAlertFRT.SLAAlertFRTPhones.Any())
                    {
                        var listPhone = SLAAlertFRT.SLAAlertFRTPhones.Select(x => new SLAAlertFRTPhoneDAO
                        {
                            Id = x.Id,
                            SLAAlertFRTId = SLAAlertFRT.Id,
                            Phone = x.Phone,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow
                        }).ToList();
                        SLAAlertFRTPhoneDAOs.AddRange(listPhone);
                    }
                    if (SLAAlertFRT.SLAAlertFRTUsers != null && SLAAlertFRT.SLAAlertFRTUsers.Any())
                    {
                        var listUser = SLAAlertFRT.SLAAlertFRTUsers.Select(x => new SLAAlertFRTUserDAO
                        {
                            Id = x.Id,
                            SLAAlertFRTId = SLAAlertFRT.Id,
                            AppUserId = x.AppUserId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow
                        }).ToList();
                        SLAAlertFRTUserDAOs.AddRange(listUser);
                    }
                }
                await DataContext.SLAAlertFRTMail.BulkMergeAsync(SLAAlertFRTMailDAOs);
                await DataContext.SLAAlertFRTPhone.BulkMergeAsync(SLAAlertFRTPhoneDAOs);
                await DataContext.SLAAlertFRTUser.BulkMergeAsync(SLAAlertFRTUserDAOs);
            }
            //SLAEscalation
            List<SLAEscalationDAO> SLAEscalationDAOs = new List<SLAEscalationDAO>();
            if (TicketIssueLevel.SLAEscalations != null)
            {


                foreach (SLAEscalation SLAEscalation in TicketIssueLevel.SLAEscalations)
                {
                    SLAEscalationDAO SLAEscalationDAO = new SLAEscalationDAO();
                    SLAEscalationDAO.Id = SLAEscalation.Id;
                    SLAEscalationDAO.TicketIssueLevelId = TicketIssueLevel.Id;
                    SLAEscalationDAO.IsNotification = SLAEscalation.IsNotification;
                    SLAEscalationDAO.IsMail = SLAEscalation.IsMail;
                    SLAEscalationDAO.IsSMS = SLAEscalation.IsSMS;
                    SLAEscalationDAO.Time = SLAEscalation.Time;
                    SLAEscalationDAO.TimeUnitId = SLAEscalation.TimeUnitId;
                    SLAEscalationDAO.IsAssignedToUser = SLAEscalation.IsAssignedToUser;
                    SLAEscalationDAO.IsAssignedToGroup = SLAEscalation.IsAssignedToGroup;
                    SLAEscalationDAO.SmsTemplateId = SLAEscalation.SmsTemplateId;
                    SLAEscalationDAO.MailTemplateId = SLAEscalation.MailTemplateId;
                    SLAEscalationDAO.CreatedAt = StaticParams.DateTimeNow;
                    SLAEscalationDAO.UpdatedAt = StaticParams.DateTimeNow;
                    SLAEscalationDAO.RowId = SLAEscalation.RowId;
                    SLAEscalationDAOs.Add(SLAEscalationDAO);
                }

                var SLAEscalationIds = SLAEscalationDAOs.Select(x => x.Id).ToList();
                await DataContext.SLAEscalationMail
                    .Where(x => SLAEscalationIds.Contains(x.SLAEscalationId.Value))
                    .DeleteFromQueryAsync();
                await DataContext.SLAEscalationPhone
                    .Where(x => SLAEscalationIds.Contains(x.SLAEscalationId.Value))
                    .DeleteFromQueryAsync();
                await DataContext.SLAEscalationUser
                    .Where(x => SLAEscalationIds.Contains(x.SLAEscalationId.Value))
                    .DeleteFromQueryAsync();

                await DataContext.SLAEscalation
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .DeleteFromQueryAsync();

                await DataContext.SLAEscalation.BulkMergeAsync(SLAEscalationDAOs);

                List<SLAEscalationMailDAO> SLAEscalationMailDAOs = new List<SLAEscalationMailDAO>();
                List<SLAEscalationPhoneDAO> SLAEscalationPhoneDAOs = new List<SLAEscalationPhoneDAO>();
                List<SLAEscalationUserDAO> SLAEscalationUserDAOs = new List<SLAEscalationUserDAO>();

                foreach (SLAEscalation SLAEscalation in TicketIssueLevel.SLAEscalations)
                {
                    SLAEscalation.Id = SLAEscalationDAOs.Where(x => x.RowId == SLAEscalation.RowId).Select(x => x.Id).FirstOrDefault();
                    if (SLAEscalation.SLAEscalationMails != null && SLAEscalation.SLAEscalationMails.Any())
                    {
                        var listMail = SLAEscalation.SLAEscalationMails.Select(x => new SLAEscalationMailDAO
                        {
                            Id = x.Id,
                            SLAEscalationId = SLAEscalation.Id,
                            Mail = x.Mail,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow

                        }).ToList();
                        SLAEscalationMailDAOs.AddRange(listMail);
                    }
                    if (SLAEscalation.SLAEscalationPhones != null && SLAEscalation.SLAEscalationPhones.Any())
                    {
                        var listPhone = SLAEscalation.SLAEscalationPhones.Select(x => new SLAEscalationPhoneDAO
                        {
                            Id = x.Id,
                            SLAEscalationId = SLAEscalation.Id,
                            Phone = x.Phone,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow
                        }).ToList();
                        SLAEscalationPhoneDAOs.AddRange(listPhone);
                    }
                    if (SLAEscalation.SLAEscalationUsers != null && SLAEscalation.SLAEscalationUsers.Any())
                    {
                        var listUser = SLAEscalation.SLAEscalationUsers.Select(x => new SLAEscalationUserDAO
                        {
                            Id = x.Id,
                            SLAEscalationId = SLAEscalation.Id,
                            AppUserId = x.AppUserId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow
                        }).ToList();
                        SLAEscalationUserDAOs.AddRange(listUser);
                    }
                }
                await DataContext.SLAEscalationMail.BulkMergeAsync(SLAEscalationMailDAOs);
                await DataContext.SLAEscalationPhone.BulkMergeAsync(SLAEscalationPhoneDAOs);
                await DataContext.SLAEscalationUser.BulkMergeAsync(SLAEscalationUserDAOs);
            }
            //SLAEscalationFRT
            List<SLAEscalationFRTDAO> SLAEscalationFRTDAOs = new List<SLAEscalationFRTDAO>();
            if (TicketIssueLevel.SLAEscalationFRTs != null)
            {


                foreach (SLAEscalationFRT SLAEscalationFRT in TicketIssueLevel.SLAEscalationFRTs)
                {
                    SLAEscalationFRTDAO SLAEscalationFRTDAO = new SLAEscalationFRTDAO();
                    SLAEscalationFRTDAO.Id = SLAEscalationFRT.Id;
                    SLAEscalationFRTDAO.TicketIssueLevelId = TicketIssueLevel.Id;
                    SLAEscalationFRTDAO.IsNotification = SLAEscalationFRT.IsNotification;
                    SLAEscalationFRTDAO.IsMail = SLAEscalationFRT.IsMail;
                    SLAEscalationFRTDAO.IsSMS = SLAEscalationFRT.IsSMS;
                    SLAEscalationFRTDAO.Time = SLAEscalationFRT.Time;
                    SLAEscalationFRTDAO.TimeUnitId = SLAEscalationFRT.TimeUnitId;
                    SLAEscalationFRTDAO.IsAssignedToUser = SLAEscalationFRT.IsAssignedToUser;
                    SLAEscalationFRTDAO.IsAssignedToGroup = SLAEscalationFRT.IsAssignedToGroup;
                    SLAEscalationFRTDAO.SmsTemplateId = SLAEscalationFRT.SmsTemplateId;
                    SLAEscalationFRTDAO.MailTemplateId = SLAEscalationFRT.MailTemplateId;
                    SLAEscalationFRTDAO.CreatedAt = StaticParams.DateTimeNow;
                    SLAEscalationFRTDAO.UpdatedAt = StaticParams.DateTimeNow;
                    SLAEscalationFRTDAO.RowId = SLAEscalationFRT.RowId;
                    SLAEscalationFRTDAOs.Add(SLAEscalationFRTDAO);
                }

                var SLAEscalationFRTIds = SLAEscalationFRTDAOs.Select(x => x.Id).ToList();
                await DataContext.SLAEscalationFRTMail
                    .Where(x => SLAEscalationFRTIds.Contains(x.SLAEscalationFRTId.Value))
                    .DeleteFromQueryAsync();
                await DataContext.SLAEscalationFRTPhone
                    .Where(x => SLAEscalationFRTIds.Contains(x.SLAEscalationFRTId.Value))
                    .DeleteFromQueryAsync();
                await DataContext.SLAEscalationFRTUser
                    .Where(x => SLAEscalationFRTIds.Contains(x.SLAEscalationFRTId.Value))
                    .DeleteFromQueryAsync();

                await DataContext.SLAEscalationFRT
                .Where(x => x.TicketIssueLevelId == TicketIssueLevel.Id)
                .DeleteFromQueryAsync();

                await DataContext.SLAEscalationFRT.BulkMergeAsync(SLAEscalationFRTDAOs);

                List<SLAEscalationFRTMailDAO> SLAEscalationFRTMailDAOs = new List<SLAEscalationFRTMailDAO>();
                List<SLAEscalationFRTPhoneDAO> SLAEscalationFRTPhoneDAOs = new List<SLAEscalationFRTPhoneDAO>();
                List<SLAEscalationFRTUserDAO> SLAEscalationFRTUserDAOs = new List<SLAEscalationFRTUserDAO>();

                foreach (SLAEscalationFRT SLAEscalationFRT in TicketIssueLevel.SLAEscalationFRTs)
                {
                    SLAEscalationFRT.Id = SLAEscalationFRTDAOs.Where(x => x.RowId == SLAEscalationFRT.RowId).Select(x => x.Id).FirstOrDefault();
                    if (SLAEscalationFRT.SLAEscalationFRTMails != null && SLAEscalationFRT.SLAEscalationFRTMails.Any())
                    {
                        var listMail = SLAEscalationFRT.SLAEscalationFRTMails.Select(x => new SLAEscalationFRTMailDAO
                        {
                            Id = x.Id,
                            SLAEscalationFRTId = SLAEscalationFRT.Id,
                            Mail = x.Mail,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow

                        }).ToList();
                        SLAEscalationFRTMailDAOs.AddRange(listMail);
                    }
                    if (SLAEscalationFRT.SLAEscalationFRTPhones != null && SLAEscalationFRT.SLAEscalationFRTPhones.Any())
                    {
                        var listPhone = SLAEscalationFRT.SLAEscalationFRTPhones.Select(x => new SLAEscalationFRTPhoneDAO
                        {
                            Id = x.Id,
                            SLAEscalationFRTId = SLAEscalationFRT.Id,
                            Phone = x.Phone,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow
                        }).ToList();
                        SLAEscalationFRTPhoneDAOs.AddRange(listPhone);
                    }
                    if (SLAEscalationFRT.SLAEscalationFRTUsers != null && SLAEscalationFRT.SLAEscalationFRTUsers.Any())
                    {
                        var listUser = SLAEscalationFRT.SLAEscalationFRTUsers.Select(x => new SLAEscalationFRTUserDAO
                        {
                            Id = x.Id,
                            SLAEscalationFRTId = SLAEscalationFRT.Id,
                            AppUserId = x.AppUserId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow
                        }).ToList();
                        SLAEscalationFRTUserDAOs.AddRange(listUser);
                    }
                }
                await DataContext.SLAEscalationFRTMail.BulkMergeAsync(SLAEscalationFRTMailDAOs);
                await DataContext.SLAEscalationFRTPhone.BulkMergeAsync(SLAEscalationFRTPhoneDAOs);
                await DataContext.SLAEscalationFRTUser.BulkMergeAsync(SLAEscalationFRTUserDAOs);
            }
        }

        public async Task ReminderTicket()
        {
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
                        DisplayName = x.User.DisplayName
                    },
                    Creator = x.Creator == null ? null : new AppUser
                    {
                        Id = x.Creator.Id,
                        Username = x.Creator.Username,
                        DisplayName = x.Creator.DisplayName
                    },
                })
                .ToListAsync();
            
            List<SLAPolicyDAO> SLAPolicyDAOs = await DataContext.SLAPolicy.AsNoTracking().ToListAsync();
            try
            {
                var a = await DataContext.SLAAlert.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
            }
            List<SLAAlert> SLAAlerts = await DataContext.SLAAlert.AsNoTracking().Select(x => new SLAAlert
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
            List<SLAAlertMail> SLAAlertMails = new List<SLAAlertMail>();
            var SLAAlertIds = SLAAlerts.Select(x => x.Id).ToList();
            
            foreach (var SLAAlert in SLAAlerts)
            {
                SLAAlert.SLAAlertMails = SLAAlertMails.Where(x => x.SLAAlertId == SLAAlert.Id).ToList();
            }

            List<SLAAlertFRTDAO> SLAAlertFRTDAOs = await DataContext.SLAAlertFRT.ToListAsync();
            List<SLAEscalationDAO> SLAEscalationDAOs = await DataContext.SLAEscalation.ToListAsync();
            List<SLAEscalationFRTDAO> SLAEscalationFRTDAOs = await DataContext.SLAEscalationFRT.ToListAsync();

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
                                    UserNotification NotificationUtils = new UserNotification
                                    {
                                        TitleWeb = $"Thông báo từ CRM",
                                        Time = Now,
                                        Unread = true,
                                        ContentWeb = $"Ticket [{Ticket.TicketNumber} - {Ticket.Name} - {Ticket.TicketIssueLevel.Name}] sẽ hết hạn xử lý vào lúc {ResolveTimeAt}. Vui lòng kiểm tra ticket và hoàn thành!",
                                        //LinkWebsite = $"{TicketRoute.Detail}/preview/*".Replace("*", Ticket.Id.ToString()),
                                        SenderId = Ticket.UserId,
                                        RecipientId = Ticket.UserId,
                                        RowId = Guid.NewGuid(),
                                    };
                                    UserNotifications.Add(NotificationUtils);
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
                                            .Replace("{{TICKET_NAME}}", Ticket.Name).Replace("{{RESOLVE_TIME_AT}}", ResolveTimeAt.ToString());

                                        Mail MailSend = new Mail
                                        {
                                            Recipients = emails,
                                            Subject = Subject,
                                            Body = Content,
                                            RowId = Guid.NewGuid(),
                                        };
                                        Mails.Add(MailSend);
                                        List<EventMessage<Mail>> messages = Mails.Select(m => new EventMessage<Mail>(m, m.RowId)).ToList();
                                        //RabbitManager.PublishList(messages, RoutingKeyEnum.MailSend);
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

                            SLAAlertFRTDAO SLAAlertFRTDAO = SLAAlertFRTDAOs.Find(o => o.TicketIssueLevelId == Ticket.TicketIssueLevelId);
                            if (SLAAlertFRTDAO == null) continue;
                            long Time = SLAAlertFRTDAO.Time.Value;
                            if (ResolveTimeAt.AddMinutes(-Time) <= Now && ResolveTimeAt > Now)
                            {
                                if (SLAAlertFRTDAO.IsNotification.HasValue && SLAAlertFRTDAO.IsNotification.Value)
                                {
                                    UserNotification NotificationUtils = new UserNotification
                                    {
                                        TitleWeb = $"Thông báo từ CRM",
                                        Time = Now,
                                        Unread = true,
                                        ContentWeb = $"Ticket [{Ticket.TicketNumber} - {Ticket.Name} - {Ticket.TicketIssueLevel.Name}] sẽ hết hạn phản hồi vào lúc {FirstResponeAt}. Vui lòng kiểm tra ticket và hoàn thành!",
                                        LinkWebsite = "",
                                        SenderId = Ticket.UserId,
                                        RecipientId = Ticket.UserId,
                                        RowId = Guid.NewGuid(),
                                    };
                                    UserNotifications.Add(NotificationUtils);
                                }
                                if (SLAAlertFRTDAO.IsMail.HasValue && SLAAlertFRTDAO.IsMail.Value)
                                {
                                    //Do something
                                }
                                if (SLAAlertFRTDAO.IsSMS.HasValue && SLAAlertFRTDAO.IsSMS.Value)
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

                            SLAEscalationDAO SLAEscalationDAO = SLAEscalationDAOs.Find(o => o.TicketIssueLevelId == Ticket.TicketIssueLevelId);
                            if (SLAEscalationDAO == null) continue;
                            long Time = SLAEscalationDAO.Time.Value;
                            if (ResolveTimeAt.AddMinutes(Time) <= Now)
                            {
                                if (SLAEscalationDAO.IsNotification.HasValue && SLAEscalationDAO.IsNotification.Value)
                                {
                                    UserNotification NotificationUtils = new UserNotification
                                    {
                                        TitleWeb = $"Thông báo từ CRM",
                                        Time = Now,
                                        Unread = true,
                                        ContentWeb = $"Ticket [{Ticket.TicketNumber} - {Ticket.Name} - {Ticket.TicketIssueLevel.Name}] đã hết hạn xử lý lúc {ResolveTimeAt} . Vui lòng kiểm tra ticket và hoàn thành!",
                                        //LinkWebsite = $"{TicketRoute.Detail}/preview/*".Replace("*", Ticket.Id.ToString()),
                                        SenderId = Ticket.UserId,
                                        RecipientId = Ticket.UserId,
                                        RowId = Guid.NewGuid(),
                                    };
                                    UserNotifications.Add(NotificationUtils);
                                }
                                if (SLAEscalationDAO.IsMail.HasValue && SLAEscalationDAO.IsMail.Value)
                                {
                                    //Do something
                                }
                                if (SLAEscalationDAO.IsSMS.HasValue && SLAEscalationDAO.IsSMS.Value)
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
                    // Cảnh báo xử lý
                    if (Ticket.IsEscalatedFRT.HasValue && !Ticket.IsEscalatedFRT.Value)
                    {
                        if (SLAPolicyDAO.IsEscalationFRT.HasValue && SLAPolicyDAO.IsEscalationFRT.Value)
                        {

                            SLAEscalationFRTDAO SLAEscalationFRTDAO = SLAEscalationFRTDAOs.Find(o => o.TicketIssueLevelId == Ticket.TicketIssueLevelId);
                            if (SLAEscalationFRTDAO == null) continue;
                            long Time = SLAEscalationFRTDAO.Time.Value;
                            if (FirstResponeAt.AddMinutes(Time) <= Now)
                            {
                                if (SLAEscalationFRTDAO.IsNotification.HasValue && SLAEscalationFRTDAO.IsNotification.Value)
                                {
                                    UserNotification NotificationUtils = new UserNotification
                                    {
                                        TitleWeb = $"Thông báo từ CRM",
                                        Time = Now,
                                        Unread = true,
                                        ContentWeb = $"Ticket [{Ticket.TicketNumber} - {Ticket.Name} - {Ticket.TicketIssueLevel.Name}] đã hết hạn phản hồi lúc {FirstResponeAt} . Vui lòng kiểm tra ticket và hoàn thành!",
                                        //LinkWebsite = $"{TicketRoute.Detail}/preview/*".Replace("*", Ticket.Id.ToString()),
                                        SenderId = Ticket.UserId,
                                        RecipientId = Ticket.UserId,
                                        RowId = Guid.NewGuid(),
                                    };
                                    UserNotifications.Add(NotificationUtils);
                                }
                                if (SLAEscalationFRTDAO.IsMail.HasValue && SLAEscalationFRTDAO.IsMail.Value)
                                {
                                    //Do something
                                }
                                if (SLAEscalationFRTDAO.IsSMS.HasValue && SLAEscalationFRTDAO.IsSMS.Value)
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
            //RabbitManager.PublishList(EventUserNotifications, RoutingKeyEnum.UserNotificationSend);
        }

    }
}
