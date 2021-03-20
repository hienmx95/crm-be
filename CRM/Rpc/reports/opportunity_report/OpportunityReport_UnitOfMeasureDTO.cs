using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.opportunity_report
{
    public class OpportunityReport_UnitOfMeasureDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public long StatusId { get; set; }
        
        public bool Used { get; set; }
        

        public OpportunityReport_UnitOfMeasureDTO() {}
        public OpportunityReport_UnitOfMeasureDTO(UnitOfMeasure UnitOfMeasure)
        {
            
            this.Id = UnitOfMeasure.Id;
            
            this.Code = UnitOfMeasure.Code;
            
            this.Name = UnitOfMeasure.Name;
            
            this.Description = UnitOfMeasure.Description;
            
            this.StatusId = UnitOfMeasure.StatusId;
            
            this.Used = UnitOfMeasure.Used;
            
            this.Errors = UnitOfMeasure.Errors;
        }
    }

    public class OpportunityReport_UnitOfMeasureFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Description { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public UnitOfMeasureOrder OrderBy { get; set; }
    }
}