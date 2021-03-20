using CRM.Common;
using CRM.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Rpc.reports.opportunity_report
{
    public class OpportunityReport_ItemDTO : DataDTO
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ScanCode { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public long SaleStock { get; set; }
        public bool CanDelete { get; set; }
        public bool HasInventory { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        //public OpportunityReport_ProductDTO Product { get; set; }
        public OpportunityReport_ItemDTO() { }
        public OpportunityReport_ItemDTO(Item Item)
        {
            this.Id = Item.Id;
            this.ProductId = Item.ProductId;
            this.Code = Item.Code;
            this.Name = Item.Name;
            this.ScanCode = Item.ScanCode;
            this.SalePrice = Item.SalePrice;
            this.RetailPrice = Item.RetailPrice;
            this.SaleStock = Item.SaleStock;
            this.CanDelete = Item.CanDelete;
            this.HasInventory = Item.HasInventory;
            this.StatusId = Item.StatusId;
            this.Used = Item.Used;
            //this.Product = Item.Product == null ? null : new OpportunityReport_ProductDTO(Item.Product);
            this.Errors = Item.Errors;
        }
    }

    public class OpportunityReport_ItemFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public IdFilter ProductId { get; set; }
        public IdFilter OpportunityReportId { get; set; }

        public IdFilter SupplierId { get; set; }

        public IdFilter ProductTypeId { get; set; }

        public IdFilter ProductGroupingId { get; set; }

        public StringFilter Code { get; set; }

        public StringFilter Name { get; set; }

        public StringFilter ScanCode { get; set; }

        public DecimalFilter SalePrice { get; set; }

        public DecimalFilter RetailPrice { get; set; }

        public ItemOrder OrderBy { get; set; }
    }
}