using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_TicketPriorityDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public long? OrderNumber { get; set; }
        
        public string ColorCode { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public TicketReport_TicketPriorityDTO() {}
        public TicketReport_TicketPriorityDTO(TicketPriority TicketPriority)
        {
            
            this.Id = TicketPriority.Id;
            
            this.Name = TicketPriority.Name;
            
            this.OrderNumber = TicketPriority.OrderNumber;
            
            this.ColorCode = TicketPriority.ColorCode;
            
            this.StatusId = TicketPriority.StatusId;
            
            this.Used = TicketPriority.Used;
            
            this.Errors = TicketPriority.Errors;
        }
    }

    public class TicketReport_TicketPriorityFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter OrderNumber { get; set; }
        
        public StringFilter ColorCode { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public TicketPriorityOrder OrderBy { get; set; }
    }
}