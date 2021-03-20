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
    public interface ITicketPriorityRepository
    {
        Task<int> Count(TicketPriorityFilter TicketPriorityFilter);
        Task<List<TicketPriority>> List(TicketPriorityFilter TicketPriorityFilter);
        Task<TicketPriority> Get(long Id);
        Task<bool> Create(TicketPriority TicketPriority);
        Task<bool> Update(TicketPriority TicketPriority);
        Task<bool> Delete(TicketPriority TicketPriority);
        Task<bool> BulkMerge(List<TicketPriority> TicketPriorities);
        Task<bool> BulkDelete(List<TicketPriority> TicketPriorities);
    }
    public class TicketPriorityRepository : ITicketPriorityRepository
    {
        private DataContext DataContext;
        public TicketPriorityRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketPriorityDAO> DynamicFilter(IQueryable<TicketPriorityDAO> query, TicketPriorityFilter filter)
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

         private IQueryable<TicketPriorityDAO> OrFilter(IQueryable<TicketPriorityDAO> query, TicketPriorityFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketPriorityDAO> initQuery = query.Where(q => false);
            foreach (TicketPriorityFilter TicketPriorityFilter in filter.OrFilter)
            {
                IQueryable<TicketPriorityDAO> queryable = query;
                if (TicketPriorityFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TicketPriorityFilter.Id);
                if (TicketPriorityFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, TicketPriorityFilter.Name);
                if (TicketPriorityFilter.OrderNumber != null)
                    queryable = queryable.Where(q => q.OrderNumber, TicketPriorityFilter.OrderNumber);
                if (TicketPriorityFilter.ColorCode != null)
                    queryable = queryable.Where(q => q.ColorCode, TicketPriorityFilter.ColorCode);
                if (TicketPriorityFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, TicketPriorityFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketPriorityDAO> DynamicOrder(IQueryable<TicketPriorityDAO> query, TicketPriorityFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketPriorityOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketPriorityOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TicketPriorityOrder.OrderNumber:
                            query = query.OrderBy(q => q.OrderNumber);
                            break;
                        case TicketPriorityOrder.ColorCode:
                            query = query.OrderBy(q => q.ColorCode);
                            break;
                        case TicketPriorityOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case TicketPriorityOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketPriorityOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketPriorityOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TicketPriorityOrder.OrderNumber:
                            query = query.OrderByDescending(q => q.OrderNumber);
                            break;
                        case TicketPriorityOrder.ColorCode:
                            query = query.OrderByDescending(q => q.ColorCode);
                            break;
                        case TicketPriorityOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case TicketPriorityOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketPriority>> DynamicSelect(IQueryable<TicketPriorityDAO> query, TicketPriorityFilter filter)
        {
            List<TicketPriority> TicketPriorities = await query.Select(q => new TicketPriority()
            {
                Id = filter.Selects.Contains(TicketPrioritySelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(TicketPrioritySelect.Name) ? q.Name : default(string),
                OrderNumber = filter.Selects.Contains(TicketPrioritySelect.OrderNumber) ? q.OrderNumber : default(long),
                ColorCode = filter.Selects.Contains(TicketPrioritySelect.ColorCode) ? q.ColorCode : default(string),
                StatusId = filter.Selects.Contains(TicketPrioritySelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(TicketPrioritySelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(TicketPrioritySelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketPriorities;
        }

        public async Task<int> Count(TicketPriorityFilter filter)
        {
            IQueryable<TicketPriorityDAO> TicketPriorities = DataContext.TicketPriority.AsNoTracking();
            TicketPriorities = DynamicFilter(TicketPriorities, filter);
            return await TicketPriorities.CountAsync();
        }

        public async Task<List<TicketPriority>> List(TicketPriorityFilter filter)
        {
            if (filter == null) return new List<TicketPriority>();
            IQueryable<TicketPriorityDAO> TicketPriorityDAOs = DataContext.TicketPriority.AsNoTracking();
            TicketPriorityDAOs = DynamicFilter(TicketPriorityDAOs, filter);
            TicketPriorityDAOs = DynamicOrder(TicketPriorityDAOs, filter);
            List<TicketPriority> TicketPriorities = await DynamicSelect(TicketPriorityDAOs, filter);
            return TicketPriorities;
        }

        public async Task<TicketPriority> Get(long Id)
        {
            TicketPriority TicketPriority = await DataContext.TicketPriority.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketPriority()
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

            if (TicketPriority == null)
                return null;

            return TicketPriority;
        }
        public async Task<bool> Create(TicketPriority TicketPriority)
        {
            TicketPriorityDAO TicketPriorityDAO = new TicketPriorityDAO();
            TicketPriorityDAO.Id = TicketPriority.Id;
            TicketPriorityDAO.Name = TicketPriority.Name;
            TicketPriorityDAO.OrderNumber = TicketPriority.OrderNumber;
            TicketPriorityDAO.ColorCode = TicketPriority.ColorCode;
            TicketPriorityDAO.StatusId = TicketPriority.StatusId;
            TicketPriorityDAO.Used = TicketPriority.Used;
            TicketPriorityDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketPriorityDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketPriority.Add(TicketPriorityDAO);
            await DataContext.SaveChangesAsync();
            TicketPriority.Id = TicketPriorityDAO.Id;
            await SaveReference(TicketPriority);
            return true;
        }

        public async Task<bool> Update(TicketPriority TicketPriority)
        {
            TicketPriorityDAO TicketPriorityDAO = DataContext.TicketPriority.Where(x => x.Id == TicketPriority.Id).FirstOrDefault();
            if (TicketPriorityDAO == null)
                return false;
            TicketPriorityDAO.Id = TicketPriority.Id;
            TicketPriorityDAO.Name = TicketPriority.Name;
            TicketPriorityDAO.OrderNumber = TicketPriority.OrderNumber;
            TicketPriorityDAO.ColorCode = TicketPriority.ColorCode;
            TicketPriorityDAO.StatusId = TicketPriority.StatusId;
            TicketPriorityDAO.Used = TicketPriority.Used;
            TicketPriorityDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(TicketPriority);
            return true;
        }

        public async Task<bool> Delete(TicketPriority TicketPriority)
        {
            await DataContext.TicketPriority.Where(x => x.Id == TicketPriority.Id).UpdateFromQueryAsync(x => new TicketPriorityDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<TicketPriority> TicketPriorities)
        {
            List<TicketPriorityDAO> TicketPriorityDAOs = new List<TicketPriorityDAO>();
            foreach (TicketPriority TicketPriority in TicketPriorities)
            {
                TicketPriorityDAO TicketPriorityDAO = new TicketPriorityDAO();
                TicketPriorityDAO.Id = TicketPriority.Id;
                TicketPriorityDAO.Name = TicketPriority.Name;
                TicketPriorityDAO.OrderNumber = TicketPriority.OrderNumber;
                TicketPriorityDAO.ColorCode = TicketPriority.ColorCode;
                TicketPriorityDAO.StatusId = TicketPriority.StatusId;
                TicketPriorityDAO.Used = TicketPriority.Used;
                TicketPriorityDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketPriorityDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketPriorityDAOs.Add(TicketPriorityDAO);
            }
            await DataContext.BulkMergeAsync(TicketPriorityDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<TicketPriority> TicketPriorities)
        {
            List<long> Ids = TicketPriorities.Select(x => x.Id).ToList();
            await DataContext.TicketPriority
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketPriorityDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(TicketPriority TicketPriority)
        {
        }
        
    }
}
