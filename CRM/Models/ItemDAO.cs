using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ItemDAO
    {
        public ItemDAO()
        {
            ContractItemDetails = new HashSet<ContractItemDetailDAO>();
            CustomerLeadItemMappings = new HashSet<CustomerLeadItemMappingDAO>();
            CustomerSalesOrderContents = new HashSet<CustomerSalesOrderContentDAO>();
            CustomerSalesOrderPromotions = new HashSet<CustomerSalesOrderPromotionDAO>();
            DirectSalesOrderContents = new HashSet<DirectSalesOrderContentDAO>();
            DirectSalesOrderPromotions = new HashSet<DirectSalesOrderPromotionDAO>();
            Inventories = new HashSet<InventoryDAO>();
            ItemImageMappings = new HashSet<ItemImageMappingDAO>();
            KnowledgeArticles = new HashSet<KnowledgeArticleDAO>();
            KpiItemContents = new HashSet<KpiItemContentDAO>();
            OpportunityItemMappings = new HashSet<OpportunityItemMappingDAO>();
            OrderQuoteContents = new HashSet<OrderQuoteContentDAO>();
            RepairTickets = new HashSet<RepairTicketDAO>();
        }

        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ScanCode { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }

        public virtual ProductDAO Product { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<ContractItemDetailDAO> ContractItemDetails { get; set; }
        public virtual ICollection<CustomerLeadItemMappingDAO> CustomerLeadItemMappings { get; set; }
        public virtual ICollection<CustomerSalesOrderContentDAO> CustomerSalesOrderContents { get; set; }
        public virtual ICollection<CustomerSalesOrderPromotionDAO> CustomerSalesOrderPromotions { get; set; }
        public virtual ICollection<DirectSalesOrderContentDAO> DirectSalesOrderContents { get; set; }
        public virtual ICollection<DirectSalesOrderPromotionDAO> DirectSalesOrderPromotions { get; set; }
        public virtual ICollection<InventoryDAO> Inventories { get; set; }
        public virtual ICollection<ItemImageMappingDAO> ItemImageMappings { get; set; }
        public virtual ICollection<KnowledgeArticleDAO> KnowledgeArticles { get; set; }
        public virtual ICollection<KpiItemContentDAO> KpiItemContents { get; set; }
        public virtual ICollection<OpportunityItemMappingDAO> OpportunityItemMappings { get; set; }
        public virtual ICollection<OrderQuoteContentDAO> OrderQuoteContents { get; set; }
        public virtual ICollection<RepairTicketDAO> RepairTickets { get; set; }
    }
}
