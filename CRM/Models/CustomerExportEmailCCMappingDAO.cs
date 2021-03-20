using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerExportEmailCCMappingDAO
    {
        public long CustomerExportEmailId { get; set; }
        public long AppUserId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CustomerExportEmailDAO CustomerExportEmail { get; set; }
    }
}
