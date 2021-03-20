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
    public interface ISLAPolicyRepository
    {
        Task<int> Count(SLAPolicyFilter SLAPolicyFilter);
        Task<List<SLAPolicy>> List(SLAPolicyFilter SLAPolicyFilter);
        Task<SLAPolicy> Get(long Id);
        Task<SLAPolicy> GetByTicket(Ticket Ticket);
        Task<bool> Create(SLAPolicy SLAPolicy);
        Task<bool> Update(SLAPolicy SLAPolicy);
        Task<bool> Delete(SLAPolicy SLAPolicy);
        Task<bool> BulkMerge(List<SLAPolicy> SLAPolicies);
        Task<bool> BulkDelete(List<SLAPolicy> SLAPolicies);
    }
    public class SLAPolicyRepository : ISLAPolicyRepository
    {
        private DataContext DataContext;
        public SLAPolicyRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAPolicyDAO> DynamicFilter(IQueryable<SLAPolicyDAO> query, SLAPolicyFilter filter)
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
            if (filter.TicketPriorityId != null)
                query = query.Where(q => q.TicketPriorityId.HasValue).Where(q => q.TicketPriorityId, filter.TicketPriorityId);
            if (filter.FirstResponseTime != null)
                query = query.Where(q => q.FirstResponseTime.HasValue).Where(q => q.FirstResponseTime, filter.FirstResponseTime);
            if (filter.ResolveTime != null)
                query = query.Where(q => q.ResolveTime.HasValue).Where(q => q.ResolveTime, filter.ResolveTime);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAPolicyDAO> OrFilter(IQueryable<SLAPolicyDAO> query, SLAPolicyFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAPolicyDAO> initQuery = query.Where(q => false);
            foreach (SLAPolicyFilter SLAPolicyFilter in filter.OrFilter)
            {
                IQueryable<SLAPolicyDAO> queryable = query;
                if (SLAPolicyFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAPolicyFilter.Id);
                if (SLAPolicyFilter.TicketIssueLevelId != null)
                    queryable = queryable.Where(q => q.TicketIssueLevelId.HasValue).Where(q => q.TicketIssueLevelId, SLAPolicyFilter.TicketIssueLevelId);
                if (SLAPolicyFilter.TicketPriorityId != null)
                    queryable = queryable.Where(q => q.TicketPriorityId.HasValue).Where(q => q.TicketPriorityId, SLAPolicyFilter.TicketPriorityId);
                if (SLAPolicyFilter.FirstResponseTime != null)
                    queryable = queryable.Where(q => q.FirstResponseTime.HasValue).Where(q => q.FirstResponseTime, SLAPolicyFilter.FirstResponseTime);
                if (SLAPolicyFilter.ResolveTime != null)
                    queryable = queryable.Where(q => q.ResolveTime.HasValue).Where(q => q.ResolveTime, SLAPolicyFilter.ResolveTime);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAPolicyDAO> DynamicOrder(IQueryable<SLAPolicyDAO> query, SLAPolicyFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAPolicyOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAPolicyOrder.TicketIssueLevel:
                            query = query.OrderBy(q => q.TicketIssueLevelId);
                            break;
                        case SLAPolicyOrder.TicketPriority:
                            query = query.OrderBy(q => q.TicketPriorityId);
                            break;
                        case SLAPolicyOrder.FirstResponseTime:
                            query = query.OrderBy(q => q.FirstResponseTime);
                            break;
                        case SLAPolicyOrder.ResolveTime:
                            query = query.OrderBy(q => q.ResolveTime);
                            break;
                        case SLAPolicyOrder.IsAlert:
                            query = query.OrderBy(q => q.IsAlert);
                            break;
                        case SLAPolicyOrder.IsAlertFRT:
                            query = query.OrderBy(q => q.IsAlertFRT);
                            break;
                        case SLAPolicyOrder.IsEscalation:
                            query = query.OrderBy(q => q.IsEscalation);
                            break;
                        case SLAPolicyOrder.IsEscalationFRT:
                            query = query.OrderBy(q => q.IsEscalationFRT);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAPolicyOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAPolicyOrder.TicketIssueLevel:
                            query = query.OrderByDescending(q => q.TicketIssueLevelId);
                            break;
                        case SLAPolicyOrder.TicketPriority:
                            query = query.OrderByDescending(q => q.TicketPriorityId);
                            break;
                        case SLAPolicyOrder.FirstResponseTime:
                            query = query.OrderByDescending(q => q.FirstResponseTime);
                            break;
                        case SLAPolicyOrder.ResolveTime:
                            query = query.OrderByDescending(q => q.ResolveTime);
                            break;
                        case SLAPolicyOrder.IsAlert:
                            query = query.OrderByDescending(q => q.IsAlert);
                            break;
                        case SLAPolicyOrder.IsAlertFRT:
                            query = query.OrderByDescending(q => q.IsAlertFRT);
                            break;
                        case SLAPolicyOrder.IsEscalation:
                            query = query.OrderByDescending(q => q.IsEscalation);
                            break;
                        case SLAPolicyOrder.IsEscalationFRT:
                            query = query.OrderByDescending(q => q.IsEscalationFRT);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAPolicy>> DynamicSelect(IQueryable<SLAPolicyDAO> query, SLAPolicyFilter filter)
        {
            List<SLAPolicy> SLAPolicies = await query.Select(q => new SLAPolicy()
            {
                Id = filter.Selects.Contains(SLAPolicySelect.Id) ? q.Id : default(long),
                TicketIssueLevelId = filter.Selects.Contains(SLAPolicySelect.TicketIssueLevel) ? q.TicketIssueLevelId : default(long?),
                TicketPriorityId = filter.Selects.Contains(SLAPolicySelect.TicketPriority) ? q.TicketPriorityId : default(long?),
                FirstResponseTime = filter.Selects.Contains(SLAPolicySelect.FirstResponseTime) ? q.FirstResponseTime : default(long?),
                ResolveTime = filter.Selects.Contains(SLAPolicySelect.ResolveTime) ? q.ResolveTime : default(long?),
                IsAlert = filter.Selects.Contains(SLAPolicySelect.IsAlert) ? q.IsAlert : default(bool?),
                IsAlertFRT = filter.Selects.Contains(SLAPolicySelect.IsAlertFRT) ? q.IsAlertFRT : default(bool?),
                IsEscalation = filter.Selects.Contains(SLAPolicySelect.IsEscalation) ? q.IsEscalation : default(bool?),
                IsEscalationFRT = filter.Selects.Contains(SLAPolicySelect.IsEscalationFRT) ? q.IsEscalationFRT : default(bool?),
                TicketIssueLevel = filter.Selects.Contains(SLAPolicySelect.TicketIssueLevel) && q.TicketIssueLevel != null ? new TicketIssueLevel
                {
                    Id = q.TicketIssueLevel.Id,
                    Name = q.TicketIssueLevel.Name,
                    OrderNumber = q.TicketIssueLevel.OrderNumber,
                    TicketGroupId = q.TicketIssueLevel.TicketGroupId,
                    StatusId = q.TicketIssueLevel.StatusId,
                    SLA = q.TicketIssueLevel.SLA,
                    Used = q.TicketIssueLevel.Used,
                } : null,
                TicketPriority = filter.Selects.Contains(SLAPolicySelect.TicketPriority) && q.TicketPriority != null ? new TicketPriority
                {
                    Id = q.TicketPriority.Id,
                    Name = q.TicketPriority.Name,
                    OrderNumber = q.TicketPriority.OrderNumber,
                    ColorCode = q.TicketPriority.ColorCode,
                    StatusId = q.TicketPriority.StatusId,
                    Used = q.TicketPriority.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAPolicies;
        }

        public async Task<int> Count(SLAPolicyFilter filter)
        {
            IQueryable<SLAPolicyDAO> SLAPolicies = DataContext.SLAPolicy.AsNoTracking();
            SLAPolicies = DynamicFilter(SLAPolicies, filter);
            return await SLAPolicies.CountAsync();
        }

        public async Task<List<SLAPolicy>> List(SLAPolicyFilter filter)
        {
            if (filter == null) return new List<SLAPolicy>();
            IQueryable<SLAPolicyDAO> SLAPolicyDAOs = DataContext.SLAPolicy.AsNoTracking();
            SLAPolicyDAOs = DynamicFilter(SLAPolicyDAOs, filter);
            SLAPolicyDAOs = DynamicOrder(SLAPolicyDAOs, filter);
            List<SLAPolicy> SLAPolicies = await DynamicSelect(SLAPolicyDAOs, filter);
            return SLAPolicies;
        }

        public async Task<SLAPolicy> Get(long Id)
        {
            SLAPolicy SLAPolicy = await DataContext.SLAPolicy.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAPolicy()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                TicketIssueLevelId = x.TicketIssueLevelId,
                TicketPriorityId = x.TicketPriorityId,
                FirstResponseTime = x.FirstResponseTime,
                ResolveTime = x.ResolveTime,
                IsAlert = x.IsAlert,
                IsAlertFRT = x.IsAlertFRT,
                IsEscalation = x.IsEscalation,
                IsEscalationFRT = x.IsEscalationFRT,
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
                TicketPriority = x.TicketPriority == null ? null : new TicketPriority
                {
                    Id = x.TicketPriority.Id,
                    Name = x.TicketPriority.Name,
                    OrderNumber = x.TicketPriority.OrderNumber,
                    ColorCode = x.TicketPriority.ColorCode,
                    StatusId = x.TicketPriority.StatusId,
                    Used = x.TicketPriority.Used,
                },
            }).FirstOrDefaultAsync();

            if (SLAPolicy == null)
                return null;

            return SLAPolicy;
        }

        public async Task<SLAPolicy> GetByTicket(Ticket Ticket)
        {
            SLAPolicy SLAPolicy = await DataContext.SLAPolicy.AsNoTracking()
            .Where(p => p.TicketPriorityId == Ticket.TicketPriorityId && p.TicketIssueLevelId == Ticket.TicketIssueLevelId)
            .Select(x => new SLAPolicy()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                TicketIssueLevelId = x.TicketIssueLevelId,
                TicketPriorityId = x.TicketPriorityId,
                FirstResponseTime = x.FirstResponseTime,
                ResolveTime = x.ResolveTime,
                IsAlert = x.IsAlert,
                IsAlertFRT = x.IsAlertFRT,
                IsEscalation = x.IsEscalation,
                IsEscalationFRT = x.IsEscalationFRT,
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
                TicketPriority = x.TicketPriority == null ? null : new TicketPriority
                {
                    Id = x.TicketPriority.Id,
                    Name = x.TicketPriority.Name,
                    OrderNumber = x.TicketPriority.OrderNumber,
                    ColorCode = x.TicketPriority.ColorCode,
                    StatusId = x.TicketPriority.StatusId,
                    Used = x.TicketPriority.Used,
                },
                FirstResponseUnitId = x.FirstResponseUnitId,
                FirstResponseUnit = x.FirstResponseUnit == null ? null : new SLATimeUnit
                {
                    Id = x.ResolveUnit.Id,
                    Code = x.ResolveUnit.Code,
                    Name = x.ResolveUnit.Name
                },
                ResolveUnitId = x.ResolveUnitId,
                ResolveUnit = x.ResolveUnit == null ? null : new SLATimeUnit
                {
                    Id = x.ResolveUnit.Id,
                    Code = x.ResolveUnit.Code,
                    Name = x.ResolveUnit.Name
                },
            }).FirstOrDefaultAsync();

            if (SLAPolicy == null)
                return null;

            return SLAPolicy;
        }
        public async Task<bool> Create(SLAPolicy SLAPolicy)
        {
            SLAPolicyDAO SLAPolicyDAO = new SLAPolicyDAO();
            SLAPolicyDAO.Id = SLAPolicy.Id;
            SLAPolicyDAO.TicketIssueLevelId = SLAPolicy.TicketIssueLevelId;
            SLAPolicyDAO.TicketPriorityId = SLAPolicy.TicketPriorityId;
            SLAPolicyDAO.FirstResponseTime = SLAPolicy.FirstResponseTime;
            SLAPolicyDAO.ResolveTime = SLAPolicy.ResolveTime;
            SLAPolicyDAO.IsAlert = SLAPolicy.IsAlert;
            SLAPolicyDAO.IsAlertFRT = SLAPolicy.IsAlertFRT;
            SLAPolicyDAO.IsEscalation = SLAPolicy.IsEscalation;
            SLAPolicyDAO.IsEscalationFRT = SLAPolicy.IsEscalationFRT;
            SLAPolicyDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAPolicyDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAPolicy.Add(SLAPolicyDAO);
            await DataContext.SaveChangesAsync();
            SLAPolicy.Id = SLAPolicyDAO.Id;
            await SaveReference(SLAPolicy);
            return true;
        }

        public async Task<bool> Update(SLAPolicy SLAPolicy)
        {
            SLAPolicyDAO SLAPolicyDAO = DataContext.SLAPolicy.Where(x => x.Id == SLAPolicy.Id).FirstOrDefault();
            if (SLAPolicyDAO == null)
                return false;
            SLAPolicyDAO.Id = SLAPolicy.Id;
            SLAPolicyDAO.TicketIssueLevelId = SLAPolicy.TicketIssueLevelId;
            SLAPolicyDAO.TicketPriorityId = SLAPolicy.TicketPriorityId;
            SLAPolicyDAO.FirstResponseTime = SLAPolicy.FirstResponseTime;
            SLAPolicyDAO.ResolveTime = SLAPolicy.ResolveTime;
            SLAPolicyDAO.IsAlert = SLAPolicy.IsAlert;
            SLAPolicyDAO.IsAlertFRT = SLAPolicy.IsAlertFRT;
            SLAPolicyDAO.IsEscalation = SLAPolicy.IsEscalation;
            SLAPolicyDAO.IsEscalationFRT = SLAPolicy.IsEscalationFRT;
            SLAPolicyDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAPolicy);
            return true;
        }

        public async Task<bool> Delete(SLAPolicy SLAPolicy)
        {
            await DataContext.SLAPolicy.Where(x => x.Id == SLAPolicy.Id).UpdateFromQueryAsync(x => new SLAPolicyDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAPolicy> SLAPolicies)
        {
            List<SLAPolicyDAO> SLAPolicyDAOs = new List<SLAPolicyDAO>();
            foreach (SLAPolicy SLAPolicy in SLAPolicies)
            {
                SLAPolicyDAO SLAPolicyDAO = new SLAPolicyDAO();
                SLAPolicyDAO.Id = SLAPolicy.Id;
                SLAPolicyDAO.TicketIssueLevelId = SLAPolicy.TicketIssueLevelId;
                SLAPolicyDAO.TicketPriorityId = SLAPolicy.TicketPriorityId;
                SLAPolicyDAO.FirstResponseTime = SLAPolicy.FirstResponseTime;
                SLAPolicyDAO.ResolveTime = SLAPolicy.ResolveTime;
                SLAPolicyDAO.IsAlert = SLAPolicy.IsAlert;
                SLAPolicyDAO.IsAlertFRT = SLAPolicy.IsAlertFRT;
                SLAPolicyDAO.IsEscalation = SLAPolicy.IsEscalation;
                SLAPolicyDAO.IsEscalationFRT = SLAPolicy.IsEscalationFRT;
                SLAPolicyDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAPolicyDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAPolicyDAOs.Add(SLAPolicyDAO);
            }
            await DataContext.BulkMergeAsync(SLAPolicyDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAPolicy> SLAPolicies)
        {
            List<long> Ids = SLAPolicies.Select(x => x.Id).ToList();
            await DataContext.SLAPolicy
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAPolicyDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAPolicy SLAPolicy)
        {
        }
        
    }
}
