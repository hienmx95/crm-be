using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerGroupingDAO
    {
        public CustomerGroupingDAO()
        {
            CustomerCustomerGroupingMappings = new HashSet<CustomerCustomerGroupingMappingDAO>();
            InverseParent = new HashSet<CustomerGroupingDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long CustomerTypeId { get; set; }
        public long? ParentId { get; set; }
        public string Path { get; set; }
        public long Level { get; set; }
        public long StatusId { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        public virtual CustomerTypeDAO CustomerType { get; set; }
        public virtual CustomerGroupingDAO Parent { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CustomerCustomerGroupingMappingDAO> CustomerCustomerGroupingMappings { get; set; }
        public virtual ICollection<CustomerGroupingDAO> InverseParent { get; set; }
    }
}
