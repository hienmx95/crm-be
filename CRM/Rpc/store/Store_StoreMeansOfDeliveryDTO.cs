using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreMeansOfDeliveryDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public long? Owned { get; set; }
        public long? Rent { get; set; }
        public long? StoreId { get; set; }
        public Store_StoreDTO Store { get; set; }
        public Store_StoreMeansOfDeliveryDTO() {}
        public Store_StoreMeansOfDeliveryDTO(StoreMeansOfDelivery StoreMeansOfDelivery)
        {
            this.Id = StoreMeansOfDelivery.Id;
            this.Name = StoreMeansOfDelivery.Name;
            this.Quantity = StoreMeansOfDelivery.Quantity;
            this.Owned = StoreMeansOfDelivery.Owned;
            this.Rent = StoreMeansOfDelivery.Rent;
            this.StoreId = StoreMeansOfDelivery.StoreId;
            this.Store = StoreMeansOfDelivery.Store == null ? null : new Store_StoreDTO(StoreMeansOfDelivery.Store);
            this.Errors = StoreMeansOfDelivery.Errors;
        }
    }

    public class Store_StoreMeansOfDeliveryFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter Quantity { get; set; }
        public LongFilter Owned { get; set; }
        public LongFilter Rent { get; set; }
        public IdFilter StoreId { get; set; }
        public StoreMeansOfDeliveryOrder OrderBy { get; set; }
    }
}
