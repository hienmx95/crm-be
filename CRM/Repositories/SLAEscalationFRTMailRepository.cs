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
    public interface ISLAEscalationFRTMailRepository
    {
        Task<int> Count(SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter);
        Task<List<SLAEscalationFRTMail>> List(SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter);
        Task<SLAEscalationFRTMail> Get(long Id);
        Task<bool> Create(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<bool> Update(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<bool> Delete(SLAEscalationFRTMail SLAEscalationFRTMail);
        Task<bool> BulkMerge(List<SLAEscalationFRTMail> SLAEscalationFRTMails);
        Task<bool> BulkDelete(List<SLAEscalationFRTMail> SLAEscalationFRTMails);
    }
    public class SLAEscalationFRTMailRepository : ISLAEscalationFRTMailRepository
    {
        private DataContext DataContext;
        public SLAEscalationFRTMailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAEscalationFRTMailDAO> DynamicFilter(IQueryable<SLAEscalationFRTMailDAO> query, SLAEscalationFRTMailFilter filter)
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
            if (filter.Mail != null)
                query = query.Where(q => q.Mail, filter.Mail);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAEscalationFRTMailDAO> OrFilter(IQueryable<SLAEscalationFRTMailDAO> query, SLAEscalationFRTMailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAEscalationFRTMailDAO> initQuery = query.Where(q => false);
            foreach (SLAEscalationFRTMailFilter SLAEscalationFRTMailFilter in filter.OrFilter)
            {
                IQueryable<SLAEscalationFRTMailDAO> queryable = query;
                if (SLAEscalationFRTMailFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAEscalationFRTMailFilter.Id);
                if (SLAEscalationFRTMailFilter.SLAEscalationFRTId != null)
                    queryable = queryable.Where(q => q.SLAEscalationFRTId.HasValue).Where(q => q.SLAEscalationFRTId, SLAEscalationFRTMailFilter.SLAEscalationFRTId);
                if (SLAEscalationFRTMailFilter.Mail != null)
                    queryable = queryable.Where(q => q.Mail, SLAEscalationFRTMailFilter.Mail);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAEscalationFRTMailDAO> DynamicOrder(IQueryable<SLAEscalationFRTMailDAO> query, SLAEscalationFRTMailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationFRTMailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAEscalationFRTMailOrder.SLAEscalationFRT:
                            query = query.OrderBy(q => q.SLAEscalationFRTId);
                            break;
                        case SLAEscalationFRTMailOrder.Mail:
                            query = query.OrderBy(q => q.Mail);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationFRTMailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAEscalationFRTMailOrder.SLAEscalationFRT:
                            query = query.OrderByDescending(q => q.SLAEscalationFRTId);
                            break;
                        case SLAEscalationFRTMailOrder.Mail:
                            query = query.OrderByDescending(q => q.Mail);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAEscalationFRTMail>> DynamicSelect(IQueryable<SLAEscalationFRTMailDAO> query, SLAEscalationFRTMailFilter filter)
        {
            List<SLAEscalationFRTMail> SLAEscalationFRTMails = await query.Select(q => new SLAEscalationFRTMail()
            {
                Id = filter.Selects.Contains(SLAEscalationFRTMailSelect.Id) ? q.Id : default(long),
                SLAEscalationFRTId = filter.Selects.Contains(SLAEscalationFRTMailSelect.SLAEscalationFRT) ? q.SLAEscalationFRTId : default(long?),
                Mail = filter.Selects.Contains(SLAEscalationFRTMailSelect.Mail) ? q.Mail : default(string),
                SLAEscalationFRT = filter.Selects.Contains(SLAEscalationFRTMailSelect.SLAEscalationFRT) && q.SLAEscalationFRT != null ? new SLAEscalationFRT
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
            return SLAEscalationFRTMails;
        }

        public async Task<int> Count(SLAEscalationFRTMailFilter filter)
        {
            IQueryable<SLAEscalationFRTMailDAO> SLAEscalationFRTMails = DataContext.SLAEscalationFRTMail.AsNoTracking();
            SLAEscalationFRTMails = DynamicFilter(SLAEscalationFRTMails, filter);
            return await SLAEscalationFRTMails.CountAsync();
        }

        public async Task<List<SLAEscalationFRTMail>> List(SLAEscalationFRTMailFilter filter)
        {
            if (filter == null) return new List<SLAEscalationFRTMail>();
            IQueryable<SLAEscalationFRTMailDAO> SLAEscalationFRTMailDAOs = DataContext.SLAEscalationFRTMail.AsNoTracking();
            SLAEscalationFRTMailDAOs = DynamicFilter(SLAEscalationFRTMailDAOs, filter);
            SLAEscalationFRTMailDAOs = DynamicOrder(SLAEscalationFRTMailDAOs, filter);
            List<SLAEscalationFRTMail> SLAEscalationFRTMails = await DynamicSelect(SLAEscalationFRTMailDAOs, filter);
            return SLAEscalationFRTMails;
        }

        public async Task<SLAEscalationFRTMail> Get(long Id)
        {
            SLAEscalationFRTMail SLAEscalationFRTMail = await DataContext.SLAEscalationFRTMail.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAEscalationFRTMail()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAEscalationFRTId = x.SLAEscalationFRTId,
                Mail = x.Mail,
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

            if (SLAEscalationFRTMail == null)
                return null;

            return SLAEscalationFRTMail;
        }
        public async Task<bool> Create(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            SLAEscalationFRTMailDAO SLAEscalationFRTMailDAO = new SLAEscalationFRTMailDAO();
            SLAEscalationFRTMailDAO.Id = SLAEscalationFRTMail.Id;
            SLAEscalationFRTMailDAO.SLAEscalationFRTId = SLAEscalationFRTMail.SLAEscalationFRTId;
            SLAEscalationFRTMailDAO.Mail = SLAEscalationFRTMail.Mail;
            SLAEscalationFRTMailDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAEscalationFRTMailDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAEscalationFRTMail.Add(SLAEscalationFRTMailDAO);
            await DataContext.SaveChangesAsync();
            SLAEscalationFRTMail.Id = SLAEscalationFRTMailDAO.Id;
            await SaveReference(SLAEscalationFRTMail);
            return true;
        }

        public async Task<bool> Update(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            SLAEscalationFRTMailDAO SLAEscalationFRTMailDAO = DataContext.SLAEscalationFRTMail.Where(x => x.Id == SLAEscalationFRTMail.Id).FirstOrDefault();
            if (SLAEscalationFRTMailDAO == null)
                return false;
            SLAEscalationFRTMailDAO.Id = SLAEscalationFRTMail.Id;
            SLAEscalationFRTMailDAO.SLAEscalationFRTId = SLAEscalationFRTMail.SLAEscalationFRTId;
            SLAEscalationFRTMailDAO.Mail = SLAEscalationFRTMail.Mail;
            SLAEscalationFRTMailDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAEscalationFRTMail);
            return true;
        }

        public async Task<bool> Delete(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            await DataContext.SLAEscalationFRTMail.Where(x => x.Id == SLAEscalationFRTMail.Id).UpdateFromQueryAsync(x => new SLAEscalationFRTMailDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAEscalationFRTMail> SLAEscalationFRTMails)
        {
            List<SLAEscalationFRTMailDAO> SLAEscalationFRTMailDAOs = new List<SLAEscalationFRTMailDAO>();
            foreach (SLAEscalationFRTMail SLAEscalationFRTMail in SLAEscalationFRTMails)
            {
                SLAEscalationFRTMailDAO SLAEscalationFRTMailDAO = new SLAEscalationFRTMailDAO();
                SLAEscalationFRTMailDAO.Id = SLAEscalationFRTMail.Id;
                SLAEscalationFRTMailDAO.SLAEscalationFRTId = SLAEscalationFRTMail.SLAEscalationFRTId;
                SLAEscalationFRTMailDAO.Mail = SLAEscalationFRTMail.Mail;
                SLAEscalationFRTMailDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAEscalationFRTMailDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAEscalationFRTMailDAOs.Add(SLAEscalationFRTMailDAO);
            }
            await DataContext.BulkMergeAsync(SLAEscalationFRTMailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAEscalationFRTMail> SLAEscalationFRTMails)
        {
            List<long> Ids = SLAEscalationFRTMails.Select(x => x.Id).ToList();
            await DataContext.SLAEscalationFRTMail
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAEscalationFRTMailDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
        }
        
    }
}
