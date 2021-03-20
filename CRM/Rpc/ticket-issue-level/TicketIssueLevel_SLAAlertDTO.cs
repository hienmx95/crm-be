using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAAlertDTO : DataDTO
    {
        public long Id { get; set; }
        public long? TicketIssueLevelId { get; set; }
        public bool? IsNotification { get; set; }
        public bool? IsMail { get; set; }
        public bool? IsSMS { get; set; }
        public long? Time { get; set; }
        public long? TimeUnitId { get; set; }
        public bool? IsAssignedToUser { get; set; }
        public bool? IsAssignedToGroup { get; set; }
        public long? SmsTemplateId { get; set; }
        public long? MailTemplateId { get; set; }
        public TicketIssueLevel_MailTemplateDTO MailTemplate { get; set; }
        public TicketIssueLevel_SmsTemplateDTO SmsTemplate { get; set; }
        public TicketIssueLevel_TicketIssueLevelDTO TicketIssueLevel { get; set; }
        public List<TicketIssueLevel_SLAAlertMailDTO> SLAAlertMails { get; set; }
        public List<TicketIssueLevel_SLAAlertPhoneDTO> SLAAlertPhones { get; set; }
        public List<TicketIssueLevel_SLAAlertUserDTO> SLAAlertUsers { get; set; }
        public TicketIssueLevel_SLATimeUnitDTO TimeUnit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAAlertDTO() {}
        public TicketIssueLevel_SLAAlertDTO(SLAAlert SLAAlert)
        {
            this.Id = SLAAlert.Id;
            this.TicketIssueLevelId = SLAAlert.TicketIssueLevelId;
            this.IsNotification = SLAAlert.IsNotification;
            this.IsMail = SLAAlert.IsMail;
            this.IsSMS = SLAAlert.IsSMS;
            this.Time = SLAAlert.Time;
            this.TimeUnitId = SLAAlert.TimeUnitId;
            this.IsAssignedToUser = SLAAlert.IsAssignedToUser;
            this.IsAssignedToGroup = SLAAlert.IsAssignedToGroup;
            this.SmsTemplateId = SLAAlert.SmsTemplateId;
            this.MailTemplateId = SLAAlert.MailTemplateId;
            this.MailTemplate = SLAAlert.MailTemplate == null ? null : new TicketIssueLevel_MailTemplateDTO(SLAAlert.MailTemplate);
            this.SmsTemplate = SLAAlert.SmsTemplate == null ? null : new TicketIssueLevel_SmsTemplateDTO(SLAAlert.SmsTemplate);
            this.TicketIssueLevel = SLAAlert.TicketIssueLevel == null ? null : new TicketIssueLevel_TicketIssueLevelDTO(SLAAlert.TicketIssueLevel);
            this.TimeUnit = SLAAlert.TimeUnit == null ? null : new TicketIssueLevel_SLATimeUnitDTO(SLAAlert.TimeUnit);
            this.SLAAlertMails = SLAAlert.SLAAlertMails?.Select(x => new TicketIssueLevel_SLAAlertMailDTO(x)).ToList();
            this.SLAAlertPhones = SLAAlert.SLAAlertPhones?.Select(x => new TicketIssueLevel_SLAAlertPhoneDTO(x)).ToList();
            this.SLAAlertUsers = SLAAlert.SLAAlertUsers?.Select(x => new TicketIssueLevel_SLAAlertUserDTO(x)).ToList();
            this.CreatedAt = SLAAlert.CreatedAt;
            this.UpdatedAt = SLAAlert.UpdatedAt;
            this.Errors = SLAAlert.Errors;
        }
    }

    public class TicketIssueLevel_SLAAlertFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter TicketIssueLevelId { get; set; }
        public LongFilter Time { get; set; }
        public IdFilter TimeUnitId { get; set; }
        public IdFilter SmsTemplateId { get; set; }
        public IdFilter MailTemplateId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAAlertOrder OrderBy { get; set; }
    }
}
