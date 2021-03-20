using CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.opportunity
{
    public class Opportunity_FileDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public long? AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long? GroupId { get; set; }

        public Opportunity_AppUserDTO AppUser { get; set; }
        public Opportunity_FileDTO() { }
        public Opportunity_FileDTO(File File)
        {
            this.Id = File.Id;
            this.Name = File.Name;
            this.Url = File.Url;
            this.AppUserId = File.AppUserId;
            this.CreatedAt = File.CreatedAt;
            this.UpdatedAt = File.UpdatedAt;
            this.AppUser = File.AppUser == null ? null : new Opportunity_AppUserDTO(File.AppUser);
        }
    }
}
