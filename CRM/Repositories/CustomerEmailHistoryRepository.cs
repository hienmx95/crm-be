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
    public interface ICustomerEmailHistoryRepository
    {
        Task<int> Count(CustomerEmailHistoryFilter CustomerEmailHistoryFilter);
        Task<List<CustomerEmailHistory>> List(CustomerEmailHistoryFilter CustomerEmailHistoryFilter);
        Task<List<CustomerEmailHistory>> List(List<long> Ids);
        Task<CustomerEmailHistory> Get(long Id);
        Task<bool> Create(CustomerEmailHistory CustomerEmailHistory);
        Task<bool> Update(CustomerEmailHistory CustomerEmailHistory);
        Task<bool> Delete(CustomerEmailHistory CustomerEmailHistory);
        Task<bool> BulkMerge(List<CustomerEmailHistory> CustomerEmailHistories);
        Task<bool> BulkDelete(List<CustomerEmailHistory> CustomerEmailHistories);
    }
    public class CustomerEmailHistoryRepository : ICustomerEmailHistoryRepository
    {
        private DataContext DataContext;
        public CustomerEmailHistoryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerEmailHistoryDAO> DynamicFilter(IQueryable<CustomerEmailHistoryDAO> query, CustomerEmailHistoryFilter filter)
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
            if (filter.Title != null && filter.Title.HasValue)
                query = query.Where(q => q.Title, filter.Title);
            if (filter.Content != null && filter.Content.HasValue)
                query = query.Where(q => q.Content, filter.Content);
            if (filter.Reciepient != null && filter.Reciepient.HasValue)
                query = query.Where(q => q.Reciepient, filter.Reciepient);
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.EmailStatusId != null && filter.EmailStatusId.HasValue)
                query = query.Where(q => q.EmailStatusId, filter.EmailStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerEmailHistoryDAO> OrFilter(IQueryable<CustomerEmailHistoryDAO> query, CustomerEmailHistoryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerEmailHistoryDAO> initQuery = query.Where(q => false);
            foreach (CustomerEmailHistoryFilter CustomerEmailHistoryFilter in filter.OrFilter)
            {
                IQueryable<CustomerEmailHistoryDAO> queryable = query;
                if (CustomerEmailHistoryFilter.Id != null && CustomerEmailHistoryFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerEmailHistoryFilter.Id);
                if (CustomerEmailHistoryFilter.Title != null && CustomerEmailHistoryFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CustomerEmailHistoryFilter.Title);
                if (CustomerEmailHistoryFilter.Content != null && CustomerEmailHistoryFilter.Content.HasValue)
                    queryable = queryable.Where(q => q.Content, CustomerEmailHistoryFilter.Content);
                if (CustomerEmailHistoryFilter.Reciepient != null && CustomerEmailHistoryFilter.Reciepient.HasValue)
                    queryable = queryable.Where(q => q.Reciepient, CustomerEmailHistoryFilter.Reciepient);
                if (CustomerEmailHistoryFilter.CustomerId != null && CustomerEmailHistoryFilter.CustomerId.HasValue)
                    queryable = queryable.Where(q => q.CustomerId, CustomerEmailHistoryFilter.CustomerId);
                if (CustomerEmailHistoryFilter.CreatorId != null && CustomerEmailHistoryFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CustomerEmailHistoryFilter.CreatorId);
                if (CustomerEmailHistoryFilter.EmailStatusId != null && CustomerEmailHistoryFilter.EmailStatusId.HasValue)
                    queryable = queryable.Where(q => q.EmailStatusId, CustomerEmailHistoryFilter.EmailStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<CustomerEmailHistoryDAO> DynamicOrder(IQueryable<CustomerEmailHistoryDAO> query, CustomerEmailHistoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerEmailHistoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerEmailHistoryOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CustomerEmailHistoryOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case CustomerEmailHistoryOrder.Reciepient:
                            query = query.OrderBy(q => q.Reciepient);
                            break;
                        case CustomerEmailHistoryOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case CustomerEmailHistoryOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CustomerEmailHistoryOrder.EmailStatus:
                            query = query.OrderBy(q => q.EmailStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerEmailHistoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerEmailHistoryOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CustomerEmailHistoryOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case CustomerEmailHistoryOrder.Reciepient:
                            query = query.OrderByDescending(q => q.Reciepient);
                            break;
                        case CustomerEmailHistoryOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case CustomerEmailHistoryOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CustomerEmailHistoryOrder.EmailStatus:
                            query = query.OrderByDescending(q => q.EmailStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerEmailHistory>> DynamicSelect(IQueryable<CustomerEmailHistoryDAO> query, CustomerEmailHistoryFilter filter)
        {
            List<CustomerEmailHistory> CustomerEmailHistories = await query.Select(q => new CustomerEmailHistory()
            {
                Id = filter.Selects.Contains(CustomerEmailHistorySelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(CustomerEmailHistorySelect.Title) ? q.Title : default(string),
                Content = filter.Selects.Contains(CustomerEmailHistorySelect.Content) ? q.Content : default(string),
                Reciepient = filter.Selects.Contains(CustomerEmailHistorySelect.Reciepient) ? q.Reciepient : default(string),
                CustomerId = filter.Selects.Contains(CustomerEmailHistorySelect.Customer) ? q.CustomerId : default(long),
                CreatorId = filter.Selects.Contains(CustomerEmailHistorySelect.Creator) ? q.CreatorId : default(long),
                EmailStatusId = filter.Selects.Contains(CustomerEmailHistorySelect.EmailStatus) ? q.EmailStatusId : default(long),
                Creator = filter.Selects.Contains(CustomerEmailHistorySelect.Creator) && q.Creator != null ? new AppUser
                {
                    Id = q.Creator.Id,
                    Username = q.Creator.Username,
                    DisplayName = q.Creator.DisplayName,
                    Address = q.Creator.Address,
                    Email = q.Creator.Email,
                    Phone = q.Creator.Phone,
                    SexId = q.Creator.SexId,
                    Birthday = q.Creator.Birthday,
                    Avatar = q.Creator.Avatar,
                    Department = q.Creator.Department,
                    OrganizationId = q.Creator.OrganizationId,
                    Longitude = q.Creator.Longitude,
                    Latitude = q.Creator.Latitude,
                    StatusId = q.Creator.StatusId,
                    RowId = q.Creator.RowId,
                    Used = q.Creator.Used,
                } : null,
                Customer = filter.Selects.Contains(CustomerEmailHistorySelect.Customer) && q.Customer != null ? new Customer
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
                EmailStatus = filter.Selects.Contains(CustomerEmailHistorySelect.EmailStatus) && q.EmailStatus != null ? new EmailStatus
                {
                    Id = q.EmailStatus.Id,
                    Code = q.EmailStatus.Code,
                    Name = q.EmailStatus.Name,
                } : null,
            }).ToListAsync();
            return CustomerEmailHistories;
        }

        public async Task<int> Count(CustomerEmailHistoryFilter filter)
        {
            IQueryable<CustomerEmailHistoryDAO> CustomerEmailHistories = DataContext.CustomerEmailHistory.AsNoTracking();
            CustomerEmailHistories = DynamicFilter(CustomerEmailHistories, filter);
            return await CustomerEmailHistories.CountAsync();
        }

        public async Task<List<CustomerEmailHistory>> List(CustomerEmailHistoryFilter filter)
        {
            if (filter == null) return new List<CustomerEmailHistory>();
            IQueryable<CustomerEmailHistoryDAO> CustomerEmailHistoryDAOs = DataContext.CustomerEmailHistory.AsNoTracking();
            CustomerEmailHistoryDAOs = DynamicFilter(CustomerEmailHistoryDAOs, filter);
            CustomerEmailHistoryDAOs = DynamicOrder(CustomerEmailHistoryDAOs, filter);
            List<CustomerEmailHistory> CustomerEmailHistories = await DynamicSelect(CustomerEmailHistoryDAOs, filter);
            return CustomerEmailHistories;
        }

        public async Task<List<CustomerEmailHistory>> List(List<long> Ids)
        {
            List<CustomerEmailHistory> CustomerEmailHistories = await DataContext.CustomerEmailHistory.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerEmailHistory()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Reciepient = x.Reciepient,
                CustomerId = x.CustomerId,
                CreatorId = x.CreatorId,
                EmailStatusId = x.EmailStatusId,
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
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
                EmailStatus = x.EmailStatus == null ? null : new EmailStatus
                {
                    Id = x.EmailStatus.Id,
                    Code = x.EmailStatus.Code,
                    Name = x.EmailStatus.Name,
                },
            }).ToListAsync();


            return CustomerEmailHistories;
        }

        public async Task<CustomerEmailHistory> Get(long Id)
        {
            CustomerEmailHistory CustomerEmailHistory = await DataContext.CustomerEmailHistory.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerEmailHistory()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Reciepient = x.Reciepient,
                CustomerId = x.CustomerId,
                CreatorId = x.CreatorId,
                EmailStatusId = x.EmailStatusId,
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
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
                EmailStatus = x.EmailStatus == null ? null : new EmailStatus
                {
                    Id = x.EmailStatus.Id,
                    Code = x.EmailStatus.Code,
                    Name = x.EmailStatus.Name,
                },
            }).FirstOrDefaultAsync();

            if (CustomerEmailHistory == null)
                return null;

            CustomerEmailHistory.CustomerCCEmailHistories = await DataContext.CustomerCCEmailHistory
                .Where(x => x.CustomerEmailHistoryId == CustomerEmailHistory.Id)
                .Select(x => new CustomerCCEmailHistory
                {
                    Id = x.Id,
                    CCEmail = x.CCEmail,
                    CustomerEmailHistoryId = x.CustomerEmailHistoryId
                }).ToListAsync();
            return CustomerEmailHistory;
        }
        public async Task<bool> Create(CustomerEmailHistory CustomerEmailHistory)
        {
            CustomerEmailHistoryDAO CustomerEmailHistoryDAO = new CustomerEmailHistoryDAO();
            CustomerEmailHistoryDAO.Id = CustomerEmailHistory.Id;
            CustomerEmailHistoryDAO.Title = CustomerEmailHistory.Title;
            CustomerEmailHistoryDAO.Content = CustomerEmailHistory.Content;
            CustomerEmailHistoryDAO.Reciepient = CustomerEmailHistory.Reciepient;
            CustomerEmailHistoryDAO.CustomerId = CustomerEmailHistory.CustomerId;
            CustomerEmailHistoryDAO.CreatorId = CustomerEmailHistory.CreatorId;
            CustomerEmailHistoryDAO.EmailStatusId = CustomerEmailHistory.EmailStatusId;
            CustomerEmailHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerEmailHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerEmailHistory.Add(CustomerEmailHistoryDAO);
            await DataContext.SaveChangesAsync();
            CustomerEmailHistory.Id = CustomerEmailHistoryDAO.Id;
            await SaveReference(CustomerEmailHistory);
            return true;
        }

        public async Task<bool> Update(CustomerEmailHistory CustomerEmailHistory)
        {
            CustomerEmailHistoryDAO CustomerEmailHistoryDAO = DataContext.CustomerEmailHistory.Where(x => x.Id == CustomerEmailHistory.Id).FirstOrDefault();
            if (CustomerEmailHistoryDAO == null)
                return false;
            CustomerEmailHistoryDAO.Id = CustomerEmailHistory.Id;
            CustomerEmailHistoryDAO.Title = CustomerEmailHistory.Title;
            CustomerEmailHistoryDAO.Content = CustomerEmailHistory.Content;
            CustomerEmailHistoryDAO.Reciepient = CustomerEmailHistory.Reciepient;
            CustomerEmailHistoryDAO.CustomerId = CustomerEmailHistory.CustomerId;
            CustomerEmailHistoryDAO.CreatorId = CustomerEmailHistory.CreatorId;
            CustomerEmailHistoryDAO.EmailStatusId = CustomerEmailHistory.EmailStatusId;
            CustomerEmailHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerEmailHistory);
            return true;
        }

        public async Task<bool> Delete(CustomerEmailHistory CustomerEmailHistory)
        {
            await DataContext.CustomerEmailHistory.Where(x => x.Id == CustomerEmailHistory.Id).UpdateFromQueryAsync(x => new CustomerEmailHistoryDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<CustomerEmailHistory> CustomerEmailHistories)
        {
            List<CustomerEmailHistoryDAO> CustomerEmailHistoryDAOs = new List<CustomerEmailHistoryDAO>();
            foreach (CustomerEmailHistory CustomerEmailHistory in CustomerEmailHistories)
            {
                CustomerEmailHistoryDAO CustomerEmailHistoryDAO = new CustomerEmailHistoryDAO();
                CustomerEmailHistoryDAO.Id = CustomerEmailHistory.Id;
                CustomerEmailHistoryDAO.Title = CustomerEmailHistory.Title;
                CustomerEmailHistoryDAO.Content = CustomerEmailHistory.Content;
                CustomerEmailHistoryDAO.Reciepient = CustomerEmailHistory.Reciepient;
                CustomerEmailHistoryDAO.CustomerId = CustomerEmailHistory.CustomerId;
                CustomerEmailHistoryDAO.CreatorId = CustomerEmailHistory.CreatorId;
                CustomerEmailHistoryDAO.EmailStatusId = CustomerEmailHistory.EmailStatusId;
                CustomerEmailHistoryDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerEmailHistoryDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerEmailHistoryDAOs.Add(CustomerEmailHistoryDAO);
            }
            await DataContext.BulkMergeAsync(CustomerEmailHistoryDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerEmailHistory> CustomerEmailHistories)
        {
            List<long> Ids = CustomerEmailHistories.Select(x => x.Id).ToList();
            await DataContext.CustomerEmailHistory
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerEmailHistoryDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerEmailHistory CustomerEmailHistory)
        {
            await DataContext.CustomerCCEmailHistory.Where(x => x.CustomerEmailHistoryId == CustomerEmailHistory.Id).DeleteFromQueryAsync();
            if (CustomerEmailHistory.CustomerCCEmailHistories != null)
            {
                List<CustomerCCEmailHistoryDAO> CustomerCCEmailHistoryDAOs = new List<CustomerCCEmailHistoryDAO>();
                foreach (var CustomerCCEmailHistory in CustomerEmailHistory.CustomerCCEmailHistories)
                {
                    CustomerCCEmailHistoryDAO CustomerCCEmailHistoryDAO = new CustomerCCEmailHistoryDAO
                    {
                        Id = CustomerCCEmailHistory.Id,
                        CustomerEmailHistoryId = CustomerEmailHistory.Id,
                        CCEmail = CustomerCCEmailHistory.CCEmail
                    };
                    CustomerCCEmailHistoryDAOs.Add(CustomerCCEmailHistoryDAO);
                }
                await DataContext.BulkMergeAsync(CustomerCCEmailHistoryDAOs);
            }
        }

    }
}
