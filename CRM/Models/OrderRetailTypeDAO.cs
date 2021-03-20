using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderRetailTypeDAO
    {
        public OrderRetailTypeDAO()
        {
            OrderRetails = new HashSet<OrderRetailDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderRetailDAO> OrderRetails { get; set; }
    }
}
