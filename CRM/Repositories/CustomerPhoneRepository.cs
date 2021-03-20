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
    public interface ICustomerPhoneRepository
    {
        Task<int> Count(CustomerPhoneFilter CustomerPhoneFilter);
        Task<List<CustomerPhone>> List(CustomerPhoneFilter CustomerPhoneFilter);
        Task<List<CustomerPhone>> List(List<long> Ids);
        Task<CustomerPhone> Get(long Id);
        Task<bool> Create(CustomerPhone CustomerPhone);
        Task<bool> Update(CustomerPhone CustomerPhone);
        Task<bool> Delete(CustomerPhone CustomerPhone);
        Task<bool> BulkMerge(List<CustomerPhone> CustomerPhones);
        Task<bool> BulkDelete(List<CustomerPhone> CustomerPhones);
    }
    public class CustomerPhoneRepository : ICustomerPhoneRepository
    {
        private DataContext DataContext;
        public CustomerPhoneRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerPhoneDAO> DynamicFilter(IQueryable<CustomerPhoneDAO> query, CustomerPhoneFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerId != null && filter.CustomerId.HasValue)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.Phone != null && filter.Phone.HasValue)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.PhoneTypeId != null && filter.PhoneTypeId.HasValue)
                query = query.Where(q => q.PhoneTypeId, filter.PhoneTypeId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerPhoneDAO> OrFilter(IQueryable<CustomerPhoneDAO> query, CustomerPhoneFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerPhoneDAO> initQuery = query.Where(q => false);
            foreach (CustomerPhoneFilter CustomerPhoneFilter in filter.OrFilter)
            {
                IQueryable<CustomerPhoneDAO> queryable = query;
                if (CustomerPhoneFilter.Id != null && CustomerPhoneFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerPhoneFilter.Id);
                if (CustomerPhoneFilter.CustomerId != null && CustomerPhoneFilter.CustomerId.HasValue)
                    queryable = queryable.Where(q => q.CustomerId, CustomerPhoneFilter.CustomerId);
                if (CustomerPhoneFilter.Phone != null && CustomerPhoneFilter.Phone.HasValue)
                    queryable = queryable.Where(q => q.Phone, CustomerPhoneFilter.Phone);
                if (CustomerPhoneFilter.PhoneTypeId != null && CustomerPhoneFilter.PhoneTypeId.HasValue)
                    queryable = queryable.Where(q => q.PhoneTypeId, CustomerPhoneFilter.PhoneTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerPhoneDAO> DynamicOrder(IQueryable<CustomerPhoneDAO> query, CustomerPhoneFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerPhoneOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerPhoneOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case CustomerPhoneOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case CustomerPhoneOrder.PhoneType:
                            query = query.OrderBy(q => q.PhoneTypeId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerPhoneOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerPhoneOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case CustomerPhoneOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case CustomerPhoneOrder.PhoneType:
                            query = query.OrderByDescending(q => q.PhoneTypeId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerPhone>> DynamicSelect(IQueryable<CustomerPhoneDAO> query, CustomerPhoneFilter filter)
        {
            List<CustomerPhone> CustomerPhones = await query.Select(q => new CustomerPhone()
            {
                Id = filter.Selects.Contains(CustomerPhoneSelect.Id) ? q.Id : default(long),
                CustomerId = filter.Selects.Contains(CustomerPhoneSelect.Customer) ? q.CustomerId : default(long),
                Phone = filter.Selects.Contains(CustomerPhoneSelect.Phone) ? q.Phone : default(string),
                PhoneTypeId = filter.Selects.Contains(CustomerPhoneSelect.PhoneType) ? q.PhoneTypeId : default(long),
                Customer = filter.Selects.Contains(CustomerPhoneSelect.Customer) && q.Customer != null ? new Customer
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
                PhoneType = filter.Selects.Contains(CustomerPhoneSelect.PhoneType) && q.PhoneType != null ? new PhoneType
                {
                    Id = q.PhoneType.Id,
                    Code = q.PhoneType.Code,
                    Name = q.PhoneType.Name,
                    StatusId = q.PhoneType.StatusId,
                    Used = q.PhoneType.Used,
                    RowId = q.PhoneType.RowId,
                } : null,
            }).ToListAsync();
            return CustomerPhones;
        }

        public async Task<int> Count(CustomerPhoneFilter filter)
        {
            IQueryable<CustomerPhoneDAO> CustomerPhones = DataContext.CustomerPhone.AsNoTracking();
            CustomerPhones = DynamicFilter(CustomerPhones, filter);
            return await CustomerPhones.CountAsync();
        }

        public async Task<List<CustomerPhone>> List(CustomerPhoneFilter filter)
        {
            if (filter == null) return new List<CustomerPhone>();
            IQueryable<CustomerPhoneDAO> CustomerPhoneDAOs = DataContext.CustomerPhone.AsNoTracking();
            CustomerPhoneDAOs = DynamicFilter(CustomerPhoneDAOs, filter);
            CustomerPhoneDAOs = DynamicOrder(CustomerPhoneDAOs, filter);
            List<CustomerPhone> CustomerPhones = await DynamicSelect(CustomerPhoneDAOs, filter);
            return CustomerPhones;
        }

        public async Task<List<CustomerPhone>> List(List<long> Ids)
        {
            List<CustomerPhone> CustomerPhones = await DataContext.CustomerPhone.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerPhone()
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                Phone = x.Phone,
                PhoneTypeId = x.PhoneTypeId,
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
                PhoneType = x.PhoneType == null ? null : new PhoneType
                {
                    Id = x.PhoneType.Id,
                    Code = x.PhoneType.Code,
                    Name = x.PhoneType.Name,
                    StatusId = x.PhoneType.StatusId,
                    Used = x.PhoneType.Used,
                    RowId = x.PhoneType.RowId,
                },
            }).ToListAsync();
            

            return CustomerPhones;
        }

        public async Task<CustomerPhone> Get(long Id)
        {
            CustomerPhone CustomerPhone = await DataContext.CustomerPhone.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerPhone()
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                Phone = x.Phone,
                PhoneTypeId = x.PhoneTypeId,
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
                PhoneType = x.PhoneType == null ? null : new PhoneType
                {
                    Id = x.PhoneType.Id,
                    Code = x.PhoneType.Code,
                    Name = x.PhoneType.Name,
                    StatusId = x.PhoneType.StatusId,
                    Used = x.PhoneType.Used,
                    RowId = x.PhoneType.RowId,
                },
            }).FirstOrDefaultAsync();

            if (CustomerPhone == null)
                return null;

            return CustomerPhone;
        }
        public async Task<bool> Create(CustomerPhone CustomerPhone)
        {
            CustomerPhoneDAO CustomerPhoneDAO = new CustomerPhoneDAO();
            CustomerPhoneDAO.Id = CustomerPhone.Id;
            CustomerPhoneDAO.CustomerId = CustomerPhone.CustomerId;
            CustomerPhoneDAO.Phone = CustomerPhone.Phone;
            CustomerPhoneDAO.PhoneTypeId = CustomerPhone.PhoneTypeId;
            DataContext.CustomerPhone.Add(CustomerPhoneDAO);
            await DataContext.SaveChangesAsync();
            CustomerPhone.Id = CustomerPhoneDAO.Id;
            await SaveReference(CustomerPhone);
            return true;
        }

        public async Task<bool> Update(CustomerPhone CustomerPhone)
        {
            CustomerPhoneDAO CustomerPhoneDAO = DataContext.CustomerPhone.Where(x => x.Id == CustomerPhone.Id).FirstOrDefault();
            if (CustomerPhoneDAO == null)
                return false;
            CustomerPhoneDAO.Id = CustomerPhone.Id;
            CustomerPhoneDAO.CustomerId = CustomerPhone.CustomerId;
            CustomerPhoneDAO.Phone = CustomerPhone.Phone;
            CustomerPhoneDAO.PhoneTypeId = CustomerPhone.PhoneTypeId;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerPhone);
            return true;
        }

        public async Task<bool> Delete(CustomerPhone CustomerPhone)
        {
            await DataContext.CustomerPhone.Where(x => x.Id == CustomerPhone.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerPhone> CustomerPhones)
        {
            List<CustomerPhoneDAO> CustomerPhoneDAOs = new List<CustomerPhoneDAO>();
            foreach (CustomerPhone CustomerPhone in CustomerPhones)
            {
                CustomerPhoneDAO CustomerPhoneDAO = new CustomerPhoneDAO();
                CustomerPhoneDAO.Id = CustomerPhone.Id;
                CustomerPhoneDAO.CustomerId = CustomerPhone.CustomerId;
                CustomerPhoneDAO.Phone = CustomerPhone.Phone;
                CustomerPhoneDAO.PhoneTypeId = CustomerPhone.PhoneTypeId;
                CustomerPhoneDAOs.Add(CustomerPhoneDAO);
            }
            await DataContext.BulkMergeAsync(CustomerPhoneDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerPhone> CustomerPhones)
        {
            List<long> Ids = CustomerPhones.Select(x => x.Id).ToList();
            await DataContext.CustomerPhone
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(CustomerPhone CustomerPhone)
        {
        }
        
    }
}
