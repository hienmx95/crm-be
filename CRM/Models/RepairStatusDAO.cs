using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class RepairStatusDAO
    {
        public RepairStatusDAO()
        {
            RepairTickets = new HashSet<RepairTicketDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<RepairTicketDAO> RepairTickets { get; set; }
    }
}
