using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAEscalationFRTPhoneDAO
    {
        public long Id { get; set; }
        public long? SLAEscalationFRTId { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual SLAEscalationFRTDAO SLAEscalationFRT { get; set; }
    }
}
