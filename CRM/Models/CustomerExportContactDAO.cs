using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerExportContactDAO
    {
        public long Id { get; set; }
        public long CustomerExportId { get; set; }
        public long CustomerContactTypeId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual CustomerContactTypeDAO CustomerContactType { get; set; }
        public virtual CustomerExportDAO CustomerExport { get; set; }
    }
}
