using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CompanyCallLogMappingDAO
    {
        public long CompanyId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual CompanyDAO Company { get; set; }
    }
}
