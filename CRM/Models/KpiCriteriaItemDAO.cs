using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class KpiCriteriaItemDAO
    {
        public KpiCriteriaItemDAO()
        {
            KpiItemContentKpiCriteriaItemMappings = new HashSet<KpiItemContentKpiCriteriaItemMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<KpiItemContentKpiCriteriaItemMappingDAO> KpiItemContentKpiCriteriaItemMappings { get; set; }
    }
}
