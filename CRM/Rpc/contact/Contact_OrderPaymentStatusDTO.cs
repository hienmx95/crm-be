using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_OrderPaymentStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Contact_OrderPaymentStatusDTO() {}
        public Contact_OrderPaymentStatusDTO(OrderPaymentStatus OrderPaymentStatus)
        {
            
            this.Id = OrderPaymentStatus.Id;
            
            this.Code = OrderPaymentStatus.Code;
            
            this.Name = OrderPaymentStatus.Name;
            
            this.Errors = OrderPaymentStatus.Errors;
        }
    }

    public class Contact_OrderPaymentStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public OrderPaymentStatusOrder OrderBy { get; set; }
    }
}