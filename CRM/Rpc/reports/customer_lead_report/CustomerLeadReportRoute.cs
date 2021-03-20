using CRM.Common;
using System.Collections.Generic;

namespace CRM.Rpc.reports.customer_lead_report
{
    public class CustomerLeadReportRoute : Root
    {
        public const string Parent = Module + "/reports";
        public const string Master = Module + "/reports/customer-lead-report/customer-lead-report-master";

        private const string Default = Rpc + Module + "/customer-lead-report";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Export = Default + "/export";

        public const string FilterListCustomerLeadStatus = Default + "/filter-list-customer-lead-status";
        public const string FilterListCustomerLeadSource = Default + "/filter-list-customer-lead-source";
        public const string FilterListAppUser = Default + "/filter-list-app-user";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(CustomerLeadReport_CustomerLeadReportFilterDTO.LeadName), FieldTypeEnum.ID.Id },
            { nameof(CustomerLeadReport_CustomerLeadReportFilterDTO.LeadPhoneNumber), FieldTypeEnum.ID.Id },
            { nameof(CustomerLeadReport_CustomerLeadReportFilterDTO.LeadEmail), FieldTypeEnum.ID.Id },
            { nameof(CustomerLeadReport_CustomerLeadReportFilterDTO.LeadStatusId), FieldTypeEnum.ID.Id },
            { nameof(CustomerLeadReport_CustomerLeadReportFilterDTO.LeadSourceId), FieldTypeEnum.ID.Id },
            { nameof(CustomerLeadReport_CustomerLeadReportFilterDTO.LeadStaffId), FieldTypeEnum.ID.Id },
            { nameof(CustomerLeadReport_CustomerLeadReportFilterDTO.LeadTime), FieldTypeEnum.ID.Id },
            { nameof(CurrentContext.UserId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List,
                FilterListCustomerLeadStatus,FilterListCustomerLeadSource,FilterListAppUser  } },
             { "Export", new List<string> {
                Parent,
                Master, Count, List, Export,
                FilterListCustomerLeadStatus,FilterListCustomerLeadSource,FilterListAppUser  } },

        };
    }
}
