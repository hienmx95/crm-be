using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.knowledge_article
{
    public class KnowledgeArticle_KMSStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public KnowledgeArticle_KMSStatusDTO() {}
        public KnowledgeArticle_KMSStatusDTO(KMSStatus KMSStatus)
        {
            
            this.Id = KMSStatus.Id;
            
            this.Code = KMSStatus.Code;
            
            this.Name = KMSStatus.Name;
            
            this.Errors = KMSStatus.Errors;
        }
    }

    public class KnowledgeArticle_KMSStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public KMSStatusOrder OrderBy { get; set; }
    }
}