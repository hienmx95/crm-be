using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StorePersonnelDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Quantity { get; set; }
        public long? StoreId { get; set; }
        public Store_StoreDTO Store { get; set; }
        public Store_StorePersonnelDTO() {}
        public Store_StorePersonnelDTO(StorePersonnel StorePersonnel)
        {
            this.Id = StorePersonnel.Id;
            this.Name = StorePersonnel.Name;
            this.Quantity = StorePersonnel.Quantity;
            this.StoreId = StorePersonnel.StoreId;
            this.Store = StorePersonnel.Store == null ? null : new Store_StoreDTO(StorePersonnel.Store);
            this.Errors = StorePersonnel.Errors;
        }
    }

    public class Store_StorePersonnelFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public LongFilter Quantity { get; set; }
        public IdFilter StoreId { get; set; }
        public StorePersonnelOrder OrderBy { get; set; }
    }
}
