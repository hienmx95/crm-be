using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAEscalationFRTUserDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAEscalationFRTId { get; set; }
        public long? AppUserId { get; set; }
        public TicketIssueLevel_AppUserDTO AppUser { get; set; }
        public TicketIssueLevel_SLAEscalationFRTDTO SLAEscalationFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAEscalationFRTUserDTO() {}
        public TicketIssueLevel_SLAEscalationFRTUserDTO(SLAEscalationFRTUser SLAEscalationFRTUser)
        {
            this.Id = SLAEscalationFRTUser.Id;
            this.SLAEscalationFRTId = SLAEscalationFRTUser.SLAEscalationFRTId;
            this.AppUserId = SLAEscalationFRTUser.AppUserId;
            this.AppUser = SLAEscalationFRTUser.AppUser == null ? null : new TicketIssueLevel_AppUserDTO(SLAEscalationFRTUser.AppUser);
            this.SLAEscalationFRT = SLAEscalationFRTUser.SLAEscalationFRT == null ? null : new TicketIssueLevel_SLAEscalationFRTDTO(SLAEscalationFRTUser.SLAEscalationFRT);
            this.CreatedAt = SLAEscalationFRTUser.CreatedAt;
            this.UpdatedAt = SLAEscalationFRTUser.UpdatedAt;
            this.Errors = SLAEscalationFRTUser.Errors;
        }
    }

    public class TicketIssueLevel_SLAEscalationFRTUserFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationFRTId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAEscalationFRTUserOrder OrderBy { get; set; }
    }
}
