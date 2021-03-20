using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContractStatusDAO
    {
        public ContractStatusDAO()
        {
            Contracts = new HashSet<ContractDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<ContractDAO> Contracts { get; set; }
    }
}
