using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CompanyEmailCCMappingDAO
    {
        public long CompanyEmailId { get; set; }
        public long AppUserId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CompanyEmailDAO CompanyEmail { get; set; }
    }
}
