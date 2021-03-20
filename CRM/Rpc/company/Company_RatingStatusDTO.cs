using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_RatingStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Code { get; set; }
        

        public Company_RatingStatusDTO() {}
        public Company_RatingStatusDTO(RatingStatus RatingStatus)
        {
            
            this.Id = RatingStatus.Id;
            
            this.Name = RatingStatus.Name;
            
            this.Code = RatingStatus.Code;
            
            this.Errors = RatingStatus.Errors;
        }
    }

    public class Company_RatingStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Code { get; set; }
        
        public RatingStatusOrder OrderBy { get; set; }
    }
}