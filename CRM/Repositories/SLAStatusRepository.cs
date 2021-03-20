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
    public interface ISLAStatusRepository
    {
        Task<int> Count(SLAStatusFilter SLAStatusFilter);
        Task<List<SLAStatus>> List(SLAStatusFilter SLAStatusFilter);
        Task<List<SLAStatus>> List(List<long> Ids);
        Task<SLAStatus> Get(long Id);
    }
    public class SLAStatusRepository : ISLAStatusRepository
    {
        private DataContext DataContext;
        public SLAStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAStatusDAO> DynamicFilter(IQueryable<SLAStatusDAO> query, SLAStatusFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.ColorCode != null && filter.ColorCode.HasValue)
                query = query.Where(q => q.ColorCode, filter.ColorCode);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<SLAStatusDAO> OrFilter(IQueryable<SLAStatusDAO> query, SLAStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAStatusDAO> initQuery = query.Where(q => false);
            foreach (SLAStatusFilter SLAStatusFilter in filter.OrFilter)
            {
                IQueryable<SLAStatusDAO> queryable = query;
                if (SLAStatusFilter.Id != null && SLAStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, SLAStatusFilter.Id);
                if (SLAStatusFilter.Code != null && SLAStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, SLAStatusFilter.Code);
                if (SLAStatusFilter.Name != null && SLAStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, SLAStatusFilter.Name);
                if (SLAStatusFilter.ColorCode != null && SLAStatusFilter.ColorCode.HasValue)
                    queryable = queryable.Where(q => q.ColorCode, SLAStatusFilter.ColorCode);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAStatusDAO> DynamicOrder(IQueryable<SLAStatusDAO> query, SLAStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SLAStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case SLAStatusOrder.ColorCode:
                            query = query.OrderBy(q => q.ColorCode);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SLAStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case SLAStatusOrder.ColorCode:
                            query = query.OrderByDescending(q => q.ColorCode);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAStatus>> DynamicSelect(IQueryable<SLAStatusDAO> query, SLAStatusFilter filter)
        {
            List<SLAStatus> SLAStatuses = await query.Select(q => new SLAStatus()
            {
                Id = filter.Selects.Contains(SLAStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(SLAStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SLAStatusSelect.Name) ? q.Name : default(string),
                ColorCode = filter.Selects.Contains(SLAStatusSelect.ColorCode) ? q.ColorCode : default(string),
            }).ToListAsync();
            return SLAStatuses;
        }

        public async Task<int> Count(SLAStatusFilter filter)
        {
            IQueryable<SLAStatusDAO> SLAStatuses = DataContext.SLAStatus.AsNoTracking();
            SLAStatuses = DynamicFilter(SLAStatuses, filter);
            return await SLAStatuses.CountAsync();
        }

        public async Task<List<SLAStatus>> List(SLAStatusFilter filter)
        {
            if (filter == null) return new List<SLAStatus>();
            IQueryable<SLAStatusDAO> SLAStatusDAOs = DataContext.SLAStatus.AsNoTracking();
            SLAStatusDAOs = DynamicFilter(SLAStatusDAOs, filter);
            SLAStatusDAOs = DynamicOrder(SLAStatusDAOs, filter);
            List<SLAStatus> SLAStatuses = await DynamicSelect(SLAStatusDAOs, filter);
            return SLAStatuses;
        }

        public async Task<List<SLAStatus>> List(List<long> Ids)
        {
            List<SLAStatus> SLAStatuses = await DataContext.SLAStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new SLAStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                ColorCode = x.ColorCode,
            }).ToListAsync();
            

            return SLAStatuses;
        }

        public async Task<SLAStatus> Get(long Id)
        {
            SLAStatus SLAStatus = await DataContext.SLAStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new SLAStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                ColorCode = x.ColorCode,
            }).FirstOrDefaultAsync();

            if (SLAStatus == null)
                return null;

            return SLAStatus;
        }
    }
}
