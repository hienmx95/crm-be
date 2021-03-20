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
    public interface ITicketResolveTypeRepository
    {
        Task<int> Count(TicketResolveTypeFilter TicketResolveTypeFilter);
        Task<List<TicketResolveType>> List(TicketResolveTypeFilter TicketResolveTypeFilter);
        Task<List<TicketResolveType>> List(List<long> Ids);
        Task<TicketResolveType> Get(long Id);
    }
    public class TicketResolveTypeRepository : ITicketResolveTypeRepository
    {
        private DataContext DataContext;
        public TicketResolveTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketResolveTypeDAO> DynamicFilter(IQueryable<TicketResolveTypeDAO> query, TicketResolveTypeFilter filter)
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

        private IQueryable<TicketResolveTypeDAO> OrFilter(IQueryable<TicketResolveTypeDAO> query, TicketResolveTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketResolveTypeDAO> initQuery = query.Where(q => false);
            foreach (TicketResolveTypeFilter TicketResolveTypeFilter in filter.OrFilter)
            {
                IQueryable<TicketResolveTypeDAO> queryable = query;
                if (TicketResolveTypeFilter.Id != null && TicketResolveTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, TicketResolveTypeFilter.Id);
                if (TicketResolveTypeFilter.Code != null && TicketResolveTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, TicketResolveTypeFilter.Code);
                if (TicketResolveTypeFilter.Name != null && TicketResolveTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, TicketResolveTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketResolveTypeDAO> DynamicOrder(IQueryable<TicketResolveTypeDAO> query, TicketResolveTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketResolveTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketResolveTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case TicketResolveTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketResolveTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketResolveTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case TicketResolveTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketResolveType>> DynamicSelect(IQueryable<TicketResolveTypeDAO> query, TicketResolveTypeFilter filter)
        {
            List<TicketResolveType> TicketResolveTypes = await query.Select(q => new TicketResolveType()
            {
                Id = filter.Selects.Contains(TicketResolveTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(TicketResolveTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(TicketResolveTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return TicketResolveTypes;
        }

        public async Task<int> Count(TicketResolveTypeFilter filter)
        {
            IQueryable<TicketResolveTypeDAO> TicketResolveTypes = DataContext.TicketResolveType.AsNoTracking();
            TicketResolveTypes = DynamicFilter(TicketResolveTypes, filter);
            return await TicketResolveTypes.CountAsync();
        }

        public async Task<List<TicketResolveType>> List(TicketResolveTypeFilter filter)
        {
            if (filter == null) return new List<TicketResolveType>();
            IQueryable<TicketResolveTypeDAO> TicketResolveTypeDAOs = DataContext.TicketResolveType.AsNoTracking();
            TicketResolveTypeDAOs = DynamicFilter(TicketResolveTypeDAOs, filter);
            TicketResolveTypeDAOs = DynamicOrder(TicketResolveTypeDAOs, filter);
            List<TicketResolveType> TicketResolveTypes = await DynamicSelect(TicketResolveTypeDAOs, filter);
            return TicketResolveTypes;
        }

        public async Task<List<TicketResolveType>> List(List<long> Ids)
        {
            List<TicketResolveType> TicketResolveTypes = await DataContext.TicketResolveType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new TicketResolveType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return TicketResolveTypes;
        }

        public async Task<TicketResolveType> Get(long Id)
        {
            TicketResolveType TicketResolveType = await DataContext.TicketResolveType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new TicketResolveType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (TicketResolveType == null)
                return null;

            return TicketResolveType;
        }
    }
}
