using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_group
{
    public class TicketGroup_TicketGroupDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public long StatusId { get; set; }
        public long TicketTypeId { get; set; }
        public bool Used { get; set; }
        public TicketGroup_StatusDTO Status { get; set; }
        public TicketGroup_TicketTypeDTO TicketType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketGroup_TicketGroupDTO() {}
        public TicketGroup_TicketGroupDTO(TicketGroup TicketGroup)
        {
            this.Id = TicketGroup.Id;
            this.Name = TicketGroup.Name;
            this.OrderNumber = TicketGroup.OrderNumber;
            this.StatusId = TicketGroup.StatusId;
            this.TicketTypeId = TicketGroup.TicketTypeId;
            this.Used = TicketGroup.Used;
            this.Status = TicketGroup.Status == null ? null : new TicketGroup_StatusDTO(TicketGroup.Status);
            this.TicketType = TicketGroup.TicketType == null ? null : new TicketGroup_TicketTypeDTO(TicketGroup.TicketType);
            this.CreatedAt = TicketGroup.CreatedAt;
            this.UpdatedAt = TicketGroup.UpdatedAt;
            this.Errors = TicketGroup.Errors;
        }
    }

    public class TicketGroup_TicketGroupFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter OrderNumber { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter TicketTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketGroupOrder OrderBy { get; set; }
    }
}
