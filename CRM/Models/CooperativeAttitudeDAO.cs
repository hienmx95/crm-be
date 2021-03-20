using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CooperativeAttitudeDAO
    {
        public CooperativeAttitudeDAO()
        {
            StoreCooperativeAttitudeMappings = new HashSet<StoreCooperativeAttitudeMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StoreCooperativeAttitudeMappingDAO> StoreCooperativeAttitudeMappings { get; set; }
    }
}
