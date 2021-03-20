using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerRetailCallLogMappingDAO
    {
        public long CustomerRetailId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual CustomerRetailDAO CustomerRetail { get; set; }
    }
}
