using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerResourceDAO
    {
        public CustomerResourceDAO()
        {
            Customers = new HashSet<CustomerDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
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
        public bool Used { get; set; }
        public Guid RowId { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CustomerDAO> Customers { get; set; }
    }
}
