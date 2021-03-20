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
    public interface IContactFileGroupingRepository
    {
        Task<int> Count(ContactFileGroupingFilter ContactFileGroupingFilter);
        Task<List<ContactFileGrouping>> List(ContactFileGroupingFilter ContactFileGroupingFilter);
        Task<List<ContactFileGrouping>> List(List<long> Ids);
        Task<ContactFileGrouping> Get(long Id);
        Task<bool> Create(ContactFileGrouping ContactFileGrouping);
        Task<bool> Update(ContactFileGrouping ContactFileGrouping);
        Task<bool> Delete(ContactFileGrouping ContactFileGrouping);
        Task<bool> BulkMerge(List<ContactFileGrouping> ContactFileGroupings);
        Task<bool> BulkDelete(List<ContactFileGrouping> ContactFileGroupings);
    }
    public class ContactFileGroupingRepository : IContactFileGroupingRepository
    {
        private DataContext DataContext;
        public ContactFileGroupingRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContactFileGroupingDAO> DynamicFilter(IQueryable<ContactFileGroupingDAO> query, ContactFileGroupingFilter filter)
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
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.ContactId != null && filter.ContactId.HasValue)
                query = query.Where(q => q.ContactId, filter.ContactId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.FileTypeId != null && filter.FileTypeId.HasValue)
                query = query.Where(q => q.FileTypeId, filter.FileTypeId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ContactFileGroupingDAO> OrFilter(IQueryable<ContactFileGroupingDAO> query, ContactFileGroupingFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContactFileGroupingDAO> initQuery = query.Where(q => false);
            foreach (ContactFileGroupingFilter ContactFileGroupingFilter in filter.OrFilter)
            {
                IQueryable<ContactFileGroupingDAO> queryable = query;
                if (ContactFileGroupingFilter.Id != null && ContactFileGroupingFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContactFileGroupingFilter.Id);
                if (ContactFileGroupingFilter.Title != null && ContactFileGroupingFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, ContactFileGroupingFilter.Title);
                if (ContactFileGroupingFilter.Description != null && ContactFileGroupingFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, ContactFileGroupingFilter.Description);
                if (ContactFileGroupingFilter.ContactId != null && ContactFileGroupingFilter.ContactId.HasValue)
                    queryable = queryable.Where(q => q.ContactId, ContactFileGroupingFilter.ContactId);
                if (ContactFileGroupingFilter.CreatorId != null && ContactFileGroupingFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, ContactFileGroupingFilter.CreatorId);
                if (ContactFileGroupingFilter.FileTypeId != null && ContactFileGroupingFilter.FileTypeId.HasValue)
                    queryable = queryable.Where(q => q.FileTypeId, ContactFileGroupingFilter.FileTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ContactFileGroupingDAO> DynamicOrder(IQueryable<ContactFileGroupingDAO> query, ContactFileGroupingFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContactFileGroupingOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContactFileGroupingOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case ContactFileGroupingOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ContactFileGroupingOrder.Contact:
                            query = query.OrderBy(q => q.ContactId);
                            break;
                        case ContactFileGroupingOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case ContactFileGroupingOrder.FileType:
                            query = query.OrderBy(q => q.FileTypeId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContactFileGroupingOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContactFileGroupingOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case ContactFileGroupingOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ContactFileGroupingOrder.Contact:
                            query = query.OrderByDescending(q => q.ContactId);
                            break;
                        case ContactFileGroupingOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case ContactFileGroupingOrder.FileType:
                            query = query.OrderByDescending(q => q.FileTypeId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ContactFileGrouping>> DynamicSelect(IQueryable<ContactFileGroupingDAO> query, ContactFileGroupingFilter filter)
        {
            List<ContactFileGrouping> ContactFileGroupings = await query.Select(q => new ContactFileGrouping()
            {
                Id = filter.Selects.Contains(ContactFileGroupingSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(ContactFileGroupingSelect.Title) ? q.Title : default(string),
                Description = filter.Selects.Contains(ContactFileGroupingSelect.Description) ? q.Description : default(string),
                ContactId = filter.Selects.Contains(ContactFileGroupingSelect.Contact) ? q.ContactId : default(long),
                CreatorId = filter.Selects.Contains(ContactFileGroupingSelect.Creator) ? q.CreatorId : default(long),
                FileTypeId = filter.Selects.Contains(ContactFileGroupingSelect.FileType) ? q.FileTypeId : default(long),
                Contact = filter.Selects.Contains(ContactFileGroupingSelect.Contact) && q.Contact != null ? new Contact
                {
                    Id = q.Contact.Id,
                    Name = q.Contact.Name,
                    ProfessionId = q.Contact.ProfessionId,
                    CompanyId = q.Contact.CompanyId,
                    ContactStatusId = q.Contact.ContactStatusId,
                    Address = q.Contact.Address,
                    NationId = q.Contact.NationId,
                    ProvinceId = q.Contact.ProvinceId,
                    DistrictId = q.Contact.DistrictId,
                    CustomerLeadId = q.Contact.CustomerLeadId,
                    ImageId = q.Contact.ImageId,
                    Description = q.Contact.Description,
                    EmailOther = q.Contact.EmailOther,
                    DateOfBirth = q.Contact.DateOfBirth,
                    Phone = q.Contact.Phone,
                    PhoneHome = q.Contact.PhoneHome,
                    FAX = q.Contact.FAX,
                    Email = q.Contact.Email,
                    Department = q.Contact.Department,
                    ZIPCode = q.Contact.ZIPCode,
                    SexId = q.Contact.SexId,
                    AppUserId = q.Contact.AppUserId,
                    RefuseReciveEmail = q.Contact.RefuseReciveEmail,
                    RefuseReciveSMS = q.Contact.RefuseReciveSMS,
                    PositionId = q.Contact.PositionId,
                    CreatorId = q.Contact.CreatorId,
                } : null,
                Creator = filter.Selects.Contains(ContactFileGroupingSelect.Creator) && q.Creator != null ? new AppUser
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
                FileType = filter.Selects.Contains(ContactFileGroupingSelect.FileType) && q.FileType != null ? new FileType
                {
                    Id = q.FileType.Id,
                    Code = q.FileType.Code,
                    Name = q.FileType.Name,
                } : null,
            }).ToListAsync();
            return ContactFileGroupings;
        }

        public async Task<int> Count(ContactFileGroupingFilter filter)
        {
            IQueryable<ContactFileGroupingDAO> ContactFileGroupings = DataContext.ContactFileGrouping.AsNoTracking();
            ContactFileGroupings = DynamicFilter(ContactFileGroupings, filter);
            return await ContactFileGroupings.CountAsync();
        }

        public async Task<List<ContactFileGrouping>> List(ContactFileGroupingFilter filter)
        {
            if (filter == null) return new List<ContactFileGrouping>();
            IQueryable<ContactFileGroupingDAO> ContactFileGroupingDAOs = DataContext.ContactFileGrouping.AsNoTracking();
            ContactFileGroupingDAOs = DynamicFilter(ContactFileGroupingDAOs, filter);
            ContactFileGroupingDAOs = DynamicOrder(ContactFileGroupingDAOs, filter);
            List<ContactFileGrouping> ContactFileGroupings = await DynamicSelect(ContactFileGroupingDAOs, filter);
            return ContactFileGroupings;
        }

        public async Task<List<ContactFileGrouping>> List(List<long> Ids)
        {
            List<ContactFileGrouping> ContactFileGroupings = await DataContext.ContactFileGrouping.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ContactFileGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ContactId = x.ContactId,
                CreatorId = x.CreatorId,
                FileTypeId = x.FileTypeId,
                RowId = x.RowId,
                Contact = x.Contact == null ? null : new Contact
                {
                    Id = x.Contact.Id,
                    Name = x.Contact.Name,
                    ProfessionId = x.Contact.ProfessionId,
                    CompanyId = x.Contact.CompanyId,
                    ContactStatusId = x.Contact.ContactStatusId,
                    Address = x.Contact.Address,
                    NationId = x.Contact.NationId,
                    ProvinceId = x.Contact.ProvinceId,
                    DistrictId = x.Contact.DistrictId,
                    CustomerLeadId = x.Contact.CustomerLeadId,
                    ImageId = x.Contact.ImageId,
                    Description = x.Contact.Description,
                    EmailOther = x.Contact.EmailOther,
                    DateOfBirth = x.Contact.DateOfBirth,
                    Phone = x.Contact.Phone,
                    PhoneHome = x.Contact.PhoneHome,
                    FAX = x.Contact.FAX,
                    Email = x.Contact.Email,
                    Department = x.Contact.Department,
                    ZIPCode = x.Contact.ZIPCode,
                    SexId = x.Contact.SexId,
                    AppUserId = x.Contact.AppUserId,
                    RefuseReciveEmail = x.Contact.RefuseReciveEmail,
                    RefuseReciveSMS = x.Contact.RefuseReciveSMS,
                    PositionId = x.Contact.PositionId,
                    CreatorId = x.Contact.CreatorId,
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
                FileType = x.FileType == null ? null : new FileType
                {
                    Id = x.FileType.Id,
                    Code = x.FileType.Code,
                    Name = x.FileType.Name,
                },
            }).ToListAsync();
            

            return ContactFileGroupings;
        }

        public async Task<ContactFileGrouping> Get(long Id)
        {
            ContactFileGrouping ContactFileGrouping = await DataContext.ContactFileGrouping.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new ContactFileGrouping()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ContactId = x.ContactId,
                CreatorId = x.CreatorId,
                FileTypeId = x.FileTypeId,
                RowId = x.RowId,
                Contact = x.Contact == null ? null : new Contact
                {
                    Id = x.Contact.Id,
                    Name = x.Contact.Name,
                    ProfessionId = x.Contact.ProfessionId,
                    CompanyId = x.Contact.CompanyId,
                    ContactStatusId = x.Contact.ContactStatusId,
                    Address = x.Contact.Address,
                    NationId = x.Contact.NationId,
                    ProvinceId = x.Contact.ProvinceId,
                    DistrictId = x.Contact.DistrictId,
                    CustomerLeadId = x.Contact.CustomerLeadId,
                    ImageId = x.Contact.ImageId,
                    Description = x.Contact.Description,
                    EmailOther = x.Contact.EmailOther,
                    DateOfBirth = x.Contact.DateOfBirth,
                    Phone = x.Contact.Phone,
                    PhoneHome = x.Contact.PhoneHome,
                    FAX = x.Contact.FAX,
                    Email = x.Contact.Email,
                    Department = x.Contact.Department,
                    ZIPCode = x.Contact.ZIPCode,
                    SexId = x.Contact.SexId,
                    AppUserId = x.Contact.AppUserId,
                    RefuseReciveEmail = x.Contact.RefuseReciveEmail,
                    RefuseReciveSMS = x.Contact.RefuseReciveSMS,
                    PositionId = x.Contact.PositionId,
                    CreatorId = x.Contact.CreatorId,
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
                FileType = x.FileType == null ? null : new FileType
                {
                    Id = x.FileType.Id,
                    Code = x.FileType.Code,
                    Name = x.FileType.Name,
                },
            }).FirstOrDefaultAsync();

            if (ContactFileGrouping == null)
                return null;

            return ContactFileGrouping;
        }
        public async Task<bool> Create(ContactFileGrouping ContactFileGrouping)
        {
            ContactFileGroupingDAO ContactFileGroupingDAO = new ContactFileGroupingDAO();
            ContactFileGroupingDAO.Id = ContactFileGrouping.Id;
            ContactFileGroupingDAO.Title = ContactFileGrouping.Title;
            ContactFileGroupingDAO.Description = ContactFileGrouping.Description;
            ContactFileGroupingDAO.ContactId = ContactFileGrouping.ContactId;
            ContactFileGroupingDAO.CreatorId = ContactFileGrouping.CreatorId;
            ContactFileGroupingDAO.FileTypeId = ContactFileGrouping.FileTypeId;
            ContactFileGroupingDAO.RowId = ContactFileGrouping.RowId;
            ContactFileGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
            ContactFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.ContactFileGrouping.Add(ContactFileGroupingDAO);
            await DataContext.SaveChangesAsync();
            ContactFileGrouping.Id = ContactFileGroupingDAO.Id;
            await SaveReference(ContactFileGrouping);
            return true;
        }

        public async Task<bool> Update(ContactFileGrouping ContactFileGrouping)
        {
            ContactFileGroupingDAO ContactFileGroupingDAO = DataContext.ContactFileGrouping.Where(x => x.Id == ContactFileGrouping.Id).FirstOrDefault();
            if (ContactFileGroupingDAO == null)
                return false;
            ContactFileGroupingDAO.Id = ContactFileGrouping.Id;
            ContactFileGroupingDAO.Title = ContactFileGrouping.Title;
            ContactFileGroupingDAO.Description = ContactFileGrouping.Description;
            ContactFileGroupingDAO.ContactId = ContactFileGrouping.ContactId;
            ContactFileGroupingDAO.CreatorId = ContactFileGrouping.CreatorId;
            ContactFileGroupingDAO.FileTypeId = ContactFileGrouping.FileTypeId;
            ContactFileGroupingDAO.RowId = ContactFileGrouping.RowId;
            ContactFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(ContactFileGrouping);
            return true;
        }

        public async Task<bool> Delete(ContactFileGrouping ContactFileGrouping)
        {
            await DataContext.ContactFileGrouping.Where(x => x.Id == ContactFileGrouping.Id).UpdateFromQueryAsync(x => new ContactFileGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<ContactFileGrouping> ContactFileGroupings)
        {
            List<ContactFileGroupingDAO> ContactFileGroupingDAOs = new List<ContactFileGroupingDAO>();
            foreach (ContactFileGrouping ContactFileGrouping in ContactFileGroupings)
            {
                ContactFileGroupingDAO ContactFileGroupingDAO = new ContactFileGroupingDAO();
                ContactFileGroupingDAO.Id = ContactFileGrouping.Id;
                ContactFileGroupingDAO.Title = ContactFileGrouping.Title;
                ContactFileGroupingDAO.Description = ContactFileGrouping.Description;
                ContactFileGroupingDAO.ContactId = ContactFileGrouping.ContactId;
                ContactFileGroupingDAO.CreatorId = ContactFileGrouping.CreatorId;
                ContactFileGroupingDAO.FileTypeId = ContactFileGrouping.FileTypeId;
                ContactFileGroupingDAO.RowId = ContactFileGrouping.RowId;
                ContactFileGroupingDAO.CreatedAt = StaticParams.DateTimeNow;
                ContactFileGroupingDAO.UpdatedAt = StaticParams.DateTimeNow;
                ContactFileGroupingDAOs.Add(ContactFileGroupingDAO);
            }
            await DataContext.BulkMergeAsync(ContactFileGroupingDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<ContactFileGrouping> ContactFileGroupings)
        {
            List<long> Ids = ContactFileGroupings.Select(x => x.Id).ToList();
            await DataContext.ContactFileGrouping
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ContactFileGroupingDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(ContactFileGrouping ContactFileGrouping)
        {
        }
        
    }
}
