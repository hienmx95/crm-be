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
    public interface IEmailStatusRepository
    {
        Task<int> Count(EmailStatusFilter EmailStatusFilter);
        Task<List<EmailStatus>> List(EmailStatusFilter EmailStatusFilter);
        Task<List<EmailStatus>> List(List<long> Ids);
        Task<EmailStatus> Get(long Id);
    }
    public class EmailStatusRepository : IEmailStatusRepository
    {
        private DataContext DataContext;
        public EmailStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<EmailStatusDAO> DynamicFilter(IQueryable<EmailStatusDAO> query, EmailStatusFilter filter)
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

        private IQueryable<EmailStatusDAO> OrFilter(IQueryable<EmailStatusDAO> query, EmailStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<EmailStatusDAO> initQuery = query.Where(q => false);
            foreach (EmailStatusFilter EmailStatusFilter in filter.OrFilter)
            {
                IQueryable<EmailStatusDAO> queryable = query;
                if (EmailStatusFilter.Id != null && EmailStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, EmailStatusFilter.Id);
                if (EmailStatusFilter.Code != null && EmailStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, EmailStatusFilter.Code);
                if (EmailStatusFilter.Name != null && EmailStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, EmailStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<EmailStatusDAO> DynamicOrder(IQueryable<EmailStatusDAO> query, EmailStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case EmailStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case EmailStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case EmailStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case EmailStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case EmailStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case EmailStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<EmailStatus>> DynamicSelect(IQueryable<EmailStatusDAO> query, EmailStatusFilter filter)
        {
            List<EmailStatus> EmailStatuses = await query.Select(q => new EmailStatus()
            {
                Id = filter.Selects.Contains(EmailStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(EmailStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(EmailStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return EmailStatuses;
        }

        public async Task<int> Count(EmailStatusFilter filter)
        {
            IQueryable<EmailStatusDAO> EmailStatuses = DataContext.EmailStatus.AsNoTracking();
            EmailStatuses = DynamicFilter(EmailStatuses, filter);
            return await EmailStatuses.CountAsync();
        }

        public async Task<List<EmailStatus>> List(EmailStatusFilter filter)
        {
            if (filter == null) return new List<EmailStatus>();
            IQueryable<EmailStatusDAO> EmailStatusDAOs = DataContext.EmailStatus.AsNoTracking();
            EmailStatusDAOs = DynamicFilter(EmailStatusDAOs, filter);
            EmailStatusDAOs = DynamicOrder(EmailStatusDAOs, filter);
            List<EmailStatus> EmailStatuses = await DynamicSelect(EmailStatusDAOs, filter);
            return EmailStatuses;
        }

        public async Task<List<EmailStatus>> List(List<long> Ids)
        {
            List<EmailStatus> EmailStatuses = await DataContext.EmailStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new EmailStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return EmailStatuses;
        }

        public async Task<EmailStatus> Get(long Id)
        {
            EmailStatus EmailStatus = await DataContext.EmailStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new EmailStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (EmailStatus == null)
                return null;

            return EmailStatus;
        }
    }
}
