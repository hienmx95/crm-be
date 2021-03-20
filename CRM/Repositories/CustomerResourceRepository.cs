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
    public interface ICustomerResourceRepository
    {
        Task<int> Count(CustomerResourceFilter CustomerResourceFilter);
        Task<List<CustomerResource>> List(CustomerResourceFilter CustomerResourceFilter);
        Task<List<CustomerResource>> List(List<long> Ids);
        Task<CustomerResource> Get(long Id);
        Task<bool> Create(CustomerResource CustomerResource);
        Task<bool> Update(CustomerResource CustomerResource);
        Task<bool> Delete(CustomerResource CustomerResource);
        Task<bool> BulkMerge(List<CustomerResource> CustomerResources);
        Task<bool> BulkDelete(List<CustomerResource> CustomerResources);
    }
    public class CustomerResourceRepository : ICustomerResourceRepository
    {
        private DataContext DataContext;
        public CustomerResourceRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerResourceDAO> DynamicFilter(IQueryable<CustomerResourceDAO> query, CustomerResourceFilter filter)
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
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.RowId != null && filter.RowId.HasValue)
                query = query.Where(q => q.RowId, filter.RowId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerResourceDAO> OrFilter(IQueryable<CustomerResourceDAO> query, CustomerResourceFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerResourceDAO> initQuery = query.Where(q => false);
            foreach (CustomerResourceFilter CustomerResourceFilter in filter.OrFilter)
            {
                IQueryable<CustomerResourceDAO> queryable = query;
                if (CustomerResourceFilter.Id != null && CustomerResourceFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerResourceFilter.Id);
                if (CustomerResourceFilter.Code != null && CustomerResourceFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerResourceFilter.Code);
                if (CustomerResourceFilter.Name != null && CustomerResourceFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerResourceFilter.Name);
                if (CustomerResourceFilter.StatusId != null && CustomerResourceFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, CustomerResourceFilter.StatusId);
                if (CustomerResourceFilter.Description != null && CustomerResourceFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CustomerResourceFilter.Description);
                if (CustomerResourceFilter.RowId != null && CustomerResourceFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, CustomerResourceFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerResourceDAO> DynamicOrder(IQueryable<CustomerResourceDAO> query, CustomerResourceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerResourceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerResourceOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerResourceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CustomerResourceOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case CustomerResourceOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CustomerResourceOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case CustomerResourceOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerResourceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerResourceOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerResourceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CustomerResourceOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case CustomerResourceOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CustomerResourceOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case CustomerResourceOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerResource>> DynamicSelect(IQueryable<CustomerResourceDAO> query, CustomerResourceFilter filter)
        {
            List<CustomerResource> CustomerResources = await query.Select(q => new CustomerResource()
            {
                Id = filter.Selects.Contains(CustomerResourceSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerResourceSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerResourceSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(CustomerResourceSelect.Status) ? q.StatusId : default(long),
                Description = filter.Selects.Contains(CustomerResourceSelect.Description) ? q.Description : default(string),
                Used = filter.Selects.Contains(CustomerResourceSelect.Used) ? q.Used : default(bool),
                RowId = filter.Selects.Contains(CustomerResourceSelect.Row) ? q.RowId : default(Guid),
                Status = filter.Selects.Contains(CustomerResourceSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return CustomerResources;
        }

        public async Task<int> Count(CustomerResourceFilter filter)
        {
            IQueryable<CustomerResourceDAO> CustomerResources = DataContext.CustomerResource.AsNoTracking();
            CustomerResources = DynamicFilter(CustomerResources, filter);
            return await CustomerResources.CountAsync();
        }

        public async Task<List<CustomerResource>> List(CustomerResourceFilter filter)
        {
            if (filter == null) return new List<CustomerResource>();
            IQueryable<CustomerResourceDAO> CustomerResourceDAOs = DataContext.CustomerResource.AsNoTracking();
            CustomerResourceDAOs = DynamicFilter(CustomerResourceDAOs, filter);
            CustomerResourceDAOs = DynamicOrder(CustomerResourceDAOs, filter);
            List<CustomerResource> CustomerResources = await DynamicSelect(CustomerResourceDAOs, filter);
            return CustomerResources;
        }

        public async Task<List<CustomerResource>> List(List<long> Ids)
        {
            List<CustomerResource> CustomerResources = await DataContext.CustomerResource.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerResource()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                Description = x.Description,
                Used = x.Used,
                RowId = x.RowId,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();
            

            return CustomerResources;
        }

        public async Task<CustomerResource> Get(long Id)
        {
            CustomerResource CustomerResource = await DataContext.CustomerResource.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerResource()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                Description = x.Description,
                Used = x.Used,
                RowId = x.RowId,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (CustomerResource == null)
                return null;

            return CustomerResource;
        }
        public async Task<bool> Create(CustomerResource CustomerResource)
        {
            CustomerResourceDAO CustomerResourceDAO = new CustomerResourceDAO();
            CustomerResourceDAO.Id = CustomerResource.Id;
            CustomerResourceDAO.Code = CustomerResource.Code;
            CustomerResourceDAO.Name = CustomerResource.Name;
            CustomerResourceDAO.StatusId = CustomerResource.StatusId;
            CustomerResourceDAO.Description = CustomerResource.Description;
            CustomerResourceDAO.Used = CustomerResource.Used;
            CustomerResourceDAO.RowId = CustomerResource.RowId;
            CustomerResourceDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerResourceDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerResource.Add(CustomerResourceDAO);
            await DataContext.SaveChangesAsync();
            CustomerResource.Id = CustomerResourceDAO.Id;
            await SaveReference(CustomerResource);
            return true;
        }

        public async Task<bool> Update(CustomerResource CustomerResource)
        {
            CustomerResourceDAO CustomerResourceDAO = DataContext.CustomerResource.Where(x => x.Id == CustomerResource.Id).FirstOrDefault();
            if (CustomerResourceDAO == null)
                return false;
            CustomerResourceDAO.Id = CustomerResource.Id;
            CustomerResourceDAO.Code = CustomerResource.Code;
            CustomerResourceDAO.Name = CustomerResource.Name;
            CustomerResourceDAO.StatusId = CustomerResource.StatusId;
            CustomerResourceDAO.Description = CustomerResource.Description;
            CustomerResourceDAO.Used = CustomerResource.Used;
            CustomerResourceDAO.RowId = CustomerResource.RowId;
            CustomerResourceDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerResource);
            return true;
        }

        public async Task<bool> Delete(CustomerResource CustomerResource)
        {
            await DataContext.CustomerResource.Where(x => x.Id == CustomerResource.Id).UpdateFromQueryAsync(x => new CustomerResourceDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerResource> CustomerResources)
        {
            List<CustomerResourceDAO> CustomerResourceDAOs = new List<CustomerResourceDAO>();
            foreach (CustomerResource CustomerResource in CustomerResources)
            {
                CustomerResourceDAO CustomerResourceDAO = new CustomerResourceDAO();
                CustomerResourceDAO.Id = CustomerResource.Id;
                CustomerResourceDAO.Code = CustomerResource.Code;
                CustomerResourceDAO.Name = CustomerResource.Name;
                CustomerResourceDAO.StatusId = CustomerResource.StatusId;
                CustomerResourceDAO.Description = CustomerResource.Description;
                CustomerResourceDAO.Used = CustomerResource.Used;
                CustomerResourceDAO.RowId = CustomerResource.RowId;
                CustomerResourceDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerResourceDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerResourceDAOs.Add(CustomerResourceDAO);
            }
            await DataContext.BulkMergeAsync(CustomerResourceDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerResource> CustomerResources)
        {
            List<long> Ids = CustomerResources.Select(x => x.Id).ToList();
            await DataContext.CustomerResource
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerResourceDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerResource CustomerResource)
        {
        }
        
    }
}
