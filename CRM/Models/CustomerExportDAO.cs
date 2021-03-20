using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerExportDAO
    {
        public CustomerExportDAO()
        {
            CustomerExportCallLogMappings = new HashSet<CustomerExportCallLogMappingDAO>();
            CustomerExportContacts = new HashSet<CustomerExportContactDAO>();
            CustomerExportContractMappings = new HashSet<CustomerExportContractMappingDAO>();
            CustomerExportEmails = new HashSet<CustomerExportEmailDAO>();
            CustomerExportLeaderships = new HashSet<CustomerExportLeadershipDAO>();
            OrderExports = new HashSet<OrderExportDAO>();
        }

        public long CustomerId { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public long? CompanyId { get; set; }
        public string EmailOther { get; set; }
        public string Fax { get; set; }
        public decimal? BusinessCapital { get; set; }
        public string BankNumber { get; set; }
        public long? CustomerTypeId { get; set; }
        public long? CustomerGroupId { get; set; }
        public long? CustomerReSourceId { get; set; }
        public long? CustomerLevelId { get; set; }
        public string BusinessLicense { get; set; }
        public string Bank { get; set; }
        public string TaxCode { get; set; }
        public DateTime? BusinessRegistrationDate { get; set; }
        public long? BusinessTypeId { get; set; }
        public long? AppUserAssignedId { get; set; }
        public long? OrganizationId { get; set; }
        public long? StatusId { get; set; }
        public string Website { get; set; }
        public decimal? AwardPoint { get; set; }
        public decimal? SubAwardPoint { get; set; }
        public bool? Used { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        public long? CurrencyId { get; set; }
        public long? CreatedById { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public virtual AppUserDAO AppUserAssigned { get; set; }
        public virtual BusinessTypeDAO BusinessType { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual AppUserDAO CreatedBy { get; set; }
        public virtual CurrencyDAO Currency { get; set; }
        public virtual CustomerDAO Customer { get; set; }
        public virtual CustomerGroupDAO CustomerGroup { get; set; }
        public virtual CustomerLevelDAO CustomerLevel { get; set; }
        public virtual CustomerResourceDAO CustomerReSource { get; set; }
        public virtual CustomerTypeDAO CustomerType { get; set; }
        public virtual OrganizationDAO Organization { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CustomerExportCallLogMappingDAO> CustomerExportCallLogMappings { get; set; }
        public virtual ICollection<CustomerExportContactDAO> CustomerExportContacts { get; set; }
        public virtual ICollection<CustomerExportContractMappingDAO> CustomerExportContractMappings { get; set; }
        public virtual ICollection<CustomerExportEmailDAO> CustomerExportEmails { get; set; }
        public virtual ICollection<CustomerExportLeadershipDAO> CustomerExportLeaderships { get; set; }
        public virtual ICollection<OrderExportDAO> OrderExports { get; set; }
    }
}
