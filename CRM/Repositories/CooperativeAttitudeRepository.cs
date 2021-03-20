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
    public interface ICooperativeAttitudeRepository
    {
        Task<int> Count(CooperativeAttitudeFilter CooperativeAttitudeFilter);
        Task<List<CooperativeAttitude>> List(CooperativeAttitudeFilter CooperativeAttitudeFilter);
        Task<List<CooperativeAttitude>> List(List<long> Ids);
        Task<CooperativeAttitude> Get(long Id);
    }
    public class CooperativeAttitudeRepository : ICooperativeAttitudeRepository
    {
        private DataContext DataContext;
        public CooperativeAttitudeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CooperativeAttitudeDAO> DynamicFilter(IQueryable<CooperativeAttitudeDAO> query, CooperativeAttitudeFilter filter)
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

        private IQueryable<CooperativeAttitudeDAO> OrFilter(IQueryable<CooperativeAttitudeDAO> query, CooperativeAttitudeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CooperativeAttitudeDAO> initQuery = query.Where(q => false);
            foreach (CooperativeAttitudeFilter CooperativeAttitudeFilter in filter.OrFilter)
            {
                IQueryable<CooperativeAttitudeDAO> queryable = query;
                if (CooperativeAttitudeFilter.Id != null && CooperativeAttitudeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CooperativeAttitudeFilter.Id);
                if (CooperativeAttitudeFilter.Code != null && CooperativeAttitudeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CooperativeAttitudeFilter.Code);
                if (CooperativeAttitudeFilter.Name != null && CooperativeAttitudeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CooperativeAttitudeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CooperativeAttitudeDAO> DynamicOrder(IQueryable<CooperativeAttitudeDAO> query, CooperativeAttitudeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CooperativeAttitudeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CooperativeAttitudeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CooperativeAttitudeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CooperativeAttitudeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CooperativeAttitudeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CooperativeAttitudeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CooperativeAttitude>> DynamicSelect(IQueryable<CooperativeAttitudeDAO> query, CooperativeAttitudeFilter filter)
        {
            List<CooperativeAttitude> CooperativeAttitudes = await query.Select(q => new CooperativeAttitude()
            {
                Id = filter.Selects.Contains(CooperativeAttitudeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CooperativeAttitudeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CooperativeAttitudeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CooperativeAttitudes;
        }

        public async Task<int> Count(CooperativeAttitudeFilter filter)
        {
            IQueryable<CooperativeAttitudeDAO> CooperativeAttitudes = DataContext.CooperativeAttitude.AsNoTracking();
            CooperativeAttitudes = DynamicFilter(CooperativeAttitudes, filter);
            return await CooperativeAttitudes.CountAsync();
        }

        public async Task<List<CooperativeAttitude>> List(CooperativeAttitudeFilter filter)
        {
            if (filter == null) return new List<CooperativeAttitude>();
            IQueryable<CooperativeAttitudeDAO> CooperativeAttitudeDAOs = DataContext.CooperativeAttitude.AsNoTracking();
            CooperativeAttitudeDAOs = DynamicFilter(CooperativeAttitudeDAOs, filter);
            CooperativeAttitudeDAOs = DynamicOrder(CooperativeAttitudeDAOs, filter);
            List<CooperativeAttitude> CooperativeAttitudes = await DynamicSelect(CooperativeAttitudeDAOs, filter);
            return CooperativeAttitudes;
        }

        public async Task<List<CooperativeAttitude>> List(List<long> Ids)
        {
            List<CooperativeAttitude> CooperativeAttitudes = await DataContext.CooperativeAttitude.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CooperativeAttitude()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CooperativeAttitudes;
        }

        public async Task<CooperativeAttitude> Get(long Id)
        {
            CooperativeAttitude CooperativeAttitude = await DataContext.CooperativeAttitude.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CooperativeAttitude()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CooperativeAttitude == null)
                return null;

            return CooperativeAttitude;
        }
    }
}
