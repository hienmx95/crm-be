using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderRetailPaymentHistoryDAO
    {
        public long Id { get; set; }
        public long OrderRetailId { get; set; }
        public string PaymentMilestone { get; set; }
        public decimal? PaymentPercentage { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string Description { get; set; }
        public bool? IsPaid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual OrderRetailDAO OrderRetail { get; set; }
    }
}
