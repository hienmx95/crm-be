using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_ProductProductGroupingMappingDTO : DataDTO
    {
        public long ProductId { get; set; }
        public long ProductGroupingId { get; set; }
        public Contract_ProductGroupingDTO ProductGrouping { get; set; }

        public Contract_ProductProductGroupingMappingDTO() { }
        public Contract_ProductProductGroupingMappingDTO(ProductProductGroupingMapping ProductProductGroupingMapping)
        {
            this.ProductId = ProductProductGroupingMapping.ProductId;
            this.ProductGroupingId = ProductProductGroupingMapping.ProductGroupingId;
            this.ProductGrouping = ProductProductGroupingMapping.ProductGrouping == null ? null : new Contract_ProductGroupingDTO(ProductProductGroupingMapping.ProductGrouping);
        }
    }

    public class Contract_ProductProductGroupingMappingFilterDTO : FilterDTO
    {

        public IdFilter ProductId { get; set; }

        public IdFilter ProductGroupingId { get; set; }

        public ProductProductGroupingMappingOrder OrderBy { get; set; }
    }
}