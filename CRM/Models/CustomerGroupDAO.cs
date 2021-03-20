using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerGroupDAO
    {
        public CustomerGroupDAO()
        {
            CustomerAgents = new HashSet<CustomerAgentDAO>();
            CustomerExports = new HashSet<CustomerExportDAO>();
            CustomerProjects = new HashSet<CustomerProjectDAO>();
            CustomerRetails = new HashSet<CustomerRetailDAO>();
            InverseParent = new HashSet<CustomerGroupDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public long? TypeId { get; set; }
        public long Level { get; set; }
        public long? StatusId { get; set; }
        public string Color { get; set; }
        public long? DisplayOrder { get; set; }
        public string Description { get; set; }
        public long? ParentId { get; set; }
        public decimal? Credit { get; set; }
        public decimal? CreditGroupParent { get; set; }
        public decimal? CreditGroupMember { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public virtual CustomerGroupDAO Parent { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual CustomerTypeDAO Type { get; set; }
        public virtual ICollection<CustomerAgentDAO> CustomerAgents { get; set; }
        public virtual ICollection<CustomerExportDAO> CustomerExports { get; set; }
        public virtual ICollection<CustomerProjectDAO> CustomerProjects { get; set; }
        public virtual ICollection<CustomerRetailDAO> CustomerRetails { get; set; }
        public virtual ICollection<CustomerGroupDAO> InverseParent { get; set; }
    }
}
