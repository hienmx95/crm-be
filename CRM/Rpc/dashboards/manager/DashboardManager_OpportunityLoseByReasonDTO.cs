using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManager_OpportunityLoseByReasonDTO : DataDTO
    {
        public decimal Revenue { get; set; }
        public List<DashboardManager_OpportunityLoseRevenueByReasonDTO> OpportunityLoseRevenueByReasons { get; set; }
    }

    public class DashboardManager_OpportunityLoseRevenueByReasonDTO : DataDTO
    {
        public string Reason { get; set; }
        public decimal Revenue { get; set; }
    }
    public class DashboardManager_OpportunityLoseByReasonFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
    }
}
