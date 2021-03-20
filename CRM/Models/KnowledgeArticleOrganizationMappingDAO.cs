using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class KnowledgeArticleOrganizationMappingDAO
    {
        public long KnowledgeArticleId { get; set; }
        public long OrganizationId { get; set; }

        public virtual KnowledgeArticleDAO KnowledgeArticle { get; set; }
        public virtual OrganizationDAO Organization { get; set; }
    }
}
