using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CompanyFileMappingDAO
    {
        public long CompanyFileGroupingId { get; set; }
        public long FileId { get; set; }

        public virtual CompanyFileGroupingDAO CompanyFileGrouping { get; set; }
        public virtual FileDAO File { get; set; }
    }
}
