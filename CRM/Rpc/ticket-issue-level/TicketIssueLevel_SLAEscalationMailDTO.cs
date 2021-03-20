using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAEscalationMailDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAEscalationId { get; set; }
        public string Mail { get; set; }
        public TicketIssueLevel_SLAEscalationDTO SLAEscalation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAEscalationMailDTO() {}
        public TicketIssueLevel_SLAEscalationMailDTO(SLAEscalationMail SLAEscalationMail)
        {
            this.Id = SLAEscalationMail.Id;
            this.SLAEscalationId = SLAEscalationMail.SLAEscalationId;
            this.Mail = SLAEscalationMail.Mail;
            this.SLAEscalation = SLAEscalationMail.SLAEscalation == null ? null : new TicketIssueLevel_SLAEscalationDTO(SLAEscalationMail.SLAEscalation);
            this.CreatedAt = SLAEscalationMail.CreatedAt;
            this.UpdatedAt = SLAEscalationMail.UpdatedAt;
            this.Errors = SLAEscalationMail.Errors;
        }
    }

    public class TicketIssueLevel_SLAEscalationMailFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationId { get; set; }
        public StringFilter Mail { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAEscalationMailOrder OrderBy { get; set; }
    }
}
