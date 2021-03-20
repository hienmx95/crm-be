using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerRetailEmailCCMappingDAO
    {
        public long CustomerRetailEmailId { get; set; }
        public long AppUserId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CustomerRetailEmailDAO CustomerRetailEmail { get; set; }
    }
}
