using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SmsQueueDAO
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string SmsCode { get; set; }
        public string SmsTitle { get; set; }
        public DateTime? SentDate { get; set; }
        public string SmsContent { get; set; }
        public long? SentByAppUserId { get; set; }
        public long? SmsQueueStatusId { get; set; }
        public long? EntityReferenceId { get; set; }
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

        public virtual EntityReferenceDAO EntityReference { get; set; }
        public virtual AppUserDAO SentByAppUser { get; set; }
        public virtual SmsQueueStatusDAO SmsQueueStatus { get; set; }
    }
}
