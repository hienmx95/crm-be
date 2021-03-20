using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreMarketPriceMappingDAO
    {
        public long StoreId { get; set; }
        public long MarketPriceId { get; set; }

        public virtual MarketPriceDAO MarketPrice { get; set; }
        public virtual StoreDAO Store { get; set; }
    }
}
