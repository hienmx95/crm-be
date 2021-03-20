using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OpportunityEmailCCMappingDAO
    {
        public long OpportunityEmailId { get; set; }
        public long AppUserId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual OpportunityEmailDAO OpportunityEmail { get; set; }
    }
}
