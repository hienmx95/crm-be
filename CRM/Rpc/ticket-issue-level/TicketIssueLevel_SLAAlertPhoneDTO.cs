using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAAlertPhoneDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAAlertId { get; set; }
        public string Phone { get; set; }
        public TicketIssueLevel_SLAAlertDTO SLAAlert { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAAlertPhoneDTO() {}
        public TicketIssueLevel_SLAAlertPhoneDTO(SLAAlertPhone SLAAlertPhone)
        {
            this.Id = SLAAlertPhone.Id;
            this.SLAAlertId = SLAAlertPhone.SLAAlertId;
            this.Phone = SLAAlertPhone.Phone;
            this.SLAAlert = SLAAlertPhone.SLAAlert == null ? null : new TicketIssueLevel_SLAAlertDTO(SLAAlertPhone.SLAAlert);
            this.CreatedAt = SLAAlertPhone.CreatedAt;
            this.UpdatedAt = SLAAlertPhone.UpdatedAt;
            this.Errors = SLAAlertPhone.Errors;
        }
    }

    public class TicketIssueLevel_SLAAlertPhoneFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertId { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAAlertPhoneOrder OrderBy { get; set; }
    }
}
