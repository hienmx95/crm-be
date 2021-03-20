using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreInfulenceLevelMarketMappingDAO
    {
        public long StoreId { get; set; }
        public long InfulenceLevelMarketId { get; set; }

        public virtual InfulenceLevelMarketDAO InfulenceLevelMarket { get; set; }
        public virtual StoreDAO Store { get; set; }
    }
}
