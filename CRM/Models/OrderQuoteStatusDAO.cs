using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderQuoteStatusDAO
    {
        public OrderQuoteStatusDAO()
        {
            OrderQuotes = new HashSet<OrderQuoteDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderQuoteDAO> OrderQuotes { get; set; }
    }
}
