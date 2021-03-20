using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.order_quote
{
    public class OrderQuote_EditedPriceStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public OrderQuote_EditedPriceStatusDTO() {}
        public OrderQuote_EditedPriceStatusDTO(EditedPriceStatus EditedPriceStatus)
        {
            
            this.Id = EditedPriceStatus.Id;
            
            this.Code = EditedPriceStatus.Code;
            
            this.Name = EditedPriceStatus.Name;
            
            this.Errors = EditedPriceStatus.Errors;
        }
    }

    public class OrderQuote_EditedPriceStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public EditedPriceStatusOrder OrderBy { get; set; }
    }
}