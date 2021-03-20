using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_EntityReferenceDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Company_EntityReferenceDTO() {}
        public Company_EntityReferenceDTO(EntityReference EntityReference)
        {
            
            this.Id = EntityReference.Id;
            
            this.Code = EntityReference.Code;
            
            this.Name = EntityReference.Name;
            
            this.Errors = EntityReference.Errors;
        }
    }

    public class Company_EntityReferenceFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public EntityReferenceOrder OrderBy { get; set; }
    }
}