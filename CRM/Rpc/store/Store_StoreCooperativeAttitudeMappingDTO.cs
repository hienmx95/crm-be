using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreCooperativeAttitudeMappingDTO : DataDTO
    {
        public long StoreId { get; set; }
        public long CooperativeAttitudeId { get; set; }
        public CooperativeAttitude CooperativeAttitude { get; set; }

        public Store_StoreCooperativeAttitudeMappingDTO()
        {

        }

        public Store_StoreCooperativeAttitudeMappingDTO(StoreCooperativeAttitudeMapping StoreCooperativeAttitudeMapping)
        {
            this.StoreId = StoreCooperativeAttitudeMapping.StoreId;
            this.CooperativeAttitudeId = StoreCooperativeAttitudeMapping.CooperativeAttitudeId;
            this.CooperativeAttitude = StoreCooperativeAttitudeMapping.CooperativeAttitude;
        }

    }

}
