using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class RequestStateDAO
    {
        public RequestStateDAO()
        {
            CustomerSalesOrders = new HashSet<CustomerSalesOrderDAO>();
            DirectSalesOrders = new HashSet<DirectSalesOrderDAO>();
            RequestWorkflowDefinitionMappings = new HashSet<RequestWorkflowDefinitionMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerSalesOrderDAO> CustomerSalesOrders { get; set; }
        public virtual ICollection<DirectSalesOrderDAO> DirectSalesOrders { get; set; }
        public virtual ICollection<RequestWorkflowDefinitionMappingDAO> RequestWorkflowDefinitionMappings { get; set; }
    }
}
