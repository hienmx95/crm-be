using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_OpportunityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CallLog_OpportunityDTO() {}
        public CallLog_OpportunityDTO(Opportunity Opportunity)
        {
            this.Id = Opportunity.Id;
            this.Name = Opportunity.Name;
        }
    }

    public class CallLog_OpportunityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public OpportunityOrder OrderBy { get; set; }
    }
}