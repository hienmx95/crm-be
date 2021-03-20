using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerExportLeadershipDAO
    {
        public long Id { get; set; }
        public long CustomerExportId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public long? PositionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual CustomerExportDAO CustomerExport { get; set; }
        public virtual PositionDAO Position { get; set; }
    }
}
