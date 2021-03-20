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
    public interface ICustomerLeadStatusRepository
    {
        Task<int> Count(CustomerLeadStatusFilter CustomerLeadStatusFilter);
        Task<List<CustomerLeadStatus>> List(CustomerLeadStatusFilter CustomerLeadStatusFilter);
        Task<List<CustomerLeadStatus>> List(List<long> Ids);
        Task<CustomerLeadStatus> Get(long Id);
    }
    public class CustomerLeadStatusRepository : ICustomerLeadStatusRepository
    {
        private DataContext DataContext;
        public CustomerLeadStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerLeadStatusDAO> DynamicFilter(IQueryable<CustomerLeadStatusDAO> query, CustomerLeadStatusFilter filter)
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

        private IQueryable<CustomerLeadStatusDAO> OrFilter(IQueryable<CustomerLeadStatusDAO> query, CustomerLeadStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerLeadStatusDAO> initQuery = query.Where(q => false);
            foreach (CustomerLeadStatusFilter CustomerLeadStatusFilter in filter.OrFilter)
            {
                IQueryable<CustomerLeadStatusDAO> queryable = query;
                if (CustomerLeadStatusFilter.Id != null && CustomerLeadStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerLeadStatusFilter.Id);
                if (CustomerLeadStatusFilter.Code != null && CustomerLeadStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerLeadStatusFilter.Code);
                if (CustomerLeadStatusFilter.Name != null && CustomerLeadStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerLeadStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerLeadStatusDAO> DynamicOrder(IQueryable<CustomerLeadStatusDAO> query, CustomerLeadStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerLeadStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerLeadStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerLeadStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerLeadStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerLeadStatus>> DynamicSelect(IQueryable<CustomerLeadStatusDAO> query, CustomerLeadStatusFilter filter)
        {
            List<CustomerLeadStatus> CustomerLeadStatuses = await query.Select(q => new CustomerLeadStatus()
            {
                Id = filter.Selects.Contains(CustomerLeadStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerLeadStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerLeadStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CustomerLeadStatuses;
        }

        public async Task<int> Count(CustomerLeadStatusFilter filter)
        {
            IQueryable<CustomerLeadStatusDAO> CustomerLeadStatuses = DataContext.CustomerLeadStatus.AsNoTracking();
            CustomerLeadStatuses = DynamicFilter(CustomerLeadStatuses, filter);
            return await CustomerLeadStatuses.CountAsync();
        }

        public async Task<List<CustomerLeadStatus>> List(CustomerLeadStatusFilter filter)
        {
            if (filter == null) return new List<CustomerLeadStatus>();
            IQueryable<CustomerLeadStatusDAO> CustomerLeadStatusDAOs = DataContext.CustomerLeadStatus.AsNoTracking();
            CustomerLeadStatusDAOs = DynamicFilter(CustomerLeadStatusDAOs, filter);
            CustomerLeadStatusDAOs = DynamicOrder(CustomerLeadStatusDAOs, filter);
            List<CustomerLeadStatus> CustomerLeadStatuses = await DynamicSelect(CustomerLeadStatusDAOs, filter);
            return CustomerLeadStatuses;
        }

        public async Task<List<CustomerLeadStatus>> List(List<long> Ids)
        {
            List<CustomerLeadStatus> CustomerLeadStatuses = await DataContext.CustomerLeadStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerLeadStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CustomerLeadStatuses;
        }

        public async Task<CustomerLeadStatus> Get(long Id)
        {
            CustomerLeadStatus CustomerLeadStatus = await DataContext.CustomerLeadStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerLeadStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CustomerLeadStatus == null)
                return null;

            return CustomerLeadStatus;
        }
    }
}
