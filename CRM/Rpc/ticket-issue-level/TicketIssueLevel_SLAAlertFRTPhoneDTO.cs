using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAAlertFRTPhoneDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAAlertFRTId { get; set; }
        public string Phone { get; set; }
        public TicketIssueLevel_SLAAlertFRTDTO SLAAlertFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAAlertFRTPhoneDTO() {}
        public TicketIssueLevel_SLAAlertFRTPhoneDTO(SLAAlertFRTPhone SLAAlertFRTPhone)
        {
            this.Id = SLAAlertFRTPhone.Id;
            this.SLAAlertFRTId = SLAAlertFRTPhone.SLAAlertFRTId;
            this.Phone = SLAAlertFRTPhone.Phone;
            this.SLAAlertFRT = SLAAlertFRTPhone.SLAAlertFRT == null ? null : new TicketIssueLevel_SLAAlertFRTDTO(SLAAlertFRTPhone.SLAAlertFRT);
            this.CreatedAt = SLAAlertFRTPhone.CreatedAt;
            this.UpdatedAt = SLAAlertFRTPhone.UpdatedAt;
            this.Errors = SLAAlertFRTPhone.Errors;
        }
    }

    public class TicketIssueLevel_SLAAlertFRTPhoneFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertFRTId { get; set; }
        public StringFilter Phone { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAAlertFRTPhoneOrder OrderBy { get; set; }
    }
}
