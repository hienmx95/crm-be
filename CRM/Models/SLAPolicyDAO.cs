using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAPolicyDAO
    {
        public SLAPolicyDAO()
        {
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public long? TicketIssueLevelId { get; set; }
        public long? TicketPriorityId { get; set; }
        public long? FirstResponseTime { get; set; }
        public long? FirstResponseUnitId { get; set; }
        public long? ResolveTime { get; set; }
        public long? ResolveUnitId { get; set; }
        public bool? IsAlert { get; set; }
        public bool? IsAlertFRT { get; set; }
        public bool? IsEscalation { get; set; }
        public bool? IsEscalationFRT { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual SLATimeUnitDAO FirstResponseUnit { get; set; }
        public virtual SLATimeUnitDAO ResolveUnit { get; set; }
        public virtual TicketIssueLevelDAO TicketIssueLevel { get; set; }
        public virtual TicketPriorityDAO TicketPriority { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
