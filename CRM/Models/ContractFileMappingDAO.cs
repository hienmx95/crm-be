using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContractFileMappingDAO
    {
        public long ContractFileGroupingId { get; set; }
        public long FileId { get; set; }

        public virtual ContractFileGroupingDAO ContractFileGrouping { get; set; }
        public virtual FileDAO File { get; set; }
    }
}
