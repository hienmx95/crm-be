using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_TicketIssueLevelDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long TicketGroupId { get; set; }
        public long TicketTypeId { get; set; }
        public long StatusId { get; set; }
        public long SLA { get; set; }
        public bool Used { get; set; }
        public TicketIssueLevel_StatusDTO Status { get; set; }
        public TicketIssueLevel_TicketGroupDTO TicketGroup { get; set; }
        public List<TicketIssueLevel_SLAPolicyDTO> SLAPolicies { get; set; }
        public List<TicketIssueLevel_SLAAlertDTO> SLAAlerts { get; set; }
        public List<TicketIssueLevel_SLAAlertFRTDTO> SLAAlertFRTs { get; set; }
        public List<TicketIssueLevel_SLAEscalationDTO> SLAEscalations { get; set; }
        public List<TicketIssueLevel_SLAEscalationFRTDTO> SLAEscalationFRTs { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_TicketIssueLevelDTO() {}
        public TicketIssueLevel_TicketIssueLevelDTO(TicketIssueLevel TicketIssueLevel)
        {
            this.Id = TicketIssueLevel.Id;
            this.Name = TicketIssueLevel.Name;
            this.OrderNumber = TicketIssueLevel.OrderNumber;
            this.TicketTypeId = TicketIssueLevel.TicketGroupId;
            this.TicketGroupId = TicketIssueLevel.TicketGroupId;
            this.StatusId = TicketIssueLevel.StatusId;
            this.SLA = TicketIssueLevel.SLA;
            this.Used = TicketIssueLevel.Used;
            this.Status = TicketIssueLevel.Status == null ? null : new TicketIssueLevel_StatusDTO(TicketIssueLevel.Status);
            this.TicketGroup = TicketIssueLevel.TicketGroup == null ? null : new TicketIssueLevel_TicketGroupDTO(TicketIssueLevel.TicketGroup);
            this.CreatedAt = TicketIssueLevel.CreatedAt;
            this.UpdatedAt = TicketIssueLevel.UpdatedAt;
            this.Errors = TicketIssueLevel.Errors;
            this.SLAPolicies = TicketIssueLevel.SLAPolicies?.Select(x => new TicketIssueLevel_SLAPolicyDTO(x)).ToList();
            this.SLAAlerts = TicketIssueLevel.SLAAlerts?.Select(x => new TicketIssueLevel_SLAAlertDTO(x)).ToList();
            this.SLAAlertFRTs = TicketIssueLevel.SLAAlertFRTs?.Select(x => new TicketIssueLevel_SLAAlertFRTDTO(x)).ToList();
            this.SLAEscalations = TicketIssueLevel.SLAEscalations?.Select(x => new TicketIssueLevel_SLAEscalationDTO(x)).ToList();
            this.SLAEscalationFRTs = TicketIssueLevel.SLAEscalationFRTs?.Select(x => new TicketIssueLevel_SLAEscalationFRTDTO(x)).ToList();
        }
    }

    public class TicketIssueLevel_TicketIssueLevelFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public IdFilter TicketGroupId { get; set; }
        public IdFilter TicketTypeId { get; set; }
        public IdFilter StatusId { get; set; }
        public LongFilter SLA { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketIssueLevelOrder OrderBy { get; set; }
    }
}
