using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerAgentCallLogMappingDAO
    {
        public long CustomerAgentId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual CustomerAgentDAO CustomerAgent { get; set; }
    }
}
