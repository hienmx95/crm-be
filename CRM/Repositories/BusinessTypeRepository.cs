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
    public interface IBusinessTypeRepository
    {
        Task<int> Count(BusinessTypeFilter BusinessTypeFilter);
        Task<List<BusinessType>> List(BusinessTypeFilter BusinessTypeFilter);
        Task<List<BusinessType>> List(List<long> Ids);
        Task<BusinessType> Get(long Id);
    }
    public class BusinessTypeRepository : IBusinessTypeRepository
    {
        private DataContext DataContext;
        public BusinessTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<BusinessTypeDAO> DynamicFilter(IQueryable<BusinessTypeDAO> query, BusinessTypeFilter filter)
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

        private IQueryable<BusinessTypeDAO> OrFilter(IQueryable<BusinessTypeDAO> query, BusinessTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<BusinessTypeDAO> initQuery = query.Where(q => false);
            foreach (BusinessTypeFilter BusinessTypeFilter in filter.OrFilter)
            {
                IQueryable<BusinessTypeDAO> queryable = query;
                if (BusinessTypeFilter.Id != null && BusinessTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, BusinessTypeFilter.Id);
                if (BusinessTypeFilter.Code != null && BusinessTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, BusinessTypeFilter.Code);
                if (BusinessTypeFilter.Name != null && BusinessTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, BusinessTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<BusinessTypeDAO> DynamicOrder(IQueryable<BusinessTypeDAO> query, BusinessTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case BusinessTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case BusinessTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case BusinessTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case BusinessTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case BusinessTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case BusinessTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<BusinessType>> DynamicSelect(IQueryable<BusinessTypeDAO> query, BusinessTypeFilter filter)
        {
            List<BusinessType> BusinessTypes = await query.Select(q => new BusinessType()
            {
                Id = filter.Selects.Contains(BusinessTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(BusinessTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(BusinessTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return BusinessTypes;
        }

        public async Task<int> Count(BusinessTypeFilter filter)
        {
            IQueryable<BusinessTypeDAO> BusinessTypes = DataContext.BusinessType.AsNoTracking();
            BusinessTypes = DynamicFilter(BusinessTypes, filter);
            return await BusinessTypes.CountAsync();
        }

        public async Task<List<BusinessType>> List(BusinessTypeFilter filter)
        {
            if (filter == null) return new List<BusinessType>();
            IQueryable<BusinessTypeDAO> BusinessTypeDAOs = DataContext.BusinessType.AsNoTracking();
            BusinessTypeDAOs = DynamicFilter(BusinessTypeDAOs, filter);
            BusinessTypeDAOs = DynamicOrder(BusinessTypeDAOs, filter);
            List<BusinessType> BusinessTypes = await DynamicSelect(BusinessTypeDAOs, filter);
            return BusinessTypes;
        }

        public async Task<List<BusinessType>> List(List<long> Ids)
        {
            List<BusinessType> BusinessTypes = await DataContext.BusinessType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new BusinessType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return BusinessTypes;
        }

        public async Task<BusinessType> Get(long Id)
        {
            BusinessType BusinessType = await DataContext.BusinessType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new BusinessType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (BusinessType == null)
                return null;

            return BusinessType;
        }
    }
}
