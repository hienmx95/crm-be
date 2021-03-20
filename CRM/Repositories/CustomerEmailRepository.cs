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
    public interface ICustomerEmailRepository
    {
        Task<int> Count(CustomerEmailFilter CustomerEmailFilter);
        Task<List<CustomerEmail>> List(CustomerEmailFilter CustomerEmailFilter);
        Task<List<CustomerEmail>> List(List<long> Ids);
        Task<CustomerEmail> Get(long Id);
        Task<bool> Create(CustomerEmail CustomerEmail);
        Task<bool> Update(CustomerEmail CustomerEmail);
        Task<bool> Delete(CustomerEmail CustomerEmail);
        Task<bool> BulkMerge(List<CustomerEmail> CustomerEmails);
        Task<bool> BulkDelete(List<CustomerEmail> CustomerEmails);
    }
    public class CustomerEmailRepository : ICustomerEmailRepository
    {
        private DataContext DataContext;
        public CustomerEmailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerEmailDAO> DynamicFilter(IQueryable<CustomerEmailDAO> query, CustomerEmailFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.Email != null && filter.Email.HasValue)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.EmailTypeId != null && filter.EmailTypeId.HasValue)
                query = query.Where(q => q.EmailTypeId, filter.EmailTypeId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerEmailDAO> OrFilter(IQueryable<CustomerEmailDAO> query, CustomerEmailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerEmailDAO> initQuery = query.Where(q => false);
            foreach (CustomerEmailFilter CustomerEmailFilter in filter.OrFilter)
            {
                IQueryable<CustomerEmailDAO> queryable = query;
                if (CustomerEmailFilter.Id != null && CustomerEmailFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerEmailFilter.Id);
                if (CustomerEmailFilter.CustomerId != null && CustomerEmailFilter.CustomerId.HasValue)
                    queryable = queryable.Where(q => q.CustomerId, CustomerEmailFilter.CustomerId);
                if (CustomerEmailFilter.Email != null && CustomerEmailFilter.Email.HasValue)
                    queryable = queryable.Where(q => q.Email, CustomerEmailFilter.Email);
                if (CustomerEmailFilter.EmailTypeId != null && CustomerEmailFilter.EmailTypeId.HasValue)
                    queryable = queryable.Where(q => q.EmailTypeId, CustomerEmailFilter.EmailTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerEmailDAO> DynamicOrder(IQueryable<CustomerEmailDAO> query, CustomerEmailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerEmailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerEmailOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case CustomerEmailOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case CustomerEmailOrder.EmailType:
                            query = query.OrderBy(q => q.EmailTypeId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerEmailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerEmailOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case CustomerEmailOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case CustomerEmailOrder.EmailType:
                            query = query.OrderByDescending(q => q.EmailTypeId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerEmail>> DynamicSelect(IQueryable<CustomerEmailDAO> query, CustomerEmailFilter filter)
        {
            List<CustomerEmail> CustomerEmails = await query.Select(q => new CustomerEmail()
            {
                Id = filter.Selects.Contains(CustomerEmailSelect.Id) ? q.Id : default(long),
                CustomerId = filter.Selects.Contains(CustomerEmailSelect.Customer) ? q.CustomerId : default(long),
                Email = filter.Selects.Contains(CustomerEmailSelect.Email) ? q.Email : default(string),
                EmailTypeId = filter.Selects.Contains(CustomerEmailSelect.EmailType) ? q.EmailTypeId : default(long),
                Customer = filter.Selects.Contains(CustomerEmailSelect.Customer) && q.Customer != null ? new Customer
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
                EmailType = filter.Selects.Contains(CustomerEmailSelect.EmailType) && q.EmailType != null ? new EmailType
                {
                    Id = q.EmailType.Id,
                    Code = q.EmailType.Code,
                    Name = q.EmailType.Name,
                } : null,
            }).ToListAsync();
            return CustomerEmails;
        }

        public async Task<int> Count(CustomerEmailFilter filter)
        {
            IQueryable<CustomerEmailDAO> CustomerEmails = DataContext.CustomerEmail.AsNoTracking();
            CustomerEmails = DynamicFilter(CustomerEmails, filter);
            return await CustomerEmails.CountAsync();
        }

        public async Task<List<CustomerEmail>> List(CustomerEmailFilter filter)
        {
            if (filter == null) return new List<CustomerEmail>();
            IQueryable<CustomerEmailDAO> CustomerEmailDAOs = DataContext.CustomerEmail.AsNoTracking();
            CustomerEmailDAOs = DynamicFilter(CustomerEmailDAOs, filter);
            CustomerEmailDAOs = DynamicOrder(CustomerEmailDAOs, filter);
            List<CustomerEmail> CustomerEmails = await DynamicSelect(CustomerEmailDAOs, filter);
            return CustomerEmails;
        }

        public async Task<List<CustomerEmail>> List(List<long> Ids)
        {
            List<CustomerEmail> CustomerEmails = await DataContext.CustomerEmail.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerEmail()
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                Email = x.Email,
                EmailTypeId = x.EmailTypeId,
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
                EmailType = x.EmailType == null ? null : new EmailType
                {
                    Id = x.EmailType.Id,
                    Code = x.EmailType.Code,
                    Name = x.EmailType.Name,
                },
            }).ToListAsync();
            

            return CustomerEmails;
        }

        public async Task<CustomerEmail> Get(long Id)
        {
            CustomerEmail CustomerEmail = await DataContext.CustomerEmail.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerEmail()
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                Email = x.Email,
                EmailTypeId = x.EmailTypeId,
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
                EmailType = x.EmailType == null ? null : new EmailType
                {
                    Id = x.EmailType.Id,
                    Code = x.EmailType.Code,
                    Name = x.EmailType.Name,
                },
            }).FirstOrDefaultAsync();

            if (CustomerEmail == null)
                return null;

            return CustomerEmail;
        }
        public async Task<bool> Create(CustomerEmail CustomerEmail)
        {
            CustomerEmailDAO CustomerEmailDAO = new CustomerEmailDAO();
            CustomerEmailDAO.Id = CustomerEmail.Id;
            CustomerEmailDAO.CustomerId = CustomerEmail.CustomerId;
            CustomerEmailDAO.Email = CustomerEmail.Email;
            CustomerEmailDAO.EmailTypeId = CustomerEmail.EmailTypeId;
            DataContext.CustomerEmail.Add(CustomerEmailDAO);
            await DataContext.SaveChangesAsync();
            CustomerEmail.Id = CustomerEmailDAO.Id;
            await SaveReference(CustomerEmail);
            return true;
        }

        public async Task<bool> Update(CustomerEmail CustomerEmail)
        {
            CustomerEmailDAO CustomerEmailDAO = DataContext.CustomerEmail.Where(x => x.Id == CustomerEmail.Id).FirstOrDefault();
            if (CustomerEmailDAO == null)
                return false;
            CustomerEmailDAO.Id = CustomerEmail.Id;
            CustomerEmailDAO.CustomerId = CustomerEmail.CustomerId;
            CustomerEmailDAO.Email = CustomerEmail.Email;
            CustomerEmailDAO.EmailTypeId = CustomerEmail.EmailTypeId;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerEmail);
            return true;
        }

        public async Task<bool> Delete(CustomerEmail CustomerEmail)
        {
            await DataContext.CustomerEmail.Where(x => x.Id == CustomerEmail.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerEmail> CustomerEmails)
        {
            List<CustomerEmailDAO> CustomerEmailDAOs = new List<CustomerEmailDAO>();
            foreach (CustomerEmail CustomerEmail in CustomerEmails)
            {
                CustomerEmailDAO CustomerEmailDAO = new CustomerEmailDAO();
                CustomerEmailDAO.Id = CustomerEmail.Id;
                CustomerEmailDAO.CustomerId = CustomerEmail.CustomerId;
                CustomerEmailDAO.Email = CustomerEmail.Email;
                CustomerEmailDAO.EmailTypeId = CustomerEmail.EmailTypeId;
                CustomerEmailDAOs.Add(CustomerEmailDAO);
            }
            await DataContext.BulkMergeAsync(CustomerEmailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerEmail> CustomerEmails)
        {
            List<long> Ids = CustomerEmails.Select(x => x.Id).ToList();
            await DataContext.CustomerEmail
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(CustomerEmail CustomerEmail)
        {
        }
        
    }
}
