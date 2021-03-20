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
    public interface ISmsQueueStatusRepository
    {
        Task<int> Count(SmsQueueStatusFilter SmsQueueStatusFilter);
        Task<List<SmsQueueStatus>> List(SmsQueueStatusFilter SmsQueueStatusFilter);
        Task<SmsQueueStatus> Get(long Id);
        Task<bool> Create(SmsQueueStatus SmsQueueStatus);
        Task<bool> Update(SmsQueueStatus SmsQueueStatus);
        Task<bool> Delete(SmsQueueStatus SmsQueueStatus);
        Task<bool> BulkMerge(List<SmsQueueStatus> SmsQueueStatuses);
        Task<bool> BulkDelete(List<SmsQueueStatus> SmsQueueStatuses);
    }
    public class SmsQueueStatusRepository : ISmsQueueStatusRepository
    {
        private DataContext DataContext;
        public SmsQueueStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SmsQueueStatusDAO> DynamicFilter(IQueryable<SmsQueueStatusDAO> query, SmsQueueStatusFilter filter)
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
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SmsQueueStatusDAO> OrFilter(IQueryable<SmsQueueStatusDAO> query, SmsQueueStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SmsQueueStatusDAO> initQuery = query.Where(q => false);
            foreach (SmsQueueStatusFilter SmsQueueStatusFilter in filter.OrFilter)
            {
                IQueryable<SmsQueueStatusDAO> queryable = query;
                if (SmsQueueStatusFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SmsQueueStatusFilter.Id);
                if (SmsQueueStatusFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, SmsQueueStatusFilter.Code);
                if (SmsQueueStatusFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, SmsQueueStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SmsQueueStatusDAO> DynamicOrder(IQueryable<SmsQueueStatusDAO> query, SmsQueueStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SmsQueueStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SmsQueueStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SmsQueueStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SmsQueueStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SmsQueueStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SmsQueueStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SmsQueueStatus>> DynamicSelect(IQueryable<SmsQueueStatusDAO> query, SmsQueueStatusFilter filter)
        {
            List<SmsQueueStatus> SmsQueueStatuses = await query.Select(q => new SmsQueueStatus()
            {
                Id = filter.Selects.Contains(SmsQueueStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(SmsQueueStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SmsQueueStatusSelect.Name) ? q.Name : default(string),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SmsQueueStatuses;
        }

        public async Task<int> Count(SmsQueueStatusFilter filter)
        {
            IQueryable<SmsQueueStatusDAO> SmsQueueStatuses = DataContext.SmsQueueStatus.AsNoTracking();
            SmsQueueStatuses = DynamicFilter(SmsQueueStatuses, filter);
            return await SmsQueueStatuses.CountAsync();
        }

        public async Task<List<SmsQueueStatus>> List(SmsQueueStatusFilter filter)
        {
            if (filter == null) return new List<SmsQueueStatus>();
            IQueryable<SmsQueueStatusDAO> SmsQueueStatusDAOs = DataContext.SmsQueueStatus.AsNoTracking();
            SmsQueueStatusDAOs = DynamicFilter(SmsQueueStatusDAOs, filter);
            SmsQueueStatusDAOs = DynamicOrder(SmsQueueStatusDAOs, filter);
            List<SmsQueueStatus> SmsQueueStatuses = await DynamicSelect(SmsQueueStatusDAOs, filter);
            return SmsQueueStatuses;
        }

        public async Task<SmsQueueStatus> Get(long Id)
        {
            SmsQueueStatus SmsQueueStatus = await DataContext.SmsQueueStatus.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SmsQueueStatus()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (SmsQueueStatus == null)
                return null;

            return SmsQueueStatus;
        }
        public async Task<bool> Create(SmsQueueStatus SmsQueueStatus)
        {
            SmsQueueStatusDAO SmsQueueStatusDAO = new SmsQueueStatusDAO();
            SmsQueueStatusDAO.Id = SmsQueueStatus.Id;
            SmsQueueStatusDAO.Code = SmsQueueStatus.Code;
            SmsQueueStatusDAO.Name = SmsQueueStatus.Name;
            SmsQueueStatusDAO.CreatedAt = StaticParams.DateTimeNow;
            SmsQueueStatusDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SmsQueueStatus.Add(SmsQueueStatusDAO);
            await DataContext.SaveChangesAsync();
            SmsQueueStatus.Id = SmsQueueStatusDAO.Id;
            await SaveReference(SmsQueueStatus);
            return true;
        }

        public async Task<bool> Update(SmsQueueStatus SmsQueueStatus)
        {
            SmsQueueStatusDAO SmsQueueStatusDAO = DataContext.SmsQueueStatus.Where(x => x.Id == SmsQueueStatus.Id).FirstOrDefault();
            if (SmsQueueStatusDAO == null)
                return false;
            SmsQueueStatusDAO.Id = SmsQueueStatus.Id;
            SmsQueueStatusDAO.Code = SmsQueueStatus.Code;
            SmsQueueStatusDAO.Name = SmsQueueStatus.Name;
            SmsQueueStatusDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SmsQueueStatus);
            return true;
        }

        public async Task<bool> Delete(SmsQueueStatus SmsQueueStatus)
        {
            await DataContext.SmsQueueStatus.Where(x => x.Id == SmsQueueStatus.Id).UpdateFromQueryAsync(x => new SmsQueueStatusDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SmsQueueStatus> SmsQueueStatuses)
        {
            List<SmsQueueStatusDAO> SmsQueueStatusDAOs = new List<SmsQueueStatusDAO>();
            foreach (SmsQueueStatus SmsQueueStatus in SmsQueueStatuses)
            {
                SmsQueueStatusDAO SmsQueueStatusDAO = new SmsQueueStatusDAO();
                SmsQueueStatusDAO.Id = SmsQueueStatus.Id;
                SmsQueueStatusDAO.Code = SmsQueueStatus.Code;
                SmsQueueStatusDAO.Name = SmsQueueStatus.Name;
                SmsQueueStatusDAO.CreatedAt = StaticParams.DateTimeNow;
                SmsQueueStatusDAO.UpdatedAt = StaticParams.DateTimeNow;
                SmsQueueStatusDAOs.Add(SmsQueueStatusDAO);
            }
            await DataContext.BulkMergeAsync(SmsQueueStatusDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SmsQueueStatus> SmsQueueStatuses)
        {
            List<long> Ids = SmsQueueStatuses.Select(x => x.Id).ToList();
            await DataContext.SmsQueueStatus
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SmsQueueStatusDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SmsQueueStatus SmsQueueStatus)
        {
        }
        
    }
}
