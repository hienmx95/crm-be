using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAEscalationPhone : DataEntity,  IEquatable<SLAEscalationPhone>
    {
        public long Id { get; set; }
        public long? SLAEscalationId { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAEscalationPhone other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAEscalationPhoneFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationId { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAEscalationPhoneFilter> OrFilter { get; set; }
        public SLAEscalationPhoneOrder OrderBy {get; set;}
        public SLAEscalationPhoneSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAEscalationPhoneOrder
    {
        Id = 0,
        SLAEscalation = 1,
        Phone = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAEscalationPhoneSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAEscalation = E._1,
        Phone = E._2,
    }
}
