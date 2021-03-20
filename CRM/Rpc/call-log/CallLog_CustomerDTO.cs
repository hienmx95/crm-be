using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_CustomerDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long? StatusId { get; set; }
        public CallLog_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CallLog_CustomerDTO() { }
        public CallLog_CustomerDTO(Customer Customer)
        {
            this.Id = Customer.Id;
            this.Code = Customer.Code;
            this.Name = Customer.Name;
            this.Phone = Customer.Phone;
            this.Email = Customer.Email;
            this.Address = Customer.Address;
            this.StatusId = Customer.StatusId;
            this.Status = Customer.Status == null ? null : new CallLog_StatusDTO(Customer.Status);
            this.CreatedAt = Customer.CreatedAt;
            this.UpdatedAt = Customer.UpdatedAt;
            this.Errors = Customer.Errors;

        }
    }

    public class CallLog_CustomerFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter IdentificationNumber { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerOrder OrderBy { get; set; }
    }
}
