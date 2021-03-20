using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CRM.Rpc.reports.staff_report
{
    public class StaffReport_StaffReportFilterDTO : FilterDTO
    {
         public StringFilter Username { get; set; }
         public IdFilter StaffId { get; set; }
         public IdFilter OrganizationId { get; set; }
         public DateFilter CreatedAt { get; set; }
    }

}
