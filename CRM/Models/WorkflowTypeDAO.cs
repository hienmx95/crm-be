using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class WorkflowTypeDAO
    {
        public WorkflowTypeDAO()
        {
            WorkflowDefinitions = new HashSet<WorkflowDefinitionDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<WorkflowDefinitionDAO> WorkflowDefinitions { get; set; }
    }
}
