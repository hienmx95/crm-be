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
    public interface IRatingStatusRepository
    {
        Task<int> Count(RatingStatusFilter RatingStatusFilter);
        Task<List<RatingStatus>> List(RatingStatusFilter RatingStatusFilter);
        Task<List<RatingStatus>> List(List<long> Ids);
        Task<RatingStatus> Get(long Id);
    }
    public class RatingStatusRepository : IRatingStatusRepository
    {
        private DataContext DataContext;
        public RatingStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<RatingStatusDAO> DynamicFilter(IQueryable<RatingStatusDAO> query, RatingStatusFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<RatingStatusDAO> OrFilter(IQueryable<RatingStatusDAO> query, RatingStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<RatingStatusDAO> initQuery = query.Where(q => false);
            foreach (RatingStatusFilter RatingStatusFilter in filter.OrFilter)
            {
                IQueryable<RatingStatusDAO> queryable = query;
                if (RatingStatusFilter.Id != null && RatingStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, RatingStatusFilter.Id);
                if (RatingStatusFilter.Name != null && RatingStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, RatingStatusFilter.Name);
                if (RatingStatusFilter.Code != null && RatingStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, RatingStatusFilter.Code);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<RatingStatusDAO> DynamicOrder(IQueryable<RatingStatusDAO> query, RatingStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case RatingStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case RatingStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case RatingStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case RatingStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case RatingStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case RatingStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<RatingStatus>> DynamicSelect(IQueryable<RatingStatusDAO> query, RatingStatusFilter filter)
        {
            List<RatingStatus> RatingStatuses = await query.Select(q => new RatingStatus()
            {
                Id = filter.Selects.Contains(RatingStatusSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(RatingStatusSelect.Name) ? q.Name : default(string),
                Code = filter.Selects.Contains(RatingStatusSelect.Code) ? q.Code : default(string),
            }).ToListAsync();
            return RatingStatuses;
        }

        public async Task<int> Count(RatingStatusFilter filter)
        {
            IQueryable<RatingStatusDAO> RatingStatuses = DataContext.RatingStatus.AsNoTracking();
            RatingStatuses = DynamicFilter(RatingStatuses, filter);
            return await RatingStatuses.CountAsync();
        }

        public async Task<List<RatingStatus>> List(RatingStatusFilter filter)
        {
            if (filter == null) return new List<RatingStatus>();
            IQueryable<RatingStatusDAO> RatingStatusDAOs = DataContext.RatingStatus.AsNoTracking();
            RatingStatusDAOs = DynamicFilter(RatingStatusDAOs, filter);
            RatingStatusDAOs = DynamicOrder(RatingStatusDAOs, filter);
            List<RatingStatus> RatingStatuses = await DynamicSelect(RatingStatusDAOs, filter);
            return RatingStatuses;
        }

        public async Task<List<RatingStatus>> List(List<long> Ids)
        {
            List<RatingStatus> RatingStatuses = await DataContext.RatingStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new RatingStatus()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).ToListAsync();
            

            return RatingStatuses;
        }

        public async Task<RatingStatus> Get(long Id)
        {
            RatingStatus RatingStatus = await DataContext.RatingStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new RatingStatus()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).FirstOrDefaultAsync();

            if (RatingStatus == null)
                return null;

            return RatingStatus;
        }
    }
}
