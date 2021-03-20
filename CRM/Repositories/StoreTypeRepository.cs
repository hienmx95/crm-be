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
    public interface IStoreTypeRepository
    {
        Task<int> Count(StoreTypeFilter StoreTypeFilter);
        Task<List<StoreType>> List(StoreTypeFilter StoreTypeFilter);
        Task<StoreType> Get(long Id);
    }
    public class StoreTypeRepository : IStoreTypeRepository
    {
        private DataContext DataContext;
        public StoreTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<StoreTypeDAO> DynamicFilter(IQueryable<StoreTypeDAO> query, StoreTypeFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.ColorId != null)
                query = query.Where(q => q.ColorId.HasValue).Where(q => q.ColorId, filter.ColorId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<StoreTypeDAO> OrFilter(IQueryable<StoreTypeDAO> query, StoreTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<StoreTypeDAO> initQuery = query.Where(q => false);
            foreach (StoreTypeFilter StoreTypeFilter in filter.OrFilter)
            {
                IQueryable<StoreTypeDAO> queryable = query;
                if (StoreTypeFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, StoreTypeFilter.Id);
                if (StoreTypeFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, StoreTypeFilter.Code);
                if (StoreTypeFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, StoreTypeFilter.Name);
                if (StoreTypeFilter.ColorId != null)
                    queryable = queryable.Where(q => q.ColorId.HasValue).Where(q => q.ColorId, StoreTypeFilter.ColorId);
                if (StoreTypeFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, StoreTypeFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<StoreTypeDAO> DynamicOrder(IQueryable<StoreTypeDAO> query, StoreTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case StoreTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case StoreTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case StoreTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case StoreTypeOrder.Color:
                            query = query.OrderBy(q => q.ColorId);
                            break;
                        case StoreTypeOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case StoreTypeOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case StoreTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case StoreTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case StoreTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case StoreTypeOrder.Color:
                            query = query.OrderByDescending(q => q.ColorId);
                            break;
                        case StoreTypeOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case StoreTypeOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<StoreType>> DynamicSelect(IQueryable<StoreTypeDAO> query, StoreTypeFilter filter)
        {
            List<StoreType> StoreTypes = await query.Select(q => new StoreType()
            {
                Id = filter.Selects.Contains(StoreTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(StoreTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(StoreTypeSelect.Name) ? q.Name : default(string),
                ColorId = filter.Selects.Contains(StoreTypeSelect.Color) ? q.ColorId : default(long?),
                StatusId = filter.Selects.Contains(StoreTypeSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(StoreTypeSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(StoreTypeSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return StoreTypes;
        }

        public async Task<int> Count(StoreTypeFilter filter)
        {
            IQueryable<StoreTypeDAO> StoreTypes = DataContext.StoreType.AsNoTracking();
            StoreTypes = DynamicFilter(StoreTypes, filter);
            return await StoreTypes.CountAsync();
        }

        public async Task<List<StoreType>> List(StoreTypeFilter filter)
        {
            if (filter == null) return new List<StoreType>();
            IQueryable<StoreTypeDAO> StoreTypeDAOs = DataContext.StoreType.AsNoTracking();
            StoreTypeDAOs = DynamicFilter(StoreTypeDAOs, filter);
            StoreTypeDAOs = DynamicOrder(StoreTypeDAOs, filter);
            List<StoreType> StoreTypes = await DynamicSelect(StoreTypeDAOs, filter);
            return StoreTypes;
        }

        public async Task<StoreType> Get(long Id)
        {
            StoreType StoreType = await DataContext.StoreType.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new StoreType()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                ColorId = x.ColorId,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (StoreType == null)
                return null;

            return StoreType;
        }
    }
}
