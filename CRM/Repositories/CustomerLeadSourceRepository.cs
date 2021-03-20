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
    public interface ICustomerLeadSourceRepository
    {
        Task<int> Count(CustomerLeadSourceFilter CustomerLeadSourceFilter);
        Task<List<CustomerLeadSource>> List(CustomerLeadSourceFilter CustomerLeadSourceFilter);
        Task<List<CustomerLeadSource>> List(List<long> Ids);
        Task<CustomerLeadSource> Get(long Id);
    }
    public class CustomerLeadSourceRepository : ICustomerLeadSourceRepository
    {
        private DataContext DataContext;
        public CustomerLeadSourceRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerLeadSourceDAO> DynamicFilter(IQueryable<CustomerLeadSourceDAO> query, CustomerLeadSourceFilter filter)
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

        private IQueryable<CustomerLeadSourceDAO> OrFilter(IQueryable<CustomerLeadSourceDAO> query, CustomerLeadSourceFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerLeadSourceDAO> initQuery = query.Where(q => false);
            foreach (CustomerLeadSourceFilter CustomerLeadSourceFilter in filter.OrFilter)
            {
                IQueryable<CustomerLeadSourceDAO> queryable = query;
                if (CustomerLeadSourceFilter.Id != null && CustomerLeadSourceFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerLeadSourceFilter.Id);
                if (CustomerLeadSourceFilter.Code != null && CustomerLeadSourceFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerLeadSourceFilter.Code);
                if (CustomerLeadSourceFilter.Name != null && CustomerLeadSourceFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerLeadSourceFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerLeadSourceDAO> DynamicOrder(IQueryable<CustomerLeadSourceDAO> query, CustomerLeadSourceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadSourceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerLeadSourceOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerLeadSourceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadSourceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerLeadSourceOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerLeadSourceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerLeadSource>> DynamicSelect(IQueryable<CustomerLeadSourceDAO> query, CustomerLeadSourceFilter filter)
        {
            List<CustomerLeadSource> CustomerLeadSources = await query.Select(q => new CustomerLeadSource()
            {
                Id = filter.Selects.Contains(CustomerLeadSourceSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerLeadSourceSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerLeadSourceSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CustomerLeadSources;
        }

        public async Task<int> Count(CustomerLeadSourceFilter filter)
        {
            IQueryable<CustomerLeadSourceDAO> CustomerLeadSources = DataContext.CustomerLeadSource.AsNoTracking();
            CustomerLeadSources = DynamicFilter(CustomerLeadSources, filter);
            return await CustomerLeadSources.CountAsync();
        }

        public async Task<List<CustomerLeadSource>> List(CustomerLeadSourceFilter filter)
        {
            if (filter == null) return new List<CustomerLeadSource>();
            IQueryable<CustomerLeadSourceDAO> CustomerLeadSourceDAOs = DataContext.CustomerLeadSource.AsNoTracking();
            CustomerLeadSourceDAOs = DynamicFilter(CustomerLeadSourceDAOs, filter);
            CustomerLeadSourceDAOs = DynamicOrder(CustomerLeadSourceDAOs, filter);
            List<CustomerLeadSource> CustomerLeadSources = await DynamicSelect(CustomerLeadSourceDAOs, filter);
            return CustomerLeadSources;
        }

        public async Task<List<CustomerLeadSource>> List(List<long> Ids)
        {
            List<CustomerLeadSource> CustomerLeadSources = await DataContext.CustomerLeadSource.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerLeadSource()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CustomerLeadSources;
        }

        public async Task<CustomerLeadSource> Get(long Id)
        {
            CustomerLeadSource CustomerLeadSource = await DataContext.CustomerLeadSource.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerLeadSource()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CustomerLeadSource == null)
                return null;

            return CustomerLeadSource;
        }
    }
}
