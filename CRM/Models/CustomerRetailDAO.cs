using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerRetailDAO
    {
        public CustomerRetailDAO()
        {
            CustomerRetailCallLogMappings = new HashSet<CustomerRetailCallLogMappingDAO>();
            CustomerRetailContractMappings = new HashSet<CustomerRetailContractMappingDAO>();
            CustomerRetailEmails = new HashSet<CustomerRetailEmailDAO>();
            CustomerRetailRepairTicketMappings = new HashSet<CustomerRetailRepairTicketMappingDAO>();
            CustomerRetailTicketMappings = new HashSet<CustomerRetailTicketMappingDAO>();
            OrderRetails = new HashSet<OrderRetailDAO>();
        }

        public long CustomerId { get; set; }
        public long? CustomerTypeId { get; set; }
        public long? CustomerGroupId { get; set; }
        public long? CustomerResourceId { get; set; }
        public long? CustomerLevelId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string TaxCode { get; set; }
        public string DeliveryAddress { get; set; }
        public long? SexId { get; set; }
        public long? StatusId { get; set; }
        public long? AssignedAppUserId { get; set; }
        public decimal? AwardPoint { get; set; }
        public decimal? SubAwardPoint { get; set; }
        public bool? Used { get; set; }
        public long? CreatedById { get; set; }
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

        public virtual AppUserDAO AssignedAppUser { get; set; }
        public virtual AppUserDAO CreatedBy { get; set; }
        public virtual CustomerDAO Customer { get; set; }
        public virtual CustomerGroupDAO CustomerGroup { get; set; }
        public virtual CustomerLevelDAO CustomerLevel { get; set; }
        public virtual CustomerResourceDAO CustomerResource { get; set; }
        public virtual CustomerTypeDAO CustomerType { get; set; }
        public virtual SexDAO Sex { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CustomerRetailCallLogMappingDAO> CustomerRetailCallLogMappings { get; set; }
        public virtual ICollection<CustomerRetailContractMappingDAO> CustomerRetailContractMappings { get; set; }
        public virtual ICollection<CustomerRetailEmailDAO> CustomerRetailEmails { get; set; }
        public virtual ICollection<CustomerRetailRepairTicketMappingDAO> CustomerRetailRepairTicketMappings { get; set; }
        public virtual ICollection<CustomerRetailTicketMappingDAO> CustomerRetailTicketMappings { get; set; }
        public virtual ICollection<OrderRetailDAO> OrderRetails { get; set; }
    }
}
