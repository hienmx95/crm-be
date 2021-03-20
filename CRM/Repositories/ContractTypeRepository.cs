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
    public interface IContractTypeRepository
    {
        Task<int> Count(ContractTypeFilter ContractTypeFilter);
        Task<List<ContractType>> List(ContractTypeFilter ContractTypeFilter);
        Task<List<ContractType>> List(List<long> Ids);
        Task<ContractType> Get(long Id);
    }
    public class ContractTypeRepository : IContractTypeRepository
    {
        private DataContext DataContext;
        public ContractTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContractTypeDAO> DynamicFilter(IQueryable<ContractTypeDAO> query, ContractTypeFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Code != null && filter.Code.HasValue)
                query = query.Where(q => q.Code, filter.Code);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ContractTypeDAO> OrFilter(IQueryable<ContractTypeDAO> query, ContractTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContractTypeDAO> initQuery = query.Where(q => false);
            foreach (ContractTypeFilter ContractTypeFilter in filter.OrFilter)
            {
                IQueryable<ContractTypeDAO> queryable = query;
                if (ContractTypeFilter.Id != null && ContractTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContractTypeFilter.Id);
                if (ContractTypeFilter.Name != null && ContractTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ContractTypeFilter.Name);
                if (ContractTypeFilter.Code != null && ContractTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ContractTypeFilter.Code);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ContractTypeDAO> DynamicOrder(IQueryable<ContractTypeDAO> query, ContractTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContractTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContractTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ContractTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContractTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContractTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ContractTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ContractType>> DynamicSelect(IQueryable<ContractTypeDAO> query, ContractTypeFilter filter)
        {
            List<ContractType> ContractTypes = await query.Select(q => new ContractType()
            {
                Id = filter.Selects.Contains(ContractTypeSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(ContractTypeSelect.Name) ? q.Name : default(string),
                Code = filter.Selects.Contains(ContractTypeSelect.Code) ? q.Code : default(string),
            }).ToListAsync();
            return ContractTypes;
        }

        public async Task<int> Count(ContractTypeFilter filter)
        {
            IQueryable<ContractTypeDAO> ContractTypes = DataContext.ContractType.AsNoTracking();
            ContractTypes = DynamicFilter(ContractTypes, filter);
            return await ContractTypes.CountAsync();
        }

        public async Task<List<ContractType>> List(ContractTypeFilter filter)
        {
            if (filter == null) return new List<ContractType>();
            IQueryable<ContractTypeDAO> ContractTypeDAOs = DataContext.ContractType.AsNoTracking();
            ContractTypeDAOs = DynamicFilter(ContractTypeDAOs, filter);
            ContractTypeDAOs = DynamicOrder(ContractTypeDAOs, filter);
            List<ContractType> ContractTypes = await DynamicSelect(ContractTypeDAOs, filter);
            return ContractTypes;
        }

        public async Task<List<ContractType>> List(List<long> Ids)
        {
            List<ContractType> ContractTypes = await DataContext.ContractType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ContractType()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).ToListAsync();
            

            return ContractTypes;
        }

        public async Task<ContractType> Get(long Id)
        {
            ContractType ContractType = await DataContext.ContractType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new ContractType()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).FirstOrDefaultAsync();

            if (ContractType == null)
                return null;

            return ContractType;
        }
    }
}
