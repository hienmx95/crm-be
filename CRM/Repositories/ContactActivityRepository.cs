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
    public interface IContactActivityRepository
    {
        Task<int> Count(ContactActivityFilter ContactActivityFilter);
        Task<List<ContactActivity>> List(ContactActivityFilter ContactActivityFilter);
        Task<List<ContactActivity>> List(List<long> Ids);
        Task<ContactActivity> Get(long Id);
        Task<bool> Create(ContactActivity ContactActivity);
        Task<bool> Update(ContactActivity ContactActivity);
        Task<bool> Delete(ContactActivity ContactActivity);
        Task<bool> BulkMerge(List<ContactActivity> ContactActivities);
        Task<bool> BulkDelete(List<ContactActivity> ContactActivities);
    }
    public class ContactActivityRepository : IContactActivityRepository
    {
        private DataContext DataContext;
        public ContactActivityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContactActivityDAO> DynamicFilter(IQueryable<ContactActivityDAO> query, ContactActivityFilter filter)
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
            if (filter.FromDate != null && filter.FromDate.HasValue)
                query = query.Where(q => q.FromDate, filter.FromDate);
            if (filter.ToDate != null && filter.ToDate.HasValue)
                query = query.Where(q => q.ToDate == null).Union(query.Where(q => q.ToDate.HasValue).Where(q => q.ToDate, filter.ToDate));
            if (filter.ActivityTypeId != null && filter.ActivityTypeId.HasValue)
                query = query.Where(q => q.ActivityTypeId, filter.ActivityTypeId);
            if (filter.ActivityPriorityId != null && filter.ActivityPriorityId.HasValue)
                query = query.Where(q => q.ActivityPriorityId.HasValue).Where(q => q.ActivityPriorityId, filter.ActivityPriorityId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.Address != null && filter.Address.HasValue)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.ContactId != null && filter.ContactId.HasValue)
                query = query.Where(q => q.ContactId, filter.ContactId);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.ActivityStatusId != null && filter.ActivityStatusId.HasValue)
                query = query.Where(q => q.ActivityStatusId, filter.ActivityStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ContactActivityDAO> OrFilter(IQueryable<ContactActivityDAO> query, ContactActivityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContactActivityDAO> initQuery = query.Where(q => false);
            foreach (ContactActivityFilter ContactActivityFilter in filter.OrFilter)
            {
                IQueryable<ContactActivityDAO> queryable = query;
                if (ContactActivityFilter.Id != null && ContactActivityFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContactActivityFilter.Id);
                if (ContactActivityFilter.Title != null && ContactActivityFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, ContactActivityFilter.Title);
                if (ContactActivityFilter.FromDate != null && ContactActivityFilter.FromDate.HasValue)
                    queryable = queryable.Where(q => q.FromDate, ContactActivityFilter.FromDate);
                if (ContactActivityFilter.ToDate != null && ContactActivityFilter.ToDate.HasValue)
                    queryable = queryable.Where(q => q.ToDate.HasValue).Where(q => q.ToDate, ContactActivityFilter.ToDate);
                if (ContactActivityFilter.ActivityTypeId != null && ContactActivityFilter.ActivityTypeId.HasValue)
                    queryable = queryable.Where(q => q.ActivityTypeId, ContactActivityFilter.ActivityTypeId);
                if (ContactActivityFilter.ActivityPriorityId != null && ContactActivityFilter.ActivityPriorityId.HasValue)
                    queryable = queryable.Where(q => q.ActivityPriorityId.HasValue).Where(q => q.ActivityPriorityId, ContactActivityFilter.ActivityPriorityId);
                if (ContactActivityFilter.Description != null && ContactActivityFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, ContactActivityFilter.Description);
                if (ContactActivityFilter.Address != null && ContactActivityFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, ContactActivityFilter.Address);
                if (ContactActivityFilter.ContactId != null && ContactActivityFilter.ContactId.HasValue)
                    queryable = queryable.Where(q => q.ContactId, ContactActivityFilter.ContactId);
                if (ContactActivityFilter.AppUserId != null && ContactActivityFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, ContactActivityFilter.AppUserId);
                if (ContactActivityFilter.ActivityStatusId != null && ContactActivityFilter.ActivityStatusId.HasValue)
                    queryable = queryable.Where(q => q.ActivityStatusId, ContactActivityFilter.ActivityStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ContactActivityDAO> DynamicOrder(IQueryable<ContactActivityDAO> query, ContactActivityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContactActivityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContactActivityOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case ContactActivityOrder.FromDate:
                            query = query.OrderBy(q => q.FromDate);
                            break;
                        case ContactActivityOrder.ToDate:
                            query = query.OrderBy(q => q.ToDate);
                            break;
                        case ContactActivityOrder.ActivityType:
                            query = query.OrderBy(q => q.ActivityTypeId);
                            break;
                        case ContactActivityOrder.ActivityPriority:
                            query = query.OrderBy(q => q.ActivityPriorityId);
                            break;
                        case ContactActivityOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case ContactActivityOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case ContactActivityOrder.Contact:
                            query = query.OrderBy(q => q.ContactId);
                            break;
                        case ContactActivityOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case ContactActivityOrder.ActivityStatus:
                            query = query.OrderBy(q => q.ActivityStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContactActivityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContactActivityOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case ContactActivityOrder.FromDate:
                            query = query.OrderByDescending(q => q.FromDate);
                            break;
                        case ContactActivityOrder.ToDate:
                            query = query.OrderByDescending(q => q.ToDate);
                            break;
                        case ContactActivityOrder.ActivityType:
                            query = query.OrderByDescending(q => q.ActivityTypeId);
                            break;
                        case ContactActivityOrder.ActivityPriority:
                            query = query.OrderByDescending(q => q.ActivityPriorityId);
                            break;
                        case ContactActivityOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case ContactActivityOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case ContactActivityOrder.Contact:
                            query = query.OrderByDescending(q => q.ContactId);
                            break;
                        case ContactActivityOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case ContactActivityOrder.ActivityStatus:
                            query = query.OrderByDescending(q => q.ActivityStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ContactActivity>> DynamicSelect(IQueryable<ContactActivityDAO> query, ContactActivityFilter filter)
        {
            List<ContactActivity> ContactActivities = await query.Select(q => new ContactActivity()
            {
                Id = filter.Selects.Contains(ContactActivitySelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(ContactActivitySelect.Title) ? q.Title : default(string),
                FromDate = filter.Selects.Contains(ContactActivitySelect.FromDate) ? q.FromDate : default(DateTime),
                ToDate = filter.Selects.Contains(ContactActivitySelect.ToDate) ? q.ToDate : default(DateTime?),
                ActivityTypeId = filter.Selects.Contains(ContactActivitySelect.ActivityType) ? q.ActivityTypeId : default(long),
                ActivityPriorityId = filter.Selects.Contains(ContactActivitySelect.ActivityPriority) ? q.ActivityPriorityId : default(long?),
                Description = filter.Selects.Contains(ContactActivitySelect.Description) ? q.Description : default(string),
                Address = filter.Selects.Contains(ContactActivitySelect.Address) ? q.Address : default(string),
                ContactId = filter.Selects.Contains(ContactActivitySelect.Contact) ? q.ContactId : default(long),
                AppUserId = filter.Selects.Contains(ContactActivitySelect.AppUser) ? q.AppUserId : default(long),
                ActivityStatusId = filter.Selects.Contains(ContactActivitySelect.ActivityStatus) ? q.ActivityStatusId : default(long),
                ActivityPriority = filter.Selects.Contains(ContactActivitySelect.ActivityPriority) && q.ActivityPriority != null ? new ActivityPriority
                {
                    Id = q.ActivityPriority.Id,
                    Code = q.ActivityPriority.Code,
                    Name = q.ActivityPriority.Name,
                } : null,
                ActivityStatus = filter.Selects.Contains(ContactActivitySelect.ActivityStatus) && q.ActivityStatus != null ? new ActivityStatus
                {
                    Id = q.ActivityStatus.Id,
                    Code = q.ActivityStatus.Code,
                    Name = q.ActivityStatus.Name,
                } : null,
                ActivityType = filter.Selects.Contains(ContactActivitySelect.ActivityType) && q.ActivityType != null ? new ActivityType
                {
                    Id = q.ActivityType.Id,
                    Code = q.ActivityType.Code,
                    Name = q.ActivityType.Name,
                } : null,
                AppUser = filter.Selects.Contains(ContactActivitySelect.AppUser) && q.AppUser != null ? new AppUser
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
                Contact = filter.Selects.Contains(ContactActivitySelect.Contact) && q.Contact != null ? new Contact
                {
                    Id = q.Contact.Id,
                    Name = q.Contact.Name,
                    ProfessionId = q.Contact.ProfessionId,
                    CompanyId = q.Contact.CompanyId,
                } : null,
            }).ToListAsync();
            return ContactActivities;
        }

        public async Task<int> Count(ContactActivityFilter filter)
        {
            IQueryable<ContactActivityDAO> ContactActivities = DataContext.ContactActivity.AsNoTracking();
            ContactActivities = DynamicFilter(ContactActivities, filter);
            return await ContactActivities.CountAsync();
        }

        public async Task<List<ContactActivity>> List(ContactActivityFilter filter)
        {
            if (filter == null) return new List<ContactActivity>();
            IQueryable<ContactActivityDAO> ContactActivityDAOs = DataContext.ContactActivity.AsNoTracking();
            ContactActivityDAOs = DynamicFilter(ContactActivityDAOs, filter);
            ContactActivityDAOs = DynamicOrder(ContactActivityDAOs, filter);
            List<ContactActivity> ContactActivities = await DynamicSelect(ContactActivityDAOs, filter);
            return ContactActivities;
        }

        public async Task<List<ContactActivity>> List(List<long> Ids)
        {
            List<ContactActivity> ContactActivities = await DataContext.ContactActivity.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ContactActivity()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                ActivityTypeId = x.ActivityTypeId,
                ActivityPriorityId = x.ActivityPriorityId,
                Description = x.Description,
                Address = x.Address,
                ContactId = x.ContactId,
                AppUserId = x.AppUserId,
                ActivityStatusId = x.ActivityStatusId,
                ActivityPriority = x.ActivityPriority == null ? null : new ActivityPriority
                {
                    Id = x.ActivityPriority.Id,
                    Code = x.ActivityPriority.Code,
                    Name = x.ActivityPriority.Name,
                },
                ActivityStatus = x.ActivityStatus == null ? null : new ActivityStatus
                {
                    Id = x.ActivityStatus.Id,
                    Code = x.ActivityStatus.Code,
                    Name = x.ActivityStatus.Name,
                },
                ActivityType = x.ActivityType == null ? null : new ActivityType
                {
                    Id = x.ActivityType.Id,
                    Code = x.ActivityType.Code,
                    Name = x.ActivityType.Name,
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
                Contact = x.Contact == null ? null : new Contact
                {
                    Id = x.Contact.Id,
                    Name = x.Contact.Name,
                    ProfessionId = x.Contact.ProfessionId,
                    CompanyId = x.Contact.CompanyId,
                },
            }).ToListAsync();
            

            return ContactActivities;
        }

        public async Task<ContactActivity> Get(long Id)
        {
            ContactActivity ContactActivity = await DataContext.ContactActivity.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new ContactActivity()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                ActivityTypeId = x.ActivityTypeId,
                ActivityPriorityId = x.ActivityPriorityId,
                Description = x.Description,
                Address = x.Address,
                ContactId = x.ContactId,
                AppUserId = x.AppUserId,
                ActivityStatusId = x.ActivityStatusId,
                ActivityPriority = x.ActivityPriority == null ? null : new ActivityPriority
                {
                    Id = x.ActivityPriority.Id,
                    Code = x.ActivityPriority.Code,
                    Name = x.ActivityPriority.Name,
                },
                ActivityStatus = x.ActivityStatus == null ? null : new ActivityStatus
                {
                    Id = x.ActivityStatus.Id,
                    Code = x.ActivityStatus.Code,
                    Name = x.ActivityStatus.Name,
                },
                ActivityType = x.ActivityType == null ? null : new ActivityType
                {
                    Id = x.ActivityType.Id,
                    Code = x.ActivityType.Code,
                    Name = x.ActivityType.Name,
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
                Contact = x.Contact == null ? null : new Contact
                {
                    Id = x.Contact.Id,
                    Name = x.Contact.Name,
                    ProfessionId = x.Contact.ProfessionId,
                    CompanyId = x.Contact.CompanyId,
                },
            }).FirstOrDefaultAsync();

            if (ContactActivity == null)
                return null;

            return ContactActivity;
        }
        public async Task<bool> Create(ContactActivity ContactActivity)
        {
            ContactActivityDAO ContactActivityDAO = new ContactActivityDAO();
            ContactActivityDAO.Id = ContactActivity.Id;
            ContactActivityDAO.Title = ContactActivity.Title;
            ContactActivityDAO.FromDate = ContactActivity.FromDate;
            ContactActivityDAO.ToDate = ContactActivity.ToDate;
            ContactActivityDAO.ActivityTypeId = ContactActivity.ActivityTypeId;
            ContactActivityDAO.ActivityPriorityId = ContactActivity.ActivityPriorityId;
            ContactActivityDAO.Description = ContactActivity.Description;
            ContactActivityDAO.Address = ContactActivity.Address;
            ContactActivityDAO.ContactId = ContactActivity.ContactId;
            ContactActivityDAO.AppUserId = ContactActivity.AppUserId;
            ContactActivityDAO.ActivityStatusId = ContactActivity.ActivityStatusId;
            ContactActivityDAO.CreatedAt = StaticParams.DateTimeNow;
            ContactActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.ContactActivity.Add(ContactActivityDAO);
            await DataContext.SaveChangesAsync();
            ContactActivity.Id = ContactActivityDAO.Id;
            await SaveReference(ContactActivity);
            return true;
        }

        public async Task<bool> Update(ContactActivity ContactActivity)
        {
            ContactActivityDAO ContactActivityDAO = DataContext.ContactActivity.Where(x => x.Id == ContactActivity.Id).FirstOrDefault();
            if (ContactActivityDAO == null)
                return false;
            ContactActivityDAO.Id = ContactActivity.Id;
            ContactActivityDAO.Title = ContactActivity.Title;
            ContactActivityDAO.FromDate = ContactActivity.FromDate;
            ContactActivityDAO.ToDate = ContactActivity.ToDate;
            ContactActivityDAO.ActivityTypeId = ContactActivity.ActivityTypeId;
            ContactActivityDAO.ActivityPriorityId = ContactActivity.ActivityPriorityId;
            ContactActivityDAO.Description = ContactActivity.Description;
            ContactActivityDAO.Address = ContactActivity.Address;
            ContactActivityDAO.ContactId = ContactActivity.ContactId;
            ContactActivityDAO.AppUserId = ContactActivity.AppUserId;
            ContactActivityDAO.ActivityStatusId = ContactActivity.ActivityStatusId;
            ContactActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(ContactActivity);
            return true;
        }

        public async Task<bool> Delete(ContactActivity ContactActivity)
        {
            await DataContext.ContactActivity.Where(x => x.Id == ContactActivity.Id).UpdateFromQueryAsync(x => new ContactActivityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<ContactActivity> ContactActivities)
        {
            List<ContactActivityDAO> ContactActivityDAOs = new List<ContactActivityDAO>();
            foreach (ContactActivity ContactActivity in ContactActivities)
            {
                ContactActivityDAO ContactActivityDAO = new ContactActivityDAO();
                ContactActivityDAO.Id = ContactActivity.Id;
                ContactActivityDAO.Title = ContactActivity.Title;
                ContactActivityDAO.FromDate = ContactActivity.FromDate;
                ContactActivityDAO.ToDate = ContactActivity.ToDate;
                ContactActivityDAO.ActivityTypeId = ContactActivity.ActivityTypeId;
                ContactActivityDAO.ActivityPriorityId = ContactActivity.ActivityPriorityId;
                ContactActivityDAO.Description = ContactActivity.Description;
                ContactActivityDAO.Address = ContactActivity.Address;
                ContactActivityDAO.ContactId = ContactActivity.ContactId;
                ContactActivityDAO.AppUserId = ContactActivity.AppUserId;
                ContactActivityDAO.ActivityStatusId = ContactActivity.ActivityStatusId;
                ContactActivityDAO.CreatedAt = StaticParams.DateTimeNow;
                ContactActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
                ContactActivityDAOs.Add(ContactActivityDAO);
            }
            await DataContext.BulkMergeAsync(ContactActivityDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<ContactActivity> ContactActivities)
        {
            List<long> Ids = ContactActivities.Select(x => x.Id).ToList();
            await DataContext.ContactActivity
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ContactActivityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(ContactActivity ContactActivity)
        {
        }
        
    }
}
