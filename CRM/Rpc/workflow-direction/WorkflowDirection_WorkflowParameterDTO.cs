using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.workflow_direction
{
    public class WorkflowDirection_WorkflowParameterDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public WorkflowDirection_WorkflowParameterDTO() { }
        public WorkflowDirection_WorkflowParameterDTO(WorkflowParameter WorkflowParameter)
        {
            this.Id = WorkflowParameter.Id;
            this.Code = WorkflowParameter.Code;
            this.Name = WorkflowParameter.Name;
        }
    }
}
