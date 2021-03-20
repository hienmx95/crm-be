using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_ContractContactMappingDTO : DataDTO
    {
        public long ContractId { get; set; }
        public long ContactId { get; set; }
        public Contract_ContactDTO Contact { get; set; }   

        public Contract_ContractContactMappingDTO() {}
        public Contract_ContractContactMappingDTO(ContractContactMapping ContractContactMapping)
        {
            this.ContractId = ContractContactMapping.ContractId;
            this.ContactId = ContractContactMapping.ContactId;
            this.Contact = ContractContactMapping.Contact == null ? null : new Contract_ContactDTO(ContractContactMapping.Contact);
            this.Errors = ContractContactMapping.Errors;
        }
    }

    public class Contract_ContractContactMappingFilterDTO : FilterDTO
    {
        
        public IdFilter ContractId { get; set; }
        
        public IdFilter ContactId { get; set; }
        
        public ContractContactMappingOrder OrderBy { get; set; }
    }
}