using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_EditedPriceStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Contract_EditedPriceStatusDTO() {}
        public Contract_EditedPriceStatusDTO(EditedPriceStatus EditedPriceStatus)
        {
            
            this.Id = EditedPriceStatus.Id;
            
            this.Code = EditedPriceStatus.Code;
            
            this.Name = EditedPriceStatus.Name;
            
            this.Errors = EditedPriceStatus.Errors;
        }
    }

    public class Contract_EditedPriceStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public EditedPriceStatusOrder OrderBy { get; set; }
    }
}