using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class TicketSource : DataEntity,  IEquatable<TicketSource>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(TicketSource other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TicketSourceFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<TicketSourceFilter> OrFilter { get; set; }
        public TicketSourceOrder OrderBy {get; set;}
        public TicketSourceSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketSourceOrder
    {
        Id = 0,
        Name = 1,
        OrderNumber = 2,
        Status = 3,
        Used = 7,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum TicketSourceSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        OrderNumber = E._2,
        Status = E._3,
        Used = E._7,
    }
}
