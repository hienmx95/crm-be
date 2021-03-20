using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class MarketPriceDAO
    {
        public MarketPriceDAO()
        {
            StoreMarketPriceMappings = new HashSet<StoreMarketPriceMappingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StoreMarketPriceMappingDAO> StoreMarketPriceMappings { get; set; }
    }
}
