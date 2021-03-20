using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class InfulenceLevelMarketDAO
    {
        public InfulenceLevelMarketDAO()
        {
            StoreInfulenceLevelMarketMappings = new HashSet<StoreInfulenceLevelMarketMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StoreInfulenceLevelMarketMappingDAO> StoreInfulenceLevelMarketMappings { get; set; }
    }
}
