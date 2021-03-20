using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_ProductDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string SupplierCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScanCode { get; set; }
        public string ERPCode { get; set; }
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
        public bool Used { get; set; }
        public Customer_TaxTypeDTO TaxType { get; set; }
        public Customer_ProductTypeDTO ProductType { get; set; }
        public Customer_SupplierDTO Supplier { get; set; }
        public Customer_UnitOfMeasureDTO UnitOfMeasure { get; set; }
        public List<Customer_ProductProductGroupingMappingDTO> ProductProductGroupingMappings { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Customer_ProductDTO() { }
        public Customer_ProductDTO(Product Product)
        {
            this.Id = Product.Id;
            this.Code = Product.Code;
            this.SupplierCode = Product.SupplierCode;
            this.Name = Product.Name;
            this.Description = Product.Description;
            this.ScanCode = Product.ScanCode;
            this.ERPCode = Product.ERPCode;
            this.ProductTypeId = Product.ProductTypeId;
            this.SupplierId = Product.SupplierId;
            this.BrandId = Product.BrandId;
            this.UnitOfMeasureId = Product.UnitOfMeasureId;
            this.UnitOfMeasureGroupingId = Product.UnitOfMeasureGroupingId;
            this.SalePrice = Product.SalePrice;
            this.RetailPrice = Product.RetailPrice;
            this.TaxTypeId = Product.TaxTypeId;
            this.StatusId = Product.StatusId;
            this.OtherName = Product.OtherName;
            this.TechnicalName = Product.TechnicalName;
            this.Note = Product.Note;
            this.IsNew = Product.IsNew;
            this.UsedVariationId = Product.UsedVariationId;
            this.Used = Product.Used;
            this.TaxType = Product.TaxType == null ? null : new Customer_TaxTypeDTO(Product.TaxType);
            this.ProductType = Product.ProductType == null ? null : new Customer_ProductTypeDTO(Product.ProductType);
            this.Supplier = Product.Supplier == null ? null : new Customer_SupplierDTO(Product.Supplier);
            this.UnitOfMeasure = Product.UnitOfMeasure == null ? null : new Customer_UnitOfMeasureDTO(Product.UnitOfMeasure);
            this.ProductProductGroupingMappings = Product.ProductProductGroupingMappings?.Select(x => new Customer_ProductProductGroupingMappingDTO(x)).ToList();
            this.CreatedAt = Product.CreatedAt;
            this.UpdatedAt = Product.UpdatedAt;
            this.Errors = Product.Errors;
        }
    }

    public class Customer_ProductFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter SupplierCode { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public StringFilter ScanCode { get; set; }
        public StringFilter ERPCode { get; set; }
        public IdFilter ProductTypeId { get; set; }
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
        public ProductOrder OrderBy { get; set; }
    }
}
