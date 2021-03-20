using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.knowledge_article
{
    public class KnowledgeArticle_KnowledgeArticleDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public long StatusId { get; set; }
        public long? ItemId { get; set; }
        public long KMSStatusId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public long GroupId { get; set; }
        public long CreatorId { get; set; }
        public long DisplayOrder { get; set; }
        public KnowledgeArticle_AppUserDTO Creator { get; set; }
        public KnowledgeArticle_KnowledgeGroupDTO Group { get; set; }
        public KnowledgeArticle_StatusDTO Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public KnowledgeArticle_KMSStatusDTO KMSStatus { get; set; }
        public KnowledgeArticle_ItemDTO Item { get; set; }
        public List<KnowledgeArticle_KnowledgeArticleOrganizationMappingDTO> KnowledgeArticleOrganizationMappings { get; set; }
        public List<KnowledgeArticle_KnowledgeArticleKeywordDTO> KnowledgeArticleKeywords { get; set; }

        public KnowledgeArticle_KnowledgeArticleDTO() { }
        public KnowledgeArticle_KnowledgeArticleDTO(KnowledgeArticle KnowledgeArticle)
        {
            this.Id = KnowledgeArticle.Id;
            this.Title = KnowledgeArticle.Title;
            this.Detail = KnowledgeArticle.Detail;
            this.StatusId = KnowledgeArticle.StatusId;
            this.ItemId = KnowledgeArticle.ItemId;
            this.GroupId = KnowledgeArticle.GroupId;
            this.CreatorId = KnowledgeArticle.CreatorId;
            this.DisplayOrder = KnowledgeArticle.DisplayOrder;
            this.Creator = KnowledgeArticle.Creator == null ? null : new KnowledgeArticle_AppUserDTO(KnowledgeArticle.Creator);
            this.Group = KnowledgeArticle.Group == null ? null : new KnowledgeArticle_KnowledgeGroupDTO(KnowledgeArticle.Group);
            this.Status = KnowledgeArticle.Status == null ? null : new KnowledgeArticle_StatusDTO(KnowledgeArticle.Status);
            this.CreatedAt = KnowledgeArticle.CreatedAt;
            this.UpdatedAt = KnowledgeArticle.UpdatedAt;
            this.Errors = KnowledgeArticle.Errors;

            this.KMSStatus = KnowledgeArticle.KMSStatus == null ? null : new KnowledgeArticle_KMSStatusDTO(KnowledgeArticle.KMSStatus);
            this.Item = KnowledgeArticle.Item == null ? null : new KnowledgeArticle_ItemDTO(KnowledgeArticle.Item);
            this.FromDate = KnowledgeArticle.FromDate;
            this.ToDate = KnowledgeArticle.ToDate;
            this.KnowledgeArticleOrganizationMappings = KnowledgeArticle.KnowledgeArticleOrganizationMappings == null ? null : KnowledgeArticle.KnowledgeArticleOrganizationMappings.Select(p => new KnowledgeArticle_KnowledgeArticleOrganizationMappingDTO(p)).ToList();
            this.KnowledgeArticleKeywords = KnowledgeArticle.KnowledgeArticleKeywords == null ? null : KnowledgeArticle.KnowledgeArticleKeywords.Select(p => new KnowledgeArticle_KnowledgeArticleKeywordDTO(p)).ToList();
        }
    }

    public class KnowledgeArticle_KnowledgeArticleFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Detail { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter GroupId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter KMSStatusId { get; set; }
        public IdFilter ItemId { get; set; }
        public LongFilter DisplayOrder { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public DateFilter FromDate { get; set; }
        public DateFilter ToDate { get; set; }
        public IdFilter OrganizationId { get; set; }
        public KnowledgeArticleOrder OrderBy { get; set; }
    }
}
