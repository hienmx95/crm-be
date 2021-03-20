using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAAlertFRTMailDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAAlertFRTId { get; set; }
        public string Mail { get; set; }
        public TicketIssueLevel_SLAAlertFRTDTO SLAAlertFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAAlertFRTMailDTO() {}
        public TicketIssueLevel_SLAAlertFRTMailDTO(SLAAlertFRTMail SLAAlertFRTMail)
        {
            this.Id = SLAAlertFRTMail.Id;
            this.SLAAlertFRTId = SLAAlertFRTMail.SLAAlertFRTId;
            this.Mail = SLAAlertFRTMail.Mail;
            this.SLAAlertFRT = SLAAlertFRTMail.SLAAlertFRT == null ? null : new TicketIssueLevel_SLAAlertFRTDTO(SLAAlertFRTMail.SLAAlertFRT);
            this.CreatedAt = SLAAlertFRTMail.CreatedAt;
            this.UpdatedAt = SLAAlertFRTMail.UpdatedAt;
            this.Errors = SLAAlertFRTMail.Errors;
        }
    }

    public class TicketIssueLevel_SLAAlertFRTMailFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertFRTId { get; set; }
        public StringFilter Mail { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAAlertFRTMailOrder OrderBy { get; set; }
    }
}
