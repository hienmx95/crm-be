using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreCoverageCapacityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public long? StoreId { get; set; }
        public Store_StoreCoverageCapacityDTO()
        {

        }
        public Store_StoreCoverageCapacityDTO(StoreCoverageCapacity StoreCoverageCapacity)
        {
            this.Id = StoreCoverageCapacity.Id;
            this.Name = StoreCoverageCapacity.Name;
            this.Detail = StoreCoverageCapacity.Detail;
            this.StoreId = StoreCoverageCapacity.StoreId;
        }

    }

}
