using CRM.Common;
using System.Collections.Generic;

namespace CRM.Rpc.kpi_tracking.kpi_item_report
{
    public class KpiItemReport_KpiItemContentKpiCriteriaItemMappingDTO : DataDTO
    {
        public long SaleEmployeeId { get; set; }
        public long ItemId { get; set; }
        public long KpiCriteriaItemId { get; set; }
        public long? Value { get; set; }
    }
}
