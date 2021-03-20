using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAAlertDAO
    {
        public SLAAlertDAO()
        {
            SLAAlertMails = new HashSet<SLAAlertMailDAO>();
            SLAAlertPhones = new HashSet<SLAAlertPhoneDAO>();
            SLAAlertUsers = new HashSet<SLAAlertUserDAO>();
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
        public virtual ICollection<SLAAlertMailDAO> SLAAlertMails { get; set; }
        public virtual ICollection<SLAAlertPhoneDAO> SLAAlertPhones { get; set; }
        public virtual ICollection<SLAAlertUserDAO> SLAAlertUsers { get; set; }
    }
}
