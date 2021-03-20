using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class KnowledgeArticleKeyword : DataEntity,  IEquatable<KnowledgeArticleKeyword>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long KnowledgeArticleId { get; set; }
        public KnowledgeArticle KnowledgeArticle { get; set; }
        
        public bool Equals(KnowledgeArticleKeyword other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class KnowledgeArticleKeywordFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter KnowledgeArticleId { get; set; }
        public List<KnowledgeArticleKeywordFilter> OrFilter { get; set; }
        public KnowledgeArticleKeywordOrder OrderBy {get; set;}
        public KnowledgeArticleKeywordSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnowledgeArticleKeywordOrder
    {
        Id = 0,
        Name = 1,
        KnowledgeArticle = 2,
    }

    [Flags]
    public enum KnowledgeArticleKeywordSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        KnowledgeArticle = E._2,
    }
}
