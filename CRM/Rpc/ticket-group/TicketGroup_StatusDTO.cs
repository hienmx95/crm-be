using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_group
{
    public class TicketGroup_StatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public TicketGroup_StatusDTO() {}
        public TicketGroup_StatusDTO(Status Status)
        {
            
            this.Id = Status.Id;
            
            this.Code = Status.Code;
            
            this.Name = Status.Name;
            
            this.Errors = Status.Errors;
        }
    }

    public class TicketGroup_StatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StatusOrder OrderBy { get; set; }
    }
}