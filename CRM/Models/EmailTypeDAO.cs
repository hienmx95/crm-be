using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class EmailTypeDAO
    {
        public EmailTypeDAO()
        {
            CustomerEmails = new HashSet<CustomerEmailDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerEmailDAO> CustomerEmails { get; set; }
    }
}
