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
    public interface ITaxTypeRepository
    {
        Task<int> Count(TaxTypeFilter TaxTypeFilter);
        Task<List<TaxType>> List(TaxTypeFilter TaxTypeFilter);
        Task<TaxType> Get(long Id);
    }
    public class TaxTypeRepository : ITaxTypeRepository
    {
        private DataContext DataContext;
        public TaxTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TaxTypeDAO> DynamicFilter(IQueryable<TaxTypeDAO> query, TaxTypeFilter filter)
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
            if (filter.Percentage != null)
                query = query.Where(q => q.Percentage, filter.Percentage);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<TaxTypeDAO> OrFilter(IQueryable<TaxTypeDAO> query, TaxTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TaxTypeDAO> initQuery = query.Where(q => false);
            foreach (TaxTypeFilter TaxTypeFilter in filter.OrFilter)
            {
                IQueryable<TaxTypeDAO> queryable = query;
                if (TaxTypeFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TaxTypeFilter.Id);
                if (TaxTypeFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, TaxTypeFilter.Code);
                if (TaxTypeFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, TaxTypeFilter.Name);
                if (TaxTypeFilter.Percentage != null)
                    queryable = queryable.Where(q => q.Percentage, TaxTypeFilter.Percentage);
                if (TaxTypeFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, TaxTypeFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TaxTypeDAO> DynamicOrder(IQueryable<TaxTypeDAO> query, TaxTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TaxTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TaxTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case TaxTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TaxTypeOrder.Percentage:
                            query = query.OrderBy(q => q.Percentage);
                            break;
                        case TaxTypeOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case TaxTypeOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TaxTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TaxTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case TaxTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TaxTypeOrder.Percentage:
                            query = query.OrderByDescending(q => q.Percentage);
                            break;
                        case TaxTypeOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case TaxTypeOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TaxType>> DynamicSelect(IQueryable<TaxTypeDAO> query, TaxTypeFilter filter)
        {
            List<TaxType> TaxTypes = await query.Select(q => new TaxType()
            {
                Id = filter.Selects.Contains(TaxTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(TaxTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(TaxTypeSelect.Name) ? q.Name : default(string),
                Percentage = filter.Selects.Contains(TaxTypeSelect.Percentage) ? q.Percentage : default(decimal),
                StatusId = filter.Selects.Contains(TaxTypeSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(TaxTypeSelect.Used) ? q.Used : default(bool),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TaxTypes;
        }

        public async Task<int> Count(TaxTypeFilter filter)
        {
            IQueryable<TaxTypeDAO> TaxTypes = DataContext.TaxType.AsNoTracking();
            TaxTypes = DynamicFilter(TaxTypes, filter);
            return await TaxTypes.CountAsync();
        }

        public async Task<List<TaxType>> List(TaxTypeFilter filter)
        {
            if (filter == null) return new List<TaxType>();
            IQueryable<TaxTypeDAO> TaxTypeDAOs = DataContext.TaxType.AsNoTracking();
            TaxTypeDAOs = DynamicFilter(TaxTypeDAOs, filter);
            TaxTypeDAOs = DynamicOrder(TaxTypeDAOs, filter);
            List<TaxType> TaxTypes = await DynamicSelect(TaxTypeDAOs, filter);
            return TaxTypes;
        }

        public async Task<TaxType> Get(long Id)
        {
            TaxType TaxType = await DataContext.TaxType.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TaxType()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Percentage = x.Percentage,
                StatusId = x.StatusId,
                Used = x.Used,
            }).FirstOrDefaultAsync();

            if (TaxType == null)
                return null;

            return TaxType;
        }
    }
}
