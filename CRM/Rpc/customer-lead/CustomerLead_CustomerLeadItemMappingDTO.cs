using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadItemMappingDTO : DataDTO
    {
        public long CustomerLeadId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal RequestQuantity { get; set; }
        public decimal PrimaryPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? VATOther { get; set; }
        public decimal TotalPrice { get; set; }
        public long? Factor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public CustomerLead_ItemDTO Item { get; set; }
        public CustomerLead_UnitOfMeasureDTO UnitOfMeasure { get; set; }
        public CustomerLead_CustomerLeadItemMappingDTO() { }
        public CustomerLead_CustomerLeadItemMappingDTO(CustomerLeadItemMapping CustomerLeadItemMapping)
        {
            this.CustomerLeadId = CustomerLeadItemMapping.CustomerLeadId;
            this.ItemId = CustomerLeadItemMapping.ItemId;
            this.UnitOfMeasureId = CustomerLeadItemMapping.UnitOfMeasureId;
            this.Quantity = CustomerLeadItemMapping.Quantity;
            this.DiscountPercentage = CustomerLeadItemMapping.DiscountPercentage;
            this.RequestQuantity = CustomerLeadItemMapping.RequestQuantity;
            this.PrimaryPrice = CustomerLeadItemMapping.PrimaryPrice;
            this.SalePrice = CustomerLeadItemMapping.SalePrice;
            this.Discount = CustomerLeadItemMapping.Discount;
            this.VAT = CustomerLeadItemMapping.VAT;
            this.VATOther = CustomerLeadItemMapping.VATOther;
            this.TotalPrice = CustomerLeadItemMapping.TotalPrice;
            this.Factor = CustomerLeadItemMapping.Factor;
            this.Item = CustomerLeadItemMapping.Item == null ? null : new CustomerLead_ItemDTO(CustomerLeadItemMapping.Item);
            this.UnitOfMeasure = CustomerLeadItemMapping.UnitOfMeasure == null ? null : new CustomerLead_UnitOfMeasureDTO(CustomerLeadItemMapping.UnitOfMeasure);
        }
    }

    public class CustomerLead_ItemMappingFilterDTO : FilterDTO
    {
        public long CustomerLeadId { get; set; }
        public long ItemId { get; set; }
        public CustomerLeadItemMappingOrder OrderBy { get; set; }
    }
}
