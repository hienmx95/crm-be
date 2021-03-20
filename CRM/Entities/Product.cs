using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Product : DataEntity,  IEquatable<Product>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string SupplierCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScanCode { get; set; }
        public string ERPCode { get; set; }
        public long CategoryId { get; set; }
        public long ProductTypeId { get; set; }
        public long? SupplierId { get; set; }
        public long? BrandId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long? UnitOfMeasureGroupingId { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public long TaxTypeId { get; set; }
        public long StatusId { get; set; }
        public string OtherName { get; set; }
        public string TechnicalName { get; set; }
        public string Note { get; set; }
        public bool IsNew { get; set; }
        public long UsedVariationId { get; set; }
        public long VariationCounter { get; set; }
        public bool Used { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public ProductType ProductType { get; set; }
        public Supplier Supplier { get; set; }
        public TaxType TaxType { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public UnitOfMeasureGrouping UnitOfMeasureGrouping { get; set; }
        public List<Item> Items { get; set; }
        public List<ProductImageMapping> ProductImageMappings { get; set; }
        public List<CustomerLeadItemMapping> CustomerLeadItemMappings { get; set; }
        public List<OpportunityItemMapping> OpportunityProductMappings { get; set; }
        public List<ProductProductGroupingMapping> ProductProductGroupingMappings { get; set; }
        public List<VariationGrouping> VariationGroupings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }
        public bool Equals(Product other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ProductFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter SupplierCode { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public StringFilter ScanCode { get; set; }
        public StringFilter ERPCode { get; set; }
        public IdFilter ProductTypeId { get; set; }
        public IdFilter CategoryId { get; set; }
        public IdFilter SupplierId { get; set; }
        public IdFilter BrandId { get; set; }
        public IdFilter UnitOfMeasureId { get; set; }
        public IdFilter UnitOfMeasureGroupingId { get; set; }
        public DecimalFilter SalePrice { get; set; }
        public DecimalFilter RetailPrice { get; set; }
        public IdFilter TaxTypeId { get; set; }
        public IdFilter StatusId { get; set; }
        public StringFilter OtherName { get; set; }
        public StringFilter TechnicalName { get; set; }
        public StringFilter Note { get; set; }
        public IdFilter UsedVariationId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<ProductFilter> OrFilter { get; set; }
        public ProductOrder OrderBy {get; set;}
        public ProductSelect Selects {get; set;}

        public IdFilter CustomerLeadId { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProductOrder
    {
        Id = 0,
        Code = 1,
        SupplierCode = 2,
        Name = 3,
        Description = 4,
        ScanCode = 5,
        ERPCode = 6,
        ProductType = 7,
        Supplier = 8,
        Brand = 9,
        UnitOfMeasure = 10,
        UnitOfMeasureGrouping = 11,
        SalePrice = 12,
        RetailPrice = 13,
        TaxType = 14,
        Status = 15,
        OtherName = 16,
        TechnicalName = 17,
        Note = 18,
        IsNew = 19,
        UsedVariation = 20,
        Used = 24,
        Category = 25,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum ProductSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        SupplierCode = E._2,
        Name = E._3,
        Description = E._4,
        ScanCode = E._5,
        ERPCode = E._6,
        ProductType = E._7,
        Supplier = E._8,
        Brand = E._9,
        UnitOfMeasure = E._10,
        UnitOfMeasureGrouping = E._11,
        SalePrice = E._12,
        RetailPrice = E._13,
        TaxType = E._14,
        Status = E._15,
        OtherName = E._16,
        TechnicalName = E._17,
        Note = E._18,
        IsNew = E._19,
        UsedVariation = E._20,
        Used = E._24,
        Category = E._25,
        ProductProductGroupingMapping = E._25,
    }
}
