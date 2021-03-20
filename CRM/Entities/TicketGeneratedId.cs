using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class TicketGeneratedId : DataEntity,  IEquatable<TicketGeneratedId>
    {
        public long Id { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(TicketGeneratedId other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TicketGeneratedIdFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<TicketGeneratedIdFilter> OrFilter { get; set; }
        public TicketGeneratedIdOrder OrderBy {get; set;}
        public TicketGeneratedIdSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketGeneratedIdOrder
    {
        Id = 0,
        Used = 4,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum TicketGeneratedIdSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Used = E._4,
    }
}
