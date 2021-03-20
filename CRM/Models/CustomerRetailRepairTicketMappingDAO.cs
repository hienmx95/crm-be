using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerRetailRepairTicketMappingDAO
    {
        public long CustomerRetailId { get; set; }
        public long RepairTicketId { get; set; }

        public virtual CustomerRetailDAO CustomerRetail { get; set; }
        public virtual RepairTicketDAO RepairTicket { get; set; }
    }
}
