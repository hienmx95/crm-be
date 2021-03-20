using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.store
{
    public class Store_StoreRepresentDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long? PositionId { get; set; }
        public long StoreId { get; set; }
        public Store_PositionDTO Position { get; set; }
        public Store_StoreDTO Store { get; set; }
        public Store_StoreRepresentDTO() {}
        public Store_StoreRepresentDTO(StoreRepresent StoreRepresent)
        {
            this.Id = StoreRepresent.Id;
            this.Name = StoreRepresent.Name;
            this.DateOfBirth = StoreRepresent.DateOfBirth;
            this.Phone = StoreRepresent.Phone;
            this.Email = StoreRepresent.Email;
            this.PositionId = StoreRepresent.PositionId;
            this.StoreId = StoreRepresent.StoreId;
            this.Position = StoreRepresent.Position == null ? null : new Store_PositionDTO(StoreRepresent.Position);
            this.Store = StoreRepresent.Store == null ? null : new Store_StoreDTO(StoreRepresent.Store);
            this.Errors = StoreRepresent.Errors;
        }
    }

    public class Store_StoreRepresentFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public DateFilter DateOfBirth { get; set; }
        public StringFilter Phone { get; set; }
        public StringFilter Email { get; set; }
        public IdFilter PositionId { get; set; }
        public IdFilter StoreId { get; set; }
        public StoreRepresentOrder OrderBy { get; set; }
    }
}
