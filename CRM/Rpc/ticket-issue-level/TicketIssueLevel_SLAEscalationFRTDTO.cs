using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAEscalationFRTDTO : DataDTO
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
        public List<TicketIssueLevel_SLAEscalationFRTMailDTO> SLAEscalationFRTMails { get; set; }
        public List<TicketIssueLevel_SLAEscalationFRTPhoneDTO> SLAEscalationFRTPhones { get; set; }
        public List<TicketIssueLevel_SLAEscalationFRTUserDTO> SLAEscalationFRTUsers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAEscalationFRTDTO() {}
        public TicketIssueLevel_SLAEscalationFRTDTO(SLAEscalationFRT SLAEscalationFRT)
        {
            this.Id = SLAEscalationFRT.Id;
            this.TicketIssueLevelId = SLAEscalationFRT.TicketIssueLevelId;
            this.IsNotification = SLAEscalationFRT.IsNotification;
            this.IsMail = SLAEscalationFRT.IsMail;
            this.IsSMS = SLAEscalationFRT.IsSMS;
            this.Time = SLAEscalationFRT.Time;
            this.TimeUnitId = SLAEscalationFRT.TimeUnitId;
            this.IsAssignedToUser = SLAEscalationFRT.IsAssignedToUser;
            this.IsAssignedToGroup = SLAEscalationFRT.IsAssignedToGroup;
            this.SmsTemplateId = SLAEscalationFRT.SmsTemplateId;
            this.MailTemplateId = SLAEscalationFRT.MailTemplateId;
            this.MailTemplate = SLAEscalationFRT.MailTemplate == null ? null : new TicketIssueLevel_MailTemplateDTO(SLAEscalationFRT.MailTemplate);
            this.SmsTemplate = SLAEscalationFRT.SmsTemplate == null ? null : new TicketIssueLevel_SmsTemplateDTO(SLAEscalationFRT.SmsTemplate);
            this.TicketIssueLevel = SLAEscalationFRT.TicketIssueLevel == null ? null : new TicketIssueLevel_TicketIssueLevelDTO(SLAEscalationFRT.TicketIssueLevel);
            this.TimeUnit = SLAEscalationFRT.TimeUnit == null ? null : new TicketIssueLevel_SLATimeUnitDTO(SLAEscalationFRT.TimeUnit);
            this.SLAEscalationFRTMails = SLAEscalationFRT.SLAEscalationFRTMails?.Select(x => new TicketIssueLevel_SLAEscalationFRTMailDTO(x)).ToList();
            this.SLAEscalationFRTPhones = SLAEscalationFRT.SLAEscalationFRTPhones?.Select(x => new TicketIssueLevel_SLAEscalationFRTPhoneDTO(x)).ToList();
            this.SLAEscalationFRTUsers = SLAEscalationFRT.SLAEscalationFRTUsers?.Select(x => new TicketIssueLevel_SLAEscalationFRTUserDTO(x)).ToList();
            this.CreatedAt = SLAEscalationFRT.CreatedAt;
            this.UpdatedAt = SLAEscalationFRT.UpdatedAt;
            this.Errors = SLAEscalationFRT.Errors;
        }
    }

    public class TicketIssueLevel_SLAEscalationFRTFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter TicketIssueLevelId { get; set; }
        public LongFilter Time { get; set; }
        public IdFilter TimeUnitId { get; set; }
        public IdFilter SmsTemplateId { get; set; }
        public IdFilter MailTemplateId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAEscalationFRTOrder OrderBy { get; set; }
    }
}
