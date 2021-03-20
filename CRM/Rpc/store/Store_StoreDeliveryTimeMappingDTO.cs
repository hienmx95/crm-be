using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreDeliveryTimeMappingDTO : DataDTO
    {
        public long StoreId { get; set; }
        public long StoreDeliveryTimeId { get; set; }

        public Store_StoreDeliveryTimeDTO StoreDeliveryTime { get; set; }

        public Store_StoreDeliveryTimeMappingDTO()
        {

        }
        public Store_StoreDeliveryTimeMappingDTO(StoreDeliveryTimeMapping StoreDeliveryTimeMapping)
        {
            this.StoreId = StoreDeliveryTimeMapping.StoreId;
            this.StoreDeliveryTimeId = StoreDeliveryTimeMapping.StoreDeliveryTimeId;
            this.StoreDeliveryTime = StoreDeliveryTimeMapping.StoreDeliveryTime == null ? null : new Store_StoreDeliveryTimeDTO(StoreDeliveryTimeMapping.StoreDeliveryTime);
        }
    }

}
