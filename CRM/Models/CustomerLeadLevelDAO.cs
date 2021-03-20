using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLeadLevelDAO
    {
        public CustomerLeadLevelDAO()
        {
            CustomerLeads = new HashSet<CustomerLeadDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
    }
}
