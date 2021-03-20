using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket_issue_level
{
    public class TicketIssueLevel_SLATimeUnitDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public TicketIssueLevel_SLATimeUnitDTO() {}
        public TicketIssueLevel_SLATimeUnitDTO(SLATimeUnit SLATimeUnit)
        {
            this.Id = SLATimeUnit.Id;
            this.Code = SLATimeUnit.Code;
            this.Name = SLATimeUnit.Name;
            this.Errors = SLATimeUnit.Errors;
        }
    }

    public class TicketIssueLevel_SLATimeUnitFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public SLATimeUnitOrder OrderBy { get; set; }
    }
}
