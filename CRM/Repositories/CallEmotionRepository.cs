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
    public interface ICallEmotionRepository
    {
        Task<int> Count(CallEmotionFilter CallEmotionFilter);
        Task<List<CallEmotion>> List(CallEmotionFilter CallEmotionFilter);
        Task<List<CallEmotion>> List(List<long> Ids);
        Task<CallEmotion> Get(long Id);
        Task<bool> Create(CallEmotion CallEmotion);
        Task<bool> Update(CallEmotion CallEmotion);
        Task<bool> Delete(CallEmotion CallEmotion);
        Task<bool> BulkMerge(List<CallEmotion> CallEmotions);
        Task<bool> BulkDelete(List<CallEmotion> CallEmotions);
    }
    public class CallEmotionRepository : ICallEmotionRepository
    {
        private DataContext DataContext;
        public CallEmotionRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CallEmotionDAO> DynamicFilter(IQueryable<CallEmotionDAO> query, CallEmotionFilter filter)
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
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.StatusId != null && filter.StatusId.HasValue)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.Description != null && filter.Description.HasValue)
                query = query.Where(q => q.Description, filter.Description);
            if (filter.RowId != null && filter.RowId.HasValue)
                query = query.Where(q => q.RowId, filter.RowId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CallEmotionDAO> OrFilter(IQueryable<CallEmotionDAO> query, CallEmotionFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CallEmotionDAO> initQuery = query.Where(q => false);
            foreach (CallEmotionFilter CallEmotionFilter in filter.OrFilter)
            {
                IQueryable<CallEmotionDAO> queryable = query;
                if (CallEmotionFilter.Id != null && CallEmotionFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CallEmotionFilter.Id);
                if (CallEmotionFilter.Code != null && CallEmotionFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CallEmotionFilter.Code);
                if (CallEmotionFilter.Name != null && CallEmotionFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CallEmotionFilter.Name);
                if (CallEmotionFilter.StatusId != null && CallEmotionFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, CallEmotionFilter.StatusId);
                if (CallEmotionFilter.Description != null && CallEmotionFilter.Description.HasValue)
                    queryable = queryable.Where(q => q.Description, CallEmotionFilter.Description);
                if (CallEmotionFilter.RowId != null && CallEmotionFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, CallEmotionFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CallEmotionDAO> DynamicOrder(IQueryable<CallEmotionDAO> query, CallEmotionFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CallEmotionOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CallEmotionOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CallEmotionOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CallEmotionOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case CallEmotionOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                        case CallEmotionOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case CallEmotionOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CallEmotionOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CallEmotionOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CallEmotionOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CallEmotionOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case CallEmotionOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                        case CallEmotionOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case CallEmotionOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CallEmotion>> DynamicSelect(IQueryable<CallEmotionDAO> query, CallEmotionFilter filter)
        {
            List<CallEmotion> CallEmotions = await query.Select(q => new CallEmotion()
            {
                Id = filter.Selects.Contains(CallEmotionSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CallEmotionSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CallEmotionSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(CallEmotionSelect.Status) ? q.StatusId : default(long),
                Description = filter.Selects.Contains(CallEmotionSelect.Description) ? q.Description : default(string),
                Used = filter.Selects.Contains(CallEmotionSelect.Used) ? q.Used : default(bool),
                RowId = filter.Selects.Contains(CallEmotionSelect.Row) ? q.RowId : default(Guid),
                Status = filter.Selects.Contains(CallEmotionSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return CallEmotions;
        }

        public async Task<int> Count(CallEmotionFilter filter)
        {
            IQueryable<CallEmotionDAO> CallEmotions = DataContext.CallEmotion.AsNoTracking();
            CallEmotions = DynamicFilter(CallEmotions, filter);
            return await CallEmotions.CountAsync();
        }

        public async Task<List<CallEmotion>> List(CallEmotionFilter filter)
        {
            if (filter == null) return new List<CallEmotion>();
            IQueryable<CallEmotionDAO> CallEmotionDAOs = DataContext.CallEmotion.AsNoTracking();
            CallEmotionDAOs = DynamicFilter(CallEmotionDAOs, filter);
            CallEmotionDAOs = DynamicOrder(CallEmotionDAOs, filter);
            List<CallEmotion> CallEmotions = await DynamicSelect(CallEmotionDAOs, filter);
            return CallEmotions;
        }

        public async Task<List<CallEmotion>> List(List<long> Ids)
        {
            List<CallEmotion> CallEmotions = await DataContext.CallEmotion.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CallEmotion()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                Description = x.Description,
                Used = x.Used,
                RowId = x.RowId,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();
            

            return CallEmotions;
        }

        public async Task<CallEmotion> Get(long Id)
        {
            CallEmotion CallEmotion = await DataContext.CallEmotion.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new CallEmotion()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                Description = x.Description,
                Used = x.Used,
                RowId = x.RowId,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (CallEmotion == null)
                return null;

            return CallEmotion;
        }
        public async Task<bool> Create(CallEmotion CallEmotion)
        {
            CallEmotionDAO CallEmotionDAO = new CallEmotionDAO();
            CallEmotionDAO.Id = CallEmotion.Id;
            CallEmotionDAO.Code = CallEmotion.Code;
            CallEmotionDAO.Name = CallEmotion.Name;
            CallEmotionDAO.StatusId = CallEmotion.StatusId;
            CallEmotionDAO.Description = CallEmotion.Description;
            CallEmotionDAO.Used = CallEmotion.Used;
            CallEmotionDAO.RowId = CallEmotion.RowId;
            CallEmotionDAO.CreatedAt = StaticParams.DateTimeNow;
            CallEmotionDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CallEmotion.Add(CallEmotionDAO);
            await DataContext.SaveChangesAsync();
            CallEmotion.Id = CallEmotionDAO.Id;
            await SaveReference(CallEmotion);
            return true;
        }

        public async Task<bool> Update(CallEmotion CallEmotion)
        {
            CallEmotionDAO CallEmotionDAO = DataContext.CallEmotion.Where(x => x.Id == CallEmotion.Id).FirstOrDefault();
            if (CallEmotionDAO == null)
                return false;
            CallEmotionDAO.Id = CallEmotion.Id;
            CallEmotionDAO.Code = CallEmotion.Code;
            CallEmotionDAO.Name = CallEmotion.Name;
            CallEmotionDAO.StatusId = CallEmotion.StatusId;
            CallEmotionDAO.Description = CallEmotion.Description;
            CallEmotionDAO.Used = CallEmotion.Used;
            CallEmotionDAO.RowId = CallEmotion.RowId;
            CallEmotionDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CallEmotion);
            return true;
        }

        public async Task<bool> Delete(CallEmotion CallEmotion)
        {
            await DataContext.CallEmotion.Where(x => x.Id == CallEmotion.Id).UpdateFromQueryAsync(x => new CallEmotionDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CallEmotion> CallEmotions)
        {
            List<CallEmotionDAO> CallEmotionDAOs = new List<CallEmotionDAO>();
            foreach (CallEmotion CallEmotion in CallEmotions)
            {
                CallEmotionDAO CallEmotionDAO = new CallEmotionDAO();
                CallEmotionDAO.Id = CallEmotion.Id;
                CallEmotionDAO.Code = CallEmotion.Code;
                CallEmotionDAO.Name = CallEmotion.Name;
                CallEmotionDAO.StatusId = CallEmotion.StatusId;
                CallEmotionDAO.Description = CallEmotion.Description;
                CallEmotionDAO.Used = CallEmotion.Used;
                CallEmotionDAO.RowId = CallEmotion.RowId;
                CallEmotionDAO.CreatedAt = StaticParams.DateTimeNow;
                CallEmotionDAO.UpdatedAt = StaticParams.DateTimeNow;
                CallEmotionDAOs.Add(CallEmotionDAO);
            }
            await DataContext.BulkMergeAsync(CallEmotionDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CallEmotion> CallEmotions)
        {
            List<long> Ids = CallEmotions.Select(x => x.Id).ToList();
            await DataContext.CallEmotion
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CallEmotionDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CallEmotion CallEmotion)
        {
        }
        
    }
}
