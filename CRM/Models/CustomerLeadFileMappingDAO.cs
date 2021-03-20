using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLeadFileMappingDAO
    {
        public long CustomerLeadFileGroupId { get; set; }
        public long FileId { get; set; }

        public virtual CustomerLeadFileGroupDAO CustomerLeadFileGroup { get; set; }
        public virtual FileDAO File { get; set; }
    }
}
