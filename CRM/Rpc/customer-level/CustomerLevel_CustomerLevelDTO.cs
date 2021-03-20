using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_level
{
    public class CustomerLevel_CustomerLevelDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public long PointFrom { get; set; }
        public long PointTo { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }
        public CustomerLevel_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerLevel_CustomerLevelDTO() {}
        public CustomerLevel_CustomerLevelDTO(CustomerLevel CustomerLevel)
        {
            this.Id = CustomerLevel.Id;
            this.Code = CustomerLevel.Code;
            this.Name = CustomerLevel.Name;
            this.Color = CustomerLevel.Color;
            this.PointFrom = CustomerLevel.PointFrom;
            this.PointTo = CustomerLevel.PointTo;
            this.StatusId = CustomerLevel.StatusId;
            this.Description = CustomerLevel.Description;
            this.Used = CustomerLevel.Used;
            this.RowId = CustomerLevel.RowId;
            this.Status = CustomerLevel.Status == null ? null : new CustomerLevel_StatusDTO(CustomerLevel.Status);
            this.CreatedAt = CustomerLevel.CreatedAt;
            this.UpdatedAt = CustomerLevel.UpdatedAt;
            this.Errors = CustomerLevel.Errors;
        }
    }

    public class CustomerLevel_CustomerLevelFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Color { get; set; }
        public LongFilter PointFrom { get; set; }
        public LongFilter PointTo { get; set; }
        public IdFilter StatusId { get; set; }
        public StringFilter Description { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerLevelOrder OrderBy { get; set; }
    }
}
