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
    public interface ICustomerTypeRepository
    {
        Task<int> Count(CustomerTypeFilter CustomerTypeFilter);
        Task<List<CustomerType>> List(CustomerTypeFilter CustomerTypeFilter);
        Task<CustomerType> Get(long Id);
    }
    public class CustomerTypeRepository : ICustomerTypeRepository
    {
        private DataContext DataContext;
        public CustomerTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerTypeDAO> DynamicFilter(IQueryable<CustomerTypeDAO> query, CustomerTypeFilter filter)
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

        private IQueryable<CustomerTypeDAO> OrFilter(IQueryable<CustomerTypeDAO> query, CustomerTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerTypeDAO> initQuery = query.Where(q => false);
            foreach (CustomerTypeFilter CustomerTypeFilter in filter.OrFilter)
            {
                IQueryable<CustomerTypeDAO> queryable = query;
                if (CustomerTypeFilter.Id != null && CustomerTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerTypeFilter.Id);
                if (CustomerTypeFilter.Code != null && CustomerTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerTypeFilter.Code);
                if (CustomerTypeFilter.Name != null && CustomerTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerTypeDAO> DynamicOrder(IQueryable<CustomerTypeDAO> query, CustomerTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerType>> DynamicSelect(IQueryable<CustomerTypeDAO> query, CustomerTypeFilter filter)
        {
            List<CustomerType> CustomerTypes = await query.Select(q => new CustomerType()
            {
                Id = filter.Selects.Contains(CustomerTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CustomerTypes;
        }

        public async Task<int> Count(CustomerTypeFilter filter)
        {
            IQueryable<CustomerTypeDAO> CustomerTypes = DataContext.CustomerType.AsNoTracking();
            CustomerTypes = DynamicFilter(CustomerTypes, filter);
            return await CustomerTypes.CountAsync();
        }

        public async Task<List<CustomerType>> List(CustomerTypeFilter filter)
        {
            if (filter == null) return new List<CustomerType>();
            IQueryable<CustomerTypeDAO> CustomerTypeDAOs = DataContext.CustomerType.AsNoTracking();
            CustomerTypeDAOs = DynamicFilter(CustomerTypeDAOs, filter);
            CustomerTypeDAOs = DynamicOrder(CustomerTypeDAOs, filter);
            List<CustomerType> CustomerTypes = await DynamicSelect(CustomerTypeDAOs, filter);
            return CustomerTypes;
        }

        public async Task<List<CustomerType>> List(List<long> Ids)
        {
            List<CustomerType> CustomerTypes = await DataContext.CustomerType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CustomerTypes;
        }

        public async Task<CustomerType> Get(long Id)
        {
            CustomerType CustomerType = await DataContext.CustomerType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CustomerType == null)
                return null;

            return CustomerType;
        }
    }
}
