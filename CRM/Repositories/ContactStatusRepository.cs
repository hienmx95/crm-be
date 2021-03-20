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
    public interface IContactStatusRepository
    {
        Task<int> Count(ContactStatusFilter ContactStatusFilter);
        Task<List<ContactStatus>> List(ContactStatusFilter ContactStatusFilter);
        Task<List<ContactStatus>> List(List<long> Ids);
        Task<ContactStatus> Get(long Id);
    }
    public class ContactStatusRepository : IContactStatusRepository
    {
        private DataContext DataContext;
        public ContactStatusRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ContactStatusDAO> DynamicFilter(IQueryable<ContactStatusDAO> query, ContactStatusFilter filter)
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

        private IQueryable<ContactStatusDAO> OrFilter(IQueryable<ContactStatusDAO> query, ContactStatusFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ContactStatusDAO> initQuery = query.Where(q => false);
            foreach (ContactStatusFilter ContactStatusFilter in filter.OrFilter)
            {
                IQueryable<ContactStatusDAO> queryable = query;
                if (ContactStatusFilter.Id != null && ContactStatusFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, ContactStatusFilter.Id);
                if (ContactStatusFilter.Code != null && ContactStatusFilter.Code.HasValue)
                    queryable = queryable.Where(q => q.Code, ContactStatusFilter.Code);
                if (ContactStatusFilter.Name != null && ContactStatusFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, ContactStatusFilter.Name);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<ContactStatusDAO> DynamicOrder(IQueryable<ContactStatusDAO> query, ContactStatusFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ContactStatusOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ContactStatusOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ContactStatusOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ContactStatusOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ContactStatusOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ContactStatusOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<ContactStatus>> DynamicSelect(IQueryable<ContactStatusDAO> query, ContactStatusFilter filter)
        {
            List<ContactStatus> ContactStatuses = await query.Select(q => new ContactStatus()
            {
                Id = filter.Selects.Contains(ContactStatusSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ContactStatusSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ContactStatusSelect.Name) ? q.Name : default(string),
            }).ToListAsync();
            return ContactStatuses;
        }

        public async Task<int> Count(ContactStatusFilter filter)
        {
            IQueryable<ContactStatusDAO> ContactStatuses = DataContext.ContactStatus.AsNoTracking();
            ContactStatuses = DynamicFilter(ContactStatuses, filter);
            return await ContactStatuses.CountAsync();
        }

        public async Task<List<ContactStatus>> List(ContactStatusFilter filter)
        {
            if (filter == null) return new List<ContactStatus>();
            IQueryable<ContactStatusDAO> ContactStatusDAOs = DataContext.ContactStatus.AsNoTracking();
            ContactStatusDAOs = DynamicFilter(ContactStatusDAOs, filter);
            ContactStatusDAOs = DynamicOrder(ContactStatusDAOs, filter);
            List<ContactStatus> ContactStatuses = await DynamicSelect(ContactStatusDAOs, filter);
            return ContactStatuses;
        }

        public async Task<List<ContactStatus>> List(List<long> Ids)
        {
            List<ContactStatus> ContactStatuses = await DataContext.ContactStatus.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ContactStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).ToListAsync();
            

            return ContactStatuses;
        }

        public async Task<ContactStatus> Get(long Id)
        {
            ContactStatus ContactStatus = await DataContext.ContactStatus.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new ContactStatus()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if (ContactStatus == null)
                return null;

            return ContactStatus;
        }
    }
}
