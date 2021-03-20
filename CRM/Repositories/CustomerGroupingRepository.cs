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
    public interface ICustomerGroupingRepository
    {
        Task<int> Count(CustomerGroupingFilter CustomerGroupingFilter);
        Task<List<CustomerGrouping>> List(CustomerGroupingFilter CustomerGroupingFilter);
        Task<List<CustomerGrouping>> List(List<long> Ids);
        Task<CustomerGrouping> Get(long Id);
        Task<bool> Create(CustomerGrouping CustomerGrouping);
        Task<bool> Update(CustomerGrouping CustomerGrouping);
        Task<bool> Delete(CustomerGrouping CustomerGrouping);
        Task<bool> BulkMerge(List<CustomerGrouping> CustomerGroupings);
        Task<bool> BulkDelete(List<CustomerGrouping> CustomerGroupings);
    }
    public class CustomerGroupingRepository : ICustomerGroupingRepository
    {
        private DataContext DataContext;
        public CustomerGroupingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerGroupingDAO> DynamicFilter(IQueryable<CustomerGroupingDAO> query, CustomerGroupingFilter filter)
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
            if (filter.CustomerTypeId != null && filter.CustomerTypeId.HasValue)
                query = query.Where(q => q.CustomerTypeId, filter.CustomerTypeId);
            if (filter.ParentId != null && filter.ParentId.HasValue)
                query = query.Where(q => q.ParentId.HasValue).Where(q => q.ParentId, filter.ParentId);
            if (filter.Path != null && filter.Path.HasValue)
                query = query.Where(q => q.Path, filter.Path);
            if (filter.Level != null && filter.Level.HasValue)
                query = query.Where(q => q.Level, filter.Level);
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerGroupingDAO> OrFilter(IQueryable<CustomerGroupingDAO> query, CustomerGroupingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerGroupingDAO> initQuery = query.Where(q => false);
            foreach (CustomerGroupingFilter CustomerGroupingFilter in filter.OrFilter)
            {
                IQueryable<CustomerGroupingDAO> queryable = query;
                if (CustomerGroupingFilter.Id != null && CustomerGroupingFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerGroupingFilter.Id);
                if (CustomerGroupingFilter.Code != null && CustomerGroupingFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerGroupingFilter.Code);
                if (CustomerGroupingFilter.Name != null && CustomerGroupingFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerGroupingFilter.Name);
                if (CustomerGroupingFilter.CustomerTypeId != null && CustomerGroupingFilter.CustomerTypeId.HasValue)
                    queryable = queryable.Where(q => q.CustomerTypeId, CustomerGroupingFilter.CustomerTypeId);
                if (CustomerGroupingFilter.ParentId != null && CustomerGroupingFilter.ParentId.HasValue)
                    queryable = queryable.Where(q => q.ParentId.HasValue).Where(q => q.ParentId, CustomerGroupingFilter.ParentId);
                if (CustomerGroupingFilter.Path != null && CustomerGroupingFilter.Path.HasValue)
                    queryable = queryable.Where(q => q.Path, CustomerGroupingFilter.Path);
                if (CustomerGroupingFilter.Level != null && CustomerGroupingFilter.Level.HasValue)
                    queryable = queryable.Where(q => q.Level, CustomerGroupingFilter.Level);
                if (CustomerGroupingFilter.StatusId != null && CustomerGroupingFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, CustomerGroupingFilter.StatusId);
                if (CustomerGroupingFilter.Description != null && CustomerGroupingFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CustomerGroupingFilter.Description);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerGroupingDAO> DynamicOrder(IQueryable<CustomerGroupingDAO> query, CustomerGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerGroupingOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerGroupingOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CustomerGroupingOrder.CustomerType:
                            query = query.OrderBy(q => q.CustomerTypeId);
                            break;
                        case CustomerGroupingOrder.Parent:
                            query = query.OrderBy(q => q.ParentId);
                            break;
                        case CustomerGroupingOrder.Path:
                            query = query.OrderBy(q => q.Path);
                            break;
                        case CustomerGroupingOrder.Level:
                            query = query.OrderBy(q => q.Level);
                            break;
                        case CustomerGroupingOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case CustomerGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerGroupingOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerGroupingOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CustomerGroupingOrder.CustomerType:
                            query = query.OrderByDescending(q => q.CustomerTypeId);
                            break;
                        case CustomerGroupingOrder.Parent:
                            query = query.OrderByDescending(q => q.ParentId);
                            break;
                        case CustomerGroupingOrder.Path:
                            query = query.OrderByDescending(q => q.Path);
                            break;
                        case CustomerGroupingOrder.Level:
                            query = query.OrderByDescending(q => q.Level);
                            break;
                        case CustomerGroupingOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case CustomerGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerGrouping>> DynamicSelect(IQueryable<CustomerGroupingDAO> query, CustomerGroupingFilter filter)
        {
            List<CustomerGrouping> CustomerGroupings = await query.Select(q => new CustomerGrouping()
            {
                Id = filter.Selects.Contains(CustomerGroupingSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerGroupingSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerGroupingSelect.Name) ? q.Name : default(string),
                CustomerTypeId = filter.Selects.Contains(CustomerGroupingSelect.CustomerType) ? q.CustomerTypeId : default(long),
                ParentId = filter.Selects.Contains(CustomerGroupingSelect.Parent) ? q.ParentId : default(long?),
                Path = filter.Selects.Contains(CustomerGroupingSelect.Path) ? q.Path : default(string),
                Level = filter.Selects.Contains(CustomerGroupingSelect.Level) ? q.Level : default(long),
                StatusId = filter.Selects.Contains(CustomerGroupingSelect.Status) ? q.StatusId : default(long),
                Description = filter.Selects.Contains(CustomerGroupingSelect.Description) ? q.Description : default(string),
                CustomerType = filter.Selects.Contains(CustomerGroupingSelect.CustomerType) && q.CustomerType != null ? new CustomerType
                {
                    Id = q.CustomerType.Id,
                    Code = q.CustomerType.Code,
                    Name = q.CustomerType.Name,
                } : null,
                Parent = filter.Selects.Contains(CustomerGroupingSelect.Parent) && q.Parent != null ? new CustomerGrouping
                {
                    Id = q.Parent.Id,
                    Code = q.Parent.Code,
                    Name = q.Parent.Name,
                    CustomerTypeId = q.Parent.CustomerTypeId,
                    ParentId = q.Parent.ParentId,
                    Path = q.Parent.Path,
                    Level = q.Parent.Level,
                    StatusId = q.Parent.StatusId,
                    Description = q.Parent.Description,
                } : null,
                Status = filter.Selects.Contains(CustomerGroupingSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return CustomerGroupings;
        }

        public async Task<int> Count(CustomerGroupingFilter filter)
        {
            IQueryable<CustomerGroupingDAO> CustomerGroupings = DataContext.CustomerGrouping.AsNoTracking();
            CustomerGroupings = DynamicFilter(CustomerGroupings, filter);
            return await CustomerGroupings.CountAsync();
        }

        public async Task<List<CustomerGrouping>> List(CustomerGroupingFilter filter)
        {
            if (filter == null) return new List<CustomerGrouping>();
            IQueryable<CustomerGroupingDAO> CustomerGroupingDAOs = DataContext.CustomerGrouping.AsNoTracking();
            CustomerGroupingDAOs = DynamicFilter(CustomerGroupingDAOs, filter);
            CustomerGroupingDAOs = DynamicOrder(CustomerGroupingDAOs, filter);
            List<CustomerGrouping> CustomerGroupings = await DynamicSelect(CustomerGroupingDAOs, filter);
            return CustomerGroupings;
        }

        public async Task<List<CustomerGrouping>> List(List<long> Ids)
        {
            List<CustomerGrouping> CustomerGroupings = await DataContext.CustomerGrouping.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                CustomerTypeId = x.CustomerTypeId,
                ParentId = x.ParentId,
                Path = x.Path,
                Level = x.Level,
                StatusId = x.StatusId,
                Description = x.Description,
                CustomerType = x.CustomerType == null ? null : new CustomerType
                {
                    Id = x.CustomerType.Id,
                    Code = x.CustomerType.Code,
                    Name = x.CustomerType.Name,
                },
                Parent = x.Parent == null ? null : new CustomerGrouping
                {
                    Id = x.Parent.Id,
                    Code = x.Parent.Code,
                    Name = x.Parent.Name,
                    CustomerTypeId = x.Parent.CustomerTypeId,
                    ParentId = x.Parent.ParentId,
                    Path = x.Parent.Path,
                    Level = x.Parent.Level,
                    StatusId = x.Parent.StatusId,
                    Description = x.Parent.Description,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();
            

            return CustomerGroupings;
        }

        public async Task<CustomerGrouping> Get(long Id)
        {
            CustomerGrouping CustomerGrouping = await DataContext.CustomerGrouping.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                CustomerTypeId = x.CustomerTypeId,
                ParentId = x.ParentId,
                Path = x.Path,
                Level = x.Level,
                StatusId = x.StatusId,
                Description = x.Description,
                CustomerType = x.CustomerType == null ? null : new CustomerType
                {
                    Id = x.CustomerType.Id,
                    Code = x.CustomerType.Code,
                    Name = x.CustomerType.Name,
                },
                Parent = x.Parent == null ? null : new CustomerGrouping
                {
                    Id = x.Parent.Id,
                    Code = x.Parent.Code,
                    Name = x.Parent.Name,
                    CustomerTypeId = x.Parent.CustomerTypeId,
                    ParentId = x.Parent.ParentId,
                    Path = x.Parent.Path,
                    Level = x.Parent.Level,
                    StatusId = x.Parent.StatusId,
                    Description = x.Parent.Description,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (CustomerGrouping == null)
                return null;

            return CustomerGrouping;
        }
        public async Task<bool> Create(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingDAO CustomerGroupingDAO = new CustomerGroupingDAO();
            CustomerGroupingDAO.Id = CustomerGrouping.Id;
            CustomerGroupingDAO.Code = CustomerGrouping.Code;
            CustomerGroupingDAO.Name = CustomerGrouping.Name;
            CustomerGroupingDAO.CustomerTypeId = CustomerGrouping.CustomerTypeId;
            CustomerGroupingDAO.ParentId = CustomerGrouping.ParentId;
            CustomerGroupingDAO.Path = CustomerGrouping.Path;
            CustomerGroupingDAO.Level = CustomerGrouping.Level;
            CustomerGroupingDAO.StatusId = CustomerGrouping.StatusId;
            CustomerGroupingDAO.Description = CustomerGrouping.Description;
            CustomerGroupingDAO.Path = "";
            CustomerGroupingDAO.Level = 1;
            CustomerGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerGrouping.Add(CustomerGroupingDAO);
            await DataContext.SaveChangesAsync();
            CustomerGrouping.Id = CustomerGroupingDAO.Id;
            await SaveReference(CustomerGrouping);
            await BuildPath();
            return true;
        }

        public async Task<bool> Update(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingDAO CustomerGroupingDAO = DataContext.CustomerGrouping.Where(x => x.Id == CustomerGrouping.Id).FirstOrDefault();
            if (CustomerGroupingDAO == null)
                return false;
            CustomerGroupingDAO.Id = CustomerGrouping.Id;
            CustomerGroupingDAO.Code = CustomerGrouping.Code;
            CustomerGroupingDAO.Name = CustomerGrouping.Name;
            CustomerGroupingDAO.CustomerTypeId = CustomerGrouping.CustomerTypeId;
            CustomerGroupingDAO.ParentId = CustomerGrouping.ParentId;
            CustomerGroupingDAO.Path = CustomerGrouping.Path;
            CustomerGroupingDAO.Level = CustomerGrouping.Level;
            CustomerGroupingDAO.StatusId = CustomerGrouping.StatusId;
            CustomerGroupingDAO.Description = CustomerGrouping.Description;
            CustomerGroupingDAO.Path = "";
            CustomerGroupingDAO.Level = 1;
            CustomerGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerGrouping);
            await BuildPath();
            return true;
        }

        public async Task<bool> Delete(CustomerGrouping CustomerGrouping)
        {
            CustomerGroupingDAO CustomerGroupingDAO = await DataContext.CustomerGrouping.Where(x => x.Id == CustomerGrouping.Id).FirstOrDefaultAsync();
            await DataContext.CustomerGrouping.Where(x => x.Path.StartsWith(CustomerGroupingDAO.Id + ".")).UpdateFromQueryAsync(x => new CustomerGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            await DataContext.CustomerGrouping.Where(x => x.Id == CustomerGrouping.Id).UpdateFromQueryAsync(x => new CustomerGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            await BuildPath();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerGrouping> CustomerGroupings)
        {
            List<CustomerGroupingDAO> CustomerGroupingDAOs = new List<CustomerGroupingDAO>();
            foreach (CustomerGrouping CustomerGrouping in CustomerGroupings)
            {
                CustomerGroupingDAO CustomerGroupingDAO = new CustomerGroupingDAO();
                CustomerGroupingDAO.Id = CustomerGrouping.Id;
                CustomerGroupingDAO.Code = CustomerGrouping.Code;
                CustomerGroupingDAO.Name = CustomerGrouping.Name;
                CustomerGroupingDAO.CustomerTypeId = CustomerGrouping.CustomerTypeId;
                CustomerGroupingDAO.ParentId = CustomerGrouping.ParentId;
                CustomerGroupingDAO.Path = CustomerGrouping.Path;
                CustomerGroupingDAO.Level = CustomerGrouping.Level;
                CustomerGroupingDAO.StatusId = CustomerGrouping.StatusId;
                CustomerGroupingDAO.Description = CustomerGrouping.Description;
                CustomerGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerGroupingDAOs.Add(CustomerGroupingDAO);
            }
            await DataContext.BulkMergeAsync(CustomerGroupingDAOs);
            await BuildPath();
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerGrouping> CustomerGroupings)
        {
            List<long> Ids = CustomerGroupings.Select(x => x.Id).ToList();
            await DataContext.CustomerGrouping
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            await BuildPath();
            return true;
        }

        private async Task SaveReference(CustomerGrouping CustomerGrouping)
        {
        }
        
        private async Task BuildPath()
        {
            List<CustomerGroupingDAO> CustomerGroupingDAOs = await DataContext.CustomerGrouping
                .Where(x => x.DeletedAt == null)
                .AsNoTracking().ToListAsync();
            Queue<CustomerGroupingDAO> queue = new Queue<CustomerGroupingDAO>();
            CustomerGroupingDAOs.ForEach(x =>
            {
                if (!x.ParentId.HasValue)
                {
                    x.Path = x.Id + ".";
                    x.Level = 1;
                    queue.Enqueue(x);
                }
            });
            while(queue.Count > 0)
            {
                CustomerGroupingDAO Parent = queue.Dequeue();
                foreach (CustomerGroupingDAO CustomerGroupingDAO in CustomerGroupingDAOs)
                {
                    if (CustomerGroupingDAO.ParentId == Parent.Id)
                    {
                        CustomerGroupingDAO.Path = Parent.Path + CustomerGroupingDAO.Id + ".";
                        CustomerGroupingDAO.Level = Parent.Level + 1;
                        queue.Enqueue(CustomerGroupingDAO);
                    }
                }
            }
            await DataContext.BulkMergeAsync(CustomerGroupingDAOs);
        }
    }
}
