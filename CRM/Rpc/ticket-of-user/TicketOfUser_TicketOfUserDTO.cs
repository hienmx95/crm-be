using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_of_user
{
    public class TicketOfUser_TicketOfUserDTO : DataDTO
    {
        public long Id { get; set; }
        public string Notes { get; set; }
        public long UserId { get; set; }
        public long TicketId { get; set; }
        public long TicketStatusId { get; set; }
        public TicketOfUser_TicketDTO Ticket { get; set; }
        public TicketOfUser_TicketStatusDTO TicketStatus { get; set; }
        public TicketOfUser_AppUserDTO User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketOfUser_TicketOfUserDTO() {}
        public TicketOfUser_TicketOfUserDTO(TicketOfUser TicketOfUser)
        {
            this.Id = TicketOfUser.Id;
            this.Notes = TicketOfUser.Notes;
            this.UserId = TicketOfUser.UserId;
            this.TicketId = TicketOfUser.TicketId;
            this.TicketStatusId = TicketOfUser.TicketStatusId;
            this.Ticket = TicketOfUser.Ticket == null ? null : new TicketOfUser_TicketDTO(TicketOfUser.Ticket);
            this.TicketStatus = TicketOfUser.TicketStatus == null ? null : new TicketOfUser_TicketStatusDTO(TicketOfUser.TicketStatus);
            this.User = TicketOfUser.User == null ? null : new TicketOfUser_AppUserDTO(TicketOfUser.User);
            this.CreatedAt = TicketOfUser.CreatedAt;
            this.UpdatedAt = TicketOfUser.UpdatedAt;
            this.Errors = TicketOfUser.Errors;
        }
    }

    public class TicketOfUser_TicketOfUserFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Notes { get; set; }
        public IdFilter UserId { get; set; }
        public IdFilter TicketId { get; set; }
        public IdFilter TicketStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public TicketOfUserOrder OrderBy { get; set; }
    }
}
