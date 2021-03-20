using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_TaxTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public decimal Percentage { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public CustomerLead_TaxTypeDTO() {}
        public CustomerLead_TaxTypeDTO(TaxType TaxType)
        {
            
            this.Id = TaxType.Id;
            
            this.Code = TaxType.Code;
            
            this.Name = TaxType.Name;
            
            this.Percentage = TaxType.Percentage;
            
            this.StatusId = TaxType.StatusId;
            
            this.Used = TaxType.Used;
            
            this.Errors = TaxType.Errors;
        }
    }

    public class CustomerLead_TaxTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public DecimalFilter Percentage { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public TaxTypeOrder OrderBy { get; set; }
    }
}