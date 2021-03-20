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
    public interface ICompanyStatusRepository
    {
        Task<int> Count(CompanyStatusFilter CompanyStatusFilter);
        Task<List<CompanyStatus>> List(CompanyStatusFilter CompanyStatusFilter);
        Task<List<CompanyStatus>> List(List<long> Ids);
        Task<CompanyStatus> Get(long Id);
        Task<bool> Create(CompanyStatus CompanyStatus);
        Task<bool> Update(CompanyStatus CompanyStatus);
        Task<bool> Delete(CompanyStatus CompanyStatus);
        Task<bool> BulkMerge(List<CompanyStatus> CompanyStatuses);
        Task<bool> BulkDelete(List<CompanyStatus> CompanyStatuses);
    }
    public class CompanyStatusRepository : ICompanyStatusRepository
    {
        private DataContext DataContext;
        public CompanyStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CompanyStatusDAO> DynamicFilter(IQueryable<CompanyStatusDAO> query, CompanyStatusFilter filter)
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

        private IQueryable<CompanyStatusDAO> OrFilter(IQueryable<CompanyStatusDAO> query, CompanyStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CompanyStatusDAO> initQuery = query.Where(q => false);
            foreach (CompanyStatusFilter CompanyStatusFilter in filter.OrFilter)
            {
                IQueryable<CompanyStatusDAO> queryable = query;
                if (CompanyStatusFilter.Id != null && CompanyStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CompanyStatusFilter.Id);
                if (CompanyStatusFilter.Code != null && CompanyStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, CompanyStatusFilter.Code);
                if (CompanyStatusFilter.Name != null && CompanyStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, CompanyStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CompanyStatusDAO> DynamicOrder(IQueryable<CompanyStatusDAO> query, CompanyStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CompanyStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CompanyStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case CompanyStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CompanyStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CompanyStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case CompanyStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CompanyStatus>> DynamicSelect(IQueryable<CompanyStatusDAO> query, CompanyStatusFilter filter)
        {
            List<CompanyStatus> CompanyStatuses = await query.Select(q => new CompanyStatus()
            {
                Id = filter.Selects.Contains(CompanyStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(CompanyStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(CompanyStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return CompanyStatuses;
        }

        public async Task<int> Count(CompanyStatusFilter filter)
        {
            IQueryable<CompanyStatusDAO> CompanyStatuses = DataContext.CompanyStatus.AsNoTracking();
            CompanyStatuses = DynamicFilter(CompanyStatuses, filter);
            return await CompanyStatuses.CountAsync();
        }

        public async Task<List<CompanyStatus>> List(CompanyStatusFilter filter)
        {
            if (filter == null) return new List<CompanyStatus>();
            IQueryable<CompanyStatusDAO> CompanyStatusDAOs = DataContext.CompanyStatus.AsNoTracking();
            CompanyStatusDAOs = DynamicFilter(CompanyStatusDAOs, filter);
            CompanyStatusDAOs = DynamicOrder(CompanyStatusDAOs, filter);
            List<CompanyStatus> CompanyStatuses = await DynamicSelect(CompanyStatusDAOs, filter);
            return CompanyStatuses;
        }

        public async Task<List<CompanyStatus>> List(List<long> Ids)
        {
            List<CompanyStatus> CompanyStatuses = await DataContext.CompanyStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CompanyStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return CompanyStatuses;
        }

        public async Task<CompanyStatus> Get(long Id)
        {
            CompanyStatus CompanyStatus = await DataContext.CompanyStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CompanyStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (CompanyStatus == null)
                return null;

            return CompanyStatus;
        }
        public async Task<bool> Create(CompanyStatus CompanyStatus)
        {
            CompanyStatusDAO CompanyStatusDAO = new CompanyStatusDAO();
            CompanyStatusDAO.Id = CompanyStatus.Id;
            CompanyStatusDAO.Code = CompanyStatus.Code;
            CompanyStatusDAO.Name = CompanyStatus.Name;
            DataContext.CompanyStatus.Add(CompanyStatusDAO);
            await DataContext.SaveChangesAsync();
            CompanyStatus.Id = CompanyStatusDAO.Id;
            await SaveReference(CompanyStatus);
            return true;
        }

        public async Task<bool> Update(CompanyStatus CompanyStatus)
        {
            CompanyStatusDAO CompanyStatusDAO = DataContext.CompanyStatus.Where(x => x.Id == CompanyStatus.Id).FirstOrDefault();
            if (CompanyStatusDAO == null)
                return false;
            CompanyStatusDAO.Id = CompanyStatus.Id;
            CompanyStatusDAO.Code = CompanyStatus.Code;
            CompanyStatusDAO.Name = CompanyStatus.Name;
            await DataContext.SaveChangesAsync();
            await SaveReference(CompanyStatus);
            return true;
        }

        public async Task<bool> Delete(CompanyStatus CompanyStatus)
        {
            await DataContext.CompanyStatus.Where(x => x.Id == CompanyStatus.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CompanyStatus> CompanyStatuses)
        {
            List<CompanyStatusDAO> CompanyStatusDAOs = new List<CompanyStatusDAO>();
            foreach (CompanyStatus CompanyStatus in CompanyStatuses)
            {
                CompanyStatusDAO CompanyStatusDAO = new CompanyStatusDAO();
                CompanyStatusDAO.Id = CompanyStatus.Id;
                CompanyStatusDAO.Code = CompanyStatus.Code;
                CompanyStatusDAO.Name = CompanyStatus.Name;
                CompanyStatusDAOs.Add(CompanyStatusDAO);
            }
            await DataContext.BulkMergeAsync(CompanyStatusDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CompanyStatus> CompanyStatuses)
        {
            List<long> Ids = CompanyStatuses.Select(x => x.Id).ToList();
            await DataContext.CompanyStatus
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(CompanyStatus CompanyStatus)
        {
        }
        
    }
}
