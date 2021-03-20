using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerPointHistoryDAO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long TotalPoint { get; set; }
        public long CurrentPoint { get; set; }
        public long ChangePoint { get; set; }
        public bool IsIncrease { get; set; }
        public string Description { get; set; }
        public bool ReduceTotal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual CustomerDAO Customer { get; set; }
    }
}
