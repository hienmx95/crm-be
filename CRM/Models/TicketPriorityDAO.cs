using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketPriorityDAO
    {
        public TicketPriorityDAO()
        {
            SLAPolicies = new HashSet<SLAPolicyDAO>();
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<SLAPolicyDAO> SLAPolicies { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
