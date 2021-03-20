using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_RelationshipCustomerTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        

        public Store_RelationshipCustomerTypeDTO() {}
        public Store_RelationshipCustomerTypeDTO(RelationshipCustomerType RelationshipCustomerType)
        {
            
            this.Id = RelationshipCustomerType.Id;
            
            
            this.Name = RelationshipCustomerType.Name;
            
            this.Errors = RelationshipCustomerType.Errors;
        }
    }

    public class Store_RelationshipCustomerTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        
        public StringFilter Name { get; set; }
        
        public RelationshipCustomerTypeOrder OrderBy { get; set; }
    }
}