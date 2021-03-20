using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAEscalationUserDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAEscalationId { get; set; }
        public long? AppUserId { get; set; }
        public TicketIssueLevel_AppUserDTO AppUser { get; set; }
        public TicketIssueLevel_SLAEscalationDTO SLAEscalation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAEscalationUserDTO() {}
        public TicketIssueLevel_SLAEscalationUserDTO(SLAEscalationUser SLAEscalationUser)
        {
            this.Id = SLAEscalationUser.Id;
            this.SLAEscalationId = SLAEscalationUser.SLAEscalationId;
            this.AppUserId = SLAEscalationUser.AppUserId;
            this.AppUser = SLAEscalationUser.AppUser == null ? null : new TicketIssueLevel_AppUserDTO(SLAEscalationUser.AppUser);
            this.SLAEscalation = SLAEscalationUser.SLAEscalation == null ? null : new TicketIssueLevel_SLAEscalationDTO(SLAEscalationUser.SLAEscalation);
            this.CreatedAt = SLAEscalationUser.CreatedAt;
            this.UpdatedAt = SLAEscalationUser.UpdatedAt;
            this.Errors = SLAEscalationUser.Errors;
        }
    }

    public class TicketIssueLevel_SLAEscalationUserFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAEscalationUserOrder OrderBy { get; set; }
    }
}
