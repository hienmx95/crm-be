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
    public interface IEmailTypeRepository
    {
        Task<int> Count(EmailTypeFilter EmailTypeFilter);
        Task<List<EmailType>> List(EmailTypeFilter EmailTypeFilter);
        Task<List<EmailType>> List(List<long> Ids);
        Task<EmailType> Get(long Id);
    }
    public class EmailTypeRepository : IEmailTypeRepository
    {
        private DataContext DataContext;
        public EmailTypeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<EmailTypeDAO> DynamicFilter(IQueryable<EmailTypeDAO> query, EmailTypeFilter filter)
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

        private IQueryable<EmailTypeDAO> OrFilter(IQueryable<EmailTypeDAO> query, EmailTypeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<EmailTypeDAO> initQuery = query.Where(q => false);
            foreach (EmailTypeFilter EmailTypeFilter in filter.OrFilter)
            {
                IQueryable<EmailTypeDAO> queryable = query;
                if (EmailTypeFilter.Id != null && EmailTypeFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, EmailTypeFilter.Id);
                if (EmailTypeFilter.Code != null && EmailTypeFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, EmailTypeFilter.Code);
                if (EmailTypeFilter.Name != null && EmailTypeFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, EmailTypeFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<EmailTypeDAO> DynamicOrder(IQueryable<EmailTypeDAO> query, EmailTypeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case EmailTypeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case EmailTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case EmailTypeOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case EmailTypeOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case EmailTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case EmailTypeOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<EmailType>> DynamicSelect(IQueryable<EmailTypeDAO> query, EmailTypeFilter filter)
        {
            List<EmailType> EmailTypes = await query.Select(q => new EmailType()
            {
                Id = filter.Selects.Contains(EmailTypeSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(EmailTypeSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(EmailTypeSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return EmailTypes;
        }

        public async Task<int> Count(EmailTypeFilter filter)
        {
            IQueryable<EmailTypeDAO> EmailTypes = DataContext.EmailType.AsNoTracking();
            EmailTypes = DynamicFilter(EmailTypes, filter);
            return await EmailTypes.CountAsync();
        }

        public async Task<List<EmailType>> List(EmailTypeFilter filter)
        {
            if (filter == null) return new List<EmailType>();
            IQueryable<EmailTypeDAO> EmailTypeDAOs = DataContext.EmailType.AsNoTracking();
            EmailTypeDAOs = DynamicFilter(EmailTypeDAOs, filter);
            EmailTypeDAOs = DynamicOrder(EmailTypeDAOs, filter);
            List<EmailType> EmailTypes = await DynamicSelect(EmailTypeDAOs, filter);
            return EmailTypes;
        }

        public async Task<List<EmailType>> List(List<long> Ids)
        {
            List<EmailType> EmailTypes = await DataContext.EmailType.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new EmailType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return EmailTypes;
        }

        public async Task<EmailType> Get(long Id)
        {
            EmailType EmailType = await DataContext.EmailType.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new EmailType()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (EmailType == null)
                return null;

            return EmailType;
        }
    }
}
