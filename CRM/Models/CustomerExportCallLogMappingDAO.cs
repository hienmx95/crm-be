using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerExportCallLogMappingDAO
    {
        public long CustomerExportId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual CustomerExportDAO CustomerExport { get; set; }
    }
}
