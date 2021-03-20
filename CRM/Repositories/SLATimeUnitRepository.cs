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
    public interface ISLATimeUnitRepository
    {
        Task<int> Count(SLATimeUnitFilter SLATimeUnitFilter);
        Task<List<SLATimeUnit>> List(SLATimeUnitFilter SLATimeUnitFilter);
        Task<List<SLATimeUnit>> List(List<long> Ids);
        Task<SLATimeUnit> Get(long Id);
    }
    public class SLATimeUnitRepository : ISLATimeUnitRepository
    {
        private DataContext DataContext;
        public SLATimeUnitRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLATimeUnitDAO> DynamicFilter(IQueryable<SLATimeUnitDAO> query, SLATimeUnitFilter filter)
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

        private IQueryable<SLATimeUnitDAO> OrFilter(IQueryable<SLATimeUnitDAO> query, SLATimeUnitFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLATimeUnitDAO> initQuery = query.Where(q => false);
            foreach (SLATimeUnitFilter SLATimeUnitFilter in filter.OrFilter)
            {
                IQueryable<SLATimeUnitDAO> queryable = query;
                if (SLATimeUnitFilter.Id != null && SLATimeUnitFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, SLATimeUnitFilter.Id);
                if (SLATimeUnitFilter.Code != null && SLATimeUnitFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, SLATimeUnitFilter.Code);
                if (SLATimeUnitFilter.Name != null && SLATimeUnitFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, SLATimeUnitFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLATimeUnitDAO> DynamicOrder(IQueryable<SLATimeUnitDAO> query, SLATimeUnitFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLATimeUnitOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLATimeUnitOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SLATimeUnitOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLATimeUnitOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLATimeUnitOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SLATimeUnitOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLATimeUnit>> DynamicSelect(IQueryable<SLATimeUnitDAO> query, SLATimeUnitFilter filter)
        {
            List<SLATimeUnit> SLATimeUnits = await query.Select(q => new SLATimeUnit()
            {
                Id = filter.Selects.Contains(SLATimeUnitSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(SLATimeUnitSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SLATimeUnitSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return SLATimeUnits;
        }

        public async Task<int> Count(SLATimeUnitFilter filter)
        {
            IQueryable<SLATimeUnitDAO> SLATimeUnits = DataContext.SLATimeUnit.AsNoTracking();
            SLATimeUnits = DynamicFilter(SLATimeUnits, filter);
            return await SLATimeUnits.CountAsync();
        }

        public async Task<List<SLATimeUnit>> List(SLATimeUnitFilter filter)
        {
            if (filter == null) return new List<SLATimeUnit>();
            IQueryable<SLATimeUnitDAO> SLATimeUnitDAOs = DataContext.SLATimeUnit.AsNoTracking();
            SLATimeUnitDAOs = DynamicFilter(SLATimeUnitDAOs, filter);
            SLATimeUnitDAOs = DynamicOrder(SLATimeUnitDAOs, filter);
            List<SLATimeUnit> SLATimeUnits = await DynamicSelect(SLATimeUnitDAOs, filter);
            return SLATimeUnits;
        }

        public async Task<List<SLATimeUnit>> List(List<long> Ids)
        {
            List<SLATimeUnit> SLATimeUnits = await DataContext.SLATimeUnit.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new SLATimeUnit()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return SLATimeUnits;
        }

        public async Task<SLATimeUnit> Get(long Id)
        {
            SLATimeUnit SLATimeUnit = await DataContext.SLATimeUnit.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new SLATimeUnit()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (SLATimeUnit == null)
                return null;

            return SLATimeUnit;
        }
    }
}
