using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerCCEmailHistoryDAO
    {
        public long Id { get; set; }
        public long CustomerEmailHistoryId { get; set; }
        public string CCEmail { get; set; }

        public virtual CustomerEmailHistoryDAO CustomerEmailHistory { get; set; }
    }
}
