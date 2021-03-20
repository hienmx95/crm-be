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
    public interface ICompanyEmailRepository
    {
        Task<int> Count(CompanyEmailFilter CompanyEmailFilter);
        Task<List<CompanyEmail>> List(CompanyEmailFilter CompanyEmailFilter);
        Task<CompanyEmail> Get(long Id);
        Task<bool> Create(CompanyEmail CompanyEmail);
        Task<bool> Update(CompanyEmail CompanyEmail);
        Task<bool> Delete(CompanyEmail CompanyEmail);
        Task<bool> BulkMerge(List<CompanyEmail> CompanyEmails);
        Task<bool> BulkDelete(List<CompanyEmail> CompanyEmails);
    }
    public class CompanyEmailRepository : ICompanyEmailRepository
    {
        private DataContext DataContext;
        public CompanyEmailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CompanyEmailDAO> DynamicFilter(IQueryable<CompanyEmailDAO> query, CompanyEmailFilter filter)
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
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId, filter.CompanyId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.EmailStatusId != null && filter.EmailStatusId.HasValue)
                query = query.Where(q => q.EmailStatusId, filter.EmailStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CompanyEmailDAO> OrFilter(IQueryable<CompanyEmailDAO> query, CompanyEmailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CompanyEmailDAO> initQuery = query.Where(q => false);
            foreach (CompanyEmailFilter CompanyEmailFilter in filter.OrFilter)
            {
                IQueryable<CompanyEmailDAO> queryable = query;
                if (CompanyEmailFilter.Id != null && CompanyEmailFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CompanyEmailFilter.Id);
                if (CompanyEmailFilter.Title != null && CompanyEmailFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CompanyEmailFilter.Title);
                if (CompanyEmailFilter.Content != null && CompanyEmailFilter.Content.HasValue)
                    queryable = queryable.Where(q => q.Content, CompanyEmailFilter.Content);
                if (CompanyEmailFilter.Reciepient != null && CompanyEmailFilter.Reciepient.HasValue)
                    queryable = queryable.Where(q => q.Reciepient, CompanyEmailFilter.Reciepient);
                if (CompanyEmailFilter.CompanyId != null && CompanyEmailFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId, CompanyEmailFilter.CompanyId);
                if (CompanyEmailFilter.CreatorId != null && CompanyEmailFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CompanyEmailFilter.CreatorId);
                if (CompanyEmailFilter.EmailStatusId != null && CompanyEmailFilter.EmailStatusId.HasValue)
                    queryable = queryable.Where(q => q.EmailStatusId, CompanyEmailFilter.EmailStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CompanyEmailDAO> DynamicOrder(IQueryable<CompanyEmailDAO> query, CompanyEmailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CompanyEmailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CompanyEmailOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CompanyEmailOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case CompanyEmailOrder.Reciepient:
                            query = query.OrderBy(q => q.Reciepient);
                            break;
                        case CompanyEmailOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case CompanyEmailOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case CompanyEmailOrder.EmailStatus:
                            query = query.OrderBy(q => q.EmailStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CompanyEmailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CompanyEmailOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CompanyEmailOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case CompanyEmailOrder.Reciepient:
                            query = query.OrderByDescending(q => q.Reciepient);
                            break;
                        case CompanyEmailOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case CompanyEmailOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case CompanyEmailOrder.EmailStatus:
                            query = query.OrderByDescending(q => q.EmailStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CompanyEmail>> DynamicSelect(IQueryable<CompanyEmailDAO> query, CompanyEmailFilter filter)
        {
            List<CompanyEmail> CompanyEmails = await query.Select(q => new CompanyEmail()
            {
                Id = filter.Selects.Contains(CompanyEmailSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(CompanyEmailSelect.Title) ? q.Title : default(string),
                Content = filter.Selects.Contains(CompanyEmailSelect.Content) ? q.Content : default(string),
                Reciepient = filter.Selects.Contains(CompanyEmailSelect.Reciepient) ? q.Reciepient : default(string),
                CompanyId = filter.Selects.Contains(CompanyEmailSelect.Company) ? q.CompanyId : default(long),
                CreatorId = filter.Selects.Contains(CompanyEmailSelect.Creator) ? q.CreatorId : default(long),
                EmailStatusId = filter.Selects.Contains(CompanyEmailSelect.EmailStatus) ? q.EmailStatusId : default(long),
                Company = filter.Selects.Contains(CompanyEmailSelect.Company) && q.Company != null ? new Company
                {
                    Id = q.Company.Id,
                    Name = q.Company.Name,
                    Phone = q.Company.Phone,
                    FAX = q.Company.FAX,
                    PhoneOther = q.Company.PhoneOther,
                    Email = q.Company.Email,
                    EmailOther = q.Company.EmailOther,
                } : null,
                Creator = filter.Selects.Contains(CompanyEmailSelect.Creator) && q.Creator != null ? new AppUser
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
                EmailStatus = filter.Selects.Contains(CompanyEmailSelect.EmailStatus) && q.EmailStatus != null ? new EmailStatus
                {
                    Id = q.EmailStatus.Id,
                    Code = q.EmailStatus.Code,
                    Name = q.EmailStatus.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();

            var Ids = CompanyEmails.Select(x => x.Id).ToList();
            var CompanyEmailCCMappings = await DataContext.CompanyEmailCCMapping.Where(x => Ids.Contains(x.CompanyEmailId)).Select(x => new CompanyEmailCCMapping
            {
                AppUserId = x.AppUserId,
                CompanyEmailId = x.CompanyEmailId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Email = x.AppUser.Email,
                }
            }).ToListAsync();

            foreach (var CompanyEmail in CompanyEmails)
            {
                CompanyEmail.CompanyEmailCCMappings = CompanyEmailCCMappings.Where(x => x.CompanyEmailId == CompanyEmail.Id).ToList();
            }
            return CompanyEmails;
        }

        public async Task<int> Count(CompanyEmailFilter filter)
        {
            IQueryable<CompanyEmailDAO> CompanyEmails = DataContext.CompanyEmail.AsNoTracking();
            CompanyEmails = DynamicFilter(CompanyEmails, filter);
            return await CompanyEmails.CountAsync();
        }

        public async Task<List<CompanyEmail>> List(CompanyEmailFilter filter)
        {
            if (filter == null) return new List<CompanyEmail>();
            IQueryable<CompanyEmailDAO> CompanyEmailDAOs = DataContext.CompanyEmail.AsNoTracking();
            CompanyEmailDAOs = DynamicFilter(CompanyEmailDAOs, filter);
            CompanyEmailDAOs = DynamicOrder(CompanyEmailDAOs, filter);
            List<CompanyEmail> CompanyEmails = await DynamicSelect(CompanyEmailDAOs, filter);
            return CompanyEmails;
        }

        public async Task<CompanyEmail> Get(long Id)
        {
            CompanyEmail CompanyEmail = await DataContext.CompanyEmail.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CompanyEmail()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Reciepient = x.Reciepient,
                CompanyId = x.CompanyId,
                CreatorId = x.CreatorId,
                EmailStatusId = x.EmailStatusId,
                Company = x.Company == null ? null : new Company
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    Phone = x.Company.Phone,
                    FAX = x.Company.FAX,
                    PhoneOther = x.Company.PhoneOther,
                    Email = x.Company.Email,
                    EmailOther = x.Company.EmailOther,
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

            if (CompanyEmail == null)
                return null;

            CompanyEmail.CompanyEmailCCMappings = await DataContext.CompanyEmailCCMapping
                .Where(x => x.CompanyEmailId == Id)
                .Select(x => new CompanyEmailCCMapping
                {
                    CompanyEmailId = x.CompanyEmailId,
                    AppUserId = x.AppUserId,
                    AppUser = x.AppUser == null ? null : new AppUser
                    {
                        Id = x.AppUser.Id,
                        Username = x.AppUser.Username,
                        DisplayName = x.AppUser.DisplayName,
                        Email = x.AppUser.Email,
                    }
                }).ToListAsync();
            return CompanyEmail;
        }
        public async Task<bool> Create(CompanyEmail CompanyEmail)
        {
            CompanyEmailDAO CompanyEmailDAO = new CompanyEmailDAO();
            CompanyEmailDAO.Id = CompanyEmail.Id;
            CompanyEmailDAO.Title = CompanyEmail.Title;
            CompanyEmailDAO.Content = CompanyEmail.Content;
            CompanyEmailDAO.Reciepient = CompanyEmail.Reciepient;
            CompanyEmailDAO.CompanyId = CompanyEmail.CompanyId;
            CompanyEmailDAO.CreatorId = CompanyEmail.CreatorId;
            CompanyEmailDAO.EmailStatusId = CompanyEmail.EmailStatusId;
            CompanyEmailDAO.CreatedAt = StaticParams.DateTimeNow;
            CompanyEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CompanyEmail.Add(CompanyEmailDAO);
            await DataContext.SaveChangesAsync();
            CompanyEmail.Id = CompanyEmailDAO.Id;
            await SaveReference(CompanyEmail);
            return true;
        }

        public async Task<bool> Update(CompanyEmail CompanyEmail)
        {
            CompanyEmailDAO CompanyEmailDAO = DataContext.CompanyEmail.Where(x => x.Id == CompanyEmail.Id).FirstOrDefault();
            if (CompanyEmailDAO == null)
                return false;
            CompanyEmailDAO.Id = CompanyEmail.Id;
            CompanyEmailDAO.Title = CompanyEmail.Title;
            CompanyEmailDAO.Content = CompanyEmail.Content;
            CompanyEmailDAO.Reciepient = CompanyEmail.Reciepient;
            CompanyEmailDAO.CompanyId = CompanyEmail.CompanyId;
            CompanyEmailDAO.CreatorId = CompanyEmail.CreatorId;
            CompanyEmailDAO.EmailStatusId = CompanyEmail.EmailStatusId;
            CompanyEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CompanyEmail);
            return true;
        }

        public async Task<bool> Delete(CompanyEmail CompanyEmail)
        {
            await DataContext.CompanyEmail.Where(x => x.Id == CompanyEmail.Id).UpdateFromQueryAsync(x => new CompanyEmailDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CompanyEmail> CompanyEmails)
        {
            List<CompanyEmailDAO> CompanyEmailDAOs = new List<CompanyEmailDAO>();
            foreach (CompanyEmail CompanyEmail in CompanyEmails)
            {
                CompanyEmailDAO CompanyEmailDAO = new CompanyEmailDAO();
                CompanyEmailDAO.Id = CompanyEmail.Id;
                CompanyEmailDAO.Title = CompanyEmail.Title;
                CompanyEmailDAO.Content = CompanyEmail.Content;
                CompanyEmailDAO.Reciepient = CompanyEmail.Reciepient;
                CompanyEmailDAO.CompanyId = CompanyEmail.CompanyId;
                CompanyEmailDAO.CreatorId = CompanyEmail.CreatorId;
                CompanyEmailDAO.EmailStatusId = CompanyEmail.EmailStatusId;
                CompanyEmailDAO.CreatedAt = StaticParams.DateTimeNow;
                CompanyEmailDAO.UpdatedAt = StaticParams.DateTimeNow;
                CompanyEmailDAOs.Add(CompanyEmailDAO);
            }
            await DataContext.BulkMergeAsync(CompanyEmailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CompanyEmail> CompanyEmails)
        {
            List<long> Ids = CompanyEmails.Select(x => x.Id).ToList();
            await DataContext.CompanyEmail
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CompanyEmailDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CompanyEmail CompanyEmail)
        {
            await DataContext.CompanyEmailCCMapping.Where(x => x.CompanyEmailId == CompanyEmail.Id).DeleteFromQueryAsync();
            if(CompanyEmail.CompanyEmailCCMappings != null)
            {
                List<CompanyEmailCCMappingDAO> CompanyEmailCCMappingDAOs = new List<CompanyEmailCCMappingDAO>();
                foreach (var CompanyEmailCCMapping in CompanyEmail.CompanyEmailCCMappings)
                {
                    CompanyEmailCCMappingDAO CompanyEmailCCMappingDAO = new CompanyEmailCCMappingDAO
                    {
                        AppUserId = CompanyEmailCCMapping.AppUserId,
                        CompanyEmailId = CompanyEmail.Id,
                    };
                    CompanyEmailCCMappingDAOs.Add(CompanyEmailCCMappingDAO);
                }
                await DataContext.BulkMergeAsync(CompanyEmailCCMappingDAOs);
            }
        }
    }
}
