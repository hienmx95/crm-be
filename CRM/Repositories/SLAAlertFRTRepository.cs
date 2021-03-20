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
    public interface ISLAAlertFRTRepository
    {
        Task<int> Count(SLAAlertFRTFilter SLAAlertFRTFilter);
        Task<List<SLAAlertFRT>> List(SLAAlertFRTFilter SLAAlertFRTFilter);
        Task<SLAAlertFRT> Get(long Id);
        Task<bool> Create(SLAAlertFRT SLAAlertFRT);
        Task<bool> Update(SLAAlertFRT SLAAlertFRT);
        Task<bool> Delete(SLAAlertFRT SLAAlertFRT);
        Task<bool> BulkMerge(List<SLAAlertFRT> SLAAlertFRTs);
        Task<bool> BulkDelete(List<SLAAlertFRT> SLAAlertFRTs);
    }
    public class SLAAlertFRTRepository : ISLAAlertFRTRepository
    {
        private DataContext DataContext;
        public SLAAlertFRTRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAAlertFRTDAO> DynamicFilter(IQueryable<SLAAlertFRTDAO> query, SLAAlertFRTFilter filter)
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

         private IQueryable<SLAAlertFRTDAO> OrFilter(IQueryable<SLAAlertFRTDAO> query, SLAAlertFRTFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAAlertFRTDAO> initQuery = query.Where(q => false);
            foreach (SLAAlertFRTFilter SLAAlertFRTFilter in filter.OrFilter)
            {
                IQueryable<SLAAlertFRTDAO> queryable = query;
                if (SLAAlertFRTFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAAlertFRTFilter.Id);
                if (SLAAlertFRTFilter.TicketIssueLevelId != null)
                    queryable = queryable.Where(q => q.TicketIssueLevelId.HasValue).Where(q => q.TicketIssueLevelId, SLAAlertFRTFilter.TicketIssueLevelId);
                if (SLAAlertFRTFilter.Time != null)
                    queryable = queryable.Where(q => q.Time.HasValue).Where(q => q.Time, SLAAlertFRTFilter.Time);
                if (SLAAlertFRTFilter.TimeUnitId != null)
                    queryable = queryable.Where(q => q.TimeUnitId.HasValue).Where(q => q.TimeUnitId, SLAAlertFRTFilter.TimeUnitId);
                if (SLAAlertFRTFilter.SmsTemplateId != null)
                    queryable = queryable.Where(q => q.SmsTemplateId.HasValue).Where(q => q.SmsTemplateId, SLAAlertFRTFilter.SmsTemplateId);
                if (SLAAlertFRTFilter.MailTemplateId != null)
                    queryable = queryable.Where(q => q.MailTemplateId.HasValue).Where(q => q.MailTemplateId, SLAAlertFRTFilter.MailTemplateId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAAlertFRTDAO> DynamicOrder(IQueryable<SLAAlertFRTDAO> query, SLAAlertFRTFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertFRTOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAAlertFRTOrder.TicketIssueLevel:
                            query = query.OrderBy(q => q.TicketIssueLevelId);
                            break;
                        case SLAAlertFRTOrder.IsNotification:
                            query = query.OrderBy(q => q.IsNotification);
                            break;
                        case SLAAlertFRTOrder.IsMail:
                            query = query.OrderBy(q => q.IsMail);
                            break;
                        case SLAAlertFRTOrder.IsSMS:
                            query = query.OrderBy(q => q.IsSMS);
                            break;
                        case SLAAlertFRTOrder.Time:
                            query = query.OrderBy(q => q.Time);
                            break;
                        case SLAAlertFRTOrder.TimeUnit:
                            query = query.OrderBy(q => q.TimeUnitId);
                            break;
                        case SLAAlertFRTOrder.IsAssignedToUser:
                            query = query.OrderBy(q => q.IsAssignedToUser);
                            break;
                        case SLAAlertFRTOrder.IsAssignedToGroup:
                            query = query.OrderBy(q => q.IsAssignedToGroup);
                            break;
                        case SLAAlertFRTOrder.SmsTemplate:
                            query = query.OrderBy(q => q.SmsTemplateId);
                            break;
                        case SLAAlertFRTOrder.MailTemplate:
                            query = query.OrderBy(q => q.MailTemplateId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertFRTOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAAlertFRTOrder.TicketIssueLevel:
                            query = query.OrderByDescending(q => q.TicketIssueLevelId);
                            break;
                        case SLAAlertFRTOrder.IsNotification:
                            query = query.OrderByDescending(q => q.IsNotification);
                            break;
                        case SLAAlertFRTOrder.IsMail:
                            query = query.OrderByDescending(q => q.IsMail);
                            break;
                        case SLAAlertFRTOrder.IsSMS:
                            query = query.OrderByDescending(q => q.IsSMS);
                            break;
                        case SLAAlertFRTOrder.Time:
                            query = query.OrderByDescending(q => q.Time);
                            break;
                        case SLAAlertFRTOrder.TimeUnit:
                            query = query.OrderByDescending(q => q.TimeUnitId);
                            break;
                        case SLAAlertFRTOrder.IsAssignedToUser:
                            query = query.OrderByDescending(q => q.IsAssignedToUser);
                            break;
                        case SLAAlertFRTOrder.IsAssignedToGroup:
                            query = query.OrderByDescending(q => q.IsAssignedToGroup);
                            break;
                        case SLAAlertFRTOrder.SmsTemplate:
                            query = query.OrderByDescending(q => q.SmsTemplateId);
                            break;
                        case SLAAlertFRTOrder.MailTemplate:
                            query = query.OrderByDescending(q => q.MailTemplateId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAAlertFRT>> DynamicSelect(IQueryable<SLAAlertFRTDAO> query, SLAAlertFRTFilter filter)
        {
            List<SLAAlertFRT> SLAAlertFRTs = await query.Select(q => new SLAAlertFRT()
            {
                Id = filter.Selects.Contains(SLAAlertFRTSelect.Id) ? q.Id : default(long),
                TicketIssueLevelId = filter.Selects.Contains(SLAAlertFRTSelect.TicketIssueLevel) ? q.TicketIssueLevelId : default(long?),
                IsNotification = filter.Selects.Contains(SLAAlertFRTSelect.IsNotification) ? q.IsNotification : default(bool?),
                IsMail = filter.Selects.Contains(SLAAlertFRTSelect.IsMail) ? q.IsMail : default(bool?),
                IsSMS = filter.Selects.Contains(SLAAlertFRTSelect.IsSMS) ? q.IsSMS : default(bool?),
                Time = filter.Selects.Contains(SLAAlertFRTSelect.Time) ? q.Time : default(long?),
                TimeUnitId = filter.Selects.Contains(SLAAlertFRTSelect.TimeUnit) ? q.TimeUnitId : default(long?),
                IsAssignedToUser = filter.Selects.Contains(SLAAlertFRTSelect.IsAssignedToUser) ? q.IsAssignedToUser : default(bool?),
                IsAssignedToGroup = filter.Selects.Contains(SLAAlertFRTSelect.IsAssignedToGroup) ? q.IsAssignedToGroup : default(bool?),
                SmsTemplateId = filter.Selects.Contains(SLAAlertFRTSelect.SmsTemplate) ? q.SmsTemplateId : default(long?),
                MailTemplateId = filter.Selects.Contains(SLAAlertFRTSelect.MailTemplate) ? q.MailTemplateId : default(long?),
                MailTemplate = filter.Selects.Contains(SLAAlertFRTSelect.MailTemplate) && q.MailTemplate != null ? new MailTemplate
                {
                    Id = q.MailTemplate.Id,
                    Code = q.MailTemplate.Code,
                    Name = q.MailTemplate.Name,
                    Content = q.MailTemplate.Content,
                    StatusId = q.MailTemplate.StatusId,
                } : null,
                SmsTemplate = filter.Selects.Contains(SLAAlertFRTSelect.SmsTemplate) && q.SmsTemplate != null ? new SmsTemplate
                {
                    Id = q.SmsTemplate.Id,
                    Code = q.SmsTemplate.Code,
                    Name = q.SmsTemplate.Name,
                    Content = q.SmsTemplate.Content,
                    StatusId = q.SmsTemplate.StatusId,
                } : null,
                TicketIssueLevel = filter.Selects.Contains(SLAAlertFRTSelect.TicketIssueLevel) && q.TicketIssueLevel != null ? new TicketIssueLevel
                {
                    Id = q.TicketIssueLevel.Id,
                    Name = q.TicketIssueLevel.Name,
                    OrderNumber = q.TicketIssueLevel.OrderNumber,
                    TicketGroupId = q.TicketIssueLevel.TicketGroupId,
                    StatusId = q.TicketIssueLevel.StatusId,
                    SLA = q.TicketIssueLevel.SLA,
                    Used = q.TicketIssueLevel.Used,
                } : null,
                TimeUnit = filter.Selects.Contains(SLAAlertFRTSelect.TimeUnit) && q.TimeUnit != null ? new SLATimeUnit
                {
                    Id = q.TimeUnit.Id,
                    Code = q.TimeUnit.Code,
                    Name = q.TimeUnit.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAAlertFRTs;
        }

        public async Task<int> Count(SLAAlertFRTFilter filter)
        {
            IQueryable<SLAAlertFRTDAO> SLAAlertFRTs = DataContext.SLAAlertFRT.AsNoTracking();
            SLAAlertFRTs = DynamicFilter(SLAAlertFRTs, filter);
            return await SLAAlertFRTs.CountAsync();
        }

        public async Task<List<SLAAlertFRT>> List(SLAAlertFRTFilter filter)
        {
            if (filter == null) return new List<SLAAlertFRT>();
            IQueryable<SLAAlertFRTDAO> SLAAlertFRTDAOs = DataContext.SLAAlertFRT.AsNoTracking();
            SLAAlertFRTDAOs = DynamicFilter(SLAAlertFRTDAOs, filter);
            SLAAlertFRTDAOs = DynamicOrder(SLAAlertFRTDAOs, filter);
            List<SLAAlertFRT> SLAAlertFRTs = await DynamicSelect(SLAAlertFRTDAOs, filter);
            return SLAAlertFRTs;
        }

        public async Task<SLAAlertFRT> Get(long Id)
        {
            SLAAlertFRT SLAAlertFRT = await DataContext.SLAAlertFRT.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAAlertFRT()
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

            if (SLAAlertFRT == null)
                return null;

            return SLAAlertFRT;
        }
        public async Task<bool> Create(SLAAlertFRT SLAAlertFRT)
        {
            SLAAlertFRTDAO SLAAlertFRTDAO = new SLAAlertFRTDAO();
            SLAAlertFRTDAO.Id = SLAAlertFRT.Id;
            SLAAlertFRTDAO.TicketIssueLevelId = SLAAlertFRT.TicketIssueLevelId;
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
            DataContext.SLAAlertFRT.Add(SLAAlertFRTDAO);
            await DataContext.SaveChangesAsync();
            SLAAlertFRT.Id = SLAAlertFRTDAO.Id;
            await SaveReference(SLAAlertFRT);
            return true;
        }

        public async Task<bool> Update(SLAAlertFRT SLAAlertFRT)
        {
            SLAAlertFRTDAO SLAAlertFRTDAO = DataContext.SLAAlertFRT.Where(x => x.Id == SLAAlertFRT.Id).FirstOrDefault();
            if (SLAAlertFRTDAO == null)
                return false;
            SLAAlertFRTDAO.Id = SLAAlertFRT.Id;
            SLAAlertFRTDAO.TicketIssueLevelId = SLAAlertFRT.TicketIssueLevelId;
            SLAAlertFRTDAO.IsNotification = SLAAlertFRT.IsNotification;
            SLAAlertFRTDAO.IsMail = SLAAlertFRT.IsMail;
            SLAAlertFRTDAO.IsSMS = SLAAlertFRT.IsSMS;
            SLAAlertFRTDAO.Time = SLAAlertFRT.Time;
            SLAAlertFRTDAO.TimeUnitId = SLAAlertFRT.TimeUnitId;
            SLAAlertFRTDAO.IsAssignedToUser = SLAAlertFRT.IsAssignedToUser;
            SLAAlertFRTDAO.IsAssignedToGroup = SLAAlertFRT.IsAssignedToGroup;
            SLAAlertFRTDAO.SmsTemplateId = SLAAlertFRT.SmsTemplateId;
            SLAAlertFRTDAO.MailTemplateId = SLAAlertFRT.MailTemplateId;
            SLAAlertFRTDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAAlertFRT);
            return true;
        }

        public async Task<bool> Delete(SLAAlertFRT SLAAlertFRT)
        {
            await DataContext.SLAAlertFRT.Where(x => x.Id == SLAAlertFRT.Id).UpdateFromQueryAsync(x => new SLAAlertFRTDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAAlertFRT> SLAAlertFRTs)
        {
            List<SLAAlertFRTDAO> SLAAlertFRTDAOs = new List<SLAAlertFRTDAO>();
            foreach (SLAAlertFRT SLAAlertFRT in SLAAlertFRTs)
            {
                SLAAlertFRTDAO SLAAlertFRTDAO = new SLAAlertFRTDAO();
                SLAAlertFRTDAO.Id = SLAAlertFRT.Id;
                SLAAlertFRTDAO.TicketIssueLevelId = SLAAlertFRT.TicketIssueLevelId;
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
                SLAAlertFRTDAOs.Add(SLAAlertFRTDAO);
            }
            await DataContext.BulkMergeAsync(SLAAlertFRTDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAAlertFRT> SLAAlertFRTs)
        {
            List<long> Ids = SLAAlertFRTs.Select(x => x.Id).ToList();
            await DataContext.SLAAlertFRT
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAAlertFRTDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAAlertFRT SLAAlertFRT)
        {
        }
        
    }
}
