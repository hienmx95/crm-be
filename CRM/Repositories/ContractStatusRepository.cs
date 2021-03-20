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
    public interface IContractStatusRepository
    {
        Task<int> Count(ContractStatusFilter ContractStatusFilter);
        Task<List<ContractStatus>> List(ContractStatusFilter ContractStatusFilter);
        Task<List<ContractStatus>> List(List<long> Ids);
        Task<ContractStatus> Get(long Id);
    }
    public class ContractStatusRepository : IContractStatusRepository
    {
        private DataContext DataContext;
        public ContractStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContractStatusDAO> DynamicFilter(IQueryable<ContractStatusDAO> query, ContractStatusFilter filter)
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

        private IQueryable<ContractStatusDAO> OrFilter(IQueryable<ContractStatusDAO> query, ContractStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContractStatusDAO> initQuery = query.Where(q => false);
            foreach (ContractStatusFilter ContractStatusFilter in filter.OrFilter)
            {
                IQueryable<ContractStatusDAO> queryable = query;
                if (ContractStatusFilter.Id != null && ContractStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContractStatusFilter.Id);
                if (ContractStatusFilter.Name != null && ContractStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ContractStatusFilter.Name);
                if (ContractStatusFilter.Code != null && ContractStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ContractStatusFilter.Code);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ContractStatusDAO> DynamicOrder(IQueryable<ContractStatusDAO> query, ContractStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContractStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContractStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ContractStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContractStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContractStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ContractStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ContractStatus>> DynamicSelect(IQueryable<ContractStatusDAO> query, ContractStatusFilter filter)
        {
            List<ContractStatus> ContractStatuses = await query.Select(q => new ContractStatus()
            {
                Id = filter.Selects.Contains(ContractStatusSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(ContractStatusSelect.Name) ? q.Name : default(string),
                Code = filter.Selects.Contains(ContractStatusSelect.Code) ? q.Code : default(string),
            }).ToListAsync();
            return ContractStatuses;
        }

        public async Task<int> Count(ContractStatusFilter filter)
        {
            IQueryable<ContractStatusDAO> ContractStatuses = DataContext.ContractStatus.AsNoTracking();
            ContractStatuses = DynamicFilter(ContractStatuses, filter);
            return await ContractStatuses.CountAsync();
        }

        public async Task<List<ContractStatus>> List(ContractStatusFilter filter)
        {
            if (filter == null) return new List<ContractStatus>();
            IQueryable<ContractStatusDAO> ContractStatusDAOs = DataContext.ContractStatus.AsNoTracking();
            ContractStatusDAOs = DynamicFilter(ContractStatusDAOs, filter);
            ContractStatusDAOs = DynamicOrder(ContractStatusDAOs, filter);
            List<ContractStatus> ContractStatuses = await DynamicSelect(ContractStatusDAOs, filter);
            return ContractStatuses;
        }

        public async Task<List<ContractStatus>> List(List<long> Ids)
        {
            List<ContractStatus> ContractStatuses = await DataContext.ContractStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ContractStatus()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).ToListAsync();
            

            return ContractStatuses;
        }

        public async Task<ContractStatus> Get(long Id)
        {
            ContractStatus ContractStatus = await DataContext.ContractStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new ContractStatus()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).FirstOrDefaultAsync();

            if (ContractStatus == null)
                return null;

            return ContractStatus;
        }
    }
}
