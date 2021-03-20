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
    public interface ISLAEscalationUserRepository
    {
        Task<int> Count(SLAEscalationUserFilter SLAEscalationUserFilter);
        Task<List<SLAEscalationUser>> List(SLAEscalationUserFilter SLAEscalationUserFilter);
        Task<SLAEscalationUser> Get(long Id);
        Task<bool> Create(SLAEscalationUser SLAEscalationUser);
        Task<bool> Update(SLAEscalationUser SLAEscalationUser);
        Task<bool> Delete(SLAEscalationUser SLAEscalationUser);
        Task<bool> BulkMerge(List<SLAEscalationUser> SLAEscalationUsers);
        Task<bool> BulkDelete(List<SLAEscalationUser> SLAEscalationUsers);
    }
    public class SLAEscalationUserRepository : ISLAEscalationUserRepository
    {
        private DataContext DataContext;
        public SLAEscalationUserRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAEscalationUserDAO> DynamicFilter(IQueryable<SLAEscalationUserDAO> query, SLAEscalationUserFilter filter)
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
            if (filter.SLAEscalationId != null)
                query = query.Where(q => q.SLAEscalationId.HasValue).Where(q => q.SLAEscalationId, filter.SLAEscalationId);
            if (filter.AppUserId != null)
                query = query.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, filter.AppUserId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAEscalationUserDAO> OrFilter(IQueryable<SLAEscalationUserDAO> query, SLAEscalationUserFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAEscalationUserDAO> initQuery = query.Where(q => false);
            foreach (SLAEscalationUserFilter SLAEscalationUserFilter in filter.OrFilter)
            {
                IQueryable<SLAEscalationUserDAO> queryable = query;
                if (SLAEscalationUserFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAEscalationUserFilter.Id);
                if (SLAEscalationUserFilter.SLAEscalationId != null)
                    queryable = queryable.Where(q => q.SLAEscalationId.HasValue).Where(q => q.SLAEscalationId, SLAEscalationUserFilter.SLAEscalationId);
                if (SLAEscalationUserFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.AppUserId.HasValue).Where(q => q.AppUserId, SLAEscalationUserFilter.AppUserId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAEscalationUserDAO> DynamicOrder(IQueryable<SLAEscalationUserDAO> query, SLAEscalationUserFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationUserOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAEscalationUserOrder.SLAEscalation:
                            query = query.OrderBy(q => q.SLAEscalationId);
                            break;
                        case SLAEscalationUserOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationUserOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAEscalationUserOrder.SLAEscalation:
                            query = query.OrderByDescending(q => q.SLAEscalationId);
                            break;
                        case SLAEscalationUserOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAEscalationUser>> DynamicSelect(IQueryable<SLAEscalationUserDAO> query, SLAEscalationUserFilter filter)
        {
            List<SLAEscalationUser> SLAEscalationUsers = await query.Select(q => new SLAEscalationUser()
            {
                Id = filter.Selects.Contains(SLAEscalationUserSelect.Id) ? q.Id : default(long),
                SLAEscalationId = filter.Selects.Contains(SLAEscalationUserSelect.SLAEscalation) ? q.SLAEscalationId : default(long?),
                AppUserId = filter.Selects.Contains(SLAEscalationUserSelect.AppUser) ? q.AppUserId : default(long?),
                AppUser = filter.Selects.Contains(SLAEscalationUserSelect.AppUser) && q.AppUser != null ? new AppUser
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
                SLAEscalation = filter.Selects.Contains(SLAEscalationUserSelect.SLAEscalation) && q.SLAEscalation != null ? new SLAEscalation
                {
                    Id = q.SLAEscalation.Id,
                    TicketIssueLevelId = q.SLAEscalation.TicketIssueLevelId,
                    IsNotification = q.SLAEscalation.IsNotification,
                    IsMail = q.SLAEscalation.IsMail,
                    IsSMS = q.SLAEscalation.IsSMS,
                    Time = q.SLAEscalation.Time,
                    TimeUnitId = q.SLAEscalation.TimeUnitId,
                    IsAssignedToUser = q.SLAEscalation.IsAssignedToUser,
                    IsAssignedToGroup = q.SLAEscalation.IsAssignedToGroup,
                    SmsTemplateId = q.SLAEscalation.SmsTemplateId,
                    MailTemplateId = q.SLAEscalation.MailTemplateId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAEscalationUsers;
        }

        public async Task<int> Count(SLAEscalationUserFilter filter)
        {
            IQueryable<SLAEscalationUserDAO> SLAEscalationUsers = DataContext.SLAEscalationUser.AsNoTracking();
            SLAEscalationUsers = DynamicFilter(SLAEscalationUsers, filter);
            return await SLAEscalationUsers.CountAsync();
        }

        public async Task<List<SLAEscalationUser>> List(SLAEscalationUserFilter filter)
        {
            if (filter == null) return new List<SLAEscalationUser>();
            IQueryable<SLAEscalationUserDAO> SLAEscalationUserDAOs = DataContext.SLAEscalationUser.AsNoTracking();
            SLAEscalationUserDAOs = DynamicFilter(SLAEscalationUserDAOs, filter);
            SLAEscalationUserDAOs = DynamicOrder(SLAEscalationUserDAOs, filter);
            List<SLAEscalationUser> SLAEscalationUsers = await DynamicSelect(SLAEscalationUserDAOs, filter);
            return SLAEscalationUsers;
        }

        public async Task<SLAEscalationUser> Get(long Id)
        {
            SLAEscalationUser SLAEscalationUser = await DataContext.SLAEscalationUser.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAEscalationUser()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAEscalationId = x.SLAEscalationId,
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
                SLAEscalation = x.SLAEscalation == null ? null : new SLAEscalation
                {
                    Id = x.SLAEscalation.Id,
                    TicketIssueLevelId = x.SLAEscalation.TicketIssueLevelId,
                    IsNotification = x.SLAEscalation.IsNotification,
                    IsMail = x.SLAEscalation.IsMail,
                    IsSMS = x.SLAEscalation.IsSMS,
                    Time = x.SLAEscalation.Time,
                    TimeUnitId = x.SLAEscalation.TimeUnitId,
                    IsAssignedToUser = x.SLAEscalation.IsAssignedToUser,
                    IsAssignedToGroup = x.SLAEscalation.IsAssignedToGroup,
                    SmsTemplateId = x.SLAEscalation.SmsTemplateId,
                    MailTemplateId = x.SLAEscalation.MailTemplateId,
                },
            }).FirstOrDefaultAsync();

            if (SLAEscalationUser == null)
                return null;

            return SLAEscalationUser;
        }
        public async Task<bool> Create(SLAEscalationUser SLAEscalationUser)
        {
            SLAEscalationUserDAO SLAEscalationUserDAO = new SLAEscalationUserDAO();
            SLAEscalationUserDAO.Id = SLAEscalationUser.Id;
            SLAEscalationUserDAO.SLAEscalationId = SLAEscalationUser.SLAEscalationId;
            SLAEscalationUserDAO.AppUserId = SLAEscalationUser.AppUserId;
            SLAEscalationUserDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAEscalationUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAEscalationUser.Add(SLAEscalationUserDAO);
            await DataContext.SaveChangesAsync();
            SLAEscalationUser.Id = SLAEscalationUserDAO.Id;
            await SaveReference(SLAEscalationUser);
            return true;
        }

        public async Task<bool> Update(SLAEscalationUser SLAEscalationUser)
        {
            SLAEscalationUserDAO SLAEscalationUserDAO = DataContext.SLAEscalationUser.Where(x => x.Id == SLAEscalationUser.Id).FirstOrDefault();
            if (SLAEscalationUserDAO == null)
                return false;
            SLAEscalationUserDAO.Id = SLAEscalationUser.Id;
            SLAEscalationUserDAO.SLAEscalationId = SLAEscalationUser.SLAEscalationId;
            SLAEscalationUserDAO.AppUserId = SLAEscalationUser.AppUserId;
            SLAEscalationUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAEscalationUser);
            return true;
        }

        public async Task<bool> Delete(SLAEscalationUser SLAEscalationUser)
        {
            await DataContext.SLAEscalationUser.Where(x => x.Id == SLAEscalationUser.Id).UpdateFromQueryAsync(x => new SLAEscalationUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAEscalationUser> SLAEscalationUsers)
        {
            List<SLAEscalationUserDAO> SLAEscalationUserDAOs = new List<SLAEscalationUserDAO>();
            foreach (SLAEscalationUser SLAEscalationUser in SLAEscalationUsers)
            {
                SLAEscalationUserDAO SLAEscalationUserDAO = new SLAEscalationUserDAO();
                SLAEscalationUserDAO.Id = SLAEscalationUser.Id;
                SLAEscalationUserDAO.SLAEscalationId = SLAEscalationUser.SLAEscalationId;
                SLAEscalationUserDAO.AppUserId = SLAEscalationUser.AppUserId;
                SLAEscalationUserDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAEscalationUserDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAEscalationUserDAOs.Add(SLAEscalationUserDAO);
            }
            await DataContext.BulkMergeAsync(SLAEscalationUserDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAEscalationUser> SLAEscalationUsers)
        {
            List<long> Ids = SLAEscalationUsers.Select(x => x.Id).ToList();
            await DataContext.SLAEscalationUser
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAEscalationUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAEscalationUser SLAEscalationUser)
        {
        }
        
    }
}
