using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class KnowledgeArticleDAO
    {
        public KnowledgeArticleDAO()
        {
            KnowledgeArticleKeywords = new HashSet<KnowledgeArticleKeywordDAO>();
            KnowledgeArticleOrganizationMappings = new HashSet<KnowledgeArticleOrganizationMappingDAO>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public long StatusId { get; set; }
        public long? KMSStatusId { get; set; }
        public long GroupId { get; set; }
        public long CreatorId { get; set; }
        public long? ItemId { get; set; }
        public long DisplayOrder { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO Creator { get; set; }
        public virtual KnowledgeGroupDAO Group { get; set; }
        public virtual ItemDAO Item { get; set; }
        public virtual KMSStatusDAO KMSStatus { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<KnowledgeArticleKeywordDAO> KnowledgeArticleKeywords { get; set; }
        public virtual ICollection<KnowledgeArticleOrganizationMappingDAO> KnowledgeArticleOrganizationMappings { get; set; }
    }
}
