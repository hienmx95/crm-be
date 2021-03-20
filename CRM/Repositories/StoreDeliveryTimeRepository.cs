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
    public interface IStoreDeliveryTimeRepository
    {
        Task<int> Count(StoreDeliveryTimeFilter StoreDeliveryTimeFilter);
        Task<List<StoreDeliveryTime>> List(StoreDeliveryTimeFilter StoreDeliveryTimeFilter);
        Task<List<StoreDeliveryTime>> List(List<long> Ids);
        Task<StoreDeliveryTime> Get(long Id);
    }
    public class StoreDeliveryTimeRepository : IStoreDeliveryTimeRepository
    {
        private DataContext DataContext;
        public StoreDeliveryTimeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreDeliveryTimeDAO> DynamicFilter(IQueryable<StoreDeliveryTimeDAO> query, StoreDeliveryTimeFilter filter)
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

        private IQueryable<StoreDeliveryTimeDAO> OrFilter(IQueryable<StoreDeliveryTimeDAO> query, StoreDeliveryTimeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreDeliveryTimeDAO> initQuery = query.Where(q => false);
            foreach (StoreDeliveryTimeFilter StoreDeliveryTimeFilter in filter.OrFilter)
            {
                IQueryable<StoreDeliveryTimeDAO> queryable = query;
                if (StoreDeliveryTimeFilter.Id != null && StoreDeliveryTimeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, StoreDeliveryTimeFilter.Id);
                if (StoreDeliveryTimeFilter.Code != null && StoreDeliveryTimeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, StoreDeliveryTimeFilter.Code);
                if (StoreDeliveryTimeFilter.Name != null && StoreDeliveryTimeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, StoreDeliveryTimeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StoreDeliveryTimeDAO> DynamicOrder(IQueryable<StoreDeliveryTimeDAO> query, StoreDeliveryTimeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreDeliveryTimeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreDeliveryTimeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case StoreDeliveryTimeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreDeliveryTimeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreDeliveryTimeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case StoreDeliveryTimeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StoreDeliveryTime>> DynamicSelect(IQueryable<StoreDeliveryTimeDAO> query, StoreDeliveryTimeFilter filter)
        {
            List<StoreDeliveryTime> StoreDeliveryTimes = await query.Select(q => new StoreDeliveryTime()
            {
                Id = filter.Selects.Contains(StoreDeliveryTimeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(StoreDeliveryTimeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(StoreDeliveryTimeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return StoreDeliveryTimes;
        }

        public async Task<int> Count(StoreDeliveryTimeFilter filter)
        {
            IQueryable<StoreDeliveryTimeDAO> StoreDeliveryTimes = DataContext.StoreDeliveryTime.AsNoTracking();
            StoreDeliveryTimes = DynamicFilter(StoreDeliveryTimes, filter);
            return await StoreDeliveryTimes.CountAsync();
        }

        public async Task<List<StoreDeliveryTime>> List(StoreDeliveryTimeFilter filter)
        {
            if (filter == null) return new List<StoreDeliveryTime>();
            IQueryable<StoreDeliveryTimeDAO> StoreDeliveryTimeDAOs = DataContext.StoreDeliveryTime.AsNoTracking();
            StoreDeliveryTimeDAOs = DynamicFilter(StoreDeliveryTimeDAOs, filter);
            StoreDeliveryTimeDAOs = DynamicOrder(StoreDeliveryTimeDAOs, filter);
            List<StoreDeliveryTime> StoreDeliveryTimes = await DynamicSelect(StoreDeliveryTimeDAOs, filter);
            return StoreDeliveryTimes;
        }

        public async Task<List<StoreDeliveryTime>> List(List<long> Ids)
        {
            List<StoreDeliveryTime> StoreDeliveryTimes = await DataContext.StoreDeliveryTime.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new StoreDeliveryTime()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return StoreDeliveryTimes;
        }

        public async Task<StoreDeliveryTime> Get(long Id)
        {
            StoreDeliveryTime StoreDeliveryTime = await DataContext.StoreDeliveryTime.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new StoreDeliveryTime()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (StoreDeliveryTime == null)
                return null;

            return StoreDeliveryTime;
        }
    }
}
