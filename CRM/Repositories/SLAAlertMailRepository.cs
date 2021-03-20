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
    public interface ISLAAlertMailRepository
    {
        Task<int> Count(SLAAlertMailFilter SLAAlertMailFilter);
        Task<List<SLAAlertMail>> List(SLAAlertMailFilter SLAAlertMailFilter);
        Task<SLAAlertMail> Get(long Id);
        Task<bool> Create(SLAAlertMail SLAAlertMail);
        Task<bool> Update(SLAAlertMail SLAAlertMail);
        Task<bool> Delete(SLAAlertMail SLAAlertMail);
        Task<bool> BulkMerge(List<SLAAlertMail> SLAAlertMails);
        Task<bool> BulkDelete(List<SLAAlertMail> SLAAlertMails);
    }
    public class SLAAlertMailRepository : ISLAAlertMailRepository
    {
        private DataContext DataContext;
        public SLAAlertMailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAAlertMailDAO> DynamicFilter(IQueryable<SLAAlertMailDAO> query, SLAAlertMailFilter filter)
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
            if (filter.Mail != null)
                query = query.Where(q => q.Mail, filter.Mail);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAAlertMailDAO> OrFilter(IQueryable<SLAAlertMailDAO> query, SLAAlertMailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAAlertMailDAO> initQuery = query.Where(q => false);
            foreach (SLAAlertMailFilter SLAAlertMailFilter in filter.OrFilter)
            {
                IQueryable<SLAAlertMailDAO> queryable = query;
                if (SLAAlertMailFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAAlertMailFilter.Id);
                if (SLAAlertMailFilter.SLAAlertId != null)
                    queryable = queryable.Where(q => q.SLAAlertId.HasValue).Where(q => q.SLAAlertId, SLAAlertMailFilter.SLAAlertId);
                if (SLAAlertMailFilter.Mail != null)
                    queryable = queryable.Where(q => q.Mail, SLAAlertMailFilter.Mail);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAAlertMailDAO> DynamicOrder(IQueryable<SLAAlertMailDAO> query, SLAAlertMailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertMailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAAlertMailOrder.SLAAlert:
                            query = query.OrderBy(q => q.SLAAlertId);
                            break;
                        case SLAAlertMailOrder.Mail:
                            query = query.OrderBy(q => q.Mail);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertMailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAAlertMailOrder.SLAAlert:
                            query = query.OrderByDescending(q => q.SLAAlertId);
                            break;
                        case SLAAlertMailOrder.Mail:
                            query = query.OrderByDescending(q => q.Mail);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAAlertMail>> DynamicSelect(IQueryable<SLAAlertMailDAO> query, SLAAlertMailFilter filter)
        {
            List<SLAAlertMail> SLAAlertMails = await query.Select(q => new SLAAlertMail()
            {
                Id = filter.Selects.Contains(SLAAlertMailSelect.Id) ? q.Id : default(long),
                SLAAlertId = filter.Selects.Contains(SLAAlertMailSelect.SLAAlert) ? q.SLAAlertId : default(long?),
                Mail = filter.Selects.Contains(SLAAlertMailSelect.Mail) ? q.Mail : default(string),
                SLAAlert = filter.Selects.Contains(SLAAlertMailSelect.SLAAlert) && q.SLAAlert != null ? new SLAAlert
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
            return SLAAlertMails;
        }

        public async Task<int> Count(SLAAlertMailFilter filter)
        {
            IQueryable<SLAAlertMailDAO> SLAAlertMails = DataContext.SLAAlertMail.AsNoTracking();
            SLAAlertMails = DynamicFilter(SLAAlertMails, filter);
            return await SLAAlertMails.CountAsync();
        }

        public async Task<List<SLAAlertMail>> List(SLAAlertMailFilter filter)
        {
            if (filter == null) return new List<SLAAlertMail>();
            IQueryable<SLAAlertMailDAO> SLAAlertMailDAOs = DataContext.SLAAlertMail.AsNoTracking();
            SLAAlertMailDAOs = DynamicFilter(SLAAlertMailDAOs, filter);
            SLAAlertMailDAOs = DynamicOrder(SLAAlertMailDAOs, filter);
            List<SLAAlertMail> SLAAlertMails = await DynamicSelect(SLAAlertMailDAOs, filter);
            return SLAAlertMails;
        }

        public async Task<SLAAlertMail> Get(long Id)
        {
            SLAAlertMail SLAAlertMail = await DataContext.SLAAlertMail.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAAlertMail()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAAlertId = x.SLAAlertId,
                Mail = x.Mail,
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

            if (SLAAlertMail == null)
                return null;

            return SLAAlertMail;
        }
        public async Task<bool> Create(SLAAlertMail SLAAlertMail)
        {
            SLAAlertMailDAO SLAAlertMailDAO = new SLAAlertMailDAO();
            SLAAlertMailDAO.Id = SLAAlertMail.Id;
            SLAAlertMailDAO.SLAAlertId = SLAAlertMail.SLAAlertId;
            SLAAlertMailDAO.Mail = SLAAlertMail.Mail;
            SLAAlertMailDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAAlertMailDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAAlertMail.Add(SLAAlertMailDAO);
            await DataContext.SaveChangesAsync();
            SLAAlertMail.Id = SLAAlertMailDAO.Id;
            await SaveReference(SLAAlertMail);
            return true;
        }

        public async Task<bool> Update(SLAAlertMail SLAAlertMail)
        {
            SLAAlertMailDAO SLAAlertMailDAO = DataContext.SLAAlertMail.Where(x => x.Id == SLAAlertMail.Id).FirstOrDefault();
            if (SLAAlertMailDAO == null)
                return false;
            SLAAlertMailDAO.Id = SLAAlertMail.Id;
            SLAAlertMailDAO.SLAAlertId = SLAAlertMail.SLAAlertId;
            SLAAlertMailDAO.Mail = SLAAlertMail.Mail;
            SLAAlertMailDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAAlertMail);
            return true;
        }

        public async Task<bool> Delete(SLAAlertMail SLAAlertMail)
        {
            await DataContext.SLAAlertMail.Where(x => x.Id == SLAAlertMail.Id).UpdateFromQueryAsync(x => new SLAAlertMailDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAAlertMail> SLAAlertMails)
        {
            List<SLAAlertMailDAO> SLAAlertMailDAOs = new List<SLAAlertMailDAO>();
            foreach (SLAAlertMail SLAAlertMail in SLAAlertMails)
            {
                SLAAlertMailDAO SLAAlertMailDAO = new SLAAlertMailDAO();
                SLAAlertMailDAO.Id = SLAAlertMail.Id;
                SLAAlertMailDAO.SLAAlertId = SLAAlertMail.SLAAlertId;
                SLAAlertMailDAO.Mail = SLAAlertMail.Mail;
                SLAAlertMailDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAAlertMailDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAAlertMailDAOs.Add(SLAAlertMailDAO);
            }
            await DataContext.BulkMergeAsync(SLAAlertMailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAAlertMail> SLAAlertMails)
        {
            List<long> Ids = SLAAlertMails.Select(x => x.Id).ToList();
            await DataContext.SLAAlertMail
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAAlertMailDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAAlertMail SLAAlertMail)
        {
        }
        
    }
}
