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
    public interface IActivityTypeRepository
    {
        Task<int> Count(ActivityTypeFilter ActivityTypeFilter);
        Task<List<ActivityType>> List(ActivityTypeFilter ActivityTypeFilter);
        Task<List<ActivityType>> List(List<long> Ids);
        Task<ActivityType> Get(long Id);
    }
    public class ActivityTypeRepository : IActivityTypeRepository
    {
        private DataContext DataContext;
        public ActivityTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ActivityTypeDAO> DynamicFilter(IQueryable<ActivityTypeDAO> query, ActivityTypeFilter filter)
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

        private IQueryable<ActivityTypeDAO> OrFilter(IQueryable<ActivityTypeDAO> query, ActivityTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ActivityTypeDAO> initQuery = query.Where(q => false);
            foreach (ActivityTypeFilter ActivityTypeFilter in filter.OrFilter)
            {
                IQueryable<ActivityTypeDAO> queryable = query;
                if (ActivityTypeFilter.Id != null && ActivityTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ActivityTypeFilter.Id);
                if (ActivityTypeFilter.Code != null && ActivityTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ActivityTypeFilter.Code);
                if (ActivityTypeFilter.Name != null && ActivityTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ActivityTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ActivityTypeDAO> DynamicOrder(IQueryable<ActivityTypeDAO> query, ActivityTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ActivityTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ActivityTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ActivityTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ActivityTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ActivityTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ActivityTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ActivityType>> DynamicSelect(IQueryable<ActivityTypeDAO> query, ActivityTypeFilter filter)
        {
            List<ActivityType> ActivityTypes = await query.Select(q => new ActivityType()
            {
                Id = filter.Selects.Contains(ActivityTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ActivityTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ActivityTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ActivityTypes;
        }

        public async Task<int> Count(ActivityTypeFilter filter)
        {
            IQueryable<ActivityTypeDAO> ActivityTypes = DataContext.ActivityType.AsNoTracking();
            ActivityTypes = DynamicFilter(ActivityTypes, filter);
            return await ActivityTypes.CountAsync();
        }

        public async Task<List<ActivityType>> List(ActivityTypeFilter filter)
        {
            if (filter == null) return new List<ActivityType>();
            IQueryable<ActivityTypeDAO> ActivityTypeDAOs = DataContext.ActivityType.AsNoTracking();
            ActivityTypeDAOs = DynamicFilter(ActivityTypeDAOs, filter);
            ActivityTypeDAOs = DynamicOrder(ActivityTypeDAOs, filter);
            List<ActivityType> ActivityTypes = await DynamicSelect(ActivityTypeDAOs, filter);
            return ActivityTypes;
        }

        public async Task<List<ActivityType>> List(List<long> Ids)
        {
            List<ActivityType> ActivityTypes = await DataContext.ActivityType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ActivityType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return ActivityTypes;
        }

        public async Task<ActivityType> Get(long Id)
        {
            ActivityType ActivityType = await DataContext.ActivityType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new ActivityType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (ActivityType == null)
                return null;

            return ActivityType;
        }
    }
}
