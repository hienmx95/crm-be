using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLeadStatusDAO
    {
        public CustomerLeadStatusDAO()
        {
            CustomerLeads = new HashSet<CustomerLeadDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
    }
}
