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
    public interface ICustomerFeedbackTypeRepository
    {
        Task<int> Count(CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter);
        Task<List<CustomerFeedbackType>> List(CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter);
        Task<List<CustomerFeedbackType>> List(List<long> Ids);
        Task<CustomerFeedbackType> Get(long Id);
    }
    public class CustomerFeedbackTypeRepository : ICustomerFeedbackTypeRepository
    {
        private DataContext DataContext;
        public CustomerFeedbackTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerFeedbackTypeDAO> DynamicFilter(IQueryable<CustomerFeedbackTypeDAO> query, CustomerFeedbackTypeFilter filter)
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

        private IQueryable<CustomerFeedbackTypeDAO> OrFilter(IQueryable<CustomerFeedbackTypeDAO> query, CustomerFeedbackTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerFeedbackTypeDAO> initQuery = query.Where(q => false);
            foreach (CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter in filter.OrFilter)
            {
                IQueryable<CustomerFeedbackTypeDAO> queryable = query;
                if (CustomerFeedbackTypeFilter.Id != null && CustomerFeedbackTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerFeedbackTypeFilter.Id);
                if (CustomerFeedbackTypeFilter.Code != null && CustomerFeedbackTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerFeedbackTypeFilter.Code);
                if (CustomerFeedbackTypeFilter.Name != null && CustomerFeedbackTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerFeedbackTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerFeedbackTypeDAO> DynamicOrder(IQueryable<CustomerFeedbackTypeDAO> query, CustomerFeedbackTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerFeedbackTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerFeedbackTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerFeedbackTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerFeedbackTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerFeedbackTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerFeedbackTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerFeedbackType>> DynamicSelect(IQueryable<CustomerFeedbackTypeDAO> query, CustomerFeedbackTypeFilter filter)
        {
            List<CustomerFeedbackType> CustomerFeedbackTypes = await query.Select(q => new CustomerFeedbackType()
            {
                Id = filter.Selects.Contains(CustomerFeedbackTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerFeedbackTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerFeedbackTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CustomerFeedbackTypes;
        }

        public async Task<int> Count(CustomerFeedbackTypeFilter filter)
        {
            IQueryable<CustomerFeedbackTypeDAO> CustomerFeedbackTypes = DataContext.CustomerFeedbackType.AsNoTracking();
            CustomerFeedbackTypes = DynamicFilter(CustomerFeedbackTypes, filter);
            return await CustomerFeedbackTypes.CountAsync();
        }

        public async Task<List<CustomerFeedbackType>> List(CustomerFeedbackTypeFilter filter)
        {
            if (filter == null) return new List<CustomerFeedbackType>();
            IQueryable<CustomerFeedbackTypeDAO> CustomerFeedbackTypeDAOs = DataContext.CustomerFeedbackType.AsNoTracking();
            CustomerFeedbackTypeDAOs = DynamicFilter(CustomerFeedbackTypeDAOs, filter);
            CustomerFeedbackTypeDAOs = DynamicOrder(CustomerFeedbackTypeDAOs, filter);
            List<CustomerFeedbackType> CustomerFeedbackTypes = await DynamicSelect(CustomerFeedbackTypeDAOs, filter);
            return CustomerFeedbackTypes;
        }

        public async Task<List<CustomerFeedbackType>> List(List<long> Ids)
        {
            List<CustomerFeedbackType> CustomerFeedbackTypes = await DataContext.CustomerFeedbackType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerFeedbackType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CustomerFeedbackTypes;
        }

        public async Task<CustomerFeedbackType> Get(long Id)
        {
            CustomerFeedbackType CustomerFeedbackType = await DataContext.CustomerFeedbackType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerFeedbackType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CustomerFeedbackType == null)
                return null;

            return CustomerFeedbackType;
        }
    }
}
