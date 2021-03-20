using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAEscalationFRTMailDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAEscalationFRTId { get; set; }
        public string Mail { get; set; }
        public TicketIssueLevel_SLAEscalationFRTDTO SLAEscalationFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAEscalationFRTMailDTO() {}
        public TicketIssueLevel_SLAEscalationFRTMailDTO(SLAEscalationFRTMail SLAEscalationFRTMail)
        {
            this.Id = SLAEscalationFRTMail.Id;
            this.SLAEscalationFRTId = SLAEscalationFRTMail.SLAEscalationFRTId;
            this.Mail = SLAEscalationFRTMail.Mail;
            this.SLAEscalationFRT = SLAEscalationFRTMail.SLAEscalationFRT == null ? null : new TicketIssueLevel_SLAEscalationFRTDTO(SLAEscalationFRTMail.SLAEscalationFRT);
            this.CreatedAt = SLAEscalationFRTMail.CreatedAt;
            this.UpdatedAt = SLAEscalationFRTMail.UpdatedAt;
            this.Errors = SLAEscalationFRTMail.Errors;
        }
    }

    public class TicketIssueLevel_SLAEscalationFRTMailFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationFRTId { get; set; }
        public StringFilter Mail { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAEscalationFRTMailOrder OrderBy { get; set; }
    }
}
