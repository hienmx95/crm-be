using CRM.Common;
using CRM.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CRM.Rpc.contact
{
    public class Contact_ContactEmailCCMappingDTO : DataDTO
    {
        public long ContactEmailId { get; set; }
        public long AppUserId { get; set; }
        public Contact_AppUserDTO AppUser { get; set; }

        public Contact_ContactEmailCCMappingDTO() { }
        public Contact_ContactEmailCCMappingDTO(ContactEmailCCMapping ContactEmailCCMapping)
        {
            this.ContactEmailId = ContactEmailCCMapping.ContactEmailId;
            this.AppUserId = ContactEmailCCMapping.AppUserId;
            this.AppUser = ContactEmailCCMapping.AppUser == null ? null : new Contact_AppUserDTO(ContactEmailCCMapping.AppUser);
            this.Errors = ContactEmailCCMapping.Errors;
        }
    }

    public class Contact_ContactEmailCCMappingFilter : FilterDTO
    {
        public IdFilter ContactEmailId { get; set; }
        public IdFilter AppUserId { get; set; }
        public ContactEmailCCMappingOrder OrderBy { get; set; }
    }
}
