using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_ProductProductGroupingMappingDTO : DataDTO
    {
        public long ProductId { get; set; }
        public long ProductGroupingId { get; set; }
        public Company_ProductGroupingDTO ProductGrouping { get; set; }

        public Company_ProductProductGroupingMappingDTO() { }
        public Company_ProductProductGroupingMappingDTO(ProductProductGroupingMapping ProductProductGroupingMapping)
        {
            this.ProductId = ProductProductGroupingMapping.ProductId;
            this.ProductGroupingId = ProductProductGroupingMapping.ProductGroupingId;
            this.ProductGrouping = ProductProductGroupingMapping.ProductGrouping == null ? null : new Company_ProductGroupingDTO(ProductProductGroupingMapping.ProductGrouping);
        }
    }

    public class Company_ProductProductGroupingMappingFilterDTO : FilterDTO
    {

        public IdFilter ProductId { get; set; }

        public IdFilter ProductGroupingId { get; set; }

        public ProductProductGroupingMappingOrder OrderBy { get; set; }
    }
}