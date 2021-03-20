using CRM.Common;
using CRM.Helpers;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IActivityStatusRepository
    {
        Task<int> Count(ActivityStatusFilter ActivityStatusFilter);
        Task<List<ActivityStatus>> List(ActivityStatusFilter ActivityStatusFilter);
        Task<List<ActivityStatus>> List(List<long> Ids);
        Task<ActivityStatus> Get(long Id);
    }
    public class ActivityStatusRepository : IActivityStatusRepository
    {
        private DataContext DataContext;
        public ActivityStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ActivityStatusDAO> DynamicFilter(IQueryable<ActivityStatusDAO> query, ActivityStatusFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ActivityStatusDAO> OrFilter(IQueryable<ActivityStatusDAO> query, ActivityStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ActivityStatusDAO> initQuery = query.Where(q => false);
            foreach (ActivityStatusFilter ActivityStatusFilter in filter.OrFilter)
            {
                IQueryable<ActivityStatusDAO> queryable = query;
                if (ActivityStatusFilter.Id != null && ActivityStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ActivityStatusFilter.Id);
                if (ActivityStatusFilter.Code != null && ActivityStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ActivityStatusFilter.Code);
                if (ActivityStatusFilter.Name != null && ActivityStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ActivityStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ActivityStatusDAO> DynamicOrder(IQueryable<ActivityStatusDAO> query, ActivityStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ActivityStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ActivityStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ActivityStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ActivityStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ActivityStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ActivityStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ActivityStatus>> DynamicSelect(IQueryable<ActivityStatusDAO> query, ActivityStatusFilter filter)
        {
            List<ActivityStatus> ActivityStatuses = await query.Select(q => new ActivityStatus()
            {
                Id = filter.Selects.Contains(ActivityStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ActivityStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ActivityStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ActivityStatuses;
        }

        public async Task<int> Count(ActivityStatusFilter filter)
        {
            IQueryable<ActivityStatusDAO> ActivityStatuses = DataContext.ActivityStatus.AsNoTracking();
            ActivityStatuses = DynamicFilter(ActivityStatuses, filter);
            return await ActivityStatuses.CountAsync();
        }

        public async Task<List<ActivityStatus>> List(ActivityStatusFilter filter)
        {
            if (filter == null) return new List<ActivityStatus>();
            IQueryable<ActivityStatusDAO> ActivityStatusDAOs = DataContext.ActivityStatus.AsNoTracking();
            ActivityStatusDAOs = DynamicFilter(ActivityStatusDAOs, filter);
            ActivityStatusDAOs = DynamicOrder(ActivityStatusDAOs, filter);
            List<ActivityStatus> ActivityStatuses = await DynamicSelect(ActivityStatusDAOs, filter);
            return ActivityStatuses;
        }

        public async Task<List<ActivityStatus>> List(List<long> Ids)
        {
            List<ActivityStatus> ActivityStatuses = await DataContext.ActivityStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ActivityStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return ActivityStatuses;
        }

        public async Task<ActivityStatus> Get(long Id)
        {
            ActivityStatus ActivityStatus = await DataContext.ActivityStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new ActivityStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (ActivityStatus == null)
                return null;

            return ActivityStatus;
        }
    }
}
