using CRM.Common;
using CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.company
{
    public class Company_WorkflowStepDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long RoleId { get; set; }
        public Company_WorkflowStepDTO() { }
        public Company_WorkflowStepDTO(WorkflowStep WorkflowStep)
        {
            this.Id = WorkflowStep.Id;
            this.Code = WorkflowStep.Code;
            this.Name = WorkflowStep.Name;
            this.RoleId = WorkflowStep.RoleId;
        }
    }
}
