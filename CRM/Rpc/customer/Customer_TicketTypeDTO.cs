using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_TicketTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string ColorCode { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public Customer_TicketTypeDTO() {}
        public Customer_TicketTypeDTO(TicketType TicketType)
        {
            
            this.Id = TicketType.Id;
            
            this.Code = TicketType.Code;
            
            this.Name = TicketType.Name;
            
            this.ColorCode = TicketType.ColorCode;
            
            this.StatusId = TicketType.StatusId;
            
            this.Used = TicketType.Used;
            
            this.Errors = TicketType.Errors;
        }
    }

    public class Customer_TicketTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter ColorCode { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public TicketTypeOrder OrderBy { get; set; }
    }
}