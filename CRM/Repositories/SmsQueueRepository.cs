using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Enums;

namespace CRM.Repositories
{
    public interface ISmsQueueRepository
    {
        Task<int> Count(SmsQueueFilter SmsQueueFilter);
        Task<List<SmsQueue>> List(SmsQueueFilter SmsQueueFilter);
        Task<SmsQueue> Get(long Id);
        Task<bool> Create(SmsQueue SmsQueue);
        Task<bool> Update(SmsQueue SmsQueue);
        Task<bool> Delete(SmsQueue SmsQueue);
        Task<bool> BulkMerge(List<SmsQueue> SmsQueues);
        Task<bool> BulkDelete(List<SmsQueue> SmsQueues);
    }
    public class SmsQueueRepository : ISmsQueueRepository
    {
        private DataContext DataContext;
        public SmsQueueRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SmsQueueDAO> DynamicFilter(IQueryable<SmsQueueDAO> query, SmsQueueFilter filter)
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
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.SmsCode != null)
                query = query.Where(q => q.SmsCode, filter.SmsCode);
            if (filter.SmsTitle != null)
                query = query.Where(q => q.SmsTitle, filter.SmsTitle);
            if (filter.SmsContent != null)
                query = query.Where(q => q.SmsContent, filter.SmsContent);
            if (filter.SmsQueueStatusId != null)
                query = query.Where(q => q.SmsQueueStatusId, filter.SmsQueueStatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<SmsQueueDAO> OrFilter(IQueryable<SmsQueueDAO> query, SmsQueueFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SmsQueueDAO> initQuery = query.Where(q => false);
            foreach (SmsQueueFilter SmsQueueFilter in filter.OrFilter)
            {
                IQueryable<SmsQueueDAO> queryable = query;
                if (SmsQueueFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SmsQueueFilter.Id);
                if (SmsQueueFilter.Phone != null)
                    queryable = queryable.Where(q => q.Phone, SmsQueueFilter.Phone);
                if (SmsQueueFilter.SmsCode != null)
                    queryable = queryable.Where(q => q.SmsCode, SmsQueueFilter.SmsCode);
                if (SmsQueueFilter.SmsTitle != null)
                    queryable = queryable.Where(q => q.SmsTitle, SmsQueueFilter.SmsTitle);
                if (SmsQueueFilter.SmsContent != null)
                    queryable = queryable.Where(q => q.SmsContent, SmsQueueFilter.SmsContent);
                if (SmsQueueFilter.SmsQueueStatusId != null)
                    queryable = queryable.Where(q => q.SmsQueueStatusId, SmsQueueFilter.SmsQueueStatusId);
                if (SmsQueueFilter.SentByAppUserId != null)
                    queryable = queryable.Where(q => q.SentByAppUserId, SmsQueueFilter.SentByAppUserId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<SmsQueueDAO> DynamicOrder(IQueryable<SmsQueueDAO> query, SmsQueueFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SmsQueueOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SmsQueueOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case SmsQueueOrder.SmsCode:
                            query = query.OrderBy(q => q.SmsCode);
                            break;
                        case SmsQueueOrder.SmsTitle:
                            query = query.OrderBy(q => q.SmsTitle);
                            break;
                        case SmsQueueOrder.SmsContent:
                            query = query.OrderBy(q => q.SmsContent);
                            break;
                        case SmsQueueOrder.SmsQueueStatus:
                            query = query.OrderBy(q => q.SmsQueueStatusId);
                            break;
                        case SmsQueueOrder.SentByAppUser:
                            query = query.OrderBy(q => q.SentByAppUser);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SmsQueueOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SmsQueueOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case SmsQueueOrder.SmsCode:
                            query = query.OrderByDescending(q => q.SmsCode);
                            break;
                        case SmsQueueOrder.SmsTitle:
                            query = query.OrderByDescending(q => q.SmsTitle);
                            break;
                        case SmsQueueOrder.SmsContent:
                            query = query.OrderByDescending(q => q.SmsContent);
                            break;
                        case SmsQueueOrder.SmsQueueStatus:
                            query = query.OrderByDescending(q => q.SmsQueueStatusId);
                            break;
                        case SmsQueueOrder.SentByAppUser:
                            query = query.OrderBy(q => q.SentByAppUser);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SmsQueue>> DynamicSelect(IQueryable<SmsQueueDAO> query, SmsQueueFilter filter)
        {
            List<SmsQueue> SmsQueues = await query.Select(q => new SmsQueue()
            {
                Id = filter.Selects.Contains(SmsQueueSelect.Id) ? q.Id : default(long),
                Phone = filter.Selects.Contains(SmsQueueSelect.Phone) ? q.Phone : default(string),
                SmsCode = filter.Selects.Contains(SmsQueueSelect.SmsCode) ? q.SmsCode : default(string),
                SmsTitle = filter.Selects.Contains(SmsQueueSelect.SmsTitle) ? q.SmsTitle : default(string),
                SmsContent = filter.Selects.Contains(SmsQueueSelect.SmsContent) ? q.SmsContent : default(string),
                SmsQueueStatusId = filter.Selects.Contains(SmsQueueSelect.SmsQueueStatus) ? q.SmsQueueStatusId : default(long?),
                SentByAppUserId = filter.Selects.Contains(SmsQueueSelect.SentByAppUser) ? q.SentByAppUserId : default(long?),
                SmsQueueStatus = filter.Selects.Contains(SmsQueueSelect.SmsQueueStatus) && q.SmsQueueStatus != null ? new SmsQueueStatus
                {
                    Id = q.SmsQueueStatus.Id,
                    Code = q.SmsQueueStatus.Code,
                    Name = q.SmsQueueStatus.Name,
                } : null,
                SentByAppUser = filter.Selects.Contains(SmsQueueSelect.SentByAppUser) && q.SentByAppUser != null ? new AppUser
                {
                    Id = q.SentByAppUser.Id,
                    Username = q.SentByAppUser.Username,
                    DisplayName = q.SentByAppUser.DisplayName,
                    Address = q.SentByAppUser.Address,
                    Email = q.SentByAppUser.Email,
                    Phone = q.SentByAppUser.Phone,
                    SexId = q.SentByAppUser.SexId,
                    Birthday = q.SentByAppUser.Birthday,
                    Avatar = q.SentByAppUser.Avatar,
                    Department = q.SentByAppUser.Department,
                    OrganizationId = q.SentByAppUser.OrganizationId,
                    Longitude = q.SentByAppUser.Longitude,
                    Latitude = q.SentByAppUser.Latitude,
                    StatusId = q.SentByAppUser.StatusId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SmsQueues;
        }

        public async Task<int> Count(SmsQueueFilter filter)
        {
            IQueryable<SmsQueueDAO> SmsQueues = DataContext.SmsQueue.AsNoTracking();
            SmsQueues = DynamicFilter(SmsQueues, filter);
            return await SmsQueues.CountAsync();
        }

        public async Task<List<SmsQueue>> List(SmsQueueFilter filter)
        {
            if (filter == null) return new List<SmsQueue>();
            IQueryable<SmsQueueDAO> SmsQueueDAOs = DataContext.SmsQueue.AsNoTracking();
            SmsQueueDAOs = DynamicFilter(SmsQueueDAOs, filter);
            SmsQueueDAOs = DynamicOrder(SmsQueueDAOs, filter);
            List<SmsQueue> SmsQueues = await DynamicSelect(SmsQueueDAOs, filter);
            return SmsQueues;
        }

        public async Task<SmsQueue> Get(long Id)
        {
            SmsQueue SmsQueue = await DataContext.SmsQueue.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SmsQueue()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Phone = x.Phone,
                SmsCode = x.SmsCode,
                SmsTitle = x.SmsTitle,
                SmsContent = x.SmsContent,
                SmsQueueStatusId = x.SmsQueueStatusId,
                SentByAppUserId = x.SentByAppUserId,
                SmsQueueStatus = x.SmsQueueStatus == null ? null : new SmsQueueStatus
                {
                    Id = x.SmsQueueStatus.Id,
                    Code = x.SmsQueueStatus.Code,
                    Name = x.SmsQueueStatus.Name,
                },
                SentByAppUser = x.SentByAppUser == null ? null : new AppUser
                {
                    Id = x.SentByAppUser.Id,
                    Username = x.SentByAppUser.Username,
                    DisplayName = x.SentByAppUser.DisplayName,
                    Address = x.SentByAppUser.Address,
                    Email = x.SentByAppUser.Email,
                    Phone = x.SentByAppUser.Phone,
                    SexId = x.SentByAppUser.SexId,
                    Birthday = x.SentByAppUser.Birthday,
                    Avatar = x.SentByAppUser.Avatar,
                    Department = x.SentByAppUser.Department,
                    OrganizationId = x.SentByAppUser.OrganizationId,
                    Longitude = x.SentByAppUser.Longitude,
                    Latitude = x.SentByAppUser.Latitude,
                    StatusId = x.SentByAppUser.StatusId,
                },
            }).FirstOrDefaultAsync();

            if (SmsQueue == null)
                return null;

            return SmsQueue;
        }
        public async Task<bool> Create(SmsQueue SmsQueue)
        {
            SmsQueueDAO SmsQueueDAO = new SmsQueueDAO();
            SmsQueueDAO.Id = SmsQueue.Id;
            SmsQueueDAO.Phone = SmsQueue.Phone;
            SmsQueueDAO.SmsCode = SmsQueue.SmsCode;
            SmsQueueDAO.SmsTitle = SmsQueue.SmsTitle;
            SmsQueueDAO.SmsContent = SmsQueue.SmsContent;
            SmsQueueDAO.SmsQueueStatusId = SmsQueue.SmsQueueStatusId;
            SmsQueueDAO.SentDate = StaticParams.DateTimeNow;
            SmsQueueDAO.SentByAppUserId = SmsQueue.SentByAppUserId;
            SmsQueueDAO.CreatedAt = StaticParams.DateTimeNow;
            SmsQueueDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SmsQueue.Add(SmsQueueDAO);
            await DataContext.SaveChangesAsync();
            SmsQueue.Id = SmsQueueDAO.Id;
            await SaveReference(SmsQueue);
            return true;
        }

        public async Task<bool> Update(SmsQueue SmsQueue)
        {
            SmsQueueDAO SmsQueueDAO = DataContext.SmsQueue.Where(x => x.Id == SmsQueue.Id).FirstOrDefault();
            if (SmsQueueDAO == null)
                return false;
            SmsQueueDAO.Id = SmsQueue.Id;
            SmsQueueDAO.Phone = SmsQueue.Phone;
            SmsQueueDAO.SmsCode = SmsQueue.SmsCode;
            SmsQueueDAO.SmsTitle = SmsQueue.SmsTitle;
            SmsQueueDAO.SmsContent = SmsQueue.SmsContent;
            SmsQueueDAO.SmsQueueStatusId = SmsQueue.SmsQueueStatusId;
            SmsQueueDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SmsQueue);
            return true;
        }

        public async Task<bool> Delete(SmsQueue SmsQueue)
        {
            await DataContext.SmsQueue.Where(x => x.Id == SmsQueue.Id).UpdateFromQueryAsync(x => new SmsQueueDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<SmsQueue> SmsQueues)
        {
            List<SmsQueueDAO> SmsQueueDAOs = new List<SmsQueueDAO>();
            foreach (SmsQueue SmsQueue in SmsQueues)
            {
                SmsQueueDAO SmsQueueDAO = new SmsQueueDAO();
                SmsQueueDAO.Id = SmsQueue.Id;
                SmsQueueDAO.Phone = SmsQueue.Phone;
                SmsQueueDAO.SmsCode = SmsQueue.SmsCode;
                SmsQueueDAO.SmsTitle = SmsQueue.SmsTitle;
                SmsQueueDAO.SmsContent = SmsQueue.SmsContent;
                SmsQueueDAO.SmsQueueStatusId = SmsQueue.SmsQueueStatusId;
                SmsQueueDAO.CreatedAt = StaticParams.DateTimeNow;
                SmsQueueDAO.UpdatedAt = StaticParams.DateTimeNow;
                SmsQueueDAOs.Add(SmsQueueDAO);
            }
            await DataContext.BulkMergeAsync(SmsQueueDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SmsQueue> SmsQueues)
        {
            List<long> Ids = SmsQueues.Select(x => x.Id).ToList();
            await DataContext.SmsQueue
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SmsQueueDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SmsQueue SmsQueue)
        {

        }
    }
}
