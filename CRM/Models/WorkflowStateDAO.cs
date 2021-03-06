using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class WorkflowStateDAO
    {
        public WorkflowStateDAO()
        {
            RequestWorkflowStepMappings = new HashSet<RequestWorkflowStepMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RequestWorkflowStepMappingDAO> RequestWorkflowStepMappings { get; set; }
    }
}
