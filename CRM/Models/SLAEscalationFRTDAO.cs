using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAEscalationFRTDAO
    {
        public SLAEscalationFRTDAO()
        {
            SLAEscalationFRTMails = new HashSet<SLAEscalationFRTMailDAO>();
            SLAEscalationFRTPhones = new HashSet<SLAEscalationFRTPhoneDAO>();
            SLAEscalationFRTUsers = new HashSet<SLAEscalationFRTUserDAO>();
        }

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
        public Guid? RowId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual MailTemplateDAO MailTemplate { get; set; }
        public virtual SmsTemplateDAO SmsTemplate { get; set; }
        public virtual TicketIssueLevelDAO TicketIssueLevel { get; set; }
        public virtual SLATimeUnitDAO TimeUnit { get; set; }
        public virtual ICollection<SLAEscalationFRTMailDAO> SLAEscalationFRTMails { get; set; }
        public virtual ICollection<SLAEscalationFRTPhoneDAO> SLAEscalationFRTPhones { get; set; }
        public virtual ICollection<SLAEscalationFRTUserDAO> SLAEscalationFRTUsers { get; set; }
    }
}
