using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class KnowledgeGroupDAO
    {
        public KnowledgeGroupDAO()
        {
            KnowledgeArticles = new HashSet<KnowledgeArticleDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public long? DisplayOrder { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<KnowledgeArticleDAO> KnowledgeArticles { get; set; }
    }
}
