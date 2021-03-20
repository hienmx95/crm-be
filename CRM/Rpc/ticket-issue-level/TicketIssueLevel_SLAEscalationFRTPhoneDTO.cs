using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAEscalationFRTPhoneDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAEscalationFRTId { get; set; }
        public string Phone { get; set; }
        public TicketIssueLevel_SLAEscalationFRTDTO SLAEscalationFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAEscalationFRTPhoneDTO() {}
        public TicketIssueLevel_SLAEscalationFRTPhoneDTO(SLAEscalationFRTPhone SLAEscalationFRTPhone)
        {
            this.Id = SLAEscalationFRTPhone.Id;
            this.SLAEscalationFRTId = SLAEscalationFRTPhone.SLAEscalationFRTId;
            this.Phone = SLAEscalationFRTPhone.Phone;
            this.SLAEscalationFRT = SLAEscalationFRTPhone.SLAEscalationFRT == null ? null : new TicketIssueLevel_SLAEscalationFRTDTO(SLAEscalationFRTPhone.SLAEscalationFRT);
            this.CreatedAt = SLAEscalationFRTPhone.CreatedAt;
            this.UpdatedAt = SLAEscalationFRTPhone.UpdatedAt;
            this.Errors = SLAEscalationFRTPhone.Errors;
        }
    }

    public class TicketIssueLevel_SLAEscalationFRTPhoneFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationFRTId { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAEscalationFRTPhoneOrder OrderBy { get; set; }
    }
}
