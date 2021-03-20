using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_OpportunityResultTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? DisplayOrder { get; set; }
        

        public Contact_OpportunityResultTypeDTO() {}
        public Contact_OpportunityResultTypeDTO(OpportunityResultType OpportunityResultType)
        {
            
            this.Id = OpportunityResultType.Id;
            
            this.Code = OpportunityResultType.Code;
            
            this.Name = OpportunityResultType.Name;
            
            this.Errors = OpportunityResultType.Errors;
        }
    }

    public class Contact_OpportunityResultTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public OpportunityResultTypeOrder OrderBy { get; set; }
    }
}