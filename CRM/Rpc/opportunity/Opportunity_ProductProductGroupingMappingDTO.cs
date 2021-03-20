using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_ProductProductGroupingMappingDTO : DataDTO
    {
        public long ProductId { get; set; }
        public long ProductGroupingId { get; set; }
        public Opportunity_ProductGroupingDTO ProductGrouping { get; set; }

        public Opportunity_ProductProductGroupingMappingDTO() { }
        public Opportunity_ProductProductGroupingMappingDTO(ProductProductGroupingMapping ProductProductGroupingMapping)
        {
            this.ProductId = ProductProductGroupingMapping.ProductId;
            this.ProductGroupingId = ProductProductGroupingMapping.ProductGroupingId;
            this.ProductGrouping = ProductProductGroupingMapping.ProductGrouping == null ? null : new Opportunity_ProductGroupingDTO(ProductProductGroupingMapping.ProductGrouping);
        }
    }

    public class Opportunity_ProductProductGroupingMappingFilterDTO : FilterDTO
    {

        public IdFilter ProductId { get; set; }

        public IdFilter ProductGroupingId { get; set; }

        public ProductProductGroupingMappingOrder OrderBy { get; set; }
    }
}