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
    public interface ICallCategoryRepository
    {
        Task<int> Count(CallCategoryFilter CallCategoryFilter);
        Task<List<CallCategory>> List(CallCategoryFilter CallCategoryFilter);
        Task<List<CallCategory>> List(List<long> Ids);
        Task<CallCategory> Get(long Id);
    }
    public class CallCategoryRepository : ICallCategoryRepository
    {
        private DataContext DataContext;
        public CallCategoryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CallCategoryDAO> DynamicFilter(IQueryable<CallCategoryDAO> query, CallCategoryFilter filter)
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

        private IQueryable<CallCategoryDAO> OrFilter(IQueryable<CallCategoryDAO> query, CallCategoryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CallCategoryDAO> initQuery = query.Where(q => false);
            foreach (CallCategoryFilter CallCategoryFilter in filter.OrFilter)
            {
                IQueryable<CallCategoryDAO> queryable = query;
                if (CallCategoryFilter.Id != null && CallCategoryFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CallCategoryFilter.Id);
                if (CallCategoryFilter.Code != null && CallCategoryFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CallCategoryFilter.Code);
                if (CallCategoryFilter.Name != null && CallCategoryFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CallCategoryFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CallCategoryDAO> DynamicOrder(IQueryable<CallCategoryDAO> query, CallCategoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CallCategoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CallCategoryOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CallCategoryOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CallCategoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CallCategoryOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CallCategoryOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CallCategory>> DynamicSelect(IQueryable<CallCategoryDAO> query, CallCategoryFilter filter)
        {
            List<CallCategory> CallCategories = await query.Select(q => new CallCategory()
            {
                Id = filter.Selects.Contains(CallCategorySelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CallCategorySelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CallCategorySelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CallCategories;
        }

        public async Task<int> Count(CallCategoryFilter filter)
        {
            IQueryable<CallCategoryDAO> CallCategories = DataContext.CallCategory.AsNoTracking();
            CallCategories = DynamicFilter(CallCategories, filter);
            return await CallCategories.CountAsync();
        }

        public async Task<List<CallCategory>> List(CallCategoryFilter filter)
        {
            if (filter == null) return new List<CallCategory>();
            IQueryable<CallCategoryDAO> CallCategoryDAOs = DataContext.CallCategory.AsNoTracking();
            CallCategoryDAOs = DynamicFilter(CallCategoryDAOs, filter);
            CallCategoryDAOs = DynamicOrder(CallCategoryDAOs, filter);
            List<CallCategory> CallCategories = await DynamicSelect(CallCategoryDAOs, filter);
            return CallCategories;
        }

        public async Task<List<CallCategory>> List(List<long> Ids)
        {
            List<CallCategory> CallCategories = await DataContext.CallCategory.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CallCategory()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CallCategories;
        }

        public async Task<CallCategory> Get(long Id)
        {
            CallCategory CallCategory = await DataContext.CallCategory.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CallCategory()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CallCategory == null)
                return null;

            return CallCategory;
        }
    }
}
