using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_type
{
    public class TicketType_TicketTypeDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public TicketType_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketType_TicketTypeDTO() {}
        public TicketType_TicketTypeDTO(TicketType TicketType)
        {
            this.Id = TicketType.Id;
            this.Code = TicketType.Code;
            this.Name = TicketType.Name;
            this.ColorCode = TicketType.ColorCode;
            this.StatusId = TicketType.StatusId;
            this.Used = TicketType.Used;
            this.Status = TicketType.Status == null ? null : new TicketType_StatusDTO(TicketType.Status);
            this.CreatedAt = TicketType.CreatedAt;
            this.UpdatedAt = TicketType.UpdatedAt;
            this.Errors = TicketType.Errors;
        }
    }

    public class TicketType_TicketTypeFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter ColorCode { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketTypeOrder OrderBy { get; set; }
    }
}
