using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_feedback
{
    public class CustomerFeedback_CustomerFeedbackDTO : DataDTO
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
        public CustomerFeedback_CustomerDTO Customer { get; set; }
        public CustomerFeedback_CustomerFeedbackTypeDTO CustomerFeedbackType { get; set; }
        public CustomerFeedback_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerFeedback_CustomerFeedbackDTO() {}
        public CustomerFeedback_CustomerFeedbackDTO(CustomerFeedback CustomerFeedback)
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
            this.Customer = CustomerFeedback.Customer == null ? null : new CustomerFeedback_CustomerDTO(CustomerFeedback.Customer);
            this.CustomerFeedbackType = CustomerFeedback.CustomerFeedbackType == null ? null : new CustomerFeedback_CustomerFeedbackTypeDTO(CustomerFeedback.CustomerFeedbackType);
            this.Status = CustomerFeedback.Status == null ? null : new CustomerFeedback_StatusDTO(CustomerFeedback.Status);
            this.CreatedAt = CustomerFeedback.CreatedAt;
            this.UpdatedAt = CustomerFeedback.UpdatedAt;
            this.Errors = CustomerFeedback.Errors;
        }
    }

    public class CustomerFeedback_CustomerFeedbackFilterDTO : FilterDTO
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
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerFeedbackOrder OrderBy { get; set; }
    }
}
