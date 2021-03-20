using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CRM.Rpc.reports.contract_report
{
    public class ContractReport_ContractReportDTO : DataDTO
    {
        public List<ContractReport_ContractDTO> Contracts { get; set; }
    }

    public class ContractReport_ContractReportFilterDTO : FilterDTO
    {
        public StringFilter Code { get; set; }
        public IdFilter ContractTypeId { get; set; }
        public DecimalFilter TotalValue { get; set; }
        public DecimalFilter PaymentPercentage { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter UserId { get; set; }
        public DateFilter ExpirationDate { get; set; }
        public DateFilter CreatedAt { get; set; }
    }

}
