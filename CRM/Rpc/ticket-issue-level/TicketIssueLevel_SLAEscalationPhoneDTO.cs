using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAEscalationPhoneDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAEscalationId { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAEscalationPhoneDTO() {}
        public TicketIssueLevel_SLAEscalationPhoneDTO(SLAEscalationPhone SLAEscalationPhone)
        {
            this.Id = SLAEscalationPhone.Id;
            this.SLAEscalationId = SLAEscalationPhone.SLAEscalationId;
            this.Phone = SLAEscalationPhone.Phone;
            this.CreatedAt = SLAEscalationPhone.CreatedAt;
            this.UpdatedAt = SLAEscalationPhone.UpdatedAt;
            this.Errors = SLAEscalationPhone.Errors;
        }
    }

    public class TicketIssueLevel_SLAEscalationPhoneFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAEscalationId { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAEscalationPhoneOrder OrderBy { get; set; }
    }
}
