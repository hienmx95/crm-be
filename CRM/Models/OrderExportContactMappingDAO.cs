using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderExportContactMappingDAO
    {
        public long OrderExportId { get; set; }
        public long ContactId { get; set; }

        public virtual ContactDAO Contact { get; set; }
        public virtual OrderExportDAO OrderExport { get; set; }
    }
}
