using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_ImageDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Url { get; set; }
        

        public Opportunity_ImageDTO() {}
        public Opportunity_ImageDTO(Image Image)
        {
            
            this.Id = Image.Id;
            
            this.Name = Image.Name;
            
            this.Url = Image.Url;
            
            this.Errors = Image.Errors;
        }
    }

    public class Opportunity_ImageFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Url { get; set; }
        
        public ImageOrder OrderBy { get; set; }
    }
}