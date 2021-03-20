using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Enums;
using System.ComponentModel;
using System.Reflection;

namespace CRM.Repositories
{
    public interface IContactRepository
    {
        Task<int> Count(ContactFilter ContactFilter);
        Task<List<Contact>> List(ContactFilter ContactFilter);
        Task<Contact> Get(long Id);
        Task<bool> Create(Contact Contact);
        Task<bool> Update(Contact Contact);
        Task<bool> Delete(Contact Contact);
        Task<bool> BulkMerge(List<Contact> Contacts);
        Task<bool> BulkDelete(List<Contact> Contacts);
    }
    public class ContactRepository : IContactRepository
    {
        private DataContext DataContext;
        public ContactRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContactDAO> DynamicFilter(IQueryable<ContactDAO> query, ContactFilter filter)
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
            if (filter.ProfessionId != null && filter.ProfessionId.HasValue)
                query = query.Where(q => q.ProfessionId.HasValue).Where(q => q.ProfessionId, filter.ProfessionId);
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, filter.CompanyId);
            if (filter.ContactStatusId != null && filter.ContactStatusId.HasValue)
                query = query.Where(q => q.ContactStatusId.HasValue).Where(q => q.ContactStatusId, filter.ContactStatusId);
            if (filter.Address != null && filter.Address.HasValue)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.NationId != null && filter.NationId.HasValue)
                query = query.Where(q => q.NationId.HasValue).Where(q => q.NationId, filter.NationId);
            if (filter.ProvinceId != null && filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.DistrictId != null && filter.DistrictId.HasValue)
                query = query.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, filter.DistrictId);
            if (filter.CustomerLeadId != null && filter.CustomerLeadId.HasValue)
                query = query.Where(q => q.CustomerLeadId.HasValue).Where(q => q.CustomerLeadId, filter.CustomerLeadId);
            if (filter.ImageId != null && filter.ImageId.HasValue)
                query = query.Where(q => q.ImageId.HasValue).Where(q => q.ImageId, filter.ImageId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.EmailOther != null && filter.EmailOther.HasValue)
                query = query.Where(q => q.EmailOther, filter.EmailOther);
            if (filter.DateOfBirth != null && filter.DateOfBirth.HasValue)
                query = query.Where(q => q.DateOfBirth == null).Union(query.Where(q => q.DateOfBirth.HasValue).Where(q => q.DateOfBirth, filter.DateOfBirth));
            if (filter.Phone != null && filter.Phone.HasValue)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.PhoneHome != null && filter.PhoneHome.HasValue)
                query = query.Where(q => q.PhoneHome, filter.PhoneHome);
            if (filter.FAX != null && filter.FAX.HasValue)
                query = query.Where(q => q.FAX, filter.FAX);
            if (filter.Email != null && filter.Email.HasValue)
                query = query.Where(q => q.Email, filter.Email);
            if (filter.Department != null && filter.Department.HasValue)
                query = query.Where(q => q.Department, filter.Department);
            if (filter.ZIPCode != null && filter.ZIPCode.HasValue)
                query = query.Where(q => q.ZIPCode, filter.ZIPCode);
            if (filter.SexId != null && filter.SexId.HasValue)
                query = query.Where(q => q.SexId.HasValue).Where(q => q.SexId, filter.SexId);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            if (filter.PositionId != null && filter.PositionId.HasValue)
                query = query.Where(q => q.PositionId.HasValue).Where(q => q.PositionId, filter.PositionId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
        
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ContactDAO> OrFilter(IQueryable<ContactDAO> query, ContactFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContactDAO> initQuery = query.Where(q => false);
            foreach (ContactFilter ContactFilter in filter.OrFilter)
            {
                IQueryable<ContactDAO> queryable = query;
                if (ContactFilter.Id != null && ContactFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContactFilter.Id);
                if (ContactFilter.Name != null && ContactFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ContactFilter.Name);
                if (ContactFilter.ProfessionId != null && ContactFilter.ProfessionId.HasValue)
                    queryable = queryable.Where(q => q.ProfessionId.HasValue).Where(q => q.ProfessionId, ContactFilter.ProfessionId);
                if (ContactFilter.CompanyId != null && ContactFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId.HasValue).Where(q => q.CompanyId, ContactFilter.CompanyId);
                if (ContactFilter.ContactStatusId != null && ContactFilter.ContactStatusId.HasValue)
                    queryable = queryable.Where(q => q.ContactStatusId.HasValue).Where(q => q.ContactStatusId, ContactFilter.ContactStatusId);
                if (ContactFilter.Address != null && ContactFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, ContactFilter.Address);
                if (ContactFilter.NationId != null && ContactFilter.NationId.HasValue)
                    queryable = queryable.Where(q => q.NationId.HasValue).Where(q => q.NationId, ContactFilter.NationId);
                if (ContactFilter.ProvinceId != null && ContactFilter.ProvinceId.HasValue)
                    queryable = queryable.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, ContactFilter.ProvinceId);
                if (ContactFilter.DistrictId != null && ContactFilter.DistrictId.HasValue)
                    queryable = queryable.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, ContactFilter.DistrictId);
                if (ContactFilter.CustomerLeadId != null && ContactFilter.CustomerLeadId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadId.HasValue).Where(q => q.CustomerLeadId, ContactFilter.CustomerLeadId);
                if (ContactFilter.ImageId != null && ContactFilter.ImageId.HasValue)
                    queryable = queryable.Where(q => q.ImageId.HasValue).Where(q => q.ImageId, ContactFilter.ImageId);
                if (ContactFilter.Description != null && ContactFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, ContactFilter.Description);
                if (ContactFilter.EmailOther != null && ContactFilter.EmailOther.HasValue)
                    queryable = queryable.Where(q => q.EmailOther, ContactFilter.EmailOther);
                if (ContactFilter.DateOfBirth != null && ContactFilter.DateOfBirth.HasValue)
                    queryable = queryable.Where(q => q.DateOfBirth.HasValue).Where(q => q.DateOfBirth, ContactFilter.DateOfBirth);
                if (ContactFilter.Phone != null && ContactFilter.Phone.HasValue)
                    queryable = queryable.Where(q => q.Phone, ContactFilter.Phone);
                if (ContactFilter.PhoneHome != null && ContactFilter.PhoneHome.HasValue)
                    queryable = queryable.Where(q => q.PhoneHome, ContactFilter.PhoneHome);
                if (ContactFilter.FAX != null && ContactFilter.FAX.HasValue)
                    queryable = queryable.Where(q => q.FAX, ContactFilter.FAX);
                if (ContactFilter.Email != null && ContactFilter.Email.HasValue)
                    queryable = queryable.Where(q => q.Email, ContactFilter.Email);
                if (ContactFilter.Department != null && ContactFilter.Department.HasValue)
                    queryable = queryable.Where(q => q.Department, ContactFilter.Department);
                if (ContactFilter.ZIPCode != null && ContactFilter.ZIPCode.HasValue)
                    queryable = queryable.Where(q => q.ZIPCode, ContactFilter.ZIPCode);
                if (ContactFilter.SexId != null && ContactFilter.SexId.HasValue)
                    queryable = queryable.Where(q => q.SexId.HasValue).Where(q => q.SexId, ContactFilter.SexId);
                if (ContactFilter.AppUserId != null && ContactFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, ContactFilter.AppUserId);
                if (ContactFilter.PositionId != null && ContactFilter.PositionId.HasValue)
                    queryable = queryable.Where(q => q.PositionId.HasValue).Where(q => q.PositionId, ContactFilter.PositionId);
                if (ContactFilter.CreatorId != null && ContactFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, ContactFilter.CreatorId);
             initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<ContactDAO> DynamicOrder(IQueryable<ContactDAO> query, ContactFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContactOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContactOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ContactOrder.Profession:
                            query = query.OrderBy(q => q.ProfessionId);
                            break;
                        case ContactOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case ContactOrder.ContactStatus:
                            query = query.OrderBy(q => q.ContactStatusId);
                            break;
                        case ContactOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case ContactOrder.Nation:
                            query = query.OrderBy(q => q.NationId);
                            break;
                        case ContactOrder.Province:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case ContactOrder.District:
                            query = query.OrderBy(q => q.DistrictId);
                            break;
                        case ContactOrder.CustomerLead:
                            query = query.OrderBy(q => q.CustomerLeadId);
                            break;
                        case ContactOrder.Image:
                            query = query.OrderBy(q => q.ImageId);
                            break;
                        case ContactOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ContactOrder.EmailOther:
                            query = query.OrderBy(q => q.EmailOther);
                            break;
                        case ContactOrder.DateOfBirth:
                            query = query.OrderBy(q => q.DateOfBirth);
                            break;
                        case ContactOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case ContactOrder.PhoneHome:
                            query = query.OrderBy(q => q.PhoneHome);
                            break;
                        case ContactOrder.FAX:
                            query = query.OrderBy(q => q.FAX);
                            break;
                        case ContactOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case ContactOrder.Department:
                            query = query.OrderBy(q => q.Department);
                            break;
                        case ContactOrder.ZIPCode:
                            query = query.OrderBy(q => q.ZIPCode);
                            break;
                        case ContactOrder.Sex:
                            query = query.OrderBy(q => q.SexId);
                            break;
                        case ContactOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case ContactOrder.RefuseReciveEmail:
                            query = query.OrderBy(q => q.RefuseReciveEmail);
                            break;
                        case ContactOrder.RefuseReciveSMS:
                            query = query.OrderBy(q => q.RefuseReciveSMS);
                            break;
                        case ContactOrder.Position:
                            query = query.OrderBy(q => q.PositionId);
                            break;
                        case ContactOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContactOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContactOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ContactOrder.Profession:
                            query = query.OrderByDescending(q => q.ProfessionId);
                            break;
                        case ContactOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case ContactOrder.ContactStatus:
                            query = query.OrderByDescending(q => q.ContactStatusId);
                            break;
                        case ContactOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case ContactOrder.Nation:
                            query = query.OrderByDescending(q => q.NationId);
                            break;
                        case ContactOrder.Province:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case ContactOrder.District:
                            query = query.OrderByDescending(q => q.DistrictId);
                            break;
                        case ContactOrder.CustomerLead:
                            query = query.OrderByDescending(q => q.CustomerLeadId);
                            break;
                        case ContactOrder.Image:
                            query = query.OrderByDescending(q => q.ImageId);
                            break;
                        case ContactOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ContactOrder.EmailOther:
                            query = query.OrderByDescending(q => q.EmailOther);
                            break;
                        case ContactOrder.DateOfBirth:
                            query = query.OrderByDescending(q => q.DateOfBirth);
                            break;
                        case ContactOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case ContactOrder.PhoneHome:
                            query = query.OrderByDescending(q => q.PhoneHome);
                            break;
                        case ContactOrder.FAX:
                            query = query.OrderByDescending(q => q.FAX);
                            break;
                        case ContactOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case ContactOrder.Department:
                            query = query.OrderByDescending(q => q.Department);
                            break;
                        case ContactOrder.ZIPCode:
                            query = query.OrderByDescending(q => q.ZIPCode);
                            break;
                        case ContactOrder.Sex:
                            query = query.OrderByDescending(q => q.SexId);
                            break;
                        case ContactOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case ContactOrder.RefuseReciveEmail:
                            query = query.OrderByDescending(q => q.RefuseReciveEmail);
                            break;
                        case ContactOrder.RefuseReciveSMS:
                            query = query.OrderByDescending(q => q.RefuseReciveSMS);
                            break;
                        case ContactOrder.Position:
                            query = query.OrderByDescending(q => q.PositionId);
                            break;
                        case ContactOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Contact>> DynamicSelect(IQueryable<ContactDAO> query, ContactFilter filter)
        {
            List<Contact> Contacts = await query.Select(q => new Contact()
            {
                Id = filter.Selects.Contains(ContactSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(ContactSelect.Name) ? q.Name : default(string),
                ProfessionId = filter.Selects.Contains(ContactSelect.Profession) ? q.ProfessionId : default(long?),
                CompanyId = filter.Selects.Contains(ContactSelect.Company) ? q.CompanyId : default(long?),
                ContactStatusId = filter.Selects.Contains(ContactSelect.ContactStatus) ? q.ContactStatusId : default(long?),
                Address = filter.Selects.Contains(ContactSelect.Address) ? q.Address : default(string),
                NationId = filter.Selects.Contains(ContactSelect.Nation) ? q.NationId : default(long?),
                ProvinceId = filter.Selects.Contains(ContactSelect.Province) ? q.ProvinceId : default(long?),
                DistrictId = filter.Selects.Contains(ContactSelect.District) ? q.DistrictId : default(long?),
                CustomerLeadId = filter.Selects.Contains(ContactSelect.CustomerLead) ? q.CustomerLeadId : default(long?),
                ImageId = filter.Selects.Contains(ContactSelect.Image) ? q.ImageId : default(long?),
                Description = filter.Selects.Contains(ContactSelect.Description) ? q.Description : default(string),
                EmailOther = filter.Selects.Contains(ContactSelect.EmailOther) ? q.EmailOther : default(string),
                DateOfBirth = filter.Selects.Contains(ContactSelect.DateOfBirth) ? q.DateOfBirth : default(DateTime?),
                Phone = filter.Selects.Contains(ContactSelect.Phone) ? q.Phone : default(string),
                PhoneHome = filter.Selects.Contains(ContactSelect.PhoneHome) ? q.PhoneHome : default(string),
                FAX = filter.Selects.Contains(ContactSelect.FAX) ? q.FAX : default(string),
                Email = filter.Selects.Contains(ContactSelect.Email) ? q.Email : default(string),
                Department = filter.Selects.Contains(ContactSelect.Department) ? q.Department : default(string),
                ZIPCode = filter.Selects.Contains(ContactSelect.ZIPCode) ? q.ZIPCode : default(string),
                SexId = filter.Selects.Contains(ContactSelect.Sex) ? q.SexId : default(long?),
                AppUserId = filter.Selects.Contains(ContactSelect.AppUser) ? q.AppUserId : default(long?),
                RefuseReciveEmail = filter.Selects.Contains(ContactSelect.RefuseReciveEmail) ? q.RefuseReciveEmail : default(bool?),
                RefuseReciveSMS = filter.Selects.Contains(ContactSelect.RefuseReciveSMS) ? q.RefuseReciveSMS : default(bool?),
                PositionId = filter.Selects.Contains(ContactSelect.Position) ? q.PositionId : default(long?),
                CreatorId = filter.Selects.Contains(ContactSelect.Creator) ? q.CreatorId : default(long),
                AppUser = filter.Selects.Contains(ContactSelect.AppUser) && q.AppUser != null ? new AppUser
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
                Company = filter.Selects.Contains(ContactSelect.Company) && q.Company != null ? new Company
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
                ContactStatus = filter.Selects.Contains(ContactSelect.ContactStatus) && q.ContactStatus != null ? new ContactStatus
                {
                    Id = q.ContactStatus.Id,
                    Code = q.ContactStatus.Code,
                    Name = q.ContactStatus.Name,
                } : null,
                Creator = filter.Selects.Contains(ContactSelect.Creator) && q.Creator != null ? new AppUser
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
                CustomerLead = filter.Selects.Contains(ContactSelect.CustomerLead) && q.CustomerLead != null ? new CustomerLead
                {
                    Id = q.CustomerLead.Id,
                    Name = q.CustomerLead.Name,
                    CompanyName = q.CustomerLead.CompanyName,
                    TelePhone = q.CustomerLead.TelePhone,
                    Phone = q.CustomerLead.Phone,
                    Fax = q.CustomerLead.Fax,
                    Email = q.CustomerLead.Email,
                    SecondEmail = q.CustomerLead.SecondEmail,
                    Website = q.CustomerLead.Website,
                    CustomerLeadSourceId = q.CustomerLead.CustomerLeadSourceId,
                    CustomerLeadLevelId = q.CustomerLead.CustomerLeadLevelId,
                    CompanyId = q.CustomerLead.CompanyId,
                    CampaignId = q.CustomerLead.CampaignId,
                    ProfessionId = q.CustomerLead.ProfessionId,
                    Revenue = q.CustomerLead.Revenue,
                    EmployeeQuantity = q.CustomerLead.EmployeeQuantity,
                    Address = q.CustomerLead.Address,
                    NationId = q.CustomerLead.NationId,
                    ProvinceId = q.CustomerLead.ProvinceId,
                    DistrictId = q.CustomerLead.DistrictId,
                    CustomerLeadStatusId = q.CustomerLead.CustomerLeadStatusId,
                    BusinessRegistrationCode = q.CustomerLead.BusinessRegistrationCode,
                    SexId = q.CustomerLead.SexId,
                    RefuseReciveSMS = q.CustomerLead.RefuseReciveSMS,
                    RefuseReciveEmail = q.CustomerLead.RefuseReciveEmail,
                    Description = q.CustomerLead.Description,
                    AppUserId = q.CustomerLead.AppUserId,
                    CreatorId = q.CustomerLead.CreatorId,
                    ZipCode = q.CustomerLead.ZipCode,
                    CurrencyId = q.CustomerLead.CurrencyId,
                    RowId = q.CustomerLead.RowId,
                } : null,
                District = filter.Selects.Contains(ContactSelect.District) && q.District != null ? new District
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
                Image = filter.Selects.Contains(ContactSelect.Image) && q.Image != null ? new Image
                {
                    Id = q.Image.Id,
                    Name = q.Image.Name,
                    Url = q.Image.Url,
                    ThumbnailUrl = q.Image.ThumbnailUrl,
                    RowId = q.Image.RowId,
                } : null,
                Nation = filter.Selects.Contains(ContactSelect.Nation) && q.Nation != null ? new Nation
                {
                    Id = q.Nation.Id,
                    Code = q.Nation.Code,
                    Name = q.Nation.Name,
                    Priority = q.Nation.Priority,
                    StatusId = q.Nation.StatusId,
                    Used = q.Nation.Used,
                    RowId = q.Nation.RowId,
                } : null,
               
                Position = filter.Selects.Contains(ContactSelect.Position) && q.Position != null ? new Position
                {
                    Id = q.Position.Id,
                    Code = q.Position.Code,
                    Name = q.Position.Name,
                    StatusId = q.Position.StatusId,
                    RowId = q.Position.RowId,
                    Used = q.Position.Used,
                } : null,
                Profession = filter.Selects.Contains(ContactSelect.Profession) && q.Profession != null ? new Profession
                {
                    Id = q.Profession.Id,
                    Code = q.Profession.Code,
                    Name = q.Profession.Name,
                    StatusId = q.Profession.StatusId,
                    RowId = q.Profession.RowId,
                    Used = q.Profession.Used,
                } : null,
                Province = filter.Selects.Contains(ContactSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Code = q.Province.Code,
                    Name = q.Province.Name,
                    Priority = q.Province.Priority,
                    StatusId = q.Province.StatusId,
                    RowId = q.Province.RowId,
                    Used = q.Province.Used,
                } : null,
                Sex = filter.Selects.Contains(ContactSelect.Sex) && q.Sex != null ? new Sex
                {
                    Id = q.Sex.Id,
                    Code = q.Sex.Code,
                    Name = q.Sex.Name,
                } : null,
            }).ToListAsync();
            return Contacts;
        }

        public async Task<int> Count(ContactFilter filter)
        {
            IQueryable<ContactDAO> Contacts = DataContext.Contact.AsNoTracking();
            Contacts = DynamicFilter(Contacts, filter);
            return await Contacts.CountAsync();
        }

        public async Task<List<Contact>> List(ContactFilter filter)
        {
            if (filter == null) return new List<Contact>();
            IQueryable<ContactDAO> ContactDAOs = DataContext.Contact.AsNoTracking();
            ContactDAOs = DynamicFilter(ContactDAOs, filter);
            ContactDAOs = DynamicOrder(ContactDAOs, filter);
            List<Contact> Contacts = await DynamicSelect(ContactDAOs, filter);
            return Contacts;
        }

        public async Task<Contact> Get(long Id)
        {
            Contact Contact = await DataContext.Contact.AsNoTracking()
           .Where(x => x.Id == Id)
           .Where(x => x.DeletedAt == null)
           .Select(x => new Contact()
           {
               CreatedAt = x.CreatedAt,
               UpdatedAt = x.UpdatedAt,
               Id = x.Id,
               Name = x.Name,
               ProfessionId = x.ProfessionId,
               CompanyId = x.CompanyId,
               ContactStatusId = x.ContactStatusId,
               Address = x.Address,
               NationId = x.NationId,
               ProvinceId = x.ProvinceId,
               DistrictId = x.DistrictId,
               CustomerLeadId = x.CustomerLeadId,
               ImageId = x.ImageId,
               Description = x.Description,
               EmailOther = x.EmailOther,
               DateOfBirth = x.DateOfBirth,
               Phone = x.Phone,
               PhoneHome = x.PhoneHome,
               FAX = x.FAX,
               Email = x.Email,
               Department = x.Department,
               ZIPCode = x.ZIPCode,
               SexId = x.SexId,
               AppUserId = x.AppUserId,
               RefuseReciveEmail = x.RefuseReciveEmail,
               RefuseReciveSMS = x.RefuseReciveSMS,
               PositionId = x.PositionId,
               CreatorId = x.CreatorId,
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
               ContactStatus = x.ContactStatus == null ? null : new ContactStatus
               {
                   Id = x.ContactStatus.Id,
                   Code = x.ContactStatus.Code,
                   Name = x.ContactStatus.Name,
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
               CustomerLead = x.CustomerLead == null ? null : new CustomerLead
               {
                   Id = x.CustomerLead.Id,
                   Name = x.CustomerLead.Name,
                   CompanyName = x.CustomerLead.CompanyName,
                   TelePhone = x.CustomerLead.TelePhone,
                   Phone = x.CustomerLead.Phone,
                   Fax = x.CustomerLead.Fax,
                   Email = x.CustomerLead.Email,
                   SecondEmail = x.CustomerLead.SecondEmail,
                   Website = x.CustomerLead.Website,
                   CustomerLeadSourceId = x.CustomerLead.CustomerLeadSourceId,
                   CustomerLeadLevelId = x.CustomerLead.CustomerLeadLevelId,
                   CompanyId = x.CustomerLead.CompanyId,
                   CampaignId = x.CustomerLead.CampaignId,
                   ProfessionId = x.CustomerLead.ProfessionId,
                   Revenue = x.CustomerLead.Revenue,
                   EmployeeQuantity = x.CustomerLead.EmployeeQuantity,
                   Address = x.CustomerLead.Address,
                   NationId = x.CustomerLead.NationId,
                   ProvinceId = x.CustomerLead.ProvinceId,
                   DistrictId = x.CustomerLead.DistrictId,
                   CustomerLeadStatusId = x.CustomerLead.CustomerLeadStatusId,
                   BusinessRegistrationCode = x.CustomerLead.BusinessRegistrationCode,
                   SexId = x.CustomerLead.SexId,
                   RefuseReciveSMS = x.CustomerLead.RefuseReciveSMS,
                   RefuseReciveEmail = x.CustomerLead.RefuseReciveEmail,
                   Description = x.CustomerLead.Description,
                   AppUserId = x.CustomerLead.AppUserId,
                   CreatorId = x.CustomerLead.CreatorId,
                   ZipCode = x.CustomerLead.ZipCode,
                   CurrencyId = x.CustomerLead.CurrencyId,
                   RowId = x.CustomerLead.RowId,
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
               Image = x.Image == null ? null : new Image
               {
                   Id = x.Image.Id,
                   Name = x.Image.Name,
                   Url = x.Image.Url,
                   ThumbnailUrl = x.Image.ThumbnailUrl,
                   RowId = x.Image.RowId,
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
              
               Position = x.Position == null ? null : new Position
               {
                   Id = x.Position.Id,
                   Code = x.Position.Code,
                   Name = x.Position.Name,
                   StatusId = x.Position.StatusId,
                   RowId = x.Position.RowId,
                   Used = x.Position.Used,
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

            if (Contact == null)
                return null;

            Contact.ContactFileGroupings = await DataContext.ContactFileGrouping
                .Where(x => x.ContactId == Id)
                .Where(x => x.DeletedAt == null)
                .Select(x => new ContactFileGrouping
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    FileTypeId = x.FileTypeId,
                    ContactId = x.ContactId,
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
            var ContactFileGroupingIds = Contact.ContactFileGroupings.Select(x => x.Id).ToList();

            var ContactFileMappings = await DataContext.ContactFileMapping.Where(x => ContactFileGroupingIds.Contains(x.ContactFileGroupingId))
                .Select(x => new ContactFileMapping
                {
                    ContactFileGroupingId = x.ContactFileGroupingId,
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

            foreach (ContactFileGrouping ContactFileGrouping in Contact.ContactFileGroupings)
            {
                ContactFileGrouping.ContactFileMappings = ContactFileMappings.Where(x => x.ContactFileGroupingId == ContactFileGrouping.Id).ToList();
            }
            return Contact;
        }
        public async Task<bool> Create(Contact Contact)
        {
            ContactDAO ContactDAO = new ContactDAO();
            ContactDAO.Id = Contact.Id;
            ContactDAO.Name = Contact.Name;
            ContactDAO.ProfessionId = Contact.ProfessionId;
            ContactDAO.CompanyId = Contact.CompanyId;
            ContactDAO.ContactStatusId = Contact.ContactStatusId;
            ContactDAO.Address = Contact.Address;
            ContactDAO.NationId = Contact.NationId;
            ContactDAO.ProvinceId = Contact.ProvinceId;
            ContactDAO.DistrictId = Contact.DistrictId;
            ContactDAO.CustomerLeadId = Contact.CustomerLeadId;
            ContactDAO.ImageId = Contact.ImageId;
            ContactDAO.Description = Contact.Description;
            ContactDAO.EmailOther = Contact.EmailOther;
            ContactDAO.DateOfBirth = Contact.DateOfBirth;
            ContactDAO.Phone = Contact.Phone;
            ContactDAO.PhoneHome = Contact.PhoneHome;
            ContactDAO.FAX = Contact.FAX;
            ContactDAO.Email = Contact.Email;
            ContactDAO.Department = Contact.Department;
            ContactDAO.ZIPCode = Contact.ZIPCode;
            ContactDAO.SexId = Contact.SexId;
            ContactDAO.AppUserId = Contact.AppUserId;
            ContactDAO.RefuseReciveEmail = Contact.RefuseReciveEmail;
            ContactDAO.RefuseReciveSMS = Contact.RefuseReciveSMS;
            ContactDAO.PositionId = Contact.PositionId;
            ContactDAO.CreatorId = Contact.CreatorId;
            ContactDAO.CreatedAt = StaticParams.DateTimeNow;
            ContactDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Contact.Add(ContactDAO);
            await DataContext.SaveChangesAsync();
            Contact.Id = ContactDAO.Id;
            await SaveReference(Contact);
            return true;
        }

        public async Task<bool> Update(Contact Contact)
        {
            ContactDAO ContactDAO = DataContext.Contact.Where(x => x.Id == Contact.Id).FirstOrDefault();
            if (ContactDAO == null)
                return false;
            ContactDAO.Id = Contact.Id;
            ContactDAO.Name = Contact.Name;
            ContactDAO.ProfessionId = Contact.ProfessionId;
            ContactDAO.CompanyId = Contact.CompanyId;
            ContactDAO.ContactStatusId = Contact.ContactStatusId;
            ContactDAO.Address = Contact.Address;
            ContactDAO.NationId = Contact.NationId;
            ContactDAO.ProvinceId = Contact.ProvinceId;
            ContactDAO.DistrictId = Contact.DistrictId;
            ContactDAO.CustomerLeadId = Contact.CustomerLeadId;
            ContactDAO.ImageId = Contact.ImageId;
            ContactDAO.Description = Contact.Description;
            ContactDAO.EmailOther = Contact.EmailOther;
            ContactDAO.DateOfBirth = Contact.DateOfBirth;
            ContactDAO.Phone = Contact.Phone;
            ContactDAO.PhoneHome = Contact.PhoneHome;
            ContactDAO.FAX = Contact.FAX;
            ContactDAO.Email = Contact.Email;
            ContactDAO.Department = Contact.Department;
            ContactDAO.ZIPCode = Contact.ZIPCode;
            ContactDAO.SexId = Contact.SexId;
            ContactDAO.AppUserId = Contact.AppUserId;
            ContactDAO.RefuseReciveEmail = Contact.RefuseReciveEmail;
            ContactDAO.RefuseReciveSMS = Contact.RefuseReciveSMS;
            ContactDAO.PositionId = Contact.PositionId;
            ContactDAO.CreatorId = Contact.CreatorId;
            ContactDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Contact);
            return true;
        }

        public async Task<bool> Delete(Contact Contact)
        {
            await DataContext.Contact.Where(x => x.Id == Contact.Id).UpdateFromQueryAsync(x => new ContactDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<Contact> Contacts)
        {
            List<ContactDAO> ContactDAOs = new List<ContactDAO>();
            foreach (Contact Contact in Contacts)
            {
                ContactDAO ContactDAO = new ContactDAO();
                ContactDAO.Id = Contact.Id;
                ContactDAO.Name = Contact.Name;
                ContactDAO.ProfessionId = Contact.ProfessionId;
                ContactDAO.CompanyId = Contact.CompanyId;
                ContactDAO.ContactStatusId = Contact.ContactStatusId;
                ContactDAO.Address = Contact.Address;
                ContactDAO.NationId = Contact.NationId;
                ContactDAO.ProvinceId = Contact.ProvinceId;
                ContactDAO.DistrictId = Contact.DistrictId;
                ContactDAO.CustomerLeadId = Contact.CustomerLeadId;
                ContactDAO.ImageId = Contact.ImageId;
                ContactDAO.Description = Contact.Description;
                ContactDAO.EmailOther = Contact.EmailOther;
                ContactDAO.DateOfBirth = Contact.DateOfBirth;
                ContactDAO.Phone = Contact.Phone;
                ContactDAO.PhoneHome = Contact.PhoneHome;
                ContactDAO.FAX = Contact.FAX;
                ContactDAO.Email = Contact.Email;
                ContactDAO.Department = Contact.Department;
                ContactDAO.ZIPCode = Contact.ZIPCode;
                ContactDAO.SexId = Contact.SexId;
                ContactDAO.AppUserId = Contact.AppUserId;
                ContactDAO.RefuseReciveEmail = Contact.RefuseReciveEmail;
                ContactDAO.RefuseReciveSMS = Contact.RefuseReciveSMS;
                ContactDAO.PositionId = Contact.PositionId;
                ContactDAO.CreatorId = Contact.CreatorId;
                ContactDAO.CreatedAt = StaticParams.DateTimeNow;
                ContactDAO.UpdatedAt = StaticParams.DateTimeNow;
                ContactDAOs.Add(ContactDAO);
            }
            await DataContext.BulkMergeAsync(ContactDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<Contact> Contacts)
        {
            List<long> Ids = Contacts.Select(x => x.Id).ToList();
            await DataContext.Contact
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ContactDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(Contact Contact)
        {
            List<ContactFileGroupingDAO> ContactFileGroupingDAOs = await DataContext.ContactFileGrouping.Where(x => x.ContactId == Contact.Id).ToListAsync();
            ContactFileGroupingDAOs.ForEach(x => x.DeletedAt = StaticParams.DateTimeNow);
            await DataContext.ContactFileMapping.Where(x => x.ContactFileGrouping.ContactId == Contact.Id).DeleteFromQueryAsync();
            List<ContactFileMappingDAO> ContactFileMappingDAOs = new List<ContactFileMappingDAO>();
            if (Contact.ContactFileGroupings != null)
            {
                foreach (var ContactFileGrouping in Contact.ContactFileGroupings)
                {
                    ContactFileGroupingDAO ContactFileGroupingDAO = ContactFileGroupingDAOs.Where(x => x.Id == ContactFileGrouping.Id && x.Id != 0).FirstOrDefault();
                    if (ContactFileGroupingDAO == null)
                    {
                        ContactFileGroupingDAO = new ContactFileGroupingDAO
                        {
                            CreatorId = ContactFileGrouping.CreatorId,
                            ContactId = Contact.Id,
                            Description = ContactFileGrouping.Description,
                            Title = ContactFileGrouping.Title,
                            FileTypeId = ContactFileGrouping.FileTypeId,
                            CreatedAt = StaticParams.DateTimeNow,
                            UpdatedAt = StaticParams.DateTimeNow,
                            RowId = Guid.NewGuid()
                        };
                        ContactFileGroupingDAOs.Add(ContactFileGroupingDAO);
                        ContactFileGrouping.RowId = ContactFileGroupingDAO.RowId;
                    }
                    else
                    {
                        ContactFileGroupingDAO.Description = ContactFileGrouping.Description;
                        ContactFileGroupingDAO.Title = ContactFileGrouping.Title;
                        ContactFileGroupingDAO.FileTypeId = ContactFileGrouping.FileTypeId;
                        ContactFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                        ContactFileGroupingDAO.DeletedAt = null;
                        ContactFileGrouping.RowId = ContactFileGroupingDAO.RowId;
                    }
                }
            }
            await DataContext.BulkMergeAsync(ContactFileGroupingDAOs);
            if (Contact.ContactFileGroupings != null)
            {
                foreach (var ContactFileGrouping in Contact.ContactFileGroupings)
                {
                    ContactFileGrouping.Id = ContactFileGroupingDAOs.Where(x => x.RowId == ContactFileGrouping.RowId).Select(x => x.Id).FirstOrDefault();
                    if (ContactFileGrouping.ContactFileMappings != null)
                    {
                        foreach (var ContactFileMapping in ContactFileGrouping.ContactFileMappings)
                        {
                            ContactFileMappingDAO ContactFileMappingDAO = new ContactFileMappingDAO
                            {
                                FileId = ContactFileMapping.FileId,
                                ContactFileGroupingId = ContactFileGrouping.Id,
                            };
                            ContactFileMappingDAOs.Add(ContactFileMappingDAO);
                        }
                    }
                }
            }
            await DataContext.BulkMergeAsync(ContactFileMappingDAOs);
        }

        private async Task AuditLogProperty(Contact Contact)
        {
            var modifiedEntities = DataContext.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified
                || p.State == EntityState.Added
                || p.State == EntityState.Deleted
                || p.State == EntityState.Modified
                || p.State == EntityState.Detached)
                .ToList();


            foreach (var change in modifiedEntities)
            {
                var type = change.Entity.GetType();

                var EntityDisplayName = type.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Select(x => ((DisplayNameAttribute)x).DisplayName)
                .DefaultIfEmpty(type.Name)
                .First();


                if (change.State == EntityState.Modified)
                {
                    foreach (var prop in change.Entity.GetType().GetTypeInfo().DeclaredProperties)
                    {
                        if (!prop.GetGetMethod().IsVirtual)
                        {
                            var currentValue = change.Property(prop.Name).CurrentValue;
                            var originalValue = change.Property(prop.Name).OriginalValue;
                            if (!Equals(originalValue, currentValue))
                            {
                                var attributes = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                                if (attributes != null && attributes.Length > 0)
                                {
                                    AuditLogPropertyDAO AuditLogPropertyDAO = new AuditLogPropertyDAO();
                                    var displayName = (DisplayNameAttribute)attributes[0];
                                    AuditLogPropertyDAO.OldValue = originalValue == null ? "" : originalValue.ToString();
                                    AuditLogPropertyDAO.NewValue = currentValue == null ? "" : currentValue.ToString();
                                    if (prop.Name.Equals("ProvinceId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            Province ProvinceNew = await DataContext.Province.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Province()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.NewValue = ProvinceNew.Name;
                                        }


                                        if (originalValue != null)
                                        {
                                            Province ProvinceOld = await DataContext.Province.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Province()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = ProvinceOld.Name;
                                        }


                                    }
                                    else if (prop.Name.Equals("DistrictId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            District DistrictNew = await DataContext.District.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new District()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.NewValue = DistrictNew.Name;
                                        }

                                        if (originalValue != null)
                                        {
                                            District DistrictOld = await DataContext.District.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new District()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = DistrictOld.Name;
                                        }
                                    }
                                    else if (prop.Name.Equals("AppUserAssignedId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            AppUser AppUserNew = await DataContext.AppUser.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new AppUser()
                                            {
                                                Id = x.Id,
                                                DisplayName = x.DisplayName,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.NewValue = AppUserNew.DisplayName;
                                        }

                                        if (originalValue != null)
                                        {
                                            AppUser AppUserOld = await DataContext.AppUser.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new AppUser()
                                            {
                                                Id = x.Id,
                                                DisplayName = x.DisplayName,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = AppUserOld.DisplayName;
                                        }


                                    }
                                    else if (prop.Name.Equals("ProfessionId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            Profession ProfessionNew = await DataContext.Profession.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Profession()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.NewValue = ProfessionNew.Name;
                                        }

                                        if (originalValue != null)
                                        {
                                            Profession ProfessionOld = await DataContext.Profession.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Profession()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.OldValue = ProfessionOld.Name;
                                        }
                                    }
                                    else if (prop.Name.Equals("LeadSourceId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            CustomerLeadSource CustomerLeadSourceNew = await DataContext.CustomerLeadSource.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new CustomerLeadSource()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.NewValue = CustomerLeadSourceNew.Name;
                                        }

                                        if (originalValue != null)
                                        {
                                            CustomerLeadSource CustomerLeadSourceOld = await DataContext.CustomerLeadSource.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new CustomerLeadSource()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = CustomerLeadSourceOld.Name;
                                        }

                                    }
                                    else if (prop.Name.Equals("SexId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            Sex SexNew = await DataContext.Sex.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Sex()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.NewValue = SexNew.Name;
                                        }


                                        if (originalValue != null)
                                        {
                                            Sex SexOld = await DataContext.Sex.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Sex()
                                            {
                                                Id = x.Id,
                                                Code = x.Code,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = SexOld.Name;
                                        }


                                    }
                                    else if (prop.Name.Equals("CompanyId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            Company CompanyNew = await DataContext.Company.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Company()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.NewValue = CompanyNew.Name;
                                        }

                                        if (originalValue != null)
                                        {
                                            Company CompanyOld = await DataContext.Company.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Company()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = CompanyOld.Name;
                                        }

                                    }
                                    else if (prop.Name.Equals("NationId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            Nation NationNew = await DataContext.Nation.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Nation()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.NewValue = NationNew.Name;
                                        }

                                        if (originalValue != null)
                                        {
                                            Nation NationOld = await DataContext.Nation.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Nation()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = NationOld.Name;
                                        }

                                    }
                                    else if (prop.Name.Equals("PositionId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            Position PositionNew = await DataContext.Position.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Position()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.NewValue = PositionNew.Name;
                                        }

                                        if (originalValue != null)
                                        {
                                            Position PositionOld = await DataContext.Position.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Position()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = PositionOld.Name;
                                        }


                                    }
                                    else if (prop.Name.Equals("OrganizationId"))
                                    {
                                        if (currentValue != null)
                                        {
                                            Organization New = await DataContext.Organization.Where(x => x.Id == (long)currentValue).AsNoTracking().Select(x => new Organization()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();
                                            AuditLogPropertyDAO.NewValue = New.Name;
                                        }

                                        if (originalValue != null)
                                        {
                                            Organization Old = await DataContext.Organization.Where(x => x.Id == (long)originalValue).AsNoTracking().Select(x => new Organization()
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                            }).FirstOrDefaultAsync();

                                            AuditLogPropertyDAO.OldValue = Old.Name;
                                        }
                                    }
                                    AuditLogPropertyDAO.Property = displayName.DisplayName;
                                    AuditLogPropertyDAO.ActionName = AuditLogPropertyActionEnum.EDIT.Name;
                                    AuditLogPropertyDAO.Time = StaticParams.DateTimeNow;
                                    AuditLogPropertyDAO.ClassName = EntityDisplayName;
                                    AuditLogPropertyDAO.AppUserId = 2;
                                    DataContext.AuditLogProperty.Add(AuditLogPropertyDAO);
                                    await DataContext.SaveChangesAsync();
                                    //ContactAuditLogPropertyMappingDAO ContactAuditLogPropertyMappingDAO = new ContactAuditLogPropertyMappingDAO();
                                    //ContactAuditLogPropertyMappingDAO.ContactId = Contact.Id;
                                    //ContactAuditLogPropertyMappingDAO.AuditLogPropertyId = AuditLogPropertyDAO.Id;
                                    //DataContext.ContactAuditLogPropertyMapping.Add(ContactAuditLogPropertyMappingDAO);
                                }
                            }
                        }

                    }
                }
            }
        }

    }
}
