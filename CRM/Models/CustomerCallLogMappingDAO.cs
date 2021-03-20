using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerCallLogMappingDAO
    {
        public long CustomerId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual CustomerDAO Customer { get; set; }
    }
}
