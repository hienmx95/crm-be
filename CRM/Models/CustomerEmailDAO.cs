using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerEmailDAO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Email { get; set; }
        public long EmailTypeId { get; set; }

        public virtual CustomerDAO Customer { get; set; }
        public virtual EmailTypeDAO EmailType { get; set; }
    }
}
