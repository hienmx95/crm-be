using CRM.Common;
using CRM.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CRM.Rpc.company
{
    public class Company_CompanyEmailCCMappingDTO : DataDTO
    {
        public long CompanyEmailId { get; set; }
        public long AppUserId { get; set; }
        public Company_AppUserDTO AppUser { get; set; }

        public Company_CompanyEmailCCMappingDTO() { }
        public Company_CompanyEmailCCMappingDTO(CompanyEmailCCMapping CompanyEmailCCMapping)
        {
            this.CompanyEmailId = CompanyEmailCCMapping.CompanyEmailId;
            this.AppUserId = CompanyEmailCCMapping.AppUserId;
            this.AppUser = CompanyEmailCCMapping.AppUser == null ? null : new Company_AppUserDTO(CompanyEmailCCMapping.AppUser);
            this.Errors = CompanyEmailCCMapping.Errors;
        }
    }

    public class Company_CompanyEmailCCMappingFilter : FilterDTO
    {
        public IdFilter CompanyEmailId { get; set; }
        public IdFilter AppUserId { get; set; }
        public CompanyEmailCCMappingOrder OrderBy { get; set; }
    }
}
