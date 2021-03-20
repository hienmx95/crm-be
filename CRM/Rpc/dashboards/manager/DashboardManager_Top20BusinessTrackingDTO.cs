using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManager_Top20BusinessTrackingDTO : DataDTO
    {
        public DashboardManager_AppUserDTO AppUser { get; set; }
        public decimal Revenue { get; set; }
        public long Call { get; set; }
        public long OpportunityWin { get; set; }
        public long Ticket { get; set; }
    }

    public class DashboardManager_Top20BusinessTrackingFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
    }
}
