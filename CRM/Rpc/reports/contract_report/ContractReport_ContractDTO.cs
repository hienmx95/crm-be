using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.reports.contract_report
{
    public class ContractReport_ContractDTO : DataDTO
    {
        public string STT { get; set; }
        public string ContractTypeName { get; set; }
        public string Total { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

}
