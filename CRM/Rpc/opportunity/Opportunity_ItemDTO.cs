using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_ItemDTO : DataDTO
    {

        public long Id { get; set; }

        public long ProductId { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductTypeCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ScanCode { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public long StatusId { get; set; }
        public long SaleStock { get; set; }


        public Opportunity_ProductDTO Product { get; set; }



        public Opportunity_ItemDTO() { }
        public Opportunity_ItemDTO(Item Item)
        {

            this.Id = Item.Id;

            this.ProductId = Item.ProductId;

            this.Code = Item.Code;

            this.Name = Item.Name;

            this.ScanCode = Item.ScanCode;

            this.SalePrice = Item.SalePrice;

            this.RetailPrice = Item.RetailPrice;

            this.StatusId = Item.StatusId;
            this.SaleStock = Item.SaleStock;
            this.Product = Item.Product == null ? null : new Opportunity_ProductDTO(Item.Product);

            this.Errors = Item.Errors;
        }
    }

    public class Opportunity_ItemFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }
        public IdFilter ProductId { get; set; }
        public IdFilter ProductGroupingId { get; set; }
        public IdFilter ProductTypeId { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter ScanCode { get; set; }
        public StringFilter OtherName { get; set; }
        public DecimalFilter SalePrice { get; set; }
        public DecimalFilter RetailPrice { get; set; }
        public IdFilter StatusId { get; set; }
        public string Search { get; set; }

        public ItemOrder OrderBy { get; set; }


        public IdFilter ItemId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter UnitOfMesureId { get; set; }
    }
}