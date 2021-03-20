using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_RepairStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        

        public Customer_RepairStatusDTO() {}
        public Customer_RepairStatusDTO(RepairStatus RepairStatus)
        {
            
            this.Id = RepairStatus.Id;
            
            this.Name = RepairStatus.Name;
            
            this.Code = RepairStatus.Code;
            
            this.Errors = RepairStatus.Errors;
        }
    }

    public class Customer_RepairStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public RepairStatusOrder OrderBy { get; set; }
    }
}