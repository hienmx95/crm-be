using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_FileDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long? AppUserId { get; set; }
        public Company_AppUserDTO AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Company_FileDTO() {}
        public Company_FileDTO(File File)
        {
            this.Id = File.Id;
            this.Name = File.Name;
            this.Url = File.Url;
            this.AppUserId = File.AppUserId;
            this.AppUser = File.AppUser == null ? null : new Company_AppUserDTO(File.AppUser);
            this.CreatedAt = File.CreatedAt;
            this.UpdatedAt = File.UpdatedAt;
            this.Errors = File.Errors;
        }
    }

    public class Company_FileFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Url { get; set; }
        public IdFilter AppUserId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public FileOrder OrderBy { get; set; }
    }
}
