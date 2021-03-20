using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ContactEmailDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long ContactId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public Contact_AppUserDTO Creator { get; set; }
        public Contact_EmailStatusDTO EmailStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Contact_ContactEmailCCMappingDTO> ContactEmailCCMappings { get; set; }
        public Contact_ContactEmailDTO() {}
        public Contact_ContactEmailDTO(ContactEmail ContactEmail)
        {
            this.Id = ContactEmail.Id;
            this.Title = ContactEmail.Title;
            this.Content = ContactEmail.Content;
            this.Reciepient = ContactEmail.Reciepient;
            this.ContactId = ContactEmail.ContactId;
            this.CreatorId = ContactEmail.CreatorId;
            this.EmailStatusId = ContactEmail.EmailStatusId;
            this.Creator = ContactEmail.Creator == null ? null : new Contact_AppUserDTO(ContactEmail.Creator);
            this.EmailStatus = ContactEmail.EmailStatus == null ? null : new Contact_EmailStatusDTO(ContactEmail.EmailStatus);
            this.ContactEmailCCMappings = ContactEmail.ContactEmailCCMappings?.Select(x => new Contact_ContactEmailCCMappingDTO(x)).ToList();
            this.CreatedAt = ContactEmail.CreatedAt;
            this.UpdatedAt = ContactEmail.UpdatedAt;
            this.Errors = ContactEmail.Errors;
        }
    }

    public class Contact_ContactEmailFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Reciepient { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter EmailStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ContactEmailOrder OrderBy { get; set; }
    }
}
