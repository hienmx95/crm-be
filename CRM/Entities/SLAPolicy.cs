using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class SLAPolicy : DataEntity,  IEquatable<SLAPolicy>
    {
        public long Id { get; set; }
        public long? TicketIssueLevelId { get; set; }
        public long? TicketPriorityId { get; set; }
        public long? FirstResponseTime { get; set; }
        public long? FirstResponseUnitId { get; set; }
        public long? ResolveTime { get; set; }
        public long? ResolveUnitId { get; set; }
        public bool? IsAlert { get; set; }
        public bool? IsAlertFRT { get; set; }
        public bool? IsEscalation { get; set; }
        public bool? IsEscalationFRT { get; set; }
        public SLATimeUnit FirstResponseUnit { get; set; }
        public SLATimeUnit ResolveUnit { get; set; }
        public TicketIssueLevel TicketIssueLevel { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(SLAPolicy other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class SLAPolicyFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter TicketIssueLevelId { get; set; }
        public IdFilter TicketPriorityId { get; set; }
        public LongFilter FirstResponseTime { get; set; }
        public IdFilter FirstResponseUnitId { get; set; }
        public LongFilter ResolveTime { get; set; }
        public IdFilter ResolveUnitId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<SLAPolicyFilter> OrFilter { get; set; }
        public SLAPolicyOrder OrderBy {get; set;}
        public SLAPolicySelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SLAPolicyOrder
    {
        Id = 0,
        TicketIssueLevel = 1,
        TicketPriority = 2,
        FirstResponseTime = 3,
        ResolveTime = 4,
        IsAlert = 5,
        IsAlertFRT = 6,
        IsEscalation = 7,
        IsEscalationFRT = 8,
        FirstResponseUnit = 9,
        ResolveUnit = 10,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum SLAPolicySelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        TicketIssueLevel = E._1,
        TicketPriority = E._2,
        FirstResponseTime = E._3,
        ResolveTime = E._4,
        IsAlert = E._5,
        IsAlertFRT = E._6,
        IsEscalation = E._7,
        IsEscalationFRT = E._8,
        FirstResponseUnit = E._9,
        ResolveUnit = E._10,
    }
}
