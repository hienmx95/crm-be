using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CurrencyDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_CurrencyDTO() {}
        public Customer_CurrencyDTO(Currency Currency)
        {
            
            this.Id = Currency.Id;
            
            this.Code = Currency.Code;
            
            this.Name = Currency.Name;
            
            this.Errors = Currency.Errors;
        }
    }

    public class Customer_CurrencyFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CurrencyOrder OrderBy { get; set; }
    }
}