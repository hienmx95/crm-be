using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_CooperativeAttitudeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        

        public Store_CooperativeAttitudeDTO() {}
        public Store_CooperativeAttitudeDTO(CooperativeAttitude CooperativeAttitude)
        {
            
            this.Id = CooperativeAttitude.Id;
            
            
            this.Name = CooperativeAttitude.Name;
            
            this.Errors = CooperativeAttitude.Errors;
        }
    }

    public class Store_CooperativeAttitudeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        
        public StringFilter Name { get; set; }
        
        public CooperativeAttitudeOrder OrderBy { get; set; }
    }
}