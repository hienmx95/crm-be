using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContactEmailDAO
    {
        public ContactEmailDAO()
        {
            ContactEmailCCMappings = new HashSet<ContactEmailCCMappingDAO>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long ContactId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        public virtual ContactDAO Contact { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual EmailStatusDAO EmailStatus { get; set; }
        public virtual ICollection<ContactEmailCCMappingDAO> ContactEmailCCMappings { get; set; }
    }
}
