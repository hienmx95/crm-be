using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public interface INationRepository
    {
        Task<int> Count(NationFilter NationFilter);
        Task<List<Nation>> List(NationFilter NationFilter);
        Task<Nation> Get(long Id);
    }
    public class NationRepository : INationRepository
    {
        private DataContext DataContext;
        public NationRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<NationDAO> DynamicFilter(IQueryable<NationDAO> query, NationFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null && filter.CreatedAt.HasValue)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null && filter.UpdatedAt.HasValue)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Priority != null && filter.Priority.HasValue)
                query = query.Where(q => q.Priority.HasValue).Where(q => q.Priority, filter.Priority);
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<NationDAO> OrFilter(IQueryable<NationDAO> query, NationFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<NationDAO> initQuery = query.Where(q => false);
            foreach (NationFilter NationFilter in filter.OrFilter)
            {
                IQueryable<NationDAO> queryable = query;
                if (NationFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, NationFilter.Id);
                if (NationFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, NationFilter.Code);
                if (NationFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, NationFilter.Name);
                if (NationFilter.Priority != null)
                    queryable = queryable.Where(q => q.Priority.HasValue).Where(q => q.Priority, NationFilter.Priority);
                if (NationFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, NationFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<NationDAO> DynamicOrder(IQueryable<NationDAO> query, NationFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case NationOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case NationOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case NationOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case NationOrder.Priority:
                            query = query.OrderBy(q => q.Priority);
                            break;
                        case NationOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case NationOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case NationOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case NationOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case NationOrder.Priority:
                            query = query.OrderByDescending(q => q.Priority);
                            break;
                        case NationOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Nation>> DynamicSelect(IQueryable<NationDAO> query, NationFilter filter)
        {
            List<Nation> Nations = await query.Select(q => new Nation()
            {
                Id = filter.Selects.Contains(NationSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(NationSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(NationSelect.Name) ? q.Name : default(string),
                Priority = filter.Selects.Contains(NationSelect.Priority) ? q.Priority : default(long?),
                StatusId = filter.Selects.Contains(NationSelect.Status) ? q.StatusId : default(long),
                Status = filter.Selects.Contains(NationSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return Nations;
        }

        public async Task<int> Count(NationFilter filter)
        {
            IQueryable<NationDAO> Nations = DataContext.Nation.AsNoTracking();
            Nations = DynamicFilter(Nations, filter);
            return await Nations.CountAsync();
        }

        public async Task<List<Nation>> List(NationFilter filter)
        {
            if (filter == null) return new List<Nation>();
            IQueryable<NationDAO> NationDAOs = DataContext.Nation.AsNoTracking();
            NationDAOs = DynamicFilter(NationDAOs, filter);
            NationDAOs = DynamicOrder(NationDAOs, filter);
            List<Nation> Nations = await DynamicSelect(NationDAOs, filter);
            return Nations;
        }

        public async Task<Nation> Get(long Id)
        {
            Nation Nation = await DataContext.Nation.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new Nation()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Priority = x.Priority,
                StatusId = x.StatusId,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (Nation == null)
                return null;

            return Nation;
        }
    }
}
