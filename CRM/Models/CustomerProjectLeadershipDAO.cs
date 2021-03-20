using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerProjectLeadershipDAO
    {
        public long Id { get; set; }
        public long CustomerProjectId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public long? PositionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual CustomerProjectDAO CustomerProject { get; set; }
        public virtual PositionDAO Position { get; set; }
    }
}
