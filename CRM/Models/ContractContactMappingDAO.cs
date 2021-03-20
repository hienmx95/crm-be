using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContractContactMappingDAO
    {
        public long ContractId { get; set; }
        public long ContactId { get; set; }

        public virtual ContactDAO Contact { get; set; }
        public virtual ContractDAO Contract { get; set; }
    }
}
