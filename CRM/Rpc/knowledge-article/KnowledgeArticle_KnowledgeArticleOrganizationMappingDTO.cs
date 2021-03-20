using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.knowledge_article
{
    public class KnowledgeArticle_KnowledgeArticleOrganizationMappingDTO : DataDTO
    {
        public long KnowledgeArticleId { get; set; }
        public long OrganizationId { get; set; }
        public KnowledgeArticle_OrganizationDTO Organization { get; set; }

        public KnowledgeArticle_KnowledgeArticleOrganizationMappingDTO() { }
        public KnowledgeArticle_KnowledgeArticleOrganizationMappingDTO(KnowledgeArticleOrganizationMapping KnowledgeArticleOrganizationMapping)
        {
            this.KnowledgeArticleId = KnowledgeArticleOrganizationMapping.KnowledgeArticleId;
            this.OrganizationId = KnowledgeArticleOrganizationMapping.OrganizationId;
            this.Organization = KnowledgeArticleOrganizationMapping.Organization == null ? null : new KnowledgeArticle_OrganizationDTO(KnowledgeArticleOrganizationMapping.Organization);
        }
    }

}