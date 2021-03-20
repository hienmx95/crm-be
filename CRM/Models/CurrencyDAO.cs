using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CurrencyDAO
    {
        public CurrencyDAO()
        {
            Companies = new HashSet<CompanyDAO>();
            Contracts = new HashSet<ContractDAO>();
            CustomerLeads = new HashSet<CustomerLeadDAO>();
            Opportunities = new HashSet<OpportunityDAO>();
            StoreExtends = new HashSet<StoreExtendDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompanyDAO> Companies { get; set; }
        public virtual ICollection<ContractDAO> Contracts { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
        public virtual ICollection<OpportunityDAO> Opportunities { get; set; }
        public virtual ICollection<StoreExtendDAO> StoreExtends { get; set; }
    }
}
