using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_status
{
    public class TicketStatus_TicketStatusDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public TicketStatus_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketStatus_TicketStatusDTO() {}
        public TicketStatus_TicketStatusDTO(TicketStatus TicketStatus)
        {
            this.Id = TicketStatus.Id;
            this.Name = TicketStatus.Name;
            this.OrderNumber = TicketStatus.OrderNumber;
            this.ColorCode = TicketStatus.ColorCode;
            this.StatusId = TicketStatus.StatusId;
            this.Used = TicketStatus.Used;
            this.Status = TicketStatus.Status == null ? null : new TicketStatus_StatusDTO(TicketStatus.Status);
            this.CreatedAt = TicketStatus.CreatedAt;
            this.UpdatedAt = TicketStatus.UpdatedAt;
            this.Errors = TicketStatus.Errors;
        }
    }

    public class TicketStatus_TicketStatusFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public StringFilter ColorCode { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketStatusOrder OrderBy { get; set; }
    }
}
