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
    public interface ITicketSourceRepository
    {
        Task<int> Count(TicketSourceFilter TicketSourceFilter);
        Task<List<TicketSource>> List(TicketSourceFilter TicketSourceFilter);
        Task<TicketSource> Get(long Id);
        Task<bool> Create(TicketSource TicketSource);
        Task<bool> Update(TicketSource TicketSource);
        Task<bool> Delete(TicketSource TicketSource);
        Task<bool> BulkMerge(List<TicketSource> TicketSources);
        Task<bool> BulkDelete(List<TicketSource> TicketSources);
    }
    public class TicketSourceRepository : ITicketSourceRepository
    {
        private DataContext DataContext;
        public TicketSourceRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketSourceDAO> DynamicFilter(IQueryable<TicketSourceDAO> query, TicketSourceFilter filter)
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
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<TicketSourceDAO> OrFilter(IQueryable<TicketSourceDAO> query, TicketSourceFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketSourceDAO> initQuery = query.Where(q => false);
            foreach (TicketSourceFilter TicketSourceFilter in filter.OrFilter)
            {
                IQueryable<TicketSourceDAO> queryable = query;
                if (TicketSourceFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TicketSourceFilter.Id);
                if (TicketSourceFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, TicketSourceFilter.Name);
                if (TicketSourceFilter.OrderNumber != null)
                    queryable = queryable.Where(q => q.OrderNumber, TicketSourceFilter.OrderNumber);
                if (TicketSourceFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, TicketSourceFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketSourceDAO> DynamicOrder(IQueryable<TicketSourceDAO> query, TicketSourceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketSourceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketSourceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TicketSourceOrder.OrderNumber:
                            query = query.OrderBy(q => q.OrderNumber);
                            break;
                        case TicketSourceOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case TicketSourceOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketSourceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketSourceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TicketSourceOrder.OrderNumber:
                            query = query.OrderByDescending(q => q.OrderNumber);
                            break;
                        case TicketSourceOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case TicketSourceOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketSource>> DynamicSelect(IQueryable<TicketSourceDAO> query, TicketSourceFilter filter)
        {
            List<TicketSource> TicketSources = await query.Select(q => new TicketSource()
            {
                Id = filter.Selects.Contains(TicketSourceSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(TicketSourceSelect.Name) ? q.Name : default(string),
                OrderNumber = filter.Selects.Contains(TicketSourceSelect.OrderNumber) ? q.OrderNumber : default(long),
                StatusId = filter.Selects.Contains(TicketSourceSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(TicketSourceSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(TicketSourceSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketSources;
        }

        public async Task<int> Count(TicketSourceFilter filter)
        {
            IQueryable<TicketSourceDAO> TicketSources = DataContext.TicketSource.AsNoTracking();
            TicketSources = DynamicFilter(TicketSources, filter);
            return await TicketSources.CountAsync();
        }

        public async Task<List<TicketSource>> List(TicketSourceFilter filter)
        {
            if (filter == null) return new List<TicketSource>();
            IQueryable<TicketSourceDAO> TicketSourceDAOs = DataContext.TicketSource.AsNoTracking();
            TicketSourceDAOs = DynamicFilter(TicketSourceDAOs, filter);
            TicketSourceDAOs = DynamicOrder(TicketSourceDAOs, filter);
            List<TicketSource> TicketSources = await DynamicSelect(TicketSourceDAOs, filter);
            return TicketSources;
        }

        public async Task<TicketSource> Get(long Id)
        {
            TicketSource TicketSource = await DataContext.TicketSource.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketSource()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                OrderNumber = x.OrderNumber,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (TicketSource == null)
                return null;

            return TicketSource;
        }
        public async Task<bool> Create(TicketSource TicketSource)
        {
            TicketSourceDAO TicketSourceDAO = new TicketSourceDAO();
            TicketSourceDAO.Id = TicketSource.Id;
            TicketSourceDAO.Name = TicketSource.Name;
            TicketSourceDAO.OrderNumber = TicketSource.OrderNumber;
            TicketSourceDAO.StatusId = TicketSource.StatusId;
            TicketSourceDAO.Used = TicketSource.Used;
            TicketSourceDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketSourceDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketSource.Add(TicketSourceDAO);
            await DataContext.SaveChangesAsync();
            TicketSource.Id = TicketSourceDAO.Id;
            await SaveReference(TicketSource);
            return true;
        }

        public async Task<bool> Update(TicketSource TicketSource)
        {
            TicketSourceDAO TicketSourceDAO = DataContext.TicketSource.Where(x => x.Id == TicketSource.Id).FirstOrDefault();
            if (TicketSourceDAO == null)
                return false;
            TicketSourceDAO.Id = TicketSource.Id;
            TicketSourceDAO.Name = TicketSource.Name;
            TicketSourceDAO.OrderNumber = TicketSource.OrderNumber;
            TicketSourceDAO.StatusId = TicketSource.StatusId;
            TicketSourceDAO.Used = TicketSource.Used;
            TicketSourceDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(TicketSource);
            return true;
        }

        public async Task<bool> Delete(TicketSource TicketSource)
        {
            await DataContext.TicketSource.Where(x => x.Id == TicketSource.Id).UpdateFromQueryAsync(x => new TicketSourceDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<TicketSource> TicketSources)
        {
            List<TicketSourceDAO> TicketSourceDAOs = new List<TicketSourceDAO>();
            foreach (TicketSource TicketSource in TicketSources)
            {
                TicketSourceDAO TicketSourceDAO = new TicketSourceDAO();
                TicketSourceDAO.Id = TicketSource.Id;
                TicketSourceDAO.Name = TicketSource.Name;
                TicketSourceDAO.OrderNumber = TicketSource.OrderNumber;
                TicketSourceDAO.StatusId = TicketSource.StatusId;
                TicketSourceDAO.Used = TicketSource.Used;
                TicketSourceDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketSourceDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketSourceDAOs.Add(TicketSourceDAO);
            }
            await DataContext.BulkMergeAsync(TicketSourceDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<TicketSource> TicketSources)
        {
            List<long> Ids = TicketSources.Select(x => x.Id).ToList();
            await DataContext.TicketSource
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketSourceDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(TicketSource TicketSource)
        {
        }
        
    }
}
