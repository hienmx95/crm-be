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
    public interface IMarketPriceRepository
    {
        Task<int> Count(MarketPriceFilter MarketPriceFilter);
        Task<List<MarketPrice>> List(MarketPriceFilter MarketPriceFilter);
        Task<List<MarketPrice>> List(List<long> Ids);
        Task<MarketPrice> Get(long Id);
    }
    public class MarketPriceRepository : IMarketPriceRepository
    {
        private DataContext DataContext;
        public MarketPriceRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<MarketPriceDAO> DynamicFilter(IQueryable<MarketPriceDAO> query, MarketPriceFilter filter)
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

        private IQueryable<MarketPriceDAO> OrFilter(IQueryable<MarketPriceDAO> query, MarketPriceFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<MarketPriceDAO> initQuery = query.Where(q => false);
            foreach (MarketPriceFilter MarketPriceFilter in filter.OrFilter)
            {
                IQueryable<MarketPriceDAO> queryable = query;
                if (MarketPriceFilter.Id != null && MarketPriceFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, MarketPriceFilter.Id);
                if (MarketPriceFilter.Code != null && MarketPriceFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, MarketPriceFilter.Code);
                if (MarketPriceFilter.Name != null && MarketPriceFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, MarketPriceFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<MarketPriceDAO> DynamicOrder(IQueryable<MarketPriceDAO> query, MarketPriceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case MarketPriceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case MarketPriceOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case MarketPriceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case MarketPriceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case MarketPriceOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case MarketPriceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<MarketPrice>> DynamicSelect(IQueryable<MarketPriceDAO> query, MarketPriceFilter filter)
        {
            List<MarketPrice> MarketPrices = await query.Select(q => new MarketPrice()
            {
                Id = filter.Selects.Contains(MarketPriceSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(MarketPriceSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(MarketPriceSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return MarketPrices;
        }

        public async Task<int> Count(MarketPriceFilter filter)
        {
            IQueryable<MarketPriceDAO> MarketPrices = DataContext.MarketPrice.AsNoTracking();
            MarketPrices = DynamicFilter(MarketPrices, filter);
            return await MarketPrices.CountAsync();
        }

        public async Task<List<MarketPrice>> List(MarketPriceFilter filter)
        {
            if (filter == null) return new List<MarketPrice>();
            IQueryable<MarketPriceDAO> MarketPriceDAOs = DataContext.MarketPrice.AsNoTracking();
            MarketPriceDAOs = DynamicFilter(MarketPriceDAOs, filter);
            MarketPriceDAOs = DynamicOrder(MarketPriceDAOs, filter);
            List<MarketPrice> MarketPrices = await DynamicSelect(MarketPriceDAOs, filter);
            return MarketPrices;
        }

        public async Task<List<MarketPrice>> List(List<long> Ids)
        {
            List<MarketPrice> MarketPrices = await DataContext.MarketPrice.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new MarketPrice()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return MarketPrices;
        }

        public async Task<MarketPrice> Get(long Id)
        {
            MarketPrice MarketPrice = await DataContext.MarketPrice.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new MarketPrice()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (MarketPrice == null)
                return null;

            return MarketPrice;
        }
    }
}
