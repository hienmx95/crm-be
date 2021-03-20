using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreCooperativeAttitudeMappingDAO
    {
        public long StoreId { get; set; }
        public long CooperativeAttitudeId { get; set; }

        public virtual CooperativeAttitudeDAO CooperativeAttitude { get; set; }
        public virtual StoreDAO Store { get; set; }
    }
}
