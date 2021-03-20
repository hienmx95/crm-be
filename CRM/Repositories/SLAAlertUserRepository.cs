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
    public interface ISLAAlertUserRepository
    {
        Task<int> Count(SLAAlertUserFilter SLAAlertUserFilter);
        Task<List<SLAAlertUser>> List(SLAAlertUserFilter SLAAlertUserFilter);
        Task<SLAAlertUser> Get(long Id);
        Task<bool> Create(SLAAlertUser SLAAlertUser);
        Task<bool> Update(SLAAlertUser SLAAlertUser);
        Task<bool> Delete(SLAAlertUser SLAAlertUser);
        Task<bool> BulkMerge(List<SLAAlertUser> SLAAlertUsers);
        Task<bool> BulkDelete(List<SLAAlertUser> SLAAlertUsers);
    }
    public class SLAAlertUserRepository : ISLAAlertUserRepository
    {
        private DataContext DataContext;
        public SLAAlertUserRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAAlertUserDAO> DynamicFilter(IQueryable<SLAAlertUserDAO> query, SLAAlertUserFilter filter)
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
            if (filter.SLAAlertId != null)
                query = query.Where(q => q.SLAAlertId.HasValue).Where(q => q.SLAAlertId, filter.SLAAlertId);
            if (filter.AppUserId != null)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAAlertUserDAO> OrFilter(IQueryable<SLAAlertUserDAO> query, SLAAlertUserFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAAlertUserDAO> initQuery = query.Where(q => false);
            foreach (SLAAlertUserFilter SLAAlertUserFilter in filter.OrFilter)
            {
                IQueryable<SLAAlertUserDAO> queryable = query;
                if (SLAAlertUserFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAAlertUserFilter.Id);
                if (SLAAlertUserFilter.SLAAlertId != null)
                    queryable = queryable.Where(q => q.SLAAlertId.HasValue).Where(q => q.SLAAlertId, SLAAlertUserFilter.SLAAlertId);
                if (SLAAlertUserFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, SLAAlertUserFilter.AppUserId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAAlertUserDAO> DynamicOrder(IQueryable<SLAAlertUserDAO> query, SLAAlertUserFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertUserOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAAlertUserOrder.SLAAlert:
                            query = query.OrderBy(q => q.SLAAlertId);
                            break;
                        case SLAAlertUserOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertUserOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAAlertUserOrder.SLAAlert:
                            query = query.OrderByDescending(q => q.SLAAlertId);
                            break;
                        case SLAAlertUserOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAAlertUser>> DynamicSelect(IQueryable<SLAAlertUserDAO> query, SLAAlertUserFilter filter)
        {
            List<SLAAlertUser> SLAAlertUsers = await query.Select(q => new SLAAlertUser()
            {
                Id = filter.Selects.Contains(SLAAlertUserSelect.Id) ? q.Id : default(long),
                SLAAlertId = filter.Selects.Contains(SLAAlertUserSelect.SLAAlert) ? q.SLAAlertId : default(long?),
                AppUserId = filter.Selects.Contains(SLAAlertUserSelect.AppUser) ? q.AppUserId : default(long?),
                AppUser = filter.Selects.Contains(SLAAlertUserSelect.AppUser) && q.AppUser != null ? new AppUser
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
                SLAAlert = filter.Selects.Contains(SLAAlertUserSelect.SLAAlert) && q.SLAAlert != null ? new SLAAlert
                {
                    Id = q.SLAAlert.Id,
                    TicketIssueLevelId = q.SLAAlert.TicketIssueLevelId,
                    IsNotification = q.SLAAlert.IsNotification,
                    IsMail = q.SLAAlert.IsMail,
                    IsSMS = q.SLAAlert.IsSMS,
                    Time = q.SLAAlert.Time,
                    TimeUnitId = q.SLAAlert.TimeUnitId,
                    IsAssignedToUser = q.SLAAlert.IsAssignedToUser,
                    IsAssignedToGroup = q.SLAAlert.IsAssignedToGroup,
                    SmsTemplateId = q.SLAAlert.SmsTemplateId,
                    MailTemplateId = q.SLAAlert.MailTemplateId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAAlertUsers;
        }

        public async Task<int> Count(SLAAlertUserFilter filter)
        {
            IQueryable<SLAAlertUserDAO> SLAAlertUsers = DataContext.SLAAlertUser.AsNoTracking();
            SLAAlertUsers = DynamicFilter(SLAAlertUsers, filter);
            return await SLAAlertUsers.CountAsync();
        }

        public async Task<List<SLAAlertUser>> List(SLAAlertUserFilter filter)
        {
            if (filter == null) return new List<SLAAlertUser>();
            IQueryable<SLAAlertUserDAO> SLAAlertUserDAOs = DataContext.SLAAlertUser.AsNoTracking();
            SLAAlertUserDAOs = DynamicFilter(SLAAlertUserDAOs, filter);
            SLAAlertUserDAOs = DynamicOrder(SLAAlertUserDAOs, filter);
            List<SLAAlertUser> SLAAlertUsers = await DynamicSelect(SLAAlertUserDAOs, filter);
            return SLAAlertUsers;
        }

        public async Task<SLAAlertUser> Get(long Id)
        {
            SLAAlertUser SLAAlertUser = await DataContext.SLAAlertUser.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAAlertUser()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAAlertId = x.SLAAlertId,
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
                SLAAlert = x.SLAAlert == null ? null : new SLAAlert
                {
                    Id = x.SLAAlert.Id,
                    TicketIssueLevelId = x.SLAAlert.TicketIssueLevelId,
                    IsNotification = x.SLAAlert.IsNotification,
                    IsMail = x.SLAAlert.IsMail,
                    IsSMS = x.SLAAlert.IsSMS,
                    Time = x.SLAAlert.Time,
                    TimeUnitId = x.SLAAlert.TimeUnitId,
                    IsAssignedToUser = x.SLAAlert.IsAssignedToUser,
                    IsAssignedToGroup = x.SLAAlert.IsAssignedToGroup,
                    SmsTemplateId = x.SLAAlert.SmsTemplateId,
                    MailTemplateId = x.SLAAlert.MailTemplateId,
                },
            }).FirstOrDefaultAsync();

            if (SLAAlertUser == null)
                return null;

            return SLAAlertUser;
        }
        public async Task<bool> Create(SLAAlertUser SLAAlertUser)
        {
            SLAAlertUserDAO SLAAlertUserDAO = new SLAAlertUserDAO();
            SLAAlertUserDAO.Id = SLAAlertUser.Id;
            SLAAlertUserDAO.SLAAlertId = SLAAlertUser.SLAAlertId;
            SLAAlertUserDAO.AppUserId = SLAAlertUser.AppUserId;
            SLAAlertUserDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAAlertUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAAlertUser.Add(SLAAlertUserDAO);
            await DataContext.SaveChangesAsync();
            SLAAlertUser.Id = SLAAlertUserDAO.Id;
            await SaveReference(SLAAlertUser);
            return true;
        }

        public async Task<bool> Update(SLAAlertUser SLAAlertUser)
        {
            SLAAlertUserDAO SLAAlertUserDAO = DataContext.SLAAlertUser.Where(x => x.Id == SLAAlertUser.Id).FirstOrDefault();
            if (SLAAlertUserDAO == null)
                return false;
            SLAAlertUserDAO.Id = SLAAlertUser.Id;
            SLAAlertUserDAO.SLAAlertId = SLAAlertUser.SLAAlertId;
            SLAAlertUserDAO.AppUserId = SLAAlertUser.AppUserId;
            SLAAlertUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAAlertUser);
            return true;
        }

        public async Task<bool> Delete(SLAAlertUser SLAAlertUser)
        {
            await DataContext.SLAAlertUser.Where(x => x.Id == SLAAlertUser.Id).UpdateFromQueryAsync(x => new SLAAlertUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAAlertUser> SLAAlertUsers)
        {
            List<SLAAlertUserDAO> SLAAlertUserDAOs = new List<SLAAlertUserDAO>();
            foreach (SLAAlertUser SLAAlertUser in SLAAlertUsers)
            {
                SLAAlertUserDAO SLAAlertUserDAO = new SLAAlertUserDAO();
                SLAAlertUserDAO.Id = SLAAlertUser.Id;
                SLAAlertUserDAO.SLAAlertId = SLAAlertUser.SLAAlertId;
                SLAAlertUserDAO.AppUserId = SLAAlertUser.AppUserId;
                SLAAlertUserDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAAlertUserDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAAlertUserDAOs.Add(SLAAlertUserDAO);
            }
            await DataContext.BulkMergeAsync(SLAAlertUserDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAAlertUser> SLAAlertUsers)
        {
            List<long> Ids = SLAAlertUsers.Select(x => x.Id).ToList();
            await DataContext.SLAAlertUser
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAAlertUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAAlertUser SLAAlertUser)
        {
        }
        
    }
}
