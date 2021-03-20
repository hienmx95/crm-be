using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_ProfessionDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public long StatusId { get; set; }
        
        public Guid RowId { get; set; }
        
        public bool Used { get; set; }
        

        public Company_ProfessionDTO() {}
        public Company_ProfessionDTO(Profession Profession)
        {
            
            this.Id = Profession.Id;
            
            this.Code = Profession.Code;
            
            this.Name = Profession.Name;
            
            this.StatusId = Profession.StatusId;
            
            this.RowId = Profession.RowId;
            
            this.Used = Profession.Used;
            
            this.Errors = Profession.Errors;
        }
    }

    public class Company_ProfessionFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public ProfessionOrder OrderBy { get; set; }
    }
}