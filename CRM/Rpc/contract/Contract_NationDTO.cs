using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_NationDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? DisplayOrder { get; set; }
        
        public long StatusId { get; set; }
        

        public Contract_NationDTO() {}
        public Contract_NationDTO(Nation Nation)
        {
            
            this.Id = Nation.Id;
            
            this.Code = Nation.Code;
            
            this.Name = Nation.Name;
            
            this.DisplayOrder = Nation.Priority;
            
            this.StatusId = Nation.StatusId;
            
            this.Errors = Nation.Errors;
        }
    }

    public class Contract_NationFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public NationOrder OrderBy { get; set; }
    }
}