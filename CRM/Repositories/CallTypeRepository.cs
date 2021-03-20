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
    public interface ICallTypeRepository
    {
        Task<int> Count(CallTypeFilter CallTypeFilter);
        Task<List<CallType>> List(CallTypeFilter CallTypeFilter);
        Task<CallType> Get(long Id);
        Task<bool> Create(CallType CallType);
        Task<bool> Update(CallType CallType);
        Task<bool> Delete(CallType CallType);
        Task<bool> BulkMerge(List<CallType> CallTypes);
        Task<bool> BulkDelete(List<CallType> CallTypes);
    }
    public class CallTypeRepository : ICallTypeRepository
    {
        private DataContext DataContext;
        public CallTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CallTypeDAO> DynamicFilter(IQueryable<CallTypeDAO> query, CallTypeFilter filter)
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
            if (filter.ColorCode != null)
                query = query.Where(q => q.ColorCode, filter.ColorCode);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<CallTypeDAO> OrFilter(IQueryable<CallTypeDAO> query, CallTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CallTypeDAO> initQuery = query.Where(q => false);
            foreach (CallTypeFilter CallTypeFilter in filter.OrFilter)
            {
                IQueryable<CallTypeDAO> queryable = query;
                if (CallTypeFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, CallTypeFilter.Id);
                if (CallTypeFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, CallTypeFilter.Code);
                if (CallTypeFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, CallTypeFilter.Name);
                if (CallTypeFilter.ColorCode != null)
                    queryable = queryable.Where(q => q.ColorCode, CallTypeFilter.ColorCode);
                if (CallTypeFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, CallTypeFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CallTypeDAO> DynamicOrder(IQueryable<CallTypeDAO> query, CallTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CallTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CallTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CallTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case CallTypeOrder.ColorCode:
                            query = query.OrderBy(q => q.ColorCode);
                            break;
                        case CallTypeOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case CallTypeOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CallTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CallTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CallTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case CallTypeOrder.ColorCode:
                            query = query.OrderByDescending(q => q.ColorCode);
                            break;
                        case CallTypeOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case CallTypeOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CallType>> DynamicSelect(IQueryable<CallTypeDAO> query, CallTypeFilter filter)
        {
            List<CallType> CallTypes = await query.Select(q => new CallType()
            {
                Id = filter.Selects.Contains(CallTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CallTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CallTypeSelect.Name) ? q.Name : default(string),
                ColorCode = filter.Selects.Contains(CallTypeSelect.ColorCode) ? q.ColorCode : default(string),
                StatusId = filter.Selects.Contains(CallTypeSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(CallTypeSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(CallTypeSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return CallTypes;
        }

        public async Task<int> Count(CallTypeFilter filter)
        {
            IQueryable<CallTypeDAO> CallTypes = DataContext.CallType.AsNoTracking();
            CallTypes = DynamicFilter(CallTypes, filter);
            return await CallTypes.CountAsync();
        }

        public async Task<List<CallType>> List(CallTypeFilter filter)
        {
            if (filter == null) return new List<CallType>();
            IQueryable<CallTypeDAO> CallTypeDAOs = DataContext.CallType.AsNoTracking();
            CallTypeDAOs = DynamicFilter(CallTypeDAOs, filter);
            CallTypeDAOs = DynamicOrder(CallTypeDAOs, filter);
            List<CallType> CallTypes = await DynamicSelect(CallTypeDAOs, filter);
            return CallTypes;
        }

        public async Task<CallType> Get(long Id)
        {
            CallType CallType = await DataContext.CallType.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new CallType()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
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

            if (CallType == null)
                return null;

            return CallType;
        }
        public async Task<bool> Create(CallType CallType)
        {
            CallTypeDAO CallTypeDAO = new CallTypeDAO();
            CallTypeDAO.Id = CallType.Id;
            CallTypeDAO.Code = CallType.Code;
            CallTypeDAO.Name = CallType.Name;
            CallTypeDAO.ColorCode = CallType.ColorCode;
            CallTypeDAO.StatusId = CallType.StatusId;
            CallTypeDAO.Used = CallType.Used;
            CallTypeDAO.CreatedAt = StaticParams.DateTimeNow;
            CallTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.CallType.Add(CallTypeDAO);
            await DataContext.SaveChangesAsync();
            CallType.Id = CallTypeDAO.Id;
            await SaveReference(CallType);
            return true;
        }

        public async Task<bool> Update(CallType CallType)
        {
            CallTypeDAO CallTypeDAO = DataContext.CallType.Where(x => x.Id == CallType.Id).FirstOrDefault();
            if (CallTypeDAO == null)
                return false;
            CallTypeDAO.Id = CallType.Id;
            CallTypeDAO.Code = CallType.Code;
            CallTypeDAO.Name = CallType.Name;
            CallTypeDAO.ColorCode = CallType.ColorCode;
            CallTypeDAO.StatusId = CallType.StatusId;
            CallTypeDAO.Used = CallType.Used;
            CallTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(CallType);
            return true;
        }

        public async Task<bool> Delete(CallType CallType)
        {
            await DataContext.CallType.Where(x => x.Id == CallType.Id).UpdateFromQueryAsync(x => new CallTypeDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CallType> CallTypes)
        {
            List<CallTypeDAO> CallTypeDAOs = new List<CallTypeDAO>();
            foreach (CallType CallType in CallTypes)
            {
                CallTypeDAO CallTypeDAO = new CallTypeDAO();
                CallTypeDAO.Id = CallType.Id;
                CallTypeDAO.Code = CallType.Code;
                CallTypeDAO.Name = CallType.Name;
                CallTypeDAO.ColorCode = CallType.ColorCode;
                CallTypeDAO.StatusId = CallType.StatusId;
                CallTypeDAO.Used = CallType.Used;
                CallTypeDAO.CreatedAt = StaticParams.DateTimeNow;
                CallTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
                CallTypeDAOs.Add(CallTypeDAO);
            }
            await DataContext.BulkMergeAsync(CallTypeDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CallType> CallTypes)
        {
            List<long> Ids = CallTypes.Select(x => x.Id).ToList();
            await DataContext.CallType
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new CallTypeDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(CallType CallType)
        {
        }
        
    }
}
