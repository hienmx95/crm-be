using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SmsQueueStatusDAO
    {
        public SmsQueueStatusDAO()
        {
            SmsQueues = new HashSet<SmsQueueDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<SmsQueueDAO> SmsQueues { get; set; }
    }
}
