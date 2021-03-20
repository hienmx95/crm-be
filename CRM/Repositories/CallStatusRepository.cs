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
    public interface ICallStatusRepository
    {
        Task<int> Count(CallStatusFilter CallStatusFilter);
        Task<List<CallStatus>> List(CallStatusFilter CallStatusFilter);
        Task<List<CallStatus>> List(List<long> Ids);
        Task<CallStatus> Get(long Id);
    }
    public class CallStatusRepository : ICallStatusRepository
    {
        private DataContext DataContext;
        public CallStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CallStatusDAO> DynamicFilter(IQueryable<CallStatusDAO> query, CallStatusFilter filter)
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

        private IQueryable<CallStatusDAO> OrFilter(IQueryable<CallStatusDAO> query, CallStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CallStatusDAO> initQuery = query.Where(q => false);
            foreach (CallStatusFilter CallStatusFilter in filter.OrFilter)
            {
                IQueryable<CallStatusDAO> queryable = query;
                if (CallStatusFilter.Id != null && CallStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CallStatusFilter.Id);
                if (CallStatusFilter.Code != null && CallStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CallStatusFilter.Code);
                if (CallStatusFilter.Name != null && CallStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CallStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CallStatusDAO> DynamicOrder(IQueryable<CallStatusDAO> query, CallStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CallStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CallStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CallStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CallStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CallStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CallStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CallStatus>> DynamicSelect(IQueryable<CallStatusDAO> query, CallStatusFilter filter)
        {
            List<CallStatus> CallStatuses = await query.Select(q => new CallStatus()
            {
                Id = filter.Selects.Contains(CallStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CallStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CallStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CallStatuses;
        }

        public async Task<int> Count(CallStatusFilter filter)
        {
            IQueryable<CallStatusDAO> CallStatuses = DataContext.CallStatus.AsNoTracking();
            CallStatuses = DynamicFilter(CallStatuses, filter);
            return await CallStatuses.CountAsync();
        }

        public async Task<List<CallStatus>> List(CallStatusFilter filter)
        {
            if (filter == null) return new List<CallStatus>();
            IQueryable<CallStatusDAO> CallStatusDAOs = DataContext.CallStatus.AsNoTracking();
            CallStatusDAOs = DynamicFilter(CallStatusDAOs, filter);
            CallStatusDAOs = DynamicOrder(CallStatusDAOs, filter);
            List<CallStatus> CallStatuses = await DynamicSelect(CallStatusDAOs, filter);
            return CallStatuses;
        }

        public async Task<List<CallStatus>> List(List<long> Ids)
        {
            List<CallStatus> CallStatuses = await DataContext.CallStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CallStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CallStatuses;
        }

        public async Task<CallStatus> Get(long Id)
        {
            CallStatus CallStatus = await DataContext.CallStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CallStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CallStatus == null)
                return null;

            return CallStatus;
        }
    }
}
