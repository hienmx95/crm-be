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
    public interface IKpiCriteriaItemRepository
    {
        Task<int> Count(KpiCriteriaItemFilter KpiCriteriaItemFilter);
        Task<List<KpiCriteriaItem>> List(KpiCriteriaItemFilter KpiCriteriaItemFilter);
        Task<List<KpiCriteriaItem>> List(List<long> Ids);
        Task<KpiCriteriaItem> Get(long Id);
    }
    public class KpiCriteriaItemRepository : IKpiCriteriaItemRepository
    {
        private DataContext DataContext;
        public KpiCriteriaItemRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<KpiCriteriaItemDAO> DynamicFilter(IQueryable<KpiCriteriaItemDAO> query, KpiCriteriaItemFilter filter)
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

        private IQueryable<KpiCriteriaItemDAO> OrFilter(IQueryable<KpiCriteriaItemDAO> query, KpiCriteriaItemFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<KpiCriteriaItemDAO> initQuery = query.Where(q => false);
            foreach (KpiCriteriaItemFilter KpiCriteriaItemFilter in filter.OrFilter)
            {
                IQueryable<KpiCriteriaItemDAO> queryable = query;
                if (KpiCriteriaItemFilter.Id != null && KpiCriteriaItemFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, KpiCriteriaItemFilter.Id);
                if (KpiCriteriaItemFilter.Code != null && KpiCriteriaItemFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, KpiCriteriaItemFilter.Code);
                if (KpiCriteriaItemFilter.Name != null && KpiCriteriaItemFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, KpiCriteriaItemFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<KpiCriteriaItemDAO> DynamicOrder(IQueryable<KpiCriteriaItemDAO> query, KpiCriteriaItemFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case KpiCriteriaItemOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case KpiCriteriaItemOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case KpiCriteriaItemOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case KpiCriteriaItemOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case KpiCriteriaItemOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case KpiCriteriaItemOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<KpiCriteriaItem>> DynamicSelect(IQueryable<KpiCriteriaItemDAO> query, KpiCriteriaItemFilter filter)
        {
            List<KpiCriteriaItem> KpiCriteriaItems = await query.Select(q => new KpiCriteriaItem()
            {
                Id = filter.Selects.Contains(KpiCriteriaItemSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(KpiCriteriaItemSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(KpiCriteriaItemSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return KpiCriteriaItems;
        }

        public async Task<int> Count(KpiCriteriaItemFilter filter)
        {
            IQueryable<KpiCriteriaItemDAO> KpiCriteriaItems = DataContext.KpiCriteriaItem.AsNoTracking();
            KpiCriteriaItems = DynamicFilter(KpiCriteriaItems, filter);
            return await KpiCriteriaItems.CountAsync();
        }

        public async Task<List<KpiCriteriaItem>> List(KpiCriteriaItemFilter filter)
        {
            if (filter == null) return new List<KpiCriteriaItem>();
            IQueryable<KpiCriteriaItemDAO> KpiCriteriaItemDAOs = DataContext.KpiCriteriaItem.AsNoTracking();
            KpiCriteriaItemDAOs = DynamicFilter(KpiCriteriaItemDAOs, filter);
            KpiCriteriaItemDAOs = DynamicOrder(KpiCriteriaItemDAOs, filter);
            List<KpiCriteriaItem> KpiCriteriaItems = await DynamicSelect(KpiCriteriaItemDAOs, filter);
            return KpiCriteriaItems;
        }

        public async Task<List<KpiCriteriaItem>> List(List<long> Ids)
        {
            List<KpiCriteriaItem> KpiCriteriaItems = await DataContext.KpiCriteriaItem.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new KpiCriteriaItem()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return KpiCriteriaItems;
        }

        public async Task<KpiCriteriaItem> Get(long Id)
        {
            KpiCriteriaItem KpiCriteriaItem = await DataContext.KpiCriteriaItem.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new KpiCriteriaItem()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (KpiCriteriaItem == null)
                return null;

            return KpiCriteriaItem;
        }
    }
}
