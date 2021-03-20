using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class OrderQuoteContent : DataEntity, IEquatable<OrderQuoteContent>
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
        public EditedPriceStatus EditedPriceStatus { get; set; }
        public Item Item { get; set; }
        public OrderQuote OrderQuote { get; set; }
        public UnitOfMeasure PrimaryUnitOfMeasure { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public TaxType TaxType { get; set; }

        public bool Equals(OrderQuoteContent other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class OrderQuoteContentFilter : FilterEntity
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
        public List<OrderQuoteContentFilter> OrFilter { get; set; }
        public OrderQuoteContentOrder OrderBy { get; set; }
        public OrderQuoteContentSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderQuoteContentOrder
    {
        Id = 0,
        OrderQuote = 1,
        Item = 2,
        UnitOfMeasure = 3,
        Quantity = 4,
        RequestedQuantity = 5,
        PrimaryUnitOfMeasure = 6,
        PrimaryPrice = 7,
        SalePrice = 8,
        DiscountPercentage = 9,
        DiscountAmount = 10,
        GeneralDiscountPercentage = 11,
        GeneralDiscountAmount = 12,
        TaxPercentage = 13,
        TaxAmount = 14,
        TaxAmountOther = 15,
        TaxPercentageOther = 16,
        Amount = 17,
        Factor = 18,
        EditedPriceStatus = 19,
        TaxType = 20,
    }

    [Flags]
    public enum OrderQuoteContentSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        OrderQuote = E._1,
        Item = E._2,
        UnitOfMeasure = E._3,
        Quantity = E._4,
        RequestedQuantity = E._5,
        PrimaryUnitOfMeasure = E._6,
        PrimaryPrice = E._7,
        SalePrice = E._8,
        DiscountPercentage = E._9,
        DiscountAmount = E._10,
        GeneralDiscountPercentage = E._11,
        GeneralDiscountAmount = E._12,
        TaxPercentage = E._13,
        TaxAmount = E._14,
        TaxAmountOther = E._15,
        TaxPercentageOther = E._16,
        Amount = E._17,
        Factor = E._18,
        EditedPriceStatus = E._19,
        TaxType = E._20,
    }
}
