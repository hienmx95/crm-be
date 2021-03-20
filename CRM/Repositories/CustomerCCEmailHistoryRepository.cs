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
    public interface ICustomerCCEmailHistoryRepository
    {
        Task<int> Count(CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter);
        Task<List<CustomerCCEmailHistory>> List(CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter);
        Task<List<CustomerCCEmailHistory>> List(List<long> Ids);
        Task<CustomerCCEmailHistory> Get(long Id);
        Task<bool> Create(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<bool> Update(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<bool> Delete(CustomerCCEmailHistory CustomerCCEmailHistory);
        Task<bool> BulkMerge(List<CustomerCCEmailHistory> CustomerCCEmailHistories);
        Task<bool> BulkDelete(List<CustomerCCEmailHistory> CustomerCCEmailHistories);
    }
    public class CustomerCCEmailHistoryRepository : ICustomerCCEmailHistoryRepository
    {
        private DataContext DataContext;
        public CustomerCCEmailHistoryRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<CustomerCCEmailHistoryDAO> DynamicFilter(IQueryable<CustomerCCEmailHistoryDAO> query, CustomerCCEmailHistoryFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.CustomerEmailHistoryId != null && filter.CustomerEmailHistoryId.HasValue)
                query = query.Where(q => q.CustomerEmailHistoryId, filter.CustomerEmailHistoryId);
            if (filter.CCEmail != null && filter.CCEmail.HasValue)
                query = query.Where(q => q.CCEmail, filter.CCEmail);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<CustomerCCEmailHistoryDAO> OrFilter(IQueryable<CustomerCCEmailHistoryDAO> query, CustomerCCEmailHistoryFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CustomerCCEmailHistoryDAO> initQuery = query.Where(q => false);
            foreach (CustomerCCEmailHistoryFilter CustomerCCEmailHistoryFilter in filter.OrFilter)
            {
                IQueryable<CustomerCCEmailHistoryDAO> queryable = query;
                if (CustomerCCEmailHistoryFilter.Id != null && CustomerCCEmailHistoryFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, CustomerCCEmailHistoryFilter.Id);
                if (CustomerCCEmailHistoryFilter.CustomerEmailHistoryId != null && CustomerCCEmailHistoryFilter.CustomerEmailHistoryId.HasValue)
                    queryable = queryable.Where(q => q.CustomerEmailHistoryId, CustomerCCEmailHistoryFilter.CustomerEmailHistoryId);
                if (CustomerCCEmailHistoryFilter.CCEmail != null && CustomerCCEmailHistoryFilter.CCEmail.HasValue)
                    queryable = queryable.Where(q => q.CCEmail, CustomerCCEmailHistoryFilter.CCEmail);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<CustomerCCEmailHistoryDAO> DynamicOrder(IQueryable<CustomerCCEmailHistoryDAO> query, CustomerCCEmailHistoryFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CustomerCCEmailHistoryOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CustomerCCEmailHistoryOrder.CustomerEmailHistory:
                            query = query.OrderBy(q => q.CustomerEmailHistoryId);
                            break;
                        case CustomerCCEmailHistoryOrder.CCEmail:
                            query = query.OrderBy(q => q.CCEmail);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CustomerCCEmailHistoryOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CustomerCCEmailHistoryOrder.CustomerEmailHistory:
                            query = query.OrderByDescending(q => q.CustomerEmailHistoryId);
                            break;
                        case CustomerCCEmailHistoryOrder.CCEmail:
                            query = query.OrderByDescending(q => q.CCEmail);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<CustomerCCEmailHistory>> DynamicSelect(IQueryable<CustomerCCEmailHistoryDAO> query, CustomerCCEmailHistoryFilter filter)
        {
            List<CustomerCCEmailHistory> CustomerCCEmailHistories = await query.Select(q => new CustomerCCEmailHistory()
            {
                Id = filter.Selects.Contains(CustomerCCEmailHistorySelect.Id) ? q.Id : default(long),
                CustomerEmailHistoryId = filter.Selects.Contains(CustomerCCEmailHistorySelect.CustomerEmailHistory) ? q.CustomerEmailHistoryId : default(long),
                CCEmail = filter.Selects.Contains(CustomerCCEmailHistorySelect.CCEmail) ? q.CCEmail : default(string),
                CustomerEmailHistory = filter.Selects.Contains(CustomerCCEmailHistorySelect.CustomerEmailHistory) && q.CustomerEmailHistory != null ? new CustomerEmailHistory
                {
                    Id = q.CustomerEmailHistory.Id,
                    Title = q.CustomerEmailHistory.Title,
                    Content = q.CustomerEmailHistory.Content,
                    Reciepient = q.CustomerEmailHistory.Reciepient,
                    CustomerId = q.CustomerEmailHistory.CustomerId,
                    CreatorId = q.CustomerEmailHistory.CreatorId,
                    EmailStatusId = q.CustomerEmailHistory.EmailStatusId,
                } : null,
            }).ToListAsync();
            return CustomerCCEmailHistories;
        }

        public async Task<int> Count(CustomerCCEmailHistoryFilter filter)
        {
            IQueryable<CustomerCCEmailHistoryDAO> CustomerCCEmailHistories = DataContext.CustomerCCEmailHistory.AsNoTracking();
            CustomerCCEmailHistories = DynamicFilter(CustomerCCEmailHistories, filter);
            return await CustomerCCEmailHistories.CountAsync();
        }

        public async Task<List<CustomerCCEmailHistory>> List(CustomerCCEmailHistoryFilter filter)
        {
            if (filter == null) return new List<CustomerCCEmailHistory>();
            IQueryable<CustomerCCEmailHistoryDAO> CustomerCCEmailHistoryDAOs = DataContext.CustomerCCEmailHistory.AsNoTracking();
            CustomerCCEmailHistoryDAOs = DynamicFilter(CustomerCCEmailHistoryDAOs, filter);
            CustomerCCEmailHistoryDAOs = DynamicOrder(CustomerCCEmailHistoryDAOs, filter);
            List<CustomerCCEmailHistory> CustomerCCEmailHistories = await DynamicSelect(CustomerCCEmailHistoryDAOs, filter);
            return CustomerCCEmailHistories;
        }

        public async Task<List<CustomerCCEmailHistory>> List(List<long> Ids)
        {
            List<CustomerCCEmailHistory> CustomerCCEmailHistories = await DataContext.CustomerCCEmailHistory.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new CustomerCCEmailHistory()
            {
                Id = x.Id,
                CustomerEmailHistoryId = x.CustomerEmailHistoryId,
                CCEmail = x.CCEmail,
                CustomerEmailHistory = x.CustomerEmailHistory == null ? null : new CustomerEmailHistory
                {
                    Id = x.CustomerEmailHistory.Id,
                    Title = x.CustomerEmailHistory.Title,
                    Content = x.CustomerEmailHistory.Content,
                    Reciepient = x.CustomerEmailHistory.Reciepient,
                    CustomerId = x.CustomerEmailHistory.CustomerId,
                    CreatorId = x.CustomerEmailHistory.CreatorId,
                    EmailStatusId = x.CustomerEmailHistory.EmailStatusId,
                },
            }).ToListAsync();
            

            return CustomerCCEmailHistories;
        }

        public async Task<CustomerCCEmailHistory> Get(long Id)
        {
            CustomerCCEmailHistory CustomerCCEmailHistory = await DataContext.CustomerCCEmailHistory.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new CustomerCCEmailHistory()
            {
                Id = x.Id,
                CustomerEmailHistoryId = x.CustomerEmailHistoryId,
                CCEmail = x.CCEmail,
                CustomerEmailHistory = x.CustomerEmailHistory == null ? null : new CustomerEmailHistory
                {
                    Id = x.CustomerEmailHistory.Id,
                    Title = x.CustomerEmailHistory.Title,
                    Content = x.CustomerEmailHistory.Content,
                    Reciepient = x.CustomerEmailHistory.Reciepient,
                    CustomerId = x.CustomerEmailHistory.CustomerId,
                    CreatorId = x.CustomerEmailHistory.CreatorId,
                    EmailStatusId = x.CustomerEmailHistory.EmailStatusId,
                },
            }).FirstOrDefaultAsync();

            if (CustomerCCEmailHistory == null)
                return null;

            return CustomerCCEmailHistory;
        }
        public async Task<bool> Create(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            CustomerCCEmailHistoryDAO CustomerCCEmailHistoryDAO = new CustomerCCEmailHistoryDAO();
            CustomerCCEmailHistoryDAO.Id = CustomerCCEmailHistory.Id;
            CustomerCCEmailHistoryDAO.CustomerEmailHistoryId = CustomerCCEmailHistory.CustomerEmailHistoryId;
            CustomerCCEmailHistoryDAO.CCEmail = CustomerCCEmailHistory.CCEmail;
            DataContext.CustomerCCEmailHistory.Add(CustomerCCEmailHistoryDAO);
            await DataContext.SaveChangesAsync();
            CustomerCCEmailHistory.Id = CustomerCCEmailHistoryDAO.Id;
            await SaveReference(CustomerCCEmailHistory);
            return true;
        }

        public async Task<bool> Update(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            CustomerCCEmailHistoryDAO CustomerCCEmailHistoryDAO = DataContext.CustomerCCEmailHistory.Where(x => x.Id == CustomerCCEmailHistory.Id).FirstOrDefault();
            if (CustomerCCEmailHistoryDAO == null)
                return false;
            CustomerCCEmailHistoryDAO.Id = CustomerCCEmailHistory.Id;
            CustomerCCEmailHistoryDAO.CustomerEmailHistoryId = CustomerCCEmailHistory.CustomerEmailHistoryId;
            CustomerCCEmailHistoryDAO.CCEmail = CustomerCCEmailHistory.CCEmail;
            await DataContext.SaveChangesAsync();
            await SaveReference(CustomerCCEmailHistory);
            return true;
        }

        public async Task<bool> Delete(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
            await DataContext.CustomerCCEmailHistory.Where(x => x.Id == CustomerCCEmailHistory.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<CustomerCCEmailHistory> CustomerCCEmailHistories)
        {
            List<CustomerCCEmailHistoryDAO> CustomerCCEmailHistoryDAOs = new List<CustomerCCEmailHistoryDAO>();
            foreach (CustomerCCEmailHistory CustomerCCEmailHistory in CustomerCCEmailHistories)
            {
                CustomerCCEmailHistoryDAO CustomerCCEmailHistoryDAO = new CustomerCCEmailHistoryDAO();
                CustomerCCEmailHistoryDAO.Id = CustomerCCEmailHistory.Id;
                CustomerCCEmailHistoryDAO.CustomerEmailHistoryId = CustomerCCEmailHistory.CustomerEmailHistoryId;
                CustomerCCEmailHistoryDAO.CCEmail = CustomerCCEmailHistory.CCEmail;
                CustomerCCEmailHistoryDAOs.Add(CustomerCCEmailHistoryDAO);
            }
            await DataContext.BulkMergeAsync(CustomerCCEmailHistoryDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<CustomerCCEmailHistory> CustomerCCEmailHistories)
        {
            List<long> Ids = CustomerCCEmailHistories.Select(x => x.Id).ToList();
            await DataContext.CustomerCCEmailHistory
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(CustomerCCEmailHistory CustomerCCEmailHistory)
        {
        }
        
    }
}
