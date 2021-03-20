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
    public interface IEntityReferenceRepository
    {
        Task<int> Count(EntityReferenceFilter EntityReferenceFilter);
        Task<List<EntityReference>> List(EntityReferenceFilter EntityReferenceFilter);
        Task<List<EntityReference>> List(List<long> Ids);
        Task<EntityReference> Get(long Id);
    }
    public class EntityReferenceRepository : IEntityReferenceRepository
    {
        private DataContext DataContext;
        public EntityReferenceRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<EntityReferenceDAO> DynamicFilter(IQueryable<EntityReferenceDAO> query, EntityReferenceFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<EntityReferenceDAO> OrFilter(IQueryable<EntityReferenceDAO> query, EntityReferenceFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<EntityReferenceDAO> initQuery = query.Where(q => false);
            foreach (EntityReferenceFilter EntityReferenceFilter in filter.OrFilter)
            {
                IQueryable<EntityReferenceDAO> queryable = query;
                if (EntityReferenceFilter.Id != null && EntityReferenceFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, EntityReferenceFilter.Id);
                if (EntityReferenceFilter.Code != null && EntityReferenceFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, EntityReferenceFilter.Code);
                if (EntityReferenceFilter.Name != null && EntityReferenceFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, EntityReferenceFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<EntityReferenceDAO> DynamicOrder(IQueryable<EntityReferenceDAO> query, EntityReferenceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case EntityReferenceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case EntityReferenceOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case EntityReferenceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case EntityReferenceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case EntityReferenceOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case EntityReferenceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<EntityReference>> DynamicSelect(IQueryable<EntityReferenceDAO> query, EntityReferenceFilter filter)
        {
            List<EntityReference> EntityReferences = await query.Select(q => new EntityReference()
            {
                Id = filter.Selects.Contains(EntityReferenceSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(EntityReferenceSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(EntityReferenceSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return EntityReferences;
        }

        public async Task<int> Count(EntityReferenceFilter filter)
        {
            IQueryable<EntityReferenceDAO> EntityReferences = DataContext.EntityReference.AsNoTracking();
            EntityReferences = DynamicFilter(EntityReferences, filter);
            return await EntityReferences.CountAsync();
        }

        public async Task<List<EntityReference>> List(EntityReferenceFilter filter)
        {
            if (filter == null) return new List<EntityReference>();
            IQueryable<EntityReferenceDAO> EntityReferenceDAOs = DataContext.EntityReference.AsNoTracking();
            EntityReferenceDAOs = DynamicFilter(EntityReferenceDAOs, filter);
            EntityReferenceDAOs = DynamicOrder(EntityReferenceDAOs, filter);
            List<EntityReference> EntityReferences = await DynamicSelect(EntityReferenceDAOs, filter);
            return EntityReferences;
        }

        public async Task<List<EntityReference>> List(List<long> Ids)
        {
            List<EntityReference> EntityReferences = await DataContext.EntityReference.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new EntityReference()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return EntityReferences;
        }

        public async Task<EntityReference> Get(long Id)
        {
            EntityReference EntityReference = await DataContext.EntityReference.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new EntityReference()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (EntityReference == null)
                return null;

            return EntityReference;
        }
    }
}
