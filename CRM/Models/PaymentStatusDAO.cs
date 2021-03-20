using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class PaymentStatusDAO
    {
        public PaymentStatusDAO()
        {
            Contracts = new HashSet<ContractDAO>();
            RepairTickets = new HashSet<RepairTicketDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ContractDAO> Contracts { get; set; }
        public virtual ICollection<RepairTicketDAO> RepairTickets { get; set; }
    }
}
