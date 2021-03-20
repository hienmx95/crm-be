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
    public interface ITicketGroupRepository
    {
        Task<int> Count(TicketGroupFilter TicketGroupFilter);
        Task<List<TicketGroup>> List(TicketGroupFilter TicketGroupFilter);
        Task<TicketGroup> Get(long Id);
        Task<bool> Create(TicketGroup TicketGroup);
        Task<bool> Update(TicketGroup TicketGroup);
        Task<bool> Delete(TicketGroup TicketGroup);
        Task<bool> BulkMerge(List<TicketGroup> TicketGroups);
        Task<bool> BulkDelete(List<TicketGroup> TicketGroups);
    }
    public class TicketGroupRepository : ITicketGroupRepository
    {
        private DataContext DataContext;
        public TicketGroupRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketGroupDAO> DynamicFilter(IQueryable<TicketGroupDAO> query, TicketGroupFilter filter)
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
            if (filter.TicketTypeId != null)
                query = query.Where(q => q.TicketTypeId, filter.TicketTypeId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<TicketGroupDAO> OrFilter(IQueryable<TicketGroupDAO> query, TicketGroupFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketGroupDAO> initQuery = query.Where(q => false);
            foreach (TicketGroupFilter TicketGroupFilter in filter.OrFilter)
            {
                IQueryable<TicketGroupDAO> queryable = query;
                if (TicketGroupFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TicketGroupFilter.Id);
                if (TicketGroupFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, TicketGroupFilter.Name);
                if (TicketGroupFilter.OrderNumber != null)
                    queryable = queryable.Where(q => q.OrderNumber, TicketGroupFilter.OrderNumber);
                if (TicketGroupFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, TicketGroupFilter.StatusId);
                if (TicketGroupFilter.TicketTypeId != null)
                    queryable = queryable.Where(q => q.TicketTypeId, TicketGroupFilter.TicketTypeId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketGroupDAO> DynamicOrder(IQueryable<TicketGroupDAO> query, TicketGroupFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketGroupOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketGroupOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TicketGroupOrder.OrderNumber:
                            query = query.OrderBy(q => q.OrderNumber);
                            break;
                        case TicketGroupOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case TicketGroupOrder.TicketType:
                            query = query.OrderBy(q => q.TicketTypeId);
                            break;
                        case TicketGroupOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketGroupOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketGroupOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TicketGroupOrder.OrderNumber:
                            query = query.OrderByDescending(q => q.OrderNumber);
                            break;
                        case TicketGroupOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case TicketGroupOrder.TicketType:
                            query = query.OrderByDescending(q => q.TicketTypeId);
                            break;
                        case TicketGroupOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketGroup>> DynamicSelect(IQueryable<TicketGroupDAO> query, TicketGroupFilter filter)
        {
            List<TicketGroup> TicketGroups = await query.Select(q => new TicketGroup()
            {
                Id = filter.Selects.Contains(TicketGroupSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(TicketGroupSelect.Name) ? q.Name : default(string),
                OrderNumber = filter.Selects.Contains(TicketGroupSelect.OrderNumber) ? q.OrderNumber : default(long),
                StatusId = filter.Selects.Contains(TicketGroupSelect.Status) ? q.StatusId : default(long),
                TicketTypeId = filter.Selects.Contains(TicketGroupSelect.TicketType) ? q.TicketTypeId : default(long),
                Used = filter.Selects.Contains(TicketGroupSelect.Used) ? q.Used : default(bool),
                Status = filter.Selects.Contains(TicketGroupSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                TicketType = filter.Selects.Contains(TicketGroupSelect.TicketType) && q.TicketType != null ? new TicketType
                {
                    Id = q.TicketType.Id,
                    Code = q.TicketType.Code,
                    Name = q.TicketType.Name,
                    ColorCode = q.TicketType.ColorCode,
                    StatusId = q.TicketType.StatusId,
                    Used = q.TicketType.Used,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketGroups;
        }

        public async Task<int> Count(TicketGroupFilter filter)
        {
            IQueryable<TicketGroupDAO> TicketGroups = DataContext.TicketGroup.AsNoTracking();
            TicketGroups = DynamicFilter(TicketGroups, filter);
            return await TicketGroups.CountAsync();
        }

        public async Task<List<TicketGroup>> List(TicketGroupFilter filter)
        {
            if (filter == null) return new List<TicketGroup>();
            IQueryable<TicketGroupDAO> TicketGroupDAOs = DataContext.TicketGroup.AsNoTracking();
            TicketGroupDAOs = DynamicFilter(TicketGroupDAOs, filter);
            TicketGroupDAOs = DynamicOrder(TicketGroupDAOs, filter);
            List<TicketGroup> TicketGroups = await DynamicSelect(TicketGroupDAOs, filter);
            return TicketGroups;
        }

        public async Task<TicketGroup> Get(long Id)
        {
            TicketGroup TicketGroup = await DataContext.TicketGroup.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketGroup()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                OrderNumber = x.OrderNumber,
                StatusId = x.StatusId,
                TicketTypeId = x.TicketTypeId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                TicketType = x.TicketType == null ? null : new TicketType
                {
                    Id = x.TicketType.Id,
                    Code = x.TicketType.Code,
                    Name = x.TicketType.Name,
                    ColorCode = x.TicketType.ColorCode,
                    StatusId = x.TicketType.StatusId,
                    Used = x.TicketType.Used,
                },
            }).FirstOrDefaultAsync();

            if (TicketGroup == null)
                return null;

            return TicketGroup;
        }
        public async Task<bool> Create(TicketGroup TicketGroup)
        {
            TicketGroupDAO TicketGroupDAO = new TicketGroupDAO();
            TicketGroupDAO.Id = TicketGroup.Id;
            TicketGroupDAO.Name = TicketGroup.Name;
            TicketGroupDAO.OrderNumber = TicketGroup.OrderNumber;
            TicketGroupDAO.StatusId = TicketGroup.StatusId;
            TicketGroupDAO.TicketTypeId = TicketGroup.TicketTypeId;
            TicketGroupDAO.Used = TicketGroup.Used;
            TicketGroupDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketGroup.Add(TicketGroupDAO);
            await DataContext.SaveChangesAsync();
            TicketGroup.Id = TicketGroupDAO.Id;
            await SaveReference(TicketGroup);
            return true;
        }

        public async Task<bool> Update(TicketGroup TicketGroup)
        {
            TicketGroupDAO TicketGroupDAO = DataContext.TicketGroup.Where(x => x.Id == TicketGroup.Id).FirstOrDefault();
            if (TicketGroupDAO == null)
                return false;
            TicketGroupDAO.Id = TicketGroup.Id;
            TicketGroupDAO.Name = TicketGroup.Name;
            TicketGroupDAO.OrderNumber = TicketGroup.OrderNumber;
            TicketGroupDAO.StatusId = TicketGroup.StatusId;
            TicketGroupDAO.TicketTypeId = TicketGroup.TicketTypeId;
            TicketGroupDAO.Used = TicketGroup.Used;
            TicketGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(TicketGroup);
            return true;
        }

        public async Task<bool> Delete(TicketGroup TicketGroup)
        {
            await DataContext.TicketGroup.Where(x => x.Id == TicketGroup.Id).UpdateFromQueryAsync(x => new TicketGroupDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<TicketGroup> TicketGroups)
        {
            List<TicketGroupDAO> TicketGroupDAOs = new List<TicketGroupDAO>();
            foreach (TicketGroup TicketGroup in TicketGroups)
            {
                TicketGroupDAO TicketGroupDAO = new TicketGroupDAO();
                TicketGroupDAO.Id = TicketGroup.Id;
                TicketGroupDAO.Name = TicketGroup.Name;
                TicketGroupDAO.OrderNumber = TicketGroup.OrderNumber;
                TicketGroupDAO.StatusId = TicketGroup.StatusId;
                TicketGroupDAO.TicketTypeId = TicketGroup.TicketTypeId;
                TicketGroupDAO.Used = TicketGroup.Used;
                TicketGroupDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketGroupDAOs.Add(TicketGroupDAO);
            }
            await DataContext.BulkMergeAsync(TicketGroupDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<TicketGroup> TicketGroups)
        {
            List<long> Ids = TicketGroups.Select(x => x.Id).ToList();
            await DataContext.TicketGroup
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketGroupDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(TicketGroup TicketGroup)
        {
        }
        
    }
}
