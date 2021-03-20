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
    public interface ICustomerLeadEmailRepository
    {
        Task<int> Count(CustomerLeadEmailFilter CustomerLeadEmailFilter);
        Task<List<CustomerLeadEmail>> List(CustomerLeadEmailFilter CustomerLeadEmailFilter);
        Task<CustomerLeadEmail> Get(long Id);
        Task<bool> Create(CustomerLeadEmail CustomerLeadEmail);
        Task<bool> Update(CustomerLeadEmail CustomerLeadEmail);
        Task<bool> Delete(CustomerLeadEmail CustomerLeadEmail);
        Task<bool> BulkMerge(List<CustomerLeadEmail> CustomerLeadEmails);
        Task<bool> BulkDelete(List<CustomerLeadEmail> CustomerLeadEmails);
    }
    public class CustomerLeadEmailRepository : ICustomerLeadEmailRepository
    {
        private DataContext DataContext;
        public CustomerLeadEmailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerLeadEmailDAO> DynamicFilter(IQueryable<CustomerLeadEmailDAO> query, CustomerLeadEmailFilter filter)
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
            if (filter.CustomerLeadId != null && filter.CustomerLeadId.HasValue)
                query = query.Where(q => q.CustomerLeadId, filter.CustomerLeadId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.EmailStatusId != null && filter.EmailStatusId.HasValue)
                query = query.Where(q => q.EmailStatusId, filter.EmailStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerLeadEmailDAO> OrFilter(IQueryable<CustomerLeadEmailDAO> query, CustomerLeadEmailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerLeadEmailDAO> initQuery = query.Where(q => false);
            foreach (CustomerLeadEmailFilter CustomerLeadEmailFilter in filter.OrFilter)
            {
                IQueryable<CustomerLeadEmailDAO> queryable = query;
                if (CustomerLeadEmailFilter.Id != null && CustomerLeadEmailFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerLeadEmailFilter.Id);
                if (CustomerLeadEmailFilter.Title != null && CustomerLeadEmailFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CustomerLeadEmailFilter.Title);
                if (CustomerLeadEmailFilter.Content != null && CustomerLeadEmailFilter.Content.HasValue)
                    queryable = queryable.Where(q => q.Content, CustomerLeadEmailFilter.Content);
                if (CustomerLeadEmailFilter.Reciepient != null && CustomerLeadEmailFilter.Reciepient.HasValue)
                    queryable = queryable.Where(q => q.Reciepient, CustomerLeadEmailFilter.Reciepient);
                if (CustomerLeadEmailFilter.CustomerLeadId != null && CustomerLeadEmailFilter.CustomerLeadId.HasValue)
                    queryable = queryable.Where(q => q.CustomerLeadId, CustomerLeadEmailFilter.CustomerLeadId);
                if (CustomerLeadEmailFilter.CreatorId != null && CustomerLeadEmailFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CustomerLeadEmailFilter.CreatorId);
                if (CustomerLeadEmailFilter.EmailStatusId != null && CustomerLeadEmailFilter.EmailStatusId.HasValue)
                    queryable = queryable.Where(q => q.EmailStatusId, CustomerLeadEmailFilter.EmailStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerLeadEmailDAO> DynamicOrder(IQueryable<CustomerLeadEmailDAO> query, CustomerLeadEmailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadEmailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerLeadEmailOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CustomerLeadEmailOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case CustomerLeadEmailOrder.Reciepient:
                            query = query.OrderBy(q => q.Reciepient);
                            break;
                        case CustomerLeadEmailOrder.CustomerLead:
                            query = query.OrderBy(q => q.CustomerLeadId);
                            break;
                        case CustomerLeadEmailOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CustomerLeadEmailOrder.EmailStatus:
                            query = query.OrderBy(q => q.EmailStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerLeadEmailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerLeadEmailOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CustomerLeadEmailOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case CustomerLeadEmailOrder.Reciepient:
                            query = query.OrderByDescending(q => q.Reciepient);
                            break;
                        case CustomerLeadEmailOrder.CustomerLead:
                            query = query.OrderByDescending(q => q.CustomerLeadId);
                            break;
                        case CustomerLeadEmailOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CustomerLeadEmailOrder.EmailStatus:
                            query = query.OrderByDescending(q => q.EmailStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerLeadEmail>> DynamicSelect(IQueryable<CustomerLeadEmailDAO> query, CustomerLeadEmailFilter filter)
        {
            List<CustomerLeadEmail> CustomerLeadEmails = await query.Select(q => new CustomerLeadEmail()
            {
                Id = filter.Selects.Contains(CustomerLeadEmailSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(CustomerLeadEmailSelect.Title) ? q.Title : default(string),
                Content = filter.Selects.Contains(CustomerLeadEmailSelect.Content) ? q.Content : default(string),
                Reciepient = filter.Selects.Contains(CustomerLeadEmailSelect.Reciepient) ? q.Reciepient : default(string),
                CustomerLeadId = filter.Selects.Contains(CustomerLeadEmailSelect.CustomerLead) ? q.CustomerLeadId : default(long),
                CreatorId = filter.Selects.Contains(CustomerLeadEmailSelect.Creator) ? q.CreatorId : default(long),
                EmailStatusId = filter.Selects.Contains(CustomerLeadEmailSelect.EmailStatus) ? q.EmailStatusId : default(long),
                CustomerLead = filter.Selects.Contains(CustomerLeadEmailSelect.CustomerLead) && q.CustomerLead != null ? new CustomerLead
                {
                    Id = q.CustomerLead.Id,
                    CompanyName = q.CustomerLead.CompanyName,
                    Name = q.CustomerLead.Name,
                    Phone = q.CustomerLead.Phone,
                    Email = q.CustomerLead.Email,
                    Website = q.CustomerLead.Website,
                    NationId = q.CustomerLead.NationId,
                    ProvinceId = q.CustomerLead.ProvinceId,
                    DistrictId = q.CustomerLead.DistrictId,
                    Address = q.CustomerLead.Address,
                    RefuseReciveEmail = q.CustomerLead.RefuseReciveEmail,
                    RefuseReciveSMS = q.CustomerLead.RefuseReciveSMS,
                    ProfessionId = q.CustomerLead.ProfessionId,
                    CurrencyId = q.CustomerLead.CurrencyId,
                    CreatorId = q.CustomerLead.CreatorId,
                    Description = q.CustomerLead.Description,
                    CompanyId = q.CustomerLead.CompanyId,
                } : null,
                Creator = filter.Selects.Contains(CustomerLeadEmailSelect.Creator) && q.Creator != null ? new AppUser
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
                EmailStatus = filter.Selects.Contains(CustomerLeadEmailSelect.EmailStatus) && q.EmailStatus != null ? new EmailStatus
                {
                    Id = q.EmailStatus.Id,
                    Code = q.EmailStatus.Code,
                    Name = q.EmailStatus.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();

            var Ids = CustomerLeadEmails.Select(x => x.Id).ToList();
            var CustomerLeadEmailCCMappings = await DataContext.CustomerLeadEmailCCMapping.Where(x => Ids.Contains(x.CustomerLeadEmailId)).Select(x => new CustomerLeadEmailCCMapping
            {
                AppUserId = x.AppUserId,
                CustomerLeadEmailId = x.CustomerLeadEmailId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Email = x.AppUser.Email,
                }
            }).ToListAsync();

            foreach (var CustomerLeadEmail in CustomerLeadEmails)
            {
                CustomerLeadEmail.CustomerLeadEmailCCMappings = CustomerLeadEmailCCMappings.Where(x => x.CustomerLeadEmailId == CustomerLeadEmail.Id).ToList();
            }
            return CustomerLeadEmails;
        }

        public async Task<int> Count(CustomerLeadEmailFilter filter)
        {
            IQueryable<CustomerLeadEmailDAO> CustomerLeadEmails = DataContext.CustomerLeadEmail.AsNoTracking();
            CustomerLeadEmails = DynamicFilter(CustomerLeadEmails, filter);
            return await CustomerLeadEmails.CountAsync();
        }

        public async Task<List<CustomerLeadEmail>> List(CustomerLeadEmailFilter filter)
        {
            if (filter == null) return new List<CustomerLeadEmail>();
            IQueryable<CustomerLeadEmailDAO> CustomerLeadEmailDAOs = DataContext.CustomerLeadEmail.AsNoTracking();
            CustomerLeadEmailDAOs = DynamicFilter(CustomerLeadEmailDAOs, filter);
            CustomerLeadEmailDAOs = DynamicOrder(CustomerLeadEmailDAOs, filter);
            List<CustomerLeadEmail> CustomerLeadEmails = await DynamicSelect(CustomerLeadEmailDAOs, filter);
            return CustomerLeadEmails;
        }

        public async Task<CustomerLeadEmail> Get(long Id)
        {
            CustomerLeadEmail CustomerLeadEmail = await DataContext.CustomerLeadEmail.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CustomerLeadEmail()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Reciepient = x.Reciepient,
                CustomerLeadId = x.CustomerLeadId,
                CreatorId = x.CreatorId,
                EmailStatusId = x.EmailStatusId,
                CustomerLead = x.CustomerLead == null ? null : new CustomerLead
                {
                    Id = x.CustomerLead.Id,
                    CompanyName = x.CustomerLead.CompanyName,
                    Name = x.CustomerLead.Name,
                    Phone = x.CustomerLead.Phone,
                    Email = x.CustomerLead.Email,
                    Website = x.CustomerLead.Website,
                    NationId = x.CustomerLead.NationId,
                    ProvinceId = x.CustomerLead.ProvinceId,
                    DistrictId = x.CustomerLead.DistrictId,
                    Address = x.CustomerLead.Address,
                    RefuseReciveEmail = x.CustomerLead.RefuseReciveEmail,
                    RefuseReciveSMS = x.CustomerLead.RefuseReciveSMS,
                    ProfessionId = x.CustomerLead.ProfessionId,
                    CurrencyId = x.CustomerLead.CurrencyId,
                    CreatorId = x.CustomerLead.CreatorId,
                    Description = x.CustomerLead.Description,
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

            if (CustomerLeadEmail == null)
                return null;

            CustomerLeadEmail.CustomerLeadEmailCCMappings = await DataContext.CustomerLeadEmailCCMapping
                .Where(x => x.CustomerLeadEmailId == Id)
                .Select(x => new CustomerLeadEmailCCMapping
                {
                    CustomerLeadEmailId = x.CustomerLeadEmailId,
                    AppUserId = x.AppUserId,
                    AppUser = x.AppUser == null ? null : new AppUser
                    {
                        Id = x.AppUser.Id,
                        Username = x.AppUser.Username,
                        DisplayName = x.AppUser.DisplayName,
                        Email = x.AppUser.Email,
                    }
                }).ToListAsync();
            return CustomerLeadEmail;
        }
        public async Task<bool> Create(CustomerLeadEmail CustomerLeadEmail)
        {
            CustomerLeadEmailDAO CustomerLeadEmailDAO = new CustomerLeadEmailDAO();
            CustomerLeadEmailDAO.Id = CustomerLeadEmail.Id;
            CustomerLeadEmailDAO.Title = CustomerLeadEmail.Title;
            CustomerLeadEmailDAO.Content = CustomerLeadEmail.Content;
            CustomerLeadEmailDAO.Reciepient = CustomerLeadEmail.Reciepient;
            CustomerLeadEmailDAO.CustomerLeadId = CustomerLeadEmail.CustomerLeadId;
            CustomerLeadEmailDAO.CreatorId = CustomerLeadEmail.CreatorId;
            CustomerLeadEmailDAO.EmailStatusId = CustomerLeadEmail.EmailStatusId;
            CustomerLeadEmailDAO.CreatedAt = StaticParams.DateTimeNow;
            CustomerLeadEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CustomerLeadEmail.Add(CustomerLeadEmailDAO);
            await DataContext.SaveChangesAsync();
            CustomerLeadEmail.Id = CustomerLeadEmailDAO.Id;
            await SaveReference(CustomerLeadEmail);
            return true;
        }

        public async Task<bool> Update(CustomerLeadEmail CustomerLeadEmail)
        {
            CustomerLeadEmailDAO CustomerLeadEmailDAO = DataContext.CustomerLeadEmail.Where(x => x.Id == CustomerLeadEmail.Id).FirstOrDefault();
            if (CustomerLeadEmailDAO == null)
                return false;
            CustomerLeadEmailDAO.Id = CustomerLeadEmail.Id;
            CustomerLeadEmailDAO.Title = CustomerLeadEmail.Title;
            CustomerLeadEmailDAO.Content = CustomerLeadEmail.Content;
            CustomerLeadEmailDAO.Reciepient = CustomerLeadEmail.Reciepient;
            CustomerLeadEmailDAO.CustomerLeadId = CustomerLeadEmail.CustomerLeadId;
            CustomerLeadEmailDAO.CreatorId = CustomerLeadEmail.CreatorId;
            CustomerLeadEmailDAO.EmailStatusId = CustomerLeadEmail.EmailStatusId;
            CustomerLeadEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerLeadEmail);
            return true;
        }

        public async Task<bool> Delete(CustomerLeadEmail CustomerLeadEmail)
        {
            await DataContext.CustomerLeadEmail.Where(x => x.Id == CustomerLeadEmail.Id).UpdateFromQueryAsync(x => new CustomerLeadEmailDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerLeadEmail> CustomerLeadEmails)
        {
            List<CustomerLeadEmailDAO> CustomerLeadEmailDAOs = new List<CustomerLeadEmailDAO>();
            foreach (CustomerLeadEmail CustomerLeadEmail in CustomerLeadEmails)
            {
                CustomerLeadEmailDAO CustomerLeadEmailDAO = new CustomerLeadEmailDAO();
                CustomerLeadEmailDAO.Id = CustomerLeadEmail.Id;
                CustomerLeadEmailDAO.Title = CustomerLeadEmail.Title;
                CustomerLeadEmailDAO.Content = CustomerLeadEmail.Content;
                CustomerLeadEmailDAO.Reciepient = CustomerLeadEmail.Reciepient;
                CustomerLeadEmailDAO.CustomerLeadId = CustomerLeadEmail.CustomerLeadId;
                CustomerLeadEmailDAO.CreatorId = CustomerLeadEmail.CreatorId;
                CustomerLeadEmailDAO.EmailStatusId = CustomerLeadEmail.EmailStatusId;
                CustomerLeadEmailDAO.CreatedAt = StaticParams.DateTimeNow;
                CustomerLeadEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
                CustomerLeadEmailDAOs.Add(CustomerLeadEmailDAO);
            }
            await DataContext.BulkMergeAsync(CustomerLeadEmailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerLeadEmail> CustomerLeadEmails)
        {
            List<long> Ids = CustomerLeadEmails.Select(x => x.Id).ToList();
            await DataContext.CustomerLeadEmail
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CustomerLeadEmailDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CustomerLeadEmail CustomerLeadEmail)
        {
            await DataContext.CustomerLeadEmailCCMapping.Where(x => x.CustomerLeadEmailId == CustomerLeadEmail.Id).DeleteFromQueryAsync();
            if(CustomerLeadEmail.CustomerLeadEmailCCMappings != null)
            {
                List<CustomerLeadEmailCCMappingDAO> CustomerLeadEmailCCMappingDAOs = new List<CustomerLeadEmailCCMappingDAO>();
                foreach (var CustomerLeadEmailCCMapping in CustomerLeadEmail.CustomerLeadEmailCCMappings)
                {
                    CustomerLeadEmailCCMappingDAO CustomerLeadEmailCCMappingDAO = new CustomerLeadEmailCCMappingDAO
                    {
                        AppUserId = CustomerLeadEmailCCMapping.AppUserId,
                        CustomerLeadEmailId = CustomerLeadEmail.Id,
                    };
                    CustomerLeadEmailCCMappingDAOs.Add(CustomerLeadEmailCCMappingDAO);
                }
                await DataContext.BulkMergeAsync(CustomerLeadEmailCCMappingDAOs);
            }
        }
    }
}
