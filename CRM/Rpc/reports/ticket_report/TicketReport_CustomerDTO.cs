using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.ticket_report
{
    public class TicketReport_CustomerDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        public TicketReport_CustomerDTO() {}
        public TicketReport_CustomerDTO(Customer Customer)
        {
            
            this.Id = Customer.Id;
            
            this.Code = Customer.Code;

            this.Phone = Customer.Phone; 

            this.Email = Customer.Email; 

            this.Name = Customer.Name;

            this.Errors = Customer.Errors;
        }
    }

    public class TicketReport_CustomerFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        public StringFilter FullName { get; set; }
        
        public StringFilter Code { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter Address { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter IdentificationNumber { get; set; }
        
        public CustomerOrder OrderBy { get; set; }
    }
}