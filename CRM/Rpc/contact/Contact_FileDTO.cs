using CRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.contact
{
    public class Contact_FileDTO
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

        public Contact_FileDTO() { }
        public Contact_FileDTO(File File)
        {
            this.Id = File.Id;
            this.Name = File.Name;
            this.Url = File.Url;
            this.AppUserId = File.AppUserId;
            this.CreatedAt = File.CreatedAt;
            this.UpdatedAt = File.UpdatedAt;
        }
    }
}
