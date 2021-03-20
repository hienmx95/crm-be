using CRM.Common;
using CRM.Helpers;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IKnowledgeArticleKeywordRepository
    {
        Task<int> Count(KnowledgeArticleKeywordFilter KnowledgeArticleKeywordFilter);
        Task<List<KnowledgeArticleKeyword>> List(KnowledgeArticleKeywordFilter KnowledgeArticleKeywordFilter);
        Task<List<KnowledgeArticleKeyword>> List(List<long> Ids);
        Task<KnowledgeArticleKeyword> Get(long Id);
        Task<bool> Create(KnowledgeArticleKeyword KnowledgeArticleKeyword);
        Task<bool> Update(KnowledgeArticleKeyword KnowledgeArticleKeyword);
        Task<bool> Delete(KnowledgeArticleKeyword KnowledgeArticleKeyword);
        Task<bool> BulkMerge(List<KnowledgeArticleKeyword> KnowledgeArticleKeywords);
        Task<bool> BulkDelete(List<KnowledgeArticleKeyword> KnowledgeArticleKeywords);
    }
    public class KnowledgeArticleKeywordRepository : IKnowledgeArticleKeywordRepository
    {
        private DataContext DataContext;
        public KnowledgeArticleKeywordRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<KnowledgeArticleKeywordDAO> DynamicFilter(IQueryable<KnowledgeArticleKeywordDAO> query, KnowledgeArticleKeywordFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null && filter.Name.HasValue)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.KnowledgeArticleId != null && filter.KnowledgeArticleId.HasValue)
                query = query.Where(q => q.KnowledgeArticleId, filter.KnowledgeArticleId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<KnowledgeArticleKeywordDAO> OrFilter(IQueryable<KnowledgeArticleKeywordDAO> query, KnowledgeArticleKeywordFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<KnowledgeArticleKeywordDAO> initQuery = query.Where(q => false);
            foreach (KnowledgeArticleKeywordFilter KnowledgeArticleKeywordFilter in filter.OrFilter)
            {
                IQueryable<KnowledgeArticleKeywordDAO> queryable = query;
                if (KnowledgeArticleKeywordFilter.Id != null && KnowledgeArticleKeywordFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, KnowledgeArticleKeywordFilter.Id);
                if (KnowledgeArticleKeywordFilter.Name != null && KnowledgeArticleKeywordFilter.Name.HasValue)
                    queryable = queryable.Where(q => q.Name, KnowledgeArticleKeywordFilter.Name);
                if (KnowledgeArticleKeywordFilter.KnowledgeArticleId != null && KnowledgeArticleKeywordFilter.KnowledgeArticleId.HasValue)
                    queryable = queryable.Where(q => q.KnowledgeArticleId, KnowledgeArticleKeywordFilter.KnowledgeArticleId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }    

        private IQueryable<KnowledgeArticleKeywordDAO> DynamicOrder(IQueryable<KnowledgeArticleKeywordDAO> query, KnowledgeArticleKeywordFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case KnowledgeArticleKeywordOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case KnowledgeArticleKeywordOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case KnowledgeArticleKeywordOrder.KnowledgeArticle:
                            query = query.OrderBy(q => q.KnowledgeArticleId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case KnowledgeArticleKeywordOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case KnowledgeArticleKeywordOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case KnowledgeArticleKeywordOrder.KnowledgeArticle:
                            query = query.OrderByDescending(q => q.KnowledgeArticleId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<KnowledgeArticleKeyword>> DynamicSelect(IQueryable<KnowledgeArticleKeywordDAO> query, KnowledgeArticleKeywordFilter filter)
        {
            List<KnowledgeArticleKeyword> KnowledgeArticleKeywords = await query.Select(q => new KnowledgeArticleKeyword()
            {
                Id = filter.Selects.Contains(KnowledgeArticleKeywordSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(KnowledgeArticleKeywordSelect.Name) ? q.Name : default(string),
                KnowledgeArticleId = filter.Selects.Contains(KnowledgeArticleKeywordSelect.KnowledgeArticle) ? q.KnowledgeArticleId : default(long),
                KnowledgeArticle = filter.Selects.Contains(KnowledgeArticleKeywordSelect.KnowledgeArticle) && q.KnowledgeArticle != null ? new KnowledgeArticle
                {
                    Id = q.KnowledgeArticle.Id,
                    Title = q.KnowledgeArticle.Title,
                    Detail = q.KnowledgeArticle.Detail,
                    StatusId = q.KnowledgeArticle.StatusId,
                    KMSStatusId = q.KnowledgeArticle.KMSStatusId,
                    GroupId = q.KnowledgeArticle.GroupId,
                    CreatorId = q.KnowledgeArticle.CreatorId,
                    ItemId = q.KnowledgeArticle.ItemId,
                    DisplayOrder = q.KnowledgeArticle.DisplayOrder,
                    FromDate = q.KnowledgeArticle.FromDate,
                    ToDate = q.KnowledgeArticle.ToDate,
                } : null,
            }).ToListAsync();
            return KnowledgeArticleKeywords;
        }

        public async Task<int> Count(KnowledgeArticleKeywordFilter filter)
        {
            IQueryable<KnowledgeArticleKeywordDAO> KnowledgeArticleKeywords = DataContext.KnowledgeArticleKeyword.AsNoTracking();
            KnowledgeArticleKeywords = DynamicFilter(KnowledgeArticleKeywords, filter);
            return await KnowledgeArticleKeywords.CountAsync();
        }

        public async Task<List<KnowledgeArticleKeyword>> List(KnowledgeArticleKeywordFilter filter)
        {
            if (filter == null) return new List<KnowledgeArticleKeyword>();
            IQueryable<KnowledgeArticleKeywordDAO> KnowledgeArticleKeywordDAOs = DataContext.KnowledgeArticleKeyword.AsNoTracking();
            KnowledgeArticleKeywordDAOs = DynamicFilter(KnowledgeArticleKeywordDAOs, filter);
            KnowledgeArticleKeywordDAOs = DynamicOrder(KnowledgeArticleKeywordDAOs, filter);
            List<KnowledgeArticleKeyword> KnowledgeArticleKeywords = await DynamicSelect(KnowledgeArticleKeywordDAOs, filter);
            return KnowledgeArticleKeywords;
        }

        public async Task<List<KnowledgeArticleKeyword>> List(List<long> Ids)
        {
            List<KnowledgeArticleKeyword> KnowledgeArticleKeywords = await DataContext.KnowledgeArticleKeyword.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new KnowledgeArticleKeyword()
            {
                Id = x.Id,
                Name = x.Name,
                KnowledgeArticleId = x.KnowledgeArticleId,
                KnowledgeArticle = x.KnowledgeArticle == null ? null : new KnowledgeArticle
                {
                    Id = x.KnowledgeArticle.Id,
                    Title = x.KnowledgeArticle.Title,
                    Detail = x.KnowledgeArticle.Detail,
                    StatusId = x.KnowledgeArticle.StatusId,
                    KMSStatusId = x.KnowledgeArticle.KMSStatusId,
                    GroupId = x.KnowledgeArticle.GroupId,
                    CreatorId = x.KnowledgeArticle.CreatorId,
                    ItemId = x.KnowledgeArticle.ItemId,
                    DisplayOrder = x.KnowledgeArticle.DisplayOrder,
                    FromDate = x.KnowledgeArticle.FromDate,
                    ToDate = x.KnowledgeArticle.ToDate,
                },
            }).ToListAsync();
            

            return KnowledgeArticleKeywords;
        }

        public async Task<KnowledgeArticleKeyword> Get(long Id)
        {
            KnowledgeArticleKeyword KnowledgeArticleKeyword = await DataContext.KnowledgeArticleKeyword.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new KnowledgeArticleKeyword()
            {
                Id = x.Id,
                Name = x.Name,
                KnowledgeArticleId = x.KnowledgeArticleId,
                KnowledgeArticle = x.KnowledgeArticle == null ? null : new KnowledgeArticle
                {
                    Id = x.KnowledgeArticle.Id,
                    Title = x.KnowledgeArticle.Title,
                    Detail = x.KnowledgeArticle.Detail,
                    StatusId = x.KnowledgeArticle.StatusId,
                    KMSStatusId = x.KnowledgeArticle.KMSStatusId,
                    GroupId = x.KnowledgeArticle.GroupId,
                    CreatorId = x.KnowledgeArticle.CreatorId,
                    ItemId = x.KnowledgeArticle.ItemId,
                    DisplayOrder = x.KnowledgeArticle.DisplayOrder,
                    FromDate = x.KnowledgeArticle.FromDate,
                    ToDate = x.KnowledgeArticle.ToDate,
                },
            }).FirstOrDefaultAsync();

            if (KnowledgeArticleKeyword == null)
                return null;

            return KnowledgeArticleKeyword;
        }
        public async Task<bool> Create(KnowledgeArticleKeyword KnowledgeArticleKeyword)
        {
            KnowledgeArticleKeywordDAO KnowledgeArticleKeywordDAO = new KnowledgeArticleKeywordDAO();
            KnowledgeArticleKeywordDAO.Id = KnowledgeArticleKeyword.Id;
            KnowledgeArticleKeywordDAO.Name = KnowledgeArticleKeyword.Name;
            KnowledgeArticleKeywordDAO.KnowledgeArticleId = KnowledgeArticleKeyword.KnowledgeArticleId;
            DataContext.KnowledgeArticleKeyword.Add(KnowledgeArticleKeywordDAO);
            await DataContext.SaveChangesAsync();
            KnowledgeArticleKeyword.Id = KnowledgeArticleKeywordDAO.Id;
            await SaveReference(KnowledgeArticleKeyword);
            return true;
        }

        public async Task<bool> Update(KnowledgeArticleKeyword KnowledgeArticleKeyword)
        {
            KnowledgeArticleKeywordDAO KnowledgeArticleKeywordDAO = DataContext.KnowledgeArticleKeyword.Where(x => x.Id == KnowledgeArticleKeyword.Id).FirstOrDefault();
            if (KnowledgeArticleKeywordDAO == null)
                return false;
            KnowledgeArticleKeywordDAO.Id = KnowledgeArticleKeyword.Id;
            KnowledgeArticleKeywordDAO.Name = KnowledgeArticleKeyword.Name;
            KnowledgeArticleKeywordDAO.KnowledgeArticleId = KnowledgeArticleKeyword.KnowledgeArticleId;
            await DataContext.SaveChangesAsync();
            await SaveReference(KnowledgeArticleKeyword);
            return true;
        }

        public async Task<bool> Delete(KnowledgeArticleKeyword KnowledgeArticleKeyword)
        {
            await DataContext.KnowledgeArticleKeyword.Where(x => x.Id == KnowledgeArticleKeyword.Id).DeleteFromQueryAsync();
            return true;
        }
        
        public async Task<bool> BulkMerge(List<KnowledgeArticleKeyword> KnowledgeArticleKeywords)
        {
            List<KnowledgeArticleKeywordDAO> KnowledgeArticleKeywordDAOs = new List<KnowledgeArticleKeywordDAO>();
            foreach (KnowledgeArticleKeyword KnowledgeArticleKeyword in KnowledgeArticleKeywords)
            {
                KnowledgeArticleKeywordDAO KnowledgeArticleKeywordDAO = new KnowledgeArticleKeywordDAO();
                KnowledgeArticleKeywordDAO.Id = KnowledgeArticleKeyword.Id;
                KnowledgeArticleKeywordDAO.Name = KnowledgeArticleKeyword.Name;
                KnowledgeArticleKeywordDAO.KnowledgeArticleId = KnowledgeArticleKeyword.KnowledgeArticleId;
                KnowledgeArticleKeywordDAOs.Add(KnowledgeArticleKeywordDAO);
            }
            await DataContext.BulkMergeAsync(KnowledgeArticleKeywordDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<KnowledgeArticleKeyword> KnowledgeArticleKeywords)
        {
            List<long> Ids = KnowledgeArticleKeywords.Select(x => x.Id).ToList();
            await DataContext.KnowledgeArticleKeyword
                .Where(x => Ids.Contains(x.Id)).DeleteFromQueryAsync();
            return true;
        }

        private async Task SaveReference(KnowledgeArticleKeyword KnowledgeArticleKeyword)
        {
        }
        
    }
}
