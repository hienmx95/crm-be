using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class EditedPriceStatusDAO
    {
        public EditedPriceStatusDAO()
        {
            CustomerSalesOrderContents = new HashSet<CustomerSalesOrderContentDAO>();
            CustomerSalesOrders = new HashSet<CustomerSalesOrderDAO>();
            DirectSalesOrderContents = new HashSet<DirectSalesOrderContentDAO>();
            DirectSalesOrders = new HashSet<DirectSalesOrderDAO>();
            OrderQuoteContents = new HashSet<OrderQuoteContentDAO>();
            OrderQuotes = new HashSet<OrderQuoteDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerSalesOrderContentDAO> CustomerSalesOrderContents { get; set; }
        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrders { get; set; }
        public virtual ICollection<DirectSalesOrderContentDAO> DirectSalesOrderContents { get; set; }
        public virtual ICollection<DirectSalesOrderDAO> DirectSalesOrders { get; set; }
        public virtual ICollection<OrderQuoteContentDAO> OrderQuoteContents { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuotes { get; set; }
    }
}
