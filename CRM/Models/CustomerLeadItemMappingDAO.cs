using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLeadItemMappingDAO
    {
        public long CustomerLeadId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public decimal RequestQuantity { get; set; }
        public decimal PrimaryPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Discount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? VATOther { get; set; }
        public decimal TotalPrice { get; set; }
        public long? Factor { get; set; }

        public virtual CustomerLeadDAO CustomerLead { get; set; }
        public virtual ItemDAO Item { get; set; }
        public virtual UnitOfMeasureDAO UnitOfMeasure { get; set; }
    }
}
