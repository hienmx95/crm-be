using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_ProfessionDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Opportunity_ProfessionDTO() {}
        public Opportunity_ProfessionDTO(Profession Profession)
        {
            
            this.Id = Profession.Id;
            
            this.Code = Profession.Code;
            
            this.Name = Profession.Name;
            
            this.Errors = Profession.Errors;
        }
    }

    public class Opportunity_ProfessionFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ProfessionOrder OrderBy { get; set; }
    }
}