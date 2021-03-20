using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.order_report
{
    public class OrderReport_PaymentStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        

        public OrderReport_PaymentStatusDTO() {}
        public OrderReport_PaymentStatusDTO(PaymentStatus PaymentStatus)
        {
            
            this.Id = PaymentStatus.Id;
            
            this.Name = PaymentStatus.Name;
            
            this.Errors = PaymentStatus.Errors;
        }
    }

    public class OrderReport_PaymentStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public PaymentStatusOrder OrderBy { get; set; }
    }
}