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
    public interface ICustomerLeadLevelRepository
    {
        Task<int> Count(CustomerLeadLevelFilter CustomerLeadLevelFilter);
        Task<List<CustomerLeadLevel>> List(CustomerLeadLevelFilter CustomerLeadLevelFilter);
        Task<List<CustomerLeadLevel>> List(List<long> Ids);
        Task<CustomerLeadLevel> Get(long Id);
    }
    public class CustomerLeadLevelRepository : ICustomerLeadLevelRepository
    {
        private DataContext DataContext;
        public CustomerLeadLevelRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerLeadLevelDAO> DynamicFilter(IQueryable<CustomerLeadLevelDAO> query, CustomerLeadLevelFilter filter)
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

        private IQueryable<CustomerLeadLevelDAO> OrFilter(IQueryable<CustomerLeadLevelDAO> query, CustomerLeadLevelFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerLeadLevelDAO> initQuery = query.Where(q => false);
            foreach (CustomerLeadLevelFilter CustomerLeadLevelFilter in filter.OrFilter)
            {
                IQueryable<CustomerLeadLevelDAO> queryable = query;
                if (CustomerLeadLevelFilter.Id != null && CustomerLeadLevelFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerLeadLevelFilter.Id);
                if (CustomerLeadLevelFilter.Code != null && CustomerLeadLevelFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerLeadLevelFilter.Code);
                if (CustomerLeadLevelFilter.Name != null && CustomerLeadLevelFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerLeadLevelFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerLeadLevelDAO> DynamicOrder(IQueryable<CustomerLeadLevelDAO> query, CustomerLeadLevelFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadLevelOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerLeadLevelOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerLeadLevelOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadLevelOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerLeadLevelOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerLeadLevelOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerLeadLevel>> DynamicSelect(IQueryable<CustomerLeadLevelDAO> query, CustomerLeadLevelFilter filter)
        {
            List<CustomerLeadLevel> CustomerLeadLevels = await query.Select(q => new CustomerLeadLevel()
            {
                Id = filter.Selects.Contains(CustomerLeadLevelSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerLeadLevelSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerLeadLevelSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CustomerLeadLevels;
        }

        public async Task<int> Count(CustomerLeadLevelFilter filter)
        {
            IQueryable<CustomerLeadLevelDAO> CustomerLeadLevels = DataContext.CustomerLeadLevel.AsNoTracking();
            CustomerLeadLevels = DynamicFilter(CustomerLeadLevels, filter);
            return await CustomerLeadLevels.CountAsync();
        }

        public async Task<List<CustomerLeadLevel>> List(CustomerLeadLevelFilter filter)
        {
            if (filter == null) return new List<CustomerLeadLevel>();
            IQueryable<CustomerLeadLevelDAO> CustomerLeadLevelDAOs = DataContext.CustomerLeadLevel.AsNoTracking();
            CustomerLeadLevelDAOs = DynamicFilter(CustomerLeadLevelDAOs, filter);
            CustomerLeadLevelDAOs = DynamicOrder(CustomerLeadLevelDAOs, filter);
            List<CustomerLeadLevel> CustomerLeadLevels = await DynamicSelect(CustomerLeadLevelDAOs, filter);
            return CustomerLeadLevels;
        }

        public async Task<List<CustomerLeadLevel>> List(List<long> Ids)
        {
            List<CustomerLeadLevel> CustomerLeadLevels = await DataContext.CustomerLeadLevel.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerLeadLevel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CustomerLeadLevels;
        }

        public async Task<CustomerLeadLevel> Get(long Id)
        {
            CustomerLeadLevel CustomerLeadLevel = await DataContext.CustomerLeadLevel.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerLeadLevel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CustomerLeadLevel == null)
                return null;

            return CustomerLeadLevel;
        }
    }
}
