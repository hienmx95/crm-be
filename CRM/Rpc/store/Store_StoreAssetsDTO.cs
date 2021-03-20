using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreAssetsDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public long? Owned { get; set; }
        public long? Rent { get; set; }
        public long? StoreId { get; set; }
        public Store_StoreDTO Store { get; set; }
        public Store_StoreAssetsDTO() {}
        public Store_StoreAssetsDTO(StoreAssets StoreAssets)
        {
            this.Id = StoreAssets.Id;
            this.Name = StoreAssets.Name;
            this.Quantity = StoreAssets.Quantity;
            this.Owned = StoreAssets.Owned;
            this.Rent = StoreAssets.Rent;
            this.StoreId = StoreAssets.StoreId;
            this.Store = StoreAssets.Store == null ? null : new Store_StoreDTO(StoreAssets.Store);
            this.Errors = StoreAssets.Errors;
        }
    }

    public class Store_StoreAssetsFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter Quantity { get; set; }
        public LongFilter Owned { get; set; }
        public LongFilter Rent { get; set; }
        public IdFilter StoreId { get; set; }
        public StoreAssetsOrder OrderBy { get; set; }
    }
}
