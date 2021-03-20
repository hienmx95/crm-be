using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContractItemDetailDAO
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long PrimaryUnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public long RequestQuantity { get; set; }
        public decimal PrimaryPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxPercentageOther { get; set; }
        public decimal? TaxAmountOther { get; set; }
        public decimal TotalPrice { get; set; }
        public long? Factor { get; set; }
        public long TaxTypeId { get; set; }

        public virtual ContractDAO Contract { get; set; }
        public virtual ItemDAO Item { get; set; }
        public virtual UnitOfMeasureDAO PrimaryUnitOfMeasure { get; set; }
        public virtual TaxTypeDAO TaxType { get; set; }
        public virtual UnitOfMeasureDAO UnitOfMeasure { get; set; }
    }
}
