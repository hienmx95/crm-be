using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class FileTypeDAO
    {
        public FileTypeDAO()
        {
            CompanyFileGroupings = new HashSet<CompanyFileGroupingDAO>();
            ContactFileGroupings = new HashSet<ContactFileGroupingDAO>();
            ContractFileGroupings = new HashSet<ContractFileGroupingDAO>();
            CustomerLeadFileGroups = new HashSet<CustomerLeadFileGroupDAO>();
            OpportunityFileGroupings = new HashSet<OpportunityFileGroupingDAO>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<CompanyFileGroupingDAO> CompanyFileGroupings { get; set; }
        public virtual ICollection<ContactFileGroupingDAO> ContactFileGroupings { get; set; }
        public virtual ICollection<ContractFileGroupingDAO> ContractFileGroupings { get; set; }
        public virtual ICollection<CustomerLeadFileGroupDAO> CustomerLeadFileGroups { get; set; }
        public virtual ICollection<OpportunityFileGroupingDAO> OpportunityFileGroupings { get; set; }
    }
}
