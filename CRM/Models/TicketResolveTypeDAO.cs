using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketResolveTypeDAO
    {
        public TicketResolveTypeDAO()
        {
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
