using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAEscalationMail : DataEntity,  IEquatable<SLAEscalationMail>
    {
        public long Id { get; set; }
        public long? SLAEscalationId { get; set; }
        public string Mail { get; set; }
        public SLAEscalation SLAEscalation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAEscalationMail other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAEscalationMailFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationId { get; set; }
        public StringFilter Mail { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAEscalationMailFilter> OrFilter { get; set; }
        public SLAEscalationMailOrder OrderBy {get; set;}
        public SLAEscalationMailSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAEscalationMailOrder
    {
        Id = 0,
        SLAEscalation = 1,
        Mail = 2,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAEscalationMailSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        SLAEscalation = E._1,
        Mail = E._2,
    }
}
