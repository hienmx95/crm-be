using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAEscalationUserDAO
    {
        public long Id { get; set; }
        public long? SLAEscalationId { get; set; }
        public long? AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual SLAEscalationDAO SLAEscalation { get; set; }
    }
}
