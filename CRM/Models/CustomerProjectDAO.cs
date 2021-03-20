using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CustomerProjectDAO
    {
        public CustomerProjectDAO()
        {
            CustomerProjectCallLogMappings = new HashSet<CustomerProjectCallLogMappingDAO>();
            CustomerProjectContacts = new HashSet<CustomerProjectContactDAO>();
            CustomerProjectContractMappings = new HashSet<CustomerProjectContractMappingDAO>();
            CustomerProjectEmails = new HashSet<CustomerProjectEmailDAO>();
            CustomerProjectLeaderships = new HashSet<CustomerProjectLeadershipDAO>();
            OrderProjects = new HashSet<OrderProjectDAO>();
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
        public long? CreatedById { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        public long? CurrencyId { get; set; }

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
        public virtual ICollection<CustomerProjectCallLogMappingDAO> CustomerProjectCallLogMappings { get; set; }
        public virtual ICollection<CustomerProjectContactDAO> CustomerProjectContacts { get; set; }
        public virtual ICollection<CustomerProjectContractMappingDAO> CustomerProjectContractMappings { get; set; }
        public virtual ICollection<CustomerProjectEmailDAO> CustomerProjectEmails { get; set; }
        public virtual ICollection<CustomerProjectLeadershipDAO> CustomerProjectLeaderships { get; set; }
        public virtual ICollection<OrderProjectDAO> OrderProjects { get; set; }
    }
}
