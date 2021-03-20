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
    public interface ISLAAlertFRTMailRepository
    {
        Task<int> Count(SLAAlertFRTMailFilter SLAAlertFRTMailFilter);
        Task<List<SLAAlertFRTMail>> List(SLAAlertFRTMailFilter SLAAlertFRTMailFilter);
        Task<SLAAlertFRTMail> Get(long Id);
        Task<bool> Create(SLAAlertFRTMail SLAAlertFRTMail);
        Task<bool> Update(SLAAlertFRTMail SLAAlertFRTMail);
        Task<bool> Delete(SLAAlertFRTMail SLAAlertFRTMail);
        Task<bool> BulkMerge(List<SLAAlertFRTMail> SLAAlertFRTMails);
        Task<bool> BulkDelete(List<SLAAlertFRTMail> SLAAlertFRTMails);
    }
    public class SLAAlertFRTMailRepository : ISLAAlertFRTMailRepository
    {
        private DataContext DataContext;
        public SLAAlertFRTMailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAAlertFRTMailDAO> DynamicFilter(IQueryable<SLAAlertFRTMailDAO> query, SLAAlertFRTMailFilter filter)
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
            if (filter.Mail != null)
                query = query.Where(q => q.Mail, filter.Mail);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAAlertFRTMailDAO> OrFilter(IQueryable<SLAAlertFRTMailDAO> query, SLAAlertFRTMailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAAlertFRTMailDAO> initQuery = query.Where(q => false);
            foreach (SLAAlertFRTMailFilter SLAAlertFRTMailFilter in filter.OrFilter)
            {
                IQueryable<SLAAlertFRTMailDAO> queryable = query;
                if (SLAAlertFRTMailFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAAlertFRTMailFilter.Id);
                if (SLAAlertFRTMailFilter.SLAAlertFRTId != null)
                    queryable = queryable.Where(q => q.SLAAlertFRTId.HasValue).Where(q => q.SLAAlertFRTId, SLAAlertFRTMailFilter.SLAAlertFRTId);
                if (SLAAlertFRTMailFilter.Mail != null)
                    queryable = queryable.Where(q => q.Mail, SLAAlertFRTMailFilter.Mail);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAAlertFRTMailDAO> DynamicOrder(IQueryable<SLAAlertFRTMailDAO> query, SLAAlertFRTMailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertFRTMailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAAlertFRTMailOrder.SLAAlertFRT:
                            query = query.OrderBy(q => q.SLAAlertFRTId);
                            break;
                        case SLAAlertFRTMailOrder.Mail:
                            query = query.OrderBy(q => q.Mail);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAAlertFRTMailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAAlertFRTMailOrder.SLAAlertFRT:
                            query = query.OrderByDescending(q => q.SLAAlertFRTId);
                            break;
                        case SLAAlertFRTMailOrder.Mail:
                            query = query.OrderByDescending(q => q.Mail);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAAlertFRTMail>> DynamicSelect(IQueryable<SLAAlertFRTMailDAO> query, SLAAlertFRTMailFilter filter)
        {
            List<SLAAlertFRTMail> SLAAlertFRTMails = await query.Select(q => new SLAAlertFRTMail()
            {
                Id = filter.Selects.Contains(SLAAlertFRTMailSelect.Id) ? q.Id : default(long),
                SLAAlertFRTId = filter.Selects.Contains(SLAAlertFRTMailSelect.SLAAlertFRT) ? q.SLAAlertFRTId : default(long?),
                Mail = filter.Selects.Contains(SLAAlertFRTMailSelect.Mail) ? q.Mail : default(string),
                SLAAlertFRT = filter.Selects.Contains(SLAAlertFRTMailSelect.SLAAlertFRT) && q.SLAAlertFRT != null ? new SLAAlertFRT
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
            return SLAAlertFRTMails;
        }

        public async Task<int> Count(SLAAlertFRTMailFilter filter)
        {
            IQueryable<SLAAlertFRTMailDAO> SLAAlertFRTMails = DataContext.SLAAlertFRTMail.AsNoTracking();
            SLAAlertFRTMails = DynamicFilter(SLAAlertFRTMails, filter);
            return await SLAAlertFRTMails.CountAsync();
        }

        public async Task<List<SLAAlertFRTMail>> List(SLAAlertFRTMailFilter filter)
        {
            if (filter == null) return new List<SLAAlertFRTMail>();
            IQueryable<SLAAlertFRTMailDAO> SLAAlertFRTMailDAOs = DataContext.SLAAlertFRTMail.AsNoTracking();
            SLAAlertFRTMailDAOs = DynamicFilter(SLAAlertFRTMailDAOs, filter);
            SLAAlertFRTMailDAOs = DynamicOrder(SLAAlertFRTMailDAOs, filter);
            List<SLAAlertFRTMail> SLAAlertFRTMails = await DynamicSelect(SLAAlertFRTMailDAOs, filter);
            return SLAAlertFRTMails;
        }

        public async Task<SLAAlertFRTMail> Get(long Id)
        {
            SLAAlertFRTMail SLAAlertFRTMail = await DataContext.SLAAlertFRTMail.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAAlertFRTMail()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAAlertFRTId = x.SLAAlertFRTId,
                Mail = x.Mail,
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

            if (SLAAlertFRTMail == null)
                return null;

            return SLAAlertFRTMail;
        }
        public async Task<bool> Create(SLAAlertFRTMail SLAAlertFRTMail)
        {
            SLAAlertFRTMailDAO SLAAlertFRTMailDAO = new SLAAlertFRTMailDAO();
            SLAAlertFRTMailDAO.Id = SLAAlertFRTMail.Id;
            SLAAlertFRTMailDAO.SLAAlertFRTId = SLAAlertFRTMail.SLAAlertFRTId;
            SLAAlertFRTMailDAO.Mail = SLAAlertFRTMail.Mail;
            SLAAlertFRTMailDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAAlertFRTMailDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAAlertFRTMail.Add(SLAAlertFRTMailDAO);
            await DataContext.SaveChangesAsync();
            SLAAlertFRTMail.Id = SLAAlertFRTMailDAO.Id;
            await SaveReference(SLAAlertFRTMail);
            return true;
        }

        public async Task<bool> Update(SLAAlertFRTMail SLAAlertFRTMail)
        {
            SLAAlertFRTMailDAO SLAAlertFRTMailDAO = DataContext.SLAAlertFRTMail.Where(x => x.Id == SLAAlertFRTMail.Id).FirstOrDefault();
            if (SLAAlertFRTMailDAO == null)
                return false;
            SLAAlertFRTMailDAO.Id = SLAAlertFRTMail.Id;
            SLAAlertFRTMailDAO.SLAAlertFRTId = SLAAlertFRTMail.SLAAlertFRTId;
            SLAAlertFRTMailDAO.Mail = SLAAlertFRTMail.Mail;
            SLAAlertFRTMailDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAAlertFRTMail);
            return true;
        }

        public async Task<bool> Delete(SLAAlertFRTMail SLAAlertFRTMail)
        {
            await DataContext.SLAAlertFRTMail.Where(x => x.Id == SLAAlertFRTMail.Id).UpdateFromQueryAsync(x => new SLAAlertFRTMailDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAAlertFRTMail> SLAAlertFRTMails)
        {
            List<SLAAlertFRTMailDAO> SLAAlertFRTMailDAOs = new List<SLAAlertFRTMailDAO>();
            foreach (SLAAlertFRTMail SLAAlertFRTMail in SLAAlertFRTMails)
            {
                SLAAlertFRTMailDAO SLAAlertFRTMailDAO = new SLAAlertFRTMailDAO();
                SLAAlertFRTMailDAO.Id = SLAAlertFRTMail.Id;
                SLAAlertFRTMailDAO.SLAAlertFRTId = SLAAlertFRTMail.SLAAlertFRTId;
                SLAAlertFRTMailDAO.Mail = SLAAlertFRTMail.Mail;
                SLAAlertFRTMailDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAAlertFRTMailDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAAlertFRTMailDAOs.Add(SLAAlertFRTMailDAO);
            }
            await DataContext.BulkMergeAsync(SLAAlertFRTMailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAAlertFRTMail> SLAAlertFRTMails)
        {
            List<long> Ids = SLAAlertFRTMails.Select(x => x.Id).ToList();
            await DataContext.SLAAlertFRTMail
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAAlertFRTMailDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAAlertFRTMail SLAAlertFRTMail)
        {
        }
        
    }
}
