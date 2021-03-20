using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.role
{
    public class Role_KnowledgeGroupDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public long? StatusId { get; set; }
        
        public long? DisplayOrder { get; set; }
        
        public string Description { get; set; }
        

        public Role_KnowledgeGroupDTO() {}
        public Role_KnowledgeGroupDTO(KnowledgeGroup KnowledgeGroup)
        {
            
            this.Id = KnowledgeGroup.Id;
            
            this.Name = KnowledgeGroup.Name;
            
            this.Code = KnowledgeGroup.Code;
            
            this.StatusId = KnowledgeGroup.StatusId;
            
            this.DisplayOrder = KnowledgeGroup.DisplayOrder;
            
            this.Description = KnowledgeGroup.Description;
            
            this.Errors = KnowledgeGroup.Errors;
        }
    }

    public class Role_KnowledgeGroupFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public StringFilter Description { get; set; }
        
        public KnowledgeGroupOrder OrderBy { get; set; }
    }
}