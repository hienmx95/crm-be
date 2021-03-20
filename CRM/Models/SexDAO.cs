using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SexDAO
    {
        public SexDAO()
        {
            AppUsers = new HashSet<AppUserDAO>();
            Contacts = new HashSet<ContactDAO>();
            CustomerLeads = new HashSet<CustomerLeadDAO>();
            Customers = new HashSet<CustomerDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AppUserDAO> AppUsers { get; set; }
        public virtual ICollection<ContactDAO> Contacts { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
    }
}
