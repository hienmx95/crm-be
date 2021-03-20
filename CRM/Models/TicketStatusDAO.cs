using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TicketStatusDAO
    {
        public TicketStatusDAO()
        {
            TicketOfDepartments = new HashSet<TicketOfDepartmentDAO>();
            TicketOfUsers = new HashSet<TicketOfUserDAO>();
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderNumber { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<TicketOfDepartmentDAO> TicketOfDepartments { get; set; }
        public virtual ICollection<TicketOfUserDAO> TicketOfUsers { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
