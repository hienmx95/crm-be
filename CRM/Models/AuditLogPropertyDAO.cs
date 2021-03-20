using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class AuditLogPropertyDAO
    {
        public long Id { get; set; }
        public long? AppUserId { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ClassName { get; set; }
        public string ActionName { get; set; }
        public DateTime? Time { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
    }
}
