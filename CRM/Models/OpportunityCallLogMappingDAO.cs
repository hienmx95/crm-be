using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OpportunityCallLogMappingDAO
    {
        public long OpportunityId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual OpportunityDAO Opportunity { get; set; }
    }
}
