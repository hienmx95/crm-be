using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerEmailDTO : DataDTO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Email { get; set; }
        public long EmailTypeId { get; set; }
        public Customer_EmailTypeDTO EmailType { get; set; }   

        public Customer_CustomerEmailDTO() {}
        public Customer_CustomerEmailDTO(CustomerEmail CustomerEmail)
        {
            this.Id = CustomerEmail.Id;
            this.CustomerId = CustomerEmail.CustomerId;
            this.Email = CustomerEmail.Email;
            this.EmailTypeId = CustomerEmail.EmailTypeId;
            this.EmailType = CustomerEmail.EmailType == null ? null : new Customer_EmailTypeDTO(CustomerEmail.EmailType);
            this.Errors = CustomerEmail.Errors;
        }
    }

    public class Customer_CustomerEmailFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter CustomerId { get; set; }
        
        public StringFilter Email { get; set; }
        
        public IdFilter EmailTypeId { get; set; }
        
        public CustomerEmailOrder OrderBy { get; set; }
    }
}