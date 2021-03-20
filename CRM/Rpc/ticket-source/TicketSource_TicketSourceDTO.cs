using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_source
{
    public class TicketSource_TicketSourceDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public TicketSource_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketSource_TicketSourceDTO() {}
        public TicketSource_TicketSourceDTO(TicketSource TicketSource)
        {
            this.Id = TicketSource.Id;
            this.Name = TicketSource.Name;
            this.OrderNumber = TicketSource.OrderNumber;
            this.StatusId = TicketSource.StatusId;
            this.Used = TicketSource.Used;
            this.Status = TicketSource.Status == null ? null : new TicketSource_StatusDTO(TicketSource.Status);
            this.CreatedAt = TicketSource.CreatedAt;
            this.UpdatedAt = TicketSource.UpdatedAt;
            this.Errors = TicketSource.Errors;
        }
    }

    public class TicketSource_TicketSourceFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketSourceOrder OrderBy { get; set; }
    }
}
