using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_TicketResolveTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public TicketReport_TicketResolveTypeDTO() {}
        public TicketReport_TicketResolveTypeDTO(TicketResolveType TicketResolveType)
        {
            
            this.Id = TicketResolveType.Id;
            
            this.Code = TicketResolveType.Code;
            
            this.Name = TicketResolveType.Name;
            
            this.Errors = TicketResolveType.Errors;
        }
    }

    public class TicketReport_TicketResolveTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public TicketResolveTypeOrder OrderBy { get; set; }
    }
}