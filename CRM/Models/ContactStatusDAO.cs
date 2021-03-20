using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContactStatusDAO
    {
        public ContactStatusDAO()
        {
            Contacts = new HashSet<ContactDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ContactDAO> Contacts { get; set; }
    }
}
