using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class MailTemplateDAO
    {
        public MailTemplateDAO()
        {
            SLAAlertFRTs = new HashSet<SLAAlertFRTDAO>();
            SLAAlerts = new HashSet<SLAAlertDAO>();
            SLAEscalationFRTs = new HashSet<SLAEscalationFRTDAO>();
            SLAEscalations = new HashSet<SLAEscalationDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<SLAAlertFRTDAO> SLAAlertFRTs { get; set; }
        public virtual ICollection<SLAAlertDAO> SLAAlerts { get; set; }
        public virtual ICollection<SLAEscalationFRTDAO> SLAEscalationFRTs { get; set; }
        public virtual ICollection<SLAEscalationDAO> SLAEscalations { get; set; }
    }
}
