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
    public interface ISLAEscalationFRTUserRepository
    {
        Task<int> Count(SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter);
        Task<List<SLAEscalationFRTUser>> List(SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter);
        Task<SLAEscalationFRTUser> Get(long Id);
        Task<bool> Create(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<bool> Update(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<bool> Delete(SLAEscalationFRTUser SLAEscalationFRTUser);
        Task<bool> BulkMerge(List<SLAEscalationFRTUser> SLAEscalationFRTUsers);
        Task<bool> BulkDelete(List<SLAEscalationFRTUser> SLAEscalationFRTUsers);
    }
    public class SLAEscalationFRTUserRepository : ISLAEscalationFRTUserRepository
    {
        private DataContext DataContext;
        public SLAEscalationFRTUserRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAEscalationFRTUserDAO> DynamicFilter(IQueryable<SLAEscalationFRTUserDAO> query, SLAEscalationFRTUserFilter filter)
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
            if (filter.SLAEscalationFRTId != null)
                query = query.Where(q => q.SLAEscalationFRTId.HasValue).Where(q => q.SLAEscalationFRTId, filter.SLAEscalationFRTId);
            if (filter.AppUserId != null)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAEscalationFRTUserDAO> OrFilter(IQueryable<SLAEscalationFRTUserDAO> query, SLAEscalationFRTUserFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAEscalationFRTUserDAO> initQuery = query.Where(q => false);
            foreach (SLAEscalationFRTUserFilter SLAEscalationFRTUserFilter in filter.OrFilter)
            {
                IQueryable<SLAEscalationFRTUserDAO> queryable = query;
                if (SLAEscalationFRTUserFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAEscalationFRTUserFilter.Id);
                if (SLAEscalationFRTUserFilter.SLAEscalationFRTId != null)
                    queryable = queryable.Where(q => q.SLAEscalationFRTId.HasValue).Where(q => q.SLAEscalationFRTId, SLAEscalationFRTUserFilter.SLAEscalationFRTId);
                if (SLAEscalationFRTUserFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, SLAEscalationFRTUserFilter.AppUserId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAEscalationFRTUserDAO> DynamicOrder(IQueryable<SLAEscalationFRTUserDAO> query, SLAEscalationFRTUserFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationFRTUserOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAEscalationFRTUserOrder.SLAEscalationFRT:
                            query = query.OrderBy(q => q.SLAEscalationFRTId);
                            break;
                        case SLAEscalationFRTUserOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationFRTUserOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAEscalationFRTUserOrder.SLAEscalationFRT:
                            query = query.OrderByDescending(q => q.SLAEscalationFRTId);
                            break;
                        case SLAEscalationFRTUserOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAEscalationFRTUser>> DynamicSelect(IQueryable<SLAEscalationFRTUserDAO> query, SLAEscalationFRTUserFilter filter)
        {
            List<SLAEscalationFRTUser> SLAEscalationFRTUsers = await query.Select(q => new SLAEscalationFRTUser()
            {
                Id = filter.Selects.Contains(SLAEscalationFRTUserSelect.Id) ? q.Id : default(long),
                SLAEscalationFRTId = filter.Selects.Contains(SLAEscalationFRTUserSelect.SLAEscalationFRT) ? q.SLAEscalationFRTId : default(long?),
                AppUserId = filter.Selects.Contains(SLAEscalationFRTUserSelect.AppUser) ? q.AppUserId : default(long?),
                AppUser = filter.Selects.Contains(SLAEscalationFRTUserSelect.AppUser) && q.AppUser != null ? new AppUser
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
                SLAEscalationFRT = filter.Selects.Contains(SLAEscalationFRTUserSelect.SLAEscalationFRT) && q.SLAEscalationFRT != null ? new SLAEscalationFRT
                {
                    Id = q.SLAEscalationFRT.Id,
                    TicketIssueLevelId = q.SLAEscalationFRT.TicketIssueLevelId,
                    IsNotification = q.SLAEscalationFRT.IsNotification,
                    IsMail = q.SLAEscalationFRT.IsMail,
                    IsSMS = q.SLAEscalationFRT.IsSMS,
                    Time = q.SLAEscalationFRT.Time,
                    TimeUnitId = q.SLAEscalationFRT.TimeUnitId,
                    IsAssignedToUser = q.SLAEscalationFRT.IsAssignedToUser,
                    IsAssignedToGroup = q.SLAEscalationFRT.IsAssignedToGroup,
                    SmsTemplateId = q.SLAEscalationFRT.SmsTemplateId,
                    MailTemplateId = q.SLAEscalationFRT.MailTemplateId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAEscalationFRTUsers;
        }

        public async Task<int> Count(SLAEscalationFRTUserFilter filter)
        {
            IQueryable<SLAEscalationFRTUserDAO> SLAEscalationFRTUsers = DataContext.SLAEscalationFRTUser.AsNoTracking();
            SLAEscalationFRTUsers = DynamicFilter(SLAEscalationFRTUsers, filter);
            return await SLAEscalationFRTUsers.CountAsync();
        }

        public async Task<List<SLAEscalationFRTUser>> List(SLAEscalationFRTUserFilter filter)
        {
            if (filter == null) return new List<SLAEscalationFRTUser>();
            IQueryable<SLAEscalationFRTUserDAO> SLAEscalationFRTUserDAOs = DataContext.SLAEscalationFRTUser.AsNoTracking();
            SLAEscalationFRTUserDAOs = DynamicFilter(SLAEscalationFRTUserDAOs, filter);
            SLAEscalationFRTUserDAOs = DynamicOrder(SLAEscalationFRTUserDAOs, filter);
            List<SLAEscalationFRTUser> SLAEscalationFRTUsers = await DynamicSelect(SLAEscalationFRTUserDAOs, filter);
            return SLAEscalationFRTUsers;
        }

        public async Task<SLAEscalationFRTUser> Get(long Id)
        {
            SLAEscalationFRTUser SLAEscalationFRTUser = await DataContext.SLAEscalationFRTUser.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAEscalationFRTUser()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAEscalationFRTId = x.SLAEscalationFRTId,
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
                SLAEscalationFRT = x.SLAEscalationFRT == null ? null : new SLAEscalationFRT
                {
                    Id = x.SLAEscalationFRT.Id,
                    TicketIssueLevelId = x.SLAEscalationFRT.TicketIssueLevelId,
                    IsNotification = x.SLAEscalationFRT.IsNotification,
                    IsMail = x.SLAEscalationFRT.IsMail,
                    IsSMS = x.SLAEscalationFRT.IsSMS,
                    Time = x.SLAEscalationFRT.Time,
                    TimeUnitId = x.SLAEscalationFRT.TimeUnitId,
                    IsAssignedToUser = x.SLAEscalationFRT.IsAssignedToUser,
                    IsAssignedToGroup = x.SLAEscalationFRT.IsAssignedToGroup,
                    SmsTemplateId = x.SLAEscalationFRT.SmsTemplateId,
                    MailTemplateId = x.SLAEscalationFRT.MailTemplateId,
                },
            }).FirstOrDefaultAsync();

            if (SLAEscalationFRTUser == null)
                return null;

            return SLAEscalationFRTUser;
        }
        public async Task<bool> Create(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            SLAEscalationFRTUserDAO SLAEscalationFRTUserDAO = new SLAEscalationFRTUserDAO();
            SLAEscalationFRTUserDAO.Id = SLAEscalationFRTUser.Id;
            SLAEscalationFRTUserDAO.SLAEscalationFRTId = SLAEscalationFRTUser.SLAEscalationFRTId;
            SLAEscalationFRTUserDAO.AppUserId = SLAEscalationFRTUser.AppUserId;
            SLAEscalationFRTUserDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAEscalationFRTUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAEscalationFRTUser.Add(SLAEscalationFRTUserDAO);
            await DataContext.SaveChangesAsync();
            SLAEscalationFRTUser.Id = SLAEscalationFRTUserDAO.Id;
            await SaveReference(SLAEscalationFRTUser);
            return true;
        }

        public async Task<bool> Update(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            SLAEscalationFRTUserDAO SLAEscalationFRTUserDAO = DataContext.SLAEscalationFRTUser.Where(x => x.Id == SLAEscalationFRTUser.Id).FirstOrDefault();
            if (SLAEscalationFRTUserDAO == null)
                return false;
            SLAEscalationFRTUserDAO.Id = SLAEscalationFRTUser.Id;
            SLAEscalationFRTUserDAO.SLAEscalationFRTId = SLAEscalationFRTUser.SLAEscalationFRTId;
            SLAEscalationFRTUserDAO.AppUserId = SLAEscalationFRTUser.AppUserId;
            SLAEscalationFRTUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAEscalationFRTUser);
            return true;
        }

        public async Task<bool> Delete(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            await DataContext.SLAEscalationFRTUser.Where(x => x.Id == SLAEscalationFRTUser.Id).UpdateFromQueryAsync(x => new SLAEscalationFRTUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAEscalationFRTUser> SLAEscalationFRTUsers)
        {
            List<SLAEscalationFRTUserDAO> SLAEscalationFRTUserDAOs = new List<SLAEscalationFRTUserDAO>();
            foreach (SLAEscalationFRTUser SLAEscalationFRTUser in SLAEscalationFRTUsers)
            {
                SLAEscalationFRTUserDAO SLAEscalationFRTUserDAO = new SLAEscalationFRTUserDAO();
                SLAEscalationFRTUserDAO.Id = SLAEscalationFRTUser.Id;
                SLAEscalationFRTUserDAO.SLAEscalationFRTId = SLAEscalationFRTUser.SLAEscalationFRTId;
                SLAEscalationFRTUserDAO.AppUserId = SLAEscalationFRTUser.AppUserId;
                SLAEscalationFRTUserDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAEscalationFRTUserDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAEscalationFRTUserDAOs.Add(SLAEscalationFRTUserDAO);
            }
            await DataContext.BulkMergeAsync(SLAEscalationFRTUserDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAEscalationFRTUser> SLAEscalationFRTUsers)
        {
            List<long> Ids = SLAEscalationFRTUsers.Select(x => x.Id).ToList();
            await DataContext.SLAEscalationFRTUser
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAEscalationFRTUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
        }
        
    }
}
