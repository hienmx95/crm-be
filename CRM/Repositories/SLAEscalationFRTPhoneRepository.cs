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
    public interface ISLAEscalationFRTPhoneRepository
    {
        Task<int> Count(SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter);
        Task<List<SLAEscalationFRTPhone>> List(SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter);
        Task<SLAEscalationFRTPhone> Get(long Id);
        Task<bool> Create(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<bool> Update(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<bool> Delete(SLAEscalationFRTPhone SLAEscalationFRTPhone);
        Task<bool> BulkMerge(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones);
        Task<bool> BulkDelete(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones);
    }
    public class SLAEscalationFRTPhoneRepository : ISLAEscalationFRTPhoneRepository
    {
        private DataContext DataContext;
        public SLAEscalationFRTPhoneRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAEscalationFRTPhoneDAO> DynamicFilter(IQueryable<SLAEscalationFRTPhoneDAO> query, SLAEscalationFRTPhoneFilter filter)
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
            if (filter.SLAEscalationFRTId != null)
                query = query.Where(q => q.SLAEscalationFRTId.HasValue).Where(q => q.SLAEscalationFRTId, filter.SLAEscalationFRTId);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAEscalationFRTPhoneDAO> OrFilter(IQueryable<SLAEscalationFRTPhoneDAO> query, SLAEscalationFRTPhoneFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAEscalationFRTPhoneDAO> initQuery = query.Where(q => false);
            foreach (SLAEscalationFRTPhoneFilter SLAEscalationFRTPhoneFilter in filter.OrFilter)
            {
                IQueryable<SLAEscalationFRTPhoneDAO> queryable = query;
                if (SLAEscalationFRTPhoneFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAEscalationFRTPhoneFilter.Id);
                if (SLAEscalationFRTPhoneFilter.SLAEscalationFRTId != null)
                    queryable = queryable.Where(q => q.SLAEscalationFRTId.HasValue).Where(q => q.SLAEscalationFRTId, SLAEscalationFRTPhoneFilter.SLAEscalationFRTId);
                if (SLAEscalationFRTPhoneFilter.Phone != null)
                    queryable = queryable.Where(q => q.Phone, SLAEscalationFRTPhoneFilter.Phone);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAEscalationFRTPhoneDAO> DynamicOrder(IQueryable<SLAEscalationFRTPhoneDAO> query, SLAEscalationFRTPhoneFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationFRTPhoneOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAEscalationFRTPhoneOrder.SLAEscalationFRT:
                            query = query.OrderBy(q => q.SLAEscalationFRTId);
                            break;
                        case SLAEscalationFRTPhoneOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationFRTPhoneOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAEscalationFRTPhoneOrder.SLAEscalationFRT:
                            query = query.OrderByDescending(q => q.SLAEscalationFRTId);
                            break;
                        case SLAEscalationFRTPhoneOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAEscalationFRTPhone>> DynamicSelect(IQueryable<SLAEscalationFRTPhoneDAO> query, SLAEscalationFRTPhoneFilter filter)
        {
            List<SLAEscalationFRTPhone> SLAEscalationFRTPhones = await query.Select(q => new SLAEscalationFRTPhone()
            {
                Id = filter.Selects.Contains(SLAEscalationFRTPhoneSelect.Id) ? q.Id : default(long),
                SLAEscalationFRTId = filter.Selects.Contains(SLAEscalationFRTPhoneSelect.SLAEscalationFRT) ? q.SLAEscalationFRTId : default(long?),
                Phone = filter.Selects.Contains(SLAEscalationFRTPhoneSelect.Phone) ? q.Phone : default(string),
                SLAEscalationFRT = filter.Selects.Contains(SLAEscalationFRTPhoneSelect.SLAEscalationFRT) && q.SLAEscalationFRT != null ? new SLAEscalationFRT
                {
                    Id = q.SLAEscalationFRT.Id,
                    TicketIssueLevelId = q.SLAEscalationFRT.TicketIssueLevelId,
                    IsNotification = q.SLAEscalationFRT.IsNotification,
                    IsMail = q.SLAEscalationFRT.IsMail,
                    IsSMS = q.SLAEscalationFRT.IsSMS,
                    Time = q.SLAEscalationFRT.Time,
                    TimeUnitId = q.SLAEscalationFRT.TimeUnitId,
                    IsAssignedToUser = q.SLAEscalationFRT.IsAssignedToUser,
                    IsAssignedToGroup = q.SLAEscalationFRT.IsAssignedToGroup,
                    SmsTemplateId = q.SLAEscalationFRT.SmsTemplateId,
                    MailTemplateId = q.SLAEscalationFRT.MailTemplateId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAEscalationFRTPhones;
        }

        public async Task<int> Count(SLAEscalationFRTPhoneFilter filter)
        {
            IQueryable<SLAEscalationFRTPhoneDAO> SLAEscalationFRTPhones = DataContext.SLAEscalationFRTPhone.AsNoTracking();
            SLAEscalationFRTPhones = DynamicFilter(SLAEscalationFRTPhones, filter);
            return await SLAEscalationFRTPhones.CountAsync();
        }

        public async Task<List<SLAEscalationFRTPhone>> List(SLAEscalationFRTPhoneFilter filter)
        {
            if (filter == null) return new List<SLAEscalationFRTPhone>();
            IQueryable<SLAEscalationFRTPhoneDAO> SLAEscalationFRTPhoneDAOs = DataContext.SLAEscalationFRTPhone.AsNoTracking();
            SLAEscalationFRTPhoneDAOs = DynamicFilter(SLAEscalationFRTPhoneDAOs, filter);
            SLAEscalationFRTPhoneDAOs = DynamicOrder(SLAEscalationFRTPhoneDAOs, filter);
            List<SLAEscalationFRTPhone> SLAEscalationFRTPhones = await DynamicSelect(SLAEscalationFRTPhoneDAOs, filter);
            return SLAEscalationFRTPhones;
        }

        public async Task<SLAEscalationFRTPhone> Get(long Id)
        {
            SLAEscalationFRTPhone SLAEscalationFRTPhone = await DataContext.SLAEscalationFRTPhone.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAEscalationFRTPhone()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAEscalationFRTId = x.SLAEscalationFRTId,
                Phone = x.Phone,
                SLAEscalationFRT = x.SLAEscalationFRT == null ? null : new SLAEscalationFRT
                {
                    Id = x.SLAEscalationFRT.Id,
                    TicketIssueLevelId = x.SLAEscalationFRT.TicketIssueLevelId,
                    IsNotification = x.SLAEscalationFRT.IsNotification,
                    IsMail = x.SLAEscalationFRT.IsMail,
                    IsSMS = x.SLAEscalationFRT.IsSMS,
                    Time = x.SLAEscalationFRT.Time,
                    TimeUnitId = x.SLAEscalationFRT.TimeUnitId,
                    IsAssignedToUser = x.SLAEscalationFRT.IsAssignedToUser,
                    IsAssignedToGroup = x.SLAEscalationFRT.IsAssignedToGroup,
                    SmsTemplateId = x.SLAEscalationFRT.SmsTemplateId,
                    MailTemplateId = x.SLAEscalationFRT.MailTemplateId,
                },
            }).FirstOrDefaultAsync();

            if (SLAEscalationFRTPhone == null)
                return null;

            return SLAEscalationFRTPhone;
        }
        public async Task<bool> Create(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            SLAEscalationFRTPhoneDAO SLAEscalationFRTPhoneDAO = new SLAEscalationFRTPhoneDAO();
            SLAEscalationFRTPhoneDAO.Id = SLAEscalationFRTPhone.Id;
            SLAEscalationFRTPhoneDAO.SLAEscalationFRTId = SLAEscalationFRTPhone.SLAEscalationFRTId;
            SLAEscalationFRTPhoneDAO.Phone = SLAEscalationFRTPhone.Phone;
            SLAEscalationFRTPhoneDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAEscalationFRTPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAEscalationFRTPhone.Add(SLAEscalationFRTPhoneDAO);
            await DataContext.SaveChangesAsync();
            SLAEscalationFRTPhone.Id = SLAEscalationFRTPhoneDAO.Id;
            await SaveReference(SLAEscalationFRTPhone);
            return true;
        }

        public async Task<bool> Update(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            SLAEscalationFRTPhoneDAO SLAEscalationFRTPhoneDAO = DataContext.SLAEscalationFRTPhone.Where(x => x.Id == SLAEscalationFRTPhone.Id).FirstOrDefault();
            if (SLAEscalationFRTPhoneDAO == null)
                return false;
            SLAEscalationFRTPhoneDAO.Id = SLAEscalationFRTPhone.Id;
            SLAEscalationFRTPhoneDAO.SLAEscalationFRTId = SLAEscalationFRTPhone.SLAEscalationFRTId;
            SLAEscalationFRTPhoneDAO.Phone = SLAEscalationFRTPhone.Phone;
            SLAEscalationFRTPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAEscalationFRTPhone);
            return true;
        }

        public async Task<bool> Delete(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            await DataContext.SLAEscalationFRTPhone.Where(x => x.Id == SLAEscalationFRTPhone.Id).UpdateFromQueryAsync(x => new SLAEscalationFRTPhoneDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones)
        {
            List<SLAEscalationFRTPhoneDAO> SLAEscalationFRTPhoneDAOs = new List<SLAEscalationFRTPhoneDAO>();
            foreach (SLAEscalationFRTPhone SLAEscalationFRTPhone in SLAEscalationFRTPhones)
            {
                SLAEscalationFRTPhoneDAO SLAEscalationFRTPhoneDAO = new SLAEscalationFRTPhoneDAO();
                SLAEscalationFRTPhoneDAO.Id = SLAEscalationFRTPhone.Id;
                SLAEscalationFRTPhoneDAO.SLAEscalationFRTId = SLAEscalationFRTPhone.SLAEscalationFRTId;
                SLAEscalationFRTPhoneDAO.Phone = SLAEscalationFRTPhone.Phone;
                SLAEscalationFRTPhoneDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAEscalationFRTPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAEscalationFRTPhoneDAOs.Add(SLAEscalationFRTPhoneDAO);
            }
            await DataContext.BulkMergeAsync(SLAEscalationFRTPhoneDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAEscalationFRTPhone> SLAEscalationFRTPhones)
        {
            List<long> Ids = SLAEscalationFRTPhones.Select(x => x.Id).ToList();
            await DataContext.SLAEscalationFRTPhone
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAEscalationFRTPhoneDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
        }
        
    }
}
