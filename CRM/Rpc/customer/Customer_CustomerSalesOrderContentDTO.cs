using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerSalesOrderContentDTO : DataDTO
    {
        public long Id { get; set; }
        public long CustomerSalesOrderId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public long RequestedQuantity { get; set; }
        public long PrimaryUnitOfMeasureId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PrimaryPrice { get; set; }
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
        public long EditedPriceStatusId { get; set; }
        public long TaxTypeId { get; set; }
        public Customer_EditedPriceStatusDTO EditedPriceStatus { get; set; }   
        public Customer_ItemDTO Item { get; set; }   
        public Customer_UnitOfMeasureDTO PrimaryUnitOfMeasure { get; set; }   
        public Customer_UnitOfMeasureDTO UnitOfMeasure { get; set; }   
        public Customer_TaxTypeDTO TaxType { get; set; }   

        public Customer_CustomerSalesOrderContentDTO() {}
        public Customer_CustomerSalesOrderContentDTO(CustomerSalesOrderContent CustomerSalesOrderContent)
        {
            this.Id = CustomerSalesOrderContent.Id;
            this.CustomerSalesOrderId = CustomerSalesOrderContent.CustomerSalesOrderId;
            this.ItemId = CustomerSalesOrderContent.ItemId;
            this.UnitOfMeasureId = CustomerSalesOrderContent.UnitOfMeasureId;
            this.Quantity = CustomerSalesOrderContent.Quantity;
            this.RequestedQuantity = CustomerSalesOrderContent.RequestedQuantity;
            this.PrimaryUnitOfMeasureId = CustomerSalesOrderContent.PrimaryUnitOfMeasureId;
            this.SalePrice = CustomerSalesOrderContent.SalePrice;
            this.PrimaryPrice = CustomerSalesOrderContent.PrimaryPrice;
            this.DiscountPercentage = CustomerSalesOrderContent.DiscountPercentage;
            this.DiscountAmount = CustomerSalesOrderContent.DiscountAmount;
            this.GeneralDiscountPercentage = CustomerSalesOrderContent.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = CustomerSalesOrderContent.GeneralDiscountAmount;
            this.TaxPercentage = CustomerSalesOrderContent.TaxPercentage;
            this.TaxAmount = CustomerSalesOrderContent.TaxAmount;
            this.TaxPercentageOther = CustomerSalesOrderContent.TaxPercentageOther;
            this.TaxAmountOther = CustomerSalesOrderContent.TaxAmountOther;
            this.Amount = CustomerSalesOrderContent.Amount;
            this.Factor = CustomerSalesOrderContent.Factor;
            this.EditedPriceStatusId = CustomerSalesOrderContent.EditedPriceStatusId;
            this.TaxTypeId = CustomerSalesOrderContent.TaxTypeId;
            this.EditedPriceStatus = CustomerSalesOrderContent.EditedPriceStatus == null ? null : new Customer_EditedPriceStatusDTO(CustomerSalesOrderContent.EditedPriceStatus);
            this.Item = CustomerSalesOrderContent.Item == null ? null : new Customer_ItemDTO(CustomerSalesOrderContent.Item);
            this.PrimaryUnitOfMeasure = CustomerSalesOrderContent.PrimaryUnitOfMeasure == null ? null : new Customer_UnitOfMeasureDTO(CustomerSalesOrderContent.PrimaryUnitOfMeasure);
            this.UnitOfMeasure = CustomerSalesOrderContent.UnitOfMeasure == null ? null : new Customer_UnitOfMeasureDTO(CustomerSalesOrderContent.UnitOfMeasure);
            this.TaxType = CustomerSalesOrderContent.TaxType == null ? null : new Customer_TaxTypeDTO(CustomerSalesOrderContent.TaxType);
            this.Errors = CustomerSalesOrderContent.Errors;
        }
    }

    public class Customer_CustomerSalesOrderContentFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter CustomerSalesOrderId { get; set; }
        
        public IdFilter ItemId { get; set; }
        
        public IdFilter UnitOfMeasureId { get; set; }
        
        public LongFilter Quantity { get; set; }
        
        public LongFilter RequestedQuantity { get; set; }
        
        public IdFilter PrimaryUnitOfMeasureId { get; set; }
        
        public DecimalFilter SalePrice { get; set; }
        
        public DecimalFilter PrimaryPrice { get; set; }
        
        public DecimalFilter DiscountPercentage { get; set; }
        
        public DecimalFilter DiscountAmount { get; set; }
        
        public DecimalFilter GeneralDiscountPercentage { get; set; }
        
        public DecimalFilter GeneralDiscountAmount { get; set; }
        
        public DecimalFilter TaxPercentage { get; set; }
        
        public DecimalFilter TaxAmount { get; set; }
        
        public DecimalFilter TaxPercentageOther { get; set; }
        
        public DecimalFilter TaxAmountOther { get; set; }
        
        public DecimalFilter Amount { get; set; }
        
        public LongFilter Factor { get; set; }
        
        public IdFilter EditedPriceStatusId { get; set; }
        public IdFilter TaxTypeId { get; set; }
        
        public CustomerSalesOrderContentOrder OrderBy { get; set; }
    }
}