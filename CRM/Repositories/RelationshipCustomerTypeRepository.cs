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
    public interface IRelationshipCustomerTypeRepository
    {
        Task<int> Count(RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter);
        Task<List<RelationshipCustomerType>> List(RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter);
        Task<List<RelationshipCustomerType>> List(List<long> Ids);
        Task<RelationshipCustomerType> Get(long Id);
        Task<bool> Create(RelationshipCustomerType RelationshipCustomerType);
        Task<bool> Update(RelationshipCustomerType RelationshipCustomerType);
        Task<bool> Delete(RelationshipCustomerType RelationshipCustomerType);
        Task<bool> BulkMerge(List<RelationshipCustomerType> RelationshipCustomerTypes);
        Task<bool> BulkDelete(List<RelationshipCustomerType> RelationshipCustomerTypes);
    }
    public class RelationshipCustomerTypeRepository : IRelationshipCustomerTypeRepository
    {
        private DataContext DataContext;
        public RelationshipCustomerTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<RelationshipCustomerTypeDAO> DynamicFilter(IQueryable<RelationshipCustomerTypeDAO> query, RelationshipCustomerTypeFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<RelationshipCustomerTypeDAO> OrFilter(IQueryable<RelationshipCustomerTypeDAO> query, RelationshipCustomerTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<RelationshipCustomerTypeDAO> initQuery = query.Where(q => false);
            foreach (RelationshipCustomerTypeFilter RelationshipCustomerTypeFilter in filter.OrFilter)
            {
                IQueryable<RelationshipCustomerTypeDAO> queryable = query;
                if (RelationshipCustomerTypeFilter.Id != null && RelationshipCustomerTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, RelationshipCustomerTypeFilter.Id);
                if (RelationshipCustomerTypeFilter.Name != null && RelationshipCustomerTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, RelationshipCustomerTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<RelationshipCustomerTypeDAO> DynamicOrder(IQueryable<RelationshipCustomerTypeDAO> query, RelationshipCustomerTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case RelationshipCustomerTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case RelationshipCustomerTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case RelationshipCustomerTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case RelationshipCustomerTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<RelationshipCustomerType>> DynamicSelect(IQueryable<RelationshipCustomerTypeDAO> query, RelationshipCustomerTypeFilter filter)
        {
            List<RelationshipCustomerType> RelationshipCustomerTypes = await query.Select(q => new RelationshipCustomerType()
            {
                Id = filter.Selects.Contains(RelationshipCustomerTypeSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(RelationshipCustomerTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return RelationshipCustomerTypes;
        }

        public async Task<int> Count(RelationshipCustomerTypeFilter filter)
        {
            IQueryable<RelationshipCustomerTypeDAO> RelationshipCustomerTypes = DataContext.RelationshipCustomerType.AsNoTracking();
            RelationshipCustomerTypes = DynamicFilter(RelationshipCustomerTypes, filter);
            return await RelationshipCustomerTypes.CountAsync();
        }

        public async Task<List<RelationshipCustomerType>> List(RelationshipCustomerTypeFilter filter)
        {
            if (filter == null) return new List<RelationshipCustomerType>();
            IQueryable<RelationshipCustomerTypeDAO> RelationshipCustomerTypeDAOs = DataContext.RelationshipCustomerType.AsNoTracking();
            RelationshipCustomerTypeDAOs = DynamicFilter(RelationshipCustomerTypeDAOs, filter);
            RelationshipCustomerTypeDAOs = DynamicOrder(RelationshipCustomerTypeDAOs, filter);
            List<RelationshipCustomerType> RelationshipCustomerTypes = await DynamicSelect(RelationshipCustomerTypeDAOs, filter);
            return RelationshipCustomerTypes;
        }

        public async Task<List<RelationshipCustomerType>> List(List<long> Ids)
        {
            List<RelationshipCustomerType> RelationshipCustomerTypes = await DataContext.RelationshipCustomerType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new RelationshipCustomerType()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            

            return RelationshipCustomerTypes;
        }

        public async Task<RelationshipCustomerType> Get(long Id)
        {
            RelationshipCustomerType RelationshipCustomerType = await DataContext.RelationshipCustomerType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new RelationshipCustomerType()
            {
                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (RelationshipCustomerType == null)
                return null;

            return RelationshipCustomerType;
        }
        public async Task<bool> Create(RelationshipCustomerType RelationshipCustomerType)
        {
            RelationshipCustomerTypeDAO RelationshipCustomerTypeDAO = new RelationshipCustomerTypeDAO();
            RelationshipCustomerTypeDAO.Id = RelationshipCustomerType.Id;
            RelationshipCustomerTypeDAO.Name = RelationshipCustomerType.Name;
            DataContext.RelationshipCustomerType.Add(RelationshipCustomerTypeDAO);
            await DataContext.SaveChangesAsync();
            RelationshipCustomerType.Id = RelationshipCustomerTypeDAO.Id;
            await SaveReference(RelationshipCustomerType);
            return true;
        }

        public async Task<bool> Update(RelationshipCustomerType RelationshipCustomerType)
        {
            RelationshipCustomerTypeDAO RelationshipCustomerTypeDAO = DataContext.RelationshipCustomerType.Where(x => x.Id == RelationshipCustomerType.Id).FirstOrDefault();
            if (RelationshipCustomerTypeDAO == null)
                return false;
            RelationshipCustomerTypeDAO.Id = RelationshipCustomerType.Id;
            RelationshipCustomerTypeDAO.Name = RelationshipCustomerType.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(RelationshipCustomerType);
            return true;
        }

        public async Task<bool> Delete(RelationshipCustomerType RelationshipCustomerType)
        {
            await DataContext.RelationshipCustomerType.Where(x => x.Id == RelationshipCustomerType.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<RelationshipCustomerType> RelationshipCustomerTypes)
        {
            List<RelationshipCustomerTypeDAO> RelationshipCustomerTypeDAOs = new List<RelationshipCustomerTypeDAO>();
            foreach (RelationshipCustomerType RelationshipCustomerType in RelationshipCustomerTypes)
            {
                RelationshipCustomerTypeDAO RelationshipCustomerTypeDAO = new RelationshipCustomerTypeDAO();
                RelationshipCustomerTypeDAO.Id = RelationshipCustomerType.Id;
                RelationshipCustomerTypeDAO.Name = RelationshipCustomerType.Name;
                RelationshipCustomerTypeDAOs.Add(RelationshipCustomerTypeDAO);
            }
            await DataContext.BulkMergeAsync(RelationshipCustomerTypeDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<RelationshipCustomerType> RelationshipCustomerTypes)
        {
            List<long> Ids = RelationshipCustomerTypes.Select(x => x.Id).ToList();
            await DataContext.RelationshipCustomerType
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(RelationshipCustomerType RelationshipCustomerType)
        {
        }
        
    }
}
