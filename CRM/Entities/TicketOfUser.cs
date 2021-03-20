using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class TicketOfUser : DataEntity,  IEquatable<TicketOfUser>
    {
        public long Id { get; set; }
        public string Notes { get; set; }
        public long UserId { get; set; }
        public long TicketId { get; set; }
        public long TicketStatusId { get; set; }
        public Ticket Ticket { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public AppUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(TicketOfUser other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TicketOfUserFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Notes { get; set; }
        public IdFilter UserId { get; set; }
        public IdFilter TicketId { get; set; }
        public IdFilter TicketStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<TicketOfUserFilter> OrFilter { get; set; }
        public TicketOfUserOrder OrderBy {get; set;}
        public TicketOfUserSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketOfUserOrder
    {
        Id = 0,
        Notes = 1,
        User = 2,
        Ticket = 3,
        TicketStatus = 4,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum TicketOfUserSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Notes = E._1,
        User = E._2,
        Ticket = E._3,
        TicketStatus = E._4,
    }
}
