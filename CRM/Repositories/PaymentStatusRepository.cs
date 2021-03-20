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
    public interface IPaymentStatusRepository
    {
        Task<int> Count(PaymentStatusFilter PaymentStatusFilter);
        Task<List<PaymentStatus>> List(PaymentStatusFilter PaymentStatusFilter);
        Task<List<PaymentStatus>> List(List<long> Ids);
        Task<PaymentStatus> Get(long Id);
    }
    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        private DataContext DataContext;
        public PaymentStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<PaymentStatusDAO> DynamicFilter(IQueryable<PaymentStatusDAO> query, PaymentStatusFilter filter)
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

        private IQueryable<PaymentStatusDAO> OrFilter(IQueryable<PaymentStatusDAO> query, PaymentStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<PaymentStatusDAO> initQuery = query.Where(q => false);
            foreach (PaymentStatusFilter PaymentStatusFilter in filter.OrFilter)
            {
                IQueryable<PaymentStatusDAO> queryable = query;
                if (PaymentStatusFilter.Id != null && PaymentStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, PaymentStatusFilter.Id);
                if (PaymentStatusFilter.Code != null && PaymentStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, PaymentStatusFilter.Code);
                if (PaymentStatusFilter.Name != null && PaymentStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, PaymentStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<PaymentStatusDAO> DynamicOrder(IQueryable<PaymentStatusDAO> query, PaymentStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case PaymentStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case PaymentStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case PaymentStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case PaymentStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case PaymentStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case PaymentStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<PaymentStatus>> DynamicSelect(IQueryable<PaymentStatusDAO> query, PaymentStatusFilter filter)
        {
            List<PaymentStatus> PaymentStatuses = await query.Select(q => new PaymentStatus()
            {
                Id = filter.Selects.Contains(PaymentStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(PaymentStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(PaymentStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return PaymentStatuses;
        }

        public async Task<int> Count(PaymentStatusFilter filter)
        {
            IQueryable<PaymentStatusDAO> PaymentStatuses = DataContext.PaymentStatus.AsNoTracking();
            PaymentStatuses = DynamicFilter(PaymentStatuses, filter);
            return await PaymentStatuses.CountAsync();
        }

        public async Task<List<PaymentStatus>> List(PaymentStatusFilter filter)
        {
            if (filter == null) return new List<PaymentStatus>();
            IQueryable<PaymentStatusDAO> PaymentStatusDAOs = DataContext.PaymentStatus.AsNoTracking();
            PaymentStatusDAOs = DynamicFilter(PaymentStatusDAOs, filter);
            PaymentStatusDAOs = DynamicOrder(PaymentStatusDAOs, filter);
            List<PaymentStatus> PaymentStatuses = await DynamicSelect(PaymentStatusDAOs, filter);
            return PaymentStatuses;
        }

        public async Task<List<PaymentStatus>> List(List<long> Ids)
        {
            List<PaymentStatus> PaymentStatuses = await DataContext.PaymentStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new PaymentStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return PaymentStatuses;
        }

        public async Task<PaymentStatus> Get(long Id)
        {
            PaymentStatus PaymentStatus = await DataContext.PaymentStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new PaymentStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (PaymentStatus == null)
                return null;

            return PaymentStatus;
        }
    }
}
