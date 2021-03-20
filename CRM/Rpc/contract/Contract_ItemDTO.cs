using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_ItemDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public long ProductId { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string ScanCode { get; set; }
        
        public decimal? SalePrice { get; set; }
        
        public decimal? RetailPrice { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        public long SaleStock { get; set; }
        public bool HasInventory { get; set; }
        public bool CanDelete { get; set; }
        public Contract_ProductDTO Product { get; set; }

        public Contract_ItemDTO() {}
        public Contract_ItemDTO(Item Item)
        {
            
            this.Id = Item.Id;
            
            this.ProductId = Item.ProductId;
            
            this.Code = Item.Code;
            
            this.Name = Item.Name;
            
            this.ScanCode = Item.ScanCode;
            
            this.SalePrice = Item.SalePrice;
            
            this.RetailPrice = Item.RetailPrice;
            
            this.StatusId = Item.StatusId;
            
            this.Used = Item.Used;
            
            this.Errors = Item.Errors;
            this.SaleStock = Item.SaleStock;
            this.CanDelete = Item.CanDelete;
            this.HasInventory = Item.HasInventory;
            this.Product = Item.Product == null ? null : new Contract_ProductDTO(Item.Product);
        }
    }

    public class Contract_ItemFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter ProductId { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter ScanCode { get; set; }
        
        public DecimalFilter SalePrice { get; set; }
        
        public DecimalFilter RetailPrice { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public ItemOrder OrderBy { get; set; }
    }
}