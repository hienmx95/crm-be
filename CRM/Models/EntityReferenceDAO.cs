using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class EntityReferenceDAO
    {
        public EntityReferenceDAO()
        {
            CallLogs = new HashSet<CallLogDAO>();
            SmsQueues = new HashSet<SmsQueueDAO>();
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CallLogDAO> CallLogs { get; set; }
        public virtual ICollection<SmsQueueDAO> SmsQueues { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
