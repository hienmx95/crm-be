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
    public interface IPotentialResultRepository
    {
        Task<int> Count(PotentialResultFilter PotentialResultFilter);
        Task<List<PotentialResult>> List(PotentialResultFilter PotentialResultFilter);
        Task<List<PotentialResult>> List(List<long> Ids);
        Task<PotentialResult> Get(long Id);
    }
    public class PotentialResultRepository : IPotentialResultRepository
    {
        private DataContext DataContext;
        public PotentialResultRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<PotentialResultDAO> DynamicFilter(IQueryable<PotentialResultDAO> query, PotentialResultFilter filter)
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

        private IQueryable<PotentialResultDAO> OrFilter(IQueryable<PotentialResultDAO> query, PotentialResultFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<PotentialResultDAO> initQuery = query.Where(q => false);
            foreach (PotentialResultFilter PotentialResultFilter in filter.OrFilter)
            {
                IQueryable<PotentialResultDAO> queryable = query;
                if (PotentialResultFilter.Id != null && PotentialResultFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, PotentialResultFilter.Id);
                if (PotentialResultFilter.Code != null && PotentialResultFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, PotentialResultFilter.Code);
                if (PotentialResultFilter.Name != null && PotentialResultFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, PotentialResultFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<PotentialResultDAO> DynamicOrder(IQueryable<PotentialResultDAO> query, PotentialResultFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case PotentialResultOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case PotentialResultOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case PotentialResultOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case PotentialResultOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case PotentialResultOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case PotentialResultOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<PotentialResult>> DynamicSelect(IQueryable<PotentialResultDAO> query, PotentialResultFilter filter)
        {
            List<PotentialResult> PotentialResults = await query.Select(q => new PotentialResult()
            {
                Id = filter.Selects.Contains(PotentialResultSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(PotentialResultSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(PotentialResultSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return PotentialResults;
        }

        public async Task<int> Count(PotentialResultFilter filter)
        {
            IQueryable<PotentialResultDAO> PotentialResults = DataContext.PotentialResult.AsNoTracking();
            PotentialResults = DynamicFilter(PotentialResults, filter);
            return await PotentialResults.CountAsync();
        }

        public async Task<List<PotentialResult>> List(PotentialResultFilter filter)
        {
            if (filter == null) return new List<PotentialResult>();
            IQueryable<PotentialResultDAO> PotentialResultDAOs = DataContext.PotentialResult.AsNoTracking();
            PotentialResultDAOs = DynamicFilter(PotentialResultDAOs, filter);
            PotentialResultDAOs = DynamicOrder(PotentialResultDAOs, filter);
            List<PotentialResult> PotentialResults = await DynamicSelect(PotentialResultDAOs, filter);
            return PotentialResults;
        }

        public async Task<List<PotentialResult>> List(List<long> Ids)
        {
            List<PotentialResult> PotentialResults = await DataContext.PotentialResult.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new PotentialResult()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return PotentialResults;
        }

        public async Task<PotentialResult> Get(long Id)
        {
            PotentialResult PotentialResult = await DataContext.PotentialResult.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new PotentialResult()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (PotentialResult == null)
                return null;

            return PotentialResult;
        }
    }
}
