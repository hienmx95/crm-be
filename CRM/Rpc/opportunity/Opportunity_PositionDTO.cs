using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_PositionDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long StatusId { get; set; }
        

        public Opportunity_PositionDTO() {}
        public Opportunity_PositionDTO(Position Position)
        {
            
            this.Id = Position.Id;
            
            this.Code = Position.Code;
            
            this.Name = Position.Name;
            
            this.StatusId = Position.StatusId;
            
            this.Errors = Position.Errors;
        }
    }

    public class Opportunity_PositionFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public PositionOrder OrderBy { get; set; }
    }
}