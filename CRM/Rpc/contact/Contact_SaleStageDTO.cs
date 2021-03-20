using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_SaleStageDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long? DisplayOrder { get; set; }
        

        public Contact_SaleStageDTO() {}
        public Contact_SaleStageDTO(SaleStage SaleStage)
        {
            
            this.Id = SaleStage.Id;
            
            this.Code = SaleStage.Code;
            
            this.Name = SaleStage.Name;
            
            this.Errors = SaleStage.Errors;
        }
    }

    public class Contact_SaleStageFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public LongFilter DisplayOrder { get; set; }
        
        public SaleStageOrder OrderBy { get; set; }
    }
}