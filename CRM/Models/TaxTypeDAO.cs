using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class TaxTypeDAO
    {
        public TaxTypeDAO()
        {
            ContractItemDetails = new HashSet<ContractItemDetailDAO>();
            CustomerSalesOrderContents = new HashSet<CustomerSalesOrderContentDAO>();
            OrderQuoteContents = new HashSet<OrderQuoteContentDAO>();
            Products = new HashSet<ProductDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public long StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<ContractItemDetailDAO> ContractItemDetails { get; set; }
        public virtual ICollection<CustomerSalesOrderContentDAO> CustomerSalesOrderContents { get; set; }
        public virtual ICollection<OrderQuoteContentDAO> OrderQuoteContents { get; set; }
        public virtual ICollection<ProductDAO> Products { get; set; }
    }
}
