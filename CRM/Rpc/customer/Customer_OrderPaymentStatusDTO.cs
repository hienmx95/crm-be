using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_OrderPaymentStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_OrderPaymentStatusDTO() {}
        public Customer_OrderPaymentStatusDTO(OrderPaymentStatus OrderPaymentStatus)
        {
            
            this.Id = OrderPaymentStatus.Id;
            
            this.Code = OrderPaymentStatus.Code;
            
            this.Name = OrderPaymentStatus.Name;
            
            this.Errors = OrderPaymentStatus.Errors;
        }
    }

    public class Customer_OrderPaymentStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public OrderPaymentStatusOrder OrderBy { get; set; }
    }
}