using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ImageDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Url { get; set; }
        

        public Contact_ImageDTO() {}
        public Contact_ImageDTO(Image Image)
        {
            
            this.Id = Image.Id;
            
            this.Name = Image.Name;
            
            this.Url = Image.Url;
            
            this.Errors = Image.Errors;
        }
    }

    public class Contact_ImageFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public StringFilter Url { get; set; }
        
        public ImageOrder OrderBy { get; set; }
    }
}