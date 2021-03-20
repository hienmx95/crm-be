using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_priority
{
    public class TicketPriority_TicketPriorityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? OrderNumber { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public TicketPriority_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketPriority_TicketPriorityDTO() {}
        public TicketPriority_TicketPriorityDTO(TicketPriority TicketPriority)
        {
            this.Id = TicketPriority.Id;
            this.Name = TicketPriority.Name;
            this.OrderNumber = TicketPriority.OrderNumber;
            this.ColorCode = TicketPriority.ColorCode;
            this.StatusId = TicketPriority.StatusId;
            this.Used = TicketPriority.Used;
            this.Status = TicketPriority.Status == null ? null : new TicketPriority_StatusDTO(TicketPriority.Status);
            this.CreatedAt = TicketPriority.CreatedAt;
            this.UpdatedAt = TicketPriority.UpdatedAt;
            this.Errors = TicketPriority.Errors;
        }
    }

    public class TicketPriority_TicketPriorityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public StringFilter ColorCode { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketPriorityOrder OrderBy { get; set; }
    }
}
