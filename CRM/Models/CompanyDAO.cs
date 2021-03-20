using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class CompanyDAO
    {
        public CompanyDAO()
        {
            CompanyActivities = new HashSet<CompanyActivityDAO>();
            CompanyCallLogMappings = new HashSet<CompanyCallLogMappingDAO>();
            CompanyEmails = new HashSet<CompanyEmailDAO>();
            CompanyFileGroupings = new HashSet<CompanyFileGroupingDAO>();
            Contacts = new HashSet<ContactDAO>();
            Contracts = new HashSet<ContractDAO>();
            CustomerCompanies = new HashSet<CustomerDAO>();
            CustomerLeads = new HashSet<CustomerLeadDAO>();
            CustomerParentCompanies = new HashSet<CustomerDAO>();
            InverseParent = new HashSet<CompanyDAO>();
            Opportunities = new HashSet<OpportunityDAO>();
            OrderQuotes = new HashSet<OrderQuoteDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string FAX { get; set; }
        public string PhoneOther { get; set; }
        public string Email { get; set; }
        public string EmailOther { get; set; }
        public string ZIPCode { get; set; }
        public decimal? Revenue { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? NumberOfEmployee { get; set; }
        public bool? RefuseReciveEmail { get; set; }
        public bool? RefuseReciveSMS { get; set; }
        public long? CustomerLeadId { get; set; }
        public long? ParentId { get; set; }
        public string Path { get; set; }
        public long? Level { get; set; }
        public long? ProfessionId { get; set; }
        public long? AppUserId { get; set; }
        public long CreatorId { get; set; }
        public long? CurrencyId { get; set; }
        public long? CompanyStatusId { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CompanyStatusDAO CompanyStatus { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual CurrencyDAO Currency { get; set; }
        public virtual CustomerLeadDAO CustomerLead { get; set; }
        public virtual DistrictDAO District { get; set; }
        public virtual NationDAO Nation { get; set; }
        public virtual CompanyDAO Parent { get; set; }
        public virtual ProfessionDAO Profession { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual ICollection<CompanyActivityDAO> CompanyActivities { get; set; }
        public virtual ICollection<CompanyCallLogMappingDAO> CompanyCallLogMappings { get; set; }
        public virtual ICollection<CompanyEmailDAO> CompanyEmails { get; set; }
        public virtual ICollection<CompanyFileGroupingDAO> CompanyFileGroupings { get; set; }
        public virtual ICollection<ContactDAO> Contacts { get; set; }
        public virtual ICollection<ContractDAO> Contracts { get; set; }
        public virtual ICollection<CustomerDAO> CustomerCompanies { get; set; }
        public virtual ICollection<CustomerLeadDAO> CustomerLeads { get; set; }
        public virtual ICollection<CustomerDAO> CustomerParentCompanies { get; set; }
        public virtual ICollection<CompanyDAO> InverseParent { get; set; }
        public virtual ICollection<OpportunityDAO> Opportunities { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuotes { get; set; }
    }
}
