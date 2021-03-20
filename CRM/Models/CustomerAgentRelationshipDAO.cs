using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerAgentRelationshipDAO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public long? PositionId { get; set; }
        public long? CustomerAgentId { get; set; }

        public virtual CustomerAgentDAO CustomerAgent { get; set; }
        public virtual PositionDAO Position { get; set; }
    }
}
