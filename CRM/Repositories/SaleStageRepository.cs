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
    public interface ISaleStageRepository
    {
        Task<int> Count(SaleStageFilter SaleStageFilter);
        Task<List<SaleStage>> List(SaleStageFilter SaleStageFilter);
        Task<List<SaleStage>> List(List<long> Ids);
        Task<SaleStage> Get(long Id);
    }
    public class SaleStageRepository : ISaleStageRepository
    {
        private DataContext DataContext;
        public SaleStageRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SaleStageDAO> DynamicFilter(IQueryable<SaleStageDAO> query, SaleStageFilter filter)
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

        private IQueryable<SaleStageDAO> OrFilter(IQueryable<SaleStageDAO> query, SaleStageFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SaleStageDAO> initQuery = query.Where(q => false);
            foreach (SaleStageFilter SaleStageFilter in filter.OrFilter)
            {
                IQueryable<SaleStageDAO> queryable = query;
                if (SaleStageFilter.Id != null && SaleStageFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, SaleStageFilter.Id);
                if (SaleStageFilter.Code != null && SaleStageFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, SaleStageFilter.Code);
                if (SaleStageFilter.Name != null && SaleStageFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, SaleStageFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SaleStageDAO> DynamicOrder(IQueryable<SaleStageDAO> query, SaleStageFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SaleStageOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SaleStageOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SaleStageOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SaleStageOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SaleStageOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SaleStageOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SaleStage>> DynamicSelect(IQueryable<SaleStageDAO> query, SaleStageFilter filter)
        {
            List<SaleStage> SaleStages = await query.Select(q => new SaleStage()
            {
                Id = filter.Selects.Contains(SaleStageSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(SaleStageSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SaleStageSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return SaleStages;
        }

        public async Task<int> Count(SaleStageFilter filter)
        {
            IQueryable<SaleStageDAO> SaleStages = DataContext.SaleStage.AsNoTracking();
            SaleStages = DynamicFilter(SaleStages, filter);
            return await SaleStages.CountAsync();
        }

        public async Task<List<SaleStage>> List(SaleStageFilter filter)
        {
            if (filter == null) return new List<SaleStage>();
            IQueryable<SaleStageDAO> SaleStageDAOs = DataContext.SaleStage.AsNoTracking();
            SaleStageDAOs = DynamicFilter(SaleStageDAOs, filter);
            SaleStageDAOs = DynamicOrder(SaleStageDAOs, filter);
            List<SaleStage> SaleStages = await DynamicSelect(SaleStageDAOs, filter);
            return SaleStages;
        }

        public async Task<List<SaleStage>> List(List<long> Ids)
        {
            List<SaleStage> SaleStages = await DataContext.SaleStage.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new SaleStage()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return SaleStages;
        }

        public async Task<SaleStage> Get(long Id)
        {
            SaleStage SaleStage = await DataContext.SaleStage.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new SaleStage()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (SaleStage == null)
                return null;

            return SaleStage;
        }
    }
}
