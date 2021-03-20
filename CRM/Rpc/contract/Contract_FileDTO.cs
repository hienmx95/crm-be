using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_FileDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public long? AppUserId { get; set; }
        public Contract_AppUserDTO AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Contract_FileDTO() {}
        public Contract_FileDTO(File File)
        {
            this.Id = File.Id;
            this.Name = File.Name; 
            this.Url = File.Url;
            this.AppUserId = File.AppUserId;
            this.AppUser = File.AppUser == null ? null : new Contract_AppUserDTO(File.AppUser);
            this.CreatedAt = File.CreatedAt;
            this.UpdatedAt = File.UpdatedAt;
            this.Errors = File.Errors;
        }
    }

    public class Contract_FileFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Content { get; set; }
        public StringFilter Url { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public FileOrder OrderBy { get; set; }

        public IdFilter CompanyId { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter OpportunityId { get; set; }
    }
}
