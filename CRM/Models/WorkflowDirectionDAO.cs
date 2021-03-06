using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class WorkflowDirectionDAO
    {
        public long Id { get; set; }
        public long WorkflowDefinitionId { get; set; }
        public long FromStepId { get; set; }
        public long ToStepId { get; set; }
        public string SubjectMailForCreator { get; set; }
        public string SubjectMailForNextStep { get; set; }
        public string BodyMailForCreator { get; set; }
        public string BodyMailForNextStep { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual WorkflowStepDAO FromStep { get; set; }
        public virtual WorkflowStepDAO ToStep { get; set; }
        public virtual WorkflowDefinitionDAO WorkflowDefinition { get; set; }
    }
}
