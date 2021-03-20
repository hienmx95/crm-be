using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class UnitOfMeasureDAO
    {
        public UnitOfMeasureDAO()
        {
            ContractItemDetailPrimaryUnitOfMeasures = new HashSet<ContractItemDetailDAO>();
            ContractItemDetailUnitOfMeasures = new HashSet<ContractItemDetailDAO>();
            CustomerLeadItemMappings = new HashSet<CustomerLeadItemMappingDAO>();
            CustomerSalesOrderContentPrimaryUnitOfMeasures = new HashSet<CustomerSalesOrderContentDAO>();
            CustomerSalesOrderContentUnitOfMeasures = new HashSet<CustomerSalesOrderContentDAO>();
            CustomerSalesOrderPromotionPrimaryUnitOfMeasures = new HashSet<CustomerSalesOrderPromotionDAO>();
            CustomerSalesOrderPromotionUnitOfMeasures = new HashSet<CustomerSalesOrderPromotionDAO>();
            DirectSalesOrderContentPrimaryUnitOfMeasures = new HashSet<DirectSalesOrderContentDAO>();
            DirectSalesOrderContentUnitOfMeasures = new HashSet<DirectSalesOrderContentDAO>();
            DirectSalesOrderPromotionPrimaryUnitOfMeasures = new HashSet<DirectSalesOrderPromotionDAO>();
            DirectSalesOrderPromotionUnitOfMeasures = new HashSet<DirectSalesOrderPromotionDAO>();
            OpportunityItemMappings = new HashSet<OpportunityItemMappingDAO>();
            OrderQuoteContentPrimaryUnitOfMeasures = new HashSet<OrderQuoteContentDAO>();
            OrderQuoteContentUnitOfMeasures = new HashSet<OrderQuoteContentDAO>();
            Products = new HashSet<ProductDAO>();
            UnitOfMeasureGroupingContents = new HashSet<UnitOfMeasureGroupingContentDAO>();
            UnitOfMeasureGroupings = new HashSet<UnitOfMeasureGroupingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<ContractItemDetailDAO> ContractItemDetailPrimaryUnitOfMeasures { get; set; }
        public virtual ICollection<ContractItemDetailDAO> ContractItemDetailUnitOfMeasures { get; set; }
        public virtual ICollection<CustomerLeadItemMappingDAO> CustomerLeadItemMappings { get; set; }
        public virtual ICollection<CustomerSalesOrderContentDAO> CustomerSalesOrderContentPrimaryUnitOfMeasures { get; set; }
        public virtual ICollection<CustomerSalesOrderContentDAO> CustomerSalesOrderContentUnitOfMeasures { get; set; }
        public virtual ICollection<CustomerSalesOrderPromotionDAO> CustomerSalesOrderPromotionPrimaryUnitOfMeasures { get; set; }
        public virtual ICollection<CustomerSalesOrderPromotionDAO> CustomerSalesOrderPromotionUnitOfMeasures { get; set; }
        public virtual ICollection<DirectSalesOrderContentDAO> DirectSalesOrderContentPrimaryUnitOfMeasures { get; set; }
        public virtual ICollection<DirectSalesOrderContentDAO> DirectSalesOrderContentUnitOfMeasures { get; set; }
        public virtual ICollection<DirectSalesOrderPromotionDAO> DirectSalesOrderPromotionPrimaryUnitOfMeasures { get; set; }
        public virtual ICollection<DirectSalesOrderPromotionDAO> DirectSalesOrderPromotionUnitOfMeasures { get; set; }
        public virtual ICollection<OpportunityItemMappingDAO> OpportunityItemMappings { get; set; }
        public virtual ICollection<OrderQuoteContentDAO> OrderQuoteContentPrimaryUnitOfMeasures { get; set; }
        public virtual ICollection<OrderQuoteContentDAO> OrderQuoteContentUnitOfMeasures { get; set; }
        public virtual ICollection<ProductDAO> Products { get; set; }
        public virtual ICollection<UnitOfMeasureGroupingContentDAO> UnitOfMeasureGroupingContents { get; set; }
        public virtual ICollection<UnitOfMeasureGroupingDAO> UnitOfMeasureGroupings { get; set; }
    }
}
