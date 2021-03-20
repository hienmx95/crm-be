using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_PotentialResultDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Opportunity_PotentialResultDTO() {}
        public Opportunity_PotentialResultDTO(PotentialResult PotentialResult)
        {
            
            this.Id = PotentialResult.Id;
            
            this.Code = PotentialResult.Code;
            
            this.Name = PotentialResult.Name;
            
            this.Errors = PotentialResult.Errors;
        }
    }

    public class Opportunity_PotentialResultFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public PotentialResultOrder OrderBy { get; set; }
    }
}