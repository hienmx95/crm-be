using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class KnowledgeArticle : DataEntity,  IEquatable<KnowledgeArticle>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public long StatusId { get; set; }
        public long GroupId { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public long? ItemId { get; set; }
        public long? KMSStatusId { get; set; }
        public long DisplayOrder { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public AppUser Creator { get; set; }
        public KnowledgeGroup Group { get; set; }
        public Organization Organization { get; set; }
        public Status Status { get; set; }
        public Item Item { get; set; }
        public KMSStatus KMSStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<KnowledgeArticleOrganizationMapping> KnowledgeArticleOrganizationMappings { get; set; }
        public List<KnowledgeArticleKeyword> KnowledgeArticleKeywords { get; set; }

        public bool Equals(KnowledgeArticle other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class KnowledgeArticleFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Detail { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter GroupId { get; set; }
        public IdFilter CreatorId { get; set; }
        public LongFilter DisplayOrder { get; set; }
        public IdFilter ItemId { get; set; }
        public IdFilter KMSStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public DateFilter FromDate { get; set; }
        public DateFilter ToDate { get; set; }
        public IdFilter OrganizationId { get; set; }
        public List<KnowledgeArticleFilter> OrFilter { get; set; }
        public KnowledgeArticleOrder OrderBy {get; set;}
        public KnowledgeArticleSelect Selects {get; set;}

        public IdFilter UserId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter KnowledgeGroupId { get; set; }
        public IdFilter AppliedDepartmentId { get; set; }
        public IdFilter CurrentUserId { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnowledgeArticleOrder
    {
        Id = 0,
        Title = 1,
        Detail = 2,
        Status = 3,
        Group = 4,
        Creator = 5,
        DisplayOrder = 6,
        Item = 7,
        KMSStatus = 8,
        Organization = 9,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum KnowledgeArticleSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        Detail = E._2,
        Status = E._3, 
        Group = E._4,
        Creator = E._5,
        DisplayOrder = E._6,
        Item = E._7,
        FromDate = E._8,
        ToDate = E._9,
        KMSStatus = E._10,
        Organization = E._11,
    }
}
