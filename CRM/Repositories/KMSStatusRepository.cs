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
    public interface IKMSStatusRepository
    {
        Task<int> Count(KMSStatusFilter KMSStatusFilter);
        Task<List<KMSStatus>> List(KMSStatusFilter KMSStatusFilter);
        Task<List<KMSStatus>> List(List<long> Ids);
        Task<KMSStatus> Get(long Id);
    }
    public class KMSStatusRepository : IKMSStatusRepository
    {
        private DataContext DataContext;
        public KMSStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<KMSStatusDAO> DynamicFilter(IQueryable<KMSStatusDAO> query, KMSStatusFilter filter)
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

        private IQueryable<KMSStatusDAO> OrFilter(IQueryable<KMSStatusDAO> query, KMSStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<KMSStatusDAO> initQuery = query.Where(q => false);
            foreach (KMSStatusFilter KMSStatusFilter in filter.OrFilter)
            {
                IQueryable<KMSStatusDAO> queryable = query;
                if (KMSStatusFilter.Id != null && KMSStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, KMSStatusFilter.Id);
                if (KMSStatusFilter.Code != null && KMSStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, KMSStatusFilter.Code);
                if (KMSStatusFilter.Name != null && KMSStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, KMSStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<KMSStatusDAO> DynamicOrder(IQueryable<KMSStatusDAO> query, KMSStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case KMSStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case KMSStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case KMSStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case KMSStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case KMSStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case KMSStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<KMSStatus>> DynamicSelect(IQueryable<KMSStatusDAO> query, KMSStatusFilter filter)
        {
            List<KMSStatus> KMSStatuses = await query.Select(q => new KMSStatus()
            {
                Id = filter.Selects.Contains(KMSStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(KMSStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(KMSStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return KMSStatuses;
        }

        public async Task<int> Count(KMSStatusFilter filter)
        {
            IQueryable<KMSStatusDAO> KMSStatuses = DataContext.KMSStatus.AsNoTracking();
            KMSStatuses = DynamicFilter(KMSStatuses, filter);
            return await KMSStatuses.CountAsync();
        }

        public async Task<List<KMSStatus>> List(KMSStatusFilter filter)
        {
            if (filter == null) return new List<KMSStatus>();
            IQueryable<KMSStatusDAO> KMSStatusDAOs = DataContext.KMSStatus.AsNoTracking();
            KMSStatusDAOs = DynamicFilter(KMSStatusDAOs, filter);
            KMSStatusDAOs = DynamicOrder(KMSStatusDAOs, filter);
            List<KMSStatus> KMSStatuses = await DynamicSelect(KMSStatusDAOs, filter);
            return KMSStatuses;
        }

        public async Task<List<KMSStatus>> List(List<long> Ids)
        {
            List<KMSStatus> KMSStatuses = await DataContext.KMSStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new KMSStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return KMSStatuses;
        }

        public async Task<KMSStatus> Get(long Id)
        {
            KMSStatus KMSStatus = await DataContext.KMSStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new KMSStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (KMSStatus == null)
                return null;

            return KMSStatus;
        }
    }
}
