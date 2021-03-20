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
    public interface ICurrencyRepository
    {
        Task<int> Count(CurrencyFilter CurrencyFilter);
        Task<List<Currency>> List(CurrencyFilter CurrencyFilter);
        Task<List<Currency>> List(List<long> Ids);
        Task<Currency> Get(long Id);
    }
    public class CurrencyRepository : ICurrencyRepository
    {
        private DataContext DataContext;
        public CurrencyRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CurrencyDAO> DynamicFilter(IQueryable<CurrencyDAO> query, CurrencyFilter filter)
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

        private IQueryable<CurrencyDAO> OrFilter(IQueryable<CurrencyDAO> query, CurrencyFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CurrencyDAO> initQuery = query.Where(q => false);
            foreach (CurrencyFilter CurrencyFilter in filter.OrFilter)
            {
                IQueryable<CurrencyDAO> queryable = query;
                if (CurrencyFilter.Id != null && CurrencyFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CurrencyFilter.Id);
                if (CurrencyFilter.Code != null && CurrencyFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CurrencyFilter.Code);
                if (CurrencyFilter.Name != null && CurrencyFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CurrencyFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CurrencyDAO> DynamicOrder(IQueryable<CurrencyDAO> query, CurrencyFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CurrencyOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CurrencyOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CurrencyOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CurrencyOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CurrencyOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CurrencyOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Currency>> DynamicSelect(IQueryable<CurrencyDAO> query, CurrencyFilter filter)
        {
            List<Currency> Currencies = await query.Select(q => new Currency()
            {
                Id = filter.Selects.Contains(CurrencySelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CurrencySelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CurrencySelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return Currencies;
        }

        public async Task<int> Count(CurrencyFilter filter)
        {
            IQueryable<CurrencyDAO> Currencies = DataContext.Currency.AsNoTracking();
            Currencies = DynamicFilter(Currencies, filter);
            return await Currencies.CountAsync();
        }

        public async Task<List<Currency>> List(CurrencyFilter filter)
        {
            if (filter == null) return new List<Currency>();
            IQueryable<CurrencyDAO> CurrencyDAOs = DataContext.Currency.AsNoTracking();
            CurrencyDAOs = DynamicFilter(CurrencyDAOs, filter);
            CurrencyDAOs = DynamicOrder(CurrencyDAOs, filter);
            List<Currency> Currencies = await DynamicSelect(CurrencyDAOs, filter);
            return Currencies;
        }

        public async Task<List<Currency>> List(List<long> Ids)
        {
            List<Currency> Currencies = await DataContext.Currency.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Currency()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return Currencies;
        }

        public async Task<Currency> Get(long Id)
        {
            Currency Currency = await DataContext.Currency.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new Currency()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (Currency == null)
                return null;

            return Currency;
        }
    }
}
