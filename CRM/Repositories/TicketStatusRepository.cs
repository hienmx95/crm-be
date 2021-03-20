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
    public interface ITicketStatusRepository
    {
        Task<int> Count(TicketStatusFilter TicketStatusFilter);
        Task<List<TicketStatus>> List(TicketStatusFilter TicketStatusFilter);
        Task<TicketStatus> Get(long Id);
        Task<bool> Create(TicketStatus TicketStatus);
        Task<bool> Update(TicketStatus TicketStatus);
        Task<bool> Delete(TicketStatus TicketStatus);
        Task<bool> BulkMerge(List<TicketStatus> TicketStatuses);
        Task<bool> BulkDelete(List<TicketStatus> TicketStatuses);
    }
    public class TicketStatusRepository : ITicketStatusRepository
    {
        private DataContext DataContext;
        public TicketStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketStatusDAO> DynamicFilter(IQueryable<TicketStatusDAO> query, TicketStatusFilter filter)
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
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.OrderNumber != null)
                query = query.Where(q => q.OrderNumber, filter.OrderNumber);
            if (filter.ColorCode != null)
                query = query.Where(q => q.ColorCode, filter.ColorCode);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<TicketStatusDAO> OrFilter(IQueryable<TicketStatusDAO> query, TicketStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketStatusDAO> initQuery = query.Where(q => false);
            foreach (TicketStatusFilter TicketStatusFilter in filter.OrFilter)
            {
                IQueryable<TicketStatusDAO> queryable = query;
                if (TicketStatusFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TicketStatusFilter.Id);
                if (TicketStatusFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, TicketStatusFilter.Name);
                if (TicketStatusFilter.OrderNumber != null)
                    queryable = queryable.Where(q => q.OrderNumber, TicketStatusFilter.OrderNumber);
                if (TicketStatusFilter.ColorCode != null)
                    queryable = queryable.Where(q => q.ColorCode, TicketStatusFilter.ColorCode);
                if (TicketStatusFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, TicketStatusFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketStatusDAO> DynamicOrder(IQueryable<TicketStatusDAO> query, TicketStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TicketStatusOrder.OrderNumber:
                            query = query.OrderBy(q => q.OrderNumber);
                            break;
                        case TicketStatusOrder.ColorCode:
                            query = query.OrderBy(q => q.ColorCode);
                            break;
                        case TicketStatusOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case TicketStatusOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TicketStatusOrder.OrderNumber:
                            query = query.OrderByDescending(q => q.OrderNumber);
                            break;
                        case TicketStatusOrder.ColorCode:
                            query = query.OrderByDescending(q => q.ColorCode);
                            break;
                        case TicketStatusOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case TicketStatusOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketStatus>> DynamicSelect(IQueryable<TicketStatusDAO> query, TicketStatusFilter filter)
        {
            List<TicketStatus> TicketStatuses = await query.Select(q => new TicketStatus()
            {
                Id = filter.Selects.Contains(TicketStatusSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(TicketStatusSelect.Name) ? q.Name : default(string),
                OrderNumber = filter.Selects.Contains(TicketStatusSelect.OrderNumber) ? q.OrderNumber : default(long),
                ColorCode = filter.Selects.Contains(TicketStatusSelect.ColorCode) ? q.ColorCode : default(string),
                StatusId = filter.Selects.Contains(TicketStatusSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(TicketStatusSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(TicketStatusSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketStatuses;
        }

        public async Task<int> Count(TicketStatusFilter filter)
        {
            IQueryable<TicketStatusDAO> TicketStatuses = DataContext.TicketStatus.AsNoTracking();
            TicketStatuses = DynamicFilter(TicketStatuses, filter);
            return await TicketStatuses.CountAsync();
        }

        public async Task<List<TicketStatus>> List(TicketStatusFilter filter)
        {
            if (filter == null) return new List<TicketStatus>();
            IQueryable<TicketStatusDAO> TicketStatusDAOs = DataContext.TicketStatus.AsNoTracking();
            TicketStatusDAOs = DynamicFilter(TicketStatusDAOs, filter);
            TicketStatusDAOs = DynamicOrder(TicketStatusDAOs, filter);
            List<TicketStatus> TicketStatuses = await DynamicSelect(TicketStatusDAOs, filter);
            return TicketStatuses;
        }

        public async Task<TicketStatus> Get(long Id)
        {
            TicketStatus TicketStatus = await DataContext.TicketStatus.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketStatus()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                OrderNumber = x.OrderNumber,
                ColorCode = x.ColorCode,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (TicketStatus == null)
                return null;

            return TicketStatus;
        }
        public async Task<bool> Create(TicketStatus TicketStatus)
        {
            TicketStatusDAO TicketStatusDAO = new TicketStatusDAO();
            TicketStatusDAO.Id = TicketStatus.Id;
            TicketStatusDAO.Name = TicketStatus.Name;
            TicketStatusDAO.OrderNumber = TicketStatus.OrderNumber;
            TicketStatusDAO.ColorCode = TicketStatus.ColorCode;
            TicketStatusDAO.StatusId = TicketStatus.StatusId;
            TicketStatusDAO.Used = TicketStatus.Used;
            TicketStatusDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketStatusDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketStatus.Add(TicketStatusDAO);
            await DataContext.SaveChangesAsync();
            TicketStatus.Id = TicketStatusDAO.Id;
            await SaveReference(TicketStatus);
            return true;
        }

        public async Task<bool> Update(TicketStatus TicketStatus)
        {
            TicketStatusDAO TicketStatusDAO = DataContext.TicketStatus.Where(x => x.Id == TicketStatus.Id).FirstOrDefault();
            if (TicketStatusDAO == null)
                return false;
            TicketStatusDAO.Id = TicketStatus.Id;
            TicketStatusDAO.Name = TicketStatus.Name;
            TicketStatusDAO.OrderNumber = TicketStatus.OrderNumber;
            TicketStatusDAO.ColorCode = TicketStatus.ColorCode;
            TicketStatusDAO.StatusId = TicketStatus.StatusId;
            TicketStatusDAO.Used = TicketStatus.Used;
            TicketStatusDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(TicketStatus);
            return true;
        }

        public async Task<bool> Delete(TicketStatus TicketStatus)
        {
            await DataContext.TicketStatus.Where(x => x.Id == TicketStatus.Id).UpdateFromQueryAsync(x => new TicketStatusDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<TicketStatus> TicketStatuses)
        {
            List<TicketStatusDAO> TicketStatusDAOs = new List<TicketStatusDAO>();
            foreach (TicketStatus TicketStatus in TicketStatuses)
            {
                TicketStatusDAO TicketStatusDAO = new TicketStatusDAO();
                TicketStatusDAO.Id = TicketStatus.Id;
                TicketStatusDAO.Name = TicketStatus.Name;
                TicketStatusDAO.OrderNumber = TicketStatus.OrderNumber;
                TicketStatusDAO.ColorCode = TicketStatus.ColorCode;
                TicketStatusDAO.StatusId = TicketStatus.StatusId;
                TicketStatusDAO.Used = TicketStatus.Used;
                TicketStatusDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketStatusDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketStatusDAOs.Add(TicketStatusDAO);
            }
            await DataContext.BulkMergeAsync(TicketStatusDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<TicketStatus> TicketStatuses)
        {
            List<long> Ids = TicketStatuses.Select(x => x.Id).ToList();
            await DataContext.TicketStatus
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketStatusDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(TicketStatus TicketStatus)
        {
        }
        
    }
}
