using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderRetailContentDAO
    {
        public long Id { get; set; }
        public long OrderRetailId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMesuareId { get; set; }
        public long Quantity { get; set; }
        public long PrimaryUnitOfMeasureId { get; set; }
        public long RequestedQuantity { get; set; }
        public decimal PrimaryPrice { get; set; }
        public decimal SalePrice { get; set; }
        public long EditedPriceStatusId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxPercentageOther { get; set; }
        public decimal? TaxAmountOther { get; set; }
        public decimal Amount { get; set; }
        public long? Factor { get; set; }

        public virtual EditedPriceStatusDAO EditedPriceStatus { get; set; }
        public virtual ItemDAO Item { get; set; }
        public virtual OrderRetailDAO OrderRetail { get; set; }
        public virtual UnitOfMeasureDAO PrimaryUnitOfMeasure { get; set; }
        public virtual UnitOfMeasureDAO UnitOfMesuare { get; set; }
    }
}
