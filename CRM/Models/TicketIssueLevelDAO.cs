using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketIssueLevelDAO
    {
        public TicketIssueLevelDAO()
        {
            SLAAlertFRTs = new HashSet<SLAAlertFRTDAO>();
            SLAAlerts = new HashSet<SLAAlertDAO>();
            SLAEscalationFRTs = new HashSet<SLAEscalationFRTDAO>();
            SLAEscalations = new HashSet<SLAEscalationDAO>();
            SLAPolicies = new HashSet<SLAPolicyDAO>();
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long TicketGroupId { get; set; }
        public long StatusId { get; set; }
        public long SLA { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual TicketGroupDAO TicketGroup { get; set; }
        public virtual ICollection<SLAAlertFRTDAO> SLAAlertFRTs { get; set; }
        public virtual ICollection<SLAAlertDAO> SLAAlerts { get; set; }
        public virtual ICollection<SLAEscalationFRTDAO> SLAEscalationFRTs { get; set; }
        public virtual ICollection<SLAEscalationDAO> SLAEscalations { get; set; }
        public virtual ICollection<SLAPolicyDAO> SLAPolicies { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
