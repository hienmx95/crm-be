using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_ContractStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public long? DisplayOrder { get; set; }
        

        public Company_ContractStatusDTO() {}
        public Company_ContractStatusDTO(ContractStatus ContractStatus)
        {
            
            this.Id = ContractStatus.Id;
            
            this.Name = ContractStatus.Name;
            
            this.Code = ContractStatus.Code;
            
            this.Errors = ContractStatus.Errors;
        }
    }

    public class Company_ContractStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public ContractStatusOrder OrderBy { get; set; }
    }
}