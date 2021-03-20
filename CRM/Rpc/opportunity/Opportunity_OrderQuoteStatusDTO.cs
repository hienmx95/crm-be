using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OrderQuoteStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Opportunity_OrderQuoteStatusDTO() {}
        public Opportunity_OrderQuoteStatusDTO(OrderQuoteStatus OrderQuoteStatus)
        {
            
            this.Id = OrderQuoteStatus.Id;
            
            this.Code = OrderQuoteStatus.Code;
            
            this.Name = OrderQuoteStatus.Name;
            
            this.Errors = OrderQuoteStatus.Errors;
        }
    }

    public class Opportunity_OrderQuoteStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public OrderQuoteStatusOrder OrderBy { get; set; }
    }
}