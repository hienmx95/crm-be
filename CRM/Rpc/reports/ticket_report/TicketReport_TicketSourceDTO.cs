using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_TicketSourceDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public long OrderNumber { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public TicketReport_TicketSourceDTO() {}
        public TicketReport_TicketSourceDTO(TicketSource TicketSource)
        {
            
            this.Id = TicketSource.Id;
            
            this.Name = TicketSource.Name;
            
            this.OrderNumber = TicketSource.OrderNumber;
            
            this.StatusId = TicketSource.StatusId;
            
            this.Used = TicketSource.Used;
            
            this.Errors = TicketSource.Errors;
        }
    }

    public class TicketReport_TicketSourceFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter OrderNumber { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public TicketSourceOrder OrderBy { get; set; }
    }
}