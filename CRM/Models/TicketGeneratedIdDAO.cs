using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketGeneratedIdDAO
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }
    }
}
