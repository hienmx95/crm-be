using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerPhoneDAO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Phone { get; set; }
        public long PhoneTypeId { get; set; }

        public virtual CustomerDAO Customer { get; set; }
        public virtual PhoneTypeDAO PhoneType { get; set; }
    }
}
