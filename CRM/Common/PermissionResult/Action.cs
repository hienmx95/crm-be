using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Common.PermissionResult
{
    public class Action
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public bool status { get; set; }
        public string filter { get; set; }
        public string description { get; set; }
    }
}
