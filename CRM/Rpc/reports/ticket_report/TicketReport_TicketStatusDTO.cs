using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_TicketStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public long OrderNumber { get; set; }
        
        public string ColorCode { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public TicketReport_TicketStatusDTO() {}
        public TicketReport_TicketStatusDTO(TicketStatus TicketStatus)
        {
            
            this.Id = TicketStatus.Id;
            
            this.Name = TicketStatus.Name;
            
            this.OrderNumber = TicketStatus.OrderNumber;
            
            this.ColorCode = TicketStatus.ColorCode;
            
            this.StatusId = TicketStatus.StatusId;
            
            this.Used = TicketStatus.Used;
            
            this.Errors = TicketStatus.Errors;
        }
    }

    public class TicketReport_TicketStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter OrderNumber { get; set; }
        
        public StringFilter ColorCode { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public TicketStatusOrder OrderBy { get; set; }
    }
}