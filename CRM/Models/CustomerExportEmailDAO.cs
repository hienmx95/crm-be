using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerExportEmailDAO
    {
        public CustomerExportEmailDAO()
        {
            CustomerExportEmailCCMappings = new HashSet<CustomerExportEmailCCMappingDAO>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long CustomerExportId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO Creator { get; set; }
        public virtual CustomerExportDAO CustomerExport { get; set; }
        public virtual EmailStatusDAO EmailStatus { get; set; }
        public virtual ICollection<CustomerExportEmailCCMappingDAO> CustomerExportEmailCCMappings { get; set; }
    }
}
