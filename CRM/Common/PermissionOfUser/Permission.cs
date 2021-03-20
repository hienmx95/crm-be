using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Common.UserProfile
{
    public class Permission
    {
        public string resourceId { get; set; }
        public string resourceName { get; set; }
        public bool resourceStatus { get; set; }
        public string resourceCode { get; set; }
        public List<ActionResult> action { get; set; }
    }
}
