using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.knowledge_article
{
    public class KnowledgeArticle_KnowledgeArticleKeywordDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? KnowledgeArticleId { get; set; }
        public KnowledgeArticle_KnowledgeArticleDTO KnowledgeArticle { get; set; }
        public KnowledgeArticle_KnowledgeArticleKeywordDTO() { }
        public KnowledgeArticle_KnowledgeArticleKeywordDTO(KnowledgeArticleKeyword KnowledgeArticleKeyword)
        {
            this.Id = KnowledgeArticleKeyword.Id;
            this.Name = KnowledgeArticleKeyword.Name;
            this.KnowledgeArticleId = KnowledgeArticleKeyword.KnowledgeArticleId;
            this.KnowledgeArticle = KnowledgeArticleKeyword.KnowledgeArticle == null ? null : new KnowledgeArticle_KnowledgeArticleDTO(KnowledgeArticleKeyword.KnowledgeArticle);
            this.Errors = KnowledgeArticleKeyword.Errors;
        }
    }

    public class KnowledgeArticle_KnowledgeArticleKeywordFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter KnowledgeArticleId { get; set; }
        public KnowledgeArticleKeywordOrder OrderBy { get; set; }
    }
}
