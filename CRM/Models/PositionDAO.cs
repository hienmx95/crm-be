using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class PositionDAO
    {
        public PositionDAO()
        {
            Contacts = new HashSet<ContactDAO>();
            StoreRepresents = new HashSet<StoreRepresentDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public Guid RowId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<ContactDAO> Contacts { get; set; }
        public virtual ICollection<StoreRepresentDAO> StoreRepresents { get; set; }
    }
}
