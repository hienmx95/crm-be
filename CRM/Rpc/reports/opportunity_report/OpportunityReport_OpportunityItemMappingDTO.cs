using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.opportunity_report
{
    public class OpportunityReport_OpportunityItemMappingDTO : DataDTO
    {
        public long OpportunityId { get; set; }
        public long ItemId { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public long? TaxTypeId { get; set; }
        public long? Quantity { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? RequestQuantity { get; set; }
        public decimal? PrimaryPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? VATOther { get; set; }
        public decimal? Amount { get; set; }
        public long? Factor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public OpportunityReport_ItemDTO Item { get; set; }
        public OpportunityReport_UnitOfMeasureDTO UnitOfMeasure { get; set; }
        //public OpportunityReport_TaxTypeDTO TaxType { get; set; }
        //public OpportunityReport_OpportunityResultDTO OpportunityResult { get; set; }
        public OpportunityReport_OpportunityItemMappingDTO() { }
        public OpportunityReport_OpportunityItemMappingDTO(OpportunityItemMapping OpportunityItemMapping)
        {
            this.OpportunityId = OpportunityItemMapping.OpportunityId;
            this.ItemId = OpportunityItemMapping.ItemId;
            this.UnitOfMeasureId = OpportunityItemMapping.UnitOfMeasureId;
            this.Quantity = OpportunityItemMapping.Quantity;
            this.DiscountPercentage = OpportunityItemMapping.DiscountPercentage;
            this.RequestQuantity = OpportunityItemMapping.RequestQuantity;
            this.PrimaryPrice = OpportunityItemMapping.PrimaryPrice;
            this.SalePrice = OpportunityItemMapping.SalePrice;
            this.Discount = OpportunityItemMapping.Discount;
            this.VAT = OpportunityItemMapping.VAT;
            this.VATOther = OpportunityItemMapping.VATOther;
            this.Amount = OpportunityItemMapping.Amount;
            this.Factor = OpportunityItemMapping.Factor;
            //this.Item = OpportunityItemMapping.Item == null ? null : new OpportunityReport_ItemDTO(OpportunityItemMapping.Item);
            //this.UnitOfMeasure = OpportunityItemMapping.UnitOfMeasure == null ? null : new OpportunityReport_UnitOfMeasureDTO(OpportunityItemMapping.UnitOfMeasure);
            //this.TaxType = OpportunityItemMapping.TaxType == null ? null : new OpportunityReport_TaxTypeDTO(OpportunityItemMapping.TaxType);
            //this.OpportunityResult = OpportunityItemMapping.OpportunityResult == null ? null : new OpportunityReport_OpportunityResultDTO(OpportunityItemMapping.OpportunityResult);
        }
    }

    public class OpportunityReport_OpportunityItemMappingFilterDTO : FilterDTO
    {
        public long OpportunityId { get; set; }
        public long ItemId { get; set; }
        public OpportunityItemMappingOrder OrderBy { get; set; }
    }
}
