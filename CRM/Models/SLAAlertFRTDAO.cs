using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAAlertFRTDAO
    {
        public SLAAlertFRTDAO()
        {
            SLAAlertFRTMails = new HashSet<SLAAlertFRTMailDAO>();
            SLAAlertFRTPhones = new HashSet<SLAAlertFRTPhoneDAO>();
            SLAAlertFRTUsers = new HashSet<SLAAlertFRTUserDAO>();
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
        public virtual ICollection<SLAAlertFRTMailDAO> SLAAlertFRTMails { get; set; }
        public virtual ICollection<SLAAlertFRTPhoneDAO> SLAAlertFRTPhones { get; set; }
        public virtual ICollection<SLAAlertFRTUserDAO> SLAAlertFRTUsers { get; set; }
    }
}
