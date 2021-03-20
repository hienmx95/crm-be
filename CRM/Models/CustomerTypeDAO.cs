using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerTypeDAO
    {
        public CustomerTypeDAO()
        {
            CustomerGroupings = new HashSet<CustomerGroupingDAO>();
            CustomerSalesOrders = new HashSet<CustomerSalesOrderDAO>();
            Customers = new HashSet<CustomerDAO>();
            Tickets = new HashSet<TicketDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerGroupingDAO> CustomerGroupings { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrders { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
        public virtual ICollection<TicketDAO> Tickets { get; set; }
    }
}
