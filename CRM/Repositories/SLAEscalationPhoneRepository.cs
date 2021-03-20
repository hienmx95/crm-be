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
    public interface ISLAEscalationPhoneRepository
    {
        Task<int> Count(SLAEscalationPhoneFilter SLAEscalationPhoneFilter);
        Task<List<SLAEscalationPhone>> List(SLAEscalationPhoneFilter SLAEscalationPhoneFilter);
        Task<SLAEscalationPhone> Get(long Id);
        Task<bool> Create(SLAEscalationPhone SLAEscalationPhone);
        Task<bool> Update(SLAEscalationPhone SLAEscalationPhone);
        Task<bool> Delete(SLAEscalationPhone SLAEscalationPhone);
        Task<bool> BulkMerge(List<SLAEscalationPhone> SLAEscalationPhones);
        Task<bool> BulkDelete(List<SLAEscalationPhone> SLAEscalationPhones);
    }
    public class SLAEscalationPhoneRepository : ISLAEscalationPhoneRepository
    {
        private DataContext DataContext;
        public SLAEscalationPhoneRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAEscalationPhoneDAO> DynamicFilter(IQueryable<SLAEscalationPhoneDAO> query, SLAEscalationPhoneFilter filter)
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
            if (filter.SLAEscalationId != null)
                query = query.Where(q => q.SLAEscalationId.HasValue).Where(q => q.SLAEscalationId, filter.SLAEscalationId);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAEscalationPhoneDAO> OrFilter(IQueryable<SLAEscalationPhoneDAO> query, SLAEscalationPhoneFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAEscalationPhoneDAO> initQuery = query.Where(q => false);
            foreach (SLAEscalationPhoneFilter SLAEscalationPhoneFilter in filter.OrFilter)
            {
                IQueryable<SLAEscalationPhoneDAO> queryable = query;
                if (SLAEscalationPhoneFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAEscalationPhoneFilter.Id);
                if (SLAEscalationPhoneFilter.SLAEscalationId != null)
                    queryable = queryable.Where(q => q.SLAEscalationId.HasValue).Where(q => q.SLAEscalationId, SLAEscalationPhoneFilter.SLAEscalationId);
                if (SLAEscalationPhoneFilter.Phone != null)
                    queryable = queryable.Where(q => q.Phone, SLAEscalationPhoneFilter.Phone);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAEscalationPhoneDAO> DynamicOrder(IQueryable<SLAEscalationPhoneDAO> query, SLAEscalationPhoneFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationPhoneOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAEscalationPhoneOrder.SLAEscalation:
                            query = query.OrderBy(q => q.SLAEscalationId);
                            break;
                        case SLAEscalationPhoneOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationPhoneOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAEscalationPhoneOrder.SLAEscalation:
                            query = query.OrderByDescending(q => q.SLAEscalationId);
                            break;
                        case SLAEscalationPhoneOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAEscalationPhone>> DynamicSelect(IQueryable<SLAEscalationPhoneDAO> query, SLAEscalationPhoneFilter filter)
        {
            List<SLAEscalationPhone> SLAEscalationPhones = await query.Select(q => new SLAEscalationPhone()
            {
                Id = filter.Selects.Contains(SLAEscalationPhoneSelect.Id) ? q.Id : default(long),
                SLAEscalationId = filter.Selects.Contains(SLAEscalationPhoneSelect.SLAEscalation) ? q.SLAEscalationId : default(long?),
                Phone = filter.Selects.Contains(SLAEscalationPhoneSelect.Phone) ? q.Phone : default(string),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAEscalationPhones;
        }

        public async Task<int> Count(SLAEscalationPhoneFilter filter)
        {
            IQueryable<SLAEscalationPhoneDAO> SLAEscalationPhones = DataContext.SLAEscalationPhone.AsNoTracking();
            SLAEscalationPhones = DynamicFilter(SLAEscalationPhones, filter);
            return await SLAEscalationPhones.CountAsync();
        }

        public async Task<List<SLAEscalationPhone>> List(SLAEscalationPhoneFilter filter)
        {
            if (filter == null) return new List<SLAEscalationPhone>();
            IQueryable<SLAEscalationPhoneDAO> SLAEscalationPhoneDAOs = DataContext.SLAEscalationPhone.AsNoTracking();
            SLAEscalationPhoneDAOs = DynamicFilter(SLAEscalationPhoneDAOs, filter);
            SLAEscalationPhoneDAOs = DynamicOrder(SLAEscalationPhoneDAOs, filter);
            List<SLAEscalationPhone> SLAEscalationPhones = await DynamicSelect(SLAEscalationPhoneDAOs, filter);
            return SLAEscalationPhones;
        }

        public async Task<SLAEscalationPhone> Get(long Id)
        {
            SLAEscalationPhone SLAEscalationPhone = await DataContext.SLAEscalationPhone.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAEscalationPhone()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAEscalationId = x.SLAEscalationId,
                Phone = x.Phone,
            }).FirstOrDefaultAsync();

            if (SLAEscalationPhone == null)
                return null;

            return SLAEscalationPhone;
        }
        public async Task<bool> Create(SLAEscalationPhone SLAEscalationPhone)
        {
            SLAEscalationPhoneDAO SLAEscalationPhoneDAO = new SLAEscalationPhoneDAO();
            SLAEscalationPhoneDAO.Id = SLAEscalationPhone.Id;
            SLAEscalationPhoneDAO.SLAEscalationId = SLAEscalationPhone.SLAEscalationId;
            SLAEscalationPhoneDAO.Phone = SLAEscalationPhone.Phone;
            SLAEscalationPhoneDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAEscalationPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAEscalationPhone.Add(SLAEscalationPhoneDAO);
            await DataContext.SaveChangesAsync();
            SLAEscalationPhone.Id = SLAEscalationPhoneDAO.Id;
            await SaveReference(SLAEscalationPhone);
            return true;
        }

        public async Task<bool> Update(SLAEscalationPhone SLAEscalationPhone)
        {
            SLAEscalationPhoneDAO SLAEscalationPhoneDAO = DataContext.SLAEscalationPhone.Where(x => x.Id == SLAEscalationPhone.Id).FirstOrDefault();
            if (SLAEscalationPhoneDAO == null)
                return false;
            SLAEscalationPhoneDAO.Id = SLAEscalationPhone.Id;
            SLAEscalationPhoneDAO.SLAEscalationId = SLAEscalationPhone.SLAEscalationId;
            SLAEscalationPhoneDAO.Phone = SLAEscalationPhone.Phone;
            SLAEscalationPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAEscalationPhone);
            return true;
        }

        public async Task<bool> Delete(SLAEscalationPhone SLAEscalationPhone)
        {
            await DataContext.SLAEscalationPhone.Where(x => x.Id == SLAEscalationPhone.Id).UpdateFromQueryAsync(x => new SLAEscalationPhoneDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAEscalationPhone> SLAEscalationPhones)
        {
            List<SLAEscalationPhoneDAO> SLAEscalationPhoneDAOs = new List<SLAEscalationPhoneDAO>();
            foreach (SLAEscalationPhone SLAEscalationPhone in SLAEscalationPhones)
            {
                SLAEscalationPhoneDAO SLAEscalationPhoneDAO = new SLAEscalationPhoneDAO();
                SLAEscalationPhoneDAO.Id = SLAEscalationPhone.Id;
                SLAEscalationPhoneDAO.SLAEscalationId = SLAEscalationPhone.SLAEscalationId;
                SLAEscalationPhoneDAO.Phone = SLAEscalationPhone.Phone;
                SLAEscalationPhoneDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAEscalationPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAEscalationPhoneDAOs.Add(SLAEscalationPhoneDAO);
            }
            await DataContext.BulkMergeAsync(SLAEscalationPhoneDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAEscalationPhone> SLAEscalationPhones)
        {
            List<long> Ids = SLAEscalationPhones.Select(x => x.Id).ToList();
            await DataContext.SLAEscalationPhone
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAEscalationPhoneDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAEscalationPhone SLAEscalationPhone)
        {
        }
        
    }
}
