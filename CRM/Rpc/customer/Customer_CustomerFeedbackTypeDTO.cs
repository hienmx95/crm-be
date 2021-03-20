using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerFeedbackTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_CustomerFeedbackTypeDTO() {}
        public Customer_CustomerFeedbackTypeDTO(CustomerFeedbackType CustomerFeedbackType)
        {
            
            this.Id = CustomerFeedbackType.Id;
            
            this.Code = CustomerFeedbackType.Code;
            
            this.Name = CustomerFeedbackType.Name;
            
            this.Errors = CustomerFeedbackType.Errors;
        }
    }

    public class Customer_CustomerFeedbackTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CustomerFeedbackTypeOrder OrderBy { get; set; }
    }
}