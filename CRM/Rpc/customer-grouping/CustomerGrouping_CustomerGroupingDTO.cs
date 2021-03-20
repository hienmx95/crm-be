using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_grouping
{
    public class CustomerGrouping_CustomerGroupingDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long CustomerTypeId { get; set; }
        public long? ParentId { get; set; }
        public string Path { get; set; }
        public long Level { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
        public CustomerGrouping_CustomerTypeDTO CustomerType { get; set; }
        public CustomerGrouping_CustomerGroupingDTO Parent { get; set; }
        public CustomerGrouping_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CustomerGrouping_CustomerGroupingDTO() {}
        public CustomerGrouping_CustomerGroupingDTO(CustomerGrouping CustomerGrouping)
        {
            this.Id = CustomerGrouping.Id;
            this.Code = CustomerGrouping.Code;
            this.Name = CustomerGrouping.Name;
            this.CustomerTypeId = CustomerGrouping.CustomerTypeId;
            this.ParentId = CustomerGrouping.ParentId;
            this.Path = CustomerGrouping.Path;
            this.Level = CustomerGrouping.Level;
            this.StatusId = CustomerGrouping.StatusId;
            this.Description = CustomerGrouping.Description;
            this.CustomerType = CustomerGrouping.CustomerType == null ? null : new CustomerGrouping_CustomerTypeDTO(CustomerGrouping.CustomerType);
            this.Parent = CustomerGrouping.Parent == null ? null : new CustomerGrouping_CustomerGroupingDTO(CustomerGrouping.Parent);
            this.Status = CustomerGrouping.Status == null ? null : new CustomerGrouping_StatusDTO(CustomerGrouping.Status);
            this.CreatedAt = CustomerGrouping.CreatedAt;
            this.UpdatedAt = CustomerGrouping.UpdatedAt;
            this.Errors = CustomerGrouping.Errors;
        }
    }

    public class CustomerGrouping_CustomerGroupingFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        public IdFilter ParentId { get; set; }
        public StringFilter Path { get; set; }
        public LongFilter Level { get; set; }
        public IdFilter StatusId { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerGroupingOrder OrderBy { get; set; }
    }
}
