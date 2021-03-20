using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerExportContractMappingDAO
    {
        public long CustomerExportId { get; set; }
        public long ContractId { get; set; }

        public virtual ContractDAO Contract { get; set; }
        public virtual CustomerExportDAO CustomerExport { get; set; }
    }
}
