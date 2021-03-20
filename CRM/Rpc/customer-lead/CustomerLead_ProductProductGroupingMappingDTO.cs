using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_ProductProductGroupingMappingDTO : DataDTO
    {
        public long ProductId { get; set; }
        public long ProductGroupingId { get; set; }
        public CustomerLead_ProductGroupingDTO ProductGrouping { get; set; }

        public CustomerLead_ProductProductGroupingMappingDTO() { }
        public CustomerLead_ProductProductGroupingMappingDTO(ProductProductGroupingMapping ProductProductGroupingMapping)
        {
            this.ProductId = ProductProductGroupingMapping.ProductId;
            this.ProductGroupingId = ProductProductGroupingMapping.ProductGroupingId;
            this.ProductGrouping = ProductProductGroupingMapping.ProductGrouping == null ? null : new CustomerLead_ProductGroupingDTO(ProductProductGroupingMapping.ProductGrouping);
        }
    }

    public class CustomerLead_ProductProductGroupingMappingFilterDTO : FilterDTO
    {

        public IdFilter ProductId { get; set; }

        public IdFilter ProductGroupingId { get; set; }

        public ProductProductGroupingMappingOrder OrderBy { get; set; }
    }
}