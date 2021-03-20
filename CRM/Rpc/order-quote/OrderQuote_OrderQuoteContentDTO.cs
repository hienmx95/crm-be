using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.order_quote
{
    public class OrderQuote_OrderQuoteContentDTO : DataDTO
    {
        public long Id { get; set; }
        public long OrderQuoteId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public long RequestedQuantity { get; set; }
        public long PrimaryUnitOfMeasureId { get; set; }
        public decimal PrimaryPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxAmountOther { get; set; }
        public decimal? TaxPercentageOther { get; set; }
        public decimal Amount { get; set; }
        public long? Factor { get; set; }
        public long EditedPriceStatusId { get; set; }
        public long TaxTypeId { get; set; }
        public OrderQuote_EditedPriceStatusDTO EditedPriceStatus { get; set; }
        public OrderQuote_ItemDTO Item { get; set; }
        public OrderQuote_UnitOfMeasureDTO PrimaryUnitOfMeasure { get; set; }
        public OrderQuote_UnitOfMeasureDTO UnitOfMeasure { get; set; }
        public OrderQuote_TaxTypeDTO TaxType { get; set; }

        public OrderQuote_OrderQuoteContentDTO() { }
        public OrderQuote_OrderQuoteContentDTO(OrderQuoteContent OrderQuoteContent)
        {
            this.Id = OrderQuoteContent.Id;
            this.OrderQuoteId = OrderQuoteContent.OrderQuoteId;
            this.ItemId = OrderQuoteContent.ItemId;
            this.UnitOfMeasureId = OrderQuoteContent.UnitOfMeasureId;
            this.Quantity = OrderQuoteContent.Quantity;
            this.RequestedQuantity = OrderQuoteContent.RequestedQuantity;
            this.PrimaryUnitOfMeasureId = OrderQuoteContent.PrimaryUnitOfMeasureId;
            this.PrimaryPrice = OrderQuoteContent.PrimaryPrice;
            this.SalePrice = OrderQuoteContent.SalePrice;
            this.DiscountPercentage = OrderQuoteContent.DiscountPercentage;
            this.DiscountAmount = OrderQuoteContent.DiscountAmount;
            this.GeneralDiscountPercentage = OrderQuoteContent.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = OrderQuoteContent.GeneralDiscountAmount;
            this.TaxPercentage = OrderQuoteContent.TaxPercentage;
            this.TaxAmount = OrderQuoteContent.TaxAmount;
            this.TaxAmountOther = OrderQuoteContent.TaxAmountOther;
            this.TaxPercentageOther = OrderQuoteContent.TaxPercentageOther;
            this.Amount = OrderQuoteContent.Amount;
            this.Factor = OrderQuoteContent.Factor;
            this.EditedPriceStatusId = OrderQuoteContent.EditedPriceStatusId;
            this.TaxTypeId = OrderQuoteContent.TaxTypeId;
            this.EditedPriceStatus = OrderQuoteContent.EditedPriceStatus == null ? null : new OrderQuote_EditedPriceStatusDTO(OrderQuoteContent.EditedPriceStatus);
            this.Item = OrderQuoteContent.Item == null ? null : new OrderQuote_ItemDTO(OrderQuoteContent.Item);
            this.PrimaryUnitOfMeasure = OrderQuoteContent.PrimaryUnitOfMeasure == null ? null : new OrderQuote_UnitOfMeasureDTO(OrderQuoteContent.PrimaryUnitOfMeasure);
            this.UnitOfMeasure = OrderQuoteContent.UnitOfMeasure == null ? null : new OrderQuote_UnitOfMeasureDTO(OrderQuoteContent.UnitOfMeasure);
            this.TaxType = OrderQuoteContent.TaxType == null ? null : new OrderQuote_TaxTypeDTO(OrderQuoteContent.TaxType);
            this.Errors = OrderQuoteContent.Errors;
        }
    }

    public class OrderQuote_OrderQuoteContentFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public IdFilter OrderQuoteId { get; set; }

        public IdFilter ItemId { get; set; }

        public IdFilter UnitOfMeasureId { get; set; }

        public LongFilter Quantity { get; set; }

        public LongFilter RequestedQuantity { get; set; }

        public IdFilter PrimaryUnitOfMeasureId { get; set; }

        public DecimalFilter PrimaryPrice { get; set; }

        public DecimalFilter SalePrice { get; set; }

        public DecimalFilter DiscountPercentage { get; set; }

        public DecimalFilter DiscountAmount { get; set; }

        public DecimalFilter GeneralDiscountPercentage { get; set; }

        public DecimalFilter GeneralDiscountAmount { get; set; }

        public DecimalFilter TaxPercentage { get; set; }

        public DecimalFilter TaxAmount { get; set; }

        public DecimalFilter TaxAmountOther { get; set; }

        public DecimalFilter TaxPercentageOther { get; set; }

        public DecimalFilter Amount { get; set; }

        public LongFilter Factor { get; set; }

        public IdFilter EditedPriceStatusId { get; set; }
        public IdFilter TaxTypeId { get; set; }

        public OrderQuoteContentOrder OrderBy { get; set; }
    }
}