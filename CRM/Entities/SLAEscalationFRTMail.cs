using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAEscalationFRTMail : DataEntity,  IEquatable<SLAEscalationFRTMail>
    {
        public long Id { get; set; }
        public long? SLAEscalationFRTId { get; set; }
        public string Mail { get; set; }
        public SLAEscalationFRT SLAEscalationFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAEscalationFRTMail other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAEscalationFRTMailFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationFRTId { get; set; }
        public StringFilter Mail { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAEscalationFRTMailFilter> OrFilter { get; set; }
        public SLAEscalationFRTMailOrder OrderBy {get; set;}
        public SLAEscalationFRTMailSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAEscalationFRTMailOrder
    {
        Id = 0,
        SLAEscalationFRT = 1,
        Mail = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAEscalationFRTMailSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAEscalationFRT = E._1,
        Mail = E._2,
    }
}
