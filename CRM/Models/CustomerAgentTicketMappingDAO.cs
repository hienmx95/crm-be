using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerAgentTicketMappingDAO
    {
        public long CustomerAgentId { get; set; }
        public long TicketId { get; set; }

        public virtual CustomerAgentDAO CustomerAgent { get; set; }
        public virtual TicketDAO Ticket { get; set; }
    }
}
