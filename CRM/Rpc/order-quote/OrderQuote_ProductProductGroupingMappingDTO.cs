using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.order_quote
{
    public class OrderQuote_ProductProductGroupingMappingDTO : DataDTO
    {
        public long ProductId { get; set; }
        public long ProductGroupingId { get; set; }
        public OrderQuote_ProductGroupingDTO ProductGrouping { get; set; }

        public OrderQuote_ProductProductGroupingMappingDTO() { }
        public OrderQuote_ProductProductGroupingMappingDTO(ProductProductGroupingMapping ProductProductGroupingMapping)
        {
            this.ProductId = ProductProductGroupingMapping.ProductId;
            this.ProductGroupingId = ProductProductGroupingMapping.ProductGroupingId;
            this.ProductGrouping = ProductProductGroupingMapping.ProductGrouping == null ? null : new OrderQuote_ProductGroupingDTO(ProductProductGroupingMapping.ProductGrouping);
        }
    }

    public class OrderQuote_ProductProductGroupingMappingFilterDTO : FilterDTO
    {

        public IdFilter ProductId { get; set; }

        public IdFilter ProductGroupingId { get; set; }

        public ProductProductGroupingMappingOrder OrderBy { get; set; }
    }
}