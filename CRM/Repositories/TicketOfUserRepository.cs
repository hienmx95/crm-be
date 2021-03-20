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
    public interface ITicketOfUserRepository
    {
        Task<int> Count(TicketOfUserFilter TicketOfUserFilter);
        Task<List<TicketOfUser>> List(TicketOfUserFilter TicketOfUserFilter);
        Task<TicketOfUser> Get(long Id);
        Task<TicketOfUser> GetByTicketId(long Id);
        Task<bool> Create(TicketOfUser TicketOfUser);
        Task<bool> Update(TicketOfUser TicketOfUser);
        Task<bool> Delete(TicketOfUser TicketOfUser);
        Task<bool> BulkMerge(List<TicketOfUser> TicketOfUsers);
        Task<bool> BulkDelete(List<TicketOfUser> TicketOfUsers);
    }
    public class TicketOfUserRepository : ITicketOfUserRepository
    {
        private DataContext DataContext;
        public TicketOfUserRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketOfUserDAO> DynamicFilter(IQueryable<TicketOfUserDAO> query, TicketOfUserFilter filter)
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
            if (filter.Notes != null)
                query = query.Where(q => q.Notes, filter.Notes);
            if (filter.UserId != null)
                query = query.Where(q => q.UserId, filter.UserId);
            if (filter.TicketId != null)
                query = query.Where(q => q.TicketId, filter.TicketId);
            if (filter.TicketStatusId != null)
                query = query.Where(q => q.TicketStatusId, filter.TicketStatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<TicketOfUserDAO> OrFilter(IQueryable<TicketOfUserDAO> query, TicketOfUserFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketOfUserDAO> initQuery = query.Where(q => false);
            foreach (TicketOfUserFilter TicketOfUserFilter in filter.OrFilter)
            {
                IQueryable<TicketOfUserDAO> queryable = query;
                if (TicketOfUserFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TicketOfUserFilter.Id);
                if (TicketOfUserFilter.Notes != null)
                    queryable = queryable.Where(q => q.Notes, TicketOfUserFilter.Notes);
                if (TicketOfUserFilter.UserId != null)
                    queryable = queryable.Where(q => q.UserId, TicketOfUserFilter.UserId);
                if (TicketOfUserFilter.TicketId != null)
                    queryable = queryable.Where(q => q.TicketId, TicketOfUserFilter.TicketId);
                if (TicketOfUserFilter.TicketStatusId != null)
                    queryable = queryable.Where(q => q.TicketStatusId, TicketOfUserFilter.TicketStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketOfUserDAO> DynamicOrder(IQueryable<TicketOfUserDAO> query, TicketOfUserFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketOfUserOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketOfUserOrder.Notes:
                            query = query.OrderBy(q => q.Notes);
                            break;
                        case TicketOfUserOrder.User:
                            query = query.OrderBy(q => q.UserId);
                            break;
                        case TicketOfUserOrder.Ticket:
                            query = query.OrderBy(q => q.TicketId);
                            break;
                        case TicketOfUserOrder.TicketStatus:
                            query = query.OrderBy(q => q.TicketStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketOfUserOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketOfUserOrder.Notes:
                            query = query.OrderByDescending(q => q.Notes);
                            break;
                        case TicketOfUserOrder.User:
                            query = query.OrderByDescending(q => q.UserId);
                            break;
                        case TicketOfUserOrder.Ticket:
                            query = query.OrderByDescending(q => q.TicketId);
                            break;
                        case TicketOfUserOrder.TicketStatus:
                            query = query.OrderByDescending(q => q.TicketStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketOfUser>> DynamicSelect(IQueryable<TicketOfUserDAO> query, TicketOfUserFilter filter)
        {
            List<TicketOfUser> TicketOfUsers = await query.Select(q => new TicketOfUser()
            {
                Id = filter.Selects.Contains(TicketOfUserSelect.Id) ? q.Id : default(long),
                Notes = filter.Selects.Contains(TicketOfUserSelect.Notes) ? q.Notes : default(string),
                UserId = filter.Selects.Contains(TicketOfUserSelect.User) ? q.UserId : default(long),
                TicketId = filter.Selects.Contains(TicketOfUserSelect.Ticket) ? q.TicketId : default(long),
                TicketStatusId = filter.Selects.Contains(TicketOfUserSelect.TicketStatus) ? q.TicketStatusId : default(long),
                Ticket = filter.Selects.Contains(TicketOfUserSelect.Ticket) && q.Ticket != null ? new Ticket
                {
                    Id = q.Ticket.Id,
                    Name = q.Ticket.Name,
                    Phone = q.Ticket.Phone,
                    CustomerId = q.Ticket.CustomerId,
                    UserId = q.Ticket.UserId,
                    ProductId = q.Ticket.ProductId,
                    ReceiveDate = q.Ticket.ReceiveDate,
                    ProcessDate = q.Ticket.ProcessDate,
                    FinishDate = q.Ticket.FinishDate,
                    Subject = q.Ticket.Subject,
                    Content = q.Ticket.Content,
                    TicketIssueLevelId = q.Ticket.TicketIssueLevelId,
                    TicketPriorityId = q.Ticket.TicketPriorityId,
                    TicketStatusId = q.Ticket.TicketStatusId,
                    TicketSourceId = q.Ticket.TicketSourceId,
                    TicketNumber = q.Ticket.TicketNumber,
                    DepartmentId = q.Ticket.DepartmentId,
                    RelatedTicketId = q.Ticket.RelatedTicketId,
                    SLA = q.Ticket.SLA,
                    RelatedCallLogId = q.Ticket.RelatedCallLogId,
                    ResponseMethodId = q.Ticket.ResponseMethodId,
                    StatusId = q.Ticket.StatusId,
                    Used = q.Ticket.Used,
                } : null,
                TicketStatus = filter.Selects.Contains(TicketOfUserSelect.TicketStatus) && q.TicketStatus != null ? new TicketStatus
                {
                    Id = q.TicketStatus.Id,
                    Name = q.TicketStatus.Name,
                    OrderNumber = q.TicketStatus.OrderNumber,
                    ColorCode = q.TicketStatus.ColorCode,
                    StatusId = q.TicketStatus.StatusId,
                    Used = q.TicketStatus.Used,
                } : null,
                User = filter.Selects.Contains(TicketOfUserSelect.User) && q.User != null ? new AppUser
                {
                    Id = q.User.Id,
                    Username = q.User.Username,
                    DisplayName = q.User.DisplayName,
                    Address = q.User.Address,
                    Email = q.User.Email,
                    Phone = q.User.Phone,
                    SexId = q.User.SexId,
                    Birthday = q.User.Birthday,
                    Avatar = q.User.Avatar,
                    Department = q.User.Department,
                    OrganizationId = q.User.OrganizationId,
                    Longitude = q.User.Longitude,
                    Latitude = q.User.Latitude,
                    StatusId = q.User.StatusId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketOfUsers;
        }

        public async Task<int> Count(TicketOfUserFilter filter)
        {
            IQueryable<TicketOfUserDAO> TicketOfUsers = DataContext.TicketOfUser.AsNoTracking();
            TicketOfUsers = DynamicFilter(TicketOfUsers, filter);
            return await TicketOfUsers.CountAsync();
        }

        public async Task<List<TicketOfUser>> List(TicketOfUserFilter filter)
        {
            if (filter == null) return new List<TicketOfUser>();
            IQueryable<TicketOfUserDAO> TicketOfUserDAOs = DataContext.TicketOfUser.AsNoTracking();
            TicketOfUserDAOs = DynamicFilter(TicketOfUserDAOs, filter);
            TicketOfUserDAOs = DynamicOrder(TicketOfUserDAOs, filter);
            List<TicketOfUser> TicketOfUsers = await DynamicSelect(TicketOfUserDAOs, filter);
            return TicketOfUsers;
        }

        public async Task<TicketOfUser> Get(long Id)
        {
            TicketOfUser TicketOfUser = await DataContext.TicketOfUser.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketOfUser()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Notes = x.Notes,
                UserId = x.UserId,
                TicketId = x.TicketId,
                TicketStatusId = x.TicketStatusId,
                Ticket = x.Ticket == null ? null : new Ticket
                {
                    Id = x.Ticket.Id,
                    Name = x.Ticket.Name,
                    Phone = x.Ticket.Phone,
                    CustomerId = x.Ticket.CustomerId,
                    UserId = x.Ticket.UserId,
                    ProductId = x.Ticket.ProductId,
                    ReceiveDate = x.Ticket.ReceiveDate,
                    ProcessDate = x.Ticket.ProcessDate,
                    FinishDate = x.Ticket.FinishDate,
                    Subject = x.Ticket.Subject,
                    Content = x.Ticket.Content,
                    TicketIssueLevelId = x.Ticket.TicketIssueLevelId,
                    TicketPriorityId = x.Ticket.TicketPriorityId,
                    TicketStatusId = x.Ticket.TicketStatusId,
                    TicketSourceId = x.Ticket.TicketSourceId,
                    TicketNumber = x.Ticket.TicketNumber,
                    DepartmentId = x.Ticket.DepartmentId,
                    RelatedTicketId = x.Ticket.RelatedTicketId,
                    SLA = x.Ticket.SLA,
                    RelatedCallLogId = x.Ticket.RelatedCallLogId,
                    ResponseMethodId = x.Ticket.ResponseMethodId,
                    StatusId = x.Ticket.StatusId,
                    Used = x.Ticket.Used,
                },
                TicketStatus = x.TicketStatus == null ? null : new TicketStatus
                {
                    Id = x.TicketStatus.Id,
                    Name = x.TicketStatus.Name,
                    OrderNumber = x.TicketStatus.OrderNumber,
                    ColorCode = x.TicketStatus.ColorCode,
                    StatusId = x.TicketStatus.StatusId,
                    Used = x.TicketStatus.Used,
                },
                User = x.User == null ? null : new AppUser
                {
                    Id = x.User.Id,
                    Username = x.User.Username,
                    DisplayName = x.User.DisplayName,
                    Address = x.User.Address,
                    Email = x.User.Email,
                    Phone = x.User.Phone,
                    SexId = x.User.SexId,
                    Birthday = x.User.Birthday,
                    Avatar = x.User.Avatar,
                    Department = x.User.Department,
                    OrganizationId = x.User.OrganizationId,
                    Longitude = x.User.Longitude,
                    Latitude = x.User.Latitude,
                    StatusId = x.User.StatusId,
                },
            }).FirstOrDefaultAsync();

            if (TicketOfUser == null)
                return null;

            return TicketOfUser;
        }
        public async Task<TicketOfUser> GetByTicketId(long Id)
        {
            TicketOfUser TicketOfUser = await DataContext.TicketOfUser.AsNoTracking()
            .Where(x => x.TicketId == Id).OrderByDescending(x => x.CreatedAt).Select(x => new TicketOfUser()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Notes = x.Notes,
                UserId = x.UserId,
                TicketId = x.TicketId,
                TicketStatusId = x.TicketStatusId,
                Ticket = x.Ticket == null ? null : new Ticket
                {
                    Id = x.Ticket.Id,
                    Name = x.Ticket.Name,
                    Phone = x.Ticket.Phone,
                    CustomerId = x.Ticket.CustomerId,
                    UserId = x.Ticket.UserId,
                    ProductId = x.Ticket.ProductId,
                    ReceiveDate = x.Ticket.ReceiveDate,
                    ProcessDate = x.Ticket.ProcessDate,
                    FinishDate = x.Ticket.FinishDate,
                    Subject = x.Ticket.Subject,
                    Content = x.Ticket.Content,
                    TicketIssueLevelId = x.Ticket.TicketIssueLevelId,
                    TicketPriorityId = x.Ticket.TicketPriorityId,
                    TicketStatusId = x.Ticket.TicketStatusId,
                    TicketSourceId = x.Ticket.TicketSourceId,
                    TicketNumber = x.Ticket.TicketNumber,
                    DepartmentId = x.Ticket.DepartmentId,
                    RelatedTicketId = x.Ticket.RelatedTicketId,
                    SLA = x.Ticket.SLA,
                    RelatedCallLogId = x.Ticket.RelatedCallLogId,
                    ResponseMethodId = x.Ticket.ResponseMethodId,
                    StatusId = x.Ticket.StatusId,
                    Used = x.Ticket.Used,
                },
                TicketStatus = x.TicketStatus == null ? null : new TicketStatus
                {
                    Id = x.TicketStatus.Id,
                    Name = x.TicketStatus.Name,
                    OrderNumber = x.TicketStatus.OrderNumber,
                    ColorCode = x.TicketStatus.ColorCode,
                    StatusId = x.TicketStatus.StatusId,
                    Used = x.TicketStatus.Used,
                },
                User = x.User == null ? null : new AppUser
                {
                    Id = x.User.Id,
                    Username = x.User.Username,
                    DisplayName = x.User.DisplayName,
                    Address = x.User.Address,
                    Email = x.User.Email,
                    Phone = x.User.Phone,
                    SexId = x.User.SexId,
                    Birthday = x.User.Birthday,
                    Avatar = x.User.Avatar,
                    Department = x.User.Department,
                    OrganizationId = x.User.OrganizationId,
                    Longitude = x.User.Longitude,
                    Latitude = x.User.Latitude,
                    StatusId = x.User.StatusId,
                },
            }).FirstOrDefaultAsync();

            if (TicketOfUser == null)
                return null;

            return TicketOfUser;
        }
        public async Task<bool> Create(TicketOfUser TicketOfUser)
        {
            TicketOfUserDAO TicketOfUserDAO = new TicketOfUserDAO();
            TicketOfUserDAO.Id = TicketOfUser.Id;
            TicketOfUserDAO.Notes = TicketOfUser.Notes;
            TicketOfUserDAO.UserId = TicketOfUser.UserId;
            TicketOfUserDAO.TicketId = TicketOfUser.TicketId;
            TicketOfUserDAO.TicketStatusId = TicketOfUser.TicketStatusId;
            TicketOfUserDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketOfUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketOfUser.Add(TicketOfUserDAO);
            await DataContext.SaveChangesAsync();
            TicketOfUser.Id = TicketOfUserDAO.Id;
            await SaveReference(TicketOfUser);
            return true;
        }

        public async Task<bool> Update(TicketOfUser TicketOfUser)
        {
            TicketOfUserDAO TicketOfUserDAO = DataContext.TicketOfUser.Where(x => x.Id == TicketOfUser.Id).FirstOrDefault();
            if (TicketOfUserDAO == null)
                return false;
            TicketOfUserDAO.Id = TicketOfUser.Id;
            TicketOfUserDAO.Notes = TicketOfUser.Notes;
            TicketOfUserDAO.UserId = TicketOfUser.UserId;
            TicketOfUserDAO.TicketId = TicketOfUser.TicketId;
            TicketOfUserDAO.TicketStatusId = TicketOfUser.TicketStatusId;
            TicketOfUserDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(TicketOfUser);
            return true;
        }

        public async Task<bool> Delete(TicketOfUser TicketOfUser)
        {
            await DataContext.TicketOfUser.Where(x => x.Id == TicketOfUser.Id).UpdateFromQueryAsync(x => new TicketOfUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<TicketOfUser> TicketOfUsers)
        {
            List<TicketOfUserDAO> TicketOfUserDAOs = new List<TicketOfUserDAO>();
            foreach (TicketOfUser TicketOfUser in TicketOfUsers)
            {
                TicketOfUserDAO TicketOfUserDAO = new TicketOfUserDAO();
                TicketOfUserDAO.Id = TicketOfUser.Id;
                TicketOfUserDAO.Notes = TicketOfUser.Notes;
                TicketOfUserDAO.UserId = TicketOfUser.UserId;
                TicketOfUserDAO.TicketId = TicketOfUser.TicketId;
                TicketOfUserDAO.TicketStatusId = TicketOfUser.TicketStatusId;
                TicketOfUserDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketOfUserDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketOfUserDAOs.Add(TicketOfUserDAO);
            }
            await DataContext.BulkMergeAsync(TicketOfUserDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<TicketOfUser> TicketOfUsers)
        {
            List<long> Ids = TicketOfUsers.Select(x => x.Id).ToList();
            await DataContext.TicketOfUser
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketOfUserDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(TicketOfUser TicketOfUser)
        {
        }
        
    }
}
