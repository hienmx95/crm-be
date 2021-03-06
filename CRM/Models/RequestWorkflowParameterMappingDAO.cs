using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class RequestWorkflowParameterMappingDAO
    {
        public long WorkflowParameterId { get; set; }
        public Guid RequestId { get; set; }
        public string Value { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual WorkflowParameterDAO WorkflowParameter { get; set; }
    }
}
