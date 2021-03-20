using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class TicketGroup : DataEntity,  IEquatable<TicketGroup>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long StatusId { get; set; }
        public long TicketTypeId { get; set; }
        public bool Used { get; set; }
        public Status Status { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(TicketGroup other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TicketGroupFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter TicketTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<TicketGroupFilter> OrFilter { get; set; }
        public TicketGroupOrder OrderBy {get; set;}
        public TicketGroupSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketGroupOrder
    {
        Id = 0,
        Name = 1,
        OrderNumber = 2,
        Status = 3,
        TicketType = 4,
        Used = 8,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum TicketGroupSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        OrderNumber = E._2,
        Status = E._3,
        TicketType = E._4,
        Used = E._8,
    }
}
