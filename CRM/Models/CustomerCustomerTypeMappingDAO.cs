using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerCustomerTypeMappingDAO
    {
        public long CustomerId { get; set; }
        public long CustomerTypeId { get; set; }

        public virtual CustomerDAO Customer { get; set; }
        public virtual CustomerTypeDAO CustomerType { get; set; }
    }
}
