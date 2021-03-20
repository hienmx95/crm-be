using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAAlertUserDTO : DataDTO
    {
        public long Id { get; set; }
        public long? SLAAlertId { get; set; }
        public long? AppUserId { get; set; }
        public TicketIssueLevel_AppUserDTO AppUser { get; set; }
        public TicketIssueLevel_SLAAlertDTO SLAAlert { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAAlertUserDTO() {}
        public TicketIssueLevel_SLAAlertUserDTO(SLAAlertUser SLAAlertUser)
        {
            this.Id = SLAAlertUser.Id;
            this.SLAAlertId = SLAAlertUser.SLAAlertId;
            this.AppUserId = SLAAlertUser.AppUserId;
            this.AppUser = SLAAlertUser.AppUser == null ? null : new TicketIssueLevel_AppUserDTO(SLAAlertUser.AppUser);
            this.SLAAlert = SLAAlertUser.SLAAlert == null ? null : new TicketIssueLevel_SLAAlertDTO(SLAAlertUser.SLAAlert);
            this.CreatedAt = SLAAlertUser.CreatedAt;
            this.UpdatedAt = SLAAlertUser.UpdatedAt;
            this.Errors = SLAAlertUser.Errors;
        }
    }

    public class TicketIssueLevel_SLAAlertUserFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter SLAAlertId { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAAlertUserOrder OrderBy { get; set; }
    }
}
