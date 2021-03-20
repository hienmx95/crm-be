using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreMarketPriceMappingDTO : DataDTO
    {
        public long StoreId { get; set; }
        public long MarketPriceId { get; set; }

        public MarketPrice MarketPrice { get; set; }
        public Store_StoreMarketPriceMappingDTO()
        {

        }
        public Store_StoreMarketPriceMappingDTO(StoreMarketPriceMapping StoreMarketPriceMapping)
        {
            this.StoreId = StoreMarketPriceMapping.StoreId;
            this.MarketPriceId = StoreMarketPriceMapping.MarketPriceId;
            this.MarketPrice = StoreMarketPriceMapping.MarketPrice;
        }
    }

}
