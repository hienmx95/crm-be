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
    public interface IKnowledgeGroupRepository
    {
        Task<int> Count(KnowledgeGroupFilter KnowledgeGroupFilter);
        Task<List<KnowledgeGroup>> List(KnowledgeGroupFilter KnowledgeGroupFilter);
        Task<KnowledgeGroup> Get(long Id);
        Task<bool> Create(KnowledgeGroup KnowledgeGroup);
        Task<bool> Update(KnowledgeGroup KnowledgeGroup);
        Task<bool> Delete(KnowledgeGroup KnowledgeGroup);
        Task<bool> BulkMerge(List<KnowledgeGroup> KnowledgeGroups);
        Task<bool> BulkDelete(List<KnowledgeGroup> KnowledgeGroups);
    }
    public class KnowledgeGroupRepository : IKnowledgeGroupRepository
    {
        private DataContext DataContext;
        public KnowledgeGroupRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<KnowledgeGroupDAO> DynamicFilter(IQueryable<KnowledgeGroupDAO> query, KnowledgeGroupFilter filter)
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
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.DisplayOrder != null)
                query = query.Where(q => q.DisplayOrder, filter.DisplayOrder);
            if (filter.Description != null)
                query = query.Where(q => q.Description, filter.Description);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<KnowledgeGroupDAO> OrFilter(IQueryable<KnowledgeGroupDAO> query, KnowledgeGroupFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<KnowledgeGroupDAO> initQuery = query.Where(q => false);
            foreach (KnowledgeGroupFilter KnowledgeGroupFilter in filter.OrFilter)
            {
                IQueryable<KnowledgeGroupDAO> queryable = query;
                if (KnowledgeGroupFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, KnowledgeGroupFilter.Id);
                if (KnowledgeGroupFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, KnowledgeGroupFilter.Name);
                if (KnowledgeGroupFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, KnowledgeGroupFilter.Code);
                if (KnowledgeGroupFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId.HasValue).Where(q => q.StatusId, KnowledgeGroupFilter.StatusId);
                if (KnowledgeGroupFilter.DisplayOrder != null)
                    queryable = queryable.Where(q => q.DisplayOrder.HasValue).Where(q => q.DisplayOrder, KnowledgeGroupFilter.DisplayOrder);
                if (KnowledgeGroupFilter.Description != null)
                    queryable = queryable.Where(q => q.Description, KnowledgeGroupFilter.Description);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<KnowledgeGroupDAO> DynamicOrder(IQueryable<KnowledgeGroupDAO> query, KnowledgeGroupFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case KnowledgeGroupOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case KnowledgeGroupOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case KnowledgeGroupOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case KnowledgeGroupOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case KnowledgeGroupOrder.DisplayOrder:
                            query = query.OrderBy(q => q.DisplayOrder);
                            break;
                        case KnowledgeGroupOrder.Description:
                            query = query.OrderBy(q => q.Description);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case KnowledgeGroupOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case KnowledgeGroupOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case KnowledgeGroupOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case KnowledgeGroupOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case KnowledgeGroupOrder.DisplayOrder:
                            query = query.OrderByDescending(q => q.DisplayOrder);
                            break;
                        case KnowledgeGroupOrder.Description:
                            query = query.OrderByDescending(q => q.Description);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<KnowledgeGroup>> DynamicSelect(IQueryable<KnowledgeGroupDAO> query, KnowledgeGroupFilter filter)
        {
            List<KnowledgeGroup> KnowledgeGroups = await query.Select(q => new KnowledgeGroup()
            {
                Id = filter.Selects.Contains(KnowledgeGroupSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(KnowledgeGroupSelect.Name) ? q.Name : default(string),
                Code = filter.Selects.Contains(KnowledgeGroupSelect.Code) ? q.Code : default(string),
                StatusId = filter.Selects.Contains(KnowledgeGroupSelect.Status) ? q.StatusId : default(long?),
                DisplayOrder = filter.Selects.Contains(KnowledgeGroupSelect.DisplayOrder) ? q.DisplayOrder : default(long?),
                Description = filter.Selects.Contains(KnowledgeGroupSelect.Description) ? q.Description : default(string),
                Status = filter.Selects.Contains(KnowledgeGroupSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return KnowledgeGroups;
        }

        public async Task<int> Count(KnowledgeGroupFilter filter)
        {
            IQueryable<KnowledgeGroupDAO> KnowledgeGroups = DataContext.KnowledgeGroup.AsNoTracking();
            KnowledgeGroups = DynamicFilter(KnowledgeGroups, filter);
            return await KnowledgeGroups.CountAsync();
        }

        public async Task<List<KnowledgeGroup>> List(KnowledgeGroupFilter filter)
        {
            if (filter == null) return new List<KnowledgeGroup>();
            IQueryable<KnowledgeGroupDAO> KnowledgeGroupDAOs = DataContext.KnowledgeGroup.AsNoTracking();
            KnowledgeGroupDAOs = DynamicFilter(KnowledgeGroupDAOs, filter);
            KnowledgeGroupDAOs = DynamicOrder(KnowledgeGroupDAOs, filter);
            List<KnowledgeGroup> KnowledgeGroups = await DynamicSelect(KnowledgeGroupDAOs, filter);
            return KnowledgeGroups;
        }

        public async Task<KnowledgeGroup> Get(long Id)
        {
            KnowledgeGroup KnowledgeGroup = await DataContext.KnowledgeGroup.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new KnowledgeGroup()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                StatusId = x.StatusId,
                DisplayOrder = x.DisplayOrder,
                Description = x.Description,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (KnowledgeGroup == null)
                return null;

            return KnowledgeGroup;
        }
        public async Task<bool> Create(KnowledgeGroup KnowledgeGroup)
        {
            KnowledgeGroupDAO KnowledgeGroupDAO = new KnowledgeGroupDAO();
            KnowledgeGroupDAO.Id = KnowledgeGroup.Id;
            KnowledgeGroupDAO.Name = KnowledgeGroup.Name;
            KnowledgeGroupDAO.Code = KnowledgeGroup.Code;
            KnowledgeGroupDAO.StatusId = KnowledgeGroup.StatusId;
            KnowledgeGroupDAO.DisplayOrder = KnowledgeGroup.DisplayOrder;
            KnowledgeGroupDAO.Description = KnowledgeGroup.Description;
            KnowledgeGroupDAO.CreatedAt = StaticParams.DateTimeNow;
            KnowledgeGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.KnowledgeGroup.Add(KnowledgeGroupDAO);
            await DataContext.SaveChangesAsync();
            KnowledgeGroup.Id = KnowledgeGroupDAO.Id;
            await SaveReference(KnowledgeGroup);
            return true;
        }

        public async Task<bool> Update(KnowledgeGroup KnowledgeGroup)
        {
            KnowledgeGroupDAO KnowledgeGroupDAO = DataContext.KnowledgeGroup.Where(x => x.Id == KnowledgeGroup.Id).FirstOrDefault();
            if (KnowledgeGroupDAO == null)
                return false;
            KnowledgeGroupDAO.Id = KnowledgeGroup.Id;
            KnowledgeGroupDAO.Name = KnowledgeGroup.Name;
            KnowledgeGroupDAO.Code = KnowledgeGroup.Code;
            KnowledgeGroupDAO.StatusId = KnowledgeGroup.StatusId;
            KnowledgeGroupDAO.DisplayOrder = KnowledgeGroup.DisplayOrder;
            KnowledgeGroupDAO.Description = KnowledgeGroup.Description;
            KnowledgeGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(KnowledgeGroup);
            return true;
        }

        public async Task<bool> Delete(KnowledgeGroup KnowledgeGroup)
        {
            await DataContext.KnowledgeGroup.Where(x => x.Id == KnowledgeGroup.Id).UpdateFromQueryAsync(x => new KnowledgeGroupDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<KnowledgeGroup> KnowledgeGroups)
        {
            List<KnowledgeGroupDAO> KnowledgeGroupDAOs = new List<KnowledgeGroupDAO>();
            foreach (KnowledgeGroup KnowledgeGroup in KnowledgeGroups)
            {
                KnowledgeGroupDAO KnowledgeGroupDAO = new KnowledgeGroupDAO();
                KnowledgeGroupDAO.Id = KnowledgeGroup.Id;
                KnowledgeGroupDAO.Name = KnowledgeGroup.Name;
                KnowledgeGroupDAO.Code = KnowledgeGroup.Code;
                KnowledgeGroupDAO.StatusId = KnowledgeGroup.StatusId;
                KnowledgeGroupDAO.DisplayOrder = KnowledgeGroup.DisplayOrder;
                KnowledgeGroupDAO.Description = KnowledgeGroup.Description;
                KnowledgeGroupDAO.CreatedAt = StaticParams.DateTimeNow;
                KnowledgeGroupDAO.UpdatedAt = StaticParams.DateTimeNow;
                KnowledgeGroupDAOs.Add(KnowledgeGroupDAO);
            }
            await DataContext.BulkMergeAsync(KnowledgeGroupDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<KnowledgeGroup> KnowledgeGroups)
        {
            List<long> Ids = KnowledgeGroups.Select(x => x.Id).ToList();
            await DataContext.KnowledgeGroup
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new KnowledgeGroupDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(KnowledgeGroup KnowledgeGroup)
        {
        }

    }
}
