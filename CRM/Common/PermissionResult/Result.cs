using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Common.PermissionResult
{
    public class Result
    {
        public List<string> dataLimitView { get; set; }
        public List<string> dataLimitManagement { get; set; }
        public List<Permission> permissions { get; set; }
    }
}
