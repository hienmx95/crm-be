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
    public interface ISexRepository
    {
        Task<int> Count(SexFilter SexFilter);
        Task<List<Sex>> List(SexFilter SexFilter);
        Task<List<Sex>> List(List<long> Ids);
        Task<Sex> Get(long Id);
    }
    public class SexRepository : ISexRepository
    {
        private DataContext DataContext;
        public SexRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SexDAO> DynamicFilter(IQueryable<SexDAO> query, SexFilter filter)
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

        private IQueryable<SexDAO> OrFilter(IQueryable<SexDAO> query, SexFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SexDAO> initQuery = query.Where(q => false);
            foreach (SexFilter SexFilter in filter.OrFilter)
            {
                IQueryable<SexDAO> queryable = query;
                if (SexFilter.Id != null && SexFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, SexFilter.Id);
                if (SexFilter.Code != null && SexFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, SexFilter.Code);
                if (SexFilter.Name != null && SexFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, SexFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SexDAO> DynamicOrder(IQueryable<SexDAO> query, SexFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SexOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SexOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SexOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SexOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SexOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SexOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Sex>> DynamicSelect(IQueryable<SexDAO> query, SexFilter filter)
        {
            List<Sex> Sexes = await query.Select(q => new Sex()
            {
                Id = filter.Selects.Contains(SexSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(SexSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SexSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return Sexes;
        }

        public async Task<int> Count(SexFilter filter)
        {
            IQueryable<SexDAO> Sexes = DataContext.Sex.AsNoTracking();
            Sexes = DynamicFilter(Sexes, filter);
            return await Sexes.CountAsync();
        }

        public async Task<List<Sex>> List(SexFilter filter)
        {
            if (filter == null) return new List<Sex>();
            IQueryable<SexDAO> SexDAOs = DataContext.Sex.AsNoTracking();
            SexDAOs = DynamicFilter(SexDAOs, filter);
            SexDAOs = DynamicOrder(SexDAOs, filter);
            List<Sex> Sexes = await DynamicSelect(SexDAOs, filter);
            return Sexes;
        }

        public async Task<List<Sex>> List(List<long> Ids)
        {
            List<Sex> Sexes = await DataContext.Sex.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Sex()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return Sexes;
        }

        public async Task<Sex> Get(long Id)
        {
            Sex Sex = await DataContext.Sex.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new Sex()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (Sex == null)
                return null;

            return Sex;
        }
    }
}
