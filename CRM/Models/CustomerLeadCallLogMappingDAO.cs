using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLeadCallLogMappingDAO
    {
        public long CustomerLeadId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual CustomerLeadDAO CustomerLead { get; set; }
    }
}
