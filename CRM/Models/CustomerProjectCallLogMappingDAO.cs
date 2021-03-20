using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerProjectCallLogMappingDAO
    {
        public long CustomerProjectId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual CustomerProjectDAO CustomerProject { get; set; }
    }
}
