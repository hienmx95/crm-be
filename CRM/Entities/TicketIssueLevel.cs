using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class TicketIssueLevel : DataEntity,  IEquatable<TicketIssueLevel>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long TicketGroupId { get; set; }
        public long TicketTypeId { get; set; }
        public long StatusId { get; set; }
        public long SLA { get; set; }
        public bool Used { get; set; }
        public Status Status { get; set; }
        public TicketGroup TicketGroup { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<SLAPolicy> SLAPolicies { get; set; }
        public List<SLAAlert> SLAAlerts { get; set; }
        public List<SLAAlertFRT> SLAAlertFRTs { get; set; }
        public List<SLAEscalation> SLAEscalations { get; set; }
        public List<SLAEscalationFRT> SLAEscalationFRTs { get; set; }

        public bool Equals(TicketIssueLevel other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TicketIssueLevelFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public IdFilter TicketTypeId { get; set; }
        public IdFilter TicketGroupId { get; set; }
        public IdFilter StatusId { get; set; }
        public LongFilter SLA { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<TicketIssueLevelFilter> OrFilter { get; set; }
        public TicketIssueLevelOrder OrderBy {get; set;}
        public TicketIssueLevelSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketIssueLevelOrder
    {
        Id = 0,
        Name = 1,
        OrderNumber = 2,
        TicketGroup = 3,
        Status = 4,
        SLA = 5,
        Used = 9,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum TicketIssueLevelSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        OrderNumber = E._2,
        TicketGroup = E._3,
        Status = E._4,
        SLA = E._5,
        Used = E._9,
    }
}
