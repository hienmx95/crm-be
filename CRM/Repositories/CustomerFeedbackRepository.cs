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
    public interface ICustomerFeedbackRepository
    {
        Task<int> Count(CustomerFeedbackFilter CustomerFeedbackFilter);
        Task<List<CustomerFeedback>> List(CustomerFeedbackFilter CustomerFeedbackFilter);
        Task<List<CustomerFeedback>> List(List<long> Ids);
        Task<CustomerFeedback> Get(long Id);
        Task<bool> Create(CustomerFeedback CustomerFeedback);
        Task<bool> Update(CustomerFeedback CustomerFeedback);
        Task<bool> Delete(CustomerFeedback CustomerFeedback);
        Task<bool> BulkMerge(List<CustomerFeedback> CustomerFeedbacks);
        Task<bool> BulkDelete(List<CustomerFeedback> CustomerFeedbacks);
    }
    public class CustomerFeedbackRepository : ICustomerFeedbackRepository
    {
        private DataContext DataContext;
        public CustomerFeedbackRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerFeedbackDAO> DynamicFilter(IQueryable<CustomerFeedbackDAO> query, CustomerFeedbackFilter filter)
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
                query = query.Where(q => q.CustomerId.HasValue).Where(q => q.CustomerId, filter.CustomerId);
            if (filter.FullName != null && filter.FullName.HasValue)
                query = query.Where(q => q.FullName, filter.FullName);
            if (filter.Email != null && filter.Email.HasValue)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.PhoneNumber != null && filter.PhoneNumber.HasValue)
                query = query.Where(q => q.PhoneNumber, filter.PhoneNumber);
            if (filter.CustomerFeedbackTypeId != null && filter.CustomerFeedbackTypeId.HasValue)
                query = query.Where(q => q.CustomerFeedbackTypeId.HasValue).Where(q => q.CustomerFeedbackTypeId, filter.CustomerFeedbackTypeId);
            if (filter.Title != null && filter.Title.HasValue)
                query = query.Where(q => q.Title, filter.Title);
            if (filter.SendDate != null && filter.SendDate.HasValue)
                query = query.Where(q => q.SendDate == null).Union(query.Where(q => q.SendDate.HasValue).Where(q => q.SendDate, filter.SendDate));
            if (filter.Content != null && filter.Content.HasValue)
                query = query.Where(q => q.Content, filter.Content);
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId.HasValue).Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerFeedbackDAO> OrFilter(IQueryable<CustomerFeedbackDAO> query, CustomerFeedbackFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerFeedbackDAO> initQuery = query.Where(q => false);
            foreach (CustomerFeedbackFilter CustomerFeedbackFilter in filter.OrFilter)
            {
                IQueryable<CustomerFeedbackDAO> queryable = query;
                if (CustomerFeedbackFilter.Id != null && CustomerFeedbackFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerFeedbackFilter.Id);
                if (CustomerFeedbackFilter.CustomerId != null && CustomerFeedbackFilter.CustomerId.HasValue)
                    queryable = queryable.Where(q => q.CustomerId.HasValue).Where(q => q.CustomerId, CustomerFeedbackFilter.CustomerId);
                if (CustomerFeedbackFilter.FullName != null && CustomerFeedbackFilter.FullName.HasValue)
                    queryable = queryable.Where(q => q.FullName, CustomerFeedbackFilter.FullName);
                if (CustomerFeedbackFilter.Email != null && CustomerFeedbackFilter.Email.HasValue)
                    queryable = queryable.Where(q => q.Email, CustomerFeedbackFilter.Email);
                if (CustomerFeedbackFilter.PhoneNumber != null && CustomerFeedbackFilter.PhoneNumber.HasValue)
                    queryable = queryable.Where(q => q.PhoneNumber, CustomerFeedbackFilter.PhoneNumber);
                if (CustomerFeedbackFilter.CustomerFeedbackTypeId != null && CustomerFeedbackFilter.CustomerFeedbackTypeId.HasValue)
                    queryable = queryable.Where(q => q.CustomerFeedbackTypeId.HasValue).Where(q => q.CustomerFeedbackTypeId, CustomerFeedbackFilter.CustomerFeedbackTypeId);
                if (CustomerFeedbackFilter.Title != null && CustomerFeedbackFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CustomerFeedbackFilter.Title);
                if (CustomerFeedbackFilter.SendDate != null && CustomerFeedbackFilter.SendDate.HasValue)
                    queryable = queryable.Where(q => q.SendDate.HasValue).Where(q => q.SendDate, CustomerFeedbackFilter.SendDate);
                if (CustomerFeedbackFilter.Content != null && CustomerFeedbackFilter.Content.HasValue)
                    queryable = queryable.Where(q => q.Content, CustomerFeedbackFilter.Content);
                if (CustomerFeedbackFilter.StatusId != null && CustomerFeedbackFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId.HasValue).Where(q => q.StatusId, CustomerFeedbackFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerFeedbackDAO> DynamicOrder(IQueryable<CustomerFeedbackDAO> query, CustomerFeedbackFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerFeedbackOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerFeedbackOrder.IsSystemCustomer:
                            query = query.OrderBy(q => q.IsSystemCustomer);
                            break;
                        case CustomerFeedbackOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case CustomerFeedbackOrder.FullName:
                            query = query.OrderBy(q => q.FullName);
                            break;
                        case CustomerFeedbackOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case CustomerFeedbackOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;
                        case CustomerFeedbackOrder.CustomerFeedbackType:
                            query = query.OrderBy(q => q.CustomerFeedbackTypeId);
                            break;
                        case CustomerFeedbackOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CustomerFeedbackOrder.SendDate:
                            query = query.OrderBy(q => q.SendDate);
                            break;
                        case CustomerFeedbackOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case CustomerFeedbackOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerFeedbackOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerFeedbackOrder.IsSystemCustomer:
                            query = query.OrderByDescending(q => q.IsSystemCustomer);
                            break;
                        case CustomerFeedbackOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case CustomerFeedbackOrder.FullName:
                            query = query.OrderByDescending(q => q.FullName);
                            break;
                        case CustomerFeedbackOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case CustomerFeedbackOrder.PhoneNumber:
                            query = query.OrderByDescending(q => q.PhoneNumber);
                            break;
                        case CustomerFeedbackOrder.CustomerFeedbackType:
                            query = query.OrderByDescending(q => q.CustomerFeedbackTypeId);
                            break;
                        case CustomerFeedbackOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CustomerFeedbackOrder.SendDate:
                            query = query.OrderByDescending(q => q.SendDate);
                            break;
                        case CustomerFeedbackOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case CustomerFeedbackOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerFeedback>> DynamicSelect(IQueryable<CustomerFeedbackDAO> query, CustomerFeedbackFilter filter)
        {
            List<CustomerFeedback> CustomerFeedbacks = await query.Select(q => new CustomerFeedback()
            {
                Id = filter.Selects.Contains(CustomerFeedbackSelect.Id) ? q.Id : default(long),
                IsSystemCustomer = filter.Selects.Contains(CustomerFeedbackSelect.IsSystemCustomer) ? q.IsSystemCustomer : default(bool),
                CustomerId = filter.Selects.Contains(CustomerFeedbackSelect.Customer) ? q.CustomerId : default(long?),
                FullName = filter.Selects.Contains(CustomerFeedbackSelect.FullName) ? q.FullName : default(string),
                Email = filter.Selects.Contains(CustomerFeedbackSelect.Email) ? q.Email : default(string),
                PhoneNumber = filter.Selects.Contains(CustomerFeedbackSelect.PhoneNumber) ? q.PhoneNumber : default(string),
                CustomerFeedbackTypeId = filter.Selects.Contains(CustomerFeedbackSelect.CustomerFeedbackType) ? q.CustomerFeedbackTypeId : default(long?),
                Title = filter.Selects.Contains(CustomerFeedbackSelect.Title) ? q.Title : default(string),
                SendDate = filter.Selects.Contains(CustomerFeedbackSelect.SendDate) ? q.SendDate : default(DateTime?),
                Content = filter.Selects.Contains(CustomerFeedbackSelect.Content) ? q.Content : default(string),
                StatusId = filter.Selects.Contains(CustomerFeedbackSelect.Status) ? q.StatusId : default(long?),
                Customer = filter.Selects.Contains(CustomerFeedbackSelect.Customer) && q.Customer != null ? new Customer
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
                CustomerFeedbackType = filter.Selects.Contains(CustomerFeedbackSelect.CustomerFeedbackType) && q.CustomerFeedbackType != null ? new CustomerFeedbackType
                {
                    Id = q.CustomerFeedbackType.Id,
                    Code = q.CustomerFeedbackType.Code,
                    Name = q.CustomerFeedbackType.Name,
                } : null,
                Status = filter.Selects.Contains(CustomerFeedbackSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return CustomerFeedbacks;
        }

        public async Task<int> Count(CustomerFeedbackFilter filter)
        {
            IQueryable<CustomerFeedbackDAO> CustomerFeedbacks = DataContext.CustomerFeedback.AsNoTracking();
            CustomerFeedbacks = DynamicFilter(CustomerFeedbacks, filter);
            return await CustomerFeedbacks.CountAsync();
        }

        public async Task<List<CustomerFeedback>> List(CustomerFeedbackFilter filter)
        {
            if (filter == null) return new List<CustomerFeedback>();
            IQueryable<CustomerFeedbackDAO> CustomerFeedbackDAOs = DataContext.CustomerFeedback.AsNoTracking();
            CustomerFeedbackDAOs = DynamicFilter(CustomerFeedbackDAOs, filter);
            CustomerFeedbackDAOs = DynamicOrder(CustomerFeedbackDAOs, filter);
            List<CustomerFeedback> CustomerFeedbacks = await DynamicSelect(CustomerFeedbackDAOs, filter);
            return CustomerFeedbacks;
        }

        public async Task<List<CustomerFeedback>> List(List<long> Ids)
        {
            List<CustomerFeedback> CustomerFeedbacks = await DataContext.CustomerFeedback.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerFeedback()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                IsSystemCustomer = x.IsSystemCustomer,
                CustomerId = x.CustomerId,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CustomerFeedbackTypeId = x.CustomerFeedbackTypeId,
                Title = x.Title,
                SendDate = x.SendDate,
                Content = x.Content,
                StatusId = x.StatusId,
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
                CustomerFeedbackType = x.CustomerFeedbackType == null ? null : new CustomerFeedbackType
                {
                    Id = x.CustomerFeedbackType.Id,
                    Code = x.CustomerFeedbackType.Code,
                    Name = x.CustomerFeedbackType.Name,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();
            

            return CustomerFeedbacks;
        }

        public async Task<CustomerFeedback> Get(long Id)
        {
            CustomerFeedback CustomerFeedback = await DataContext.CustomerFeedback.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerFeedback()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                IsSystemCustomer = x.IsSystemCustomer,
                CustomerId = x.CustomerId,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                CustomerFeedbackTypeId = x.CustomerFeedbackTypeId,
                Title = x.Title,
                SendDate = x.SendDate,
                Content = x.Content,
                StatusId = x.StatusId,
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
                CustomerFeedbackType = x.CustomerFeedbackType == null ? null : new CustomerFeedbackType
                {
                    Id = x.CustomerFeedbackType.Id,
                    Code = x.CustomerFeedbackType.Code,
                    Name = x.CustomerFeedbackType.Name,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (CustomerFeedback == null)
                return null;

            return CustomerFeedback;
        }
        public async Task<bool> Create(CustomerFeedback CustomerFeedback)
        {
            CustomerFeedbackDAO CustomerFeedbackDAO = new CustomerFeedbackDAO();
            CustomerFeedbackDAO.Id = CustomerFeedback.Id;
            CustomerFeedbackDAO.IsSystemCustomer = CustomerFeedback.IsSystemCustomer;
            CustomerFeedbackDAO.CustomerId = CustomerFeedback.CustomerId;
            CustomerFeedbackDAO.FullName = CustomerFeedback.FullName;
            CustomerFeedbackDAO.Email = CustomerFeedback.Email;
            CustomerFeedbackDAO.PhoneNumber = CustomerFeedback.PhoneNumber;
            CustomerFeedbackDAO.CustomerFeedbackTypeId = CustomerFeedback.CustomerFeedbackTypeId;
            CustomerFeedbackDAO.Title = CustomerFeedback.Title;
            CustomerFeedbackDAO.SendDate = CustomerFeedback.SendDate;
            CustomerFeedbackDAO.Content = CustomerFeedback.Content;
            CustomerFeedbackDAO.StatusId = CustomerFeedback.StatusId;
            CustomerFeedbackDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerFeedbackDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerFeedback.Add(CustomerFeedbackDAO);
            await DataContext.SaveChangesAsync();
            CustomerFeedback.Id = CustomerFeedbackDAO.Id;
            await SaveReference(CustomerFeedback);
            return true;
        }

        public async Task<bool> Update(CustomerFeedback CustomerFeedback)
        {
            CustomerFeedbackDAO CustomerFeedbackDAO = DataContext.CustomerFeedback.Where(x => x.Id == CustomerFeedback.Id).FirstOrDefault();
            if (CustomerFeedbackDAO == null)
                return false;
            CustomerFeedbackDAO.Id = CustomerFeedback.Id;
            CustomerFeedbackDAO.IsSystemCustomer = CustomerFeedback.IsSystemCustomer;
            CustomerFeedbackDAO.CustomerId = CustomerFeedback.CustomerId;
            CustomerFeedbackDAO.FullName = CustomerFeedback.FullName;
            CustomerFeedbackDAO.Email = CustomerFeedback.Email;
            CustomerFeedbackDAO.PhoneNumber = CustomerFeedback.PhoneNumber;
            CustomerFeedbackDAO.CustomerFeedbackTypeId = CustomerFeedback.CustomerFeedbackTypeId;
            CustomerFeedbackDAO.Title = CustomerFeedback.Title;
            CustomerFeedbackDAO.SendDate = CustomerFeedback.SendDate;
            CustomerFeedbackDAO.Content = CustomerFeedback.Content;
            CustomerFeedbackDAO.StatusId = CustomerFeedback.StatusId;
            CustomerFeedbackDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerFeedback);
            return true;
        }

        public async Task<bool> Delete(CustomerFeedback CustomerFeedback)
        {
            await DataContext.CustomerFeedback.Where(x => x.Id == CustomerFeedback.Id).UpdateFromQueryAsync(x => new CustomerFeedbackDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerFeedback> CustomerFeedbacks)
        {
            List<CustomerFeedbackDAO> CustomerFeedbackDAOs = new List<CustomerFeedbackDAO>();
            foreach (CustomerFeedback CustomerFeedback in CustomerFeedbacks)
            {
                CustomerFeedbackDAO CustomerFeedbackDAO = new CustomerFeedbackDAO();
                CustomerFeedbackDAO.Id = CustomerFeedback.Id;
                CustomerFeedbackDAO.IsSystemCustomer = CustomerFeedback.IsSystemCustomer;
                CustomerFeedbackDAO.CustomerId = CustomerFeedback.CustomerId;
                CustomerFeedbackDAO.FullName = CustomerFeedback.FullName;
                CustomerFeedbackDAO.Email = CustomerFeedback.Email;
                CustomerFeedbackDAO.PhoneNumber = CustomerFeedback.PhoneNumber;
                CustomerFeedbackDAO.CustomerFeedbackTypeId = CustomerFeedback.CustomerFeedbackTypeId;
                CustomerFeedbackDAO.Title = CustomerFeedback.Title;
                CustomerFeedbackDAO.SendDate = CustomerFeedback.SendDate;
                CustomerFeedbackDAO.Content = CustomerFeedback.Content;
                CustomerFeedbackDAO.StatusId = CustomerFeedback.StatusId;
                CustomerFeedbackDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerFeedbackDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerFeedbackDAOs.Add(CustomerFeedbackDAO);
            }
            await DataContext.BulkMergeAsync(CustomerFeedbackDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerFeedback> CustomerFeedbacks)
        {
            List<long> Ids = CustomerFeedbacks.Select(x => x.Id).ToList();
            await DataContext.CustomerFeedback
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerFeedbackDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerFeedback CustomerFeedback)
        {
        }
        
    }
}
