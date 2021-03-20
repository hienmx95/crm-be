using CRM.Common;
using CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.company
{
    public class Company_WorkflowStateDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Company_WorkflowStateDTO() { }
        public Company_WorkflowStateDTO(WorkflowState WorkflowState)
        {
            this.Id = WorkflowState.Id;
            this.Code = WorkflowState.Code;
            this.Name = WorkflowState.Name;
        }
    }
}
