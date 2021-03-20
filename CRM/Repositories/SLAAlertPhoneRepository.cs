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
    public interface ISLAAlertPhoneRepository
    {
        Task<int> Count(SLAAlertPhoneFilter SLAAlertPhoneFilter);
        Task<List<SLAAlertPhone>> List(SLAAlertPhoneFilter SLAAlertPhoneFilter);
        Task<SLAAlertPhone> Get(long Id);
        Task<bool> Create(SLAAlertPhone SLAAlertPhone);
        Task<bool> Update(SLAAlertPhone SLAAlertPhone);
        Task<bool> Delete(SLAAlertPhone SLAAlertPhone);
        Task<bool> BulkMerge(List<SLAAlertPhone> SLAAlertPhones);
        Task<bool> BulkDelete(List<SLAAlertPhone> SLAAlertPhones);
    }
    public class SLAAlertPhoneRepository : ISLAAlertPhoneRepository
    {
        private DataContext DataContext;
        public SLAAlertPhoneRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAAlertPhoneDAO> DynamicFilter(IQueryable<SLAAlertPhoneDAO> query, SLAAlertPhoneFilter filter)
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
            if (filter.SLAAlertId != null)
                query = query.Where(q => q.SLAAlertId.HasValue).Where(q => q.SLAAlertId, filter.SLAAlertId);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAAlertPhoneDAO> OrFilter(IQueryable<SLAAlertPhoneDAO> query, SLAAlertPhoneFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAAlertPhoneDAO> initQuery = query.Where(q => false);
            foreach (SLAAlertPhoneFilter SLAAlertPhoneFilter in filter.OrFilter)
            {
                IQueryable<SLAAlertPhoneDAO> queryable = query;
                if (SLAAlertPhoneFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAAlertPhoneFilter.Id);
                if (SLAAlertPhoneFilter.SLAAlertId != null)
                    queryable = queryable.Where(q => q.SLAAlertId.HasValue).Where(q => q.SLAAlertId, SLAAlertPhoneFilter.SLAAlertId);
                if (SLAAlertPhoneFilter.Phone != null)
                    queryable = queryable.Where(q => q.Phone, SLAAlertPhoneFilter.Phone);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAAlertPhoneDAO> DynamicOrder(IQueryable<SLAAlertPhoneDAO> query, SLAAlertPhoneFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertPhoneOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAAlertPhoneOrder.SLAAlert:
                            query = query.OrderBy(q => q.SLAAlertId);
                            break;
                        case SLAAlertPhoneOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertPhoneOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAAlertPhoneOrder.SLAAlert:
                            query = query.OrderByDescending(q => q.SLAAlertId);
                            break;
                        case SLAAlertPhoneOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAAlertPhone>> DynamicSelect(IQueryable<SLAAlertPhoneDAO> query, SLAAlertPhoneFilter filter)
        {
            List<SLAAlertPhone> SLAAlertPhones = await query.Select(q => new SLAAlertPhone()
            {
                Id = filter.Selects.Contains(SLAAlertPhoneSelect.Id) ? q.Id : default(long),
                SLAAlertId = filter.Selects.Contains(SLAAlertPhoneSelect.SLAAlert) ? q.SLAAlertId : default(long?),
                Phone = filter.Selects.Contains(SLAAlertPhoneSelect.Phone) ? q.Phone : default(string),
                SLAAlert = filter.Selects.Contains(SLAAlertPhoneSelect.SLAAlert) && q.SLAAlert != null ? new SLAAlert
                {
                    Id = q.SLAAlert.Id,
                    TicketIssueLevelId = q.SLAAlert.TicketIssueLevelId,
                    IsNotification = q.SLAAlert.IsNotification,
                    IsMail = q.SLAAlert.IsMail,
                    IsSMS = q.SLAAlert.IsSMS,
                    Time = q.SLAAlert.Time,
                    TimeUnitId = q.SLAAlert.TimeUnitId,
                    IsAssignedToUser = q.SLAAlert.IsAssignedToUser,
                    IsAssignedToGroup = q.SLAAlert.IsAssignedToGroup,
                    SmsTemplateId = q.SLAAlert.SmsTemplateId,
                    MailTemplateId = q.SLAAlert.MailTemplateId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAAlertPhones;
        }

        public async Task<int> Count(SLAAlertPhoneFilter filter)
        {
            IQueryable<SLAAlertPhoneDAO> SLAAlertPhones = DataContext.SLAAlertPhone.AsNoTracking();
            SLAAlertPhones = DynamicFilter(SLAAlertPhones, filter);
            return await SLAAlertPhones.CountAsync();
        }

        public async Task<List<SLAAlertPhone>> List(SLAAlertPhoneFilter filter)
        {
            if (filter == null) return new List<SLAAlertPhone>();
            IQueryable<SLAAlertPhoneDAO> SLAAlertPhoneDAOs = DataContext.SLAAlertPhone.AsNoTracking();
            SLAAlertPhoneDAOs = DynamicFilter(SLAAlertPhoneDAOs, filter);
            SLAAlertPhoneDAOs = DynamicOrder(SLAAlertPhoneDAOs, filter);
            List<SLAAlertPhone> SLAAlertPhones = await DynamicSelect(SLAAlertPhoneDAOs, filter);
            return SLAAlertPhones;
        }

        public async Task<SLAAlertPhone> Get(long Id)
        {
            SLAAlertPhone SLAAlertPhone = await DataContext.SLAAlertPhone.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAAlertPhone()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAAlertId = x.SLAAlertId,
                Phone = x.Phone,
                SLAAlert = x.SLAAlert == null ? null : new SLAAlert
                {
                    Id = x.SLAAlert.Id,
                    TicketIssueLevelId = x.SLAAlert.TicketIssueLevelId,
                    IsNotification = x.SLAAlert.IsNotification,
                    IsMail = x.SLAAlert.IsMail,
                    IsSMS = x.SLAAlert.IsSMS,
                    Time = x.SLAAlert.Time,
                    TimeUnitId = x.SLAAlert.TimeUnitId,
                    IsAssignedToUser = x.SLAAlert.IsAssignedToUser,
                    IsAssignedToGroup = x.SLAAlert.IsAssignedToGroup,
                    SmsTemplateId = x.SLAAlert.SmsTemplateId,
                    MailTemplateId = x.SLAAlert.MailTemplateId,
                },
            }).FirstOrDefaultAsync();

            if (SLAAlertPhone == null)
                return null;

            return SLAAlertPhone;
        }
        public async Task<bool> Create(SLAAlertPhone SLAAlertPhone)
        {
            SLAAlertPhoneDAO SLAAlertPhoneDAO = new SLAAlertPhoneDAO();
            SLAAlertPhoneDAO.Id = SLAAlertPhone.Id;
            SLAAlertPhoneDAO.SLAAlertId = SLAAlertPhone.SLAAlertId;
            SLAAlertPhoneDAO.Phone = SLAAlertPhone.Phone;
            SLAAlertPhoneDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAAlertPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAAlertPhone.Add(SLAAlertPhoneDAO);
            await DataContext.SaveChangesAsync();
            SLAAlertPhone.Id = SLAAlertPhoneDAO.Id;
            await SaveReference(SLAAlertPhone);
            return true;
        }

        public async Task<bool> Update(SLAAlertPhone SLAAlertPhone)
        {
            SLAAlertPhoneDAO SLAAlertPhoneDAO = DataContext.SLAAlertPhone.Where(x => x.Id == SLAAlertPhone.Id).FirstOrDefault();
            if (SLAAlertPhoneDAO == null)
                return false;
            SLAAlertPhoneDAO.Id = SLAAlertPhone.Id;
            SLAAlertPhoneDAO.SLAAlertId = SLAAlertPhone.SLAAlertId;
            SLAAlertPhoneDAO.Phone = SLAAlertPhone.Phone;
            SLAAlertPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAAlertPhone);
            return true;
        }

        public async Task<bool> Delete(SLAAlertPhone SLAAlertPhone)
        {
            await DataContext.SLAAlertPhone.Where(x => x.Id == SLAAlertPhone.Id).UpdateFromQueryAsync(x => new SLAAlertPhoneDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAAlertPhone> SLAAlertPhones)
        {
            List<SLAAlertPhoneDAO> SLAAlertPhoneDAOs = new List<SLAAlertPhoneDAO>();
            foreach (SLAAlertPhone SLAAlertPhone in SLAAlertPhones)
            {
                SLAAlertPhoneDAO SLAAlertPhoneDAO = new SLAAlertPhoneDAO();
                SLAAlertPhoneDAO.Id = SLAAlertPhone.Id;
                SLAAlertPhoneDAO.SLAAlertId = SLAAlertPhone.SLAAlertId;
                SLAAlertPhoneDAO.Phone = SLAAlertPhone.Phone;
                SLAAlertPhoneDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAAlertPhoneDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAAlertPhoneDAOs.Add(SLAAlertPhoneDAO);
            }
            await DataContext.BulkMergeAsync(SLAAlertPhoneDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAAlertPhone> SLAAlertPhones)
        {
            List<long> Ids = SLAAlertPhones.Select(x => x.Id).ToList();
            await DataContext.SLAAlertPhone
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAAlertPhoneDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAAlertPhone SLAAlertPhone)
        {
        }
        
    }
}
