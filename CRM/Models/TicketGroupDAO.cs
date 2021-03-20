using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketGroupDAO
    {
        public TicketGroupDAO()
        {
            TicketIssueLevels = new HashSet<TicketIssueLevelDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long StatusId { get; set; }
        public long TicketTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual TicketTypeDAO TicketType { get; set; }
        public virtual ICollection<TicketIssueLevelDAO> TicketIssueLevels { get; set; }
    }
}
