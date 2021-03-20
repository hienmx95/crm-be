using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerFeedbackDAO
    {
        public long Id { get; set; }
        public bool IsSystemCustomer { get; set; }
        public long? CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long? CustomerFeedbackTypeId { get; set; }
        public string Title { get; set; }
        public DateTime? SendDate { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
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

        public virtual CustomerDAO Customer { get; set; }
        public virtual CustomerFeedbackTypeDAO CustomerFeedbackType { get; set; }
        public virtual StatusDAO Status { get; set; }
    }
}
