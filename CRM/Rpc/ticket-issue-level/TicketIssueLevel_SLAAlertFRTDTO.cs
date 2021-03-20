using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAAlertFRTDTO : DataDTO
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
        public TicketIssueLevel_SLATimeUnitDTO TimeUnit { get; set; }
        public List<TicketIssueLevel_SLAAlertFRTMailDTO> SLAAlertFRTMails { get; set; }
        public List<TicketIssueLevel_SLAAlertFRTPhoneDTO> SLAAlertFRTPhones { get; set; }
        public List<TicketIssueLevel_SLAAlertFRTUserDTO> SLAAlertFRTUsers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAAlertFRTDTO() {}
        public TicketIssueLevel_SLAAlertFRTDTO(SLAAlertFRT SLAAlertFRT)
        {
            this.Id = SLAAlertFRT.Id;
            this.TicketIssueLevelId = SLAAlertFRT.TicketIssueLevelId;
            this.IsNotification = SLAAlertFRT.IsNotification;
            this.IsMail = SLAAlertFRT.IsMail;
            this.IsSMS = SLAAlertFRT.IsSMS;
            this.Time = SLAAlertFRT.Time;
            this.TimeUnitId = SLAAlertFRT.TimeUnitId;
            this.IsAssignedToUser = SLAAlertFRT.IsAssignedToUser;
            this.IsAssignedToGroup = SLAAlertFRT.IsAssignedToGroup;
            this.SmsTemplateId = SLAAlertFRT.SmsTemplateId;
            this.MailTemplateId = SLAAlertFRT.MailTemplateId;
            this.MailTemplate = SLAAlertFRT.MailTemplate == null ? null : new TicketIssueLevel_MailTemplateDTO(SLAAlertFRT.MailTemplate);
            this.SmsTemplate = SLAAlertFRT.SmsTemplate == null ? null : new TicketIssueLevel_SmsTemplateDTO(SLAAlertFRT.SmsTemplate);
            this.TicketIssueLevel = SLAAlertFRT.TicketIssueLevel == null ? null : new TicketIssueLevel_TicketIssueLevelDTO(SLAAlertFRT.TicketIssueLevel);
            this.TimeUnit = SLAAlertFRT.TimeUnit == null ? null : new TicketIssueLevel_SLATimeUnitDTO(SLAAlertFRT.TimeUnit);
            this.SLAAlertFRTMails = SLAAlertFRT.SLAAlertFRTMails?.Select(x => new TicketIssueLevel_SLAAlertFRTMailDTO(x)).ToList();
            this.SLAAlertFRTPhones = SLAAlertFRT.SLAAlertFRTPhones?.Select(x => new TicketIssueLevel_SLAAlertFRTPhoneDTO(x)).ToList();
            this.SLAAlertFRTUsers = SLAAlertFRT.SLAAlertFRTUsers?.Select(x => new TicketIssueLevel_SLAAlertFRTUserDTO(x)).ToList();
            this.CreatedAt = SLAAlertFRT.CreatedAt;
            this.UpdatedAt = SLAAlertFRT.UpdatedAt;
            this.Errors = SLAAlertFRT.Errors;
        }
    }

    public class TicketIssueLevel_SLAAlertFRTFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter TicketIssueLevelId { get; set; }
        public LongFilter Time { get; set; }
        public IdFilter TimeUnitId { get; set; }
        public IdFilter SmsTemplateId { get; set; }
        public IdFilter MailTemplateId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAAlertFRTOrder OrderBy { get; set; }
    }
}
