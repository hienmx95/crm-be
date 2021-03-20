using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_ContractTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public long StatusId { get; set; }
        
        public long? DisplayOrder { get; set; }
        

        public Contract_ContractTypeDTO() {}
        public Contract_ContractTypeDTO(ContractType ContractType)
        {
            
            this.Id = ContractType.Id;
            
            this.Name = ContractType.Name;
            
            this.Code = ContractType.Code; 
            
            this.Errors = ContractType.Errors;
        }
    }

    public class Contract_ContractTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public ContractTypeOrder OrderBy { get; set; }
    }
}