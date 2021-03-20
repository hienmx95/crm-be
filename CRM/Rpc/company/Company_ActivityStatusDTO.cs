using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_ActivityStatusDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Company_ActivityStatusDTO() {}
        public Company_ActivityStatusDTO(ActivityStatus ActivityStatus)
        {
            
            this.Id = ActivityStatus.Id;
            
            this.Code = ActivityStatus.Code;
            
            this.Name = ActivityStatus.Name;
            
            this.Errors = ActivityStatus.Errors;
        }
    }

    public class Company_ActivityStatusFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ActivityStatusOrder OrderBy { get; set; }
    }
}