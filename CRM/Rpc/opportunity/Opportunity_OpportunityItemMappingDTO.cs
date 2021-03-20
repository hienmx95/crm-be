using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityItemMappingDTO : DataDTO
    {
        public long OpportunityId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public long RequestQuantity { get; set; }
        public decimal PrimaryPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Discount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? VATOther { get; set; }
        public decimal Amount { get; set; }
        public long? Factor { get; set; }
        public Opportunity_ItemDTO Item { get; set; }   
        public Opportunity_UnitOfMeasureDTO UnitOfMeasure { get; set; }   

        public Opportunity_OpportunityItemMappingDTO() {}
        public Opportunity_OpportunityItemMappingDTO(OpportunityItemMapping OpportunityItemMapping)
        {
            this.OpportunityId = OpportunityItemMapping.OpportunityId;
            this.ItemId = OpportunityItemMapping.ItemId;
            this.UnitOfMeasureId = OpportunityItemMapping.UnitOfMeasureId;
            this.Quantity = OpportunityItemMapping.Quantity;
            this.RequestQuantity = OpportunityItemMapping.RequestQuantity;
            this.PrimaryPrice = OpportunityItemMapping.PrimaryPrice;
            this.SalePrice = OpportunityItemMapping.SalePrice;
            this.DiscountPercentage = OpportunityItemMapping.DiscountPercentage;
            this.Discount = OpportunityItemMapping.Discount;
            this.VAT = OpportunityItemMapping.VAT;
            this.VATOther = OpportunityItemMapping.VATOther;
            this.Amount = OpportunityItemMapping.Amount;
            this.Factor = OpportunityItemMapping.Factor;
            this.Item = OpportunityItemMapping.Item == null ? null : new Opportunity_ItemDTO(OpportunityItemMapping.Item);
            this.UnitOfMeasure = OpportunityItemMapping.UnitOfMeasure == null ? null : new Opportunity_UnitOfMeasureDTO(OpportunityItemMapping.UnitOfMeasure);
            this.Errors = OpportunityItemMapping.Errors;
        }
    }

    public class Opportunity_OpportunityItemMappingFilterDTO : FilterDTO
    {
        
        public IdFilter OpportunityId { get; set; }
        
        public IdFilter ItemId { get; set; }
        
        public IdFilter UnitOfMeasureId { get; set; }
        
        public LongFilter Quantity { get; set; }
        
        public LongFilter RequestQuantity { get; set; }
        
        public DecimalFilter PrimaryPrice { get; set; }
        
        public DecimalFilter SalePrice { get; set; }
        
        public DecimalFilter DiscountPercentage { get; set; }
        
        public DecimalFilter Discount { get; set; }
        
        public DecimalFilter VAT { get; set; }
        
        public DecimalFilter VATOther { get; set; }
        
        public DecimalFilter Amount { get; set; }
        
        public LongFilter Factor { get; set; }
        
        public OpportunityItemMappingOrder OrderBy { get; set; }
    }
}