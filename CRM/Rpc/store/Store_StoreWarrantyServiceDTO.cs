using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreWarrantyServiceDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public long StoreId { get; set; }
        public Store_StoreWarrantyServiceDTO()
        {

        }
        public Store_StoreWarrantyServiceDTO(StoreWarrantyService StoreWarrantyService)
        {
            this.Id = StoreWarrantyService.Id;
            this.Name = StoreWarrantyService.Name;
            this.Detail = StoreWarrantyService.Detail;
            this.StoreId = StoreWarrantyService.StoreId;
        }

    }

}
