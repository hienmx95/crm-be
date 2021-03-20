using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ProfessionDAO
    {
        public ProfessionDAO()
        {
            Companies = new HashSet<CompanyDAO>();
            Contacts = new HashSet<ContactDAO>();
            CustomerLeads = new HashSet<CustomerLeadDAO>();
            Customers = new HashSet<CustomerDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }
        public bool Used { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CompanyDAO> Companies { get; set; }
        public virtual ICollection<ContactDAO> Contacts { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
    }
}
