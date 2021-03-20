using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CRM.Rpc.reports.customer_lead_report
{
    public class CustomerLeadReport_CustomerLeadReportDTO : DataDTO
    {
        public List<CustomerLeadReport_CustomerLeadDTO> CustomerLeads { get; set; }
    }

    public class CustomerLeadReport_CustomerLeadReportFilterDTO : FilterDTO
    {
        public StringFilter LeadName { get; set; }
        public StringFilter LeadPhoneNumber { get; set; }
        public StringFilter LeadEmail { get; set; }
        public IdFilter LeadStatusId { get; set; }
        public IdFilter LeadSourceId { get; set; }
        public IdFilter LeadStaffId { get; set; }
        public DateFilter LeadTime { get; set; }
    }

}
