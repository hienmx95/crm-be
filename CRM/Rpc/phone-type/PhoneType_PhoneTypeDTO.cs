using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.phone_type
{
    public class PhoneType_PhoneTypeDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }
        public PhoneType_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public PhoneType_PhoneTypeDTO() {}
        public PhoneType_PhoneTypeDTO(PhoneType PhoneType)
        {
            this.Id = PhoneType.Id;
            this.Code = PhoneType.Code;
            this.Name = PhoneType.Name;
            this.StatusId = PhoneType.StatusId;
            this.Used = PhoneType.Used;
            this.RowId = PhoneType.RowId;
            this.Status = PhoneType.Status == null ? null : new PhoneType_StatusDTO(PhoneType.Status);
            this.CreatedAt = PhoneType.CreatedAt;
            this.UpdatedAt = PhoneType.UpdatedAt;
            this.Errors = PhoneType.Errors;
        }
    }

    public class PhoneType_PhoneTypeFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter StatusId { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public PhoneTypeOrder OrderBy { get; set; }
    }
}
