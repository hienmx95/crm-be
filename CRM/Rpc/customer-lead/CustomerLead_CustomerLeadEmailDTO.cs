using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadEmailDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long CustomerLeadId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public CustomerLead_AppUserDTO Creator { get; set; }
        public CustomerLead_EmailStatusDTO EmailStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CustomerLead_CustomerLeadEmailCCMappingDTO> CustomerLeadEmailCCMappings { get; set; }
        public CustomerLead_CustomerLeadEmailDTO() {}
        public CustomerLead_CustomerLeadEmailDTO(CustomerLeadEmail CustomerLeadEmail)
        {
            this.Id = CustomerLeadEmail.Id;
            this.Title = CustomerLeadEmail.Title;
            this.Content = CustomerLeadEmail.Content;
            this.Reciepient = CustomerLeadEmail.Reciepient;
            this.CustomerLeadId = CustomerLeadEmail.CustomerLeadId;
            this.CreatorId = CustomerLeadEmail.CreatorId;
            this.EmailStatusId = CustomerLeadEmail.EmailStatusId;
            this.Creator = CustomerLeadEmail.Creator == null ? null : new CustomerLead_AppUserDTO(CustomerLeadEmail.Creator);
            this.EmailStatus = CustomerLeadEmail.EmailStatus == null ? null : new CustomerLead_EmailStatusDTO(CustomerLeadEmail.EmailStatus);
            this.CustomerLeadEmailCCMappings = CustomerLeadEmail.CustomerLeadEmailCCMappings?.Select(x => new CustomerLead_CustomerLeadEmailCCMappingDTO(x)).ToList();
            this.CreatedAt = CustomerLeadEmail.CreatedAt;
            this.UpdatedAt = CustomerLeadEmail.UpdatedAt;
            this.Errors = CustomerLeadEmail.Errors;
        }
    }

    public class CustomerLead_CustomerLeadEmailFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Reciepient { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter EmailStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerLeadEmailOrder OrderBy { get; set; }
    }
}
