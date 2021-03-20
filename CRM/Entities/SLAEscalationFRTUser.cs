using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAEscalationFRTUser : DataEntity,  IEquatable<SLAEscalationFRTUser>
    {
        public long Id { get; set; }
        public long? SLAEscalationFRTId { get; set; }
        public long? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public SLAEscalationFRT SLAEscalationFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAEscalationFRTUser other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAEscalationFRTUserFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationFRTId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAEscalationFRTUserFilter> OrFilter { get; set; }
        public SLAEscalationFRTUserOrder OrderBy {get; set;}
        public SLAEscalationFRTUserSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAEscalationFRTUserOrder
    {
        Id = 0,
        SLAEscalationFRT = 1,
        AppUser = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAEscalationFRTUserSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAEscalationFRT = E._1,
        AppUser = E._2,
    }
}
