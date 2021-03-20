using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.order_quote
{
    public class OrderQuote_BrandDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long StatusId { get; set; }
        
        public string Description { get; set; }
        
        public bool Used { get; set; }
        

        public OrderQuote_BrandDTO() {}
        public OrderQuote_BrandDTO(Brand Brand)
        {
            
            this.Id = Brand.Id;
            
            this.Code = Brand.Code;
            
            this.Name = Brand.Name;
            
            this.StatusId = Brand.StatusId;
            
            this.Description = Brand.Description;
            
            this.Used = Brand.Used;
            
            this.Errors = Brand.Errors;
        }
    }

    public class OrderQuote_BrandFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public StringFilter Description { get; set; }
        
        public BrandOrder OrderBy { get; set; }
    }
}