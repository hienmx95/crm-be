using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_TicketIssueLevelDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public long OrderNumber { get; set; }
        
        public long TicketGroupId { get; set; }
        
        public long StatusId { get; set; }
        
        public long SLA { get; set; }
        
        public bool Used { get; set; }
        

        public Customer_TicketIssueLevelDTO() {}
        public Customer_TicketIssueLevelDTO(TicketIssueLevel TicketIssueLevel)
        {
            
            this.Id = TicketIssueLevel.Id;
            
            this.Name = TicketIssueLevel.Name;
            
            this.OrderNumber = TicketIssueLevel.OrderNumber;
            
            this.TicketGroupId = TicketIssueLevel.TicketGroupId;
            
            this.StatusId = TicketIssueLevel.StatusId;
            
            this.SLA = TicketIssueLevel.SLA;
            
            this.Used = TicketIssueLevel.Used;
            
            this.Errors = TicketIssueLevel.Errors;
        }
    }

    public class Customer_TicketIssueLevelFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter OrderNumber { get; set; }
        
        public IdFilter TicketGroupId { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public LongFilter SLA { get; set; }
        
        public TicketIssueLevelOrder OrderBy { get; set; }
    }
}