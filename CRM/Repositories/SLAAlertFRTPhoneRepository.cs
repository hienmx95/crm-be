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
    public interface ISLAAlertFRTPhoneRepository
    {
        Task<int> Count(SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter);
        Task<List<SLAAlertFRTPhone>> List(SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter);
        Task<SLAAlertFRTPhone> Get(long Id);
        Task<bool> Create(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<bool> Update(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<bool> Delete(SLAAlertFRTPhone SLAAlertFRTPhone);
        Task<bool> BulkMerge(List<SLAAlertFRTPhone> SLAAlertFRTPhones);
        Task<bool> BulkDelete(List<SLAAlertFRTPhone> SLAAlertFRTPhones);
    }
    public class SLAAlertFRTPhoneRepository : ISLAAlertFRTPhoneRepository
    {
        private DataContext DataContext;
        public SLAAlertFRTPhoneRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAAlertFRTPhoneDAO> DynamicFilter(IQueryable<SLAAlertFRTPhoneDAO> query, SLAAlertFRTPhoneFilter filter)
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
            if (filter.SLAAlertFRTId != null)
                query = query.Where(q => q.SLAAlertFRTId.HasValue).Where(q => q.SLAAlertFRTId, filter.SLAAlertFRTId);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAAlertFRTPhoneDAO> OrFilter(IQueryable<SLAAlertFRTPhoneDAO> query, SLAAlertFRTPhoneFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAAlertFRTPhoneDAO> initQuery = query.Where(q => false);
            foreach (SLAAlertFRTPhoneFilter SLAAlertFRTPhoneFilter in filter.OrFilter)
            {
                IQueryable<SLAAlertFRTPhoneDAO> queryable = query;
                if (SLAAlertFRTPhoneFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAAlertFRTPhoneFilter.Id);
                if (SLAAlertFRTPhoneFilter.SLAAlertFRTId != null)
                    queryable = queryable.Where(q => q.SLAAlertFRTId.HasValue).Where(q => q.SLAAlertFRTId, SLAAlertFRTPhoneFilter.SLAAlertFRTId);
                if (SLAAlertFRTPhoneFilter.Phone != null)
                    queryable = queryable.Where(q => q.Phone, SLAAlertFRTPhoneFilter.Phone);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAAlertFRTPhoneDAO> DynamicOrder(IQueryable<SLAAlertFRTPhoneDAO> query, SLAAlertFRTPhoneFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertFRTPhoneOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAAlertFRTPhoneOrder.SLAAlertFRT:
                            query = query.OrderBy(q => q.SLAAlertFRTId);
                            break;
                        case SLAAlertFRTPhoneOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertFRTPhoneOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAAlertFRTPhoneOrder.SLAAlertFRT:
                            query = query.OrderByDescending(q => q.SLAAlertFRTId);
                            break;
                        case SLAAlertFRTPhoneOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAAlertFRTPhone>> DynamicSelect(IQueryable<SLAAlertFRTPhoneDAO> query, SLAAlertFRTPhoneFilter filter)
        {
            List<SLAAlertFRTPhone> SLAAlertFRTPhones = await query.Select(q => new SLAAlertFRTPhone()
            {
                Id = filter.Selects.Contains(SLAAlertFRTPhoneSelect.Id) ? q.Id : default(long),
                SLAAlertFRTId = filter.Selects.Contains(SLAAlertFRTPhoneSelect.SLAAlertFRT) ? q.SLAAlertFRTId : default(long?),
                Phone = filter.Selects.Contains(SLAAlertFRTPhoneSelect.Phone) ? q.Phone : default(string),
                SLAAlertFRT = filter.Selects.Contains(SLAAlertFRTPhoneSelect.SLAAlertFRT) && q.SLAAlertFRT != null ? new SLAAlertFRT
                {
                    Id = q.SLAAlertFRT.Id,
                    TicketIssueLevelId = q.SLAAlertFRT.TicketIssueLevelId,
                    IsNotification = q.SLAAlertFRT.IsNotification,
                    IsMail = q.SLAAlertFRT.IsMail,
                    IsSMS = q.SLAAlertFRT.IsSMS,
                    Time = q.SLAAlertFRT.Time,
                    TimeUnitId = q.SLAAlertFRT.TimeUnitId,
                    IsAssignedToUser = q.SLAAlertFRT.IsAssignedToUser,
                    IsAssignedToGroup = q.SLAAlertFRT.IsAssignedToGroup,
                    SmsTemplateId = q.SLAAlertFRT.SmsTemplateId,
                    MailTemplateId = q.SLAAlertFRT.MailTemplateId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAAlertFRTPhones;
        }

        public async Task<int> Count(SLAAlertFRTPhoneFilter filter)
        {
            IQueryable<SLAAlertFRTPhoneDAO> SLAAlertFRTPhones = DataContext.SLAAlertFRTPhone.AsNoTracking();
            SLAAlertFRTPhones = DynamicFilter(SLAAlertFRTPhones, filter);
            return await SLAAlertFRTPhones.CountAsync();
        }

        public async Task<List<SLAAlertFRTPhone>> List(SLAAlertFRTPhoneFilter filter)
        {
            if (filter == null) return new List<SLAAlertFRTPhone>();
            IQueryable<SLAAlertFRTPhoneDAO> SLAAlertFRTPhoneDAOs = DataContext.SLAAlertFRTPhone.AsNoTracking();
            SLAAlertFRTPhoneDAOs = DynamicFilter(SLAAlertFRTPhoneDAOs, filter);
            SLAAlertFRTPhoneDAOs = DynamicOrder(SLAAlertFRTPhoneDAOs, filter);
            List<SLAAlertFRTPhone> SLAAlertFRTPhones = await DynamicSelect(SLAAlertFRTPhoneDAOs, filter);
            return SLAAlertFRTPhones;
        }

        public async Task<SLAAlertFRTPhone> Get(long Id)
        {
            SLAAlertFRTPhone SLAAlertFRTPhone = await DataContext.SLAAlertFRTPhone.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAAlertFRTPhone()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAAlertFRTId = x.SLAAlertFRTId,
                Phone = x.Phone,
                SLAAlertFRT = x.SLAAlertFRT == null ? null : new SLAAlertFRT
                {
                    Id = x.SLAAlertFRT.Id,
                    TicketIssueLevelId = x.SLAAlertFRT.TicketIssueLevelId,
                    IsNotification = x.SLAAlertFRT.IsNotification,
                    IsMail = x.SLAAlertFRT.IsMail,
                    IsSMS = x.SLAAlertFRT.IsSMS,
                    Time = x.SLAAlertFRT.Time,
                    TimeUnitId = x.SLAAlertFRT.TimeUnitId,
                    IsAssignedToUser = x.SLAAlertFRT.IsAssignedToUser,
                    IsAssignedToGroup = x.SLAAlertFRT.IsAssignedToGroup,
                    SmsTemplateId = x.SLAAlertFRT.SmsTemplateId,
                    MailTemplateId = x.SLAAlertFRT.MailTemplateId,
                },
            }).FirstOrDefaultAsync();

            if (SLAAlertFRTPhone == null)
                return null;

            return SLAAlertFRTPhone;
        }
        public async Task<bool> Create(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            SLAAlertFRTPhoneDAO SLAAlertFRTPhoneDAO = new SLAAlertFRTPhoneDAO();
            SLAAlertFRTPhoneDAO.Id = SLAAlertFRTPhone.Id;
            SLAAlertFRTPhoneDAO.SLAAlertFRTId = SLAAlertFRTPhone.SLAAlertFRTId;
            SLAAlertFRTPhoneDAO.Phone = SLAAlertFRTPhone.Phone;
            SLAAlertFRTPhoneDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAAlertFRTPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAAlertFRTPhone.Add(SLAAlertFRTPhoneDAO);
            await DataContext.SaveChangesAsync();
            SLAAlertFRTPhone.Id = SLAAlertFRTPhoneDAO.Id;
            await SaveReference(SLAAlertFRTPhone);
            return true;
        }

        public async Task<bool> Update(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            SLAAlertFRTPhoneDAO SLAAlertFRTPhoneDAO = DataContext.SLAAlertFRTPhone.Where(x => x.Id == SLAAlertFRTPhone.Id).FirstOrDefault();
            if (SLAAlertFRTPhoneDAO == null)
                return false;
            SLAAlertFRTPhoneDAO.Id = SLAAlertFRTPhone.Id;
            SLAAlertFRTPhoneDAO.SLAAlertFRTId = SLAAlertFRTPhone.SLAAlertFRTId;
            SLAAlertFRTPhoneDAO.Phone = SLAAlertFRTPhone.Phone;
            SLAAlertFRTPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAAlertFRTPhone);
            return true;
        }

        public async Task<bool> Delete(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            await DataContext.SLAAlertFRTPhone.Where(x => x.Id == SLAAlertFRTPhone.Id).UpdateFromQueryAsync(x => new SLAAlertFRTPhoneDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAAlertFRTPhone> SLAAlertFRTPhones)
        {
            List<SLAAlertFRTPhoneDAO> SLAAlertFRTPhoneDAOs = new List<SLAAlertFRTPhoneDAO>();
            foreach (SLAAlertFRTPhone SLAAlertFRTPhone in SLAAlertFRTPhones)
            {
                SLAAlertFRTPhoneDAO SLAAlertFRTPhoneDAO = new SLAAlertFRTPhoneDAO();
                SLAAlertFRTPhoneDAO.Id = SLAAlertFRTPhone.Id;
                SLAAlertFRTPhoneDAO.SLAAlertFRTId = SLAAlertFRTPhone.SLAAlertFRTId;
                SLAAlertFRTPhoneDAO.Phone = SLAAlertFRTPhone.Phone;
                SLAAlertFRTPhoneDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAAlertFRTPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAAlertFRTPhoneDAOs.Add(SLAAlertFRTPhoneDAO);
            }
            await DataContext.BulkMergeAsync(SLAAlertFRTPhoneDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAAlertFRTPhone> SLAAlertFRTPhones)
        {
            List<long> Ids = SLAAlertFRTPhones.Select(x => x.Id).ToList();
            await DataContext.SLAAlertFRTPhone
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAAlertFRTPhoneDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
        }
        
    }
}
