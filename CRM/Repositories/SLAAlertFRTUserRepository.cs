using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public interface ISLAAlertFRTUserRepository
    {
        Task<int> Count(SLAAlertFRTUserFilter SLAAlertFRTUserFilter);
        Task<List<SLAAlertFRTUser>> List(SLAAlertFRTUserFilter SLAAlertFRTUserFilter);
        Task<SLAAlertFRTUser> Get(long Id);
        Task<bool> Create(SLAAlertFRTUser SLAAlertFRTUser);
        Task<bool> Update(SLAAlertFRTUser SLAAlertFRTUser);
        Task<bool> Delete(SLAAlertFRTUser SLAAlertFRTUser);
        Task<bool> BulkMerge(List<SLAAlertFRTUser> SLAAlertFRTUsers);
        Task<bool> BulkDelete(List<SLAAlertFRTUser> SLAAlertFRTUsers);
    }
    public class SLAAlertFRTUserRepository : ISLAAlertFRTUserRepository
    {
        private DataContext DataContext;
        public SLAAlertFRTUserRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAAlertFRTUserDAO> DynamicFilter(IQueryable<SLAAlertFRTUserDAO> query, SLAAlertFRTUserFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.SLAAlertFRTId != null)
                query = query.Where(q => q.SLAAlertFRTId.HasValue).Where(q => q.SLAAlertFRTId, filter.SLAAlertFRTId);
            if (filter.AppUserId != null)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAAlertFRTUserDAO> OrFilter(IQueryable<SLAAlertFRTUserDAO> query, SLAAlertFRTUserFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAAlertFRTUserDAO> initQuery = query.Where(q => false);
            foreach (SLAAlertFRTUserFilter SLAAlertFRTUserFilter in filter.OrFilter)
            {
                IQueryable<SLAAlertFRTUserDAO> queryable = query;
                if (SLAAlertFRTUserFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAAlertFRTUserFilter.Id);
                if (SLAAlertFRTUserFilter.SLAAlertFRTId != null)
                    queryable = queryable.Where(q => q.SLAAlertFRTId.HasValue).Where(q => q.SLAAlertFRTId, SLAAlertFRTUserFilter.SLAAlertFRTId);
                if (SLAAlertFRTUserFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, SLAAlertFRTUserFilter.AppUserId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAAlertFRTUserDAO> DynamicOrder(IQueryable<SLAAlertFRTUserDAO> query, SLAAlertFRTUserFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertFRTUserOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAAlertFRTUserOrder.SLAAlertFRT:
                            query = query.OrderBy(q => q.SLAAlertFRTId);
                            break;
                        case SLAAlertFRTUserOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertFRTUserOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAAlertFRTUserOrder.SLAAlertFRT:
                            query = query.OrderByDescending(q => q.SLAAlertFRTId);
                            break;
                        case SLAAlertFRTUserOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAAlertFRTUser>> DynamicSelect(IQueryable<SLAAlertFRTUserDAO> query, SLAAlertFRTUserFilter filter)
        {
            List<SLAAlertFRTUser> SLAAlertFRTUsers = await query.Select(q => new SLAAlertFRTUser()
            {
                Id = filter.Selects.Contains(SLAAlertFRTUserSelect.Id) ? q.Id : default(long),
                SLAAlertFRTId = filter.Selects.Contains(SLAAlertFRTUserSelect.SLAAlertFRT) ? q.SLAAlertFRTId : default(long?),
                AppUserId = filter.Selects.Contains(SLAAlertFRTUserSelect.AppUser) ? q.AppUserId : default(long?),
                AppUser = filter.Selects.Contains(SLAAlertFRTUserSelect.AppUser) && q.AppUser != null ? new AppUser
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
                } : null,
                SLAAlertFRT = filter.Selects.Contains(SLAAlertFRTUserSelect.SLAAlertFRT) && q.SLAAlertFRT != null ? new SLAAlertFRT
                {
                    Id = q.SLAAlertFRT.Id,
                    TicketIssueLevelId = q.SLAAlertFRT.TicketIssueLevelId,
                    IsNotification = q.SLAAlertFRT.IsNotification,
                    IsMail = q.SLAAlertFRT.IsMail,
                    IsSMS = q.SLAAlertFRT.IsSMS,
                    Time = q.SLAAlertFRT.Time,
                    TimeUnitId = q.SLAAlertFRT.TimeUnitId,
                    IsAssignedToUser = q.SLAAlertFRT.IsAssignedToUser,
                    IsAssignedToGroup = q.SLAAlertFRT.IsAssignedToGroup,
                    SmsTemplateId = q.SLAAlertFRT.SmsTemplateId,
                    MailTemplateId = q.SLAAlertFRT.MailTemplateId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAAlertFRTUsers;
        }

        public async Task<int> Count(SLAAlertFRTUserFilter filter)
        {
            IQueryable<SLAAlertFRTUserDAO> SLAAlertFRTUsers = DataContext.SLAAlertFRTUser.AsNoTracking();
            SLAAlertFRTUsers = DynamicFilter(SLAAlertFRTUsers, filter);
            return await SLAAlertFRTUsers.CountAsync();
        }

        public async Task<List<SLAAlertFRTUser>> List(SLAAlertFRTUserFilter filter)
        {
            if (filter == null) return new List<SLAAlertFRTUser>();
            IQueryable<SLAAlertFRTUserDAO> SLAAlertFRTUserDAOs = DataContext.SLAAlertFRTUser.AsNoTracking();
            SLAAlertFRTUserDAOs = DynamicFilter(SLAAlertFRTUserDAOs, filter);
            SLAAlertFRTUserDAOs = DynamicOrder(SLAAlertFRTUserDAOs, filter);
            List<SLAAlertFRTUser> SLAAlertFRTUsers = await DynamicSelect(SLAAlertFRTUserDAOs, filter);
            return SLAAlertFRTUsers;
        }

        public async Task<SLAAlertFRTUser> Get(long Id)
        {
            SLAAlertFRTUser SLAAlertFRTUser = await DataContext.SLAAlertFRTUser.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAAlertFRTUser()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAAlertFRTId = x.SLAAlertFRTId,
                AppUserId = x.AppUserId,
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
                },
                SLAAlertFRT = x.SLAAlertFRT == null ? null : new SLAAlertFRT
                {
                    Id = x.SLAAlertFRT.Id,
                    TicketIssueLevelId = x.SLAAlertFRT.TicketIssueLevelId,
                    IsNotification = x.SLAAlertFRT.IsNotification,
                    IsMail = x.SLAAlertFRT.IsMail,
                    IsSMS = x.SLAAlertFRT.IsSMS,
                    Time = x.SLAAlertFRT.Time,
                    TimeUnitId = x.SLAAlertFRT.TimeUnitId,
                    IsAssignedToUser = x.SLAAlertFRT.IsAssignedToUser,
                    IsAssignedToGroup = x.SLAAlertFRT.IsAssignedToGroup,
                    SmsTemplateId = x.SLAAlertFRT.SmsTemplateId,
                    MailTemplateId = x.SLAAlertFRT.MailTemplateId,
                },
            }).FirstOrDefaultAsync();

            if (SLAAlertFRTUser == null)
                return null;

            return SLAAlertFRTUser;
        }
        public async Task<bool> Create(SLAAlertFRTUser SLAAlertFRTUser)
        {
            SLAAlertFRTUserDAO SLAAlertFRTUserDAO = new SLAAlertFRTUserDAO();
            SLAAlertFRTUserDAO.Id = SLAAlertFRTUser.Id;
            SLAAlertFRTUserDAO.SLAAlertFRTId = SLAAlertFRTUser.SLAAlertFRTId;
            SLAAlertFRTUserDAO.AppUserId = SLAAlertFRTUser.AppUserId;
            SLAAlertFRTUserDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAAlertFRTUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAAlertFRTUser.Add(SLAAlertFRTUserDAO);
            await DataContext.SaveChangesAsync();
            SLAAlertFRTUser.Id = SLAAlertFRTUserDAO.Id;
            await SaveReference(SLAAlertFRTUser);
            return true;
        }

        public async Task<bool> Update(SLAAlertFRTUser SLAAlertFRTUser)
        {
            SLAAlertFRTUserDAO SLAAlertFRTUserDAO = DataContext.SLAAlertFRTUser.Where(x => x.Id == SLAAlertFRTUser.Id).FirstOrDefault();
            if (SLAAlertFRTUserDAO == null)
                return false;
            SLAAlertFRTUserDAO.Id = SLAAlertFRTUser.Id;
            SLAAlertFRTUserDAO.SLAAlertFRTId = SLAAlertFRTUser.SLAAlertFRTId;
            SLAAlertFRTUserDAO.AppUserId = SLAAlertFRTUser.AppUserId;
            SLAAlertFRTUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAAlertFRTUser);
            return true;
        }

        public async Task<bool> Delete(SLAAlertFRTUser SLAAlertFRTUser)
        {
            await DataContext.SLAAlertFRTUser.Where(x => x.Id == SLAAlertFRTUser.Id).UpdateFromQueryAsync(x => new SLAAlertFRTUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAAlertFRTUser> SLAAlertFRTUsers)
        {
            List<SLAAlertFRTUserDAO> SLAAlertFRTUserDAOs = new List<SLAAlertFRTUserDAO>();
            foreach (SLAAlertFRTUser SLAAlertFRTUser in SLAAlertFRTUsers)
            {
                SLAAlertFRTUserDAO SLAAlertFRTUserDAO = new SLAAlertFRTUserDAO();
                SLAAlertFRTUserDAO.Id = SLAAlertFRTUser.Id;
                SLAAlertFRTUserDAO.SLAAlertFRTId = SLAAlertFRTUser.SLAAlertFRTId;
                SLAAlertFRTUserDAO.AppUserId = SLAAlertFRTUser.AppUserId;
                SLAAlertFRTUserDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAAlertFRTUserDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAAlertFRTUserDAOs.Add(SLAAlertFRTUserDAO);
            }
            await DataContext.BulkMergeAsync(SLAAlertFRTUserDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAAlertFRTUser> SLAAlertFRTUsers)
        {
            List<long> Ids = SLAAlertFRTUsers.Select(x => x.Id).ToList();
            await DataContext.SLAAlertFRTUser
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAAlertFRTUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAAlertFRTUser SLAAlertFRTUser)
        {
        }
        
    }
}
