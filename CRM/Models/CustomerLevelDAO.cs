using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerLevelDAO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public long PointFrom { get; set; }
        public long PointTo { get; set; }
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
    }
}
