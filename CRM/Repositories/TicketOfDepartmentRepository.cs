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
    public interface ITicketOfDepartmentRepository
    {
        Task<int> Count(TicketOfDepartmentFilter TicketOfDepartmentFilter);
        Task<List<TicketOfDepartment>> List(TicketOfDepartmentFilter TicketOfDepartmentFilter);
        Task<TicketOfDepartment> Get(long Id);
        Task<bool> Create(TicketOfDepartment TicketOfDepartment);
        Task<bool> Update(TicketOfDepartment TicketOfDepartment);
        Task<bool> Delete(TicketOfDepartment TicketOfDepartment);
        Task<bool> BulkMerge(List<TicketOfDepartment> TicketOfDepartments);
        Task<bool> BulkDelete(List<TicketOfDepartment> TicketOfDepartments);
    }
    public class TicketOfDepartmentRepository : ITicketOfDepartmentRepository
    {
        private DataContext DataContext;
        public TicketOfDepartmentRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketOfDepartmentDAO> DynamicFilter(IQueryable<TicketOfDepartmentDAO> query, TicketOfDepartmentFilter filter)
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
            if (filter.DepartmentId != null)
                query = query.Where(q => q.DepartmentId, filter.DepartmentId);
            if (filter.TicketId != null)
                query = query.Where(q => q.TicketId, filter.TicketId);
            if (filter.TicketStatusId != null)
                query = query.Where(q => q.TicketStatusId, filter.TicketStatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<TicketOfDepartmentDAO> OrFilter(IQueryable<TicketOfDepartmentDAO> query, TicketOfDepartmentFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketOfDepartmentDAO> initQuery = query.Where(q => false);
            foreach (TicketOfDepartmentFilter TicketOfDepartmentFilter in filter.OrFilter)
            {
                IQueryable<TicketOfDepartmentDAO> queryable = query;
                if (TicketOfDepartmentFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TicketOfDepartmentFilter.Id);
                if (TicketOfDepartmentFilter.Notes != null)
                    queryable = queryable.Where(q => q.Notes, TicketOfDepartmentFilter.Notes);
                if (TicketOfDepartmentFilter.DepartmentId != null)
                    queryable = queryable.Where(q => q.DepartmentId, TicketOfDepartmentFilter.DepartmentId);
                if (TicketOfDepartmentFilter.TicketId != null)
                    queryable = queryable.Where(q => q.TicketId, TicketOfDepartmentFilter.TicketId);
                if (TicketOfDepartmentFilter.TicketStatusId != null)
                    queryable = queryable.Where(q => q.TicketStatusId, TicketOfDepartmentFilter.TicketStatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketOfDepartmentDAO> DynamicOrder(IQueryable<TicketOfDepartmentDAO> query, TicketOfDepartmentFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketOfDepartmentOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketOfDepartmentOrder.Notes:
                            query = query.OrderBy(q => q.Notes);
                            break;
                        case TicketOfDepartmentOrder.Department:
                            query = query.OrderBy(q => q.DepartmentId);
                            break;
                        case TicketOfDepartmentOrder.Ticket:
                            query = query.OrderBy(q => q.TicketId);
                            break;
                        case TicketOfDepartmentOrder.TicketStatus:
                            query = query.OrderBy(q => q.TicketStatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketOfDepartmentOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketOfDepartmentOrder.Notes:
                            query = query.OrderByDescending(q => q.Notes);
                            break;
                        case TicketOfDepartmentOrder.Department:
                            query = query.OrderByDescending(q => q.DepartmentId);
                            break;
                        case TicketOfDepartmentOrder.Ticket:
                            query = query.OrderByDescending(q => q.TicketId);
                            break;
                        case TicketOfDepartmentOrder.TicketStatus:
                            query = query.OrderByDescending(q => q.TicketStatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketOfDepartment>> DynamicSelect(IQueryable<TicketOfDepartmentDAO> query, TicketOfDepartmentFilter filter)
        {
            List<TicketOfDepartment> TicketOfDepartments = await query.Select(q => new TicketOfDepartment()
            {
                Id = filter.Selects.Contains(TicketOfDepartmentSelect.Id) ? q.Id : default(long),
                Notes = filter.Selects.Contains(TicketOfDepartmentSelect.Notes) ? q.Notes : default(string),
                DepartmentId = filter.Selects.Contains(TicketOfDepartmentSelect.Department) ? q.DepartmentId : default(long),
                TicketId = filter.Selects.Contains(TicketOfDepartmentSelect.Ticket) ? q.TicketId : default(long),
                TicketStatusId = filter.Selects.Contains(TicketOfDepartmentSelect.TicketStatus) ? q.TicketStatusId : default(long),
                Department = filter.Selects.Contains(TicketOfDepartmentSelect.Department) && q.Department != null ? new Organization
                {
                    Id = q.Department.Id,
                    Code = q.Department.Code,
                    Name = q.Department.Name,
                    ParentId = q.Department.ParentId,
                    Path = q.Department.Path,
                    Level = q.Department.Level,
                    StatusId = q.Department.StatusId,
                    Phone = q.Department.Phone,
                    Email = q.Department.Email,
                    Address = q.Department.Address,
                } : null,
                Ticket = filter.Selects.Contains(TicketOfDepartmentSelect.Ticket) && q.Ticket != null ? new Ticket
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
                TicketStatus = filter.Selects.Contains(TicketOfDepartmentSelect.TicketStatus) && q.TicketStatus != null ? new TicketStatus
                {
                    Id = q.TicketStatus.Id,
                    Name = q.TicketStatus.Name,
                    OrderNumber = q.TicketStatus.OrderNumber,
                    ColorCode = q.TicketStatus.ColorCode,
                    StatusId = q.TicketStatus.StatusId,
                    Used = q.TicketStatus.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketOfDepartments;
        }

        public async Task<int> Count(TicketOfDepartmentFilter filter)
        {
            IQueryable<TicketOfDepartmentDAO> TicketOfDepartments = DataContext.TicketOfDepartment.AsNoTracking();
            TicketOfDepartments = DynamicFilter(TicketOfDepartments, filter);
            return await TicketOfDepartments.CountAsync();
        }

        public async Task<List<TicketOfDepartment>> List(TicketOfDepartmentFilter filter)
        {
            if (filter == null) return new List<TicketOfDepartment>();
            IQueryable<TicketOfDepartmentDAO> TicketOfDepartmentDAOs = DataContext.TicketOfDepartment.AsNoTracking();
            TicketOfDepartmentDAOs = DynamicFilter(TicketOfDepartmentDAOs, filter);
            TicketOfDepartmentDAOs = DynamicOrder(TicketOfDepartmentDAOs, filter);
            List<TicketOfDepartment> TicketOfDepartments = await DynamicSelect(TicketOfDepartmentDAOs, filter);
            return TicketOfDepartments;
        }

        public async Task<TicketOfDepartment> Get(long Id)
        {
            TicketOfDepartment TicketOfDepartment = await DataContext.TicketOfDepartment.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketOfDepartment()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Notes = x.Notes,
                DepartmentId = x.DepartmentId,
                TicketId = x.TicketId,
                TicketStatusId = x.TicketStatusId,
                Department = x.Department == null ? null : new Organization
                {
                    Id = x.Department.Id,
                    Code = x.Department.Code,
                    Name = x.Department.Name,
                    ParentId = x.Department.ParentId,
                    Path = x.Department.Path,
                    Level = x.Department.Level,
                    StatusId = x.Department.StatusId,
                    Phone = x.Department.Phone,
                    Email = x.Department.Email,
                    Address = x.Department.Address,
                },
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
            }).FirstOrDefaultAsync();

            if (TicketOfDepartment == null)
                return null;

            return TicketOfDepartment;
        }
        public async Task<bool> Create(TicketOfDepartment TicketOfDepartment)
        {
            TicketOfDepartmentDAO TicketOfDepartmentDAO = new TicketOfDepartmentDAO();
            TicketOfDepartmentDAO.Id = TicketOfDepartment.Id;
            TicketOfDepartmentDAO.Notes = TicketOfDepartment.Notes;
            TicketOfDepartmentDAO.DepartmentId = TicketOfDepartment.DepartmentId;
            TicketOfDepartmentDAO.TicketId = TicketOfDepartment.TicketId;
            TicketOfDepartmentDAO.TicketStatusId = TicketOfDepartment.TicketStatusId;
            TicketOfDepartmentDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketOfDepartmentDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketOfDepartment.Add(TicketOfDepartmentDAO);
            await DataContext.SaveChangesAsync();
            TicketOfDepartment.Id = TicketOfDepartmentDAO.Id;
            await SaveReference(TicketOfDepartment);
            return true;
        }

        public async Task<bool> Update(TicketOfDepartment TicketOfDepartment)
        {
            TicketOfDepartmentDAO TicketOfDepartmentDAO = DataContext.TicketOfDepartment.Where(x => x.Id == TicketOfDepartment.Id).FirstOrDefault();
            if (TicketOfDepartmentDAO == null)
                return false;
            TicketOfDepartmentDAO.Id = TicketOfDepartment.Id;
            TicketOfDepartmentDAO.Notes = TicketOfDepartment.Notes;
            TicketOfDepartmentDAO.DepartmentId = TicketOfDepartment.DepartmentId;
            TicketOfDepartmentDAO.TicketId = TicketOfDepartment.TicketId;
            TicketOfDepartmentDAO.TicketStatusId = TicketOfDepartment.TicketStatusId;
            TicketOfDepartmentDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(TicketOfDepartment);
            return true;
        }

        public async Task<bool> Delete(TicketOfDepartment TicketOfDepartment)
        {
            await DataContext.TicketOfDepartment.Where(x => x.Id == TicketOfDepartment.Id).UpdateFromQueryAsync(x => new TicketOfDepartmentDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<TicketOfDepartment> TicketOfDepartments)
        {
            List<TicketOfDepartmentDAO> TicketOfDepartmentDAOs = new List<TicketOfDepartmentDAO>();
            foreach (TicketOfDepartment TicketOfDepartment in TicketOfDepartments)
            {
                TicketOfDepartmentDAO TicketOfDepartmentDAO = new TicketOfDepartmentDAO();
                TicketOfDepartmentDAO.Id = TicketOfDepartment.Id;
                TicketOfDepartmentDAO.Notes = TicketOfDepartment.Notes;
                TicketOfDepartmentDAO.DepartmentId = TicketOfDepartment.DepartmentId;
                TicketOfDepartmentDAO.TicketId = TicketOfDepartment.TicketId;
                TicketOfDepartmentDAO.TicketStatusId = TicketOfDepartment.TicketStatusId;
                TicketOfDepartmentDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketOfDepartmentDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketOfDepartmentDAOs.Add(TicketOfDepartmentDAO);
            }
            await DataContext.BulkMergeAsync(TicketOfDepartmentDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<TicketOfDepartment> TicketOfDepartments)
        {
            List<long> Ids = TicketOfDepartments.Select(x => x.Id).ToList();
            await DataContext.TicketOfDepartment
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketOfDepartmentDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(TicketOfDepartment TicketOfDepartment)
        {
        }
        
    }
}
