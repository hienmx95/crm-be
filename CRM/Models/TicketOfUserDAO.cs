using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketOfUserDAO
    {
        public long Id { get; set; }
        public string Notes { get; set; }
        public long UserId { get; set; }
        public long TicketId { get; set; }
        public long TicketStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual TicketDAO Ticket { get; set; }
        public virtual TicketStatusDAO TicketStatus { get; set; }
        public virtual AppUserDAO User { get; set; }
    }
}
