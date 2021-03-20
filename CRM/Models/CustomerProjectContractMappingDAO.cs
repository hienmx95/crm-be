using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerProjectContractMappingDAO
    {
        public long CustomerProjectId { get; set; }
        public long ContractId { get; set; }

        public virtual ContractDAO Contract { get; set; }
        public virtual CustomerProjectDAO CustomerProject { get; set; }
    }
}
