using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerAgentContractMappingDAO
    {
        public long CustomerAgentId { get; set; }
        public long ContractId { get; set; }

        public virtual ContractDAO Contract { get; set; }
        public virtual CustomerAgentDAO CustomerAgent { get; set; }
    }
}
