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
    public interface ICustomerPointHistoryRepository
    {
        Task<int> Count(CustomerPointHistoryFilter CustomerPointHistoryFilter);
        Task<List<CustomerPointHistory>> List(CustomerPointHistoryFilter CustomerPointHistoryFilter);
        Task<List<CustomerPointHistory>> List(List<long> Ids);
        Task<CustomerPointHistory> Get(long Id);
        Task<bool> Create(CustomerPointHistory CustomerPointHistory);
        Task<bool> Update(CustomerPointHistory CustomerPointHistory);
        Task<bool> Delete(CustomerPointHistory CustomerPointHistory);
        Task<bool> BulkMerge(List<CustomerPointHistory> CustomerPointHistories);
        Task<bool> BulkDelete(List<CustomerPointHistory> CustomerPointHistories);
    }
    public class CustomerPointHistoryRepository : ICustomerPointHistoryRepository
    {
        private DataContext DataContext;
        public CustomerPointHistoryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerPointHistoryDAO> DynamicFilter(IQueryable<CustomerPointHistoryDAO> query, CustomerPointHistoryFilter filter)
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
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.TotalPoint != null && filter.TotalPoint.HasValue)
                query = query.Where(q => q.TotalPoint, filter.TotalPoint);
            if (filter.CurrentPoint != null && filter.CurrentPoint.HasValue)
                query = query.Where(q => q.CurrentPoint, filter.CurrentPoint);
            if (filter.ChangePoint != null && filter.ChangePoint.HasValue)
                query = query.Where(q => q.ChangePoint, filter.ChangePoint);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerPointHistoryDAO> OrFilter(IQueryable<CustomerPointHistoryDAO> query, CustomerPointHistoryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerPointHistoryDAO> initQuery = query.Where(q => false);
            foreach (CustomerPointHistoryFilter CustomerPointHistoryFilter in filter.OrFilter)
            {
                IQueryable<CustomerPointHistoryDAO> queryable = query;
                if (CustomerPointHistoryFilter.Id != null && CustomerPointHistoryFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerPointHistoryFilter.Id);
                if (CustomerPointHistoryFilter.CustomerId != null && CustomerPointHistoryFilter.CustomerId.HasValue)
                    queryable = queryable.Where(q => q.CustomerId, CustomerPointHistoryFilter.CustomerId);
                if (CustomerPointHistoryFilter.TotalPoint != null && CustomerPointHistoryFilter.TotalPoint.HasValue)
                    queryable = queryable.Where(q => q.TotalPoint, CustomerPointHistoryFilter.TotalPoint);
                if (CustomerPointHistoryFilter.CurrentPoint != null && CustomerPointHistoryFilter.CurrentPoint.HasValue)
                    queryable = queryable.Where(q => q.CurrentPoint, CustomerPointHistoryFilter.CurrentPoint);
                if (CustomerPointHistoryFilter.ChangePoint != null && CustomerPointHistoryFilter.ChangePoint.HasValue)
                    queryable = queryable.Where(q => q.ChangePoint, CustomerPointHistoryFilter.ChangePoint);
                if (CustomerPointHistoryFilter.Description != null && CustomerPointHistoryFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CustomerPointHistoryFilter.Description);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerPointHistoryDAO> DynamicOrder(IQueryable<CustomerPointHistoryDAO> query, CustomerPointHistoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerPointHistoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerPointHistoryOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case CustomerPointHistoryOrder.TotalPoint:
                            query = query.OrderBy(q => q.TotalPoint);
                            break;
                        case CustomerPointHistoryOrder.CurrentPoint:
                            query = query.OrderBy(q => q.CurrentPoint);
                            break;
                        case CustomerPointHistoryOrder.ChangePoint:
                            query = query.OrderBy(q => q.ChangePoint);
                            break;
                        case CustomerPointHistoryOrder.IsIncrease:
                            query = query.OrderBy(q => q.IsIncrease);
                            break;
                        case CustomerPointHistoryOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CustomerPointHistoryOrder.ReduceTotal:
                            query = query.OrderBy(q => q.ReduceTotal);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerPointHistoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerPointHistoryOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case CustomerPointHistoryOrder.TotalPoint:
                            query = query.OrderByDescending(q => q.TotalPoint);
                            break;
                        case CustomerPointHistoryOrder.CurrentPoint:
                            query = query.OrderByDescending(q => q.CurrentPoint);
                            break;
                        case CustomerPointHistoryOrder.ChangePoint:
                            query = query.OrderByDescending(q => q.ChangePoint);
                            break;
                        case CustomerPointHistoryOrder.IsIncrease:
                            query = query.OrderByDescending(q => q.IsIncrease);
                            break;
                        case CustomerPointHistoryOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CustomerPointHistoryOrder.ReduceTotal:
                            query = query.OrderByDescending(q => q.ReduceTotal);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerPointHistory>> DynamicSelect(IQueryable<CustomerPointHistoryDAO> query, CustomerPointHistoryFilter filter)
        {
            List<CustomerPointHistory> CustomerPointHistories = await query.Select(q => new CustomerPointHistory()
            {
                Id = filter.Selects.Contains(CustomerPointHistorySelect.Id) ? q.Id : default(long),
                CustomerId = filter.Selects.Contains(CustomerPointHistorySelect.Customer) ? q.CustomerId : default(long),
                TotalPoint = filter.Selects.Contains(CustomerPointHistorySelect.TotalPoint) ? q.TotalPoint : default(long),
                CurrentPoint = filter.Selects.Contains(CustomerPointHistorySelect.CurrentPoint) ? q.CurrentPoint : default(long),
                ChangePoint = filter.Selects.Contains(CustomerPointHistorySelect.ChangePoint) ? q.ChangePoint : default(long),
                IsIncrease = filter.Selects.Contains(CustomerPointHistorySelect.IsIncrease) ? q.IsIncrease : default(bool),
                Description = filter.Selects.Contains(CustomerPointHistorySelect.Description) ? q.Description : default(string),
                ReduceTotal = filter.Selects.Contains(CustomerPointHistorySelect.ReduceTotal) ? q.ReduceTotal : default(bool),
                Customer = filter.Selects.Contains(CustomerPointHistorySelect.Customer) && q.Customer != null ? new Customer
                {
                    Id = q.Customer.Id,
                    Code = q.Customer.Code,
                    Name = q.Customer.Name,
                    Phone = q.Customer.Phone,
                    Address = q.Customer.Address,
                    NationId = q.Customer.NationId,
                    ProvinceId = q.Customer.ProvinceId,
                    DistrictId = q.Customer.DistrictId,
                    WardId = q.Customer.WardId,
                    CustomerTypeId = q.Customer.CustomerTypeId,
                    Birthday = q.Customer.Birthday,
                    Email = q.Customer.Email,
                    ProfessionId = q.Customer.ProfessionId,
                    CustomerResourceId = q.Customer.CustomerResourceId,
                    SexId = q.Customer.SexId,
                    StatusId = q.Customer.StatusId,
                    CompanyId = q.Customer.CompanyId,
                    ParentCompanyId = q.Customer.ParentCompanyId,
                    TaxCode = q.Customer.TaxCode,
                    Fax = q.Customer.Fax,
                    Website = q.Customer.Website,
                    NumberOfEmployee = q.Customer.NumberOfEmployee,
                    BusinessTypeId = q.Customer.BusinessTypeId,
                    Investment = q.Customer.Investment,
                    RevenueAnnual = q.Customer.RevenueAnnual,
                    IsSupplier = q.Customer.IsSupplier,
                    Descreption = q.Customer.Descreption,
                    Used = q.Customer.Used,
                    RowId = q.Customer.RowId,
                } : null,
            }).ToListAsync();
            return CustomerPointHistories;
        }

        public async Task<int> Count(CustomerPointHistoryFilter filter)
        {
            IQueryable<CustomerPointHistoryDAO> CustomerPointHistories = DataContext.CustomerPointHistory.AsNoTracking();
            CustomerPointHistories = DynamicFilter(CustomerPointHistories, filter);
            return await CustomerPointHistories.CountAsync();
        }

        public async Task<List<CustomerPointHistory>> List(CustomerPointHistoryFilter filter)
        {
            if (filter == null) return new List<CustomerPointHistory>();
            IQueryable<CustomerPointHistoryDAO> CustomerPointHistoryDAOs = DataContext.CustomerPointHistory.AsNoTracking();
            CustomerPointHistoryDAOs = DynamicFilter(CustomerPointHistoryDAOs, filter);
            CustomerPointHistoryDAOs = DynamicOrder(CustomerPointHistoryDAOs, filter);
            List<CustomerPointHistory> CustomerPointHistories = await DynamicSelect(CustomerPointHistoryDAOs, filter);
            return CustomerPointHistories;
        }

        public async Task<List<CustomerPointHistory>> List(List<long> Ids)
        {
            List<CustomerPointHistory> CustomerPointHistories = await DataContext.CustomerPointHistory.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerPointHistory()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                CustomerId = x.CustomerId,
                TotalPoint = x.TotalPoint,
                CurrentPoint = x.CurrentPoint,
                ChangePoint = x.ChangePoint,
                IsIncrease = x.IsIncrease,
                Description = x.Description,
                ReduceTotal = x.ReduceTotal,
                Customer = x.Customer == null ? null : new Customer
                {
                    Id = x.Customer.Id,
                    Code = x.Customer.Code,
                    Name = x.Customer.Name,
                    Phone = x.Customer.Phone,
                    Address = x.Customer.Address,
                    NationId = x.Customer.NationId,
                    ProvinceId = x.Customer.ProvinceId,
                    DistrictId = x.Customer.DistrictId,
                    WardId = x.Customer.WardId,
                    CustomerTypeId = x.Customer.CustomerTypeId,
                    Birthday = x.Customer.Birthday,
                    Email = x.Customer.Email,
                    ProfessionId = x.Customer.ProfessionId,
                    CustomerResourceId = x.Customer.CustomerResourceId,
                    SexId = x.Customer.SexId,
                    StatusId = x.Customer.StatusId,
                    CompanyId = x.Customer.CompanyId,
                    ParentCompanyId = x.Customer.ParentCompanyId,
                    TaxCode = x.Customer.TaxCode,
                    Fax = x.Customer.Fax,
                    Website = x.Customer.Website,
                    NumberOfEmployee = x.Customer.NumberOfEmployee,
                    BusinessTypeId = x.Customer.BusinessTypeId,
                    Investment = x.Customer.Investment,
                    RevenueAnnual = x.Customer.RevenueAnnual,
                    IsSupplier = x.Customer.IsSupplier,
                    Descreption = x.Customer.Descreption,
                    Used = x.Customer.Used,
                    RowId = x.Customer.RowId,
                },
            }).ToListAsync();
            

            return CustomerPointHistories;
        }

        public async Task<CustomerPointHistory> Get(long Id)
        {
            CustomerPointHistory CustomerPointHistory = await DataContext.CustomerPointHistory.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerPointHistory()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                CustomerId = x.CustomerId,
                TotalPoint = x.TotalPoint,
                CurrentPoint = x.CurrentPoint,
                ChangePoint = x.ChangePoint,
                IsIncrease = x.IsIncrease,
                Description = x.Description,
                ReduceTotal = x.ReduceTotal,
                Customer = x.Customer == null ? null : new Customer
                {
                    Id = x.Customer.Id,
                    Code = x.Customer.Code,
                    Name = x.Customer.Name,
                    Phone = x.Customer.Phone,
                    Address = x.Customer.Address,
                    NationId = x.Customer.NationId,
                    ProvinceId = x.Customer.ProvinceId,
                    DistrictId = x.Customer.DistrictId,
                    WardId = x.Customer.WardId,
                    CustomerTypeId = x.Customer.CustomerTypeId,
                    Birthday = x.Customer.Birthday,
                    Email = x.Customer.Email,
                    ProfessionId = x.Customer.ProfessionId,
                    CustomerResourceId = x.Customer.CustomerResourceId,
                    SexId = x.Customer.SexId,
                    StatusId = x.Customer.StatusId,
                    CompanyId = x.Customer.CompanyId,
                    ParentCompanyId = x.Customer.ParentCompanyId,
                    TaxCode = x.Customer.TaxCode,
                    Fax = x.Customer.Fax,
                    Website = x.Customer.Website,
                    NumberOfEmployee = x.Customer.NumberOfEmployee,
                    BusinessTypeId = x.Customer.BusinessTypeId,
                    Investment = x.Customer.Investment,
                    RevenueAnnual = x.Customer.RevenueAnnual,
                    IsSupplier = x.Customer.IsSupplier,
                    Descreption = x.Customer.Descreption,
                    Used = x.Customer.Used,
                    RowId = x.Customer.RowId,
                },
            }).FirstOrDefaultAsync();

            if (CustomerPointHistory == null)
                return null;

            return CustomerPointHistory;
        }
        public async Task<bool> Create(CustomerPointHistory CustomerPointHistory)
        {
            CustomerPointHistoryDAO CustomerPointHistoryDAO = new CustomerPointHistoryDAO();
            CustomerPointHistoryDAO.Id = CustomerPointHistory.Id;
            CustomerPointHistoryDAO.CustomerId = CustomerPointHistory.CustomerId;
            CustomerPointHistoryDAO.TotalPoint = CustomerPointHistory.TotalPoint;
            CustomerPointHistoryDAO.CurrentPoint = CustomerPointHistory.CurrentPoint;
            CustomerPointHistoryDAO.ChangePoint = CustomerPointHistory.ChangePoint;
            CustomerPointHistoryDAO.IsIncrease = CustomerPointHistory.IsIncrease;
            CustomerPointHistoryDAO.Description = CustomerPointHistory.Description;
            CustomerPointHistoryDAO.ReduceTotal = CustomerPointHistory.ReduceTotal;
            CustomerPointHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerPointHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerPointHistory.Add(CustomerPointHistoryDAO);
            await DataContext.SaveChangesAsync();
            CustomerPointHistory.Id = CustomerPointHistoryDAO.Id;
            await SaveReference(CustomerPointHistory);
            return true;
        }

        public async Task<bool> Update(CustomerPointHistory CustomerPointHistory)
        {
            CustomerPointHistoryDAO CustomerPointHistoryDAO = DataContext.CustomerPointHistory.Where(x => x.Id == CustomerPointHistory.Id).FirstOrDefault();
            if (CustomerPointHistoryDAO == null)
                return false;
            CustomerPointHistoryDAO.Id = CustomerPointHistory.Id;
            CustomerPointHistoryDAO.CustomerId = CustomerPointHistory.CustomerId;
            CustomerPointHistoryDAO.TotalPoint = CustomerPointHistory.TotalPoint;
            CustomerPointHistoryDAO.CurrentPoint = CustomerPointHistory.CurrentPoint;
            CustomerPointHistoryDAO.ChangePoint = CustomerPointHistory.ChangePoint;
            CustomerPointHistoryDAO.IsIncrease = CustomerPointHistory.IsIncrease;
            CustomerPointHistoryDAO.Description = CustomerPointHistory.Description;
            CustomerPointHistoryDAO.ReduceTotal = CustomerPointHistory.ReduceTotal;
            CustomerPointHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerPointHistory);
            return true;
        }

        public async Task<bool> Delete(CustomerPointHistory CustomerPointHistory)
        {
            await DataContext.CustomerPointHistory.Where(x => x.Id == CustomerPointHistory.Id).UpdateFromQueryAsync(x => new CustomerPointHistoryDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerPointHistory> CustomerPointHistories)
        {
            List<CustomerPointHistoryDAO> CustomerPointHistoryDAOs = new List<CustomerPointHistoryDAO>();
            foreach (CustomerPointHistory CustomerPointHistory in CustomerPointHistories)
            {
                CustomerPointHistoryDAO CustomerPointHistoryDAO = new CustomerPointHistoryDAO();
                CustomerPointHistoryDAO.Id = CustomerPointHistory.Id;
                CustomerPointHistoryDAO.CustomerId = CustomerPointHistory.CustomerId;
                CustomerPointHistoryDAO.TotalPoint = CustomerPointHistory.TotalPoint;
                CustomerPointHistoryDAO.CurrentPoint = CustomerPointHistory.CurrentPoint;
                CustomerPointHistoryDAO.ChangePoint = CustomerPointHistory.ChangePoint;
                CustomerPointHistoryDAO.IsIncrease = CustomerPointHistory.IsIncrease;
                CustomerPointHistoryDAO.Description = CustomerPointHistory.Description;
                CustomerPointHistoryDAO.ReduceTotal = CustomerPointHistory.ReduceTotal;
                CustomerPointHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerPointHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerPointHistoryDAOs.Add(CustomerPointHistoryDAO);
            }
            await DataContext.BulkMergeAsync(CustomerPointHistoryDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerPointHistory> CustomerPointHistories)
        {
            List<long> Ids = CustomerPointHistories.Select(x => x.Id).ToList();
            await DataContext.CustomerPointHistory
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerPointHistoryDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerPointHistory CustomerPointHistory)
        {
        }
        
    }
}
