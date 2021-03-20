using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_OpportunityResultTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? DisplayOrder { get; set; }
        

        public Contract_OpportunityResultTypeDTO() {}
        public Contract_OpportunityResultTypeDTO(OpportunityResultType OpportunityResultType)
        {
            
            this.Id = OpportunityResultType.Id;
            
            this.Code = OpportunityResultType.Code;
            
            this.Name = OpportunityResultType.Name;
            
            this.Errors = OpportunityResultType.Errors;
        }
    }

    public class Contract_OpportunityResultTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public OpportunityResultTypeOrder OrderBy { get; set; }
    }
}