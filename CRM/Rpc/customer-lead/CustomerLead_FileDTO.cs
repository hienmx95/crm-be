using CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_FileDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long? AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CustomerLead_AppUserDTO AppUser { get; set; }
        public CustomerLead_FileDTO() { }
        public CustomerLead_FileDTO(File File)
        {
            this.Id = File.Id;
            this.Name = File.Name;
            this.Url = File.Url;
            this.AppUserId = File.AppUserId;
            this.CreatedAt = File.CreatedAt;
            this.UpdatedAt = File.UpdatedAt;
            this.AppUser = File.AppUser == null ? null : new CustomerLead_AppUserDTO(File.AppUser);
        }
    }
}
