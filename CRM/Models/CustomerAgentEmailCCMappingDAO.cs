using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerAgentEmailCCMappingDAO
    {
        public long CustomerAgentEmailId { get; set; }
        public long AppUserId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CustomerAgentEmailDAO CustomerAgentEmail { get; set; }
    }
}
