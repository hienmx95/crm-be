using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.dashboards.manager
{
    public class DashboardManager_Top5RevenueByCompanyDTO : DataDTO
    {
        public string CompanyName { get; set; }
        public decimal Revenue { get; set; }
    }

    public class DashboardManager_Top5RevenueByCompanyFilterDTO : FilterDTO
    {
        public IdFilter Time { get; set; }
    }
}
