using CRM.Common;
using CRM.Helpers;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enums;

namespace CRM.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> Count(CustomerFilter CustomerFilter);
        Task<List<Customer>> List(CustomerFilter CustomerFilter);
        Task<List<Customer>> List(List<long> Ids);
        Task<Customer> Get(long Id);
        Task<bool> Create(Customer Customer);
        Task<bool> Update(Customer Customer);
        Task<bool> Delete(Customer Customer);
        Task<bool> BulkMerge(List<Customer> Customers);
        Task<bool> BulkDelete(List<Customer> Customers);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private DataContext DataContext;
        public CustomerRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerDAO> DynamicFilter(IQueryable<CustomerDAO> query, CustomerFilter filter)
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
            {
                if (filter.CustomerTypeId != null && filter.CustomerTypeId.Equal.HasValue)
                {
                    if (filter.CustomerTypeId.Equal.Value == CustomerTypeEnum.COMPANY.Id)
                    {
                        query = query.Where(x => x.Company.Name, filter.Name);
                    }
                }
                else
                    query = query.Where(q => q.Name, filter.Name);
            } 
            if (filter.Phone != null && filter.Phone.HasValue)
            {
                var retailQuery = query.Where(x => x.CustomerTypeId == CustomerTypeEnum.RETAIL.Id).Where(x => x.Phone, filter.Phone);
                var companyQuery = from q in query
                                   join ce in DataContext.CustomerPhone on q.Id equals ce.CustomerId
                                   where q.CustomerTypeId == CustomerTypeEnum.COMPANY.Id &&
                                   ce.Phone.Contains(filter.Phone.Contain)
                                   select q;
                query = retailQuery.Union(companyQuery);
            }
            if (filter.Address != null && filter.Address.HasValue)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.NationId != null && filter.NationId.HasValue)
                query = query.Where(q => q.NationId.HasValue).Where(q => q.NationId, filter.NationId);
            if (filter.ProvinceId != null && filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.DistrictId != null && filter.DistrictId.HasValue)
                query = query.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, filter.DistrictId);
            if (filter.WardId != null && filter.WardId.HasValue)
                query = query.Where(q => q.WardId.HasValue).Where(q => q.WardId, filter.WardId);
            if (filter.CustomerTypeId != null && filter.CustomerTypeId.HasValue)
                query = query.Where(q => q.CustomerTypeId, filter.CustomerTypeId);
            if (filter.Birthday != null && filter.Birthday.HasValue)
                query = query.Where(q => q.Birthday == null).Union(query.Where(q => q.Birthday.HasValue).Where(q => q.Birthday, filter.Birthday));
            if (filter.Email != null && filter.Email.HasValue)
            {
                var retailQuery = query.Where(x => x.CustomerTypeId == CustomerTypeEnum.RETAIL.Id).Where(x => x.Email, filter.Email);
                var companyQuery = from q in query
                                   join ce in DataContext.CustomerEmail on q.Id equals ce.CustomerId
                                   where q.CustomerTypeId == CustomerTypeEnum.COMPANY.Id &&
                                   ce.Email.Contains(filter.Email.Contain)
                                   select q;
                query = retailQuery.Union(companyQuery);
            }
            if (filter.ProfessionId != null && filter.ProfessionId.HasValue)
                query = query.Where(q => q.ProfessionId.HasValue).Where(q => q.ProfessionId, filter.ProfessionId);
            if (filter.CustomerResourceId != null && filter.CustomerResourceId.HasValue)
                query = query.Where(q => q.CustomerResourceId.HasValue).Where(q => q.CustomerResourceId, filter.CustomerResourceId);
            if (filter.SexId != null && filter.SexId.HasValue)
                query = query.Where(q => q.SexId.HasValue).Where(q => q.SexId, filter.SexId);
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, filter.CompanyId);
            if (filter.ParentCompanyId != null && filter.ParentCompanyId.HasValue)
                query = query.Where(q => q.ParentCompanyId.HasValue).Where(q => q.ParentCompanyId, filter.ParentCompanyId);
            if (filter.TaxCode != null && filter.TaxCode.HasValue)
                query = query.Where(q => q.TaxCode, filter.TaxCode);
            if (filter.Fax != null && filter.Fax.HasValue)
                query = query.Where(q => q.Fax, filter.Fax);
            if (filter.Website != null && filter.Website.HasValue)
                query = query.Where(q => q.Website, filter.Website);
            if (filter.NumberOfEmployee != null && filter.NumberOfEmployee.HasValue)
                query = query.Where(q => q.NumberOfEmployee.HasValue).Where(q => q.NumberOfEmployee, filter.NumberOfEmployee);
            if (filter.BusinessTypeId != null && filter.BusinessTypeId.HasValue)
                query = query.Where(q => q.BusinessTypeId.HasValue).Where(q => q.BusinessTypeId, filter.BusinessTypeId);
            if (filter.Investment != null && filter.Investment.HasValue)
                query = query.Where(q => q.Investment.HasValue).Where(q => q.Investment, filter.Investment);
            if (filter.RevenueAnnual != null && filter.RevenueAnnual.HasValue)
                query = query.Where(q => q.RevenueAnnual.HasValue).Where(q => q.RevenueAnnual, filter.RevenueAnnual);
            if (filter.Descreption != null && filter.Descreption.HasValue)
                query = query.Where(q => q.Descreption, filter.Descreption);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
         if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.CustomerGroupingId != null)
            {
                if (filter.CustomerGroupingId.Equal != null)
                {
                    CustomerGroupingDAO CustomerGroupingDAO = DataContext.CustomerGrouping
                        .Where(o => o.Id == filter.CustomerGroupingId.Equal.Value).FirstOrDefault();
                    query = from q in query
                            join ppg in DataContext.CustomerCustomerGroupingMapping on q.Id equals ppg.CustomerId
                            join pg in DataContext.CustomerGrouping on ppg.CustomerGroupingId equals pg.Id
                            where pg.Path.StartsWith(CustomerGroupingDAO.Path)
                            select q;
                }
                if (filter.CustomerGroupingId.NotEqual != null)
                {
                    CustomerGroupingDAO CustomerGroupingDAO = DataContext.CustomerGrouping
                        .Where(o => o.Id == filter.CustomerGroupingId.NotEqual.Value).FirstOrDefault();
                    query = from q in query
                            join ppg in DataContext.CustomerCustomerGroupingMapping on q.Id equals ppg.CustomerId
                            join pg in DataContext.CustomerGrouping on ppg.CustomerGroupingId equals pg.Id
                            where !pg.Path.StartsWith(CustomerGroupingDAO.Path)
                            select q;
                }
                if (filter.CustomerGroupingId.In != null)
                {
                    List<CustomerGroupingDAO> CustomerGroupingDAOs = DataContext.CustomerGrouping
                        .Where(o => o.DeletedAt == null).ToList();
                    List<CustomerGroupingDAO> Parents = CustomerGroupingDAOs.Where(o => filter.CustomerGroupingId.In.Contains(o.Id)).ToList();
                    List<CustomerGroupingDAO> Branches = CustomerGroupingDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                    List<long> CustomerGroupingIds = Branches.Select(x => x.Id).ToList();
                    query = from q in query
                            join ppg in DataContext.CustomerCustomerGroupingMapping on q.Id equals ppg.CustomerId
                            join pg in DataContext.CustomerGrouping on ppg.CustomerGroupingId equals pg.Id
                            where CustomerGroupingIds.Contains(pg.Id)
                            select q;
                }
                if (filter.CustomerGroupingId.NotIn != null)
                {
                    List<CustomerGroupingDAO> CustomerGroupingDAOs = DataContext.CustomerGrouping
                        .Where(o => o.DeletedAt == null).ToList();
                    List<CustomerGroupingDAO> Parents = CustomerGroupingDAOs.Where(o => filter.CustomerGroupingId.NotIn.Contains(o.Id)).ToList();
                    List<CustomerGroupingDAO> Branches = CustomerGroupingDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                    List<long> CustomerGroupingIds = Branches.Select(x => x.Id).ToList();
                    query = from q in query
                            join ppg in DataContext.CustomerCustomerGroupingMapping on q.Id equals ppg.CustomerId
                            join pg in DataContext.CustomerGrouping on ppg.CustomerGroupingId equals pg.Id
                            where !CustomerGroupingIds.Contains(pg.Id)
                            select q;
                }
            }
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerDAO> OrFilter(IQueryable<CustomerDAO> query, CustomerFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerDAO> initQuery = query.Where(q => false);
            foreach (CustomerFilter CustomerFilter in filter.OrFilter)
            {
                IQueryable<CustomerDAO> queryable = query;
                if (CustomerFilter.Id != null && CustomerFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerFilter.Id);
                if (CustomerFilter.Code != null && CustomerFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CustomerFilter.Code);
                if (CustomerFilter.Name != null && CustomerFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerFilter.Name);
                if (CustomerFilter.Phone != null && CustomerFilter.Phone.HasValue)
                    queryable = queryable.Where(q => q.Phone, CustomerFilter.Phone);
                if (CustomerFilter.Address != null && CustomerFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, CustomerFilter.Address);
                if (CustomerFilter.NationId != null && CustomerFilter.NationId.HasValue)
                    queryable = queryable.Where(q => q.NationId.HasValue).Where(q => q.NationId, CustomerFilter.NationId);
                if (CustomerFilter.ProvinceId != null && CustomerFilter.ProvinceId.HasValue)
                    queryable = queryable.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, CustomerFilter.ProvinceId);
                if (CustomerFilter.DistrictId != null && CustomerFilter.DistrictId.HasValue)
                    queryable = queryable.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, CustomerFilter.DistrictId);
                if (CustomerFilter.WardId != null && CustomerFilter.WardId.HasValue)
                    queryable = queryable.Where(q => q.WardId.HasValue).Where(q => q.WardId, CustomerFilter.WardId);
                if (CustomerFilter.CustomerTypeId != null && CustomerFilter.CustomerTypeId.HasValue)
                    queryable = queryable.Where(q => q.CustomerTypeId, CustomerFilter.CustomerTypeId);
                if (CustomerFilter.Birthday != null && CustomerFilter.Birthday.HasValue)
                    queryable = queryable.Where(q => q.Birthday.HasValue).Where(q => q.Birthday, CustomerFilter.Birthday);
                if (CustomerFilter.Email != null && CustomerFilter.Email.HasValue)
                    queryable = queryable.Where(q => q.Email, CustomerFilter.Email);
                if (CustomerFilter.ProfessionId != null && CustomerFilter.ProfessionId.HasValue)
                    queryable = queryable.Where(q => q.ProfessionId.HasValue).Where(q => q.ProfessionId, CustomerFilter.ProfessionId);
                if (CustomerFilter.CustomerResourceId != null && CustomerFilter.CustomerResourceId.HasValue)
                    queryable = queryable.Where(q => q.CustomerResourceId.HasValue).Where(q => q.CustomerResourceId, CustomerFilter.CustomerResourceId);
                if (CustomerFilter.SexId != null && CustomerFilter.SexId.HasValue)
                    queryable = queryable.Where(q => q.SexId.HasValue).Where(q => q.SexId, CustomerFilter.SexId);
                if (CustomerFilter.StatusId != null && CustomerFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, CustomerFilter.StatusId);
                if (CustomerFilter.CompanyId != null && CustomerFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, CustomerFilter.CompanyId);
                if (CustomerFilter.ParentCompanyId != null && CustomerFilter.ParentCompanyId.HasValue)
                    queryable = queryable.Where(q => q.ParentCompanyId.HasValue).Where(q => q.ParentCompanyId, CustomerFilter.ParentCompanyId);
                if (CustomerFilter.TaxCode != null && CustomerFilter.TaxCode.HasValue)
                    queryable = queryable.Where(q => q.TaxCode, CustomerFilter.TaxCode);
                if (CustomerFilter.Fax != null && CustomerFilter.Fax.HasValue)
                    queryable = queryable.Where(q => q.Fax, CustomerFilter.Fax);
                if (CustomerFilter.Website != null && CustomerFilter.Website.HasValue)
                    queryable = queryable.Where(q => q.Website, CustomerFilter.Website);
                if (CustomerFilter.NumberOfEmployee != null && CustomerFilter.NumberOfEmployee.HasValue)
                    queryable = queryable.Where(q => q.NumberOfEmployee.HasValue).Where(q => q.NumberOfEmployee, CustomerFilter.NumberOfEmployee);
                if (CustomerFilter.BusinessTypeId != null && CustomerFilter.BusinessTypeId.HasValue)
                    queryable = queryable.Where(q => q.BusinessTypeId.HasValue).Where(q => q.BusinessTypeId, CustomerFilter.BusinessTypeId);
                if (CustomerFilter.Investment != null && CustomerFilter.Investment.HasValue)
                    queryable = queryable.Where(q => q.Investment.HasValue).Where(q => q.Investment, CustomerFilter.Investment);
                if (CustomerFilter.RevenueAnnual != null && CustomerFilter.RevenueAnnual.HasValue)
                    queryable = queryable.Where(q => q.RevenueAnnual.HasValue).Where(q => q.RevenueAnnual, CustomerFilter.RevenueAnnual);
                if (CustomerFilter.Descreption != null && CustomerFilter.Descreption.HasValue)
                    queryable = queryable.Where(q => q.Descreption, CustomerFilter.Descreption);
                if (CustomerFilter.AppUserId != null && CustomerFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, CustomerFilter.AppUserId);
                if (CustomerFilter.CreatorId != null && CustomerFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CustomerFilter.CreatorId);
               if (CustomerFilter.RowId != null && CustomerFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, CustomerFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<CustomerDAO> DynamicOrder(IQueryable<CustomerDAO> query, CustomerFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CustomerOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CustomerOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case CustomerOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case CustomerOrder.Nation:
                            query = query.OrderBy(q => q.NationId);
                            break;
                        case CustomerOrder.Province:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case CustomerOrder.District:
                            query = query.OrderBy(q => q.DistrictId);
                            break;
                        case CustomerOrder.Ward:
                            query = query.OrderBy(q => q.WardId);
                            break;
                        case CustomerOrder.CustomerType:
                            query = query.OrderBy(q => q.CustomerTypeId);
                            break;
                        case CustomerOrder.Birthday:
                            query = query.OrderBy(q => q.Birthday);
                            break;
                        case CustomerOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case CustomerOrder.Profession:
                            query = query.OrderBy(q => q.ProfessionId);
                            break;
                        case CustomerOrder.CustomerResource:
                            query = query.OrderBy(q => q.CustomerResourceId);
                            break;
                        case CustomerOrder.Sex:
                            query = query.OrderBy(q => q.SexId);
                            break;
                        case CustomerOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case CustomerOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case CustomerOrder.ParentCompany:
                            query = query.OrderBy(q => q.ParentCompanyId);
                            break;
                        case CustomerOrder.TaxCode:
                            query = query.OrderBy(q => q.TaxCode);
                            break;
                        case CustomerOrder.Fax:
                            query = query.OrderBy(q => q.Fax);
                            break;
                        case CustomerOrder.Website:
                            query = query.OrderBy(q => q.Website);
                            break;
                        case CustomerOrder.NumberOfEmployee:
                            query = query.OrderBy(q => q.NumberOfEmployee);
                            break;
                        case CustomerOrder.BusinessType:
                            query = query.OrderBy(q => q.BusinessTypeId);
                            break;
                        case CustomerOrder.Investment:
                            query = query.OrderBy(q => q.Investment);
                            break;
                        case CustomerOrder.RevenueAnnual:
                            query = query.OrderBy(q => q.RevenueAnnual);
                            break;
                        case CustomerOrder.IsSupplier:
                            query = query.OrderBy(q => q.IsSupplier);
                            break;
                        case CustomerOrder.Descreption:
                            query = query.OrderBy(q => q.Descreption);
                            break;
                        case CustomerOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case CustomerOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CustomerOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case CustomerOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CustomerOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CustomerOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case CustomerOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case CustomerOrder.Nation:
                            query = query.OrderByDescending(q => q.NationId);
                            break;
                        case CustomerOrder.Province:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case CustomerOrder.District:
                            query = query.OrderByDescending(q => q.DistrictId);
                            break;
                        case CustomerOrder.Ward:
                            query = query.OrderByDescending(q => q.WardId);
                            break;
                        case CustomerOrder.CustomerType:
                            query = query.OrderByDescending(q => q.CustomerTypeId);
                            break;
                        case CustomerOrder.Birthday:
                            query = query.OrderByDescending(q => q.Birthday);
                            break;
                        case CustomerOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case CustomerOrder.Profession:
                            query = query.OrderByDescending(q => q.ProfessionId);
                            break;
                        case CustomerOrder.CustomerResource:
                            query = query.OrderByDescending(q => q.CustomerResourceId);
                            break;
                        case CustomerOrder.Sex:
                            query = query.OrderByDescending(q => q.SexId);
                            break;
                        case CustomerOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case CustomerOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case CustomerOrder.ParentCompany:
                            query = query.OrderByDescending(q => q.ParentCompanyId);
                            break;
                        case CustomerOrder.TaxCode:
                            query = query.OrderByDescending(q => q.TaxCode);
                            break;
                        case CustomerOrder.Fax:
                            query = query.OrderByDescending(q => q.Fax);
                            break;
                        case CustomerOrder.Website:
                            query = query.OrderByDescending(q => q.Website);
                            break;
                        case CustomerOrder.NumberOfEmployee:
                            query = query.OrderByDescending(q => q.NumberOfEmployee);
                            break;
                        case CustomerOrder.BusinessType:
                            query = query.OrderByDescending(q => q.BusinessTypeId);
                            break;
                        case CustomerOrder.Investment:
                            query = query.OrderByDescending(q => q.Investment);
                            break;
                        case CustomerOrder.RevenueAnnual:
                            query = query.OrderByDescending(q => q.RevenueAnnual);
                            break;
                        case CustomerOrder.IsSupplier:
                            query = query.OrderByDescending(q => q.IsSupplier);
                            break;
                        case CustomerOrder.Descreption:
                            query = query.OrderByDescending(q => q.Descreption);
                            break;
                        case CustomerOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case CustomerOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CustomerOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case CustomerOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Customer>> DynamicSelect(IQueryable<CustomerDAO> query, CustomerFilter filter)
        {
            List<Customer> Customers = await query.Select(q => new Customer()
            {
                Id = filter.Selects.Contains(CustomerSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CustomerSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CustomerSelect.Name) ? q.Name : default(string),
                Phone = filter.Selects.Contains(CustomerSelect.Phone) ? q.Phone : default(string),
                Address = filter.Selects.Contains(CustomerSelect.Address) ? q.Address : default(string),
                NationId = filter.Selects.Contains(CustomerSelect.Nation) ? q.NationId : default(long?),
                ProvinceId = filter.Selects.Contains(CustomerSelect.Province) ? q.ProvinceId : default(long?),
                DistrictId = filter.Selects.Contains(CustomerSelect.District) ? q.DistrictId : default(long?),
                WardId = filter.Selects.Contains(CustomerSelect.Ward) ? q.WardId : default(long?),
                CustomerTypeId = filter.Selects.Contains(CustomerSelect.CustomerType) ? q.CustomerTypeId : default(long),
                Birthday = filter.Selects.Contains(CustomerSelect.Birthday) ? q.Birthday : default(DateTime?),
                Email = filter.Selects.Contains(CustomerSelect.Email) ? q.Email : default(string),
                ProfessionId = filter.Selects.Contains(CustomerSelect.Profession) ? q.ProfessionId : default(long?),
                CustomerResourceId = filter.Selects.Contains(CustomerSelect.CustomerResource) ? q.CustomerResourceId : default(long?),
                SexId = filter.Selects.Contains(CustomerSelect.Sex) ? q.SexId : default(long?),
                StatusId = filter.Selects.Contains(CustomerSelect.Status) ? q.StatusId : default(long),
                CompanyId = filter.Selects.Contains(CustomerSelect.Company) ? q.CompanyId : default(long?),
                ParentCompanyId = filter.Selects.Contains(CustomerSelect.ParentCompany) ? q.ParentCompanyId : default(long?),
                TaxCode = filter.Selects.Contains(CustomerSelect.TaxCode) ? q.TaxCode : default(string),
                Fax = filter.Selects.Contains(CustomerSelect.Fax) ? q.Fax : default(string),
                Website = filter.Selects.Contains(CustomerSelect.Website) ? q.Website : default(string),
                NumberOfEmployee = filter.Selects.Contains(CustomerSelect.NumberOfEmployee) ? q.NumberOfEmployee : default(long?),
                BusinessTypeId = filter.Selects.Contains(CustomerSelect.BusinessType) ? q.BusinessTypeId : default(long?),
                Investment = filter.Selects.Contains(CustomerSelect.Investment) ? q.Investment : default(decimal?),
                RevenueAnnual = filter.Selects.Contains(CustomerSelect.RevenueAnnual) ? q.RevenueAnnual : default(decimal?),
                IsSupplier = filter.Selects.Contains(CustomerSelect.IsSupplier) ? q.IsSupplier : default(bool?),
                Descreption = filter.Selects.Contains(CustomerSelect.Descreption) ? q.Descreption : default(string),
                AppUserId = filter.Selects.Contains(CustomerSelect.AppUser) ? q.AppUserId : default(long),
                CreatorId = filter.Selects.Contains(CustomerSelect.Creator) ? q.CreatorId : default(long),
                Used = filter.Selects.Contains(CustomerSelect.Used) ? q.Used : default(bool),
                RowId = filter.Selects.Contains(CustomerSelect.Row) ? q.RowId : default(Guid),
                BusinessType = filter.Selects.Contains(CustomerSelect.BusinessType) && q.BusinessType != null ? new BusinessType
                {
                    Id = q.BusinessType.Id,
                    Code = q.BusinessType.Code,
                    Name = q.BusinessType.Name,
                } : null,
                Company = filter.Selects.Contains(CustomerSelect.Company) && q.Company != null ? new Company
                {
                    Id = q.Company.Id,
                    Name = q.Company.Name,
                    Phone = q.Company.Phone,
                    FAX = q.Company.FAX,
                    PhoneOther = q.Company.PhoneOther,
                    Email = q.Company.Email,
                    EmailOther = q.Company.EmailOther,
                    ZIPCode = q.Company.ZIPCode,
                    Revenue = q.Company.Revenue,
                    Website = q.Company.Website,
                    Address = q.Company.Address,
                    NationId = q.Company.NationId,
                    ProvinceId = q.Company.ProvinceId,
                    DistrictId = q.Company.DistrictId,
                    NumberOfEmployee = q.Company.NumberOfEmployee,
                    RefuseReciveEmail = q.Company.RefuseReciveEmail,
                    RefuseReciveSMS = q.Company.RefuseReciveSMS,
                    CustomerLeadId = q.Company.CustomerLeadId,
                    ParentId = q.Company.ParentId,
                    Path = q.Company.Path,
                    Level = q.Company.Level,
                    ProfessionId = q.Company.ProfessionId,
                    AppUserId = q.Company.AppUserId,
                    CreatorId = q.Company.CreatorId,
                    CurrencyId = q.Company.CurrencyId,
                    CompanyStatusId = q.Company.CompanyStatusId,
                    Description = q.Company.Description,
                    RowId = q.Company.RowId,
                } : null,
                AppUser = filter.Selects.Contains(CustomerSelect.AppUser) && q.AppUser != null ? new AppUser
                {
                    Id = q.AppUser.Id,
                    Username = q.AppUser.Username,
                    DisplayName = q.AppUser.DisplayName,
                    Address = q.AppUser.Address,
                    Email = q.AppUser.Email,
                    Phone = q.AppUser.Phone,
                    SexId = q.AppUser.SexId,
                    Birthday = q.AppUser.Birthday,
                    Avatar = q.AppUser.Avatar,
                    Department = q.AppUser.Department,
                    OrganizationId = q.AppUser.OrganizationId,
                    Longitude = q.AppUser.Longitude,
                    Latitude = q.AppUser.Latitude,
                    StatusId = q.AppUser.StatusId,
                    RowId = q.AppUser.RowId,
                    Used = q.AppUser.Used,
                } : null,
                Creator = filter.Selects.Contains(CustomerSelect.Creator) && q.Creator != null ? new AppUser
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
                CustomerResource = filter.Selects.Contains(CustomerSelect.CustomerResource) && q.CustomerResource != null ? new CustomerResource
                {
                    Id = q.CustomerResource.Id,
                    Code = q.CustomerResource.Code,
                    Name = q.CustomerResource.Name,
                    StatusId = q.CustomerResource.StatusId,
                    Description = q.CustomerResource.Description,
                    Used = q.CustomerResource.Used,
                    RowId = q.CustomerResource.RowId,
                } : null,
                CustomerType = filter.Selects.Contains(CustomerSelect.CustomerType) && q.CustomerType != null ? new CustomerType
                {
                    Id = q.CustomerType.Id,
                    Code = q.CustomerType.Code,
                    Name = q.CustomerType.Name,
                } : null,
                District = filter.Selects.Contains(CustomerSelect.District) && q.District != null ? new District
                {
                    Id = q.District.Id,
                    Code = q.District.Code,
                    Name = q.District.Name,
                    Priority = q.District.Priority,
                    ProvinceId = q.District.ProvinceId,
                    StatusId = q.District.StatusId,
                    RowId = q.District.RowId,
                    Used = q.District.Used,
                } : null,
                Nation = filter.Selects.Contains(CustomerSelect.Nation) && q.Nation != null ? new Nation
                {
                    Id = q.Nation.Id,
                    Code = q.Nation.Code,
                    Name = q.Nation.Name,
                    Priority = q.Nation.Priority,
                    StatusId = q.Nation.StatusId,
                    Used = q.Nation.Used,
                    RowId = q.Nation.RowId,
                } : null,
             
                ParentCompany = filter.Selects.Contains(CustomerSelect.ParentCompany) && q.ParentCompany != null ? new Company
                {
                    Id = q.ParentCompany.Id,
                    Name = q.ParentCompany.Name,
                    Phone = q.ParentCompany.Phone,
                    FAX = q.ParentCompany.FAX,
                    PhoneOther = q.ParentCompany.PhoneOther,
                    Email = q.ParentCompany.Email,
                    EmailOther = q.ParentCompany.EmailOther,
                    ZIPCode = q.ParentCompany.ZIPCode,
                    Revenue = q.ParentCompany.Revenue,
                    Website = q.ParentCompany.Website,
                    Address = q.ParentCompany.Address,
                    NationId = q.ParentCompany.NationId,
                    ProvinceId = q.ParentCompany.ProvinceId,
                    DistrictId = q.ParentCompany.DistrictId,
                    NumberOfEmployee = q.ParentCompany.NumberOfEmployee,
                    RefuseReciveEmail = q.ParentCompany.RefuseReciveEmail,
                    RefuseReciveSMS = q.ParentCompany.RefuseReciveSMS,
                    CustomerLeadId = q.ParentCompany.CustomerLeadId,
                    ParentId = q.ParentCompany.ParentId,
                    Path = q.ParentCompany.Path,
                    Level = q.ParentCompany.Level,
                    ProfessionId = q.ParentCompany.ProfessionId,
                    AppUserId = q.ParentCompany.AppUserId,
                    CreatorId = q.ParentCompany.CreatorId,
                    CurrencyId = q.ParentCompany.CurrencyId,
                    CompanyStatusId = q.ParentCompany.CompanyStatusId,
                    Description = q.ParentCompany.Description,
                    RowId = q.ParentCompany.RowId,
                } : null,
                Profession = filter.Selects.Contains(CustomerSelect.Profession) && q.Profession != null ? new Profession
                {
                    Id = q.Profession.Id,
                    Code = q.Profession.Code,
                    Name = q.Profession.Name,
                    StatusId = q.Profession.StatusId,
                    RowId = q.Profession.RowId,
                    Used = q.Profession.Used,
                } : null,
                Province = filter.Selects.Contains(CustomerSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Code = q.Province.Code,
                    Name = q.Province.Name,
                    Priority = q.Province.Priority,
                    StatusId = q.Province.StatusId,
                    RowId = q.Province.RowId,
                    Used = q.Province.Used,
                } : null,
                Sex = filter.Selects.Contains(CustomerSelect.Sex) && q.Sex != null ? new Sex
                {
                    Id = q.Sex.Id,
                    Code = q.Sex.Code,
                    Name = q.Sex.Name,
                } : null,
                Status = filter.Selects.Contains(CustomerSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                Ward = filter.Selects.Contains(CustomerSelect.Ward) && q.Ward != null ? new Ward
                {
                    Id = q.Ward.Id,
                    Code = q.Ward.Code,
                    Name = q.Ward.Name,
                    Priority = q.Ward.Priority,
                    DistrictId = q.Ward.DistrictId,
                    StatusId = q.Ward.StatusId,
                    RowId = q.Ward.RowId,
                    Used = q.Ward.Used,
                } : null,
            }).ToListAsync();

            var Ids = Customers.Select(x => x.Id).ToList();
            var CustomerPhones = await DataContext.CustomerPhone.Where(x => Ids.Contains(x.CustomerId)).Select(x => new CustomerPhone
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                PhoneTypeId = x.PhoneTypeId,
                Phone = x.Phone,
                PhoneType = x.PhoneType == null ? null : new PhoneType
                {
                    Id = x.PhoneType.Id,
                    Code = x.PhoneType.Code,
                    Name = x.PhoneType.Name,
                }
            }).ToListAsync();
            var CustomerEmails = await DataContext.CustomerEmail.Where(x => Ids.Contains(x.CustomerId)).Select(x => new CustomerEmail
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                EmailTypeId = x.EmailTypeId,
                Email = x.Email,
                EmailType = x.EmailType == null ? null : new EmailType
                {
                    Id = x.EmailType.Id,
                    Code = x.EmailType.Code,
                    Name = x.EmailType.Name,
                }
            }).ToListAsync();
            foreach (var Customer in Customers)
            {
                CustomerPhone CustomerPhone = CustomerPhones.Where(x => x.CustomerId == Customer.Id).FirstOrDefault();
                if (CustomerPhone != null)
                    Customer.CustomerPhones = new List<CustomerPhone> { CustomerPhone };
                CustomerEmail CustomerEmail = CustomerEmails.Where(x => x.CustomerId == Customer.Id).FirstOrDefault();
                if (CustomerEmail != null)
                    Customer.CustomerEmails = new List<CustomerEmail> { CustomerEmail };
            }
            return Customers;
        }

        public async Task<int> Count(CustomerFilter filter)
        {
            IQueryable<CustomerDAO> Customers = DataContext.Customer.AsNoTracking();
            Customers = DynamicFilter(Customers, filter);
            return await Customers.CountAsync();
        }

        public async Task<List<Customer>> List(CustomerFilter filter)
        {
            if (filter == null) return new List<Customer>();
            IQueryable<CustomerDAO> CustomerDAOs = DataContext.Customer.AsNoTracking();
            CustomerDAOs = DynamicFilter(CustomerDAOs, filter);
            CustomerDAOs = DynamicOrder(CustomerDAOs, filter);
            List<Customer> Customers = await DynamicSelect(CustomerDAOs, filter);
            return Customers;
        }

        public async Task<List<Customer>> List(List<long> Ids)
        {
            List<Customer> Customers = await DataContext.Customer.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Customer()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Phone = x.Phone,
                Address = x.Address,
                NationId = x.NationId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                WardId = x.WardId,
                CustomerTypeId = x.CustomerTypeId,
                Birthday = x.Birthday,
                Email = x.Email,
                ProfessionId = x.ProfessionId,
                CustomerResourceId = x.CustomerResourceId,
                SexId = x.SexId,
                StatusId = x.StatusId,
                CompanyId = x.CompanyId,
                ParentCompanyId = x.ParentCompanyId,
                TaxCode = x.TaxCode,
                Fax = x.Fax,
                Website = x.Website,
                NumberOfEmployee = x.NumberOfEmployee,
                BusinessTypeId = x.BusinessTypeId,
                Investment = x.Investment,
                RevenueAnnual = x.RevenueAnnual,
                IsSupplier = x.IsSupplier,
                Descreption = x.Descreption,
                AppUserId = x.AppUserId,
                CreatorId = x.CreatorId,
                Used = x.Used,
                RowId = x.RowId,
                BusinessType = x.BusinessType == null ? null : new BusinessType
                {
                    Id = x.BusinessType.Id,
                    Code = x.BusinessType.Code,
                    Name = x.BusinessType.Name,
                },
                Company = x.Company == null ? null : new Company
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    Phone = x.Company.Phone,
                    FAX = x.Company.FAX,
                    PhoneOther = x.Company.PhoneOther,
                    Email = x.Company.Email,
                    EmailOther = x.Company.EmailOther,
                    ZIPCode = x.Company.ZIPCode,
                    Revenue = x.Company.Revenue,
                    Website = x.Company.Website,
                    Address = x.Company.Address,
                    NationId = x.Company.NationId,
                    ProvinceId = x.Company.ProvinceId,
                    DistrictId = x.Company.DistrictId,
                    NumberOfEmployee = x.Company.NumberOfEmployee,
                    RefuseReciveEmail = x.Company.RefuseReciveEmail,
                    RefuseReciveSMS = x.Company.RefuseReciveSMS,
                    CustomerLeadId = x.Company.CustomerLeadId,
                    ParentId = x.Company.ParentId,
                    Path = x.Company.Path,
                    Level = x.Company.Level,
                    ProfessionId = x.Company.ProfessionId,
                    AppUserId = x.Company.AppUserId,
                    CreatorId = x.Company.CreatorId,
                    CurrencyId = x.Company.CurrencyId,
                    CompanyStatusId = x.Company.CompanyStatusId,
                    Description = x.Company.Description,
                    RowId = x.Company.RowId,
                },
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
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
                CustomerResource = x.CustomerResource == null ? null : new CustomerResource
                {
                    Id = x.CustomerResource.Id,
                    Code = x.CustomerResource.Code,
                    Name = x.CustomerResource.Name,
                    StatusId = x.CustomerResource.StatusId,
                    Description = x.CustomerResource.Description,
                    Used = x.CustomerResource.Used,
                    RowId = x.CustomerResource.RowId,
                },
                CustomerType = x.CustomerType == null ? null : new CustomerType
                {
                    Id = x.CustomerType.Id,
                    Code = x.CustomerType.Code,
                    Name = x.CustomerType.Name,
                },
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    RowId = x.District.RowId,
                    Used = x.District.Used,
                },
                Nation = x.Nation == null ? null : new Nation
                {
                    Id = x.Nation.Id,
                    Code = x.Nation.Code,
                    Name = x.Nation.Name,
                    Priority = x.Nation.Priority,
                    StatusId = x.Nation.StatusId,
                    Used = x.Nation.Used,
                    RowId = x.Nation.RowId,
                },
              
                ParentCompany = x.ParentCompany == null ? null : new Company
                {
                    Id = x.ParentCompany.Id,
                    Name = x.ParentCompany.Name,
                    Phone = x.ParentCompany.Phone,
                    FAX = x.ParentCompany.FAX,
                    PhoneOther = x.ParentCompany.PhoneOther,
                    Email = x.ParentCompany.Email,
                    EmailOther = x.ParentCompany.EmailOther,
                    ZIPCode = x.ParentCompany.ZIPCode,
                    Revenue = x.ParentCompany.Revenue,
                    Website = x.ParentCompany.Website,
                    Address = x.ParentCompany.Address,
                    NationId = x.ParentCompany.NationId,
                    ProvinceId = x.ParentCompany.ProvinceId,
                    DistrictId = x.ParentCompany.DistrictId,
                    NumberOfEmployee = x.ParentCompany.NumberOfEmployee,
                    RefuseReciveEmail = x.ParentCompany.RefuseReciveEmail,
                    RefuseReciveSMS = x.ParentCompany.RefuseReciveSMS,
                    CustomerLeadId = x.ParentCompany.CustomerLeadId,
                    ParentId = x.ParentCompany.ParentId,
                    Path = x.ParentCompany.Path,
                    Level = x.ParentCompany.Level,
                    ProfessionId = x.ParentCompany.ProfessionId,
                    AppUserId = x.ParentCompany.AppUserId,
                    CreatorId = x.ParentCompany.CreatorId,
                    CurrencyId = x.ParentCompany.CurrencyId,
                    CompanyStatusId = x.ParentCompany.CompanyStatusId,
                    Description = x.ParentCompany.Description,
                    RowId = x.ParentCompany.RowId,
                },
                Profession = x.Profession == null ? null : new Profession
                {
                    Id = x.Profession.Id,
                    Code = x.Profession.Code,
                    Name = x.Profession.Name,
                    StatusId = x.Profession.StatusId,
                    RowId = x.Profession.RowId,
                    Used = x.Profession.Used,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                    RowId = x.Province.RowId,
                    Used = x.Province.Used,
                },
                Sex = x.Sex == null ? null : new Sex
                {
                    Id = x.Sex.Id,
                    Code = x.Sex.Code,
                    Name = x.Sex.Name,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                Ward = x.Ward == null ? null : new Ward
                {
                    Id = x.Ward.Id,
                    Code = x.Ward.Code,
                    Name = x.Ward.Name,
                    Priority = x.Ward.Priority,
                    DistrictId = x.Ward.DistrictId,
                    StatusId = x.Ward.StatusId,
                    RowId = x.Ward.RowId,
                    Used = x.Ward.Used,
                },
            }).ToListAsync();

            List<CustomerCustomerGroupingMapping> CustomerCustomerGroupingMappings = await DataContext.CustomerCustomerGroupingMapping.AsNoTracking()
                .Where(x => Ids.Contains(x.CustomerId))
                .Where(x => x.CustomerGrouping.DeletedAt == null)
                .Select(x => new CustomerCustomerGroupingMapping
                {
                    CustomerId = x.CustomerId,
                    CustomerGroupingId = x.CustomerGroupingId,
                    CustomerGrouping = new CustomerGrouping
                    {
                        Id = x.CustomerGrouping.Id,
                        Code = x.CustomerGrouping.Code,
                        Name = x.CustomerGrouping.Name,
                        CustomerTypeId = x.CustomerGrouping.CustomerTypeId,
                        ParentId = x.CustomerGrouping.ParentId,
                        Path = x.CustomerGrouping.Path,
                        Level = x.CustomerGrouping.Level,
                        StatusId = x.CustomerGrouping.StatusId,
                        Description = x.CustomerGrouping.Description,
                    },
                }).ToListAsync();
            foreach (Customer Customer in Customers)
            {
                Customer.CustomerCustomerGroupingMappings = CustomerCustomerGroupingMappings
                    .Where(x => x.CustomerId == Customer.Id)
                    .ToList();
            }
            List<CustomerEmail> CustomerEmails = await DataContext.CustomerEmail.AsNoTracking()
                .Where(x => Ids.Contains(x.CustomerId))
                .Select(x => new CustomerEmail
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Email = x.Email,
                    EmailTypeId = x.EmailTypeId,
                    EmailType = new EmailType
                    {
                        Id = x.EmailType.Id,
                        Code = x.EmailType.Code,
                        Name = x.EmailType.Name,
                    },
                }).ToListAsync();
            foreach (Customer Customer in Customers)
            {
                Customer.CustomerEmails = CustomerEmails
                    .Where(x => x.CustomerId == Customer.Id)
                    .ToList();
            }
            List<CustomerFeedback> CustomerFeedbacks = await DataContext.CustomerFeedback.AsNoTracking()
                .Where(x => x.CustomerId.HasValue && Ids.Contains(x.CustomerId.Value))
                .Where(x => x.DeletedAt == null)
                .Select(x => new CustomerFeedback
                {
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
                    CustomerFeedbackType = new CustomerFeedbackType
                    {
                        Id = x.CustomerFeedbackType.Id,
                        Code = x.CustomerFeedbackType.Code,
                        Name = x.CustomerFeedbackType.Name,
                    },
                    Status = new Status
                    {
                        Id = x.Status.Id,
                        Code = x.Status.Code,
                        Name = x.Status.Name,
                    },
                }).ToListAsync();
            List<CustomerPhone> CustomerPhones = await DataContext.CustomerPhone.AsNoTracking()
                .Where(x => Ids.Contains(x.CustomerId))
                .Select(x => new CustomerPhone
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Phone = x.Phone,
                    PhoneTypeId = x.PhoneTypeId,
                    PhoneType = new PhoneType
                    {
                        Id = x.PhoneType.Id,
                        Code = x.PhoneType.Code,
                        Name = x.PhoneType.Name,
                        StatusId = x.PhoneType.StatusId,
                        Used = x.PhoneType.Used,
                        RowId = x.PhoneType.RowId,
                    },
                }).ToListAsync();
            foreach (Customer Customer in Customers)
            {
                Customer.CustomerPhones = CustomerPhones
                    .Where(x => x.CustomerId == Customer.Id)
                    .ToList();
            }
            
            return Customers;
        }

        public async Task<Customer> Get(long Id)
        {
            Customer Customer = await DataContext.Customer.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new Customer()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Phone = x.Phone,
                Address = x.Address,
                NationId = x.NationId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                WardId = x.WardId,
                CustomerTypeId = x.CustomerTypeId,
                Birthday = x.Birthday,
                Email = x.Email,
                ProfessionId = x.ProfessionId,
                CustomerResourceId = x.CustomerResourceId,
                SexId = x.SexId,
                StatusId = x.StatusId,
                CompanyId = x.CompanyId,
                ParentCompanyId = x.ParentCompanyId,
                TaxCode = x.TaxCode,
                Fax = x.Fax,
                Website = x.Website,
                NumberOfEmployee = x.NumberOfEmployee,
                BusinessTypeId = x.BusinessTypeId,
                Investment = x.Investment,
                RevenueAnnual = x.RevenueAnnual,
                IsSupplier = x.IsSupplier,
                Descreption = x.Descreption,
                AppUserId = x.AppUserId,
                CreatorId = x.CreatorId,
                Used = x.Used,
                RowId = x.RowId,
                BusinessType = x.BusinessType == null ? null : new BusinessType
                {
                    Id = x.BusinessType.Id,
                    Code = x.BusinessType.Code,
                    Name = x.BusinessType.Name,
                },
                Company = x.Company == null ? null : new Company
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    Phone = x.Company.Phone,
                    FAX = x.Company.FAX,
                    PhoneOther = x.Company.PhoneOther,
                    Email = x.Company.Email,
                    EmailOther = x.Company.EmailOther,
                    ZIPCode = x.Company.ZIPCode,
                    Revenue = x.Company.Revenue,
                    Website = x.Company.Website,
                    Address = x.Company.Address,
                    NationId = x.Company.NationId,
                    ProvinceId = x.Company.ProvinceId,
                    DistrictId = x.Company.DistrictId,
                    NumberOfEmployee = x.Company.NumberOfEmployee,
                    RefuseReciveEmail = x.Company.RefuseReciveEmail,
                    RefuseReciveSMS = x.Company.RefuseReciveSMS,
                    CustomerLeadId = x.Company.CustomerLeadId,
                    ParentId = x.Company.ParentId,
                    Path = x.Company.Path,
                    Level = x.Company.Level,
                    ProfessionId = x.Company.ProfessionId,
                    AppUserId = x.Company.AppUserId,
                    CreatorId = x.Company.CreatorId,
                    CurrencyId = x.Company.CurrencyId,
                    CompanyStatusId = x.Company.CompanyStatusId,
                    Description = x.Company.Description,
                    RowId = x.Company.RowId,
                },
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
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
                CustomerResource = x.CustomerResource == null ? null : new CustomerResource
                {
                    Id = x.CustomerResource.Id,
                    Code = x.CustomerResource.Code,
                    Name = x.CustomerResource.Name,
                    StatusId = x.CustomerResource.StatusId,
                    Description = x.CustomerResource.Description,
                    Used = x.CustomerResource.Used,
                    RowId = x.CustomerResource.RowId,
                },
                CustomerType = x.CustomerType == null ? null : new CustomerType
                {
                    Id = x.CustomerType.Id,
                    Code = x.CustomerType.Code,
                    Name = x.CustomerType.Name,
                },
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    RowId = x.District.RowId,
                    Used = x.District.Used,
                },
                Nation = x.Nation == null ? null : new Nation
                {
                    Id = x.Nation.Id,
                    Code = x.Nation.Code,
                    Name = x.Nation.Name,
                    Priority = x.Nation.Priority,
                    StatusId = x.Nation.StatusId,
                    Used = x.Nation.Used,
                    RowId = x.Nation.RowId,
                },
               
                ParentCompany = x.ParentCompany == null ? null : new Company
                {
                    Id = x.ParentCompany.Id,
                    Name = x.ParentCompany.Name,
                    Phone = x.ParentCompany.Phone,
                    FAX = x.ParentCompany.FAX,
                    PhoneOther = x.ParentCompany.PhoneOther,
                    Email = x.ParentCompany.Email,
                    EmailOther = x.ParentCompany.EmailOther,
                    ZIPCode = x.ParentCompany.ZIPCode,
                    Revenue = x.ParentCompany.Revenue,
                    Website = x.ParentCompany.Website,
                    Address = x.ParentCompany.Address,
                    NationId = x.ParentCompany.NationId,
                    ProvinceId = x.ParentCompany.ProvinceId,
                    DistrictId = x.ParentCompany.DistrictId,
                    NumberOfEmployee = x.ParentCompany.NumberOfEmployee,
                    RefuseReciveEmail = x.ParentCompany.RefuseReciveEmail,
                    RefuseReciveSMS = x.ParentCompany.RefuseReciveSMS,
                    CustomerLeadId = x.ParentCompany.CustomerLeadId,
                    ParentId = x.ParentCompany.ParentId,
                    Path = x.ParentCompany.Path,
                    Level = x.ParentCompany.Level,
                    ProfessionId = x.ParentCompany.ProfessionId,
                    AppUserId = x.ParentCompany.AppUserId,
                    CreatorId = x.ParentCompany.CreatorId,
                    CurrencyId = x.ParentCompany.CurrencyId,
                    CompanyStatusId = x.ParentCompany.CompanyStatusId,
                    Description = x.ParentCompany.Description,
                    RowId = x.ParentCompany.RowId,
                },
                Profession = x.Profession == null ? null : new Profession
                {
                    Id = x.Profession.Id,
                    Code = x.Profession.Code,
                    Name = x.Profession.Name,
                    StatusId = x.Profession.StatusId,
                    RowId = x.Profession.RowId,
                    Used = x.Profession.Used,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                    RowId = x.Province.RowId,
                    Used = x.Province.Used,
                },
                Sex = x.Sex == null ? null : new Sex
                {
                    Id = x.Sex.Id,
                    Code = x.Sex.Code,
                    Name = x.Sex.Name,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                Ward = x.Ward == null ? null : new Ward
                {
                    Id = x.Ward.Id,
                    Code = x.Ward.Code,
                    Name = x.Ward.Name,
                    Priority = x.Ward.Priority,
                    DistrictId = x.Ward.DistrictId,
                    StatusId = x.Ward.StatusId,
                    RowId = x.Ward.RowId,
                    Used = x.Ward.Used,
                },
            }).FirstOrDefaultAsync();

            if (Customer == null)
                return null;
            Customer.CustomerCustomerGroupingMappings = await DataContext.CustomerCustomerGroupingMapping.AsNoTracking()
                .Where(x => x.CustomerId == Customer.Id)
                .Where(x => x.CustomerGrouping.DeletedAt == null)
                .Select(x => new CustomerCustomerGroupingMapping
                {
                    CustomerId = x.CustomerId,
                    CustomerGroupingId = x.CustomerGroupingId,
                    CustomerGrouping = new CustomerGrouping
                    {
                        Id = x.CustomerGrouping.Id,
                        Code = x.CustomerGrouping.Code,
                        Name = x.CustomerGrouping.Name,
                        CustomerTypeId = x.CustomerGrouping.CustomerTypeId,
                        ParentId = x.CustomerGrouping.ParentId,
                        Path = x.CustomerGrouping.Path,
                        Level = x.CustomerGrouping.Level,
                        StatusId = x.CustomerGrouping.StatusId,
                        Description = x.CustomerGrouping.Description,
                    },
                }).ToListAsync();
            Customer.CustomerEmails = await DataContext.CustomerEmail.AsNoTracking()
                .Where(x => x.CustomerId == Customer.Id)
                .Select(x => new CustomerEmail
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Email = x.Email,
                    EmailTypeId = x.EmailTypeId,
                    EmailType = new EmailType
                    {
                        Id = x.EmailType.Id,
                        Code = x.EmailType.Code,
                        Name = x.EmailType.Name,
                    },
                }).ToListAsync();
            Customer.CustomerPhones = await DataContext.CustomerPhone.AsNoTracking()
                .Where(x => x.CustomerId == Customer.Id)
                .Select(x => new CustomerPhone
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Phone = x.Phone,
                    PhoneTypeId = x.PhoneTypeId,
                    PhoneType = new PhoneType
                    {
                        Id = x.PhoneType.Id,
                        Code = x.PhoneType.Code,
                        Name = x.PhoneType.Name,
                        StatusId = x.PhoneType.StatusId,
                        Used = x.PhoneType.Used,
                        RowId = x.PhoneType.RowId,
                    },
                }).ToListAsync();

            return Customer;
        }
        public async Task<bool> Create(Customer Customer)
        {
            CustomerDAO CustomerDAO = new CustomerDAO();
            CustomerDAO.Id = Customer.Id;
            CustomerDAO.Code = Customer.Code;
            CustomerDAO.Name = Customer.Name;
            CustomerDAO.Phone = Customer.Phone;
            CustomerDAO.Address = Customer.Address;
            CustomerDAO.NationId = Customer.NationId;
            CustomerDAO.ProvinceId = Customer.ProvinceId;
            CustomerDAO.DistrictId = Customer.DistrictId;
            CustomerDAO.WardId = Customer.WardId;
            CustomerDAO.CustomerTypeId = Customer.CustomerTypeId;
            CustomerDAO.Birthday = Customer.Birthday;
            CustomerDAO.Email = Customer.Email;
            CustomerDAO.ProfessionId = Customer.ProfessionId;
            CustomerDAO.CustomerResourceId = Customer.CustomerResourceId;
            CustomerDAO.SexId = Customer.SexId;
            CustomerDAO.StatusId = Customer.StatusId;
            CustomerDAO.CompanyId = Customer.CompanyId;
            CustomerDAO.ParentCompanyId = Customer.ParentCompanyId;
            CustomerDAO.TaxCode = Customer.TaxCode;
            CustomerDAO.Fax = Customer.Fax;
            CustomerDAO.Website = Customer.Website;
            CustomerDAO.NumberOfEmployee = Customer.NumberOfEmployee;
            CustomerDAO.BusinessTypeId = Customer.BusinessTypeId;
            CustomerDAO.Investment = Customer.Investment;
            CustomerDAO.RevenueAnnual = Customer.RevenueAnnual;
            CustomerDAO.IsSupplier = Customer.IsSupplier;
            CustomerDAO.Descreption = Customer.Descreption;
            CustomerDAO.AppUserId = Customer.AppUserId;
            CustomerDAO.CreatorId = Customer.CreatorId;
            CustomerDAO.Used = Customer.Used;
            CustomerDAO.RowId = Guid.NewGuid();
            CustomerDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Customer.Add(CustomerDAO);
            await DataContext.SaveChangesAsync();
            Customer.Id = CustomerDAO.Id;
            //tạm
            CustomerDAO.Code = $"KH{CustomerDAO.Id}";
            await DataContext.SaveChangesAsync();
            await SaveReference(Customer);
            return true;
        }

        public async Task<bool> Update(Customer Customer)
        {
            CustomerDAO CustomerDAO = DataContext.Customer.Where(x => x.Id == Customer.Id).FirstOrDefault();
            if (CustomerDAO == null)
                return false;
            CustomerDAO.Id = Customer.Id;
            CustomerDAO.Code = Customer.Code;
            CustomerDAO.Name = Customer.Name;
            CustomerDAO.Phone = Customer.Phone;
            CustomerDAO.Address = Customer.Address;
            CustomerDAO.NationId = Customer.NationId;
            CustomerDAO.ProvinceId = Customer.ProvinceId;
            CustomerDAO.DistrictId = Customer.DistrictId;
            CustomerDAO.WardId = Customer.WardId;
            CustomerDAO.CustomerTypeId = Customer.CustomerTypeId;
            CustomerDAO.Birthday = Customer.Birthday;
            CustomerDAO.Email = Customer.Email;
            CustomerDAO.ProfessionId = Customer.ProfessionId;
            CustomerDAO.CustomerResourceId = Customer.CustomerResourceId;
            CustomerDAO.SexId = Customer.SexId;
            CustomerDAO.StatusId = Customer.StatusId;
            CustomerDAO.CompanyId = Customer.CompanyId;
            CustomerDAO.ParentCompanyId = Customer.ParentCompanyId;
            CustomerDAO.TaxCode = Customer.TaxCode;
            CustomerDAO.Fax = Customer.Fax;
            CustomerDAO.Website = Customer.Website;
            CustomerDAO.NumberOfEmployee = Customer.NumberOfEmployee;
            CustomerDAO.BusinessTypeId = Customer.BusinessTypeId;
            CustomerDAO.Investment = Customer.Investment;
            CustomerDAO.RevenueAnnual = Customer.RevenueAnnual;
            CustomerDAO.IsSupplier = Customer.IsSupplier;
            CustomerDAO.Descreption = Customer.Descreption;
            CustomerDAO.AppUserId = Customer.AppUserId;
            CustomerDAO.CreatorId = Customer.CreatorId;
            CustomerDAO.Used = Customer.Used;
            CustomerDAO.RowId = Customer.RowId;
            CustomerDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Customer);
            return true;
        }

        public async Task<bool> Delete(Customer Customer)
        {
            await DataContext.Customer.Where(x => x.Id == Customer.Id).UpdateFromQueryAsync(x => new CustomerDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<Customer> Customers)
        {
            List<CustomerDAO> CustomerDAOs = new List<CustomerDAO>();
            foreach (Customer Customer in Customers)
            {
                CustomerDAO CustomerDAO = new CustomerDAO();
                CustomerDAO.Id = Customer.Id;
                CustomerDAO.Code = Customer.Code;
                CustomerDAO.Name = Customer.Name;
                CustomerDAO.Phone = Customer.Phone;
                CustomerDAO.Address = Customer.Address;
                CustomerDAO.NationId = Customer.NationId;
                CustomerDAO.ProvinceId = Customer.ProvinceId;
                CustomerDAO.DistrictId = Customer.DistrictId;
                CustomerDAO.WardId = Customer.WardId;
                CustomerDAO.CustomerTypeId = Customer.CustomerTypeId;
                CustomerDAO.Birthday = Customer.Birthday;
                CustomerDAO.Email = Customer.Email;
                CustomerDAO.ProfessionId = Customer.ProfessionId;
                CustomerDAO.CustomerResourceId = Customer.CustomerResourceId;
                CustomerDAO.SexId = Customer.SexId;
                CustomerDAO.StatusId = Customer.StatusId;
                CustomerDAO.CompanyId = Customer.CompanyId;
                CustomerDAO.ParentCompanyId = Customer.ParentCompanyId;
                CustomerDAO.TaxCode = Customer.TaxCode;
                CustomerDAO.Fax = Customer.Fax;
                CustomerDAO.Website = Customer.Website;
                CustomerDAO.NumberOfEmployee = Customer.NumberOfEmployee;
                CustomerDAO.BusinessTypeId = Customer.BusinessTypeId;
                CustomerDAO.Investment = Customer.Investment;
                CustomerDAO.RevenueAnnual = Customer.RevenueAnnual;
                CustomerDAO.IsSupplier = Customer.IsSupplier;
                CustomerDAO.Descreption = Customer.Descreption;
                CustomerDAO.AppUserId = Customer.AppUserId;
                CustomerDAO.CreatorId = Customer.CreatorId;
                CustomerDAO.Used = Customer.Used;
                CustomerDAO.RowId = Customer.RowId;
                CustomerDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerDAOs.Add(CustomerDAO);
            }
            await DataContext.BulkMergeAsync(CustomerDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<Customer> Customers)
        {
            List<long> Ids = Customers.Select(x => x.Id).ToList();
            await DataContext.Customer
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(Customer Customer)
        {
            await DataContext.CustomerCustomerGroupingMapping
                .Where(x => x.CustomerId == Customer.Id)
                .DeleteFromQueryAsync();
            List<CustomerCustomerGroupingMappingDAO> CustomerCustomerGroupingMappingDAOs = new List<CustomerCustomerGroupingMappingDAO>();
            if (Customer.CustomerCustomerGroupingMappings != null)
            {
                foreach (CustomerCustomerGroupingMapping CustomerCustomerGroupingMapping in Customer.CustomerCustomerGroupingMappings)
                {
                    CustomerCustomerGroupingMappingDAO CustomerCustomerGroupingMappingDAO = new CustomerCustomerGroupingMappingDAO();
                    CustomerCustomerGroupingMappingDAO.CustomerId = Customer.Id;
                    CustomerCustomerGroupingMappingDAO.CustomerGroupingId = CustomerCustomerGroupingMapping.CustomerGroupingId;
                    CustomerCustomerGroupingMappingDAOs.Add(CustomerCustomerGroupingMappingDAO);
                }
                await DataContext.CustomerCustomerGroupingMapping.BulkMergeAsync(CustomerCustomerGroupingMappingDAOs);
            }
            await DataContext.CustomerEmail
                .Where(x => x.CustomerId == Customer.Id)
                .DeleteFromQueryAsync();
            await DataContext.CustomerEmail
                .Where(x => x.CustomerId == Customer.Id)
                .DeleteFromQueryAsync();
            List<CustomerEmailDAO> CustomerEmailDAOs = new List<CustomerEmailDAO>();
            if (Customer.CustomerEmails != null)
            {
                foreach (CustomerEmail CustomerEmail in Customer.CustomerEmails)
                {
                    CustomerEmailDAO CustomerEmailDAO = new CustomerEmailDAO();
                    CustomerEmailDAO.Id = CustomerEmail.Id;
                    CustomerEmailDAO.CustomerId = Customer.Id;
                    CustomerEmailDAO.Email = CustomerEmail.Email;
                    CustomerEmailDAO.EmailTypeId = CustomerEmail.EmailTypeId;
                    CustomerEmailDAOs.Add(CustomerEmailDAO);
                }
                await DataContext.CustomerEmail.BulkMergeAsync(CustomerEmailDAOs);
            }
            await DataContext.CustomerPhone
                .Where(x => x.CustomerId == Customer.Id)
                .DeleteFromQueryAsync();
            List<CustomerPhoneDAO> CustomerPhoneDAOs = new List<CustomerPhoneDAO>();
            if (Customer.CustomerPhones != null)
            {
                foreach (CustomerPhone CustomerPhone in Customer.CustomerPhones)
                {
                    CustomerPhoneDAO CustomerPhoneDAO = new CustomerPhoneDAO();
                    CustomerPhoneDAO.Id = CustomerPhone.Id;
                    CustomerPhoneDAO.CustomerId = Customer.Id;
                    CustomerPhoneDAO.Phone = CustomerPhone.Phone;
                    CustomerPhoneDAO.PhoneTypeId = CustomerPhone.PhoneTypeId;
                    CustomerPhoneDAOs.Add(CustomerPhoneDAO);
                }
                await DataContext.CustomerPhone.BulkMergeAsync(CustomerPhoneDAOs);
            }
        }

    }
}
