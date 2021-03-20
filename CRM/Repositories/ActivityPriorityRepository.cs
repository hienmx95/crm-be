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
    public interface IActivityPriorityRepository
    {
        Task<int> Count(ActivityPriorityFilter ActivityPriorityFilter);
        Task<List<ActivityPriority>> List(ActivityPriorityFilter ActivityPriorityFilter);
        Task<List<ActivityPriority>> List(List<long> Ids);
        Task<ActivityPriority> Get(long Id);
    }
    public class ActivityPriorityRepository : IActivityPriorityRepository
    {
        private DataContext DataContext;
        public ActivityPriorityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ActivityPriorityDAO> DynamicFilter(IQueryable<ActivityPriorityDAO> query, ActivityPriorityFilter filter)
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

        private IQueryable<ActivityPriorityDAO> OrFilter(IQueryable<ActivityPriorityDAO> query, ActivityPriorityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ActivityPriorityDAO> initQuery = query.Where(q => false);
            foreach (ActivityPriorityFilter ActivityPriorityFilter in filter.OrFilter)
            {
                IQueryable<ActivityPriorityDAO> queryable = query;
                if (ActivityPriorityFilter.Id != null && ActivityPriorityFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ActivityPriorityFilter.Id);
                if (ActivityPriorityFilter.Code != null && ActivityPriorityFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ActivityPriorityFilter.Code);
                if (ActivityPriorityFilter.Name != null && ActivityPriorityFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ActivityPriorityFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ActivityPriorityDAO> DynamicOrder(IQueryable<ActivityPriorityDAO> query, ActivityPriorityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ActivityPriorityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ActivityPriorityOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ActivityPriorityOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ActivityPriorityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ActivityPriorityOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ActivityPriorityOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ActivityPriority>> DynamicSelect(IQueryable<ActivityPriorityDAO> query, ActivityPriorityFilter filter)
        {
            List<ActivityPriority> ActivityPriorities = await query.Select(q => new ActivityPriority()
            {
                Id = filter.Selects.Contains(ActivityPrioritySelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ActivityPrioritySelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ActivityPrioritySelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ActivityPriorities;
        }

        public async Task<int> Count(ActivityPriorityFilter filter)
        {
            IQueryable<ActivityPriorityDAO> ActivityPriorities = DataContext.ActivityPriority.AsNoTracking();
            ActivityPriorities = DynamicFilter(ActivityPriorities, filter);
            return await ActivityPriorities.CountAsync();
        }

        public async Task<List<ActivityPriority>> List(ActivityPriorityFilter filter)
        {
            if (filter == null) return new List<ActivityPriority>();
            IQueryable<ActivityPriorityDAO> ActivityPriorityDAOs = DataContext.ActivityPriority.AsNoTracking();
            ActivityPriorityDAOs = DynamicFilter(ActivityPriorityDAOs, filter);
            ActivityPriorityDAOs = DynamicOrder(ActivityPriorityDAOs, filter);
            List<ActivityPriority> ActivityPriorities = await DynamicSelect(ActivityPriorityDAOs, filter);
            return ActivityPriorities;
        }

        public async Task<List<ActivityPriority>> List(List<long> Ids)
        {
            List<ActivityPriority> ActivityPriorities = await DataContext.ActivityPriority.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ActivityPriority()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return ActivityPriorities;
        }

        public async Task<ActivityPriority> Get(long Id)
        {
            ActivityPriority ActivityPriority = await DataContext.ActivityPriority.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new ActivityPriority()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (ActivityPriority == null)
                return null;

            return ActivityPriority;
        }
    }
}
