using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_OpportunityCallLogMappingDTO : DataDTO
    {
        public long OpportunityId { get; set; }
        public long CallLogId { get; set; }
        public Opportunity_CallLogDTO CallLog { get; set; }   

        public Opportunity_OpportunityCallLogMappingDTO() {}
        public Opportunity_OpportunityCallLogMappingDTO(OpportunityCallLogMapping OpportunityCallLogMapping)
        {
            this.OpportunityId = OpportunityCallLogMapping.OpportunityId;
            this.CallLogId = OpportunityCallLogMapping.CallLogId;
            this.CallLog = OpportunityCallLogMapping.CallLog == null ? null : new Opportunity_CallLogDTO(OpportunityCallLogMapping.CallLog);
            this.Errors = OpportunityCallLogMapping.Errors;
        }
    }

    public class Opportunity_OpportunityCallLogMappingFilterDTO : FilterDTO
    {
        
        public IdFilter OpportunityId { get; set; }
        
        public IdFilter CallLogId { get; set; }
        
        public OpportunityCallLogMappingOrder OrderBy { get; set; }
    }
}