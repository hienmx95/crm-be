using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAAlertMailDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAAlertId { get; set; }
        public string Mail { get; set; }
        public TicketIssueLevel_SLAAlertDTO SLAAlert { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAAlertMailDTO() {}
        public TicketIssueLevel_SLAAlertMailDTO(SLAAlertMail SLAAlertMail)
        {
            this.Id = SLAAlertMail.Id;
            this.SLAAlertId = SLAAlertMail.SLAAlertId;
            this.Mail = SLAAlertMail.Mail;
            this.SLAAlert = SLAAlertMail.SLAAlert == null ? null : new TicketIssueLevel_SLAAlertDTO(SLAAlertMail.SLAAlert);
            this.CreatedAt = SLAAlertMail.CreatedAt;
            this.UpdatedAt = SLAAlertMail.UpdatedAt;
            this.Errors = SLAAlertMail.Errors;
        }
    }

    public class TicketIssueLevel_SLAAlertMailFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertId { get; set; }
        public StringFilter Mail { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAAlertMailOrder OrderBy { get; set; }
    }
}
