using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public interface ITicketGeneratedIdRepository
    {
        Task<int> Count(TicketGeneratedIdFilter TicketGeneratedIdFilter);
        Task<List<TicketGeneratedId>> List(TicketGeneratedIdFilter TicketGeneratedIdFilter);
        Task<TicketGeneratedId> Get(long Id);
        Task<TicketGeneratedId> GetTicketGeneratedIdUnused();
        Task<bool> Create(TicketGeneratedId TicketGeneratedId);
        Task<bool> Update(TicketGeneratedId TicketGeneratedId);
    }
    public class TicketGeneratedIdRepository : ITicketGeneratedIdRepository
    {
        private DataContext DataContext;
        public TicketGeneratedIdRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketGeneratedIdDAO> DynamicFilter(IQueryable<TicketGeneratedIdDAO> query, TicketGeneratedIdFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<TicketGeneratedIdDAO> OrFilter(IQueryable<TicketGeneratedIdDAO> query, TicketGeneratedIdFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketGeneratedIdDAO> initQuery = query.Where(q => false);
            foreach (TicketGeneratedIdFilter TicketGeneratedIdFilter in filter.OrFilter)
            {
                IQueryable<TicketGeneratedIdDAO> queryable = query;
                if (TicketGeneratedIdFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, TicketGeneratedIdFilter.Id);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<TicketGeneratedIdDAO> DynamicOrder(IQueryable<TicketGeneratedIdDAO> query, TicketGeneratedIdFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketGeneratedIdOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketGeneratedIdOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketGeneratedIdOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketGeneratedIdOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<TicketGeneratedId>> DynamicSelect(IQueryable<TicketGeneratedIdDAO> query, TicketGeneratedIdFilter filter)
        {
            List<TicketGeneratedId> TicketGeneratedIds = await query.Select(q => new TicketGeneratedId()
            {
                Id = filter.Selects.Contains(TicketGeneratedIdSelect.Id) ? q.Id : default(long),
                Used = filter.Selects.Contains(TicketGeneratedIdSelect.Used) ? q.Used : default(bool),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return TicketGeneratedIds;
        }

        public async Task<int> Count(TicketGeneratedIdFilter filter)
        {
            IQueryable<TicketGeneratedIdDAO> TicketGeneratedIds = DataContext.TicketGeneratedId.AsNoTracking();
            TicketGeneratedIds = DynamicFilter(TicketGeneratedIds, filter);
            return await TicketGeneratedIds.CountAsync();
        }

        public async Task<List<TicketGeneratedId>> List(TicketGeneratedIdFilter filter)
        {
            if (filter == null) return new List<TicketGeneratedId>();
            IQueryable<TicketGeneratedIdDAO> TicketGeneratedIdDAOs = DataContext.TicketGeneratedId.AsNoTracking();
            TicketGeneratedIdDAOs = DynamicFilter(TicketGeneratedIdDAOs, filter);
            TicketGeneratedIdDAOs = DynamicOrder(TicketGeneratedIdDAOs, filter);
            List<TicketGeneratedId> TicketGeneratedIds = await DynamicSelect(TicketGeneratedIdDAOs, filter);
            return TicketGeneratedIds;
        }

        public async Task<TicketGeneratedId> Get(long Id)
        {
            TicketGeneratedId TicketGeneratedId = await DataContext.TicketGeneratedId.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new TicketGeneratedId()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Used = x.Used,
            }).FirstOrDefaultAsync();

            if (TicketGeneratedId == null)
                return null;

            return TicketGeneratedId;
        }

        public async Task<TicketGeneratedId> GetTicketGeneratedIdUnused()
        {
            var datetime = StaticParams.DateTimeNow.AddHours(-1);
            TicketGeneratedId ticketUnused = await DataContext.TicketGeneratedId.AsNoTracking()
            .Where(x => !x.Used && x.CreatedAt <= datetime)
            .OrderBy(x => x.CreatedAt)
            .Select(x => new TicketGeneratedId()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Used = x.Used,
            }).FirstOrDefaultAsync();

            if (ticketUnused == null)
                return null;

            return ticketUnused;
        }

        public async Task<bool> Create(TicketGeneratedId TicketGeneratedId)
        {
            TicketGeneratedIdDAO TicketGeneratedIdDAO = new TicketGeneratedIdDAO();
            TicketGeneratedIdDAO.Id = TicketGeneratedId.Id;
            TicketGeneratedIdDAO.Used = TicketGeneratedId.Used;
            TicketGeneratedIdDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketGeneratedIdDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.TicketGeneratedId.Add(TicketGeneratedIdDAO);
            await DataContext.SaveChangesAsync();
            TicketGeneratedId.Id = TicketGeneratedIdDAO.Id;
            return true;
        }

        public async Task<bool> Update(TicketGeneratedId TicketGeneratedId)
        {
            TicketGeneratedIdDAO TicketGeneratedIdDAO = DataContext.TicketGeneratedId.Where(x => x.Id == TicketGeneratedId.Id).FirstOrDefault();
            if (TicketGeneratedIdDAO == null)
                return false;
            TicketGeneratedIdDAO.Id = TicketGeneratedId.Id;
            TicketGeneratedIdDAO.Used = TicketGeneratedId.Used;
            TicketGeneratedIdDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            return true;
        }
    }
}
