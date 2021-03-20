using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContactFileMappingDAO
    {
        public long ContactFileGroupingId { get; set; }
        public long FileId { get; set; }

        public virtual ContactFileGroupingDAO ContactFileGrouping { get; set; }
        public virtual FileDAO File { get; set; }
    }
}
