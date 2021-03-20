using CRM.Common;
using System.Collections.Generic;

namespace CRM.Rpc.reports.contract_report
{
    public class ContractReportRoute : Root
    {
        public const string Parent = Module + "/reports";
        public const string Master = Module + "/reports/contract-report/contract-report-master";

        private const string Default = Rpc + Module + "/contract-report";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Export = Default + "/export";

        public const string FilterListContractType = Default + "/filter-list-contract-type";
        public const string FilterListCompany = Default + "/filter-list-company";
        public const string FilterListOpportunity = Default + "/filter-list-opportunity";
        public const string FilterListOrganization = Default + "/filter-list-organization";
        public const string FilterListAppUser = Default + "/filter-list-app-user";

        public static Dictionary<string, long> Filters = new Dictionary<string, long>
        {
            { nameof(ContractReport_ContractReportFilterDTO.AppUserId), FieldTypeEnum.ID.Id },
            { nameof(ContractReport_ContractReportFilterDTO.OrganizationId), FieldTypeEnum.ID.Id },
            { nameof(ContractReport_ContractReportFilterDTO.UserId), FieldTypeEnum.ID.Id },
        };

        public static Dictionary<string, List<string>> Action = new Dictionary<string, List<string>>
        {
            { "Tìm kiếm", new List<string> {
                Parent,
                Master, Count, List,
                FilterListContractType,FilterListCompany,FilterListOpportunity,FilterListOrganization,FilterListAppUser} },
             { "Export", new List<string> {
                Parent,
                Master, Count, List, Export,
                FilterListContractType,FilterListCompany,FilterListOpportunity,FilterListOrganization,FilterListAppUser} },

        };
    }
}
