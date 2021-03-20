using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Common.PermissionResult
{
    public class Permission
    {
        public string resourceId { get; set; }
        public string resourceName { get; set; }
        public bool resourceStatus { get; set; }
        public string resourceCode { get; set; }
        public List<Action> action { get; set; }
    }
}
