using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerFeedbackDTO : DataDTO
    {
        public long Id { get; set; }
        public bool IsSystemCustomer { get; set; }
        public long? CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long? CustomerFeedbackTypeId { get; set; }
        public string Title { get; set; }
        public DateTime? SendDate { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
        public Customer_CustomerFeedbackTypeDTO CustomerFeedbackType { get; set; }   
        public Customer_StatusDTO Status { get; set; }   

        public Customer_CustomerFeedbackDTO() {}
        public Customer_CustomerFeedbackDTO(CustomerFeedback CustomerFeedback)
        {
            this.Id = CustomerFeedback.Id;
            this.IsSystemCustomer = CustomerFeedback.IsSystemCustomer;
            this.CustomerId = CustomerFeedback.CustomerId;
            this.FullName = CustomerFeedback.FullName;
            this.Email = CustomerFeedback.Email;
            this.PhoneNumber = CustomerFeedback.PhoneNumber;
            this.CustomerFeedbackTypeId = CustomerFeedback.CustomerFeedbackTypeId;
            this.Title = CustomerFeedback.Title;
            this.SendDate = CustomerFeedback.SendDate;
            this.Content = CustomerFeedback.Content;
            this.StatusId = CustomerFeedback.StatusId;
            this.CustomerFeedbackType = CustomerFeedback.CustomerFeedbackType == null ? null : new Customer_CustomerFeedbackTypeDTO(CustomerFeedback.CustomerFeedbackType);
            this.Status = CustomerFeedback.Status == null ? null : new Customer_StatusDTO(CustomerFeedback.Status);
            this.Errors = CustomerFeedback.Errors;
        }
    }

    public class Customer_CustomerFeedbackFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter CustomerId { get; set; }
        
        public StringFilter FullName { get; set; }
        
        public StringFilter Email { get; set; }
        
        public StringFilter PhoneNumber { get; set; }
        
        public IdFilter CustomerFeedbackTypeId { get; set; }
        
        public StringFilter Title { get; set; }
        
        public DateFilter SendDate { get; set; }
        
        public StringFilter Content { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public CustomerFeedbackOrder OrderBy { get; set; }
    }
}