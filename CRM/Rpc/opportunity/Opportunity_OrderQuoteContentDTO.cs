using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OrderQuoteContentDTO : DataDTO
    {
        public long Id { get; set; }
        public long IndirectSalesOrderId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public long PrimaryUnitOfMeasureId { get; set; }
        public long EditedPriceStatusId { get; set; }
        public long RequestedQuantity { get; set; }
        public decimal PrimaryPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxPercentageOther { get; set; }
        public decimal? TaxAmount { get; set; }
        public long? Factor { get; set; }
        public Opportunity_ItemDTO Item { get; set; }
        public Opportunity_EditedPriceStatusDTO EditedPriceStatus { get; set; }
        public Opportunity_UnitOfMeasureDTO PrimaryUnitOfMeasure { get; set; }
        public Opportunity_UnitOfMeasureDTO UnitOfMeasure { get; set; }

        public Opportunity_OrderQuoteContentDTO() { }
        public Opportunity_OrderQuoteContentDTO(OrderQuoteContent OrderQuoteContent)
        {
            this.Id = OrderQuoteContent.Id;
            this.IndirectSalesOrderId = OrderQuoteContent.OrderQuoteId;
            this.ItemId = OrderQuoteContent.ItemId;
            this.UnitOfMeasureId = OrderQuoteContent.UnitOfMeasureId;
            this.Quantity = OrderQuoteContent.Quantity;
            this.PrimaryUnitOfMeasureId = OrderQuoteContent.PrimaryUnitOfMeasureId;
            this.EditedPriceStatusId = OrderQuoteContent.EditedPriceStatusId;
            this.RequestedQuantity = OrderQuoteContent.RequestedQuantity;
            this.PrimaryPrice = OrderQuoteContent.PrimaryPrice;
            this.SalePrice = OrderQuoteContent.SalePrice;
            this.DiscountPercentage = OrderQuoteContent.DiscountPercentage;
            this.DiscountAmount = OrderQuoteContent.DiscountAmount;
            this.GeneralDiscountPercentage = OrderQuoteContent.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = OrderQuoteContent.GeneralDiscountAmount;
            this.Amount = OrderQuoteContent.Amount;
            this.TaxPercentage = OrderQuoteContent.TaxPercentage;
            this.TaxPercentageOther = OrderQuoteContent.TaxPercentageOther;
            this.TaxAmount = OrderQuoteContent.TaxAmount;
            this.Factor = OrderQuoteContent.Factor;
            this.Item = OrderQuoteContent.Item == null ? null : new Opportunity_ItemDTO(OrderQuoteContent.Item);
            this.PrimaryUnitOfMeasure = OrderQuoteContent.PrimaryUnitOfMeasure == null ? null : new Opportunity_UnitOfMeasureDTO(OrderQuoteContent.PrimaryUnitOfMeasure);
            this.UnitOfMeasure = OrderQuoteContent.UnitOfMeasure == null ? null : new Opportunity_UnitOfMeasureDTO(OrderQuoteContent.UnitOfMeasure);
            this.EditedPriceStatus = OrderQuoteContent.EditedPriceStatus == null ? null : new Opportunity_EditedPriceStatusDTO(OrderQuoteContent.EditedPriceStatus);
            this.Errors = OrderQuoteContent.Errors;
        }
    }

    public class Opportunity_OrderQuoteContentFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public IdFilter OrderQuoteId { get; set; }

        public IdFilter ItemId { get; set; }

        public IdFilter UnitOfMeasureId { get; set; }

        public LongFilter Quantity { get; set; }

        public IdFilter PrimaryUnitOfMeasureId { get; set; }

        public IdFilter EditedPriceStatusId { get; set; }

        public LongFilter RequestedQuantity { get; set; }

        public LongFilter PrimaryPrice { get; set; }

        public LongFilter SalePrice { get; set; }

        public DecimalFilter DiscountPercentage { get; set; }

        public LongFilter DiscountAmount { get; set; }

        public DecimalFilter GeneralDiscountPercentage { get; set; }

        public LongFilter GeneralDiscountAmount { get; set; }

        public LongFilter Amount { get; set; }

        public DecimalFilter TaxPercentage { get; set; }

        public LongFilter TaxAmount { get; set; }

        public OrderQuoteContentOrder OrderBy { get; set; }
    }
}