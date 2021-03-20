using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_resource
{
    public class CustomerResource_CustomerResourceDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
        public bool Used { get; set; }
        public Guid RowId { get; set; }
        public CustomerResource_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerResource_CustomerResourceDTO() {}
        public CustomerResource_CustomerResourceDTO(CustomerResource CustomerResource)
        {
            this.Id = CustomerResource.Id;
            this.Code = CustomerResource.Code;
            this.Name = CustomerResource.Name;
            this.StatusId = CustomerResource.StatusId;
            this.Description = CustomerResource.Description;
            this.Used = CustomerResource.Used;
            this.RowId = CustomerResource.RowId;
            this.Status = CustomerResource.Status == null ? null : new CustomerResource_StatusDTO(CustomerResource.Status);
            this.CreatedAt = CustomerResource.CreatedAt;
            this.UpdatedAt = CustomerResource.UpdatedAt;
            this.Errors = CustomerResource.Errors;
        }
    }

    public class CustomerResource_CustomerResourceFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter StatusId { get; set; }
        public StringFilter Description { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerResourceOrder OrderBy { get; set; }
    }
}
