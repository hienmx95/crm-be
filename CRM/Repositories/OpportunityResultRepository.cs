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
    public interface IOpportunityResultTypeRepository
    {
        Task<int> Count(OpportunityResultTypeFilter OpportunityResultTypeFilter);
        Task<List<OpportunityResultType>> List(OpportunityResultTypeFilter OpportunityResultTypeFilter);
        Task<List<OpportunityResultType>> List(List<long> Ids);
        Task<OpportunityResultType> Get(long Id);
    }
    public class OpportunityResultTypeRepository : IOpportunityResultTypeRepository
    {
        private DataContext DataContext;
        public OpportunityResultTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OpportunityResultTypeDAO> DynamicFilter(IQueryable<OpportunityResultTypeDAO> query, OpportunityResultTypeFilter filter)
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

        private IQueryable<OpportunityResultTypeDAO> OrFilter(IQueryable<OpportunityResultTypeDAO> query, OpportunityResultTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OpportunityResultTypeDAO> initQuery = query.Where(q => false);
            foreach (OpportunityResultTypeFilter OpportunityResultTypeFilter in filter.OrFilter)
            {
                IQueryable<OpportunityResultTypeDAO> queryable = query;
                if (OpportunityResultTypeFilter.Id != null && OpportunityResultTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OpportunityResultTypeFilter.Id);
                if (OpportunityResultTypeFilter.Code != null && OpportunityResultTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, OpportunityResultTypeFilter.Code);
                if (OpportunityResultTypeFilter.Name != null && OpportunityResultTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, OpportunityResultTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<OpportunityResultTypeDAO> DynamicOrder(IQueryable<OpportunityResultTypeDAO> query, OpportunityResultTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityResultTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OpportunityResultTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case OpportunityResultTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OpportunityResultTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OpportunityResultTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case OpportunityResultTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OpportunityResultType>> DynamicSelect(IQueryable<OpportunityResultTypeDAO> query, OpportunityResultTypeFilter filter)
        {
            List<OpportunityResultType> OpportunityResultTypes = await query.Select(q => new OpportunityResultType()
            {
                Id = filter.Selects.Contains(OpportunityResultTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(OpportunityResultTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(OpportunityResultTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return OpportunityResultTypes;
        }

        public async Task<int> Count(OpportunityResultTypeFilter filter)
        {
            IQueryable<OpportunityResultTypeDAO> OpportunityResultTypes = DataContext.OpportunityResultType.AsNoTracking();
            OpportunityResultTypes = DynamicFilter(OpportunityResultTypes, filter);
            return await OpportunityResultTypes.CountAsync();
        }

        public async Task<List<OpportunityResultType>> List(OpportunityResultTypeFilter filter)
        {
            if (filter == null) return new List<OpportunityResultType>();
            IQueryable<OpportunityResultTypeDAO> OpportunityResultTypeDAOs = DataContext.OpportunityResultType.AsNoTracking();
            OpportunityResultTypeDAOs = DynamicFilter(OpportunityResultTypeDAOs, filter);
            OpportunityResultTypeDAOs = DynamicOrder(OpportunityResultTypeDAOs, filter);
            List<OpportunityResultType> OpportunityResultTypes = await DynamicSelect(OpportunityResultTypeDAOs, filter);
            return OpportunityResultTypes;
        }

        public async Task<List<OpportunityResultType>> List(List<long> Ids)
        {
            List<OpportunityResultType> OpportunityResultTypes = await DataContext.OpportunityResultType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new OpportunityResultType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return OpportunityResultTypes;
        }

        public async Task<OpportunityResultType> Get(long Id)
        {
            OpportunityResultType OpportunityResultType = await DataContext.OpportunityResultType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new OpportunityResultType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (OpportunityResultType == null)
                return null;

            return OpportunityResultType;
        }
    }
}
