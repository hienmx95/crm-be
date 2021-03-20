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
    public interface ICustomerLevelRepository
    {
        Task<int> Count(CustomerLevelFilter CustomerLevelFilter);
        Task<List<CustomerLevel>> List(CustomerLevelFilter CustomerLevelFilter);
        Task<List<CustomerLevel>> List(List<long> Ids);
        Task<CustomerLevel> Get(long Id);
        Task<bool> Create(CustomerLevel CustomerLevel);
        Task<bool> Update(CustomerLevel CustomerLevel);
        Task<bool> Delete(CustomerLevel CustomerLevel);
        Task<bool> BulkMerge(List<CustomerLevel> CustomerLevels);
        Task<bool> BulkDelete(List<CustomerLevel> CustomerLevels);
    }
    public class CustomerLevelRepository : ICustomerLevelRepository
    {
        private DataContext DataContext;
        public CustomerLevelRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerLevelDAO> DynamicFilter(IQueryable<CustomerLevelDAO> query, CustomerLevelFilter filter)
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
            if (filter.Color != null && filter.Color.HasValue)
                query = query.Where(q => q.Color, filter.Color);
            if (filter.PointFrom != null && filter.PointFrom.HasValue)
                query = query.Where(q => q.PointFrom, filter.PointFrom);
            if (filter.PointTo != null && filter.PointTo.HasValue)
                query = query.Where(q => q.PointTo, filter.PointTo);
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.RowId != null && filter.RowId.HasValue)
                query = query.Where(q => q.RowId, filter.RowId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerLevelDAO> OrFilter(IQueryable<CustomerLevelDAO> query, CustomerLevelFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerLevelDAO> initQuery = query.Where(q => false);
            foreach (CustomerLevelFilter CustomerLevelFilter in filter.OrFilter)
            {
                IQueryable<CustomerLevelDAO> queryable = query;
                if (CustomerLevelFilter.Id != null && CustomerLevelFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerLevelFilter.Id);
                if (CustomerLevelFilter.Code != null && CustomerLevelFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerLevelFilter.Code);
                if (CustomerLevelFilter.Name != null && CustomerLevelFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerLevelFilter.Name);
                if (CustomerLevelFilter.Color != null && CustomerLevelFilter.Color.HasValue)
                    queryable = queryable.Where(q => q.Color, CustomerLevelFilter.Color);
                if (CustomerLevelFilter.PointFrom != null && CustomerLevelFilter.PointFrom.HasValue)
                    queryable = queryable.Where(q => q.PointFrom, CustomerLevelFilter.PointFrom);
                if (CustomerLevelFilter.PointTo != null && CustomerLevelFilter.PointTo.HasValue)
                    queryable = queryable.Where(q => q.PointTo, CustomerLevelFilter.PointTo);
                if (CustomerLevelFilter.StatusId != null && CustomerLevelFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, CustomerLevelFilter.StatusId);
                if (CustomerLevelFilter.Description != null && CustomerLevelFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CustomerLevelFilter.Description);
                if (CustomerLevelFilter.RowId != null && CustomerLevelFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, CustomerLevelFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerLevelDAO> DynamicOrder(IQueryable<CustomerLevelDAO> query, CustomerLevelFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLevelOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerLevelOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerLevelOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CustomerLevelOrder.Color:
                            query = query.OrderBy(q => q.Color);
                            break;
                        case CustomerLevelOrder.PointFrom:
                            query = query.OrderBy(q => q.PointFrom);
                            break;
                        case CustomerLevelOrder.PointTo:
                            query = query.OrderBy(q => q.PointTo);
                            break;
                        case CustomerLevelOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case CustomerLevelOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CustomerLevelOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case CustomerLevelOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLevelOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerLevelOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerLevelOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CustomerLevelOrder.Color:
                            query = query.OrderByDescending(q => q.Color);
                            break;
                        case CustomerLevelOrder.PointFrom:
                            query = query.OrderByDescending(q => q.PointFrom);
                            break;
                        case CustomerLevelOrder.PointTo:
                            query = query.OrderByDescending(q => q.PointTo);
                            break;
                        case CustomerLevelOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case CustomerLevelOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CustomerLevelOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case CustomerLevelOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerLevel>> DynamicSelect(IQueryable<CustomerLevelDAO> query, CustomerLevelFilter filter)
        {
            List<CustomerLevel> CustomerLevels = await query.Select(q => new CustomerLevel()
            {
                Id = filter.Selects.Contains(CustomerLevelSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerLevelSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerLevelSelect.Name) ? q.Name : default(string),
                Color = filter.Selects.Contains(CustomerLevelSelect.Color) ? q.Color : default(string),
                PointFrom = filter.Selects.Contains(CustomerLevelSelect.PointFrom) ? q.PointFrom : default(long),
                PointTo = filter.Selects.Contains(CustomerLevelSelect.PointTo) ? q.PointTo : default(long),
                StatusId = filter.Selects.Contains(CustomerLevelSelect.Status) ? q.StatusId : default(long),
                Description = filter.Selects.Contains(CustomerLevelSelect.Description) ? q.Description : default(string),
                Used = filter.Selects.Contains(CustomerLevelSelect.Used) ? q.Used : default(bool),
                RowId = filter.Selects.Contains(CustomerLevelSelect.Row) ? q.RowId : default(Guid),
                Status = filter.Selects.Contains(CustomerLevelSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return CustomerLevels;
        }

        public async Task<int> Count(CustomerLevelFilter filter)
        {
            IQueryable<CustomerLevelDAO> CustomerLevels = DataContext.CustomerLevel.AsNoTracking();
            CustomerLevels = DynamicFilter(CustomerLevels, filter);
            return await CustomerLevels.CountAsync();
        }

        public async Task<List<CustomerLevel>> List(CustomerLevelFilter filter)
        {
            if (filter == null) return new List<CustomerLevel>();
            IQueryable<CustomerLevelDAO> CustomerLevelDAOs = DataContext.CustomerLevel.AsNoTracking();
            CustomerLevelDAOs = DynamicFilter(CustomerLevelDAOs, filter);
            CustomerLevelDAOs = DynamicOrder(CustomerLevelDAOs, filter);
            List<CustomerLevel> CustomerLevels = await DynamicSelect(CustomerLevelDAOs, filter);
            return CustomerLevels;
        }

        public async Task<List<CustomerLevel>> List(List<long> Ids)
        {
            List<CustomerLevel> CustomerLevels = await DataContext.CustomerLevel.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerLevel()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Color = x.Color,
                PointFrom = x.PointFrom,
                PointTo = x.PointTo,
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
            

            return CustomerLevels;
        }

        public async Task<CustomerLevel> Get(long Id)
        {
            CustomerLevel CustomerLevel = await DataContext.CustomerLevel.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerLevel()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Color = x.Color,
                PointFrom = x.PointFrom,
                PointTo = x.PointTo,
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

            if (CustomerLevel == null)
                return null;

            return CustomerLevel;
        }
        public async Task<bool> Create(CustomerLevel CustomerLevel)
        {
            CustomerLevelDAO CustomerLevelDAO = new CustomerLevelDAO();
            CustomerLevelDAO.Id = CustomerLevel.Id;
            CustomerLevelDAO.Code = CustomerLevel.Code;
            CustomerLevelDAO.Name = CustomerLevel.Name;
            CustomerLevelDAO.Color = CustomerLevel.Color;
            CustomerLevelDAO.PointFrom = CustomerLevel.PointFrom;
            CustomerLevelDAO.PointTo = CustomerLevel.PointTo;
            CustomerLevelDAO.StatusId = CustomerLevel.StatusId;
            CustomerLevelDAO.Description = CustomerLevel.Description;
            CustomerLevelDAO.Used = CustomerLevel.Used;
            CustomerLevelDAO.RowId = CustomerLevel.RowId;
            CustomerLevelDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerLevelDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerLevel.Add(CustomerLevelDAO);
            await DataContext.SaveChangesAsync();
            CustomerLevel.Id = CustomerLevelDAO.Id;
            await SaveReference(CustomerLevel);
            return true;
        }

        public async Task<bool> Update(CustomerLevel CustomerLevel)
        {
            CustomerLevelDAO CustomerLevelDAO = DataContext.CustomerLevel.Where(x => x.Id == CustomerLevel.Id).FirstOrDefault();
            if (CustomerLevelDAO == null)
                return false;
            CustomerLevelDAO.Id = CustomerLevel.Id;
            CustomerLevelDAO.Code = CustomerLevel.Code;
            CustomerLevelDAO.Name = CustomerLevel.Name;
            CustomerLevelDAO.Color = CustomerLevel.Color;
            CustomerLevelDAO.PointFrom = CustomerLevel.PointFrom;
            CustomerLevelDAO.PointTo = CustomerLevel.PointTo;
            CustomerLevelDAO.StatusId = CustomerLevel.StatusId;
            CustomerLevelDAO.Description = CustomerLevel.Description;
            CustomerLevelDAO.Used = CustomerLevel.Used;
            CustomerLevelDAO.RowId = CustomerLevel.RowId;
            CustomerLevelDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerLevel);
            return true;
        }

        public async Task<bool> Delete(CustomerLevel CustomerLevel)
        {
            await DataContext.CustomerLevel.Where(x => x.Id == CustomerLevel.Id).UpdateFromQueryAsync(x => new CustomerLevelDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerLevel> CustomerLevels)
        {
            List<CustomerLevelDAO> CustomerLevelDAOs = new List<CustomerLevelDAO>();
            foreach (CustomerLevel CustomerLevel in CustomerLevels)
            {
                CustomerLevelDAO CustomerLevelDAO = new CustomerLevelDAO();
                CustomerLevelDAO.Id = CustomerLevel.Id;
                CustomerLevelDAO.Code = CustomerLevel.Code;
                CustomerLevelDAO.Name = CustomerLevel.Name;
                CustomerLevelDAO.Color = CustomerLevel.Color;
                CustomerLevelDAO.PointFrom = CustomerLevel.PointFrom;
                CustomerLevelDAO.PointTo = CustomerLevel.PointTo;
                CustomerLevelDAO.StatusId = CustomerLevel.StatusId;
                CustomerLevelDAO.Description = CustomerLevel.Description;
                CustomerLevelDAO.Used = CustomerLevel.Used;
                CustomerLevelDAO.RowId = CustomerLevel.RowId;
                CustomerLevelDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerLevelDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerLevelDAOs.Add(CustomerLevelDAO);
            }
            await DataContext.BulkMergeAsync(CustomerLevelDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerLevel> CustomerLevels)
        {
            List<long> Ids = CustomerLevels.Select(x => x.Id).ToList();
            await DataContext.CustomerLevel
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerLevelDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerLevel CustomerLevel)
        {
        }
        
    }
}
