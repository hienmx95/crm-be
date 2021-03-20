using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerPhoneDTO : DataDTO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Phone { get; set; }
        public long PhoneTypeId { get; set; }
        public Customer_PhoneTypeDTO PhoneType { get; set; }   

        public Customer_CustomerPhoneDTO() {}
        public Customer_CustomerPhoneDTO(CustomerPhone CustomerPhone)
        {
            this.Id = CustomerPhone.Id;
            this.CustomerId = CustomerPhone.CustomerId;
            this.Phone = CustomerPhone.Phone;
            this.PhoneTypeId = CustomerPhone.PhoneTypeId;
            this.PhoneType = CustomerPhone.PhoneType == null ? null : new Customer_PhoneTypeDTO(CustomerPhone.PhoneType);
            this.Errors = CustomerPhone.Errors;
        }
    }

    public class Customer_CustomerPhoneFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter CustomerId { get; set; }
        
        public StringFilter Phone { get; set; }
        
        public IdFilter PhoneTypeId { get; set; }
        
        public CustomerPhoneOrder OrderBy { get; set; }
    }
}