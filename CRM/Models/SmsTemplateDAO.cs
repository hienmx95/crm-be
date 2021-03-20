using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SmsTemplateDAO
    {
        public SmsTemplateDAO()
        {
            SLAAlertFRTs = new HashSet<SLAAlertFRTDAO>();
            SLAAlerts = new HashSet<SLAAlertDAO>();
            SLAEscalationFRTs = new HashSet<SLAEscalationFRTDAO>();
            SLAEscalations = new HashSet<SLAEscalationDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
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

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<SLAAlertFRTDAO> SLAAlertFRTs { get; set; }
        public virtual ICollection<SLAAlertDAO> SLAAlerts { get; set; }
        public virtual ICollection<SLAEscalationFRTDAO> SLAEscalationFRTs { get; set; }
        public virtual ICollection<SLAEscalationDAO> SLAEscalations { get; set; }
    }
}
