using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OpportunityFileMappingDAO
    {
        public long OpportunityFileGroupingId { get; set; }
        public long FileId { get; set; }

        public virtual FileDAO File { get; set; }
        public virtual OpportunityFileGroupingDAO OpportunityFileGrouping { get; set; }
    }
}
