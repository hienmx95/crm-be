using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLAEscalationDTO : DataDTO
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
        public List<TicketIssueLevel_SLAEscalationMailDTO> SLAEscalationMails { get; set; }
        public List<TicketIssueLevel_SLAEscalationPhoneDTO> SLAEscalationPhones { get; set; }
        public List<TicketIssueLevel_SLAEscalationUserDTO> SLAEscalationUsers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketIssueLevel_SLAEscalationDTO() {}
        public TicketIssueLevel_SLAEscalationDTO(SLAEscalation SLAEscalation)
        {
            this.Id = SLAEscalation.Id;
            this.TicketIssueLevelId = SLAEscalation.TicketIssueLevelId;
            this.IsNotification = SLAEscalation.IsNotification;
            this.IsMail = SLAEscalation.IsMail;
            this.IsSMS = SLAEscalation.IsSMS;
            this.Time = SLAEscalation.Time;
            this.TimeUnitId = SLAEscalation.TimeUnitId;
            this.IsAssignedToUser = SLAEscalation.IsAssignedToUser;
            this.IsAssignedToGroup = SLAEscalation.IsAssignedToGroup;
            this.SmsTemplateId = SLAEscalation.SmsTemplateId;
            this.MailTemplateId = SLAEscalation.MailTemplateId;
            this.MailTemplate = SLAEscalation.MailTemplate == null ? null : new TicketIssueLevel_MailTemplateDTO(SLAEscalation.MailTemplate);
            this.SmsTemplate = SLAEscalation.SmsTemplate == null ? null : new TicketIssueLevel_SmsTemplateDTO(SLAEscalation.SmsTemplate);
            this.TicketIssueLevel = SLAEscalation.TicketIssueLevel == null ? null : new TicketIssueLevel_TicketIssueLevelDTO(SLAEscalation.TicketIssueLevel);
            this.TimeUnit = SLAEscalation.TimeUnit == null ? null : new TicketIssueLevel_SLATimeUnitDTO(SLAEscalation.TimeUnit);
            this.SLAEscalationMails = SLAEscalation.SLAEscalationMails?.Select(x => new TicketIssueLevel_SLAEscalationMailDTO(x)).ToList();
            this.SLAEscalationPhones = SLAEscalation.SLAEscalationPhones?.Select(x => new TicketIssueLevel_SLAEscalationPhoneDTO(x)).ToList();
            this.SLAEscalationUsers = SLAEscalation.SLAEscalationUsers?.Select(x => new TicketIssueLevel_SLAEscalationUserDTO(x)).ToList();
            this.CreatedAt = SLAEscalation.CreatedAt;
            this.UpdatedAt = SLAEscalation.UpdatedAt;
            this.Errors = SLAEscalation.Errors;
        }
    }

    public class TicketIssueLevel_SLAEscalationFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter TicketIssueLevelId { get; set; }
        public LongFilter Time { get; set; }
        public IdFilter TimeUnitId { get; set; }
        public IdFilter SmsTemplateId { get; set; }
        public IdFilter MailTemplateId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public SLAEscalationOrder OrderBy { get; set; }
    }
}
