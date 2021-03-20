using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContactCallLogMappingDAO
    {
        public long ContactId { get; set; }
        public long CallLogId { get; set; }

        public virtual CallLogDAO CallLog { get; set; }
        public virtual ContactDAO Contact { get; set; }
    }
}
