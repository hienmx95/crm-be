using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerProjectContactDAO
    {
        public long Id { get; set; }
        public long CustomerProjectId { get; set; }
        public long CustomerContactTypeId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual CustomerContactTypeDAO CustomerContactType { get; set; }
        public virtual CustomerProjectDAO CustomerProject { get; set; }
    }
}
