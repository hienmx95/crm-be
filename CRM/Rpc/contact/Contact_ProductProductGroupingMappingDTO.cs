using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ProductProductGroupingMappingDTO : DataDTO
    {
        public long ProductId { get; set; }
        public long ProductGroupingId { get; set; }
        public Contact_ProductGroupingDTO ProductGrouping { get; set; }

        public Contact_ProductProductGroupingMappingDTO() { }
        public Contact_ProductProductGroupingMappingDTO(ProductProductGroupingMapping ProductProductGroupingMapping)
        {
            this.ProductId = ProductProductGroupingMapping.ProductId;
            this.ProductGroupingId = ProductProductGroupingMapping.ProductGroupingId;
            this.ProductGrouping = ProductProductGroupingMapping.ProductGrouping == null ? null : new Contact_ProductGroupingDTO(ProductProductGroupingMapping.ProductGrouping);
        }
    }

    public class Contact_ProductProductGroupingMappingFilterDTO : FilterDTO
    {

        public IdFilter ProductId { get; set; }

        public IdFilter ProductGroupingId { get; set; }

        public ProductProductGroupingMappingOrder OrderBy { get; set; }
    }
}