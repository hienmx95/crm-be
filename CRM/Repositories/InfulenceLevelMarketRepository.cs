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
    public interface IInfulenceLevelMarketRepository
    {
        Task<int> Count(InfulenceLevelMarketFilter InfulenceLevelMarketFilter);
        Task<List<InfulenceLevelMarket>> List(InfulenceLevelMarketFilter InfulenceLevelMarketFilter);
        Task<List<InfulenceLevelMarket>> List(List<long> Ids);
        Task<InfulenceLevelMarket> Get(long Id);
    }
    public class InfulenceLevelMarketRepository : IInfulenceLevelMarketRepository
    {
        private DataContext DataContext;
        public InfulenceLevelMarketRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<InfulenceLevelMarketDAO> DynamicFilter(IQueryable<InfulenceLevelMarketDAO> query, InfulenceLevelMarketFilter filter)
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

        private IQueryable<InfulenceLevelMarketDAO> OrFilter(IQueryable<InfulenceLevelMarketDAO> query, InfulenceLevelMarketFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<InfulenceLevelMarketDAO> initQuery = query.Where(q => false);
            foreach (InfulenceLevelMarketFilter InfulenceLevelMarketFilter in filter.OrFilter)
            {
                IQueryable<InfulenceLevelMarketDAO> queryable = query;
                if (InfulenceLevelMarketFilter.Id != null && InfulenceLevelMarketFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, InfulenceLevelMarketFilter.Id);
                if (InfulenceLevelMarketFilter.Code != null && InfulenceLevelMarketFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, InfulenceLevelMarketFilter.Code);
                if (InfulenceLevelMarketFilter.Name != null && InfulenceLevelMarketFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, InfulenceLevelMarketFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<InfulenceLevelMarketDAO> DynamicOrder(IQueryable<InfulenceLevelMarketDAO> query, InfulenceLevelMarketFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case InfulenceLevelMarketOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case InfulenceLevelMarketOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case InfulenceLevelMarketOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case InfulenceLevelMarketOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case InfulenceLevelMarketOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case InfulenceLevelMarketOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<InfulenceLevelMarket>> DynamicSelect(IQueryable<InfulenceLevelMarketDAO> query, InfulenceLevelMarketFilter filter)
        {
            List<InfulenceLevelMarket> InfulenceLevelMarkets = await query.Select(q => new InfulenceLevelMarket()
            {
                Id = filter.Selects.Contains(InfulenceLevelMarketSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(InfulenceLevelMarketSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(InfulenceLevelMarketSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return InfulenceLevelMarkets;
        }

        public async Task<int> Count(InfulenceLevelMarketFilter filter)
        {
            IQueryable<InfulenceLevelMarketDAO> InfulenceLevelMarkets = DataContext.InfulenceLevelMarket.AsNoTracking();
            InfulenceLevelMarkets = DynamicFilter(InfulenceLevelMarkets, filter);
            return await InfulenceLevelMarkets.CountAsync();
        }

        public async Task<List<InfulenceLevelMarket>> List(InfulenceLevelMarketFilter filter)
        {
            if (filter == null) return new List<InfulenceLevelMarket>();
            IQueryable<InfulenceLevelMarketDAO> InfulenceLevelMarketDAOs = DataContext.InfulenceLevelMarket.AsNoTracking();
            InfulenceLevelMarketDAOs = DynamicFilter(InfulenceLevelMarketDAOs, filter);
            InfulenceLevelMarketDAOs = DynamicOrder(InfulenceLevelMarketDAOs, filter);
            List<InfulenceLevelMarket> InfulenceLevelMarkets = await DynamicSelect(InfulenceLevelMarketDAOs, filter);
            return InfulenceLevelMarkets;
        }

        public async Task<List<InfulenceLevelMarket>> List(List<long> Ids)
        {
            List<InfulenceLevelMarket> InfulenceLevelMarkets = await DataContext.InfulenceLevelMarket.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new InfulenceLevelMarket()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return InfulenceLevelMarkets;
        }

        public async Task<InfulenceLevelMarket> Get(long Id)
        {
            InfulenceLevelMarket InfulenceLevelMarket = await DataContext.InfulenceLevelMarket.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new InfulenceLevelMarket()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (InfulenceLevelMarket == null)
                return null;

            return InfulenceLevelMarket;
        }
    }
}
