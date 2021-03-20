using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContractItemDetail : DataEntity,  IEquatable<ContractItemDetail>
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
        public Contract Contract { get; set; }
        public Item Item { get; set; }
        public UnitOfMeasure PrimaryUnitOfMeasure { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public TaxType TaxType { get; set; }
        
        public bool Equals(ContractItemDetail other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ContractItemDetailFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter ContractId { get; set; }
        public IdFilter ItemId { get; set; }
        public IdFilter UnitOfMeasureId { get; set; }
        public IdFilter PrimaryUnitOfMeasureId { get; set; }
        public LongFilter Quantity { get; set; }
        public LongFilter RequestQuantity { get; set; }
        public DecimalFilter PrimaryPrice { get; set; }
        public DecimalFilter SalePrice { get; set; }
        public DecimalFilter DiscountPercentage { get; set; }
        public DecimalFilter DiscountAmount { get; set; }
        public DecimalFilter GeneralDiscountPercentage { get; set; }
        public DecimalFilter GeneralDiscountAmount { get; set; }
        public DecimalFilter TaxPercentage { get; set; }
        public DecimalFilter TaxAmount { get; set; }
        public DecimalFilter TaxPercentageOther { get; set; }
        public DecimalFilter TaxAmountOther { get; set; }
        public DecimalFilter TotalPrice { get; set; }
        public LongFilter Factor { get; set; }
        public IdFilter TaxTypeId { get; set; }
        public List<ContractItemDetailFilter> OrFilter { get; set; }
        public ContractItemDetailOrder OrderBy {get; set;}
        public ContractItemDetailSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractItemDetailOrder
    {
        Id = 0,
        Contract = 1,
        Item = 2,
        UnitOfMeasure = 3,
        PrimaryUnitOfMeasure = 4,
        Quantity = 5,
        RequestQuantity = 6,
        PrimaryPrice = 7,
        SalePrice = 8,
        DiscountPercentage = 9,
        DiscountAmount = 10,
        GeneralDiscountPercentage = 11,
        GeneralDiscountAmount = 12,
        TaxPercentage = 13,
        TaxAmount = 14,
        TaxPercentageOther = 15,
        TaxAmountOther = 16,
        TotalPrice = 17,
        Factor = 18,
        TaxType = 19,
    }

    [Flags]
    public enum ContractItemDetailSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Contract = E._1,
        Item = E._2,
        UnitOfMeasure = E._3,
        PrimaryUnitOfMeasure = E._4,
        Quantity = E._5,
        RequestQuantity = E._6,
        PrimaryPrice = E._7,
        SalePrice = E._8,
        DiscountPercentage = E._9,
        DiscountAmount = E._10,
        GeneralDiscountPercentage = E._11,
        GeneralDiscountAmount = E._12,
        TaxPercentage = E._13,
        TaxAmount = E._14,
        TaxPercentageOther = E._15,
        TaxAmountOther = E._16,
        TotalPrice = E._17,
        Factor = E._18,
        TaxType = E._19,
    }
}
