using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContactProductMappingDAO
    {
        public long ContactId { get; set; }
        public long ProductId { get; set; }

        public virtual ContactDAO Contact { get; set; }
        public virtual ProductDAO Product { get; set; }
    }
}
