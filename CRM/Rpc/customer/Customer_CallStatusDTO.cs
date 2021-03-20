using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CallStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_CallStatusDTO() {}
        public Customer_CallStatusDTO(CallStatus CallStatus)
        {
            
            this.Id = CallStatus.Id;
            
            this.Code = CallStatus.Code;
            
            this.Name = CallStatus.Name;
            
            this.Errors = CallStatus.Errors;
        }
    }

    public class Customer_CallStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public CallStatusOrder OrderBy { get; set; }
    }
}