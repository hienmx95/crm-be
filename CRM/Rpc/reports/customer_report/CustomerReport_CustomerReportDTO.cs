using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CRM.Rpc.reports.customer_report
{
    public class CustomerReport_CustomerReportFilterDTO : FilterDTO
    {
         public StringFilter Code { get; set; }
         public StringFilter Name { get; set; }
         public IdFilter CustomerTypeId { get; set; }
         public DateFilter CreatedAt { get; set; }
    }

}
