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
    public interface IUnitOfMeasureRepository
    {
        Task<int> Count(UnitOfMeasureFilter UnitOfMeasureFilter);
        Task<List<UnitOfMeasure>> List(UnitOfMeasureFilter UnitOfMeasureFilter);
        Task<UnitOfMeasure> Get(long Id);
    }
    public class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        private DataContext DataContext;
        public UnitOfMeasureRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<UnitOfMeasureDAO> DynamicFilter(IQueryable<UnitOfMeasureDAO> query, UnitOfMeasureFilter filter)
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
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<UnitOfMeasureDAO> OrFilter(IQueryable<UnitOfMeasureDAO> query, UnitOfMeasureFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<UnitOfMeasureDAO> initQuery = query.Where(q => false);
            foreach (UnitOfMeasureFilter UnitOfMeasureFilter in filter.OrFilter)
            {
                IQueryable<UnitOfMeasureDAO> queryable = query;
                if (UnitOfMeasureFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, UnitOfMeasureFilter.Id);
                if (UnitOfMeasureFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, UnitOfMeasureFilter.Code);
                if (UnitOfMeasureFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, UnitOfMeasureFilter.Name);
                if (UnitOfMeasureFilter.Description != null)
                    queryable = queryable.Where(q => q.Description, UnitOfMeasureFilter.Description);
                if (UnitOfMeasureFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, UnitOfMeasureFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<UnitOfMeasureDAO> DynamicOrder(IQueryable<UnitOfMeasureDAO> query, UnitOfMeasureFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case UnitOfMeasureOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case UnitOfMeasureOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case UnitOfMeasureOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case UnitOfMeasureOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case UnitOfMeasureOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case UnitOfMeasureOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case UnitOfMeasureOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case UnitOfMeasureOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case UnitOfMeasureOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case UnitOfMeasureOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case UnitOfMeasureOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case UnitOfMeasureOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<UnitOfMeasure>> DynamicSelect(IQueryable<UnitOfMeasureDAO> query, UnitOfMeasureFilter filter)
        {
            List<UnitOfMeasure> UnitOfMeasures = await query.Select(q => new UnitOfMeasure()
            {
                Id = filter.Selects.Contains(UnitOfMeasureSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(UnitOfMeasureSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(UnitOfMeasureSelect.Name) ? q.Name : default(string),
                Description = filter.Selects.Contains(UnitOfMeasureSelect.Description) ? q.Description : default(string),
                StatusId = filter.Selects.Contains(UnitOfMeasureSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(UnitOfMeasureSelect.Used) ? q.Used : default(bool),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return UnitOfMeasures;
        }

        public async Task<int> Count(UnitOfMeasureFilter filter)
        {
            IQueryable<UnitOfMeasureDAO> UnitOfMeasures = DataContext.UnitOfMeasure.AsNoTracking();
            UnitOfMeasures = DynamicFilter(UnitOfMeasures, filter);
            return await UnitOfMeasures.CountAsync();
        }

        public async Task<List<UnitOfMeasure>> List(UnitOfMeasureFilter filter)
        {
            if (filter == null) return new List<UnitOfMeasure>();
            IQueryable<UnitOfMeasureDAO> UnitOfMeasureDAOs = DataContext.UnitOfMeasure.AsNoTracking();
            UnitOfMeasureDAOs = DynamicFilter(UnitOfMeasureDAOs, filter);
            UnitOfMeasureDAOs = DynamicOrder(UnitOfMeasureDAOs, filter);
            List<UnitOfMeasure> UnitOfMeasures = await DynamicSelect(UnitOfMeasureDAOs, filter);
            return UnitOfMeasures;
        }

        public async Task<UnitOfMeasure> Get(long Id)
        {
            UnitOfMeasure UnitOfMeasure = await DataContext.UnitOfMeasure.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new UnitOfMeasure()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                StatusId = x.StatusId,
                Used = x.Used,
            }).FirstOrDefaultAsync();

            if (UnitOfMeasure == null)
                return null;

            return UnitOfMeasure;
        }
    }
}
