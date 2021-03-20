using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class KnowledgeArticleOrganizationMapping : DataEntity,  IEquatable<KnowledgeArticleOrganizationMapping>
    {
        public long KnowledgeArticleId { get; set; }
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public KnowledgeArticle KnowledgeArticle { get; set; }

        public bool Equals(KnowledgeArticleOrganizationMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class KnowledgeArticleOrganizationMappingFilter : FilterEntity
    {
        public IdFilter KnowledgeArticleId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public List<KnowledgeArticleOrganizationMappingFilter> OrFilter { get; set; }
        public KnowledgeArticleOrganizationMappingOrder OrderBy {get; set;}
        public KnowledgeArticleOrganizationMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnowledgeArticleOrganizationMappingOrder
    {
        KnowledgeArticle = 0,
        Organization = 1,
    }

    [Flags]
    public enum KnowledgeArticleOrganizationMappingSelect:long
    {
        ALL = E.ALL,
        KnowledgeArticle = E._0,
        Organization = E._1,
    }
}
