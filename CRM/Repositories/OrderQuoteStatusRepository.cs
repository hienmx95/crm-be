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
    public interface IOrderQuoteStatusRepository
    {
        Task<int> Count(OrderQuoteStatusFilter OrderQuoteStatusFilter);
        Task<List<OrderQuoteStatus>> List(OrderQuoteStatusFilter OrderQuoteStatusFilter);
        Task<List<OrderQuoteStatus>> List(List<long> Ids);
        Task<OrderQuoteStatus> Get(long Id);
    }
    public class OrderQuoteStatusRepository : IOrderQuoteStatusRepository
    {
        private DataContext DataContext;
        public OrderQuoteStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OrderQuoteStatusDAO> DynamicFilter(IQueryable<OrderQuoteStatusDAO> query, OrderQuoteStatusFilter filter)
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

        private IQueryable<OrderQuoteStatusDAO> OrFilter(IQueryable<OrderQuoteStatusDAO> query, OrderQuoteStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OrderQuoteStatusDAO> initQuery = query.Where(q => false);
            foreach (OrderQuoteStatusFilter OrderQuoteStatusFilter in filter.OrFilter)
            {
                IQueryable<OrderQuoteStatusDAO> queryable = query;
                if (OrderQuoteStatusFilter.Id != null && OrderQuoteStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OrderQuoteStatusFilter.Id);
                if (OrderQuoteStatusFilter.Code != null && OrderQuoteStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, OrderQuoteStatusFilter.Code);
                if (OrderQuoteStatusFilter.Name != null && OrderQuoteStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, OrderQuoteStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<OrderQuoteStatusDAO> DynamicOrder(IQueryable<OrderQuoteStatusDAO> query, OrderQuoteStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OrderQuoteStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OrderQuoteStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case OrderQuoteStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OrderQuoteStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OrderQuoteStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case OrderQuoteStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OrderQuoteStatus>> DynamicSelect(IQueryable<OrderQuoteStatusDAO> query, OrderQuoteStatusFilter filter)
        {
            List<OrderQuoteStatus> OrderQuoteStatuses = await query.Select(q => new OrderQuoteStatus()
            {
                Id = filter.Selects.Contains(OrderQuoteStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(OrderQuoteStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(OrderQuoteStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return OrderQuoteStatuses;
        }

        public async Task<int> Count(OrderQuoteStatusFilter filter)
        {
            IQueryable<OrderQuoteStatusDAO> OrderQuoteStatuses = DataContext.OrderQuoteStatus.AsNoTracking();
            OrderQuoteStatuses = DynamicFilter(OrderQuoteStatuses, filter);
            return await OrderQuoteStatuses.CountAsync();
        }

        public async Task<List<OrderQuoteStatus>> List(OrderQuoteStatusFilter filter)
        {
            if (filter == null) return new List<OrderQuoteStatus>();
            IQueryable<OrderQuoteStatusDAO> OrderQuoteStatusDAOs = DataContext.OrderQuoteStatus.AsNoTracking();
            OrderQuoteStatusDAOs = DynamicFilter(OrderQuoteStatusDAOs, filter);
            OrderQuoteStatusDAOs = DynamicOrder(OrderQuoteStatusDAOs, filter);
            List<OrderQuoteStatus> OrderQuoteStatuses = await DynamicSelect(OrderQuoteStatusDAOs, filter);
            return OrderQuoteStatuses;
        }

        public async Task<List<OrderQuoteStatus>> List(List<long> Ids)
        {
            List<OrderQuoteStatus> OrderQuoteStatuses = await DataContext.OrderQuoteStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new OrderQuoteStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return OrderQuoteStatuses;
        }

        public async Task<OrderQuoteStatus> Get(long Id)
        {
            OrderQuoteStatus OrderQuoteStatus = await DataContext.OrderQuoteStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new OrderQuoteStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (OrderQuoteStatus == null)
                return null;

            return OrderQuoteStatus;
        }
    }
}
