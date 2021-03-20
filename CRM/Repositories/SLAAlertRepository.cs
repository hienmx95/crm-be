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
    public interface ISLAAlertRepository
    {
        Task<int> Count(SLAAlertFilter SLAAlertFilter);
        Task<List<SLAAlert>> List(SLAAlertFilter SLAAlertFilter);
        Task<SLAAlert> Get(long Id);
        Task<bool> Create(SLAAlert SLAAlert);
        Task<bool> Update(SLAAlert SLAAlert);
        Task<bool> Delete(SLAAlert SLAAlert);
        Task<bool> BulkMerge(List<SLAAlert> SLAAlerts);
        Task<bool> BulkDelete(List<SLAAlert> SLAAlerts);
    }
    public class SLAAlertRepository : ISLAAlertRepository
    {
        private DataContext DataContext;
        public SLAAlertRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAAlertDAO> DynamicFilter(IQueryable<SLAAlertDAO> query, SLAAlertFilter filter)
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
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAAlertDAO> OrFilter(IQueryable<SLAAlertDAO> query, SLAAlertFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAAlertDAO> initQuery = query.Where(q => false);
            foreach (SLAAlertFilter SLAAlertFilter in filter.OrFilter)
            {
                IQueryable<SLAAlertDAO> queryable = query;
                if (SLAAlertFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAAlertFilter.Id);
                if (SLAAlertFilter.TicketIssueLevelId != null)
                    queryable = queryable.Where(q => q.TicketIssueLevelId.HasValue).Where(q => q.TicketIssueLevelId, SLAAlertFilter.TicketIssueLevelId);
                if (SLAAlertFilter.Time != null)
                    queryable = queryable.Where(q => q.Time.HasValue).Where(q => q.Time, SLAAlertFilter.Time);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAAlertDAO> DynamicOrder(IQueryable<SLAAlertDAO> query, SLAAlertFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAAlertOrder.TicketIssueLevel:
                            query = query.OrderBy(q => q.TicketIssueLevelId);
                            break;
                        case SLAAlertOrder.IsNotification:
                            query = query.OrderBy(q => q.IsNotification);
                            break;
                        case SLAAlertOrder.IsMail:
                            query = query.OrderBy(q => q.IsMail);
                            break;
                        case SLAAlertOrder.IsSMS:
                            query = query.OrderBy(q => q.IsSMS);
                            break;
                        case SLAAlertOrder.Time:
                            query = query.OrderBy(q => q.Time);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAAlertOrder.TicketIssueLevel:
                            query = query.OrderByDescending(q => q.TicketIssueLevelId);
                            break;
                        case SLAAlertOrder.IsNotification:
                            query = query.OrderByDescending(q => q.IsNotification);
                            break;
                        case SLAAlertOrder.IsMail:
                            query = query.OrderByDescending(q => q.IsMail);
                            break;
                        case SLAAlertOrder.IsSMS:
                            query = query.OrderByDescending(q => q.IsSMS);
                            break;
                        case SLAAlertOrder.Time:
                            query = query.OrderByDescending(q => q.Time);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAAlert>> DynamicSelect(IQueryable<SLAAlertDAO> query, SLAAlertFilter filter)
        {
            List<SLAAlert> SLAAlerts = await query.Select(q => new SLAAlert()
            {
                Id = filter.Selects.Contains(SLAAlertSelect.Id) ? q.Id : default(long),
                TicketIssueLevelId = filter.Selects.Contains(SLAAlertSelect.TicketIssueLevel) ? q.TicketIssueLevelId : default(long?),
                IsNotification = filter.Selects.Contains(SLAAlertSelect.IsNotification) ? q.IsNotification : default(bool?),
                IsMail = filter.Selects.Contains(SLAAlertSelect.IsMail) ? q.IsMail : default(bool?),
                IsSMS = filter.Selects.Contains(SLAAlertSelect.IsSMS) ? q.IsSMS : default(bool?),
                Time = filter.Selects.Contains(SLAAlertSelect.Time) ? q.Time : default(long?),
                TicketIssueLevel = filter.Selects.Contains(SLAAlertSelect.TicketIssueLevel) && q.TicketIssueLevel != null ? new TicketIssueLevel
                {
                    Id = q.TicketIssueLevel.Id,
                    Name = q.TicketIssueLevel.Name,
                    OrderNumber = q.TicketIssueLevel.OrderNumber,
                    TicketGroupId = q.TicketIssueLevel.TicketGroupId,
                    StatusId = q.TicketIssueLevel.StatusId,
                    SLA = q.TicketIssueLevel.SLA,
                    Used = q.TicketIssueLevel.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAAlerts;
        }

        public async Task<int> Count(SLAAlertFilter filter)
        {
            IQueryable<SLAAlertDAO> SLAAlerts = DataContext.SLAAlert.AsNoTracking();
            SLAAlerts = DynamicFilter(SLAAlerts, filter);
            return await SLAAlerts.CountAsync();
        }

        public async Task<List<SLAAlert>> List(SLAAlertFilter filter)
        {
            if (filter == null) return new List<SLAAlert>();
            IQueryable<SLAAlertDAO> SLAAlertDAOs = DataContext.SLAAlert.AsNoTracking();
            SLAAlertDAOs = DynamicFilter(SLAAlertDAOs, filter);
            SLAAlertDAOs = DynamicOrder(SLAAlertDAOs, filter);
            List<SLAAlert> SLAAlerts = await DynamicSelect(SLAAlertDAOs, filter);
            return SLAAlerts;
        }

        public async Task<SLAAlert> Get(long Id)
        {
            SLAAlert SLAAlert = await DataContext.SLAAlert.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAAlert()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                TicketIssueLevelId = x.TicketIssueLevelId,
                IsNotification = x.IsNotification,
                IsMail = x.IsMail,
                IsSMS = x.IsSMS,
                Time = x.Time,
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
            }).FirstOrDefaultAsync();

            if (SLAAlert == null)
                return null;

            return SLAAlert;
        }
        public async Task<bool> Create(SLAAlert SLAAlert)
        {
            SLAAlertDAO SLAAlertDAO = new SLAAlertDAO();
            SLAAlertDAO.Id = SLAAlert.Id;
            SLAAlertDAO.TicketIssueLevelId = SLAAlert.TicketIssueLevelId;
            SLAAlertDAO.IsNotification = SLAAlert.IsNotification;
            SLAAlertDAO.IsMail = SLAAlert.IsMail;
            SLAAlertDAO.IsSMS = SLAAlert.IsSMS;
            SLAAlertDAO.Time = SLAAlert.Time;
            SLAAlertDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAAlertDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAAlert.Add(SLAAlertDAO);
            await DataContext.SaveChangesAsync();
            SLAAlert.Id = SLAAlertDAO.Id;
            await SaveReference(SLAAlert);
            return true;
        }

        public async Task<bool> Update(SLAAlert SLAAlert)
        {
            SLAAlertDAO SLAAlertDAO = DataContext.SLAAlert.Where(x => x.Id == SLAAlert.Id).FirstOrDefault();
            if (SLAAlertDAO == null)
                return false;
            SLAAlertDAO.Id = SLAAlert.Id;
            SLAAlertDAO.TicketIssueLevelId = SLAAlert.TicketIssueLevelId;
            SLAAlertDAO.IsNotification = SLAAlert.IsNotification;
            SLAAlertDAO.IsMail = SLAAlert.IsMail;
            SLAAlertDAO.IsSMS = SLAAlert.IsSMS;
            SLAAlertDAO.Time = SLAAlert.Time;
            SLAAlertDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAAlert);
            return true;
        }

        public async Task<bool> Delete(SLAAlert SLAAlert)
        {
            await DataContext.SLAAlert.Where(x => x.Id == SLAAlert.Id).UpdateFromQueryAsync(x => new SLAAlertDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAAlert> SLAAlerts)
        {
            List<SLAAlertDAO> SLAAlertDAOs = new List<SLAAlertDAO>();
            foreach (SLAAlert SLAAlert in SLAAlerts)
            {
                SLAAlertDAO SLAAlertDAO = new SLAAlertDAO();
                SLAAlertDAO.Id = SLAAlert.Id;
                SLAAlertDAO.TicketIssueLevelId = SLAAlert.TicketIssueLevelId;
                SLAAlertDAO.IsNotification = SLAAlert.IsNotification;
                SLAAlertDAO.IsMail = SLAAlert.IsMail;
                SLAAlertDAO.IsSMS = SLAAlert.IsSMS;
                SLAAlertDAO.Time = SLAAlert.Time;
                SLAAlertDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAAlertDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAAlertDAOs.Add(SLAAlertDAO);
            }
            await DataContext.BulkMergeAsync(SLAAlertDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAAlert> SLAAlerts)
        {
            List<long> Ids = SLAAlerts.Select(x => x.Id).ToList();
            await DataContext.SLAAlert
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAAlertDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAAlert SLAAlert)
        {
        }
        
    }
}
