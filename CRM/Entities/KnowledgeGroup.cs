using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class KnowledgeGroup : DataEntity, IEquatable<KnowledgeGroup>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public long? DisplayOrder { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(KnowledgeGroup other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class KnowledgeGroupFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public LongFilter DisplayOrder { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<KnowledgeGroupFilter> OrFilter { get; set; }
        public KnowledgeGroupOrder OrderBy { get; set; }
        public KnowledgeGroupSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnowledgeGroupOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
        Status = 3,
        DisplayOrder = 4,
        Description = 5,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum KnowledgeGroupSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
        Status = E._3,
        DisplayOrder = E._4,
        Description = E._5,
    }
}
