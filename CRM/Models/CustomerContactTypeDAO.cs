using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerContactTypeDAO
    {
        public CustomerContactTypeDAO()
        {
            CustomerExportContacts = new HashSet<CustomerExportContactDAO>();
            CustomerProjectContacts = new HashSet<CustomerProjectContactDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerExportContactDAO> CustomerExportContacts { get; set; }
        public virtual ICollection<CustomerProjectContactDAO> CustomerProjectContacts { get; set; }
    }
}
