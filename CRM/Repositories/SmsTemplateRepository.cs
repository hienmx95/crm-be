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
    public interface ISmsTemplateRepository
    {
        Task<int> Count(SmsTemplateFilter SmsTemplateFilter);
        Task<List<SmsTemplate>> List(SmsTemplateFilter SmsTemplateFilter);
        Task<SmsTemplate> Get(long Id);
        Task<bool> Create(SmsTemplate SmsTemplate);
        Task<bool> Update(SmsTemplate SmsTemplate);
        Task<bool> Delete(SmsTemplate SmsTemplate);
        Task<bool> BulkMerge(List<SmsTemplate> SmsTemplates);
        Task<bool> BulkDelete(List<SmsTemplate> SmsTemplates);
    }
    public class SmsTemplateRepository : ISmsTemplateRepository
    {
        private DataContext DataContext;
        public SmsTemplateRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<SmsTemplateDAO> DynamicFilter(IQueryable<SmsTemplateDAO> query, SmsTemplateFilter filter)
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
            if (filter.Code != null)
                query = query.Where(q => q.Code, filter.Code);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Content != null)
                query = query.Where(q => q.Content, filter.Content);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<SmsTemplateDAO> OrFilter(IQueryable<SmsTemplateDAO> query, SmsTemplateFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<SmsTemplateDAO> initQuery = query.Where(q => false);
            foreach (SmsTemplateFilter SmsTemplateFilter in filter.OrFilter)
            {
                IQueryable<SmsTemplateDAO> queryable = query;
                if (SmsTemplateFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, SmsTemplateFilter.Id);
                if (SmsTemplateFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, SmsTemplateFilter.Code);
                if (SmsTemplateFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, SmsTemplateFilter.Name);
                if (SmsTemplateFilter.Content != null)
                    queryable = queryable.Where(q => q.Content, SmsTemplateFilter.Content);
                if (SmsTemplateFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId, SmsTemplateFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<SmsTemplateDAO> DynamicOrder(IQueryable<SmsTemplateDAO> query, SmsTemplateFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case SmsTemplateOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case SmsTemplateOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SmsTemplateOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case SmsTemplateOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case SmsTemplateOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case SmsTemplateOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case SmsTemplateOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SmsTemplateOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case SmsTemplateOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case SmsTemplateOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<SmsTemplate>> DynamicSelect(IQueryable<SmsTemplateDAO> query, SmsTemplateFilter filter)
        {
            List<SmsTemplate> SmsTemplates = await query.Select(q => new SmsTemplate()
            {
                Id = filter.Selects.Contains(SmsTemplateSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(SmsTemplateSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(SmsTemplateSelect.Name) ? q.Name : default(string),
                Content = filter.Selects.Contains(SmsTemplateSelect.Content) ? q.Content : default(string),
                StatusId = filter.Selects.Contains(SmsTemplateSelect.Status) ? q.StatusId : default(long?),
                Status = filter.Selects.Contains(SmsTemplateSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return SmsTemplates;
        }

        public async Task<int> Count(SmsTemplateFilter filter)
        {
            IQueryable<SmsTemplateDAO> SmsTemplates = DataContext.SmsTemplate.AsNoTracking();
            SmsTemplates = DynamicFilter(SmsTemplates, filter);
            return await SmsTemplates.CountAsync();
        }

        public async Task<List<SmsTemplate>> List(SmsTemplateFilter filter)
        {
            if (filter == null) return new List<SmsTemplate>();
            IQueryable<SmsTemplateDAO> SmsTemplateDAOs = DataContext.SmsTemplate.AsNoTracking();
            SmsTemplateDAOs = DynamicFilter(SmsTemplateDAOs, filter);
            SmsTemplateDAOs = DynamicOrder(SmsTemplateDAOs, filter);
            List<SmsTemplate> SmsTemplates = await DynamicSelect(SmsTemplateDAOs, filter);
            return SmsTemplates;
        }

        public async Task<SmsTemplate> Get(long Id)
        {
            SmsTemplate SmsTemplate = await DataContext.SmsTemplate.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new SmsTemplate()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Content = x.Content,
                StatusId = x.StatusId,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).FirstOrDefaultAsync();

            if (SmsTemplate == null)
                return null;

            return SmsTemplate;
        }
        public async Task<bool> Create(SmsTemplate SmsTemplate)
        {
            SmsTemplateDAO SmsTemplateDAO = new SmsTemplateDAO();
            SmsTemplateDAO.Id = SmsTemplate.Id;
            SmsTemplateDAO.Code = SmsTemplate.Code;
            SmsTemplateDAO.Name = SmsTemplate.Name;
            SmsTemplateDAO.Content = SmsTemplate.Content;
            SmsTemplateDAO.StatusId = SmsTemplate.StatusId;
            SmsTemplateDAO.CreatedAt = StaticParams.DateTimeNow;
            SmsTemplateDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.SmsTemplate.Add(SmsTemplateDAO);
            await DataContext.SaveChangesAsync();
            SmsTemplate.Id = SmsTemplateDAO.Id;
            await SaveReference(SmsTemplate);
            return true;
        }

        public async Task<bool> Update(SmsTemplate SmsTemplate)
        {
            SmsTemplateDAO SmsTemplateDAO = DataContext.SmsTemplate.Where(x => x.Id == SmsTemplate.Id).FirstOrDefault();
            if (SmsTemplateDAO == null)
                return false;
            SmsTemplateDAO.Id = SmsTemplate.Id;
            SmsTemplateDAO.Code = SmsTemplate.Code;
            SmsTemplateDAO.Name = SmsTemplate.Name;
            SmsTemplateDAO.Content = SmsTemplate.Content;
            SmsTemplateDAO.StatusId = SmsTemplate.StatusId;
            SmsTemplateDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(SmsTemplate);
            return true;
        }

        public async Task<bool> Delete(SmsTemplate SmsTemplate)
        {
            await DataContext.SmsTemplate.Where(x => x.Id == SmsTemplate.Id).UpdateFromQueryAsync(x => new SmsTemplateDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<SmsTemplate> SmsTemplates)
        {
            List<SmsTemplateDAO> SmsTemplateDAOs = new List<SmsTemplateDAO>();
            foreach (SmsTemplate SmsTemplate in SmsTemplates)
            {
                SmsTemplateDAO SmsTemplateDAO = new SmsTemplateDAO();
                SmsTemplateDAO.Id = SmsTemplate.Id;
                SmsTemplateDAO.Code = SmsTemplate.Code;
                SmsTemplateDAO.Name = SmsTemplate.Name;
                SmsTemplateDAO.Content = SmsTemplate.Content;
                SmsTemplateDAO.StatusId = SmsTemplate.StatusId;
                SmsTemplateDAO.CreatedAt = StaticParams.DateTimeNow;
                SmsTemplateDAO.UpdatedAt = StaticParams.DateTimeNow;
                SmsTemplateDAOs.Add(SmsTemplateDAO);
            }
            await DataContext.BulkMergeAsync(SmsTemplateDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<SmsTemplate> SmsTemplates)
        {
            List<long> Ids = SmsTemplates.Select(x => x.Id).ToList();
            await DataContext.SmsTemplate
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new SmsTemplateDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(SmsTemplate SmsTemplate)
        {
        }
        
    }
}
