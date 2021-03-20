using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAEscalationUser : DataEntity,  IEquatable<SLAEscalationUser>
    {
        public long Id { get; set; }
        public long? SLAEscalationId { get; set; }
        public long? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public SLAEscalation SLAEscalation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAEscalationUser other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAEscalationUserFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAEscalationUserFilter> OrFilter { get; set; }
        public SLAEscalationUserOrder OrderBy {get; set;}
        public SLAEscalationUserSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAEscalationUserOrder
    {
        Id = 0,
        SLAEscalation = 1,
        AppUser = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAEscalationUserSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAEscalation = E._1,
        AppUser = E._2,
    }
}
