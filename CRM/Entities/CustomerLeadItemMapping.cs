using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerLeadItemMapping : DataEntity, IEquatable<CustomerLeadItemMapping>
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
        public CustomerLead CustomerLead { get; set; }
        public Item Item { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public bool Equals(CustomerLeadItemMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CustomerLeadItemMappingFilter : FilterEntity
    {
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter ItemId { get; set; }
        public IdFilter UnitOfMeasureId { get; set; }
        public LongFilter Quantity { get; set; }
        public DecimalFilter RequestQuantity { get; set; }
        public DecimalFilter PrimaryPrice { get; set; }
        public DecimalFilter SalePrice { get; set; }
        public DecimalFilter DiscountPercentage { get; set; }
        public DecimalFilter Discount { get; set; }
        public DecimalFilter VAT { get; set; }
        public DecimalFilter VATOther { get; set; }
        public DecimalFilter TotalPrice { get; set; }
        public LongFilter Factor { get; set; }
        public List<CustomerLeadItemMappingFilter> OrFilter { get; set; }
        public CustomerLeadItemMappingOrder OrderBy { get; set; }
        public CustomerLeadItemMappingSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerLeadItemMappingOrder
    {
        CustomerLead = 0,
        Item = 1,
        UnitOfMeasure = 2,
        Quantity = 3,
        RequestQuantity = 4,
        PrimaryPrice = 5,
        SalePrice = 6,
        DiscountPercentage = 7,
        Discount = 8,
        VAT = 9,
        VATOther = 10,
        TotalPrice = 11,
        Factor = 12,
    }

    [Flags]
    public enum CustomerLeadItemMappingSelect : long
    {
        ALL = E.ALL,
        CustomerLead = E._0,
        Item = E._1,
        UnitOfMeasure = E._2,
        Quantity = E._3,
        RequestQuantity = E._4,
        PrimaryPrice = E._5,
        SalePrice = E._6,
        DiscountPercentage = E._7,
        Discount = E._8,
        VAT = E._9,
        VATOther = E._10,
        TotalPrice = E._11,
        Factor = E._12,
    }
}
