using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContactEmailCCMappingDAO
    {
        public long ContactEmailId { get; set; }
        public long AppUserId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual ContactEmailDAO ContactEmail { get; set; }
    }
}
