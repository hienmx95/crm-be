using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.reports.opportunity_report
{
    public class OpportunityReport_OpportunityTotalDTO
    {
        public string TotalAmount { get; set; }
        public string TotalItem { get; set; }
        public string TotalRevenue { get; set; }
        public string TotalForecastAmount { get; set; }
    }
}
