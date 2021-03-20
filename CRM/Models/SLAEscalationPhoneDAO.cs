using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAEscalationPhoneDAO
    {
        public long Id { get; set; }
        public long? SLAEscalationId { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
