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
    public interface IContactEmailRepository
    {
        Task<int> Count(ContactEmailFilter ContactEmailFilter);
        Task<List<ContactEmail>> List(ContactEmailFilter ContactEmailFilter);
        Task<ContactEmail> Get(long Id);
        Task<bool> Create(ContactEmail ContactEmail);
        Task<bool> Update(ContactEmail ContactEmail);
        Task<bool> Delete(ContactEmail ContactEmail);
        Task<bool> BulkMerge(List<ContactEmail> ContactEmails);
        Task<bool> BulkDelete(List<ContactEmail> ContactEmails);
    }
    public class ContactEmailRepository : IContactEmailRepository
    {
        private DataContext DataContext;
        public ContactEmailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContactEmailDAO> DynamicFilter(IQueryable<ContactEmailDAO> query, ContactEmailFilter filter)
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
            if (filter.ContactId != null && filter.ContactId.HasValue)
                query = query.Where(q => q.ContactId, filter.ContactId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.EmailStatusId != null && filter.EmailStatusId.HasValue)
                query = query.Where(q => q.EmailStatusId, filter.EmailStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ContactEmailDAO> OrFilter(IQueryable<ContactEmailDAO> query, ContactEmailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContactEmailDAO> initQuery = query.Where(q => false);
            foreach (ContactEmailFilter ContactEmailFilter in filter.OrFilter)
            {
                IQueryable<ContactEmailDAO> queryable = query;
                if (ContactEmailFilter.Id != null && ContactEmailFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContactEmailFilter.Id);
                if (ContactEmailFilter.Title != null && ContactEmailFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, ContactEmailFilter.Title);
                if (ContactEmailFilter.Content != null && ContactEmailFilter.Content.HasValue)
                    queryable = queryable.Where(q => q.Content, ContactEmailFilter.Content);
                if (ContactEmailFilter.Reciepient != null && ContactEmailFilter.Reciepient.HasValue)
                    queryable = queryable.Where(q => q.Reciepient, ContactEmailFilter.Reciepient);
                if (ContactEmailFilter.ContactId != null && ContactEmailFilter.ContactId.HasValue)
                    queryable = queryable.Where(q => q.ContactId, ContactEmailFilter.ContactId);
                if (ContactEmailFilter.CreatorId != null && ContactEmailFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, ContactEmailFilter.CreatorId);
                if (ContactEmailFilter.EmailStatusId != null && ContactEmailFilter.EmailStatusId.HasValue)
                    queryable = queryable.Where(q => q.EmailStatusId, ContactEmailFilter.EmailStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ContactEmailDAO> DynamicOrder(IQueryable<ContactEmailDAO> query, ContactEmailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContactEmailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContactEmailOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case ContactEmailOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case ContactEmailOrder.Reciepient:
                            query = query.OrderBy(q => q.Reciepient);
                            break;
                        case ContactEmailOrder.Contact:
                            query = query.OrderBy(q => q.ContactId);
                            break;
                        case ContactEmailOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case ContactEmailOrder.EmailStatus:
                            query = query.OrderBy(q => q.EmailStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContactEmailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContactEmailOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case ContactEmailOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case ContactEmailOrder.Reciepient:
                            query = query.OrderByDescending(q => q.Reciepient);
                            break;
                        case ContactEmailOrder.Contact:
                            query = query.OrderByDescending(q => q.ContactId);
                            break;
                        case ContactEmailOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case ContactEmailOrder.EmailStatus:
                            query = query.OrderByDescending(q => q.EmailStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ContactEmail>> DynamicSelect(IQueryable<ContactEmailDAO> query, ContactEmailFilter filter)
        {
            List<ContactEmail> ContactEmails = await query.Select(q => new ContactEmail()
            {
                Id = filter.Selects.Contains(ContactEmailSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(ContactEmailSelect.Title) ? q.Title : default(string),
                Content = filter.Selects.Contains(ContactEmailSelect.Content) ? q.Content : default(string),
                Reciepient = filter.Selects.Contains(ContactEmailSelect.Reciepient) ? q.Reciepient : default(string),
                ContactId = filter.Selects.Contains(ContactEmailSelect.Contact) ? q.ContactId : default(long),
                CreatorId = filter.Selects.Contains(ContactEmailSelect.Creator) ? q.CreatorId : default(long),
                EmailStatusId = filter.Selects.Contains(ContactEmailSelect.EmailStatus) ? q.EmailStatusId : default(long),
                Contact = filter.Selects.Contains(ContactEmailSelect.Contact) && q.Contact != null ? new Contact
                {
                    Id = q.Contact.Id,
                    Name = q.Contact.Name,
                    ProfessionId = q.Contact.ProfessionId,
                    CompanyId = q.Contact.CompanyId,
                } : null,
                Creator = filter.Selects.Contains(ContactEmailSelect.Creator) && q.Creator != null ? new AppUser
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
                EmailStatus = filter.Selects.Contains(ContactEmailSelect.EmailStatus) && q.EmailStatus != null ? new EmailStatus
                {
                    Id = q.EmailStatus.Id,
                    Code = q.EmailStatus.Code,
                    Name = q.EmailStatus.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();

            var Ids = ContactEmails.Select(x => x.Id).ToList();
            var ContactEmailCCMappings = await DataContext.ContactEmailCCMapping.Where(x => Ids.Contains(x.ContactEmailId)).Select(x => new ContactEmailCCMapping
            {
                AppUserId = x.AppUserId,
                ContactEmailId = x.ContactEmailId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Email = x.AppUser.Email,
                }
            }).ToListAsync();

            foreach (var ContactEmail in ContactEmails)
            {
                ContactEmail.ContactEmailCCMappings = ContactEmailCCMappings.Where(x => x.ContactEmailId == ContactEmail.Id).ToList();
            }
            return ContactEmails;
        }

        public async Task<int> Count(ContactEmailFilter filter)
        {
            IQueryable<ContactEmailDAO> ContactEmails = DataContext.ContactEmail.AsNoTracking();
            ContactEmails = DynamicFilter(ContactEmails, filter);
            return await ContactEmails.CountAsync();
        }

        public async Task<List<ContactEmail>> List(ContactEmailFilter filter)
        {
            if (filter == null) return new List<ContactEmail>();
            IQueryable<ContactEmailDAO> ContactEmailDAOs = DataContext.ContactEmail.AsNoTracking();
            ContactEmailDAOs = DynamicFilter(ContactEmailDAOs, filter);
            ContactEmailDAOs = DynamicOrder(ContactEmailDAOs, filter);
            List<ContactEmail> ContactEmails = await DynamicSelect(ContactEmailDAOs, filter);
            return ContactEmails;
        }

        public async Task<ContactEmail> Get(long Id)
        {
            ContactEmail ContactEmail = await DataContext.ContactEmail.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new ContactEmail()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Reciepient = x.Reciepient,
                ContactId = x.ContactId,
                CreatorId = x.CreatorId,
                EmailStatusId = x.EmailStatusId,
                Contact = x.Contact == null ? null : new Contact
                {
                    Id = x.Contact.Id,
                    Name = x.Contact.Name,
                    ProfessionId = x.Contact.ProfessionId,
                    CompanyId = x.Contact.CompanyId,
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
                EmailStatus = x.EmailStatus == null ? null : new EmailStatus
                {
                    Id = x.EmailStatus.Id,
                    Code = x.EmailStatus.Code,
                    Name = x.EmailStatus.Name,
                },
            }).FirstOrDefaultAsync();

            if (ContactEmail == null)
                return null;

            ContactEmail.ContactEmailCCMappings = await DataContext.ContactEmailCCMapping
              .Where(x => x.ContactEmailId == Id)
              .Select(x => new ContactEmailCCMapping
              {
                  ContactEmailId = x.ContactEmailId,
                  AppUserId = x.AppUserId,
                  AppUser = x.AppUser == null ? null : new AppUser
                  {
                      Id = x.AppUser.Id,
                      Username = x.AppUser.Username,
                      DisplayName = x.AppUser.DisplayName,
                      Email = x.AppUser.Email,
                  }
              }).ToListAsync();
            return ContactEmail;
        }
        public async Task<bool> Create(ContactEmail ContactEmail)
        {
            ContactEmailDAO ContactEmailDAO = new ContactEmailDAO();
            ContactEmailDAO.Id = ContactEmail.Id;
            ContactEmailDAO.Title = ContactEmail.Title;
            ContactEmailDAO.Content = ContactEmail.Content;
            ContactEmailDAO.Reciepient = ContactEmail.Reciepient;
            ContactEmailDAO.ContactId = ContactEmail.ContactId;
            ContactEmailDAO.CreatorId = ContactEmail.CreatorId;
            ContactEmailDAO.EmailStatusId = ContactEmail.EmailStatusId;
            ContactEmailDAO.CreatedAt = StaticParams.DateTimeNow;
            ContactEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.ContactEmail.Add(ContactEmailDAO);
            await DataContext.SaveChangesAsync();
            ContactEmail.Id = ContactEmailDAO.Id;
            await SaveReference(ContactEmail);
            return true;
        }

        public async Task<bool> Update(ContactEmail ContactEmail)
        {
            ContactEmailDAO ContactEmailDAO = DataContext.ContactEmail.Where(x => x.Id == ContactEmail.Id).FirstOrDefault();
            if (ContactEmailDAO == null)
                return false;
            ContactEmailDAO.Id = ContactEmail.Id;
            ContactEmailDAO.Title = ContactEmail.Title;
            ContactEmailDAO.Content = ContactEmail.Content;
            ContactEmailDAO.Reciepient = ContactEmail.Reciepient;
            ContactEmailDAO.ContactId = ContactEmail.ContactId;
            ContactEmailDAO.CreatorId = ContactEmail.CreatorId;
            ContactEmailDAO.EmailStatusId = ContactEmail.EmailStatusId;
            ContactEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(ContactEmail);
            return true;
        }

        public async Task<bool> Delete(ContactEmail ContactEmail)
        {
            await DataContext.ContactEmail.Where(x => x.Id == ContactEmail.Id).UpdateFromQueryAsync(x => new ContactEmailDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<ContactEmail> ContactEmails)
        {
            List<ContactEmailDAO> ContactEmailDAOs = new List<ContactEmailDAO>();
            foreach (ContactEmail ContactEmail in ContactEmails)
            {
                ContactEmailDAO ContactEmailDAO = new ContactEmailDAO();
                ContactEmailDAO.Id = ContactEmail.Id;
                ContactEmailDAO.Title = ContactEmail.Title;
                ContactEmailDAO.Content = ContactEmail.Content;
                ContactEmailDAO.Reciepient = ContactEmail.Reciepient;
                ContactEmailDAO.ContactId = ContactEmail.ContactId;
                ContactEmailDAO.CreatorId = ContactEmail.CreatorId;
                ContactEmailDAO.EmailStatusId = ContactEmail.EmailStatusId;
                ContactEmailDAO.CreatedAt = StaticParams.DateTimeNow;
                ContactEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
                ContactEmailDAOs.Add(ContactEmailDAO);
            }
            await DataContext.BulkMergeAsync(ContactEmailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<ContactEmail> ContactEmails)
        {
            List<long> Ids = ContactEmails.Select(x => x.Id).ToList();
            await DataContext.ContactEmail
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ContactEmailDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(ContactEmail ContactEmail)
        {
            await DataContext.ContactEmailCCMapping.Where(x => x.ContactEmailId == ContactEmail.Id).DeleteFromQueryAsync();
            if (ContactEmail.ContactEmailCCMappings != null)
            {
                List<ContactEmailCCMappingDAO> ContactEmailCCMappingDAOs = new List<ContactEmailCCMappingDAO>();
                foreach (var ContactEmailCCMapping in ContactEmail.ContactEmailCCMappings)
                {
                    ContactEmailCCMappingDAO ContactEmailCCMappingDAO = new ContactEmailCCMappingDAO
                    {
                        AppUserId = ContactEmailCCMapping.AppUserId,
                        ContactEmailId = ContactEmail.Id,
                    };
                    ContactEmailCCMappingDAOs.Add(ContactEmailCCMappingDAO);
                }
                await DataContext.BulkMergeAsync(ContactEmailCCMappingDAOs);
            }
        }
    }
}
