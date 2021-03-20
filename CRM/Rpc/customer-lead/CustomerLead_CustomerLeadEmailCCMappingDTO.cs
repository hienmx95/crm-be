using CRM.Common;
using CRM.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadEmailCCMappingDTO : DataDTO
    {
        public long CustomerLeadEmailId { get; set; }
        public long AppUserId { get; set; }
        public CustomerLead_AppUserDTO AppUser { get; set; }

        public CustomerLead_CustomerLeadEmailCCMappingDTO() { }
        public CustomerLead_CustomerLeadEmailCCMappingDTO(CustomerLeadEmailCCMapping CustomerLeadEmailCCMapping)
        {
            this.CustomerLeadEmailId = CustomerLeadEmailCCMapping.CustomerLeadEmailId;
            this.AppUserId = CustomerLeadEmailCCMapping.AppUserId;
            this.AppUser = CustomerLeadEmailCCMapping.AppUser == null ? null : new CustomerLead_AppUserDTO(CustomerLeadEmailCCMapping.AppUser);
            this.Errors = CustomerLeadEmailCCMapping.Errors;
        }
    }

    public class CustomerLead_CustomerLeadEmailCCMappingFilter : FilterDTO
    {
        public IdFilter CustomerLeadEmailId { get; set; }
        public IdFilter AppUserId { get; set; }
        public CustomerLeadEmailCCMappingOrder OrderBy { get; set; }
    }
}
