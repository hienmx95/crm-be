using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_PaymentStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Company_PaymentStatusDTO() {}
        public Company_PaymentStatusDTO(PaymentStatus PaymentStatus)
        {
            
            this.Id = PaymentStatus.Id;
            
            this.Code = PaymentStatus.Code;
            
            this.Name = PaymentStatus.Name;
            
            this.Errors = PaymentStatus.Errors;
        }
    }

    public class Company_PaymentStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public PaymentStatusOrder OrderBy { get; set; }
    }
}