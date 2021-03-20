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
    public interface IRepairStatusRepository
    {
        Task<int> Count(RepairStatusFilter RepairStatusFilter);
        Task<List<RepairStatus>> List(RepairStatusFilter RepairStatusFilter);
        Task<List<RepairStatus>> List(List<long> Ids);
        Task<RepairStatus> Get(long Id);
    }
    public class RepairStatusRepository : IRepairStatusRepository
    {
        private DataContext DataContext;
        public RepairStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<RepairStatusDAO> DynamicFilter(IQueryable<RepairStatusDAO> query, RepairStatusFilter filter)
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

        private IQueryable<RepairStatusDAO> OrFilter(IQueryable<RepairStatusDAO> query, RepairStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<RepairStatusDAO> initQuery = query.Where(q => false);
            foreach (RepairStatusFilter RepairStatusFilter in filter.OrFilter)
            {
                IQueryable<RepairStatusDAO> queryable = query;
                if (RepairStatusFilter.Id != null && RepairStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, RepairStatusFilter.Id);
                if (RepairStatusFilter.Name != null && RepairStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, RepairStatusFilter.Name);
                if (RepairStatusFilter.Code != null && RepairStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, RepairStatusFilter.Code);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<RepairStatusDAO> DynamicOrder(IQueryable<RepairStatusDAO> query, RepairStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case RepairStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case RepairStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case RepairStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case RepairStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case RepairStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case RepairStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<RepairStatus>> DynamicSelect(IQueryable<RepairStatusDAO> query, RepairStatusFilter filter)
        {
            List<RepairStatus> RepairStatuses = await query.Select(q => new RepairStatus()
            {
                Id = filter.Selects.Contains(RepairStatusSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(RepairStatusSelect.Name) ? q.Name : default(string),
                Code = filter.Selects.Contains(RepairStatusSelect.Code) ? q.Code : default(string),
            }).ToListAsync();
            return RepairStatuses;
        }

        public async Task<int> Count(RepairStatusFilter filter)
        {
            IQueryable<RepairStatusDAO> RepairStatuses = DataContext.RepairStatus.AsNoTracking();
            RepairStatuses = DynamicFilter(RepairStatuses, filter);
            return await RepairStatuses.CountAsync();
        }

        public async Task<List<RepairStatus>> List(RepairStatusFilter filter)
        {
            if (filter == null) return new List<RepairStatus>();
            IQueryable<RepairStatusDAO> RepairStatusDAOs = DataContext.RepairStatus.AsNoTracking();
            RepairStatusDAOs = DynamicFilter(RepairStatusDAOs, filter);
            RepairStatusDAOs = DynamicOrder(RepairStatusDAOs, filter);
            List<RepairStatus> RepairStatuses = await DynamicSelect(RepairStatusDAOs, filter);
            return RepairStatuses;
        }

        public async Task<List<RepairStatus>> List(List<long> Ids)
        {
            List<RepairStatus> RepairStatuses = await DataContext.RepairStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new RepairStatus()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).ToListAsync();
            

            return RepairStatuses;
        }

        public async Task<RepairStatus> Get(long Id)
        {
            RepairStatus RepairStatus = await DataContext.RepairStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new RepairStatus()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).FirstOrDefaultAsync();

            if (RepairStatus == null)
                return null;

            return RepairStatus;
        }
    }
}
