using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderProjectTypeDAO
    {
        public OrderProjectTypeDAO()
        {
            OrderProjects = new HashSet<OrderProjectDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderProjectDAO> OrderProjects { get; set; }
    }
}
