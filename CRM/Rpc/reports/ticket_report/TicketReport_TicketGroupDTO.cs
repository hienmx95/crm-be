using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_TicketGroupDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public long OrderNumber { get; set; }
        
        public long StatusId { get; set; }
        
        public long TicketTypeId { get; set; }
        
        public bool Used { get; set; }

        public TicketReport_TicketTypeDTO TicketType { get; set; }

        public TicketReport_TicketGroupDTO() {}
        public TicketReport_TicketGroupDTO(TicketGroup TicketGroup)
        {
            
            this.Id = TicketGroup.Id;
            
            this.Name = TicketGroup.Name;
            
            this.OrderNumber = TicketGroup.OrderNumber;
            
            this.StatusId = TicketGroup.StatusId;
            
            this.TicketTypeId = TicketGroup.TicketTypeId;

            this.TicketType = TicketGroup.TicketType == null ? null : new TicketReport_TicketTypeDTO(TicketGroup.TicketType);

            this.Used = TicketGroup.Used;
            
            this.Errors = TicketGroup.Errors;
        }
    }

    public class TicketReport_TicketGroupFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter OrderNumber { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public IdFilter TicketTypeId { get; set; }
        
        public TicketGroupOrder OrderBy { get; set; }
    }
}