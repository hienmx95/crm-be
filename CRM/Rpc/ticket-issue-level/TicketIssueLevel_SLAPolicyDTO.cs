using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{ 
    public class TicketIssueLevel_SLAPolicyDTO : DataDTO
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
        public TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel { get; set; }
        public TicketIssueLevel_TicketPriorityDTO TicketPriority { get; set; }
        public TicketIssueLevel_SLATimeUnitDTO FirstResponseUnit { get; set; }
        public TicketIssueLevel_SLATimeUnitDTO ResolveUnit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAPolicyDTO() {}
        public TicketIssueLevel_SLAPolicyDTO(SLAPolicy SLAPolicy)
        {
            this.Id = SLAPolicy.Id;
            this.TicketIssueLevelId = SLAPolicy.TicketIssueLevelId;
            this.TicketPriorityId = SLAPolicy.TicketPriorityId;
            this.FirstResponseTime = SLAPolicy.FirstResponseTime;
            this.FirstResponseUnitId = SLAPolicy.FirstResponseUnitId;
            this.ResolveTime = SLAPolicy.ResolveTime;
            this.ResolveUnitId = SLAPolicy.ResolveUnitId;
            this.IsAlert = SLAPolicy.IsAlert;
            this.IsAlertFRT = SLAPolicy.IsAlertFRT;
            this.IsEscalation = SLAPolicy.IsEscalation;
            this.IsEscalationFRT = SLAPolicy.IsEscalationFRT;
            this.FirstResponseUnit = SLAPolicy.FirstResponseUnit == null ? null : new TicketIssueLevel_SLATimeUnitDTO(SLAPolicy.FirstResponseUnit);
            this.ResolveUnit = SLAPolicy.ResolveUnit == null ? null : new TicketIssueLevel_SLATimeUnitDTO(SLAPolicy.ResolveUnit);
            this.TicketIssueLevel = SLAPolicy.TicketIssueLevel == null ? null : new TicketIssueLevel_TicketIssueLevelDTO(SLAPolicy.TicketIssueLevel);
            this.TicketPriority = SLAPolicy.TicketPriority == null ? null : new TicketIssueLevel_TicketPriorityDTO(SLAPolicy.TicketPriority);
            this.CreatedAt = SLAPolicy.CreatedAt;
            this.UpdatedAt = SLAPolicy.UpdatedAt;
            this.Errors = SLAPolicy.Errors;
        }
    }

    public class TicketIssueLevel_SLAPolicyFilterDTO : FilterDTO
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
        public SLAPolicyOrder OrderBy { get; set; }
    }
}
