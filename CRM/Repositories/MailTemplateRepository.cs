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
    public interface IMailTemplateRepository
    {
        Task<int> Count(MailTemplateFilter MailTemplateFilter);
        Task<List<MailTemplate>> List(MailTemplateFilter MailTemplateFilter);
        Task<MailTemplate> Get(long Id);
        Task<bool> Create(MailTemplate MailTemplate);
        Task<bool> Update(MailTemplate MailTemplate);
        Task<bool> Delete(MailTemplate MailTemplate);
        Task<bool> BulkMerge(List<MailTemplate> MailTemplates);
        Task<bool> BulkDelete(List<MailTemplate> MailTemplates);
    }
    public class MailTemplateRepository : IMailTemplateRepository
    {
        private DataContext DataContext;
        public MailTemplateRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<MailTemplateDAO> DynamicFilter(IQueryable<MailTemplateDAO> query, MailTemplateFilter filter)
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
                query = query.Where(q => q.StatusId.HasValue).Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

         private IQueryable<MailTemplateDAO> OrFilter(IQueryable<MailTemplateDAO> query, MailTemplateFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<MailTemplateDAO> initQuery = query.Where(q => false);
            foreach (MailTemplateFilter MailTemplateFilter in filter.OrFilter)
            {
                IQueryable<MailTemplateDAO> queryable = query;
                if (MailTemplateFilter.Id != null)
                    queryable = queryable.Where(q => q.Id, MailTemplateFilter.Id);
                if (MailTemplateFilter.Code != null)
                    queryable = queryable.Where(q => q.Code, MailTemplateFilter.Code);
                if (MailTemplateFilter.Name != null)
                    queryable = queryable.Where(q => q.Name, MailTemplateFilter.Name);
                if (MailTemplateFilter.Content != null)
                    queryable = queryable.Where(q => q.Content, MailTemplateFilter.Content);
                if (MailTemplateFilter.StatusId != null)
                    queryable = queryable.Where(q => q.StatusId.HasValue).Where(q => q.StatusId, MailTemplateFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<MailTemplateDAO> DynamicOrder(IQueryable<MailTemplateDAO> query, MailTemplateFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case MailTemplateOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case MailTemplateOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case MailTemplateOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case MailTemplateOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case MailTemplateOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case MailTemplateOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case MailTemplateOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case MailTemplateOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case MailTemplateOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case MailTemplateOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<MailTemplate>> DynamicSelect(IQueryable<MailTemplateDAO> query, MailTemplateFilter filter)
        {
            List<MailTemplate> MailTemplates = await query.Select(q => new MailTemplate()
            {
                Id = filter.Selects.Contains(MailTemplateSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(MailTemplateSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(MailTemplateSelect.Name) ? q.Name : default(string),
                Content = filter.Selects.Contains(MailTemplateSelect.Content) ? q.Content : default(string),
                StatusId = filter.Selects.Contains(MailTemplateSelect.Status) ? q.StatusId : default(long?),
                Status = filter.Selects.Contains(MailTemplateSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
            }).ToListAsync();
            return MailTemplates;
        }

        public async Task<int> Count(MailTemplateFilter filter)
        {
            IQueryable<MailTemplateDAO> MailTemplates = DataContext.MailTemplate.AsNoTracking();
            MailTemplates = DynamicFilter(MailTemplates, filter);
            return await MailTemplates.CountAsync();
        }

        public async Task<List<MailTemplate>> List(MailTemplateFilter filter)
        {
            if (filter == null) return new List<MailTemplate>();
            IQueryable<MailTemplateDAO> MailTemplateDAOs = DataContext.MailTemplate.AsNoTracking();
            MailTemplateDAOs = DynamicFilter(MailTemplateDAOs, filter);
            MailTemplateDAOs = DynamicOrder(MailTemplateDAOs, filter);
            List<MailTemplate> MailTemplates = await DynamicSelect(MailTemplateDAOs, filter);
            return MailTemplates;
        }

        public async Task<MailTemplate> Get(long Id)
        {
            MailTemplate MailTemplate = await DataContext.MailTemplate.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new MailTemplate()
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

            if (MailTemplate == null)
                return null;

            return MailTemplate;
        }
        public async Task<bool> Create(MailTemplate MailTemplate)
        {
            MailTemplateDAO MailTemplateDAO = new MailTemplateDAO();
            MailTemplateDAO.Id = MailTemplate.Id;
            MailTemplateDAO.Code = MailTemplate.Code;
            MailTemplateDAO.Name = MailTemplate.Name;
            MailTemplateDAO.Content = MailTemplate.Content;
            MailTemplateDAO.StatusId = MailTemplate.StatusId;
            MailTemplateDAO.CreatedAt = StaticParams.DateTimeNow;
            MailTemplateDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.MailTemplate.Add(MailTemplateDAO);
            await DataContext.SaveChangesAsync();
            MailTemplate.Id = MailTemplateDAO.Id;
            await SaveReference(MailTemplate);
            return true;
        }

        public async Task<bool> Update(MailTemplate MailTemplate)
        {
            MailTemplateDAO MailTemplateDAO = DataContext.MailTemplate.Where(x => x.Id == MailTemplate.Id).FirstOrDefault();
            if (MailTemplateDAO == null)
                return false;
            MailTemplateDAO.Id = MailTemplate.Id;
            MailTemplateDAO.Code = MailTemplate.Code;
            MailTemplateDAO.Name = MailTemplate.Name;
            MailTemplateDAO.Content = MailTemplate.Content;
            MailTemplateDAO.StatusId = MailTemplate.StatusId;
            MailTemplateDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(MailTemplate);
            return true;
        }

        public async Task<bool> Delete(MailTemplate MailTemplate)
        {
            await DataContext.MailTemplate.Where(x => x.Id == MailTemplate.Id).UpdateFromQueryAsync(x => new MailTemplateDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }
        
        public async Task<bool> BulkMerge(List<MailTemplate> MailTemplates)
        {
            List<MailTemplateDAO> MailTemplateDAOs = new List<MailTemplateDAO>();
            foreach (MailTemplate MailTemplate in MailTemplates)
            {
                MailTemplateDAO MailTemplateDAO = new MailTemplateDAO();
                MailTemplateDAO.Id = MailTemplate.Id;
                MailTemplateDAO.Code = MailTemplate.Code;
                MailTemplateDAO.Name = MailTemplate.Name;
                MailTemplateDAO.Content = MailTemplate.Content;
                MailTemplateDAO.StatusId = MailTemplate.StatusId;
                MailTemplateDAO.CreatedAt = StaticParams.DateTimeNow;
                MailTemplateDAO.UpdatedAt = StaticParams.DateTimeNow;
                MailTemplateDAOs.Add(MailTemplateDAO);
            }
            await DataContext.BulkMergeAsync(MailTemplateDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<MailTemplate> MailTemplates)
        {
            List<long> Ids = MailTemplates.Select(x => x.Id).ToList();
            await DataContext.MailTemplate
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new MailTemplateDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(MailTemplate MailTemplate)
        {
        }
        
    }
}
