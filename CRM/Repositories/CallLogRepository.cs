using CRM.Common;
using CRM.Helpers;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface ICallLogRepository
    {
        Task<int> Count(CallLogFilter CallLogFilter);
        Task<List<CallLog>> List(CallLogFilter CallLogFilter);
        Task<List<CallLog>> List(List<long> Ids);
        Task<CallLog> Get(long Id);
        Task<bool> Create(CallLog CallLog);
        Task<bool> Update(CallLog CallLog);
        Task<bool> Delete(CallLog CallLog);
        Task<bool> BulkMerge(List<CallLog> CallLogs);
        Task<bool> BulkDelete(List<CallLog> CallLogs);
    }
    public class CallLogRepository : ICallLogRepository
    {
        private DataContext DataContext;
        public CallLogRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CallLogDAO> DynamicFilter(IQueryable<CallLogDAO> query, CallLogFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null && filter.CreatedAt.HasValue)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null && filter.UpdatedAt.HasValue)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Title != null && filter.Title.HasValue)
                query = query.Where(q => q.Title, filter.Title);
            if (filter.Content != null && filter.Content.HasValue)
                query = query.Where(q => q.Content, filter.Content);
            if (filter.Phone != null && filter.Phone.HasValue)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.CallTime != null && filter.CallTime.HasValue)
                query = query.Where(q => q.CallTime, filter.CallTime);
            if (filter.EntityReferenceId != null && filter.EntityReferenceId.HasValue)
                query = query.Where(q => q.EntityReferenceId, filter.EntityReferenceId);
            if (filter.EntityId != null && filter.EntityId.HasValue)
                query = query.Where(q => q.EntityId, filter.EntityId);
            if (filter.CallTypeId != null && filter.CallTypeId.HasValue)
                query = query.Where(q => q.CallTypeId, filter.CallTypeId);
            if (filter.CallCategoryId != null && filter.CallCategoryId.HasValue)
                query = query.Where(q => q.CallCategoryId.HasValue).Where(q => q.CallCategoryId, filter.CallCategoryId);
            if (filter.CallEmotionId != null && filter.CallEmotionId.HasValue)
                query = query.Where(q => q.CallEmotionId.HasValue).Where(q => q.CallEmotionId, filter.CallEmotionId);
            if (filter.CallStatusId != null && filter.CallStatusId.HasValue)
                query = query.Where(q => q.CallStatusId.HasValue).Where(q => q.CallStatusId, filter.CallStatusId);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CallLogDAO> OrFilter(IQueryable<CallLogDAO> query, CallLogFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CallLogDAO> initQuery = query.Where(q => false);
            foreach (CallLogFilter CallLogFilter in filter.OrFilter)
            {
                IQueryable<CallLogDAO> queryable = query;
                if (CallLogFilter.Id != null && CallLogFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CallLogFilter.Id);
                if (CallLogFilter.Title != null && CallLogFilter.Title.HasValue)
                    queryable = queryable.Where(q => q.Title, CallLogFilter.Title);
                if (CallLogFilter.Content != null && CallLogFilter.Content.HasValue)
                    queryable = queryable.Where(q => q.Content, CallLogFilter.Content);
                if (CallLogFilter.Phone != null && CallLogFilter.Phone.HasValue)
                    queryable = queryable.Where(q => q.Phone, CallLogFilter.Phone);
                if (CallLogFilter.CallTime != null && CallLogFilter.CallTime.HasValue)
                    queryable = queryable.Where(q => q.CallTime, CallLogFilter.CallTime);
                if (CallLogFilter.EntityReferenceId != null && CallLogFilter.EntityReferenceId.HasValue)
                    queryable = queryable.Where(q => q.EntityReferenceId, CallLogFilter.EntityReferenceId);
                if (CallLogFilter.EntityId != null && CallLogFilter.EntityId.HasValue)
                    queryable = queryable.Where(q => q.EntityId, CallLogFilter.EntityId);
                if (CallLogFilter.CallTypeId != null && CallLogFilter.CallTypeId.HasValue)
                    queryable = queryable.Where(q => q.CallTypeId, CallLogFilter.CallTypeId);
                if (CallLogFilter.CallCategoryId != null && CallLogFilter.CallCategoryId.HasValue)
                    queryable = queryable.Where(q => q.CallCategoryId.HasValue).Where(q => q.CallCategoryId, CallLogFilter.CallCategoryId);
                if (CallLogFilter.CallEmotionId != null && CallLogFilter.CallEmotionId.HasValue)
                    queryable = queryable.Where(q => q.CallEmotionId.HasValue).Where(q => q.CallEmotionId, CallLogFilter.CallEmotionId);
                if (CallLogFilter.CallStatusId != null && CallLogFilter.CallStatusId.HasValue)
                    queryable = queryable.Where(q => q.CallStatusId.HasValue).Where(q => q.CallStatusId, CallLogFilter.CallStatusId);
                if (CallLogFilter.AppUserId != null && CallLogFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, CallLogFilter.AppUserId);
                if (CallLogFilter.CreatorId != null && CallLogFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, CallLogFilter.CreatorId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<CallLogDAO> DynamicOrder(IQueryable<CallLogDAO> query, CallLogFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CallLogOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CallLogOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case CallLogOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case CallLogOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case CallLogOrder.CallTime:
                            query = query.OrderBy(q => q.CallTime);
                            break;
                        case CallLogOrder.EntityReference:
                            query = query.OrderBy(q => q.EntityReferenceId);
                            break;
                        case CallLogOrder.Entity:
                            query = query.OrderBy(q => q.EntityId);
                            break;
                        case CallLogOrder.CallType:
                            query = query.OrderBy(q => q.CallTypeId);
                            break;
                        case CallLogOrder.CallCategory:
                            query = query.OrderBy(q => q.CallCategoryId);
                            break;
                        case CallLogOrder.CallEmotion:
                            query = query.OrderBy(q => q.CallEmotionId);
                            break;
                        case CallLogOrder.CallStatus:
                            query = query.OrderBy(q => q.CallStatusId);
                            break;
                        case CallLogOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case CallLogOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CallLogOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CallLogOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case CallLogOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case CallLogOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case CallLogOrder.CallTime:
                            query = query.OrderByDescending(q => q.CallTime);
                            break;
                        case CallLogOrder.EntityReference:
                            query = query.OrderByDescending(q => q.EntityReferenceId);
                            break;
                        case CallLogOrder.Entity:
                            query = query.OrderByDescending(q => q.EntityId);
                            break;
                        case CallLogOrder.CallType:
                            query = query.OrderByDescending(q => q.CallTypeId);
                            break;
                        case CallLogOrder.CallCategory:
                            query = query.OrderByDescending(q => q.CallCategoryId);
                            break;
                        case CallLogOrder.CallEmotion:
                            query = query.OrderByDescending(q => q.CallEmotionId);
                            break;
                        case CallLogOrder.CallStatus:
                            query = query.OrderByDescending(q => q.CallStatusId);
                            break;
                        case CallLogOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case CallLogOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CallLog>> DynamicSelect(IQueryable<CallLogDAO> query, CallLogFilter filter)
        {
            List<CallLog> CallLogs = await query.Select(q => new CallLog()
            {
                Id = filter.Selects.Contains(CallLogSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(CallLogSelect.Title) ? q.Title : default(string),
                Content = filter.Selects.Contains(CallLogSelect.Content) ? q.Content : default(string),
                Phone = filter.Selects.Contains(CallLogSelect.Phone) ? q.Phone : default(string),
                CallTime = filter.Selects.Contains(CallLogSelect.CallTime) ? q.CallTime : default(DateTime),
                EntityReferenceId = filter.Selects.Contains(CallLogSelect.EntityReference) ? q.EntityReferenceId : default(long),
                EntityId = filter.Selects.Contains(CallLogSelect.Entity) ? q.EntityId : default(long),
                CallTypeId = filter.Selects.Contains(CallLogSelect.CallType) ? q.CallTypeId : default(long),
                CallCategoryId = filter.Selects.Contains(CallLogSelect.CallCategory) ? q.CallCategoryId : default(long?),
                CallEmotionId = filter.Selects.Contains(CallLogSelect.CallEmotion) ? q.CallEmotionId : default(long?),
                CallStatusId = filter.Selects.Contains(CallLogSelect.CallStatus) ? q.CallStatusId : default(long?),
                AppUserId = filter.Selects.Contains(CallLogSelect.AppUser) ? q.AppUserId : default(long),
                CreatorId = filter.Selects.Contains(CallLogSelect.Creator) ? q.CreatorId : default(long),
                AppUser = filter.Selects.Contains(CallLogSelect.AppUser) && q.AppUser != null ? new AppUser
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
                    RowId = q.AppUser.RowId,
                    Used = q.AppUser.Used,
                } : null,
                CallCategory = filter.Selects.Contains(CallLogSelect.CallCategory) && q.CallCategory != null ? new CallCategory
                {
                    Id = q.CallCategory.Id,
                    Code = q.CallCategory.Code,
                    Name = q.CallCategory.Name,
                } : null,
                CallEmotion = filter.Selects.Contains(CallLogSelect.CallEmotion) && q.CallEmotion != null ? new CallEmotion
                {
                    Id = q.CallEmotion.Id,
                    Code = q.CallEmotion.Code,
                    Name = q.CallEmotion.Name,
                    StatusId = q.CallEmotion.StatusId,
                    Description = q.CallEmotion.Description,
                    Used = q.CallEmotion.Used,
                    RowId = q.CallEmotion.RowId,
                } : null,
                CallStatus = filter.Selects.Contains(CallLogSelect.CallStatus) && q.CallStatus != null ? new CallStatus
                {
                    Id = q.CallStatus.Id,
                    Code = q.CallStatus.Code,
                    Name = q.CallStatus.Name,
                } : null,
                CallType = filter.Selects.Contains(CallLogSelect.CallType) && q.CallType != null ? new CallType
                {
                    Id = q.CallType.Id,
                    Code = q.CallType.Code,
                    Name = q.CallType.Name,
                    ColorCode = q.CallType.ColorCode,
                    StatusId = q.CallType.StatusId,
                    Used = q.CallType.Used,
                } : null,
                Creator = filter.Selects.Contains(CallLogSelect.Creator) && q.Creator != null ? new AppUser
                {
                    Id = q.Creator.Id,
                    Username = q.Creator.Username,
                    DisplayName = q.Creator.DisplayName,
                    Address = q.Creator.Address,
                    Email = q.Creator.Email,
                    Phone = q.Creator.Phone,
                    SexId = q.Creator.SexId,
                    Birthday = q.Creator.Birthday,
                    Avatar = q.Creator.Avatar,
                    Department = q.Creator.Department,
                    OrganizationId = q.Creator.OrganizationId,
                    Longitude = q.Creator.Longitude,
                    Latitude = q.Creator.Latitude,
                    StatusId = q.Creator.StatusId,
                    RowId = q.Creator.RowId,
                    Used = q.Creator.Used,
                } : null,
                EntityReference = filter.Selects.Contains(CallLogSelect.EntityReference) && q.EntityReference != null ? new EntityReference
                {
                    Id = q.EntityReference.Id,
                    Code = q.EntityReference.Code,
                    Name = q.EntityReference.Name,
                } : null,
            }).ToListAsync();
            return CallLogs;
        }

        public async Task<int> Count(CallLogFilter filter)
        {
            IQueryable<CallLogDAO> CallLogs = DataContext.CallLog.AsNoTracking();
            CallLogs = DynamicFilter(CallLogs, filter);
            return await CallLogs.CountAsync();
        }

        public async Task<List<CallLog>> List(CallLogFilter filter)
        {
            if (filter == null) return new List<CallLog>();
            IQueryable<CallLogDAO> CallLogDAOs = DataContext.CallLog.AsNoTracking();
            CallLogDAOs = DynamicFilter(CallLogDAOs, filter);
            CallLogDAOs = DynamicOrder(CallLogDAOs, filter);
            List<CallLog> CallLogs = await DynamicSelect(CallLogDAOs, filter);
            return CallLogs;
        }

        public async Task<List<CallLog>> List(List<long> Ids)
        {
            List<CallLog> CallLogs = await DataContext.CallLog.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CallLog()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Phone = x.Phone,
                CallTime = x.CallTime,
                EntityReferenceId = x.EntityReferenceId,
                EntityId = x.EntityId,
                CallTypeId = x.CallTypeId,
                CallCategoryId = x.CallCategoryId,
                CallEmotionId = x.CallEmotionId,
                CallStatusId = x.CallStatusId,
                AppUserId = x.AppUserId,
                CreatorId = x.CreatorId,
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
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                CallCategory = x.CallCategory == null ? null : new CallCategory
                {
                    Id = x.CallCategory.Id,
                    Code = x.CallCategory.Code,
                    Name = x.CallCategory.Name,
                },
                CallEmotion = x.CallEmotion == null ? null : new CallEmotion
                {
                    Id = x.CallEmotion.Id,
                    Code = x.CallEmotion.Code,
                    Name = x.CallEmotion.Name,
                    StatusId = x.CallEmotion.StatusId,
                    Description = x.CallEmotion.Description,
                    Used = x.CallEmotion.Used,
                    RowId = x.CallEmotion.RowId,
                },
                CallStatus = x.CallStatus == null ? null : new CallStatus
                {
                    Id = x.CallStatus.Id,
                    Code = x.CallStatus.Code,
                    Name = x.CallStatus.Name,
                },
                CallType = x.CallType == null ? null : new CallType
                {
                    Id = x.CallType.Id,
                    Code = x.CallType.Code,
                    Name = x.CallType.Name,
                    ColorCode = x.CallType.ColorCode,
                    StatusId = x.CallType.StatusId,
                    Used = x.CallType.Used,
                },
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
                EntityReference = x.EntityReference == null ? null : new EntityReference
                {
                    Id = x.EntityReference.Id,
                    Code = x.EntityReference.Code,
                    Name = x.EntityReference.Name,
                },
            }).ToListAsync();


            return CallLogs;
        }

        public async Task<CallLog> Get(long Id)
        {
            CallLog CallLog = await DataContext.CallLog.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CallLog()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Phone = x.Phone,
                CallTime = x.CallTime,
                EntityReferenceId = x.EntityReferenceId,
                EntityId = x.EntityId,
                CallTypeId = x.CallTypeId,
                CallCategoryId = x.CallCategoryId,
                CallEmotionId = x.CallEmotionId,
                CallStatusId = x.CallStatusId,
                AppUserId = x.AppUserId,
                CreatorId = x.CreatorId,
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
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                CallCategory = x.CallCategory == null ? null : new CallCategory
                {
                    Id = x.CallCategory.Id,
                    Code = x.CallCategory.Code,
                    Name = x.CallCategory.Name,
                },
                CallEmotion = x.CallEmotion == null ? null : new CallEmotion
                {
                    Id = x.CallEmotion.Id,
                    Code = x.CallEmotion.Code,
                    Name = x.CallEmotion.Name,
                    StatusId = x.CallEmotion.StatusId,
                    Description = x.CallEmotion.Description,
                    Used = x.CallEmotion.Used,
                    RowId = x.CallEmotion.RowId,
                },
                CallStatus = x.CallStatus == null ? null : new CallStatus
                {
                    Id = x.CallStatus.Id,
                    Code = x.CallStatus.Code,
                    Name = x.CallStatus.Name,
                },
                CallType = x.CallType == null ? null : new CallType
                {
                    Id = x.CallType.Id,
                    Code = x.CallType.Code,
                    Name = x.CallType.Name,
                    ColorCode = x.CallType.ColorCode,
                    StatusId = x.CallType.StatusId,
                    Used = x.CallType.Used,
                },
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
                EntityReference = x.EntityReference == null ? null : new EntityReference
                {
                    Id = x.EntityReference.Id,
                    Code = x.EntityReference.Code,
                    Name = x.EntityReference.Name,
                },
            }).FirstOrDefaultAsync();

            if (CallLog == null)
                return null;

            return CallLog;
        }
        public async Task<bool> Create(CallLog CallLog)
        {
            CallLogDAO CallLogDAO = new CallLogDAO();
            CallLogDAO.Id = CallLog.Id;
            CallLogDAO.Title = CallLog.Title;
            CallLogDAO.Content = CallLog.Content;
            CallLogDAO.Phone = CallLog.Phone;
            CallLogDAO.CallTime = CallLog.CallTime;
            CallLogDAO.EntityReferenceId = CallLog.EntityReferenceId;
            CallLogDAO.EntityId = CallLog.EntityId;
            CallLogDAO.CallTypeId = CallLog.CallTypeId;
            CallLogDAO.CallCategoryId = CallLog.CallCategoryId;
            CallLogDAO.CallEmotionId = CallLog.CallEmotionId;
            CallLogDAO.CallStatusId = CallLog.CallStatusId;
            CallLogDAO.AppUserId = CallLog.AppUserId;
            CallLogDAO.CreatorId = CallLog.CreatorId;
            CallLogDAO.CreatedAt = StaticParams.DateTimeNow;
            CallLogDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CallLog.Add(CallLogDAO);
            await DataContext.SaveChangesAsync();
            CallLog.Id = CallLogDAO.Id;
            await SaveReference(CallLog);
            return true;
        }

        public async Task<bool> Update(CallLog CallLog)
        {
            CallLogDAO CallLogDAO = DataContext.CallLog.Where(x => x.Id == CallLog.Id).FirstOrDefault();
            if (CallLogDAO == null)
                return false;
            CallLogDAO.Id = CallLog.Id;
            CallLogDAO.Title = CallLog.Title;
            CallLogDAO.Content = CallLog.Content;
            CallLogDAO.Phone = CallLog.Phone;
            CallLogDAO.CallTime = CallLog.CallTime;
            CallLogDAO.EntityReferenceId = CallLog.EntityReferenceId;
            CallLogDAO.EntityId = CallLog.EntityId;
            CallLogDAO.CallTypeId = CallLog.CallTypeId;
            CallLogDAO.CallCategoryId = CallLog.CallCategoryId;
            CallLogDAO.CallEmotionId = CallLog.CallEmotionId;
            CallLogDAO.CallStatusId = CallLog.CallStatusId;
            CallLogDAO.AppUserId = CallLog.AppUserId;
            CallLogDAO.CreatorId = CallLog.CreatorId;
            CallLogDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CallLog);
            return true;
        }

        public async Task<bool> Delete(CallLog CallLog)
        {
            await DataContext.CallLog.Where(x => x.Id == CallLog.Id).UpdateFromQueryAsync(x => new CallLogDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<CallLog> CallLogs)
        {
            List<CallLogDAO> CallLogDAOs = new List<CallLogDAO>();
            foreach (CallLog CallLog in CallLogs)
            {
                CallLogDAO CallLogDAO = new CallLogDAO();
                CallLogDAO.Id = CallLog.Id;
                CallLogDAO.Title = CallLog.Title;
                CallLogDAO.Content = CallLog.Content;
                CallLogDAO.Phone = CallLog.Phone;
                CallLogDAO.CallTime = CallLog.CallTime;
                CallLogDAO.EntityReferenceId = CallLog.EntityReferenceId;
                CallLogDAO.EntityId = CallLog.EntityId;
                CallLogDAO.CallTypeId = CallLog.CallTypeId;
                CallLogDAO.CallCategoryId = CallLog.CallCategoryId;
                CallLogDAO.CallEmotionId = CallLog.CallEmotionId;
                CallLogDAO.CallStatusId = CallLog.CallStatusId;
                CallLogDAO.AppUserId = CallLog.AppUserId;
                CallLogDAO.CreatorId = CallLog.CreatorId;
                CallLogDAO.CreatedAt = StaticParams.DateTimeNow;
                CallLogDAO.UpdatedAt = StaticParams.DateTimeNow;
                CallLogDAOs.Add(CallLogDAO);
            }
            await DataContext.BulkMergeAsync(CallLogDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CallLog> CallLogs)
        {
            List<long> Ids = CallLogs.Select(x => x.Id).ToList();
            await DataContext.CallLog
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CallLogDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CallLog CallLog)
        {
        }

    }
}
