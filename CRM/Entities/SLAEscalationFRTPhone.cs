using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAEscalationFRTPhone : DataEntity,  IEquatable<SLAEscalationFRTPhone>
    {
        public long Id { get; set; }
        public long? SLAEscalationFRTId { get; set; }
        public string Phone { get; set; }
        public SLAEscalationFRT SLAEscalationFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAEscalationFRTPhone other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAEscalationFRTPhoneFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationFRTId { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAEscalationFRTPhoneFilter> OrFilter { get; set; }
        public SLAEscalationFRTPhoneOrder OrderBy {get; set;}
        public SLAEscalationFRTPhoneSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAEscalationFRTPhoneOrder
    {
        Id = 0,
        SLAEscalationFRT = 1,
        Phone = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAEscalationFRTPhoneSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAEscalationFRT = E._1,
        Phone = E._2,
    }
}
