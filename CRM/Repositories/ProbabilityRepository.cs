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
    public interface IProbabilityRepository
    {
        Task<int> Count(ProbabilityFilter ProbabilityFilter);
        Task<List<Probability>> List(ProbabilityFilter ProbabilityFilter);
        Task<List<Probability>> List(List<long> Ids);
        Task<Probability> Get(long Id);
    }
    public class ProbabilityRepository : IProbabilityRepository
    {
        private DataContext DataContext;
        public ProbabilityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ProbabilityDAO> DynamicFilter(IQueryable<ProbabilityDAO> query, ProbabilityFilter filter)
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

        private IQueryable<ProbabilityDAO> OrFilter(IQueryable<ProbabilityDAO> query, ProbabilityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ProbabilityDAO> initQuery = query.Where(q => false);
            foreach (ProbabilityFilter ProbabilityFilter in filter.OrFilter)
            {
                IQueryable<ProbabilityDAO> queryable = query;
                if (ProbabilityFilter.Id != null && ProbabilityFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ProbabilityFilter.Id);
                if (ProbabilityFilter.Code != null && ProbabilityFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ProbabilityFilter.Code);
                if (ProbabilityFilter.Name != null && ProbabilityFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ProbabilityFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ProbabilityDAO> DynamicOrder(IQueryable<ProbabilityDAO> query, ProbabilityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ProbabilityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProbabilityOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProbabilityOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ProbabilityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProbabilityOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProbabilityOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Probability>> DynamicSelect(IQueryable<ProbabilityDAO> query, ProbabilityFilter filter)
        {
            List<Probability> Probabilities = await query.Select(q => new Probability()
            {
                Id = filter.Selects.Contains(ProbabilitySelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ProbabilitySelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProbabilitySelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return Probabilities;
        }

        public async Task<int> Count(ProbabilityFilter filter)
        {
            IQueryable<ProbabilityDAO> Probabilities = DataContext.Probability.AsNoTracking();
            Probabilities = DynamicFilter(Probabilities, filter);
            return await Probabilities.CountAsync();
        }

        public async Task<List<Probability>> List(ProbabilityFilter filter)
        {
            if (filter == null) return new List<Probability>();
            IQueryable<ProbabilityDAO> ProbabilityDAOs = DataContext.Probability.AsNoTracking();
            ProbabilityDAOs = DynamicFilter(ProbabilityDAOs, filter);
            ProbabilityDAOs = DynamicOrder(ProbabilityDAOs, filter);
            List<Probability> Probabilities = await DynamicSelect(ProbabilityDAOs, filter);
            return Probabilities;
        }

        public async Task<List<Probability>> List(List<long> Ids)
        {
            List<Probability> Probabilities = await DataContext.Probability.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Probability()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return Probabilities;
        }

        public async Task<Probability> Get(long Id)
        {
            Probability Probability = await DataContext.Probability.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new Probability()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (Probability == null)
                return null;

            return Probability;
        }
    }
}
