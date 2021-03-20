using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerRetailTicketMappingDAO
    {
        public long CustomerRetailId { get; set; }
        public long TicketId { get; set; }

        public virtual CustomerRetailDAO CustomerRetail { get; set; }
        public virtual TicketDAO Ticket { get; set; }
    }
}
