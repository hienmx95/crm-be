using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreInfulenceLevelMarketMappingDTO : DataDTO
    {
        public long StoreId { get; set; }
        public long InfulenceLevelMarketId { get; set; }
        public InfulenceLevelMarket InfulenceLevelMarket { get; set; }
        public Store_StoreInfulenceLevelMarketMappingDTO()
        {

        }
        public Store_StoreInfulenceLevelMarketMappingDTO(StoreInfulenceLevelMarketMapping StoreInfulenceLevelMarketMapping)
        {
            this.StoreId = StoreInfulenceLevelMarketMapping.StoreId;
            this.InfulenceLevelMarketId = StoreInfulenceLevelMarketMapping.InfulenceLevelMarketId;
            this.InfulenceLevelMarket = StoreInfulenceLevelMarketMapping.InfulenceLevelMarket;
        }
    }

}
