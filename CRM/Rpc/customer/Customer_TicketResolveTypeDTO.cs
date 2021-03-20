using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_TicketResolveTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Customer_TicketResolveTypeDTO() {}
        public Customer_TicketResolveTypeDTO(TicketResolveType TicketResolveType)
        {
            
            this.Id = TicketResolveType.Id;
            
            this.Code = TicketResolveType.Code;
            
            this.Name = TicketResolveType.Name;
            
            this.Errors = TicketResolveType.Errors;
        }
    }

    public class Customer_TicketResolveTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public TicketResolveTypeOrder OrderBy { get; set; }
    }
}