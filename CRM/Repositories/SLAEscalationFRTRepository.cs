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
    public interface ISLAEscalationFRTRepository
    {
        Task<int> Count(SLAEscalationFRTFilter SLAEscalationFRTFilter);
        Task<List<SLAEscalationFRT>> List(SLAEscalationFRTFilter SLAEscalationFRTFilter);
        Task<SLAEscalationFRT> Get(long Id);
        Task<bool> Create(SLAEscalationFRT SLAEscalationFRT);
        Task<bool> Update(SLAEscalationFRT SLAEscalationFRT);
        Task<bool> Delete(SLAEscalationFRT SLAEscalationFRT);
        Task<bool> BulkMerge(List<SLAEscalationFRT> SLAEscalationFRTs);
        Task<bool> BulkDelete(List<SLAEscalationFRT> SLAEscalationFRTs);
    }
    public class SLAEscalationFRTRepository : ISLAEscalationFRTRepository
    {
        private DataContext DataContext;
        public SLAEscalationFRTRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAEscalationFRTDAO> DynamicFilter(IQueryable<SLAEscalationFRTDAO> query, SLAEscalationFRTFilter filter)
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
            if (filter.TicketIssueLevelId != null)
                query = query.Where(q => q.TicketIssueLevelId.HasValue).Where(q => q.TicketIssueLevelId, filter.TicketIssueLevelId);
            if (filter.Time != null)
                query = query.Where(q => q.Time.HasValue).Where(q => q.Time, filter.Time);
            if (filter.TimeUnitId != null)
                query = query.Where(q => q.TimeUnitId.HasValue).Where(q => q.TimeUnitId, filter.TimeUnitId);
            if (filter.SmsTemplateId != null)
                query = query.Where(q => q.SmsTemplateId.HasValue).Where(q => q.SmsTemplateId, filter.SmsTemplateId);
            if (filter.MailTemplateId != null)
                query = query.Where(q => q.MailTemplateId.HasValue).Where(q => q.MailTemplateId, filter.MailTemplateId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAEscalationFRTDAO> OrFilter(IQueryable<SLAEscalationFRTDAO> query, SLAEscalationFRTFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAEscalationFRTDAO> initQuery = query.Where(q => false);
            foreach (SLAEscalationFRTFilter SLAEscalationFRTFilter in filter.OrFilter)
            {
                IQueryable<SLAEscalationFRTDAO> queryable = query;
                if (SLAEscalationFRTFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAEscalationFRTFilter.Id);
                if (SLAEscalationFRTFilter.TicketIssueLevelId != null)
                    queryable = queryable.Where(q => q.TicketIssueLevelId.HasValue).Where(q => q.TicketIssueLevelId, SLAEscalationFRTFilter.TicketIssueLevelId);
                if (SLAEscalationFRTFilter.Time != null)
                    queryable = queryable.Where(q => q.Time.HasValue).Where(q => q.Time, SLAEscalationFRTFilter.Time);
                if (SLAEscalationFRTFilter.TimeUnitId != null)
                    queryable = queryable.Where(q => q.TimeUnitId.HasValue).Where(q => q.TimeUnitId, SLAEscalationFRTFilter.TimeUnitId);
                if (SLAEscalationFRTFilter.SmsTemplateId != null)
                    queryable = queryable.Where(q => q.SmsTemplateId.HasValue).Where(q => q.SmsTemplateId, SLAEscalationFRTFilter.SmsTemplateId);
                if (SLAEscalationFRTFilter.MailTemplateId != null)
                    queryable = queryable.Where(q => q.MailTemplateId.HasValue).Where(q => q.MailTemplateId, SLAEscalationFRTFilter.MailTemplateId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAEscalationFRTDAO> DynamicOrder(IQueryable<SLAEscalationFRTDAO> query, SLAEscalationFRTFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationFRTOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAEscalationFRTOrder.TicketIssueLevel:
                            query = query.OrderBy(q => q.TicketIssueLevelId);
                            break;
                        case SLAEscalationFRTOrder.IsNotification:
                            query = query.OrderBy(q => q.IsNotification);
                            break;
                        case SLAEscalationFRTOrder.IsMail:
                            query = query.OrderBy(q => q.IsMail);
                            break;
                        case SLAEscalationFRTOrder.IsSMS:
                            query = query.OrderBy(q => q.IsSMS);
                            break;
                        case SLAEscalationFRTOrder.Time:
                            query = query.OrderBy(q => q.Time);
                            break;
                        case SLAEscalationFRTOrder.TimeUnit:
                            query = query.OrderBy(q => q.TimeUnitId);
                            break;
                        case SLAEscalationFRTOrder.IsAssignedToUser:
                            query = query.OrderBy(q => q.IsAssignedToUser);
                            break;
                        case SLAEscalationFRTOrder.IsAssignedToGroup:
                            query = query.OrderBy(q => q.IsAssignedToGroup);
                            break;
                        case SLAEscalationFRTOrder.SmsTemplate:
                            query = query.OrderBy(q => q.SmsTemplateId);
                            break;
                        case SLAEscalationFRTOrder.MailTemplate:
                            query = query.OrderBy(q => q.MailTemplateId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationFRTOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAEscalationFRTOrder.TicketIssueLevel:
                            query = query.OrderByDescending(q => q.TicketIssueLevelId);
                            break;
                        case SLAEscalationFRTOrder.IsNotification:
                            query = query.OrderByDescending(q => q.IsNotification);
                            break;
                        case SLAEscalationFRTOrder.IsMail:
                            query = query.OrderByDescending(q => q.IsMail);
                            break;
                        case SLAEscalationFRTOrder.IsSMS:
                            query = query.OrderByDescending(q => q.IsSMS);
                            break;
                        case SLAEscalationFRTOrder.Time:
                            query = query.OrderByDescending(q => q.Time);
                            break;
                        case SLAEscalationFRTOrder.TimeUnit:
                            query = query.OrderByDescending(q => q.TimeUnitId);
                            break;
                        case SLAEscalationFRTOrder.IsAssignedToUser:
                            query = query.OrderByDescending(q => q.IsAssignedToUser);
                            break;
                        case SLAEscalationFRTOrder.IsAssignedToGroup:
                            query = query.OrderByDescending(q => q.IsAssignedToGroup);
                            break;
                        case SLAEscalationFRTOrder.SmsTemplate:
                            query = query.OrderByDescending(q => q.SmsTemplateId);
                            break;
                        case SLAEscalationFRTOrder.MailTemplate:
                            query = query.OrderByDescending(q => q.MailTemplateId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAEscalationFRT>> DynamicSelect(IQueryable<SLAEscalationFRTDAO> query, SLAEscalationFRTFilter filter)
        {
            List<SLAEscalationFRT> SLAEscalationFRTs = await query.Select(q => new SLAEscalationFRT()
            {
                Id = filter.Selects.Contains(SLAEscalationFRTSelect.Id) ? q.Id : default(long),
                TicketIssueLevelId = filter.Selects.Contains(SLAEscalationFRTSelect.TicketIssueLevel) ? q.TicketIssueLevelId : default(long?),
                IsNotification = filter.Selects.Contains(SLAEscalationFRTSelect.IsNotification) ? q.IsNotification : default(bool?),
                IsMail = filter.Selects.Contains(SLAEscalationFRTSelect.IsMail) ? q.IsMail : default(bool?),
                IsSMS = filter.Selects.Contains(SLAEscalationFRTSelect.IsSMS) ? q.IsSMS : default(bool?),
                Time = filter.Selects.Contains(SLAEscalationFRTSelect.Time) ? q.Time : default(long?),
                TimeUnitId = filter.Selects.Contains(SLAEscalationFRTSelect.TimeUnit) ? q.TimeUnitId : default(long?),
                IsAssignedToUser = filter.Selects.Contains(SLAEscalationFRTSelect.IsAssignedToUser) ? q.IsAssignedToUser : default(bool?),
                IsAssignedToGroup = filter.Selects.Contains(SLAEscalationFRTSelect.IsAssignedToGroup) ? q.IsAssignedToGroup : default(bool?),
                SmsTemplateId = filter.Selects.Contains(SLAEscalationFRTSelect.SmsTemplate) ? q.SmsTemplateId : default(long?),
                MailTemplateId = filter.Selects.Contains(SLAEscalationFRTSelect.MailTemplate) ? q.MailTemplateId : default(long?),
                MailTemplate = filter.Selects.Contains(SLAEscalationFRTSelect.MailTemplate) && q.MailTemplate != null ? new MailTemplate
                {
                    Id = q.MailTemplate.Id,
                    Code = q.MailTemplate.Code,
                    Name = q.MailTemplate.Name,
                    Content = q.MailTemplate.Content,
                    StatusId = q.MailTemplate.StatusId,
                } : null,
                SmsTemplate = filter.Selects.Contains(SLAEscalationFRTSelect.SmsTemplate) && q.SmsTemplate != null ? new SmsTemplate
                {
                    Id = q.SmsTemplate.Id,
                    Code = q.SmsTemplate.Code,
                    Name = q.SmsTemplate.Name,
                    Content = q.SmsTemplate.Content,
                    StatusId = q.SmsTemplate.StatusId,
                } : null,
                TicketIssueLevel = filter.Selects.Contains(SLAEscalationFRTSelect.TicketIssueLevel) && q.TicketIssueLevel != null ? new TicketIssueLevel
                {
                    Id = q.TicketIssueLevel.Id,
                    Name = q.TicketIssueLevel.Name,
                    OrderNumber = q.TicketIssueLevel.OrderNumber,
                    TicketGroupId = q.TicketIssueLevel.TicketGroupId,
                    StatusId = q.TicketIssueLevel.StatusId,
                    SLA = q.TicketIssueLevel.SLA,
                    Used = q.TicketIssueLevel.Used,
                } : null,
                TimeUnit = filter.Selects.Contains(SLAEscalationFRTSelect.TimeUnit) && q.TimeUnit != null ? new SLATimeUnit
                {
                    Id = q.TimeUnit.Id,
                    Code = q.TimeUnit.Code,
                    Name = q.TimeUnit.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAEscalationFRTs;
        }

        public async Task<int> Count(SLAEscalationFRTFilter filter)
        {
            IQueryable<SLAEscalationFRTDAO> SLAEscalationFRTs = DataContext.SLAEscalationFRT.AsNoTracking();
            SLAEscalationFRTs = DynamicFilter(SLAEscalationFRTs, filter);
            return await SLAEscalationFRTs.CountAsync();
        }

        public async Task<List<SLAEscalationFRT>> List(SLAEscalationFRTFilter filter)
        {
            if (filter == null) return new List<SLAEscalationFRT>();
            IQueryable<SLAEscalationFRTDAO> SLAEscalationFRTDAOs = DataContext.SLAEscalationFRT.AsNoTracking();
            SLAEscalationFRTDAOs = DynamicFilter(SLAEscalationFRTDAOs, filter);
            SLAEscalationFRTDAOs = DynamicOrder(SLAEscalationFRTDAOs, filter);
            List<SLAEscalationFRT> SLAEscalationFRTs = await DynamicSelect(SLAEscalationFRTDAOs, filter);
            return SLAEscalationFRTs;
        }

        public async Task<SLAEscalationFRT> Get(long Id)
        {
            SLAEscalationFRT SLAEscalationFRT = await DataContext.SLAEscalationFRT.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAEscalationFRT()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
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
                    Id = x.MailTemplate.Id,
                    Code = x.MailTemplate.Code,
                    Name = x.MailTemplate.Name,
                    Content = x.MailTemplate.Content,
                    StatusId = x.MailTemplate.StatusId,
                },
                SmsTemplate = x.SmsTemplate == null ? null : new SmsTemplate
                {
                    Id = x.SmsTemplate.Id,
                    Code = x.SmsTemplate.Code,
                    Name = x.SmsTemplate.Name,
                    Content = x.SmsTemplate.Content,
                    StatusId = x.SmsTemplate.StatusId,
                },
                TicketIssueLevel = x.TicketIssueLevel == null ? null : new TicketIssueLevel
                {
                    Id = x.TicketIssueLevel.Id,
                    Name = x.TicketIssueLevel.Name,
                    OrderNumber = x.TicketIssueLevel.OrderNumber,
                    TicketGroupId = x.TicketIssueLevel.TicketGroupId,
                    StatusId = x.TicketIssueLevel.StatusId,
                    SLA = x.TicketIssueLevel.SLA,
                    Used = x.TicketIssueLevel.Used,
                },
                TimeUnit = x.TimeUnit == null ? null : new SLATimeUnit
                {
                    Id = x.TimeUnit.Id,
                    Code = x.TimeUnit.Code,
                    Name = x.TimeUnit.Name,
                },
            }).FirstOrDefaultAsync();

            if (SLAEscalationFRT == null)
                return null;

            return SLAEscalationFRT;
        }
        public async Task<bool> Create(SLAEscalationFRT SLAEscalationFRT)
        {
            SLAEscalationFRTDAO SLAEscalationFRTDAO = new SLAEscalationFRTDAO();
            SLAEscalationFRTDAO.Id = SLAEscalationFRT.Id;
            SLAEscalationFRTDAO.TicketIssueLevelId = SLAEscalationFRT.TicketIssueLevelId;
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
            DataContext.SLAEscalationFRT.Add(SLAEscalationFRTDAO);
            await DataContext.SaveChangesAsync();
            SLAEscalationFRT.Id = SLAEscalationFRTDAO.Id;
            await SaveReference(SLAEscalationFRT);
            return true;
        }

        public async Task<bool> Update(SLAEscalationFRT SLAEscalationFRT)
        {
            SLAEscalationFRTDAO SLAEscalationFRTDAO = DataContext.SLAEscalationFRT.Where(x => x.Id == SLAEscalationFRT.Id).FirstOrDefault();
            if (SLAEscalationFRTDAO == null)
                return false;
            SLAEscalationFRTDAO.Id = SLAEscalationFRT.Id;
            SLAEscalationFRTDAO.TicketIssueLevelId = SLAEscalationFRT.TicketIssueLevelId;
            SLAEscalationFRTDAO.IsNotification = SLAEscalationFRT.IsNotification;
            SLAEscalationFRTDAO.IsMail = SLAEscalationFRT.IsMail;
            SLAEscalationFRTDAO.IsSMS = SLAEscalationFRT.IsSMS;
            SLAEscalationFRTDAO.Time = SLAEscalationFRT.Time;
            SLAEscalationFRTDAO.TimeUnitId = SLAEscalationFRT.TimeUnitId;
            SLAEscalationFRTDAO.IsAssignedToUser = SLAEscalationFRT.IsAssignedToUser;
            SLAEscalationFRTDAO.IsAssignedToGroup = SLAEscalationFRT.IsAssignedToGroup;
            SLAEscalationFRTDAO.SmsTemplateId = SLAEscalationFRT.SmsTemplateId;
            SLAEscalationFRTDAO.MailTemplateId = SLAEscalationFRT.MailTemplateId;
            SLAEscalationFRTDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAEscalationFRT);
            return true;
        }

        public async Task<bool> Delete(SLAEscalationFRT SLAEscalationFRT)
        {
            await DataContext.SLAEscalationFRT.Where(x => x.Id == SLAEscalationFRT.Id).UpdateFromQueryAsync(x => new SLAEscalationFRTDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAEscalationFRT> SLAEscalationFRTs)
        {
            List<SLAEscalationFRTDAO> SLAEscalationFRTDAOs = new List<SLAEscalationFRTDAO>();
            foreach (SLAEscalationFRT SLAEscalationFRT in SLAEscalationFRTs)
            {
                SLAEscalationFRTDAO SLAEscalationFRTDAO = new SLAEscalationFRTDAO();
                SLAEscalationFRTDAO.Id = SLAEscalationFRT.Id;
                SLAEscalationFRTDAO.TicketIssueLevelId = SLAEscalationFRT.TicketIssueLevelId;
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
                SLAEscalationFRTDAOs.Add(SLAEscalationFRTDAO);
            }
            await DataContext.BulkMergeAsync(SLAEscalationFRTDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAEscalationFRT> SLAEscalationFRTs)
        {
            List<long> Ids = SLAEscalationFRTs.Select(x => x.Id).ToList();
            await DataContext.SLAEscalationFRT
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAEscalationFRTDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAEscalationFRT SLAEscalationFRT)
        {
        }
        
    }
}
