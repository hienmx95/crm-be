using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_PaymentStatusDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
        public Contract_PaymentStatusDTO() {}
        public Contract_PaymentStatusDTO(PaymentStatus PaymentStatus)
        {
            this.Id = PaymentStatus.Id;
            this.Name = PaymentStatus.Name;
            this.Code = PaymentStatus.Code;
            this.Errors = PaymentStatus.Errors;
        }
    }

    public class Contract_PaymentStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public PaymentStatusOrder OrderBy { get; set; }
    }
}