using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_PotentialResultDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Contract_PotentialResultDTO() {}
        public Contract_PotentialResultDTO(PotentialResult PotentialResult)
        {
            
            this.Id = PotentialResult.Id;
            
            this.Code = PotentialResult.Code;
            
            this.Name = PotentialResult.Name;
            
            this.Errors = PotentialResult.Errors;
        }
    }

    public class Contract_PotentialResultFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public PotentialResultOrder OrderBy { get; set; }
    }
}