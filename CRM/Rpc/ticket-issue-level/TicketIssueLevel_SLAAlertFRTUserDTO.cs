using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAAlertFRTUserDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAAlertFRTId { get; set; }
        public long? AppUserId { get; set; }
        public TicketIssueLevel_AppUserDTO AppUser { get; set; }
        public TicketIssueLevel_SLAAlertFRTDTO SLAAlertFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAAlertFRTUserDTO() {}
        public TicketIssueLevel_SLAAlertFRTUserDTO(SLAAlertFRTUser SLAAlertFRTUser)
        {
            this.Id = SLAAlertFRTUser.Id;
            this.SLAAlertFRTId = SLAAlertFRTUser.SLAAlertFRTId;
            this.AppUserId = SLAAlertFRTUser.AppUserId;
            this.AppUser = SLAAlertFRTUser.AppUser == null ? null : new TicketIssueLevel_AppUserDTO(SLAAlertFRTUser.AppUser);
            this.SLAAlertFRT = SLAAlertFRTUser.SLAAlertFRT == null ? null : new TicketIssueLevel_SLAAlertFRTDTO(SLAAlertFRTUser.SLAAlertFRT);
            this.CreatedAt = SLAAlertFRTUser.CreatedAt;
            this.UpdatedAt = SLAAlertFRTUser.UpdatedAt;
            this.Errors = SLAAlertFRTUser.Errors;
        }
    }

    public class TicketIssueLevel_SLAAlertFRTUserFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertFRTId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAAlertFRTUserOrder OrderBy { get; set; }
    }
}
