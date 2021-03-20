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
    public interface IOrderPaymentStatusRepository
    {
        Task<int> Count(OrderPaymentStatusFilter OrderPaymentStatusFilter);
        Task<List<OrderPaymentStatus>> List(OrderPaymentStatusFilter OrderPaymentStatusFilter);
        Task<List<OrderPaymentStatus>> List(List<long> Ids);
        Task<OrderPaymentStatus> Get(long Id);
    }
    public class OrderPaymentStatusRepository : IOrderPaymentStatusRepository
    {
        private DataContext DataContext;
        public OrderPaymentStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OrderPaymentStatusDAO> DynamicFilter(IQueryable<OrderPaymentStatusDAO> query, OrderPaymentStatusFilter filter)
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

        private IQueryable<OrderPaymentStatusDAO> OrFilter(IQueryable<OrderPaymentStatusDAO> query, OrderPaymentStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OrderPaymentStatusDAO> initQuery = query.Where(q => false);
            foreach (OrderPaymentStatusFilter OrderPaymentStatusFilter in filter.OrFilter)
            {
                IQueryable<OrderPaymentStatusDAO> queryable = query;
                if (OrderPaymentStatusFilter.Id != null && OrderPaymentStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OrderPaymentStatusFilter.Id);
                if (OrderPaymentStatusFilter.Code != null && OrderPaymentStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, OrderPaymentStatusFilter.Code);
                if (OrderPaymentStatusFilter.Name != null && OrderPaymentStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, OrderPaymentStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<OrderPaymentStatusDAO> DynamicOrder(IQueryable<OrderPaymentStatusDAO> query, OrderPaymentStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OrderPaymentStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OrderPaymentStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case OrderPaymentStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OrderPaymentStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OrderPaymentStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case OrderPaymentStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OrderPaymentStatus>> DynamicSelect(IQueryable<OrderPaymentStatusDAO> query, OrderPaymentStatusFilter filter)
        {
            List<OrderPaymentStatus> OrderPaymentStatuses = await query.Select(q => new OrderPaymentStatus()
            {
                Id = filter.Selects.Contains(OrderPaymentStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(OrderPaymentStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(OrderPaymentStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return OrderPaymentStatuses;
        }

        public async Task<int> Count(OrderPaymentStatusFilter filter)
        {
            IQueryable<OrderPaymentStatusDAO> OrderPaymentStatuses = DataContext.OrderPaymentStatus.AsNoTracking();
            OrderPaymentStatuses = DynamicFilter(OrderPaymentStatuses, filter);
            return await OrderPaymentStatuses.CountAsync();
        }

        public async Task<List<OrderPaymentStatus>> List(OrderPaymentStatusFilter filter)
        {
            if (filter == null) return new List<OrderPaymentStatus>();
            IQueryable<OrderPaymentStatusDAO> OrderPaymentStatusDAOs = DataContext.OrderPaymentStatus.AsNoTracking();
            OrderPaymentStatusDAOs = DynamicFilter(OrderPaymentStatusDAOs, filter);
            OrderPaymentStatusDAOs = DynamicOrder(OrderPaymentStatusDAOs, filter);
            List<OrderPaymentStatus> OrderPaymentStatuses = await DynamicSelect(OrderPaymentStatusDAOs, filter);
            return OrderPaymentStatuses;
        }

        public async Task<List<OrderPaymentStatus>> List(List<long> Ids)
        {
            List<OrderPaymentStatus> OrderPaymentStatuses = await DataContext.OrderPaymentStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new OrderPaymentStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return OrderPaymentStatuses;
        }

        public async Task<OrderPaymentStatus> Get(long Id)
        {
            OrderPaymentStatus OrderPaymentStatus = await DataContext.OrderPaymentStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new OrderPaymentStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (OrderPaymentStatus == null)
                return null;

            return OrderPaymentStatus;
        }
    }
}
