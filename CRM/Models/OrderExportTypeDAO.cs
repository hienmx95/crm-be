using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderExportTypeDAO
    {
        public OrderExportTypeDAO()
        {
            OrderExports = new HashSet<OrderExportDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderExportDAO> OrderExports { get; set; }
    }
}
