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
    public interface ICompanyActivityRepository
    {
        Task<int> Count(CompanyActivityFilter CompanyActivityFilter);
        Task<List<CompanyActivity>> List(CompanyActivityFilter CompanyActivityFilter);
        Task<List<CompanyActivity>> List(List<long> Ids);
        Task<CompanyActivity> Get(long Id);
        Task<bool> Create(CompanyActivity CompanyActivity);
        Task<bool> Update(CompanyActivity CompanyActivity);
        Task<bool> Delete(CompanyActivity CompanyActivity);
        Task<bool> BulkMerge(List<CompanyActivity> CompanyActivities);
        Task<bool> BulkDelete(List<CompanyActivity> CompanyActivities);
    }
    public class CompanyActivityRepository : ICompanyActivityRepository
    {
        private DataContext DataContext;
        public CompanyActivityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CompanyActivityDAO> DynamicFilter(IQueryable<CompanyActivityDAO> query, CompanyActivityFilter filter)
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
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId, filter.CompanyId);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.ActivityStatusId != null && filter.ActivityStatusId.HasValue)
                query = query.Where(q => q.ActivityStatusId, filter.ActivityStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CompanyActivityDAO> OrFilter(IQueryable<CompanyActivityDAO> query, CompanyActivityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CompanyActivityDAO> initQuery = query.Where(q => false);
            foreach (CompanyActivityFilter CompanyActivityFilter in filter.OrFilter)
            {
                IQueryable<CompanyActivityDAO> queryable = query;
                if (CompanyActivityFilter.Id != null && CompanyActivityFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CompanyActivityFilter.Id);
                if (CompanyActivityFilter.Title != null && CompanyActivityFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CompanyActivityFilter.Title);
                if (CompanyActivityFilter.FromDate != null && CompanyActivityFilter.FromDate.HasValue)
                    queryable = queryable.Where(q => q.FromDate, CompanyActivityFilter.FromDate);
                if (CompanyActivityFilter.ToDate != null && CompanyActivityFilter.ToDate.HasValue)
                    queryable = queryable.Where(q => q.ToDate.HasValue).Where(q => q.ToDate, CompanyActivityFilter.ToDate);
                if (CompanyActivityFilter.ActivityTypeId != null && CompanyActivityFilter.ActivityTypeId.HasValue)
                    queryable = queryable.Where(q => q.ActivityTypeId, CompanyActivityFilter.ActivityTypeId);
                if (CompanyActivityFilter.ActivityPriorityId != null && CompanyActivityFilter.ActivityPriorityId.HasValue)
                    queryable = queryable.Where(q => q.ActivityPriorityId.HasValue).Where(q => q.ActivityPriorityId, CompanyActivityFilter.ActivityPriorityId);
                if (CompanyActivityFilter.Description != null && CompanyActivityFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CompanyActivityFilter.Description);
                if (CompanyActivityFilter.Address != null && CompanyActivityFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, CompanyActivityFilter.Address);
                if (CompanyActivityFilter.CompanyId != null && CompanyActivityFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId, CompanyActivityFilter.CompanyId);
                if (CompanyActivityFilter.AppUserId != null && CompanyActivityFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, CompanyActivityFilter.AppUserId);
                if (CompanyActivityFilter.ActivityStatusId != null && CompanyActivityFilter.ActivityStatusId.HasValue)
                    queryable = queryable.Where(q => q.ActivityStatusId, CompanyActivityFilter.ActivityStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CompanyActivityDAO> DynamicOrder(IQueryable<CompanyActivityDAO> query, CompanyActivityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CompanyActivityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CompanyActivityOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CompanyActivityOrder.FromDate:
                            query = query.OrderBy(q => q.FromDate);
                            break;
                        case CompanyActivityOrder.ToDate:
                            query = query.OrderBy(q => q.ToDate);
                            break;
                        case CompanyActivityOrder.ActivityType:
                            query = query.OrderBy(q => q.ActivityTypeId);
                            break;
                        case CompanyActivityOrder.ActivityPriority:
                            query = query.OrderBy(q => q.ActivityPriorityId);
                            break;
                        case CompanyActivityOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CompanyActivityOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case CompanyActivityOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case CompanyActivityOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case CompanyActivityOrder.ActivityStatus:
                            query = query.OrderBy(q => q.ActivityStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CompanyActivityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CompanyActivityOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CompanyActivityOrder.FromDate:
                            query = query.OrderByDescending(q => q.FromDate);
                            break;
                        case CompanyActivityOrder.ToDate:
                            query = query.OrderByDescending(q => q.ToDate);
                            break;
                        case CompanyActivityOrder.ActivityType:
                            query = query.OrderByDescending(q => q.ActivityTypeId);
                            break;
                        case CompanyActivityOrder.ActivityPriority:
                            query = query.OrderByDescending(q => q.ActivityPriorityId);
                            break;
                        case CompanyActivityOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CompanyActivityOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case CompanyActivityOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case CompanyActivityOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case CompanyActivityOrder.ActivityStatus:
                            query = query.OrderByDescending(q => q.ActivityStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CompanyActivity>> DynamicSelect(IQueryable<CompanyActivityDAO> query, CompanyActivityFilter filter)
        {
            List<CompanyActivity> CompanyActivities = await query.Select(q => new CompanyActivity()
            {
                Id = filter.Selects.Contains(CompanyActivitySelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(CompanyActivitySelect.Title) ? q.Title : default(string),
                FromDate = filter.Selects.Contains(CompanyActivitySelect.FromDate) ? q.FromDate : default(DateTime),
                ToDate = filter.Selects.Contains(CompanyActivitySelect.ToDate) ? q.ToDate : default(DateTime?),
                ActivityTypeId = filter.Selects.Contains(CompanyActivitySelect.ActivityType) ? q.ActivityTypeId : default(long),
                ActivityPriorityId = filter.Selects.Contains(CompanyActivitySelect.ActivityPriority) ? q.ActivityPriorityId : default(long?),
                Description = filter.Selects.Contains(CompanyActivitySelect.Description) ? q.Description : default(string),
                Address = filter.Selects.Contains(CompanyActivitySelect.Address) ? q.Address : default(string),
                CompanyId = filter.Selects.Contains(CompanyActivitySelect.Company) ? q.CompanyId : default(long),
                AppUserId = filter.Selects.Contains(CompanyActivitySelect.AppUser) ? q.AppUserId : default(long),
                ActivityStatusId = filter.Selects.Contains(CompanyActivitySelect.ActivityStatus) ? q.ActivityStatusId : default(long),
                ActivityPriority = filter.Selects.Contains(CompanyActivitySelect.ActivityPriority) && q.ActivityPriority != null ? new ActivityPriority
                {
                    Id = q.ActivityPriority.Id,
                    Code = q.ActivityPriority.Code,
                    Name = q.ActivityPriority.Name,
                } : null,
                ActivityStatus = filter.Selects.Contains(CompanyActivitySelect.ActivityStatus) && q.ActivityStatus != null ? new ActivityStatus
                {
                    Id = q.ActivityStatus.Id,
                    Code = q.ActivityStatus.Code,
                    Name = q.ActivityStatus.Name,
                } : null,
                ActivityType = filter.Selects.Contains(CompanyActivitySelect.ActivityType) && q.ActivityType != null ? new ActivityType
                {
                    Id = q.ActivityType.Id,
                    Code = q.ActivityType.Code,
                    Name = q.ActivityType.Name,
                } : null,
                AppUser = filter.Selects.Contains(CompanyActivitySelect.AppUser) && q.AppUser != null ? new AppUser
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
                Company = filter.Selects.Contains(CompanyActivitySelect.Company) && q.Company != null ? new Company
                {
                    Id = q.Company.Id,
                    Name = q.Company.Name,
                    Phone = q.Company.Phone,
                    FAX = q.Company.FAX,
                    PhoneOther = q.Company.PhoneOther,
                    Email = q.Company.Email,
                    EmailOther = q.Company.EmailOther,
                } : null,
            }).ToListAsync();
            return CompanyActivities;
        }

        public async Task<int> Count(CompanyActivityFilter filter)
        {
            IQueryable<CompanyActivityDAO> CompanyActivities = DataContext.CompanyActivity.AsNoTracking();
            CompanyActivities = DynamicFilter(CompanyActivities, filter);
            return await CompanyActivities.CountAsync();
        }

        public async Task<List<CompanyActivity>> List(CompanyActivityFilter filter)
        {
            if (filter == null) return new List<CompanyActivity>();
            IQueryable<CompanyActivityDAO> CompanyActivityDAOs = DataContext.CompanyActivity.AsNoTracking();
            CompanyActivityDAOs = DynamicFilter(CompanyActivityDAOs, filter);
            CompanyActivityDAOs = DynamicOrder(CompanyActivityDAOs, filter);
            List<CompanyActivity> CompanyActivities = await DynamicSelect(CompanyActivityDAOs, filter);
            return CompanyActivities;
        }

        public async Task<List<CompanyActivity>> List(List<long> Ids)
        {
            List<CompanyActivity> CompanyActivities = await DataContext.CompanyActivity.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CompanyActivity()
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
                CompanyId = x.CompanyId,
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
            }).ToListAsync();
            

            return CompanyActivities;
        }

        public async Task<CompanyActivity> Get(long Id)
        {
            CompanyActivity CompanyActivity = await DataContext.CompanyActivity.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CompanyActivity()
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
                CompanyId = x.CompanyId,
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
            }).FirstOrDefaultAsync();

            if (CompanyActivity == null)
                return null;

            return CompanyActivity;
        }
        public async Task<bool> Create(CompanyActivity CompanyActivity)
        {
            CompanyActivityDAO CompanyActivityDAO = new CompanyActivityDAO();
            CompanyActivityDAO.Id = CompanyActivity.Id;
            CompanyActivityDAO.Title = CompanyActivity.Title;
            CompanyActivityDAO.FromDate = CompanyActivity.FromDate;
            CompanyActivityDAO.ToDate = CompanyActivity.ToDate;
            CompanyActivityDAO.ActivityTypeId = CompanyActivity.ActivityTypeId;
            CompanyActivityDAO.ActivityPriorityId = CompanyActivity.ActivityPriorityId;
            CompanyActivityDAO.Description = CompanyActivity.Description;
            CompanyActivityDAO.Address = CompanyActivity.Address;
            CompanyActivityDAO.CompanyId = CompanyActivity.CompanyId;
            CompanyActivityDAO.AppUserId = CompanyActivity.AppUserId;
            CompanyActivityDAO.ActivityStatusId = CompanyActivity.ActivityStatusId;
            CompanyActivityDAO.CreatedAt = StaticParams.DateTimeNow;
            CompanyActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CompanyActivity.Add(CompanyActivityDAO);
            await DataContext.SaveChangesAsync();
            CompanyActivity.Id = CompanyActivityDAO.Id;
            await SaveReference(CompanyActivity);
            return true;
        }

        public async Task<bool> Update(CompanyActivity CompanyActivity)
        {
            CompanyActivityDAO CompanyActivityDAO = DataContext.CompanyActivity.Where(x => x.Id == CompanyActivity.Id).FirstOrDefault();
            if (CompanyActivityDAO == null)
                return false;
            CompanyActivityDAO.Id = CompanyActivity.Id;
            CompanyActivityDAO.Title = CompanyActivity.Title;
            CompanyActivityDAO.FromDate = CompanyActivity.FromDate;
            CompanyActivityDAO.ToDate = CompanyActivity.ToDate;
            CompanyActivityDAO.ActivityTypeId = CompanyActivity.ActivityTypeId;
            CompanyActivityDAO.ActivityPriorityId = CompanyActivity.ActivityPriorityId;
            CompanyActivityDAO.Description = CompanyActivity.Description;
            CompanyActivityDAO.Address = CompanyActivity.Address;
            CompanyActivityDAO.CompanyId = CompanyActivity.CompanyId;
            CompanyActivityDAO.AppUserId = CompanyActivity.AppUserId;
            CompanyActivityDAO.ActivityStatusId = CompanyActivity.ActivityStatusId;
            CompanyActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CompanyActivity);
            return true;
        }

        public async Task<bool> Delete(CompanyActivity CompanyActivity)
        {
            await DataContext.CompanyActivity.Where(x => x.Id == CompanyActivity.Id).UpdateFromQueryAsync(x => new CompanyActivityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CompanyActivity> CompanyActivities)
        {
            List<CompanyActivityDAO> CompanyActivityDAOs = new List<CompanyActivityDAO>();
            foreach (CompanyActivity CompanyActivity in CompanyActivities)
            {
                CompanyActivityDAO CompanyActivityDAO = new CompanyActivityDAO();
                CompanyActivityDAO.Id = CompanyActivity.Id;
                CompanyActivityDAO.Title = CompanyActivity.Title;
                CompanyActivityDAO.FromDate = CompanyActivity.FromDate;
                CompanyActivityDAO.ToDate = CompanyActivity.ToDate;
                CompanyActivityDAO.ActivityTypeId = CompanyActivity.ActivityTypeId;
                CompanyActivityDAO.ActivityPriorityId = CompanyActivity.ActivityPriorityId;
                CompanyActivityDAO.Description = CompanyActivity.Description;
                CompanyActivityDAO.Address = CompanyActivity.Address;
                CompanyActivityDAO.CompanyId = CompanyActivity.CompanyId;
                CompanyActivityDAO.AppUserId = CompanyActivity.AppUserId;
                CompanyActivityDAO.ActivityStatusId = CompanyActivity.ActivityStatusId;
                CompanyActivityDAO.CreatedAt = StaticParams.DateTimeNow;
                CompanyActivityDAO.UpdatedAt = StaticParams.DateTimeNow;
                CompanyActivityDAOs.Add(CompanyActivityDAO);
            }
            await DataContext.BulkMergeAsync(CompanyActivityDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CompanyActivity> CompanyActivities)
        {
            List<long> Ids = CompanyActivities.Select(x => x.Id).ToList();
            await DataContext.CompanyActivity
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CompanyActivityDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CompanyActivity CompanyActivity)
        {
        }
        
    }
}
