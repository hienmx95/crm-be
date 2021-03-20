using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.ticket
{
    public class Ticket_ProductDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        

        public Ticket_ProductDTO() {}
        public Ticket_ProductDTO(Product Product)
        {
            
            this.Id = Product.Id;
            
            this.Name = Product.Name;
            
            this.Errors = Product.Errors;
        }
    }

    public class Ticket_ProductFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public ProductOrder OrderBy { get; set; }
    }
}