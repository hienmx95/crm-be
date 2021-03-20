using CRM.Common;
using CRM.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Rpc.customer
{
    public class Customer_CustomerEmailHistoryDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long CustomerId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public Customer_AppUserDTO Creator { get; set; }
        public Customer_CustomerDTO Customer { get; set; }
        public Customer_EmailStatusDTO EmailStatus { get; set; }
        public List<Customer_CustomerCCEmailHistoryDTO> CustomerCCEmailHistories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Customer_CustomerEmailHistoryDTO() { }
        public Customer_CustomerEmailHistoryDTO(CustomerEmailHistory CustomerEmailHistory)
        {
            this.Id = CustomerEmailHistory.Id;
            this.Title = CustomerEmailHistory.Title;
            this.Content = CustomerEmailHistory.Content;
            this.Reciepient = CustomerEmailHistory.Reciepient;
            this.CustomerId = CustomerEmailHistory.CustomerId;
            this.CreatorId = CustomerEmailHistory.CreatorId;
            this.EmailStatusId = CustomerEmailHistory.EmailStatusId;
            this.CreatedAt = CustomerEmailHistory.CreatedAt;
            this.UpdatedAt = CustomerEmailHistory.UpdatedAt;
            this.Creator = CustomerEmailHistory.Creator == null ? null : new Customer_AppUserDTO(CustomerEmailHistory.Creator);
            this.Customer = CustomerEmailHistory.Customer == null ? null : new Customer_CustomerDTO(CustomerEmailHistory.Customer);
            this.EmailStatus = CustomerEmailHistory.EmailStatus == null ? null : new Customer_EmailStatusDTO(CustomerEmailHistory.EmailStatus);
            this.CustomerCCEmailHistories = CustomerEmailHistory.CustomerCCEmailHistories?.Select(x => new Customer_CustomerCCEmailHistoryDTO(x)).ToList();
            this.Errors = CustomerEmailHistory.Errors;
        }
    }

    public class Customer_CustomerEmailHistoryFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Reciepient { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter EmailStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CompanyEmailCCMappingOrder OrderBy { get; set; }
    }
}
