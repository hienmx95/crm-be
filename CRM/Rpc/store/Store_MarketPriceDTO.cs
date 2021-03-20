using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_MarketPriceDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        

        public Store_MarketPriceDTO() {}
        public Store_MarketPriceDTO(MarketPrice MarketPrice)
        {
            
            this.Id = MarketPrice.Id;
            
            
            this.Name = MarketPrice.Name;
            
            this.Errors = MarketPrice.Errors;
        }
    }

    public class Store_MarketPriceFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        
        public StringFilter Name { get; set; }
        
        public MarketPriceOrder OrderBy { get; set; }
    }
}