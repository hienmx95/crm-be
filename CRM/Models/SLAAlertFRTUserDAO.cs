using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLAAlertFRTUserDAO
    {
        public long Id { get; set; }
        public long? SLAAlertFRTId { get; set; }
        public long? AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual SLAAlertFRTDAO SLAAlertFRT { get; set; }
    }
}
