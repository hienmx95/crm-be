using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ProbabilityDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Contact_ProbabilityDTO() {}
        public Contact_ProbabilityDTO(Probability Probability)
        {
            
            this.Id = Probability.Id;
            
            this.Code = Probability.Code;
            
            this.Name = Probability.Name;
            
            this.Errors = Probability.Errors;
        }
    }

    public class Contact_ProbabilityFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ProbabilityOrder OrderBy { get; set; }
    }
}