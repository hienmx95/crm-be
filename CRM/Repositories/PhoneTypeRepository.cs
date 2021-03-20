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
    public interface IPhoneTypeRepository
    {
        Task<int> Count(PhoneTypeFilter PhoneTypeFilter);
        Task<List<PhoneType>> List(PhoneTypeFilter PhoneTypeFilter);
        Task<List<PhoneType>> List(List<long> Ids);
        Task<PhoneType> Get(long Id);
        Task<bool> Create(PhoneType PhoneType);
        Task<bool> Update(PhoneType PhoneType);
        Task<bool> Delete(PhoneType PhoneType);
        Task<bool> BulkMerge(List<PhoneType> PhoneTypes);
        Task<bool> BulkDelete(List<PhoneType> PhoneTypes);
    }
    public class PhoneTypeRepository : IPhoneTypeRepository
    {
        private DataContext DataContext;
        public PhoneTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<PhoneTypeDAO> DynamicFilter(IQueryable<PhoneTypeDAO> query, PhoneTypeFilter filter)
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
            if (filter.RowId != null && filter.RowId.HasValue)
                query = query.Where(q => q.RowId, filter.RowId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<PhoneTypeDAO> OrFilter(IQueryable<PhoneTypeDAO> query, PhoneTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<PhoneTypeDAO> initQuery = query.Where(q => false);
            foreach (PhoneTypeFilter PhoneTypeFilter in filter.OrFilter)
            {
                IQueryable<PhoneTypeDAO> queryable = query;
                if (PhoneTypeFilter.Id != null && PhoneTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, PhoneTypeFilter.Id);
                if (PhoneTypeFilter.Code != null && PhoneTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, PhoneTypeFilter.Code);
                if (PhoneTypeFilter.Name != null && PhoneTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, PhoneTypeFilter.Name);
                if (PhoneTypeFilter.StatusId != null && PhoneTypeFilter.StatusId.HasValue)
                    queryable = queryable.Where(q => q.StatusId, PhoneTypeFilter.StatusId);
                if (PhoneTypeFilter.RowId != null && PhoneTypeFilter.RowId.HasValue)
                    queryable = queryable.Where(q => q.RowId, PhoneTypeFilter.RowId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<PhoneTypeDAO> DynamicOrder(IQueryable<PhoneTypeDAO> query, PhoneTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case PhoneTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case PhoneTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case PhoneTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case PhoneTypeOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case PhoneTypeOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case PhoneTypeOrder.Row:
                            query = query.OrderBy(q => q.RowId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case PhoneTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case PhoneTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case PhoneTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case PhoneTypeOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case PhoneTypeOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case PhoneTypeOrder.Row:
                            query = query.OrderByDescending(q => q.RowId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<PhoneType>> DynamicSelect(IQueryable<PhoneTypeDAO> query, PhoneTypeFilter filter)
        {
            List<PhoneType> PhoneTypes = await query.Select(q => new PhoneType()
            {
                Id = filter.Selects.Contains(PhoneTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(PhoneTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(PhoneTypeSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(PhoneTypeSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(PhoneTypeSelect.Used) ? q.Used : default(bool),
                RowId = filter.Selects.Contains(PhoneTypeSelect.Row) ? q.RowId : default(Guid),
                Status = filter.Selects.Contains(PhoneTypeSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();
            return PhoneTypes;
        }

        public async Task<int> Count(PhoneTypeFilter filter)
        {
            IQueryable<PhoneTypeDAO> PhoneTypes = DataContext.PhoneType.AsNoTracking();
            PhoneTypes = DynamicFilter(PhoneTypes, filter);
            return await PhoneTypes.CountAsync();
        }

        public async Task<List<PhoneType>> List(PhoneTypeFilter filter)
        {
            if (filter == null) return new List<PhoneType>();
            IQueryable<PhoneTypeDAO> PhoneTypeDAOs = DataContext.PhoneType.AsNoTracking();
            PhoneTypeDAOs = DynamicFilter(PhoneTypeDAOs, filter);
            PhoneTypeDAOs = DynamicOrder(PhoneTypeDAOs, filter);
            List<PhoneType> PhoneTypes = await DynamicSelect(PhoneTypeDAOs, filter);
            return PhoneTypes;
        }

        public async Task<List<PhoneType>> List(List<long> Ids)
        {
            List<PhoneType> PhoneTypes = await DataContext.PhoneType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new PhoneType()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                Used = x.Used,
                RowId = x.RowId,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();
            

            return PhoneTypes;
        }

        public async Task<PhoneType> Get(long Id)
        {
            PhoneType PhoneType = await DataContext.PhoneType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new PhoneType()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                Used = x.Used,
                RowId = x.RowId,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (PhoneType == null)
                return null;

            return PhoneType;
        }
        public async Task<bool> Create(PhoneType PhoneType)
        {
            PhoneTypeDAO PhoneTypeDAO = new PhoneTypeDAO();
            PhoneTypeDAO.Id = PhoneType.Id;
            PhoneTypeDAO.Code = PhoneType.Code;
            PhoneTypeDAO.Name = PhoneType.Name;
            PhoneTypeDAO.StatusId = PhoneType.StatusId;
            PhoneTypeDAO.Used = PhoneType.Used;
            PhoneTypeDAO.RowId = PhoneType.RowId;
            PhoneTypeDAO.CreatedAt = StaticParams.DateTimeNow;
            PhoneTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.PhoneType.Add(PhoneTypeDAO);
            await DataContext.SaveChangesAsync();
            PhoneType.Id = PhoneTypeDAO.Id;
            await SaveReference(PhoneType);
            return true;
        }

        public async Task<bool> Update(PhoneType PhoneType)
        {
            PhoneTypeDAO PhoneTypeDAO = DataContext.PhoneType.Where(x => x.Id == PhoneType.Id).FirstOrDefault();
            if (PhoneTypeDAO == null)
                return false;
            PhoneTypeDAO.Id = PhoneType.Id;
            PhoneTypeDAO.Code = PhoneType.Code;
            PhoneTypeDAO.Name = PhoneType.Name;
            PhoneTypeDAO.StatusId = PhoneType.StatusId;
            PhoneTypeDAO.Used = PhoneType.Used;
            PhoneTypeDAO.RowId = PhoneType.RowId;
            PhoneTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(PhoneType);
            return true;
        }

        public async Task<bool> Delete(PhoneType PhoneType)
        {
            await DataContext.PhoneType.Where(x => x.Id == PhoneType.Id).UpdateFromQueryAsync(x => new PhoneTypeDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<PhoneType> PhoneTypes)
        {
            List<PhoneTypeDAO> PhoneTypeDAOs = new List<PhoneTypeDAO>();
            foreach (PhoneType PhoneType in PhoneTypes)
            {
                PhoneTypeDAO PhoneTypeDAO = new PhoneTypeDAO();
                PhoneTypeDAO.Id = PhoneType.Id;
                PhoneTypeDAO.Code = PhoneType.Code;
                PhoneTypeDAO.Name = PhoneType.Name;
                PhoneTypeDAO.StatusId = PhoneType.StatusId;
                PhoneTypeDAO.Used = PhoneType.Used;
                PhoneTypeDAO.RowId = PhoneType.RowId;
                PhoneTypeDAO.CreatedAt = StaticParams.DateTimeNow;
                PhoneTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
                PhoneTypeDAOs.Add(PhoneTypeDAO);
            }
            await DataContext.BulkMergeAsync(PhoneTypeDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<PhoneType> PhoneTypes)
        {
            List<long> Ids = PhoneTypes.Select(x => x.Id).ToList();
            await DataContext.PhoneType
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new PhoneTypeDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(PhoneType PhoneType)
        {
        }
        
    }
}
