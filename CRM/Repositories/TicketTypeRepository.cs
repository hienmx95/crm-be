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
    public interface ITicketTypeRepository
    {
        Task<int> Count(TicketTypeFilter TicketTypeFilter);
        Task<List<TicketType>> List(TicketTypeFilter TicketTypeFilter);
        Task<TicketType> Get(long Id);
        Task<bool> Create(TicketType TicketType);
        Task<bool> Update(TicketType TicketType);
        Task<bool> Delete(TicketType TicketType);
        Task<bool> BulkMerge(List<TicketType> TicketTypes);
        Task<bool> BulkDelete(List<TicketType> TicketTypes);
    }
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private DataContext DataContext;
        public TicketTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketTypeDAO> DynamicFilter(IQueryable<TicketTypeDAO> query, TicketTypeFilter filter)
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

         private IQueryable<TicketTypeDAO> OrFilter(IQueryable<TicketTypeDAO> query, TicketTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketTypeDAO> initQuery = query.Where(q => false);
            foreach (TicketTypeFilter TicketTypeFilter in filter.OrFilter)
            {
                IQueryable<TicketTypeDAO> queryable = query;
                if (filter.Id != null)
                    queryable = queryable.Where(q => q.Id, filter.Id);
                if (filter.Code != null)
                    queryable = queryable.Where(q => q.Code, filter.Code);
                if (filter.Name != null)
                    queryable = queryable.Where(q => q.Name, filter.Name);
                if (filter.ColorCode != null)
                    queryable = queryable.Where(q => q.ColorCode, filter.ColorCode);
                if (filter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, filter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketTypeDAO> DynamicOrder(IQueryable<TicketTypeDAO> query, TicketTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case TicketTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TicketTypeOrder.ColorCode:
                            query = query.OrderBy(q => q.ColorCode);
                            break;
                        case TicketTypeOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case TicketTypeOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case TicketTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TicketTypeOrder.ColorCode:
                            query = query.OrderByDescending(q => q.ColorCode);
                            break;
                        case TicketTypeOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case TicketTypeOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketType>> DynamicSelect(IQueryable<TicketTypeDAO> query, TicketTypeFilter filter)
        {
            List<TicketType> TicketTypes = await query.Select(q => new TicketType()
            {
                Id = filter.Selects.Contains(TicketTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(TicketTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(TicketTypeSelect.Name) ? q.Name : default(string),
                ColorCode = filter.Selects.Contains(TicketTypeSelect.ColorCode) ? q.ColorCode : default(string),
                StatusId = filter.Selects.Contains(TicketTypeSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(TicketTypeSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(TicketTypeSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketTypes;
        }

        public async Task<int> Count(TicketTypeFilter filter)
        {
            IQueryable<TicketTypeDAO> TicketTypes = DataContext.TicketType.AsNoTracking();
            TicketTypes = DynamicFilter(TicketTypes, filter);
            return await TicketTypes.CountAsync();
        }

        public async Task<List<TicketType>> List(TicketTypeFilter filter)
        {
            if (filter == null) return new List<TicketType>();
            IQueryable<TicketTypeDAO> TicketTypeDAOs = DataContext.TicketType.AsNoTracking();
            TicketTypeDAOs = DynamicFilter(TicketTypeDAOs, filter);
            TicketTypeDAOs = DynamicOrder(TicketTypeDAOs, filter);
            List<TicketType> TicketTypes = await DynamicSelect(TicketTypeDAOs, filter);
            return TicketTypes;
        }

        public async Task<TicketType> Get(long Id)
        {
            TicketType TicketType = await DataContext.TicketType.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketType()
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

            if (TicketType == null)
                return null;

            return TicketType;
        }
        public async Task<bool> Create(TicketType TicketType)
        {
            TicketTypeDAO TicketTypeDAO = new TicketTypeDAO();
            TicketTypeDAO.Id = TicketType.Id;
            TicketTypeDAO.Code = TicketType.Code;
            TicketTypeDAO.Name = TicketType.Name;
            TicketTypeDAO.ColorCode = TicketType.ColorCode;
            TicketTypeDAO.StatusId = TicketType.StatusId;
            TicketTypeDAO.Used = TicketType.Used;
            TicketTypeDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketType.Add(TicketTypeDAO);
            await DataContext.SaveChangesAsync();
            TicketType.Id = TicketTypeDAO.Id;
            await SaveReference(TicketType);
            return true;
        }

        public async Task<bool> Update(TicketType TicketType)
        {
            TicketTypeDAO TicketTypeDAO = DataContext.TicketType.Where(x => x.Id == TicketType.Id).FirstOrDefault();
            if (TicketTypeDAO == null)
                return false;
            TicketTypeDAO.Id = TicketType.Id;
            TicketTypeDAO.Code = TicketType.Code;
            TicketTypeDAO.Name = TicketType.Name;
            TicketTypeDAO.ColorCode = TicketType.ColorCode;
            TicketTypeDAO.StatusId = TicketType.StatusId;
            TicketTypeDAO.Used = TicketType.Used;
            TicketTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(TicketType);
            return true;
        }

        public async Task<bool> Delete(TicketType TicketType)
        {
            await DataContext.TicketType.Where(x => x.Id == TicketType.Id).UpdateFromQueryAsync(x => new TicketTypeDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<TicketType> TicketTypes)
        {
            List<TicketTypeDAO> TicketTypeDAOs = new List<TicketTypeDAO>();
            foreach (TicketType TicketType in TicketTypes)
            {
                TicketTypeDAO TicketTypeDAO = new TicketTypeDAO();
                TicketTypeDAO.Id = TicketType.Id;
                TicketTypeDAO.Code = TicketType.Code;
                TicketTypeDAO.Name = TicketType.Name;
                TicketTypeDAO.ColorCode = TicketType.ColorCode;
                TicketTypeDAO.StatusId = TicketType.StatusId;
                TicketTypeDAO.Used = TicketType.Used;
                TicketTypeDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketTypeDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketTypeDAOs.Add(TicketTypeDAO);
            }
            await DataContext.BulkMergeAsync(TicketTypeDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<TicketType> TicketTypes)
        {
            List<long> Ids = TicketTypes.Select(x => x.Id).ToList();
            await DataContext.TicketType
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketTypeDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(TicketType TicketType)
        {
        }
        
    }
}
