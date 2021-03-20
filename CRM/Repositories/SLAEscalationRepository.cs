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
    public interface ISLAEscalationRepository
    {
        Task<int> Count(SLAEscalationFilter SLAEscalationFilter);
        Task<List<SLAEscalation>> List(SLAEscalationFilter SLAEscalationFilter);
        Task<SLAEscalation> Get(long Id);
        Task<bool> Create(SLAEscalation SLAEscalation);
        Task<bool> Update(SLAEscalation SLAEscalation);
        Task<bool> Delete(SLAEscalation SLAEscalation);
        Task<bool> BulkMerge(List<SLAEscalation> SLAEscalations);
        Task<bool> BulkDelete(List<SLAEscalation> SLAEscalations);
    }
    public class SLAEscalationRepository : ISLAEscalationRepository
    {
        private DataContext DataContext;
        public SLAEscalationRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAEscalationDAO> DynamicFilter(IQueryable<SLAEscalationDAO> query, SLAEscalationFilter filter)
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

         private IQueryable<SLAEscalationDAO> OrFilter(IQueryable<SLAEscalationDAO> query, SLAEscalationFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAEscalationDAO> initQuery = query.Where(q => false);
            foreach (SLAEscalationFilter SLAEscalationFilter in filter.OrFilter)
            {
                IQueryable<SLAEscalationDAO> queryable = query;
                if (SLAEscalationFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAEscalationFilter.Id);
                if (SLAEscalationFilter.TicketIssueLevelId != null)
                    queryable = queryable.Where(q => q.TicketIssueLevelId.HasValue).Where(q => q.TicketIssueLevelId, SLAEscalationFilter.TicketIssueLevelId);
                if (SLAEscalationFilter.Time != null)
                    queryable = queryable.Where(q => q.Time.HasValue).Where(q => q.Time, SLAEscalationFilter.Time);
                if (SLAEscalationFilter.TimeUnitId != null)
                    queryable = queryable.Where(q => q.TimeUnitId.HasValue).Where(q => q.TimeUnitId, SLAEscalationFilter.TimeUnitId);
                if (SLAEscalationFilter.SmsTemplateId != null)
                    queryable = queryable.Where(q => q.SmsTemplateId.HasValue).Where(q => q.SmsTemplateId, SLAEscalationFilter.SmsTemplateId);
                if (SLAEscalationFilter.MailTemplateId != null)
                    queryable = queryable.Where(q => q.MailTemplateId.HasValue).Where(q => q.MailTemplateId, SLAEscalationFilter.MailTemplateId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAEscalationDAO> DynamicOrder(IQueryable<SLAEscalationDAO> query, SLAEscalationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAEscalationOrder.TicketIssueLevel:
                            query = query.OrderBy(q => q.TicketIssueLevelId);
                            break;
                        case SLAEscalationOrder.IsNotification:
                            query = query.OrderBy(q => q.IsNotification);
                            break;
                        case SLAEscalationOrder.IsMail:
                            query = query.OrderBy(q => q.IsMail);
                            break;
                        case SLAEscalationOrder.IsSMS:
                            query = query.OrderBy(q => q.IsSMS);
                            break;
                        case SLAEscalationOrder.Time:
                            query = query.OrderBy(q => q.Time);
                            break;
                        case SLAEscalationOrder.TimeUnit:
                            query = query.OrderBy(q => q.TimeUnitId);
                            break;
                        case SLAEscalationOrder.IsAssignedToUser:
                            query = query.OrderBy(q => q.IsAssignedToUser);
                            break;
                        case SLAEscalationOrder.IsAssignedToGroup:
                            query = query.OrderBy(q => q.IsAssignedToGroup);
                            break;
                        case SLAEscalationOrder.SmsTemplate:
                            query = query.OrderBy(q => q.SmsTemplateId);
                            break;
                        case SLAEscalationOrder.MailTemplate:
                            query = query.OrderBy(q => q.MailTemplateId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAEscalationOrder.TicketIssueLevel:
                            query = query.OrderByDescending(q => q.TicketIssueLevelId);
                            break;
                        case SLAEscalationOrder.IsNotification:
                            query = query.OrderByDescending(q => q.IsNotification);
                            break;
                        case SLAEscalationOrder.IsMail:
                            query = query.OrderByDescending(q => q.IsMail);
                            break;
                        case SLAEscalationOrder.IsSMS:
                            query = query.OrderByDescending(q => q.IsSMS);
                            break;
                        case SLAEscalationOrder.Time:
                            query = query.OrderByDescending(q => q.Time);
                            break;
                        case SLAEscalationOrder.TimeUnit:
                            query = query.OrderByDescending(q => q.TimeUnitId);
                            break;
                        case SLAEscalationOrder.IsAssignedToUser:
                            query = query.OrderByDescending(q => q.IsAssignedToUser);
                            break;
                        case SLAEscalationOrder.IsAssignedToGroup:
                            query = query.OrderByDescending(q => q.IsAssignedToGroup);
                            break;
                        case SLAEscalationOrder.SmsTemplate:
                            query = query.OrderByDescending(q => q.SmsTemplateId);
                            break;
                        case SLAEscalationOrder.MailTemplate:
                            query = query.OrderByDescending(q => q.MailTemplateId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAEscalation>> DynamicSelect(IQueryable<SLAEscalationDAO> query, SLAEscalationFilter filter)
        {
            List<SLAEscalation> SLAEscalations = await query.Select(q => new SLAEscalation()
            {
                Id = filter.Selects.Contains(SLAEscalationSelect.Id) ? q.Id : default(long),
                TicketIssueLevelId = filter.Selects.Contains(SLAEscalationSelect.TicketIssueLevel) ? q.TicketIssueLevelId : default(long?),
                IsNotification = filter.Selects.Contains(SLAEscalationSelect.IsNotification) ? q.IsNotification : default(bool?),
                IsMail = filter.Selects.Contains(SLAEscalationSelect.IsMail) ? q.IsMail : default(bool?),
                IsSMS = filter.Selects.Contains(SLAEscalationSelect.IsSMS) ? q.IsSMS : default(bool?),
                Time = filter.Selects.Contains(SLAEscalationSelect.Time) ? q.Time : default(long?),
                TimeUnitId = filter.Selects.Contains(SLAEscalationSelect.TimeUnit) ? q.TimeUnitId : default(long?),
                IsAssignedToUser = filter.Selects.Contains(SLAEscalationSelect.IsAssignedToUser) ? q.IsAssignedToUser : default(bool?),
                IsAssignedToGroup = filter.Selects.Contains(SLAEscalationSelect.IsAssignedToGroup) ? q.IsAssignedToGroup : default(bool?),
                SmsTemplateId = filter.Selects.Contains(SLAEscalationSelect.SmsTemplate) ? q.SmsTemplateId : default(long?),
                MailTemplateId = filter.Selects.Contains(SLAEscalationSelect.MailTemplate) ? q.MailTemplateId : default(long?),
                MailTemplate = filter.Selects.Contains(SLAEscalationSelect.MailTemplate) && q.MailTemplate != null ? new MailTemplate
                {
                    Id = q.MailTemplate.Id,
                    Code = q.MailTemplate.Code,
                    Name = q.MailTemplate.Name,
                    Content = q.MailTemplate.Content,
                    StatusId = q.MailTemplate.StatusId,
                } : null,
                SmsTemplate = filter.Selects.Contains(SLAEscalationSelect.SmsTemplate) && q.SmsTemplate != null ? new SmsTemplate
                {
                    Id = q.SmsTemplate.Id,
                    Code = q.SmsTemplate.Code,
                    Name = q.SmsTemplate.Name,
                    Content = q.SmsTemplate.Content,
                    StatusId = q.SmsTemplate.StatusId,
                } : null,
                TicketIssueLevel = filter.Selects.Contains(SLAEscalationSelect.TicketIssueLevel) && q.TicketIssueLevel != null ? new TicketIssueLevel
                {
                    Id = q.TicketIssueLevel.Id,
                    Name = q.TicketIssueLevel.Name,
                    OrderNumber = q.TicketIssueLevel.OrderNumber,
                    TicketGroupId = q.TicketIssueLevel.TicketGroupId,
                    StatusId = q.TicketIssueLevel.StatusId,
                    SLA = q.TicketIssueLevel.SLA,
                    Used = q.TicketIssueLevel.Used,
                } : null,
                TimeUnit = filter.Selects.Contains(SLAEscalationSelect.TimeUnit) && q.TimeUnit != null ? new SLATimeUnit
                {
                    Id = q.TimeUnit.Id,
                    Code = q.TimeUnit.Code,
                    Name = q.TimeUnit.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAEscalations;
        }

        public async Task<int> Count(SLAEscalationFilter filter)
        {
            IQueryable<SLAEscalationDAO> SLAEscalations = DataContext.SLAEscalation.AsNoTracking();
            SLAEscalations = DynamicFilter(SLAEscalations, filter);
            return await SLAEscalations.CountAsync();
        }

        public async Task<List<SLAEscalation>> List(SLAEscalationFilter filter)
        {
            if (filter == null) return new List<SLAEscalation>();
            IQueryable<SLAEscalationDAO> SLAEscalationDAOs = DataContext.SLAEscalation.AsNoTracking();
            SLAEscalationDAOs = DynamicFilter(SLAEscalationDAOs, filter);
            SLAEscalationDAOs = DynamicOrder(SLAEscalationDAOs, filter);
            List<SLAEscalation> SLAEscalations = await DynamicSelect(SLAEscalationDAOs, filter);
            return SLAEscalations;
        }

        public async Task<SLAEscalation> Get(long Id)
        {
            SLAEscalation SLAEscalation = await DataContext.SLAEscalation.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAEscalation()
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

            if (SLAEscalation == null)
                return null;

            return SLAEscalation;
        }
        public async Task<bool> Create(SLAEscalation SLAEscalation)
        {
            SLAEscalationDAO SLAEscalationDAO = new SLAEscalationDAO();
            SLAEscalationDAO.Id = SLAEscalation.Id;
            SLAEscalationDAO.TicketIssueLevelId = SLAEscalation.TicketIssueLevelId;
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
            DataContext.SLAEscalation.Add(SLAEscalationDAO);
            await DataContext.SaveChangesAsync();
            SLAEscalation.Id = SLAEscalationDAO.Id;
            await SaveReference(SLAEscalation);
            return true;
        }

        public async Task<bool> Update(SLAEscalation SLAEscalation)
        {
            SLAEscalationDAO SLAEscalationDAO = DataContext.SLAEscalation.Where(x => x.Id == SLAEscalation.Id).FirstOrDefault();
            if (SLAEscalationDAO == null)
                return false;
            SLAEscalationDAO.Id = SLAEscalation.Id;
            SLAEscalationDAO.TicketIssueLevelId = SLAEscalation.TicketIssueLevelId;
            SLAEscalationDAO.IsNotification = SLAEscalation.IsNotification;
            SLAEscalationDAO.IsMail = SLAEscalation.IsMail;
            SLAEscalationDAO.IsSMS = SLAEscalation.IsSMS;
            SLAEscalationDAO.Time = SLAEscalation.Time;
            SLAEscalationDAO.TimeUnitId = SLAEscalation.TimeUnitId;
            SLAEscalationDAO.IsAssignedToUser = SLAEscalation.IsAssignedToUser;
            SLAEscalationDAO.IsAssignedToGroup = SLAEscalation.IsAssignedToGroup;
            SLAEscalationDAO.SmsTemplateId = SLAEscalation.SmsTemplateId;
            SLAEscalationDAO.MailTemplateId = SLAEscalation.MailTemplateId;
            SLAEscalationDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAEscalation);
            return true;
        }

        public async Task<bool> Delete(SLAEscalation SLAEscalation)
        {
            await DataContext.SLAEscalation.Where(x => x.Id == SLAEscalation.Id).UpdateFromQueryAsync(x => new SLAEscalationDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAEscalation> SLAEscalations)
        {
            List<SLAEscalationDAO> SLAEscalationDAOs = new List<SLAEscalationDAO>();
            foreach (SLAEscalation SLAEscalation in SLAEscalations)
            {
                SLAEscalationDAO SLAEscalationDAO = new SLAEscalationDAO();
                SLAEscalationDAO.Id = SLAEscalation.Id;
                SLAEscalationDAO.TicketIssueLevelId = SLAEscalation.TicketIssueLevelId;
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
                SLAEscalationDAOs.Add(SLAEscalationDAO);
            }
            await DataContext.BulkMergeAsync(SLAEscalationDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAEscalation> SLAEscalations)
        {
            List<long> Ids = SLAEscalations.Select(x => x.Id).ToList();
            await DataContext.SLAEscalation
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAEscalationDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAEscalation SLAEscalation)
        {
        }
        
    }
}
