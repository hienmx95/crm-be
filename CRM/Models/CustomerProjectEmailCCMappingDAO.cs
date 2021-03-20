using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerProjectEmailCCMappingDAO
    {
        public long CustomerProjectEmailId { get; set; }
        public long AppUserId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CustomerProjectEmailDAO CustomerProjectEmail { get; set; }
    }
}
