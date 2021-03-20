using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class TicketStatus : DataEntity,  IEquatable<TicketStatus>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(TicketStatus other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TicketStatusFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public StringFilter ColorCode { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<TicketStatusFilter> OrFilter { get; set; }
        public TicketStatusOrder OrderBy {get; set;}
        public TicketStatusSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketStatusOrder
    {
        Id = 0,
        Name = 1,
        OrderNumber = 2,
        ColorCode = 3,
        Status = 4,
        Used = 8,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum TicketStatusSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        OrderNumber = E._2,
        ColorCode = E._3,
        Status = E._4,
        Used = E._8,
    }
}
