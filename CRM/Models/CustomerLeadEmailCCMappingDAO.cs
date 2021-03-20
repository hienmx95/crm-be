using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLeadEmailCCMappingDAO
    {
        public long CustomerLeadEmailId { get; set; }
        public long AppUserId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CustomerLeadEmailDAO CustomerLeadEmail { get; set; }
    }
}
