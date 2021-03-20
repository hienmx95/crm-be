using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAStatusDAO
    {
        public SLAStatusDAO()
        {
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }

        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
