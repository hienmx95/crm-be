using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.order_quote
{
    public class OrderQuote_ProductTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public OrderQuote_ProductTypeDTO() {}
        public OrderQuote_ProductTypeDTO(ProductType ProductType)
        {
            
            this.Id = ProductType.Id;
            
            this.Code = ProductType.Code;
            
            this.Name = ProductType.Name;
            
            this.Description = ProductType.Description;
            
            this.StatusId = ProductType.StatusId;
            
            this.Used = ProductType.Used;
            
            this.Errors = ProductType.Errors;
        }
    }

    public class OrderQuote_ProductTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Description { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public ProductTypeOrder OrderBy { get; set; }
    }
}