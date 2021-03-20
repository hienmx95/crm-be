using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_ProductProductGroupingMappingDTO : DataDTO
    {
        public long ProductId { get; set; }
        public long ProductGroupingId { get; set; }
        public Customer_ProductGroupingDTO ProductGrouping { get; set; }

        public Customer_ProductProductGroupingMappingDTO() { }
        public Customer_ProductProductGroupingMappingDTO(ProductProductGroupingMapping ProductProductGroupingMapping)
        {
            this.ProductId = ProductProductGroupingMapping.ProductId;
            this.ProductGroupingId = ProductProductGroupingMapping.ProductGroupingId;
            this.ProductGrouping = ProductProductGroupingMapping.ProductGrouping == null ? null : new Customer_ProductGroupingDTO(ProductProductGroupingMapping.ProductGrouping);
        }
    }

    public class Customer_ProductProductGroupingMappingFilterDTO : FilterDTO
    {

        public IdFilter ProductId { get; set; }

        public IdFilter ProductGroupingId { get; set; }

        public ProductProductGroupingMappingOrder OrderBy { get; set; }
    }
}