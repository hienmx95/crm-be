using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class BusinessTypeDAO
    {
        public BusinessTypeDAO()
        {
            Customers = new HashSet<CustomerDAO>();
            StoreExtends = new HashSet<StoreExtendDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerDAO> Customers { get; set; }
        public virtual ICollection<StoreExtendDAO> StoreExtends { get; set; }
    }
}
