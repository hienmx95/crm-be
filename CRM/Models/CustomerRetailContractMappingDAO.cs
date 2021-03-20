using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerRetailContractMappingDAO
    {
        public long CustomerRetailId { get; set; }
        public long ContractId { get; set; }

        public virtual ContractDAO Contract { get; set; }
        public virtual CustomerRetailDAO CustomerRetail { get; set; }
    }
}
