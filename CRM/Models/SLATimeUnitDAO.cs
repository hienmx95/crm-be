using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class SLATimeUnitDAO
    {
        public SLATimeUnitDAO()
        {
            SLAAlertFRTs = new HashSet<SLAAlertFRTDAO>();
            SLAAlerts = new HashSet<SLAAlertDAO>();
            SLAEscalationFRTs = new HashSet<SLAEscalationFRTDAO>();
            SLAEscalations = new HashSet<SLAEscalationDAO>();
            SLAPolicyFirstResponseUnits = new HashSet<SLAPolicyDAO>();
            SLAPolicyResolveUnits = new HashSet<SLAPolicyDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SLAAlertFRTDAO> SLAAlertFRTs { get; set; }
        public virtual ICollection<SLAAlertDAO> SLAAlerts { get; set; }
        public virtual ICollection<SLAEscalationFRTDAO> SLAEscalationFRTs { get; set; }
        public virtual ICollection<SLAEscalationDAO> SLAEscalations { get; set; }
        public virtual ICollection<SLAPolicyDAO> SLAPolicyFirstResponseUnits { get; set; }
        public virtual ICollection<SLAPolicyDAO> SLAPolicyResolveUnits { get; set; }
    }
}
