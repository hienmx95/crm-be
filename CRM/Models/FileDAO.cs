using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class FileDAO
    {
        public FileDAO()
        {
            CompanyFileMappings = new HashSet<CompanyFileMappingDAO>();
            ContactFileMappings = new HashSet<ContactFileMappingDAO>();
            ContractFileMappings = new HashSet<ContractFileMappingDAO>();
            CustomerLeadFileMappings = new HashSet<CustomerLeadFileMappingDAO>();
            OpportunityFileMappings = new HashSet<OpportunityFileMappingDAO>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Đường dẫn Url
        /// </summary>
        public string Url { get; set; }
        public long? AppUserId { get; set; }
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

        public virtual AppUserDAO AppUser { get; set; }
        public virtual ICollection<CompanyFileMappingDAO> CompanyFileMappings { get; set; }
        public virtual ICollection<ContactFileMappingDAO> ContactFileMappings { get; set; }
        public virtual ICollection<ContractFileMappingDAO> ContractFileMappings { get; set; }
        public virtual ICollection<CustomerLeadFileMappingDAO> CustomerLeadFileMappings { get; set; }
        public virtual ICollection<OpportunityFileMappingDAO> OpportunityFileMappings { get; set; }
    }
}
