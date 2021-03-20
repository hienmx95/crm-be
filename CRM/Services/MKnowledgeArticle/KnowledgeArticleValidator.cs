using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Entities;
using CRM;
using CRM.Repositories;

namespace CRM.Services.MKnowledgeArticle
{
    public interface IKnowledgeArticleValidator : IServiceScoped
    {
        Task<bool> Create(KnowledgeArticle KnowledgeArticle);
        Task<bool> Update(KnowledgeArticle KnowledgeArticle);
        Task<bool> Delete(KnowledgeArticle KnowledgeArticle);
        Task<bool> BulkDelete(List<KnowledgeArticle> KnowledgeArticles);
        Task<bool> Import(List<KnowledgeArticle> KnowledgeArticles);
    }

    public class KnowledgeArticleValidator : IKnowledgeArticleValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            TitleEmpty,
            TitleOverLength,
            ToDateMoreThanFromDate,
            GroupEmpty,
            DetailEmpty,
            FromDateEmpty
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public KnowledgeArticleValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        public async Task<bool> ValidateId(KnowledgeArticle KnowledgeArticle)
        {
            KnowledgeArticleFilter KnowledgeArticleFilter = new KnowledgeArticleFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = KnowledgeArticle.Id },
                Selects = KnowledgeArticleSelect.Id
            };

            int count = await UOW.KnowledgeArticleRepository.Count(KnowledgeArticleFilter);
            if (count == 0)
                KnowledgeArticle.AddError(nameof(KnowledgeArticleValidator), nameof(KnowledgeArticle.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        public async Task<bool> ValidateTitle(KnowledgeArticle KnowledgeArticle)
        {
            if (string.IsNullOrWhiteSpace(KnowledgeArticle.Title))
            {
                KnowledgeArticle.AddError(nameof(KnowledgeArticleValidator), nameof(KnowledgeArticle.Title), ErrorCode.TitleEmpty);
            }
            else if (KnowledgeArticle.Title.Length > 2000)
            {
                KnowledgeArticle.AddError(nameof(KnowledgeArticleValidator), nameof(KnowledgeArticle.Title), ErrorCode.TitleOverLength);
            }
            return KnowledgeArticle.IsValidated;
        }
        public async Task<bool> ValidateGroup(KnowledgeArticle KnowledgeArticle)
        {
            if (KnowledgeArticle.Group == null)
            {
                KnowledgeArticle.AddError(nameof(KnowledgeArticleValidator), nameof(KnowledgeArticle.Group), ErrorCode.GroupEmpty);
            }

            return KnowledgeArticle.IsValidated;
        }
        public async Task<bool> ValidateFromDateAndToDate(KnowledgeArticle KnowledgeArticle)
        {
            if(KnowledgeArticle.FromDate == null)
            {
                KnowledgeArticle.AddError(nameof(KnowledgeArticleValidator), nameof(KnowledgeArticle.FromDate), ErrorCode.FromDateEmpty);
            }    
            if (KnowledgeArticle.FromDate != null && KnowledgeArticle.ToDate != null && KnowledgeArticle.FromDate >= KnowledgeArticle.ToDate)
            {
                KnowledgeArticle.AddError(nameof(KnowledgeArticleValidator), nameof(KnowledgeArticle.ToDate), ErrorCode.ToDateMoreThanFromDate);
            }
            return KnowledgeArticle.IsValidated;
        }

        public async Task<bool> ValidateDetail(KnowledgeArticle KnowledgeArticle)
        {
            if (string.IsNullOrWhiteSpace(KnowledgeArticle.Detail))
            {
                KnowledgeArticle.AddError(nameof(KnowledgeArticleValidator), nameof(KnowledgeArticle.Detail), ErrorCode.DetailEmpty);
            }
            return KnowledgeArticle.IsValidated;
        }

        public async Task<bool> Create(KnowledgeArticle KnowledgeArticle)
        {
            await ValidateTitle(KnowledgeArticle);
            await ValidateFromDateAndToDate(KnowledgeArticle);
            await ValidateGroup(KnowledgeArticle);
            await ValidateDetail(KnowledgeArticle);
            return KnowledgeArticle.IsValidated;
        }

        public async Task<bool> Update(KnowledgeArticle KnowledgeArticle)
        {
            if (await ValidateId(KnowledgeArticle))
            {
                await ValidateTitle(KnowledgeArticle);
                await ValidateFromDateAndToDate(KnowledgeArticle);
                await ValidateGroup(KnowledgeArticle);
                await ValidateDetail(KnowledgeArticle);
            }
            return KnowledgeArticle.IsValidated;
        }

        public async Task<bool> Delete(KnowledgeArticle KnowledgeArticle)
        {
            if (await ValidateId(KnowledgeArticle))
            {
            }
            return KnowledgeArticle.IsValidated;
        }

        public async Task<bool> BulkDelete(List<KnowledgeArticle> KnowledgeArticles)
        {
            foreach (KnowledgeArticle KnowledgeArticle in KnowledgeArticles)
            {
                await Delete(KnowledgeArticle);
            }
            return KnowledgeArticles.All(x => x.IsValidated);
        }

        public async Task<bool> Import(List<KnowledgeArticle> KnowledgeArticles)
        {
            return true;
        }
    }
}
