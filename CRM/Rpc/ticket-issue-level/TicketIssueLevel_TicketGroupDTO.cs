using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_TicketGroupDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public long OrderNumber { get; set; }
        
        public long StatusId { get; set; }
        
        public long TicketTypeId { get; set; }
        
        public bool Used { get; set; }
        
        public TicketIssueLevel_TicketTypeDTO TicketType { get; set; }

        public TicketIssueLevel_TicketGroupDTO() {}
        public TicketIssueLevel_TicketGroupDTO(TicketGroup TicketGroup)
        {
            
            this.Id = TicketGroup.Id;
            
            this.Name = TicketGroup.Name;
            
            this.OrderNumber = TicketGroup.OrderNumber;
            
            this.StatusId = TicketGroup.StatusId;
            
            this.TicketTypeId = TicketGroup.TicketTypeId;

            this.TicketType = TicketGroup.TicketType == null ? null : new TicketIssueLevel_TicketTypeDTO(TicketGroup.TicketType);

            this.Used = TicketGroup.Used;
            
            this.Errors = TicketGroup.Errors;
        }
    }

    public class TicketIssueLevel_TicketGroupFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter OrderNumber { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public IdFilter TicketTypeId { get; set; }
        
        public TicketGroupOrder OrderBy { get; set; }
    }
}