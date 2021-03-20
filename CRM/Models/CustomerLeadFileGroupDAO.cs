using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLeadFileGroupDAO
    {
        public CustomerLeadFileGroupDAO()
        {
            CustomerLeadFileMappings = new HashSet<CustomerLeadFileMappingDAO>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Description { get; set; }
        public long CustomerLeadId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
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
        public Guid RowId { get; set; }

        public virtual AppUserDAO Creator { get; set; }
        public virtual CustomerLeadDAO CustomerLead { get; set; }
        public virtual FileTypeDAO FileType { get; set; }
        public virtual ICollection<CustomerLeadFileMappingDAO> CustomerLeadFileMappings { get; set; }
    }
}
