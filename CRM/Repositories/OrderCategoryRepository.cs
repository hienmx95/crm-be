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
    public interface IOrderCategoryRepository
    {
        Task<int> Count(OrderCategoryFilter OrderCategoryFilter);
        Task<List<OrderCategory>> List(OrderCategoryFilter OrderCategoryFilter);
        Task<List<OrderCategory>> List(List<long> Ids);
        Task<OrderCategory> Get(long Id);
    }
    public class OrderCategoryRepository : IOrderCategoryRepository
    {
        private DataContext DataContext;
        public OrderCategoryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OrderCategoryDAO> DynamicFilter(IQueryable<OrderCategoryDAO> query, OrderCategoryFilter filter)
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

        private IQueryable<OrderCategoryDAO> OrFilter(IQueryable<OrderCategoryDAO> query, OrderCategoryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OrderCategoryDAO> initQuery = query.Where(q => false);
            foreach (OrderCategoryFilter OrderCategoryFilter in filter.OrFilter)
            {
                IQueryable<OrderCategoryDAO> queryable = query;
                if (OrderCategoryFilter.Id != null && OrderCategoryFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OrderCategoryFilter.Id);
                if (OrderCategoryFilter.Code != null && OrderCategoryFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, OrderCategoryFilter.Code);
                if (OrderCategoryFilter.Name != null && OrderCategoryFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, OrderCategoryFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<OrderCategoryDAO> DynamicOrder(IQueryable<OrderCategoryDAO> query, OrderCategoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OrderCategoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OrderCategoryOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case OrderCategoryOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OrderCategoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OrderCategoryOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case OrderCategoryOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OrderCategory>> DynamicSelect(IQueryable<OrderCategoryDAO> query, OrderCategoryFilter filter)
        {
            List<OrderCategory> OrderCategorys = await query.Select(q => new OrderCategory()
            {
                Id = filter.Selects.Contains(OrderCategorySelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(OrderCategorySelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(OrderCategorySelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return OrderCategorys;
        }

        public async Task<int> Count(OrderCategoryFilter filter)
        {
            IQueryable<OrderCategoryDAO> OrderCategorys = DataContext.OrderCategory.AsNoTracking();
            OrderCategorys = DynamicFilter(OrderCategorys, filter);
            return await OrderCategorys.CountAsync();
        }

        public async Task<List<OrderCategory>> List(OrderCategoryFilter filter)
        {
            if (filter == null) return new List<OrderCategory>();
            IQueryable<OrderCategoryDAO> OrderCategoryDAOs = DataContext.OrderCategory.AsNoTracking();
            OrderCategoryDAOs = DynamicFilter(OrderCategoryDAOs, filter);
            OrderCategoryDAOs = DynamicOrder(OrderCategoryDAOs, filter);
            List<OrderCategory> OrderCategorys = await DynamicSelect(OrderCategoryDAOs, filter);
            return OrderCategorys;
        }

        public async Task<List<OrderCategory>> List(List<long> Ids)
        {
            List<OrderCategory> OrderCategorys = await DataContext.OrderCategory.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new OrderCategory()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return OrderCategorys;
        }

        public async Task<OrderCategory> Get(long Id)
        {
            OrderCategory OrderCategory = await DataContext.OrderCategory.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new OrderCategory()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (OrderCategory == null)
                return null;

            return OrderCategory;
        }
    }
}
