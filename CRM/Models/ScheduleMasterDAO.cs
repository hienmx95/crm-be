using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ScheduleMasterDAO
    {
        public long Id { get; set; }
        public long? ManagerId { get; set; }
        public long? SalerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public DateTime? RecurDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? NoEndDate { get; set; }
        public DateTime? StartDayOfWeek { get; set; }
        public long? DisplayOrder { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public virtual AppUserDAO Manager { get; set; }
        public virtual AppUserDAO Saler { get; set; }
        public virtual StatusDAO Status { get; set; }
    }
}
