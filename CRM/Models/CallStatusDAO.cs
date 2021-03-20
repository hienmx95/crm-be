using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CallStatusDAO
    {
        public CallStatusDAO()
        {
            CallLogs = new HashSet<CallLogDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CallLogDAO> CallLogs { get; set; }
    }
}
