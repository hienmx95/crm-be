using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketTypeDAO
    {
        public TicketTypeDAO()
        {
            TicketGroups = new HashSet<TicketGroupDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<TicketGroupDAO> TicketGroups { get; set; }
    }
}
