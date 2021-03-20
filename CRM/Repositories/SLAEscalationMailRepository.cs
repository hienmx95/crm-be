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
    public interface ISLAEscalationMailRepository
    {
        Task<int> Count(SLAEscalationMailFilter SLAEscalationMailFilter);
        Task<List<SLAEscalationMail>> List(SLAEscalationMailFilter SLAEscalationMailFilter);
        Task<SLAEscalationMail> Get(long Id);
        Task<bool> Create(SLAEscalationMail SLAEscalationMail);
        Task<bool> Update(SLAEscalationMail SLAEscalationMail);
        Task<bool> Delete(SLAEscalationMail SLAEscalationMail);
        Task<bool> BulkMerge(List<SLAEscalationMail> SLAEscalationMails);
        Task<bool> BulkDelete(List<SLAEscalationMail> SLAEscalationMails);
    }
    public class SLAEscalationMailRepository : ISLAEscalationMailRepository
    {
        private DataContext DataContext;
        public SLAEscalationMailRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SLAEscalationMailDAO> DynamicFilter(IQueryable<SLAEscalationMailDAO> query, SLAEscalationMailFilter filter)
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
            if (filter.Mail != null)
                query = query.Where(q => q.Mail, filter.Mail);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SLAEscalationMailDAO> OrFilter(IQueryable<SLAEscalationMailDAO> query, SLAEscalationMailFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SLAEscalationMailDAO> initQuery = query.Where(q => false);
            foreach (SLAEscalationMailFilter SLAEscalationMailFilter in filter.OrFilter)
            {
                IQueryable<SLAEscalationMailDAO> queryable = query;
                if (SLAEscalationMailFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SLAEscalationMailFilter.Id);
                if (SLAEscalationMailFilter.SLAEscalationId != null)
                    queryable = queryable.Where(q => q.SLAEscalationId.HasValue).Where(q => q.SLAEscalationId, SLAEscalationMailFilter.SLAEscalationId);
                if (SLAEscalationMailFilter.Mail != null)
                    queryable = queryable.Where(q => q.Mail, SLAEscalationMailFilter.Mail);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SLAEscalationMailDAO> DynamicOrder(IQueryable<SLAEscalationMailDAO> query, SLAEscalationMailFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationMailOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SLAEscalationMailOrder.SLAEscalation:
                            query = query.OrderBy(q => q.SLAEscalationId);
                            break;
                        case SLAEscalationMailOrder.Mail:
                            query = query.OrderBy(q => q.Mail);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SLAEscalationMailOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SLAEscalationMailOrder.SLAEscalation:
                            query = query.OrderByDescending(q => q.SLAEscalationId);
                            break;
                        case SLAEscalationMailOrder.Mail:
                            query = query.OrderByDescending(q => q.Mail);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SLAEscalationMail>> DynamicSelect(IQueryable<SLAEscalationMailDAO> query, SLAEscalationMailFilter filter)
        {
            List<SLAEscalationMail> SLAEscalationMails = await query.Select(q => new SLAEscalationMail()
            {
                Id = filter.Selects.Contains(SLAEscalationMailSelect.Id) ? q.Id : default(long),
                SLAEscalationId = filter.Selects.Contains(SLAEscalationMailSelect.SLAEscalation) ? q.SLAEscalationId : default(long?),
                Mail = filter.Selects.Contains(SLAEscalationMailSelect.Mail) ? q.Mail : default(string),
                SLAEscalation = filter.Selects.Contains(SLAEscalationMailSelect.SLAEscalation) && q.SLAEscalation != null ? new SLAEscalation
                {
                    Id = q.SLAEscalation.Id,
                    TicketIssueLevelId = q.SLAEscalation.TicketIssueLevelId,
                    IsNotification = q.SLAEscalation.IsNotification,
                    IsMail = q.SLAEscalation.IsMail,
                    IsSMS = q.SLAEscalation.IsSMS,
                    Time = q.SLAEscalation.Time,
                    TimeUnitId = q.SLAEscalation.TimeUnitId,
                    IsAssignedToUser = q.SLAEscalation.IsAssignedToUser,
                    IsAssignedToGroup = q.SLAEscalation.IsAssignedToGroup,
                    SmsTemplateId = q.SLAEscalation.SmsTemplateId,
                    MailTemplateId = q.SLAEscalation.MailTemplateId,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SLAEscalationMails;
        }

        public async Task<int> Count(SLAEscalationMailFilter filter)
        {
            IQueryable<SLAEscalationMailDAO> SLAEscalationMails = DataContext.SLAEscalationMail.AsNoTracking();
            SLAEscalationMails = DynamicFilter(SLAEscalationMails, filter);
            return await SLAEscalationMails.CountAsync();
        }

        public async Task<List<SLAEscalationMail>> List(SLAEscalationMailFilter filter)
        {
            if (filter == null) return new List<SLAEscalationMail>();
            IQueryable<SLAEscalationMailDAO> SLAEscalationMailDAOs = DataContext.SLAEscalationMail.AsNoTracking();
            SLAEscalationMailDAOs = DynamicFilter(SLAEscalationMailDAOs, filter);
            SLAEscalationMailDAOs = DynamicOrder(SLAEscalationMailDAOs, filter);
            List<SLAEscalationMail> SLAEscalationMails = await DynamicSelect(SLAEscalationMailDAOs, filter);
            return SLAEscalationMails;
        }

        public async Task<SLAEscalationMail> Get(long Id)
        {
            SLAEscalationMail SLAEscalationMail = await DataContext.SLAEscalationMail.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SLAEscalationMail()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                SLAEscalationId = x.SLAEscalationId,
                Mail = x.Mail,
                SLAEscalation = x.SLAEscalation == null ? null : new SLAEscalation
                {
                    Id = x.SLAEscalation.Id,
                    TicketIssueLevelId = x.SLAEscalation.TicketIssueLevelId,
                    IsNotification = x.SLAEscalation.IsNotification,
                    IsMail = x.SLAEscalation.IsMail,
                    IsSMS = x.SLAEscalation.IsSMS,
                    Time = x.SLAEscalation.Time,
                    TimeUnitId = x.SLAEscalation.TimeUnitId,
                    IsAssignedToUser = x.SLAEscalation.IsAssignedToUser,
                    IsAssignedToGroup = x.SLAEscalation.IsAssignedToGroup,
                    SmsTemplateId = x.SLAEscalation.SmsTemplateId,
                    MailTemplateId = x.SLAEscalation.MailTemplateId,
                },
            }).FirstOrDefaultAsync();

            if (SLAEscalationMail == null)
                return null;

            return SLAEscalationMail;
        }
        public async Task<bool> Create(SLAEscalationMail SLAEscalationMail)
        {
            SLAEscalationMailDAO SLAEscalationMailDAO = new SLAEscalationMailDAO();
            SLAEscalationMailDAO.Id = SLAEscalationMail.Id;
            SLAEscalationMailDAO.SLAEscalationId = SLAEscalationMail.SLAEscalationId;
            SLAEscalationMailDAO.Mail = SLAEscalationMail.Mail;
            SLAEscalationMailDAO.CreatedAt = StaticParams.DateTimeNow;
            SLAEscalationMailDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SLAEscalationMail.Add(SLAEscalationMailDAO);
            await DataContext.SaveChangesAsync();
            SLAEscalationMail.Id = SLAEscalationMailDAO.Id;
            await SaveReference(SLAEscalationMail);
            return true;
        }

        public async Task<bool> Update(SLAEscalationMail SLAEscalationMail)
        {
            SLAEscalationMailDAO SLAEscalationMailDAO = DataContext.SLAEscalationMail.Where(x => x.Id == SLAEscalationMail.Id).FirstOrDefault();
            if (SLAEscalationMailDAO == null)
                return false;
            SLAEscalationMailDAO.Id = SLAEscalationMail.Id;
            SLAEscalationMailDAO.SLAEscalationId = SLAEscalationMail.SLAEscalationId;
            SLAEscalationMailDAO.Mail = SLAEscalationMail.Mail;
            SLAEscalationMailDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SLAEscalationMail);
            return true;
        }

        public async Task<bool> Delete(SLAEscalationMail SLAEscalationMail)
        {
            await DataContext.SLAEscalationMail.Where(x => x.Id == SLAEscalationMail.Id).UpdateFromQueryAsync(x => new SLAEscalationMailDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SLAEscalationMail> SLAEscalationMails)
        {
            List<SLAEscalationMailDAO> SLAEscalationMailDAOs = new List<SLAEscalationMailDAO>();
            foreach (SLAEscalationMail SLAEscalationMail in SLAEscalationMails)
            {
                SLAEscalationMailDAO SLAEscalationMailDAO = new SLAEscalationMailDAO();
                SLAEscalationMailDAO.Id = SLAEscalationMail.Id;
                SLAEscalationMailDAO.SLAEscalationId = SLAEscalationMail.SLAEscalationId;
                SLAEscalationMailDAO.Mail = SLAEscalationMail.Mail;
                SLAEscalationMailDAO.CreatedAt = StaticParams.DateTimeNow;
                SLAEscalationMailDAO.UpdatedAt = StaticParams.DateTimeNow;
                SLAEscalationMailDAOs.Add(SLAEscalationMailDAO);
            }
            await DataContext.BulkMergeAsync(SLAEscalationMailDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SLAEscalationMail> SLAEscalationMails)
        {
            List<long> Ids = SLAEscalationMails.Select(x => x.Id).ToList();
            await DataContext.SLAEscalationMail
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SLAEscalationMailDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SLAEscalationMail SLAEscalationMail)
        {
        }
        
    }
}
