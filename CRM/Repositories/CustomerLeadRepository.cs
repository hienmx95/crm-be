using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Helpers;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface ICustomerLeadRepository
    {
        Task<int> Count(CustomerLeadFilter CustomerLeadFilter);
        Task<List<CustomerLead>> List(CustomerLeadFilter CustomerLeadFilter);
        Task<CustomerLead> Get(long Id);
        Task<bool> Create(CustomerLead CustomerLead);
        Task<bool> Update(CustomerLead CustomerLead);
        Task<bool> UpdateState(CustomerLead CustomerLead);
        Task<bool> Delete(CustomerLead CustomerLead);
        Task<bool> BulkMerge(List<CustomerLead> CustomerLeads);
        Task<bool> BulkDelete(List<CustomerLead> CustomerLeads);
    }
    public class CustomerLeadRepository : ICustomerLeadRepository
    {
        private DataContext DataContext;
        public CustomerLeadRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerLeadDAO> DynamicFilter(IQueryable<CustomerLeadDAO> query, CustomerLeadFilter filter)
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
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.CompanyName != null && filter.CompanyName.HasValue)
                query = query.Where(q => q.CompanyName, filter.CompanyName);
            if (filter.TelePhone != null && filter.TelePhone.HasValue)
                query = query.Where(q => q.TelePhone, filter.TelePhone);
            if (filter.Phone != null && filter.Phone.HasValue)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.Fax != null && filter.Fax.HasValue)
                query = query.Where(q => q.Fax, filter.Fax);
            if (filter.Email != null && filter.Email.HasValue)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.SecondEmail != null && filter.SecondEmail.HasValue)
                query = query.Where(q => q.SecondEmail, filter.SecondEmail);
            if (filter.Website != null && filter.Website.HasValue)
                query = query.Where(q => q.Website, filter.Website);
            if (filter.CustomerLeadSourceId != null && filter.CustomerLeadSourceId.HasValue)
                query = query.Where(q => q.CustomerLeadSourceId.HasValue).Where(q => q.CustomerLeadSourceId, filter.CustomerLeadSourceId);
            if (filter.CustomerLeadLevelId != null && filter.CustomerLeadLevelId.HasValue)
                query = query.Where(q => q.CustomerLeadLevelId.HasValue).Where(q => q.CustomerLeadLevelId, filter.CustomerLeadLevelId);
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, filter.CompanyId);
            if (filter.CampaignId != null && filter.CampaignId.HasValue)
                query = query.Where(q => q.CampaignId.HasValue).Where(q => q.CampaignId, filter.CampaignId);
            if (filter.ProfessionId != null && filter.ProfessionId.HasValue)
                query = query.Where(q => q.ProfessionId.HasValue).Where(q => q.ProfessionId, filter.ProfessionId);
            if (filter.Revenue != null && filter.Revenue.HasValue)
                query = query.Where(q => q.Revenue.HasValue).Where(q => q.Revenue, filter.Revenue);
            if (filter.EmployeeQuantity != null && filter.EmployeeQuantity.HasValue)
                query = query.Where(q => q.EmployeeQuantity.HasValue).Where(q => q.EmployeeQuantity, filter.EmployeeQuantity);
            if (filter.Address != null && filter.Address.HasValue)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.NationId != null && filter.NationId.HasValue)
                query = query.Where(q => q.NationId.HasValue).Where(q => q.NationId, filter.NationId);
            if (filter.ProvinceId != null && filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.DistrictId != null && filter.DistrictId.HasValue)
                query = query.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, filter.DistrictId);
            if (filter.CustomerLeadStatusId != null && filter.CustomerLeadStatusId.HasValue)
                query = query.Where(q => q.CustomerLeadStatusId.HasValue).Where(q => q.CustomerLeadStatusId, filter.CustomerLeadStatusId);
            if (filter.BusinessRegistrationCode != null && filter.BusinessRegistrationCode.HasValue)
                query = query.Where(q => q.BusinessRegistrationCode, filter.BusinessRegistrationCode);
            if (filter.SexId != null && filter.SexId.HasValue)
                query = query.Where(q => q.SexId.HasValue).Where(q => q.SexId, filter.SexId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
              if (filter.ZipCode != null && filter.ZipCode.HasValue)
                query = query.Where(q => q.ZipCode, filter.ZipCode);
            if (filter.CurrencyId != null && filter.CurrencyId.HasValue)
                query = query.Where(q => q.CurrencyId.HasValue).Where(q => q.CurrencyId, filter.CurrencyId);
            if (filter.RowId != null && filter.RowId.HasValue)
                query = query.Where(q => q.RowId, filter.RowId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerLeadDAO> OrFilter(IQueryable<CustomerLeadDAO> query, CustomerLeadFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerLeadDAO> initQuery = query.Where(q => false);
            foreach (CustomerLeadFilter CustomerLeadFilter in filter.OrFilter)
            {
                IQueryable<CustomerLeadDAO> queryable = query;
                if (CustomerLeadFilter.Id != null && CustomerLeadFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerLeadFilter.Id);
                if (CustomerLeadFilter.Name != null && CustomerLeadFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CustomerLeadFilter.Name);
                if (CustomerLeadFilter.CompanyName != null && CustomerLeadFilter.CompanyName.HasValue)
                    queryable = queryable.Where(q => q.CompanyName, CustomerLeadFilter.CompanyName);
                if (CustomerLeadFilter.TelePhone != null && CustomerLeadFilter.TelePhone.HasValue)
                    queryable = queryable.Where(q => q.TelePhone, CustomerLeadFilter.TelePhone);
                if (CustomerLeadFilter.Phone != null && CustomerLeadFilter.Phone.HasValue)
                    queryable = queryable.Where(q => q.Phone, CustomerLeadFilter.Phone);
                if (CustomerLeadFilter.Fax != null && CustomerLeadFilter.Fax.HasValue)
                    queryable = queryable.Where(q => q.Fax, CustomerLeadFilter.Fax);
                if (CustomerLeadFilter.Email != null && CustomerLeadFilter.Email.HasValue)
                    queryable = queryable.Where(q => q.Email, CustomerLeadFilter.Email);
                if (CustomerLeadFilter.SecondEmail != null && CustomerLeadFilter.SecondEmail.HasValue)
                    queryable = queryable.Where(q => q.SecondEmail, CustomerLeadFilter.SecondEmail);
                if (CustomerLeadFilter.Website != null && CustomerLeadFilter.Website.HasValue)
                    queryable = queryable.Where(q => q.Website, CustomerLeadFilter.Website);
                if (CustomerLeadFilter.CustomerLeadSourceId != null && CustomerLeadFilter.CustomerLeadSourceId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadSourceId.HasValue).Where(q => q.CustomerLeadSourceId, CustomerLeadFilter.CustomerLeadSourceId);
                if (CustomerLeadFilter.CustomerLeadLevelId != null && CustomerLeadFilter.CustomerLeadLevelId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadLevelId.HasValue).Where(q => q.CustomerLeadLevelId, CustomerLeadFilter.CustomerLeadLevelId);
                if (CustomerLeadFilter.CompanyId != null && CustomerLeadFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, CustomerLeadFilter.CompanyId);
                if (CustomerLeadFilter.CampaignId != null && CustomerLeadFilter.CampaignId.HasValue)
                    queryable = queryable.Where(q => q.CampaignId.HasValue).Where(q => q.CampaignId, CustomerLeadFilter.CampaignId);
                if (CustomerLeadFilter.ProfessionId != null && CustomerLeadFilter.ProfessionId.HasValue)
                    queryable = queryable.Where(q => q.ProfessionId.HasValue).Where(q => q.ProfessionId, CustomerLeadFilter.ProfessionId);
                if (CustomerLeadFilter.Revenue != null && CustomerLeadFilter.Revenue.HasValue)
                    queryable = queryable.Where(q => q.Revenue.HasValue).Where(q => q.Revenue, CustomerLeadFilter.Revenue);
                if (CustomerLeadFilter.EmployeeQuantity != null && CustomerLeadFilter.EmployeeQuantity.HasValue)
                    queryable = queryable.Where(q => q.EmployeeQuantity.HasValue).Where(q => q.EmployeeQuantity, CustomerLeadFilter.EmployeeQuantity);
                if (CustomerLeadFilter.Address != null && CustomerLeadFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, CustomerLeadFilter.Address);
                if (CustomerLeadFilter.NationId != null && CustomerLeadFilter.NationId.HasValue)
                    queryable = queryable.Where(q => q.NationId.HasValue).Where(q => q.NationId, CustomerLeadFilter.NationId);
                if (CustomerLeadFilter.ProvinceId != null && CustomerLeadFilter.ProvinceId.HasValue)
                    queryable = queryable.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, CustomerLeadFilter.ProvinceId);
                if (CustomerLeadFilter.DistrictId != null && CustomerLeadFilter.DistrictId.HasValue)
                    queryable = queryable.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, CustomerLeadFilter.DistrictId);
                if (CustomerLeadFilter.CustomerLeadStatusId != null && CustomerLeadFilter.CustomerLeadStatusId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadStatusId.HasValue).Where(q => q.CustomerLeadStatusId, CustomerLeadFilter.CustomerLeadStatusId);
                if (CustomerLeadFilter.BusinessRegistrationCode != null && CustomerLeadFilter.BusinessRegistrationCode.HasValue)
                    queryable = queryable.Where(q => q.BusinessRegistrationCode, CustomerLeadFilter.BusinessRegistrationCode);
                if (CustomerLeadFilter.SexId != null && CustomerLeadFilter.SexId.HasValue)
                    queryable = queryable.Where(q => q.SexId.HasValue).Where(q => q.SexId, CustomerLeadFilter.SexId);
                if (CustomerLeadFilter.Description != null && CustomerLeadFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CustomerLeadFilter.Description);
                if (CustomerLeadFilter.AppUserId != null && CustomerLeadFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, CustomerLeadFilter.AppUserId);
                if (CustomerLeadFilter.CreatorId != null && CustomerLeadFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CustomerLeadFilter.CreatorId);
              if (CustomerLeadFilter.ZipCode != null && CustomerLeadFilter.ZipCode.HasValue)
                    queryable = queryable.Where(q => q.ZipCode, CustomerLeadFilter.ZipCode);
                if (CustomerLeadFilter.CurrencyId != null && CustomerLeadFilter.CurrencyId.HasValue)
                    queryable = queryable.Where(q => q.CurrencyId.HasValue).Where(q => q.CurrencyId, CustomerLeadFilter.CurrencyId);
                if (CustomerLeadFilter.RowId != null && CustomerLeadFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, CustomerLeadFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<CustomerLeadDAO> DynamicOrder(IQueryable<CustomerLeadDAO> query, CustomerLeadFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerLeadOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CustomerLeadOrder.CompanyName:
                            query = query.OrderBy(q => q.CompanyName);
                            break;
                        case CustomerLeadOrder.TelePhone:
                            query = query.OrderBy(q => q.TelePhone);
                            break;
                        case CustomerLeadOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case CustomerLeadOrder.Fax:
                            query = query.OrderBy(q => q.Fax);
                            break;
                        case CustomerLeadOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case CustomerLeadOrder.SecondEmail:
                            query = query.OrderBy(q => q.SecondEmail);
                            break;
                        case CustomerLeadOrder.Website:
                            query = query.OrderBy(q => q.Website);
                            break;
                        case CustomerLeadOrder.CustomerLeadSource:
                            query = query.OrderBy(q => q.CustomerLeadSourceId);
                            break;
                        case CustomerLeadOrder.CustomerLeadLevel:
                            query = query.OrderBy(q => q.CustomerLeadLevelId);
                            break;
                        case CustomerLeadOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case CustomerLeadOrder.Campaign:
                            query = query.OrderBy(q => q.CampaignId);
                            break;
                        case CustomerLeadOrder.Profession:
                            query = query.OrderBy(q => q.ProfessionId);
                            break;
                        case CustomerLeadOrder.Revenue:
                            query = query.OrderBy(q => q.Revenue);
                            break;
                        case CustomerLeadOrder.EmployeeQuantity:
                            query = query.OrderBy(q => q.EmployeeQuantity);
                            break;
                        case CustomerLeadOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case CustomerLeadOrder.Nation:
                            query = query.OrderBy(q => q.NationId);
                            break;
                        case CustomerLeadOrder.Province:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case CustomerLeadOrder.District:
                            query = query.OrderBy(q => q.DistrictId);
                            break;
                        case CustomerLeadOrder.CustomerLeadStatus:
                            query = query.OrderBy(q => q.CustomerLeadStatusId);
                            break;
                        case CustomerLeadOrder.BusinessRegistrationCode:
                            query = query.OrderBy(q => q.BusinessRegistrationCode);
                            break;
                        case CustomerLeadOrder.Sex:
                            query = query.OrderBy(q => q.SexId);
                            break;
                        case CustomerLeadOrder.RefuseReciveSMS:
                            query = query.OrderBy(q => q.RefuseReciveSMS);
                            break;
                        case CustomerLeadOrder.RefuseReciveEmail:
                            query = query.OrderBy(q => q.RefuseReciveEmail);
                            break;
                        case CustomerLeadOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CustomerLeadOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case CustomerLeadOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CustomerLeadOrder.ZipCode:
                            query = query.OrderBy(q => q.ZipCode);
                            break;
                        case CustomerLeadOrder.Currency:
                            query = query.OrderBy(q => q.CurrencyId);
                            break;
                        case CustomerLeadOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerLeadOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CustomerLeadOrder.CompanyName:
                            query = query.OrderByDescending(q => q.CompanyName);
                            break;
                        case CustomerLeadOrder.TelePhone:
                            query = query.OrderByDescending(q => q.TelePhone);
                            break;
                        case CustomerLeadOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case CustomerLeadOrder.Fax:
                            query = query.OrderByDescending(q => q.Fax);
                            break;
                        case CustomerLeadOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case CustomerLeadOrder.SecondEmail:
                            query = query.OrderByDescending(q => q.SecondEmail);
                            break;
                        case CustomerLeadOrder.Website:
                            query = query.OrderByDescending(q => q.Website);
                            break;
                        case CustomerLeadOrder.CustomerLeadSource:
                            query = query.OrderByDescending(q => q.CustomerLeadSourceId);
                            break;
                        case CustomerLeadOrder.CustomerLeadLevel:
                            query = query.OrderByDescending(q => q.CustomerLeadLevelId);
                            break;
                        case CustomerLeadOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case CustomerLeadOrder.Campaign:
                            query = query.OrderByDescending(q => q.CampaignId);
                            break;
                        case CustomerLeadOrder.Profession:
                            query = query.OrderByDescending(q => q.ProfessionId);
                            break;
                        case CustomerLeadOrder.Revenue:
                            query = query.OrderByDescending(q => q.Revenue);
                            break;
                        case CustomerLeadOrder.EmployeeQuantity:
                            query = query.OrderByDescending(q => q.EmployeeQuantity);
                            break;
                        case CustomerLeadOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case CustomerLeadOrder.Nation:
                            query = query.OrderByDescending(q => q.NationId);
                            break;
                        case CustomerLeadOrder.Province:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case CustomerLeadOrder.District:
                            query = query.OrderByDescending(q => q.DistrictId);
                            break;
                        case CustomerLeadOrder.CustomerLeadStatus:
                            query = query.OrderByDescending(q => q.CustomerLeadStatusId);
                            break;
                        case CustomerLeadOrder.BusinessRegistrationCode:
                            query = query.OrderByDescending(q => q.BusinessRegistrationCode);
                            break;
                        case CustomerLeadOrder.Sex:
                            query = query.OrderByDescending(q => q.SexId);
                            break;
                        case CustomerLeadOrder.RefuseReciveSMS:
                            query = query.OrderByDescending(q => q.RefuseReciveSMS);
                            break;
                        case CustomerLeadOrder.RefuseReciveEmail:
                            query = query.OrderByDescending(q => q.RefuseReciveEmail);
                            break;
                        case CustomerLeadOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CustomerLeadOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case CustomerLeadOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CustomerLeadOrder.ZipCode:
                            query = query.OrderByDescending(q => q.ZipCode);
                            break;
                        case CustomerLeadOrder.Currency:
                            query = query.OrderByDescending(q => q.CurrencyId);
                            break;
                        case CustomerLeadOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerLead>> DynamicSelect(IQueryable<CustomerLeadDAO> query, CustomerLeadFilter filter)
        {
            List<CustomerLead> CustomerLeads = await query.Select(q => new CustomerLead()
            {
                Id = filter.Selects.Contains(CustomerLeadSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(CustomerLeadSelect.Name) ? q.Name : default(string),
                CompanyName = filter.Selects.Contains(CustomerLeadSelect.CompanyName) ? q.CompanyName : default(string),
                TelePhone = filter.Selects.Contains(CustomerLeadSelect.TelePhone) ? q.TelePhone : default(string),
                Phone = filter.Selects.Contains(CustomerLeadSelect.Phone) ? q.Phone : default(string),
                Fax = filter.Selects.Contains(CustomerLeadSelect.Fax) ? q.Fax : default(string),
                Email = filter.Selects.Contains(CustomerLeadSelect.Email) ? q.Email : default(string),
                SecondEmail = filter.Selects.Contains(CustomerLeadSelect.SecondEmail) ? q.SecondEmail : default(string),
                Website = filter.Selects.Contains(CustomerLeadSelect.Website) ? q.Website : default(string),
                CustomerLeadSourceId = filter.Selects.Contains(CustomerLeadSelect.CustomerLeadSource) ? q.CustomerLeadSourceId : default(long?),
                CustomerLeadLevelId = filter.Selects.Contains(CustomerLeadSelect.CustomerLeadLevel) ? q.CustomerLeadLevelId : default(long?),
                CompanyId = filter.Selects.Contains(CustomerLeadSelect.Company) ? q.CompanyId : default(long?),
                CampaignId = filter.Selects.Contains(CustomerLeadSelect.Campaign) ? q.CampaignId : default(long?),
                ProfessionId = filter.Selects.Contains(CustomerLeadSelect.Profession) ? q.ProfessionId : default(long?),
                Revenue = filter.Selects.Contains(CustomerLeadSelect.Revenue) ? q.Revenue : default(decimal?),
                EmployeeQuantity = filter.Selects.Contains(CustomerLeadSelect.EmployeeQuantity) ? q.EmployeeQuantity : default(long?),
                Address = filter.Selects.Contains(CustomerLeadSelect.Address) ? q.Address : default(string),
                NationId = filter.Selects.Contains(CustomerLeadSelect.Nation) ? q.NationId : default(long?),
                ProvinceId = filter.Selects.Contains(CustomerLeadSelect.Province) ? q.ProvinceId : default(long?),
                DistrictId = filter.Selects.Contains(CustomerLeadSelect.District) ? q.DistrictId : default(long?),
                CustomerLeadStatusId = filter.Selects.Contains(CustomerLeadSelect.CustomerLeadStatus) ? q.CustomerLeadStatusId : default(long?),
                BusinessRegistrationCode = filter.Selects.Contains(CustomerLeadSelect.BusinessRegistrationCode) ? q.BusinessRegistrationCode : default(string),
                SexId = filter.Selects.Contains(CustomerLeadSelect.Sex) ? q.SexId : default(long?),
                RefuseReciveSMS = filter.Selects.Contains(CustomerLeadSelect.RefuseReciveSMS) ? q.RefuseReciveSMS : default(bool?),
                RefuseReciveEmail = filter.Selects.Contains(CustomerLeadSelect.RefuseReciveEmail) ? q.RefuseReciveEmail : default(bool?),
                Description = filter.Selects.Contains(CustomerLeadSelect.Description) ? q.Description : default(string),
                AppUserId = filter.Selects.Contains(CustomerLeadSelect.AppUser) ? q.AppUserId : default(long?),
                CreatorId = filter.Selects.Contains(CustomerLeadSelect.Creator) ? q.CreatorId : default(long),
                ZipCode = filter.Selects.Contains(CustomerLeadSelect.ZipCode) ? q.ZipCode : default(string),
                CurrencyId = filter.Selects.Contains(CustomerLeadSelect.Currency) ? q.CurrencyId : default(long?),
                RowId = filter.Selects.Contains(CustomerLeadSelect.Row) ? q.RowId : default(Guid),
                AppUser = filter.Selects.Contains(CustomerLeadSelect.AppUser) && q.AppUser != null ? new AppUser
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
                Company = filter.Selects.Contains(CustomerLeadSelect.Company) && q.Company != null ? new Company
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
                Creator = filter.Selects.Contains(CustomerLeadSelect.Creator) && q.Creator != null ? new AppUser
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
                Currency = filter.Selects.Contains(CustomerLeadSelect.Currency) && q.Currency != null ? new Currency
                {
                    Id = q.Currency.Id,
                    Code = q.Currency.Code,
                    Name = q.Currency.Name,
                } : null,
                CustomerLeadLevel = filter.Selects.Contains(CustomerLeadSelect.CustomerLeadLevel) && q.CustomerLeadLevel != null ? new CustomerLeadLevel
                {
                    Id = q.CustomerLeadLevel.Id,
                    Code = q.CustomerLeadLevel.Code,
                    Name = q.CustomerLeadLevel.Name,
                } : null,
                CustomerLeadSource = filter.Selects.Contains(CustomerLeadSelect.CustomerLeadSource) && q.CustomerLeadSource != null ? new CustomerLeadSource
                {
                    Id = q.CustomerLeadSource.Id,
                    Code = q.CustomerLeadSource.Code,
                    Name = q.CustomerLeadSource.Name,
                } : null,
                CustomerLeadStatus = filter.Selects.Contains(CustomerLeadSelect.CustomerLeadStatus) && q.CustomerLeadStatus != null ? new CustomerLeadStatus
                {
                    Id = q.CustomerLeadStatus.Id,
                    Code = q.CustomerLeadStatus.Code,
                    Name = q.CustomerLeadStatus.Name,
                } : null,
                District = filter.Selects.Contains(CustomerLeadSelect.District) && q.District != null ? new District
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
                Nation = filter.Selects.Contains(CustomerLeadSelect.Nation) && q.Nation != null ? new Nation
                {
                    Id = q.Nation.Id,
                    Code = q.Nation.Code,
                    Name = q.Nation.Name,
                    Priority = q.Nation.Priority,
                    StatusId = q.Nation.StatusId,
                    Used = q.Nation.Used,
                    RowId = q.Nation.RowId,
                } : null,
              
                Profession = filter.Selects.Contains(CustomerLeadSelect.Profession) && q.Profession != null ? new Profession
                {
                    Id = q.Profession.Id,
                    Code = q.Profession.Code,
                    Name = q.Profession.Name,
                    StatusId = q.Profession.StatusId,
                    RowId = q.Profession.RowId,
                    Used = q.Profession.Used,
                } : null,
                Province = filter.Selects.Contains(CustomerLeadSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Code = q.Province.Code,
                    Name = q.Province.Name,
                    Priority = q.Province.Priority,
                    StatusId = q.Province.StatusId,
                    RowId = q.Province.RowId,
                    Used = q.Province.Used,
                } : null,
                Sex = filter.Selects.Contains(CustomerLeadSelect.Sex) && q.Sex != null ? new Sex
                {
                    Id = q.Sex.Id,
                    Code = q.Sex.Code,
                    Name = q.Sex.Name,
                } : null,
            }).ToListAsync();
            return CustomerLeads;
        }

        public async Task<int> Count(CustomerLeadFilter filter)
        {
            IQueryable<CustomerLeadDAO> CustomerLeads = DataContext.CustomerLead.AsNoTracking();
            CustomerLeads = DynamicFilter(CustomerLeads, filter);
            return await CustomerLeads.CountAsync();
        }

        public async Task<List<CustomerLead>> List(CustomerLeadFilter filter)
        {
            if (filter == null) return new List<CustomerLead>();
            IQueryable<CustomerLeadDAO> CustomerLeadDAOs = DataContext.CustomerLead.AsNoTracking();
            CustomerLeadDAOs = DynamicFilter(CustomerLeadDAOs, filter);
            CustomerLeadDAOs = DynamicOrder(CustomerLeadDAOs, filter);
            List<CustomerLead> CustomerLeads = await DynamicSelect(CustomerLeadDAOs, filter);
            return CustomerLeads;
        }

        public async Task<CustomerLead> Get(long Id)
        {
            CustomerLead CustomerLead = await DataContext.CustomerLead.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new CustomerLead()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                CompanyName = x.CompanyName,
                TelePhone = x.TelePhone,
                Phone = x.Phone,
                Fax = x.Fax,
                Email = x.Email,
                SecondEmail = x.SecondEmail,
                Website = x.Website,
                CustomerLeadSourceId = x.CustomerLeadSourceId,
                CustomerLeadLevelId = x.CustomerLeadLevelId,
                CompanyId = x.CompanyId,
                CampaignId = x.CampaignId,
                ProfessionId = x.ProfessionId,
                Revenue = x.Revenue,
                EmployeeQuantity = x.EmployeeQuantity,
                Address = x.Address,
                NationId = x.NationId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                CustomerLeadStatusId = x.CustomerLeadStatusId,
                BusinessRegistrationCode = x.BusinessRegistrationCode,
                SexId = x.SexId,
                RefuseReciveSMS = x.RefuseReciveSMS,
                RefuseReciveEmail = x.RefuseReciveEmail,
                Description = x.Description,
                AppUserId = x.AppUserId,
                CreatorId = x.CreatorId,
                ZipCode = x.ZipCode,
                CurrencyId = x.CurrencyId,
                RowId = x.RowId,
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
                Currency = x.Currency == null ? null : new Currency
                {
                    Id = x.Currency.Id,
                    Code = x.Currency.Code,
                    Name = x.Currency.Name,
                },
                CustomerLeadLevel = x.CustomerLeadLevel == null ? null : new CustomerLeadLevel
                {
                    Id = x.CustomerLeadLevel.Id,
                    Code = x.CustomerLeadLevel.Code,
                    Name = x.CustomerLeadLevel.Name,
                },
                CustomerLeadSource = x.CustomerLeadSource == null ? null : new CustomerLeadSource
                {
                    Id = x.CustomerLeadSource.Id,
                    Code = x.CustomerLeadSource.Code,
                    Name = x.CustomerLeadSource.Name,
                },
                CustomerLeadStatus = x.CustomerLeadStatus == null ? null : new CustomerLeadStatus
                {
                    Id = x.CustomerLeadStatus.Id,
                    Code = x.CustomerLeadStatus.Code,
                    Name = x.CustomerLeadStatus.Name,
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
            }).FirstOrDefaultAsync();

            if (CustomerLead == null)
                return null;

            CustomerLead.CustomerLeadItemMappings = await DataContext.CustomerLeadItemMapping
                .Where(x => x.CustomerLeadId == CustomerLead.Id)
                .Select(p => new CustomerLeadItemMapping
                {
                    CustomerLeadId = p.CustomerLeadId,
                    ItemId = p.ItemId,
                    UnitOfMeasureId = p.UnitOfMeasureId,
                    Quantity = p.Quantity,
                    DiscountPercentage = p.DiscountPercentage,
                    RequestQuantity = p.RequestQuantity,
                    PrimaryPrice = p.PrimaryPrice,
                    SalePrice = p.SalePrice,
                    Discount = p.Discount,
                    VAT = p.VAT,
                    VATOther = p.VATOther,
                    TotalPrice = p.TotalPrice,
                    Factor = p.Factor,
                    Item = p.Item == null ? null : new Item
                    {
                        Id = p.Item.Id,
                        Name = p.Item.Name,
                        Code = p.Item.Code,
                        ScanCode = p.Item.ScanCode,
                        SalePrice = p.Item.SalePrice,
                        RetailPrice = p.Item.RetailPrice,
                        StatusId = p.Item.StatusId,
                        ProductId = p.Item.ProductId,
                        Product = p.Item.Product == null ? null : new Product
                        {
                            CreatedAt = p.Item.Product.CreatedAt,
                            UpdatedAt = p.Item.Product.UpdatedAt,
                            Id = p.Item.Product.Id,
                            Code = p.Item.Product.Code,
                            Name = p.Item.Product.Name,
                            Description = p.Item.Product.Description,
                            ScanCode = p.Item.Product.ScanCode,
                            ERPCode = p.Item.Product.ERPCode,
                            ProductTypeId = p.Item.Product.ProductTypeId,
                            BrandId = p.Item.Product.BrandId,
                            UnitOfMeasureId = p.Item.Product.UnitOfMeasureId,
                            UnitOfMeasureGroupingId = p.Item.Product.UnitOfMeasureGroupingId,
                            SalePrice = p.Item.Product.SalePrice,
                            RetailPrice = p.Item.Product.RetailPrice,
                            TaxTypeId = p.Item.Product.TaxTypeId,
                            StatusId = p.Item.Product.StatusId,
                            OtherName = p.Item.Product.OtherName,
                            TechnicalName = p.Item.Product.TechnicalName,
                            Note = p.Item.Product.Note,
                            IsNew = p.Item.Product.IsNew,
                            UsedVariationId = p.Item.Product.UsedVariationId,
                            Used = p.Item.Product.Used,
                            Brand = p.Item.Product.Brand == null ? null : new Brand
                            {
                                Id = p.Item.Product.Brand.Id,
                                Code = p.Item.Product.Brand.Code,
                                Name = p.Item.Product.Brand.Name,
                                StatusId = p.Item.Product.Brand.StatusId,
                                Description = p.Item.Product.Brand.Description,
                                Used = p.Item.Product.Brand.Used,
                            },
                            ProductType = p.Item.Product.ProductType == null ? null : new ProductType
                            {
                                Id = p.Item.Product.ProductType.Id,
                                Code = p.Item.Product.ProductType.Code,
                                Name = p.Item.Product.ProductType.Name,
                                Description = p.Item.Product.ProductType.Description,
                                StatusId = p.Item.Product.ProductType.StatusId,
                                Used = p.Item.Product.ProductType.Used,
                            },
                        }
                    },
                    UnitOfMeasure = p.UnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = p.UnitOfMeasure.Id,
                        Code = p.UnitOfMeasure.Code,
                        Name = p.UnitOfMeasure.Name,
                        Description = p.UnitOfMeasure.Description,
                        StatusId = p.UnitOfMeasure.StatusId,
                    },
                }).ToListAsync();

            CustomerLead.CustomerLeadFileGroups = await DataContext.CustomerLeadFileGroup
                .Where(x => x.CustomerLeadId == Id)
                .Where(x => x.DeletedAt == null)
                .Select(x => new CustomerLeadFileGroup
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    FileTypeId = x.FileTypeId,
                    CustomerLeadId = x.CustomerLeadId,
                    RowId = x.RowId,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Creator = x.Creator == null ? null : new AppUser
                    {
                        Id = x.Creator.Id,
                        Username = x.Creator.Username,
                        DisplayName = x.Creator.DisplayName,
                    },
                    FileType = x.FileType == null ? null : new FileType
                    {
                        Id = x.FileType.Id,
                        Code = x.FileType.Code,
                        Name = x.FileType.Name,
                    }
                }).ToListAsync();
            var CustomerLeadFileGroupIds = CustomerLead.CustomerLeadFileGroups.Select(x => x.Id).ToList();

            var CustomerLeadFileMappings = await DataContext.CustomerLeadFileMapping.Where(x => CustomerLeadFileGroupIds.Contains(x.CustomerLeadFileGroupId))
                .Select(x => new CustomerLeadFileMapping
                {
                    CustomerLeadFileGroupId = x.CustomerLeadFileGroupId,
                    FileId = x.FileId,
                    File = x.File == null ? null : new File
                    {
                        Id = x.File.Id,
                        AppUserId = x.File.AppUserId,
                        CreatedAt = x.File.CreatedAt,
                        Name = x.File.Name,
                        Url = x.File.Url,
                        UpdatedAt = x.File.UpdatedAt,
                        RowId = x.File.RowId,
                        AppUser = x.File.AppUser == null ? null : new AppUser
                        {
                            Id = x.File.AppUser.Id,
                            Username = x.File.AppUser.Username,
                            DisplayName = x.File.AppUser.DisplayName,
                        },
                    },
                }).ToListAsync();

            foreach (CustomerLeadFileGroup CustomerLeadFileGroup in CustomerLead.CustomerLeadFileGroups)
            {
                CustomerLeadFileGroup.CustomerLeadFileMappings = CustomerLeadFileMappings.Where(x => x.CustomerLeadFileGroupId == CustomerLeadFileGroup.Id).ToList();
            }
            return CustomerLead;
        }
        public async Task<bool> Create(CustomerLead CustomerLead)
        {
            CustomerLeadDAO CustomerLeadDAO = new CustomerLeadDAO();
            CustomerLeadDAO.Id = CustomerLead.Id;
            CustomerLeadDAO.Name = CustomerLead.Name;
            CustomerLeadDAO.CompanyName = CustomerLead.CompanyName;
            CustomerLeadDAO.TelePhone = CustomerLead.TelePhone;
            CustomerLeadDAO.Phone = CustomerLead.Phone;
            CustomerLeadDAO.Fax = CustomerLead.Fax;
            CustomerLeadDAO.Email = CustomerLead.Email;
            CustomerLeadDAO.SecondEmail = CustomerLead.SecondEmail;
            CustomerLeadDAO.Website = CustomerLead.Website;
            CustomerLeadDAO.CustomerLeadSourceId = CustomerLead.CustomerLeadSourceId;
            CustomerLeadDAO.CustomerLeadLevelId = CustomerLead.CustomerLeadLevelId;
            CustomerLeadDAO.CompanyId = CustomerLead.CompanyId;
            CustomerLeadDAO.CampaignId = CustomerLead.CampaignId;
            CustomerLeadDAO.ProfessionId = CustomerLead.ProfessionId;
            CustomerLeadDAO.Revenue = CustomerLead.Revenue;
            CustomerLeadDAO.EmployeeQuantity = CustomerLead.EmployeeQuantity;
            CustomerLeadDAO.Address = CustomerLead.Address;
            CustomerLeadDAO.NationId = CustomerLead.NationId;
            CustomerLeadDAO.ProvinceId = CustomerLead.ProvinceId;
            CustomerLeadDAO.DistrictId = CustomerLead.DistrictId;
            CustomerLeadDAO.CustomerLeadStatusId = CustomerLead.CustomerLeadStatusId;
            CustomerLeadDAO.BusinessRegistrationCode = CustomerLead.BusinessRegistrationCode;
            CustomerLeadDAO.SexId = CustomerLead.SexId;
            CustomerLeadDAO.RefuseReciveSMS = CustomerLead.RefuseReciveSMS;
            CustomerLeadDAO.RefuseReciveEmail = CustomerLead.RefuseReciveEmail;
            CustomerLeadDAO.Description = CustomerLead.Description;
            CustomerLeadDAO.AppUserId = CustomerLead.AppUserId;
            CustomerLeadDAO.CreatorId = CustomerLead.CreatorId;
            CustomerLeadDAO.ZipCode = CustomerLead.ZipCode;
            CustomerLeadDAO.CurrencyId = CustomerLead.CurrencyId;
            CustomerLeadDAO.RowId = CustomerLead.RowId;
            CustomerLeadDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerLeadDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerLead.Add(CustomerLeadDAO);
            await DataContext.SaveChangesAsync();
            CustomerLead.Id = CustomerLeadDAO.Id;
            await SaveReference(CustomerLead);
            return true;
        }

        public async Task<bool> Update(CustomerLead CustomerLead)
        {
            CustomerLeadDAO CustomerLeadDAO = DataContext.CustomerLead.Where(x => x.Id == CustomerLead.Id).FirstOrDefault();
            if (CustomerLeadDAO == null)
                return false;
            CustomerLeadDAO.Id = CustomerLead.Id;
            CustomerLeadDAO.Name = CustomerLead.Name;
            CustomerLeadDAO.CompanyName = CustomerLead.CompanyName;
            CustomerLeadDAO.TelePhone = CustomerLead.TelePhone;
            CustomerLeadDAO.Phone = CustomerLead.Phone;
            CustomerLeadDAO.Fax = CustomerLead.Fax;
            CustomerLeadDAO.Email = CustomerLead.Email;
            CustomerLeadDAO.SecondEmail = CustomerLead.SecondEmail;
            CustomerLeadDAO.Website = CustomerLead.Website;
            CustomerLeadDAO.CustomerLeadSourceId = CustomerLead.CustomerLeadSourceId;
            CustomerLeadDAO.CustomerLeadLevelId = CustomerLead.CustomerLeadLevelId;
            CustomerLeadDAO.CompanyId = CustomerLead.CompanyId;
            CustomerLeadDAO.CampaignId = CustomerLead.CampaignId;
            CustomerLeadDAO.ProfessionId = CustomerLead.ProfessionId;
            CustomerLeadDAO.Revenue = CustomerLead.Revenue;
            CustomerLeadDAO.EmployeeQuantity = CustomerLead.EmployeeQuantity;
            CustomerLeadDAO.Address = CustomerLead.Address;
            CustomerLeadDAO.NationId = CustomerLead.NationId;
            CustomerLeadDAO.ProvinceId = CustomerLead.ProvinceId;
            CustomerLeadDAO.DistrictId = CustomerLead.DistrictId;
            CustomerLeadDAO.CustomerLeadStatusId = CustomerLead.CustomerLeadStatusId;
            CustomerLeadDAO.BusinessRegistrationCode = CustomerLead.BusinessRegistrationCode;
            CustomerLeadDAO.SexId = CustomerLead.SexId;
            CustomerLeadDAO.RefuseReciveSMS = CustomerLead.RefuseReciveSMS;
            CustomerLeadDAO.RefuseReciveEmail = CustomerLead.RefuseReciveEmail;
            CustomerLeadDAO.Description = CustomerLead.Description;
            CustomerLeadDAO.AppUserId = CustomerLead.AppUserId;
            CustomerLeadDAO.CreatorId = CustomerLead.CreatorId;
            CustomerLeadDAO.ZipCode = CustomerLead.ZipCode;
            CustomerLeadDAO.CurrencyId = CustomerLead.CurrencyId;
            CustomerLeadDAO.RowId = CustomerLead.RowId;
            CustomerLeadDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerLead);
            return true;
        }

        public async Task<bool> Delete(CustomerLead CustomerLead)
        {
            await DataContext.CustomerLead.Where(x => x.Id == CustomerLead.Id).UpdateFromQueryAsync(x => new CustomerLeadDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<CustomerLead> CustomerLeads)
        {
            List<CustomerLeadDAO> CustomerLeadDAOs = new List<CustomerLeadDAO>();
            foreach (CustomerLead CustomerLead in CustomerLeads)
            {
                CustomerLeadDAO CustomerLeadDAO = new CustomerLeadDAO();
                CustomerLeadDAO.Id = CustomerLead.Id;
                CustomerLeadDAO.Name = CustomerLead.Name;
                CustomerLeadDAO.CompanyName = CustomerLead.CompanyName;
                CustomerLeadDAO.TelePhone = CustomerLead.TelePhone;
                CustomerLeadDAO.Phone = CustomerLead.Phone;
                CustomerLeadDAO.Fax = CustomerLead.Fax;
                CustomerLeadDAO.Email = CustomerLead.Email;
                CustomerLeadDAO.SecondEmail = CustomerLead.SecondEmail;
                CustomerLeadDAO.Website = CustomerLead.Website;
                CustomerLeadDAO.CustomerLeadSourceId = CustomerLead.CustomerLeadSourceId;
                CustomerLeadDAO.CustomerLeadLevelId = CustomerLead.CustomerLeadLevelId;
                CustomerLeadDAO.CompanyId = CustomerLead.CompanyId;
                CustomerLeadDAO.CampaignId = CustomerLead.CampaignId;
                CustomerLeadDAO.ProfessionId = CustomerLead.ProfessionId;
                CustomerLeadDAO.Revenue = CustomerLead.Revenue;
                CustomerLeadDAO.EmployeeQuantity = CustomerLead.EmployeeQuantity;
                CustomerLeadDAO.Address = CustomerLead.Address;
                CustomerLeadDAO.NationId = CustomerLead.NationId;
                CustomerLeadDAO.ProvinceId = CustomerLead.ProvinceId;
                CustomerLeadDAO.DistrictId = CustomerLead.DistrictId;
                CustomerLeadDAO.CustomerLeadStatusId = CustomerLead.CustomerLeadStatusId;
                CustomerLeadDAO.BusinessRegistrationCode = CustomerLead.BusinessRegistrationCode;
                CustomerLeadDAO.SexId = CustomerLead.SexId;
                CustomerLeadDAO.RefuseReciveSMS = CustomerLead.RefuseReciveSMS;
                CustomerLeadDAO.RefuseReciveEmail = CustomerLead.RefuseReciveEmail;
                CustomerLeadDAO.Description = CustomerLead.Description;
                CustomerLeadDAO.AppUserId = CustomerLead.AppUserId;
                CustomerLeadDAO.CreatorId = CustomerLead.CreatorId;
                CustomerLeadDAO.ZipCode = CustomerLead.ZipCode;
                CustomerLeadDAO.CurrencyId = CustomerLead.CurrencyId;
                CustomerLeadDAO.RowId = CustomerLead.RowId;
                CustomerLeadDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerLeadDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerLeadDAOs.Add(CustomerLeadDAO);
            }
            await DataContext.BulkMergeAsync(CustomerLeadDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerLead> CustomerLeads)
        {
            List<long> Ids = CustomerLeads.Select(x => x.Id).ToList();
            await DataContext.CustomerLead
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerLeadDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerLead CustomerLead)
        {
            await DataContext.CustomerLeadItemMapping
               .Where(x => x.CustomerLeadId == CustomerLead.Id)
               .DeleteFromQueryAsync();

            List<CustomerLeadItemMappingDAO> CustomerLeadItemMappingDAOs = new List<CustomerLeadItemMappingDAO>();
            if (CustomerLead.CustomerLeadItemMappings != null)
            {
                foreach (CustomerLeadItemMapping CustomerLeadItemMapping in CustomerLead.CustomerLeadItemMappings)
                {
                    CustomerLeadItemMappingDAO CustomerLeadItemMappingDAO = new CustomerLeadItemMappingDAO();
                    CustomerLeadItemMappingDAO.ItemId = CustomerLeadItemMapping.ItemId;
                    CustomerLeadItemMappingDAO.CustomerLeadId = CustomerLead.Id;
                    CustomerLeadItemMappingDAO.Quantity = CustomerLeadItemMapping.Quantity;
                    CustomerLeadItemMappingDAO.DiscountPercentage = CustomerLeadItemMapping.DiscountPercentage;
                    CustomerLeadItemMappingDAO.RequestQuantity = CustomerLeadItemMapping.RequestQuantity;
                    CustomerLeadItemMappingDAO.PrimaryPrice = CustomerLeadItemMapping.PrimaryPrice;
                    CustomerLeadItemMappingDAO.SalePrice = CustomerLeadItemMapping.SalePrice;
                    CustomerLeadItemMappingDAO.Discount = CustomerLeadItemMapping.Discount;
                    CustomerLeadItemMappingDAO.VAT = CustomerLeadItemMapping.VAT;
                    CustomerLeadItemMappingDAO.VATOther = CustomerLeadItemMapping.VATOther;
                    CustomerLeadItemMappingDAO.TotalPrice = CustomerLeadItemMapping.TotalPrice;
                    CustomerLeadItemMappingDAO.Factor = CustomerLeadItemMapping.Factor;
                    CustomerLeadItemMappingDAO.UnitOfMeasureId = CustomerLeadItemMapping.UnitOfMeasureId;
                    CustomerLeadItemMappingDAOs.Add(CustomerLeadItemMappingDAO);
                }
                await DataContext.CustomerLeadItemMapping.BulkMergeAsync(CustomerLeadItemMappingDAOs);
            }

            List<CustomerLeadFileGroupDAO> CustomerLeadFileGroupDAOs = await DataContext.CustomerLeadFileGroup.Where(x => x.CustomerLeadId == CustomerLead.Id).ToListAsync();
            CustomerLeadFileGroupDAOs.ForEach(x => x.DeletedAt = StaticParams.DateTimeNow);
            await DataContext.CustomerLeadFileMapping.Where(x => x.CustomerLeadFileGroup.CustomerLeadId == CustomerLead.Id).DeleteFromQueryAsync();
            List<CustomerLeadFileMappingDAO> CustomerLeadFileMappingDAOs = new List<CustomerLeadFileMappingDAO>();
            if (CustomerLead.CustomerLeadFileGroups != null)
            {
                foreach (var CustomerLeadFileGroup in CustomerLead.CustomerLeadFileGroups)
                {
                    CustomerLeadFileGroupDAO CustomerLeadFileGroupDAO = CustomerLeadFileGroupDAOs.Where(x => x.Id == CustomerLeadFileGroup.Id && x.Id != 0).FirstOrDefault();
                    if (CustomerLeadFileGroupDAO == null)
                    {
                        CustomerLeadFileGroupDAO = new CustomerLeadFileGroupDAO
                        {
                            CreatorId = CustomerLeadFileGroup.CreatorId,
                            CustomerLeadId = CustomerLead.Id,
                            Description = CustomerLeadFileGroup.Description,
                            Title = CustomerLeadFileGroup.Title,
                            FileTypeId = CustomerLeadFileGroup.FileTypeId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow,
                            RowId = Guid.NewGuid()
                        };
                        CustomerLeadFileGroupDAOs.Add(CustomerLeadFileGroupDAO);
                        CustomerLeadFileGroup.RowId = CustomerLeadFileGroupDAO.RowId;
                    }
                    else
                    {
                        CustomerLeadFileGroupDAO.Description = CustomerLeadFileGroup.Description;
                        CustomerLeadFileGroupDAO.Title = CustomerLeadFileGroup.Title;
                        CustomerLeadFileGroupDAO.FileTypeId = CustomerLeadFileGroup.FileTypeId;
                        CustomerLeadFileGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
                        CustomerLeadFileGroupDAO.DeletedAt = null;
                        CustomerLeadFileGroup.RowId = CustomerLeadFileGroupDAO.RowId;
                    }
                }
            }
            await DataContext.BulkMergeAsync(CustomerLeadFileGroupDAOs);

            if (CustomerLead.CustomerLeadFileGroups != null)
            {
                foreach (var CustomerLeadFileGroup in CustomerLead.CustomerLeadFileGroups)
                {
                    CustomerLeadFileGroup.Id = CustomerLeadFileGroupDAOs.Where(x => x.RowId == CustomerLeadFileGroup.RowId).Select(x => x.Id).FirstOrDefault();
                    if (CustomerLeadFileGroup.CustomerLeadFileMappings != null)
                    {
                        foreach (var CustomerLeadFileMapping in CustomerLeadFileGroup.CustomerLeadFileMappings)
                        {
                            CustomerLeadFileMappingDAO CustomerLeadFileMappingDAO = new CustomerLeadFileMappingDAO
                            {
                                FileId = CustomerLeadFileMapping.FileId,
                                CustomerLeadFileGroupId = CustomerLeadFileGroup.Id,
                            };
                            CustomerLeadFileMappingDAOs.Add(CustomerLeadFileMappingDAO);
                        }
                    }
                }
            }
            await DataContext.BulkMergeAsync(CustomerLeadFileMappingDAOs);
        }

        public async Task<bool> UpdateState(CustomerLead CustomerLead)
        {
            await DataContext.CustomerLead.Where(x => x.Id == CustomerLead.Id)
                .UpdateFromQueryAsync(x => new CustomerLeadDAO
                {
                    CustomerLeadStatusId = CustomerLeadStatusEnum.DONE.Id,
                    UpdatedAt = StaticParams.DateTimeNow
                });
            return true;
        }
    }
}
