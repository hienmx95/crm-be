using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CurrencyDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? DisplayOrder { get; set; }
        
        public long StatusId { get; set; }
        

        public CustomerLead_CurrencyDTO() {}
        public CustomerLead_CurrencyDTO(Currency Currency)
        {
            
            this.Id = Currency.Id;
            
            this.Code = Currency.Code;
            
            this.Name = Currency.Name; 
            
            this.Errors = Currency.Errors;
        }
    }

    public class CustomerLead_CurrencyFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public CurrencyOrder OrderBy { get; set; }
    }
}